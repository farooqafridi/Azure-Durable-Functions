using Azure_Durable_Functions.FanOut;
using Microsoft.Azure.Functions.Worker;

public class ProcessProduct
{
    [Function(nameof(ProcessProduct))]
    public async Task<string> Run(
        [ActivityTrigger] Product product)
    {
        Console.WriteLine(
            $"Processing {product.ProductId}");

        // Simulate API/database processing
        await Task.Delay(2000);

        return $"Processed {product.ProductId}";
    }
}