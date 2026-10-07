using System.Net;
using System.Text.Json;
using Azure_Durable_Functions.HIT;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;

namespace OrderFunctions.Functions;

public class StartOrder
{
    [Function(nameof(StartOrder))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "post",
            Route = "orders")]
        HttpRequestData req,

        [DurableClient]
        DurableTaskClient client)
    {
        var order =
            await JsonSerializer.DeserializeAsync<Order>(
                req.Body);

        if (order is null)
        {
            var badResponse =
                req.CreateResponse(HttpStatusCode.BadRequest);

            await badResponse.WriteStringAsync(
                "Invalid order.");

            return badResponse;
        }

        string instanceId =
            await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(OrderOrchestrator),
                order);

        return await client.CreateCheckStatusResponseAsync(
            req,
            instanceId);
    }
}