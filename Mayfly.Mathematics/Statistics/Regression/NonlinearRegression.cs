using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Mayfly.Extensions;
using RDotNet;

namespace Mayfly.Mathematics.Statistics
{
    public abstract class NonlinearRegression : Regression
    {
        NonlinearRegressionResult NonlinearFit { get { return (NonlinearRegressionResult)fit; } }

        public List<double> Parameters;

        public NonlinearRegression(BivariateSample data)
            : base(data)
        {
            Parameters = new List<double>();
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
            return NonlinearFit.Predict(x);
        }

        public override double Parameter(int i)
        {
            return Parameters[i];
        }
    }

    public class Logistic : NonlinearRegression
    {
        public double L { get { return Parameter(0); } }

        public double K { get { return Parameter(1); } }

        public double X0 { get { return Parameter(2); } }


        public Logistic(BivariateSample data)
            : base(data, TrendType.Logistic)
        { }



        public override string GetEquation(string y, string x, string format)
        {
            return y + string.Format("{0} = \\frac{1:" + format + "}{ 1 + e^{-{2:" + format + "} (x - {3:" + format + "})}}", y, L, K, X0);
        }

        public override double Predict(IReadOnlyList<double> p, double x)
        {
            return p[0] / (1.0 + Math.Exp(-p[1] * (x - p[2])));
        }

        //public override double Predict(double x)
        //{
        //    return Predict(new double[] { L, K, X0 }, x);
        //}

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
        public double Linf { get { return Parameter(0); } }

        public double K { get { return Parameter(1); } }

        public double T0 { get { return Parameter(2); } }


        public Growth(BivariateSample data)
            : base(data/*.GetAveragedByX(1)*/, TrendType.Growth)
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();

            var age = engine.CreateNumericVector(data.X);
            engine.SetSymbol("age", age);

            var len = engine.CreateNumericVector(data.Y);
            engine.SetSymbol("len", len);

            //var starts = engine.CreateNumericVector(GetInitials());
            //engine.SetSymbol("starts", starts);

            engine.Evaluate("library(FSA)");
            engine.Evaluate("starts = vbStarts(formula = len ~ age)");
            engine.Evaluate("starts[2] = .3");

            //engine.Evaluate("fit = nls(len ~ Linf * (1.0 - exp(-K * (age - t0))), start = c('Linf' = starts[1], 'K' = starts[2], 't0' = starts[3]))");
            engine.Evaluate("fit = nls(len ~ Linf * (1.0 - exp(-K * (age - t0))), start = starts)");
            NumericVector _params = engine.Evaluate("params = coef(fit)").AsNumeric();

            Parameters.Clear();
            foreach (double param in _params) { Parameters.Add(param); }
        }



        public override string GetEquation(string y, string x, string format)
        {
            return string.Format("{0} = {1:" + format + "} \\times (1 - e^{-{2:" + format + "} \\times ({3} - {4:" + format + "})})", y, Linf, K, x, T0);
        }

        public override double Predict(IReadOnlyList<double> p, double t)
        {
            return p[0] * (1.0 - Math.Exp(-p[1] * (t - p[2])));
        }

        public override double Predict(double t)
        {
            return Predict(Parameters, t);
        }

        public override double PredictInversed(double y)
        {
            if (fit == null) return double.NaN;
            return T0 - Math.Log((Linf - y) / Linf) / -K;
            //return Math.Log((Linf - y) / Linf) / K;
        }

        public override double[] GetInitials()
        {
            //double[] result = new double[] { 1.0, -1.0, 0.0 };

            //return result;

            // lmax is just max of L
            double lmax = Data.Y.Maximum;

            // lmax is mean of 5 maximal;
            if (Data.Count > 15)
            {
                lmax = new Sample(
                    Data.Y
                    .OrderByDescending(t => t)
                    .Take(5)).Mean;
            }

            BivariateSample lin = Data.Copy();
            lin.Y.Transform((v) => { return -Math.Log(1.0 - v / lmax); });
            Linear linreg = new Linear(lin);

            if (double.IsNaN(linreg.A)) return new double[] { lmax, .3, 0.0 };
            else return new double[] { lmax, .3 /*linreg.B*/, -linreg.A / linreg.B };
        }
    }
}