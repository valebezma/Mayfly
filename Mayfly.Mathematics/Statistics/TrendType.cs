using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Numerics;
using RDotNet;

namespace Mayfly.Mathematics.Statistics
{
    public enum TrendType
    {
        None = -1,
        Auto,

        Linear,
        Quadratic,
        Cubic,

        Power,
        Exponential,
        Logarithmic,
        Logistic,

        Growth
    };
}
