using System;
using System.Collections.Generic;
using System.Text;

namespace Azure_Durable_Functions.HIT
{
    public record Order(
    int OrderId,
    int CustomerId,
    decimal Amount);
}
