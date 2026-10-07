using Azure_Durable_Functions.FunctionChaining;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

public class ValidateOrder
{
    [Function(nameof(ValidateOrder))]
    public OrderResult Run(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine($"Validating order {order.OrderId}");

        if (order.Amount <= 0)
            throw new InvalidOperationException("Invalid order amount.");

        return new OrderResult(
            order.OrderId,
            "Validated");
    }
}