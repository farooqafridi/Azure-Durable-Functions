using Azure_Durable_Functions.AsyncApi;
using Microsoft.Azure.Functions.Worker;

namespace OrderFunctions.Functions;

public class OrderActivities
{
    [Function(nameof(ValidateOrder))]
    public async Task ValidateOrder(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine(
            $"Validating order {order.OrderId}");

        await Task.Delay(2000);

        if (order.Amount <= 0)
        {
            throw new InvalidOperationException(
                "Order amount must be greater than zero.");
        }

        Console.WriteLine(
            $"Order {order.OrderId} validated.");
    }


    [Function(nameof(ProcessPayment))]
    public async Task ProcessPayment(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine(
            $"Processing payment for order {order.OrderId}");

        await Task.Delay(5000);

        Console.WriteLine(
            $"Payment completed for order {order.OrderId}");
    }


    [Function(nameof(ReserveInventory))]
    public async Task ReserveInventory(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine(
            $"Reserving inventory for order {order.OrderId}");

        await Task.Delay(4000);

        Console.WriteLine(
            $"Inventory reserved for order {order.OrderId}");
    }


    [Function(nameof(CreateShipment))]
    public async Task CreateShipment(
        [ActivityTrigger] Order order)
    {
        Console.WriteLine(
            $"Creating shipment for order {order.OrderId}");

        await Task.Delay(3000);

        Console.WriteLine(
            $"Shipment created for order {order.OrderId}");
    }
}