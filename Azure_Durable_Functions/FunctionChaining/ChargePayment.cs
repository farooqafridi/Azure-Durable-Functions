using Azure_Durable_Functions.FunctionChaining;
using Microsoft.Azure.Functions.Worker;

public class ChargePayment
{
    [Function(nameof(ChargePayment))]
    public PaymentResult Run(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine($"Charging ${order.Amount}");

        // Normally call payment service here.

        return new PaymentResult(
            order.OrderId,
            "Payment successful");
    }
}