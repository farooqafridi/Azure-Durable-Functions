using Azure_Durable_Functions.FunctionChaining;
using Microsoft.Azure.Functions.Worker;

public class CreateShipment
{
    [Function(nameof(CreateShipment))]
    public ShipmentResult Run(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine($"Creating shipment for {order.OrderId}");

        // Normally call shipping API here.

        return new ShipmentResult(
            order.OrderId,
            "Shipment created");
    }
}