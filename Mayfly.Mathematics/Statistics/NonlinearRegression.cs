using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Mayfly.Extensions;
using RDotNet;
using Meta.Numerics;

namespace Mayfly.Mathematics.Statistics
{
    public abstract class NonlinearRegression : Regression
    {
        public NonlinearRegression(BivariateSample data)
            : base(data)
        {
            Parameters.AddRange(GetInitials());
            engine.InstallRequiredPackages("investr");
        }

        public NonlinearRegression(BivariateSample data, TrendType _type)
            : this(data)
        {
            type = _type;
            //fit = data.NonlinearRegression(Predict, this.GetInitials());
        }

        public abstract double[] GetInitials();

        public abstract double Predict(IReadOnlyList<double> p, double t);

        public override double Predict(double x)
        {
            return Predict(Parameters, x);
        }

        internal override Interval[] GetBands(double[] x, double level, IntervalType type)
        {
            var xvalues = engine.CreateNumericVector(x);
            engine.SetSymbol("axis", xvalues);

            var alpha = engine.CreateNumeric(level);
            engine.SetSymbol("alpha", alpha);

            engine.Evaluate("require(investr)");
            engine.SetSymbol("fit", fit);
            NumericMatrix predictions = engine.Evaluate("predFit(fit, data.frame(xvalues = axis), interval = '" + type.ToString().ToLower() + "', level = alpha)").AsNumericMatrix();
            List<Interval> result = new List<Interval>();
            for (int i = 0; i < predictions.RowCount; i++)
            {
                result.Add(Interval.FromEndpoints(predictions[i, 1], predictions[i, 2]));
            }
            return result.ToArray();
        }
    }

    public class Logistic : NonlinearRegression
    {
        public double L { get { return Parameters[0]; } }

        public double K { get { return Parameters[1]; } }

        public double X0 { get { return Parameters[2]; } }

        public Logistic(BivariateSample data)
            : base(data, TrendType.Logistic)
        {
            var xvalues = engine.CreateNumericVector(data.X);
            engine.SetSymbol("xvalues", xvalues);

            var yvalues = engine.CreateNumericVector(data.Y);
            engine.SetSymbol("yvalues", yvalues);

            var starts = engine.CreateNumericVector(Parameters);
            engine.SetSymbol("starts", starts);

            fit = engine.Evaluate("nls(yvalues ~ L / (1.0 + exp(-K * (xvalues - x0))), start = c('L' = starts[1], 'K' = starts[2], 'x0' = starts[3]))");
            engine.SetSymbol("fit", fit);
            Parameters.Clear();
            Parameters.AddRange(engine.Evaluate("params = coef(fit)").AsNumeric());

            ResidualStandardError = engine.Evaluate("summary(fit)$sigma").AsNumeric()[0];
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + @" = \frac{" + L.ToString(format) + "}{ 1 + e^{-" +
                   K.ToString(format) + " (x - " + X0.ToString(format) + ")}}";

        }

        public override double Predict(IReadOnlyList<double> p, double x)
        {
            return p[0] / (1.0 + Math.Exp(-p[1] * (x - p[2])));
        }

        public override double PredictInversed(double y)
        {
            return X0 + Math.Log(L / y - 1) / -K;
        }

        public override double[] GetInitials()
        {
            return new double[] { 1.0, 1.0, 0.0 };
        }
    }

    public class Growth : NonlinearRegression
    {
        public double Linf { get { return Parameters[0]; } }

        public double K { get { return Parameters[1]; } }

        public double T0 { get { return Parameters[2]; } }

        public Growth(BivariateSample data)
            : base(data, TrendType.Growth)
        {
            engine.InstallRequiredPackages("FSA");

            var xvalues = engine.CreateNumericVector(data.X);
            engine.SetSymbol("xvalues", xvalues);

            var yvalues = engine.CreateNumericVector(data.Y);
            engine.SetSymbol("yvalues", yvalues);

            var starts = engine.CreateNumericVector(Parameters);
            engine.SetSymbol("starts", starts);

            fit = engine.Evaluate("nls(yvalues ~ Linf * (1.0 - exp(-K * (xvalues - t0))), start = c('Linf' = starts[1], 'K' = starts[2], 't0' = starts[3]))");
            engine.SetSymbol("fit", fit);
            Parameters.Clear();
            Parameters.AddRange(engine.Evaluate("params = coef(fit)").AsNumeric());

            ResidualStandardError = engine.Evaluate("summary(fit)$sigma").AsNumeric()[0];
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + @" = " + Linf.ToString(format) + " (1 - e^{-" +
                K.ToString(format) + " (" + x + " - " + T0.ToString(format) + @")})";
        }

        public override double Predict(IReadOnlyList<double> p, double t)
        {
            return p[0] * (1.0 - Math.Exp(-p[1] * (t - p[2])));
        }

        public override double PredictInversed(double y)
        {
            return T0 - Math.Log((Linf - y) / Linf) / K;
        }

        public override double[] GetInitials()
        {
            engine.Evaluate("library(FSA)");
            engine.Evaluate("starts = vbStarts(formula = yvalues ~ xvalues)");
            engine.Evaluate("starts[2] = .3");
            return engine.Evaluate("starts").AsNumeric().ToArray();

            ////double[] result = new double[] { 1.0, -1.0, 0.0 };

            ////return result;

            //// lmax is just max of L
            //double lmax = Data.Y.Maximum;

            //// lmax is mean of 5 maximal;
            //if (Data.Count > 15)
            //{
            //    lmax = new Sample(
            //        Data.Y
            //        .OrderByDescending(t => t)
            //        .Take(5)).Mean;
            //}

            //BivariateSample lin = Data.Copy();
            //lin.Y.Transform((v) => { return -Math.Log(1.0 - v / lmax); });
            //Linear linreg = new Linear(lin);

            //if (double.IsNaN(linreg.Intercept)) return new double[] { lmax, .3, 0.0 };
            //else return new double[] { lmax, .3 /*linreg.B*/, -linreg.Intercept / linreg.Slope };
        }
    }
}