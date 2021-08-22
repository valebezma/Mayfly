using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Statistics
{
    public class BivariatePredictiveModel
    {
        public string Name { get; set; }

        public BivariateSample Data { get; set; }

        public Regression Regression { get; set; }

        public bool IsRegressionOK { get { return Regression != null; } }

        public TrendType TrendType = TrendType.Auto;



        public BivariatePredictiveModel(BivariateSample data, TrendType trendType)
        {
            Data = data;
            TrendType = trendType;
            Calculate();
        }



        public void Calculate()
        {
            Regression = Regression.GetRegression(Data, TrendType);
        }

        public void Calculate(TrendType trendType)
        {
            TrendType = trendType;
            Calculate();
        }
    }
}
