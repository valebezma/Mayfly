using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using System.Globalization;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;


namespace Mayfly.Fish.Explorer
{
    public class ExponentialMortalityModel
    {
        public Scatterplot Unexploited;

        public Scatterplot Exploited { get; private set; }

        public double Z { get; private set; }

        public double Fi { get; private set; }

        public double S { get; private set; }

        public AgeGroup YoungestCaught { get; private set; }



        private ExponentialMortalityModel()
        { }



        public static ExponentialMortalityModel FromComposition(Composition ageComposition, int fullExploitationIndex)
        {
            ExponentialMortalityModel result = new ExponentialMortalityModel();

            Scatterplot[] res = ageComposition.GetCatchCurve(fullExploitationIndex);

            result.Unexploited = res[0];
            result.Exploited = res[1];

            result.YoungestCaught = (AgeGroup)ageComposition[fullExploitationIndex];

            result.Exploited.Properties.ShowTrend = true;
            result.Exploited.Properties.SelectedApproximationType = TrendType.Exponential;
            result.Exploited.CalculateApproximation(TrendType.Exponential);

            if (result.Exploited.IsRegressionOK)
            {
                result.Z = -result.Exploited.Regression.Parameters[1];
                result.S = Math.Exp(-result.Z);
                result.Fi = 1 - result.S;
                return result;
            }
            else
            {
                return null;
            }

        }

        public static ExponentialMortalityModel FromComposition(Composition ageComposition)
        {
            return FromComposition(ageComposition, ageComposition.IndexOf(ageComposition.MostAbundant));
        }
    }
}
