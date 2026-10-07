using Azure_Durable_Functions.FunctionChaining;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

public class OrderOrchestrator
{
    [Function(nameof(OrderOrchestrator))]
    public async Task<OrderResult> Run(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var order = context.GetInput<Order>();

        // Step 1
        var validation =
            await context.CallActivityAsync<OrderResult>(
                nameof(ValidateOrder),
                order);

        // Step 2
        var payment =
            await context.CallActivityAsync<PaymentResult>(
                nameof(ChargePayment),
                order);

        // Step 3
        var shipment =
            await context.CallActivityAsync<ShipmentResult>(
                nameof(CreateShipment),
                order);

        return new OrderResult(
            order!.OrderId,
            $"Completed: {validation.Status}, " +
            $"{payment.Status}, " +
            $"{shipment.Status}");
    }
}