using System;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Charts
{
    public class EvaluationEventArgs : EventArgs
    {
        public Evaluation Sample { get; set; }

        public EvaluationEventArgs(Evaluation evaluation)
        {
            Sample = evaluation;
        }
    }

    public delegate void EvaluationEventHandler(object sender, EvaluationEventArgs e);
}
