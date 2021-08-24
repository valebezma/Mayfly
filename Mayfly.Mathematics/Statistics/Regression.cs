using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Numerics;
using RDotNet;

namespace Mayfly.Mathematics.Statistics
{
    public enum IntervalType
    {
        Prediction = 0,
        Confidence = 1
    }

    public abstract class Regression : IFormattable
    {
        internal string name;
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public BivariateSample data;
        public BivariateSample Data
        {
            get { return data; }
            protected set { data = value; }
        }

        public TrendType type;
        public TrendType Type {
            get { return type; }
            protected set { type = value; }
        }

        //public ResidualsResult fit;
        //public ResidualsResult Fit {
        //    get { return fit; }
        //    protected set { fit = value; }
        //}

        public string Equation
        {
            get
            {
                return GetEquation("y", "x");
            }
        }

        public BivariateSample Outliers;

        internal REngine engine;

        internal SymbolicExpression fit;

        public List<double> Parameters;

        public double RSquared { get; internal set; }

        public string DeterminationStrength
        {
            get
            {
                double r = RSquared;

                if (r < 0.1)
                {
                    return Resources.Chaddock.Strength0;
                }
                else if (r < 0.3)
                {
                    return Resources.Chaddock.Strength1;
                }
                else if (r < 0.5)
                {
                    return Resources.Chaddock.Strength2;
                }
                else if (r < 0.7)
                {
                    return Resources.Chaddock.Strength3;
                }
                else if (r < 0.9)
                {
                    return Resources.Chaddock.Strength4;
                }
                else
                {
                    return Resources.Chaddock.Strength5;
                }
            }
        }



        public Regression()
        {
            type = TrendType.Linear;
            Parameters = new List<double>();
        }

        public Regression(BivariateSample _data) : this()
        {
            data = _data;
            name = data.X.Name;

            REngine.SetEnvironmentVariables();
            engine = REngine.GetInstance();

            var xvalues = engine.CreateNumericVector(data.X);
            engine.SetSymbol("xvalues", xvalues);

            var yvalues = engine.CreateNumericVector(data.Y);
            engine.SetSymbol("yvalues", yvalues);
        }



        public virtual string GetEquation(string y, string x)
        {
            return GetEquation(y, x, "G3");
        }

        public string GetEquation(string format)
        {
            return GetEquation("y", "x", format);
        }

        public abstract string GetEquation(string y, string x, string format);



        public virtual double Predict(double x) 
        { 
            if (fit == null) return double.NaN;

            var xvalues = engine.CreateNumeric(x);
            engine.SetSymbol("new.x", xvalues);

            engine.SetSymbol("fit", fit);
            return engine.Evaluate("predict(fit, data.frame(xvalues = new.x)").AsNumeric()[0];        
        }

        public abstract double PredictInversed(double y);

        //public UncertainValue Estimate(int i)
        //{
        //    if (fit == null)
        //        throw new NullReferenceException("Fit does not exist.");

        //    if (i < 0 || i > fit.Parameters.Count - 1)
        //        throw new ArgumentException("There is no " + i + "th parameter in this model.");

        //    return fit.Parameters[i].Estimate;
        //}

        //public virtual double Parameter(int i)
        //{
        //    return Estimate(i).Value;
        //}

        internal virtual Interval[] getInterval(double[] x, double level, IntervalType type)
        {
            if (fit == null) return null;

            var xvalues = engine.CreateNumericVector(x);
            engine.SetSymbol("axis", xvalues);

            var alpha = engine.CreateNumeric(level);
            engine.SetSymbol("alpha", alpha);

            engine.SetSymbol("fit", fit);
            NumericMatrix predictions = engine.Evaluate(
                "predict(fit, data.frame(xvalues = axis), interval = '" + type.ToString().ToLower() + "', level = alpha)").AsNumericMatrix();
            List<Interval> result = new List<Interval>();
            for (int i = 0; i < predictions.RowCount; i++)
            {
                result.Add(Interval.FromEndpoints(predictions[i, 1], predictions[i, 2]));
            }
            return result.ToArray();
        }

