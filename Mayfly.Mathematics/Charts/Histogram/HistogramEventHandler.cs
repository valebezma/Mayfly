using System;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Charts
{
    public class HistogramEventArgs : EventArgs
    {
        public Histogramma Sample { get; set; }

        public HistogramEventArgs(Histogramma histogram)
        {
            Sample = histogram;
        }
    }

    public delegate void HistogramEventHandler(object sender, HistogramEventArgs e);
}
