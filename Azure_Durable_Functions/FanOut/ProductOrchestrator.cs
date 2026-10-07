using Azure_Durable_Functions.FanOut;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

public class ProductOrchestrator
{
    [Function(nameof(ProductOrchestrator))]
    public async Task<List<string>> Run(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var products = new List<Product>
        {
            new(1, "Laptop"),
            new(2, "Mouse"),
            new(3, "Keyboard"),
            new(4, "Monitor"),
            new(5, "Headset")
        };

        var tasks = new List<Task<string>>();

        // FAN-OUT
        foreach (var product in products)
        {
            tasks.Add(
                context.CallActivityAsync<string>(
                    nameof(ProcessProduct),
                    product));
        }

        // FAN-IN
        var results = await Task.WhenAll(tasks);

        return results.ToList();
    }
}