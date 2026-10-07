using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Azure_Durable_Functions.HIT
{
    public class ApproveOrder
    {
        [Function(nameof(ApproveOrder))]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(
            AuthorizationLevel.Function,
            "post",
            Route = "orders/{instanceId}/approval")]
        HttpRequestData req,

            string instanceId,

          [DurableClient]
        DurableTaskClient client)
        {
            bool approved = true;

            await client.RaiseEventAsync(
                instanceId,
                "ManagerApproval",
                approved);

            var response =
                req.CreateResponse(HttpStatusCode.OK);

            await response.WriteStringAsync(
                $"Order instance {instanceId} approved.");

            return response;
        }
    }
}
