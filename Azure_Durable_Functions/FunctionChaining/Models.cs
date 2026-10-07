using System;
using System.Collections.Generic;
using System.Text;

namespace Azure_Durable_Functions.FunctionChaining
{
    public record Order(
     string OrderId,
     decimal Amount);

    public record OrderResult(
        string OrderId,
        string Status);

    public record PaymentResult(
        string OrderId,
        string Status);

    public record ShipmentResult(
        string OrderId,
        string Status);
}