        public Interval[] GetInterval(double[] x, double level, IntervalType type)
        {
            Interval[] result = getInterval(x, level, type);

            if (result != null && type == IntervalType.Prediction)
            {
                Outliers = new BivariateSample();

                for (int i = 0; i < data.Count; i++)
                {
                    double _x = data.X.ElementAt(i);
                    double _y = data.Y.ElementAt(i);

                    for (int j = 0; j < x.Length - 1; j++)
                    {
                        if (Interval.FromEndpoints(x[j], x[j + 1]).OpenContains(_x))
                        {
                            double xgap = x[j + 1] - x[j];
                            double xtrack = _x - x[j];

                            double ygap = result[j + 1].RightEndpoint - result[j].RightEndpoint;
                            double ytrack = ygap * (xtrack / xgap);
                            double upperband = result[j].RightEndpoint + ytrack;

                            ygap = result[j + 1].LeftEndpoint - result[j].LeftEndpoint;
                            ytrack = ygap * (xtrack / xgap);
                            double lowerband = result[j].LeftEndpoint + ytrack;

                            if (!Interval.FromEndpoints(lowerband, upperband).OpenContains(_y))
                            {
                                Outliers.Add(_x, _y);
                            }
                            break;
                        }
                    }

                }
            }

            return result;
        }

        public BivariateSample GetOutliers(double level)
        {
            List<double> x = new List<double>();
            for (double v = Data.X.Minimum; v <= Data.X.Maximum; v += ((Data.X.Maximum - Data.X.Minimum) / 500))
            {
                x.Add(v);
            }
            GetInterval(x.ToArray(), level, IntervalType.Prediction);
            return Outliers;
        }



        public override string ToString()
        {
            return ToString("N3");
        }

        public string ToString(string format)
        {
            return ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }

