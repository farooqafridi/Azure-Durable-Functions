using Azure_Durable_Functions.AsyncApi;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace OrderFunctions.Functions;

public class OrderOrchestrator
{
    [Function(nameof(OrderOrchestrator))]
    public async Task<string> Run(
        [OrchestrationTrigger]
        TaskOrchestrationContext context)
    {
        var order =
            context.GetInput<Order>();

        if (order is null)
        {
            throw new InvalidOperationException(
                "Order was not supplied.");
        }

        await context.CallActivityAsync(
            nameof(OrderActivities.ValidateOrder),
            order);

        await context.CallActivityAsync(
            nameof(OrderActivities.ProcessPayment),
            order);

        await context.CallActivityAsync(
            nameof(OrderActivities.ReserveInventory),
            order);

        await context.CallActivityAsync(
            nameof(OrderActivities.CreateShipment),
            order);

        return $"Order {order.OrderId} completed.";
    }
}