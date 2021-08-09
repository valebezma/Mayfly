using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Mayfly.Extensions;
//using Meta.Numerics;
//using RDotNet;
//using System.Collections;

namespace Mayfly.Mathematics.Statistics
{
    public abstract class NonlinearRegression : Regression
    {
        NonlinearRegressionResult NonlinearFit { get { return (NonlinearRegressionResult)fit; } }

        public NonlinearRegression(BivariateSample data)
            : base(data)
        { }

        public NonlinearRegression(BivariateSample data, TrendType _type, Func<IReadOnlyList<double>, double, double> f)
            : this(data)
        {
            type = _type;
            fit = data.NonlinearRegression(f, this.GetInitials());
        }

        public abstract double[] GetInitials();

        public override double Predict(double x)
        {
            return NonlinearFit.Predict(x);
        }
    }

    public class Logistic : NonlinearRegression
    {
        public double L { get { return Parameter(0); } }

        public double K { get { return Parameter(1); } }

        public double X0 { get { return Parameter(2); } }


        public Logistic(BivariateSample data)
            : base(data, TrendType.Logistic, (p, x) => { return p[0] / (1.0 + Math.Exp(-p[1] * (x - p[2]))); })
        { }



        public override string GetEquation(string y, string x, string format)
        {
            return y + @" = \frac{" + L.ToString(format) + "}{ 1 + e^{-" +
                K.ToString(format) + " (x - " + X0.ToString(format) + ")}}";
        }

        public override double Predict(double x)
        {
            return L / (1.0 + Math.Exp(-K * (x - X0)));
        }

        public override double PredictInversed(double y)
        {
            return X0 + Math.Log(L / y - 1) / K;
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
            : base(data, TrendType.Growth, (p, t) => { return p[0] * (1.0 - Math.Exp(-p[1] * (t - p[2]))); })
            //: base(data, TrendType.Growth, (p, t) => { return p[0] * (1.0 - Math.Exp(-p[1] * t)); })
        { }



        public override string GetEquation(string y, string x, string format)
        {
            return y + @" = " + Linf.ToString(format) + " (1 - e^{-" +
                K.ToString(format) + " (" + x + " - " + T0.ToString(format) + @")})";
            //K.ToString(format) + " " + x + "})";
        }

        public override double Predict(double t)
        {
            return Linf * (1 - Math.Exp(K * (t - T0)));
            //return Linf * (1 - Math.Exp(K * t));
        }

        public override double PredictInversed(double y)
        {
            if (fit == null) return double.NaN;
            return T0 - Math.Log((Linf - y) / Linf) / K;
            //return Math.Log((Linf - y) / Linf) / K;
        }

        public override double[] GetInitials()
        {
            return new double[] { 1.0, -1.0, 0.0 };

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

            //if (double.IsNaN(linreg.A)) return new double[] { lmax, 1.0, 0.0 };
            //else return new double[] { lmax, linreg.B, -linreg.A / linreg.B };

            ////return new double[] { lmax, linreg.B };

            ////if (double.IsNaN(linreg.B)) return new double[] { lmax, 1.0 };
            ////else return new double[] { lmax, linreg.B };
        }
    }
}