        public virtual string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0}: {1} (n = {2})", this.Name, this.GetEquation("y", "x", format), data.Count);
        }



        public virtual void GetReport(Report report)
        {
            report.AddEquation(this.GetEquation("G2"));
        }

        public Report GetReport()
        {
            Report report = new Report("Regression results");

            this.GetReport(report);

            return report;
        }



        public static Regression GetRegression(BivariateSample data)
        {
            return GetRegression(data, TrendType.Auto);
        }

        public static Regression GetRegression(BivariateSample data, TrendType type)
        {
            if (data.Count < UserSettings.StrongSampleSize) return null;

            Regression result = new Linear(data);

            try
            {
                switch (type)
                {
                    case TrendType.Auto:
                        TrendType best = TrendType.None;
                        double r2 = 0;

                        foreach (TrendType t in new TrendType[] { TrendType.Linear, 
                            TrendType.Exponential, TrendType.Power, TrendType.Logarithmic,
                            TrendType.Quadratic, TrendType.Cubic, 
                            TrendType.Growth, TrendType.Logistic })
                        {
                            Regression r = GetRegression(data, t);

                            if (r == null) continue;

                            if (r.RSquared > r2)
                            {
                                r2 = r.RSquared;
                                best = t;
                            }
                        }

                        return GetRegression(data, best);

                    case TrendType.Linear:
                        result = new Linear(data);
                        break;

                    case TrendType.Quadratic:
                        result = new Polynom(data, 2);
                        break;

                    case TrendType.Cubic:
                        result = new Polynom(data, 3);
                        break;

                    case TrendType.Power:
                        result = new Power(data);
                        break;

                    case TrendType.Exponential:
                        result = new Exponent(data);
                        break;

                    case TrendType.Logarithmic:
                        result = new Logarithm(data);
                        break;

                    case TrendType.Logistic:
                        result = new Logistic(data);
                        break;

                    case TrendType.Growth:
                        result = new Growth(data);
                        break;
                }

            }
            catch (Exception e)
            {
                Log.Write(e);
                result = null;
            }

            if (result == null)
            {
                Log.Write("Failed to build {0} fit on {1} datapoints.", type, data.Count);
                result = null;
            }

            return result;
        }

        public static Regression GetSummarized(IList<Regression> regressions)
        {
            BivariateSample commonSample = new BivariateSample();
            string commonName = string.Empty;

            foreach (Regression line in regressions)
            {
                commonName += line.Name + " + ";

                for (int i = 0; i < line.Data.Count; i++)
                {
                    commonSample.Add(
                        line.Data.X.ElementAt(i),
                        line.Data.Y.ElementAt(i));
                }
            }

            Regression result = Regression.GetRegression(commonSample, regressions[0].type);
            result.Name = commonName.TrimEnd(" +".ToCharArray());
            return result;
        }
    }

    //public class RegressionPool
    //{
    //    public string Name { get; set; }

    //    public List<Regression> SeparateRegressions { get; private set; }
    //    public Regression TotalRegression { get; private set; }
    //    public TestResult Coincidence { get; internal set; }
    //    //public Mayfly.Mathematics.Charts.Functor Functor { get; internal set; }

    //    internal double pooledSS = 0;
    //    internal double pooledDF = 0;
    //    internal double totalSS = 0;

    //    public RegressionPool(List<Regression> regressions, string name)
    //    {
    //        this.SeparateRegressions = regressions;
    //        this.TotalRegression = Regression.GetSummarized(regressions);
    //        this.TotalRegression.data.X.Name = regressions[0].data.X.Name;
    //        this.TotalRegression.data.Y.Name = regressions[0].data.Y.Name;

    //        this.Name = name;

    //        double k = regressions.Count;

    //        // Pooled

    //        pooledSS = 0;
    //        pooledDF = 0;

    //        foreach (Regression regression in regressions)
    //        {
    //            pooledSS += regression.ResidualSS;
    //            pooledDF += regression.DegreedOfFreedom;
    //        }

    //        // Total

    //        totalSS = TotalRegression.ResidualSS;

    //        // Coincidence

    //        double f = ((totalSS - pooledSS) / (2 * (k - 1))) / ((pooledSS) / (pooledDF));
    //        Coincidence = new TestResult(f, new FisherDistribution(2 * (k - 1), pooledDF));
    //    }

    //    public RegressionPool(List<Regression> regressions)
    //        : this(regressions, string.Format("{0} from {1} dependence coincidence test",
    //            regressions[0].data.Y.Name, regressions[0].data.X.Name))
    //    { }

    //    public string GetShortDescription(string format)
    //    {
    //        double p = Coincidence.Probability;

    //        string pp = p.ToString(format);
    //        if (pp == (0).ToString(format)) pp = "p < " + pp.Substring(0, pp.Length - 1) + "1";
    //        else pp = "p = " + pp;


    //        return string.Format("F = {0:" + format + "}, {1}", Coincidence.Statistic, pp);
    //    }

    //    public string GetShortDescription()
    //    {
    //        return GetShortDescription("N3");// string.Format("N = {0}, R² = {1:N3}, p = {2:N3}", data.Count, Determination, fit.GoodnessOfFit.RightProbability);
    //    }
    //}

    //public class LinearPool : RegressionPool
    //{
    //    public TestResult SlopeEquality { get; private set; }

    //    public TestResult ElevationEquality { get; private set; }

    //    internal double commonDF = 0;
    //    internal double commonSS = 0;

    //    public LinearPool(List<Regression> lines)
    //        : base(lines)
    //    {
    //        double k = lines.Count();

    //        // Common

    //        double commonXX = 0;
    //        double commonXY = 0;
    //        double commonYY = 0;

    //        double commonDF = 0;

    //        foreach (Linear line in lines)
    //        {
    //            for (int i = 0; i < line.Data.Count; i++)
    //            {
    //                double y = line.Data.Y.ElementAt(i);
    //                double x = line.Data.X.ElementAt(i);

    //                commonXY += x * y;
    //            }

    //            commonXX += line.Data.X.Variance * line.Data.X.Count; //.SumOfSquareDeviations;
    //            commonYY += line.Data.Y.Variance * line.Data.Y.Count; //SumOfSquareDeviations;

    //            double n = (double)line.Data.Count;
    //            double xSum = line.Data.X.Sum();
    //            double ySum = line.Data.Y.Sum();
    //            commonXY -= (xSum * ySum / n);

    //            commonDF += line.Data.Count;
    //        }

    //        double commonSS = commonYY - (commonXY * commonXY) / commonXX;
    //        commonDF -= k - 1;

    //        double f = ((commonSS - pooledSS) / (k - 1)) / (pooledSS / pooledDF);
    //        this.SlopeEquality = new TestResult(f, new FisherDistribution(k - 1, pooledDF));

    //        f = ((totalSS - commonSS) / (k - 1)) / (commonSS / commonDF);
    //        this.ElevationEquality = new TestResult(f, new FisherDistribution(k - 1, commonDF));
    //    }
    //}
}
