using Azure_Durable_Functions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;

public class StartOrder
{
    [Function(nameof(StartOrder))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequestData req,
        [DurableClient] DurableTaskClient client)
    {
        var order = new Order(
            "ORD-1001",
            500);

        string instanceId =
            await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(OrderOrchestrator),
                order);

        return await client.CreateCheckStatusResponseAsync(
            req,
            instanceId);
    }
}