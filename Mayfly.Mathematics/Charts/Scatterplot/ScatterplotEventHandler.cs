using System;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Charts
{
    public class ScatterplotEventArgs : EventArgs
    {
        public Scatterplot Sample { get; set; }

        public ScatterplotEventArgs(Scatterplot scatterplot)
        {
            Sample = scatterplot;
        }
    }

    public delegate void ScatterplotEventHandler(object sender, ScatterplotEventArgs e);
}
