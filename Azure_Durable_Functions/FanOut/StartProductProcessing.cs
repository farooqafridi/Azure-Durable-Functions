using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;

public class StartProductProcessing
{
    [Function(nameof(StartProductProcessing))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequestData req,
        [DurableClient] DurableTaskClient client)
    {
        string instanceId =
            await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(ProductOrchestrator));

        return await client.CreateCheckStatusResponseAsync(
            req,
            instanceId);
    }
}