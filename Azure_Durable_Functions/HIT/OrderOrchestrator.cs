using Azure_Durable_Functions.HIT;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;


namespace Azure_Durable_Functions.HIT
{
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

            // Step 1
            await context.CallActivityAsync(
                nameof(OrderActivities.ValidateOrder),
                order);

            // Step 2
            // Wait for manager approval
            bool approved =
                await context.WaitForExternalEvent<bool>(
                    "ManagerApproval");

            // Step 3
            if (!approved)
            {
                return $"Order {order.OrderId} rejected.";
            }

            // Step 4
            await context.CallActivityAsync(
                nameof(OrderActivities.ProcessPayment),
                order);

            // Step 5
            await context.CallActivityAsync(
                nameof(OrderActivities.CreateShipment),
                order);

            return $"Order {order.OrderId} completed.";
        }
    }
}