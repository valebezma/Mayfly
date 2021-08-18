using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Numerics;
using RDotNet;

namespace Mayfly.Mathematics.Statistics
{
    public abstract class GeneralLinearRegression : Regression
    {
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

        public GeneralLinearRegression(BivariateSample data)
            : base(data)
        { }

        public override string ToString(string format, IFormatProvider provider)
        {
            //double p = GlmFit.Anova.Result.Probability;
            //string pp = p.ToString(format);

            //if (pp == (0).ToString(format)) pp = "p <" + pp.Substring(0, pp.Length - 1) + "1";
            //else pp = "p = " + pp;

            return string.Format("{0}: {1} (n = {2}, r² = {3:N3})",
                this.Name, this.GetEquation("y", "x", format), data.Count, RSquared);
        }

        public override void GetReport(Report report)
        {
            report.AddEquation(this.GetEquation("N4"));


            //Report.Table table1 = new Report.Table("Summary");
            //table1.StartRow();
            //table1.AddCellPrompt("Regression type", comboBoxType.Text);
            //table1.EndRow();
            //table1.StartRow();
            //table1.AddCellPromptEquation("Equation", Regression.Equation);
            //table1.EndRow();
            //report.AddTable(table1);

            //Report.Table table1 = new Report.Table("Regression values");
            //table1.StartRow();
            //table1.AddCellPrompt(labelCount.Text,  textBoxCount.Text);
            //table1.EndRow();
            //table1.StartRow();
            //table1.AddCellPrompt(labelDC.Text, textBoxDC.Text);
            //table1.AddCellPrompt(labelP.Text, textBoxP.Text);
            //table1.EndRow();
            //report.AddTable(table1);
        }
    }

    public class Linear : GeneralLinearRegression
    {
        public double Intercept { get; private set; }

        public double Slope { get; private set; }

        REngine engine;

        public Linear(BivariateSample data)
            : base(data)
        {
            this.Type = TrendType.Linear;
            fit = data.LinearRegression();

            REngine.SetEnvironmentVariables();
            engine = REngine.GetInstance();

            var xvalues = engine.CreateNumericVector(data.X);
            engine.SetSymbol("xvalues", xvalues);

            var yvalues = engine.CreateNumericVector(data.Y);
            engine.SetSymbol("yvalues", yvalues);

            engine.Evaluate("fit = lm(formula = yvalues ~ xvalues)");
            NumericVector _params = engine.Evaluate("params = coef(fit)").AsNumeric();
            Intercept = _params[0];
            Slope = _params[1];

            RSquared = engine.Evaluate("summary(fit)$r.squared").AsNumeric()[0];
        }



        public override double Predict(double x)
        {
            return Intercept + Slope * x;
        }

        public override double PredictInversed(double y)
        {
            return (y - Intercept) / Slope;
        }

        internal override Interval[] GetPredictionInterval(double[] x, double level)
        {
            var xvalues = engine.CreateNumericVector(x);
            engine.SetSymbol("_xvalues", xvalues);

            var alpha = engine.CreateNumeric(level);
            engine.SetSymbol("alpha", alpha);

            NumericMatrix predictions = engine.Evaluate(
                "predict(fit, data.frame(Length.mm = xs), interval = 'prediction', level = alpha)").AsNumericMatrix();
            List<Interval> result = new List<Interval>();
            for (int i = 0; i < predictions.RowCount; i++)
            {
                result.Add(Interval.FromEndpoints(predictions[i, 1], predictions[i, 2]));
            }            
            return result.ToArray();
        }

        internal override Interval[] GetConfidenceInterval(double[] x, double level)
        {
            var xvalues = engine.CreateNumericVector(x);
            engine.SetSymbol("_xvalues", xvalues);

            var alpha = engine.CreateNumeric(level);
            engine.SetSymbol("alpha", alpha);

            NumericMatrix predictions = engine.Evaluate(
                "predict(fit, data.frame(Length.mm = xs), interval = 'confidence', level = alpha)").AsNumericMatrix();
            List<Interval> result = new List<Interval>();
            for (int i = 0; i < predictions.RowCount; i++)
            {
                result.Add(Interval.FromEndpoints(predictions[i, 1], predictions[i, 2]));
            }
            return result.ToArray();
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + " = " + Intercept.ToString(format) + (Slope > 0 ? "+" : "-") +
                Math.Abs(Slope).ToString(format) + " " + x;
        }

        public void AddSummary(Report report)
        {
            //Report.Table table1 = new Report.Table("Parameters");
            //table1.AddHeader("Parameter", "Value", "Standard Error", "p");

            //table1.StartRow();
            //table1.AddCellValue("a");
            //table1.AddCellRight(Intercept.Value, "G3");
            //table1.AddCellRight(Intercept.Uncertainty, "G3");
            //table1.AddCellRight(double.NaN, "G3");
            //table1.EndRow();

            //table1.StartRow();
            //table1.AddCellValue("b");
            //table1.AddCellRight(LinearFit.Parameters[1].Estimate.Value, "G3");
            //table1.AddCellRight(LinearFit.Parameters[1].Estimate.Uncertainty, "G3");
            //table1.AddCellRight(LinearFit.Anova.Factor.Result.Probability, "G3");
            //table1.EndRow();

            //report.AddTable(table1);

            //Report.Table table2 = new Report.Table("Summary");
            //table2.StartRow();
            //table2.AddCellPrompt("Data points number (<i>n</i>)", this.Data.X.Count);
            //table2.EndRow();
            //table2.StartRow();
            //table2.AddCellPrompt("Regresson standard error", double.NaN);
            //table2.EndRow();
            //table2.StartRow();
            //table2.AddCellPrompt("Determination (<i>r</i>²)", string.Format("{0:N3} ({1})", LinearFit.RSquared, this.DeterminationStrength));
            //table2.EndRow();
            //report.AddTable(table2);
        }

        public override void GetReport(Report report)
        {
            report.AddParagraph("Basic equation is:");
            report.AddEquation(@"y = a + b \times x");

            AddSummary(report);

            report.AddParagraph("So, equation is:");
            report.AddEquation(this.GetEquation("G3"));
        }
    }

    public class Logarithm : GeneralLinearRegression
    {
        public Linear LogModel;

        public double A { get { return LogModel.Intercept; } }

        public double B { get { return LogModel.Slope; } }



        public Logarithm(BivariateSample data)
            : base(data)
        {
            BivariateSample logData = data.Copy();
            logData.X.Transform((v) => { return Math.Log10(v); });

            LogModel = new Linear(logData);
            fit = LogModel.Fit;
            type = TrendType.Logarithmic;
        }



        public override string GetEquation(string y, string x, string format)
        {
            return y + " = " + A.ToString(format) +
                (B > 0 ? "+" : "-") + Math.Abs(B).ToString(format) +
                @" \times \log{" + x + "}";
        }

        public override double Predict(double x)
        {
            return LogModel.Predict(Math.Log10(x));
        }

        public override double PredictInversed(double y)
        {
            return Math.Pow(
                LogModel.PredictInversed(y),
                10);
        }

        internal override Interval[] GetPredictionInterval(double[] x, double level)
        {
            return LogModel.GetPredictionInterval(Math.Log10(x), level);
        }

        internal override Interval[] GetConfidenceInterval(double[] x, double level)
        {
            return LogModel.GetPredictionInterval(Math.Log10(x), level);
        }

        public override void GetReport(Report report)
        {
            report.UseEquationNumeration = true;
            report.AddParagraph("Basic equation is:");
            report.AddEquation(this.GetEquation("y", "x", "G2"));

            report.AddParagraph("Solution for equation {0} is:", report.NextEquationNumber - 1);
            report.AddEquation(LogModel.GetEquation("y", "log(x)", "G2"));

            LogModel.AddSummary(report);

            report.AddParagraph("So, equation {0} is:", report.NextEquationNumber - 2);
            report.AddEquation(this.GetEquation("G2"));
        }
    }

    public class Exponent : GeneralLinearRegression
    {
        public Linear LogModel;

        public double A { get { return LogModel.Intercept; } }

        public double B { get { return LogModel.Slope; } }



        public Exponent(BivariateSample data)
            : base(data)
        {
            BivariateSample logData = data.Copy();
            logData.Y.Transform((v) => { return Math.Log(v); });

            LogModel = new Linear(logData);
            fit = LogModel.Fit;
            type = TrendType.Exponential;
        }



        public override string GetEquation(string y, string x, string format)
        {
            return y + " = " + A.ToString(format) + @" \times {e}^{" + B.ToString(format) + @" " + x + "}";
        }

        public override double Predict(double x)
        {
            return Math.Pow(Math.E, LogModel.Predict(x));
        }

        public override double PredictInversed(double y)
        {
            return LogModel.PredictInversed(Math.Log(y));
        }

        internal override Interval[] GetPredictionInterval(double[] x, double level)
        {
            Interval[] linPrediction = LogModel.GetPredictionInterval(x, level);

            return Interval.FromEndpoints(
                Math.Pow(Math.E, linPrediction.LeftEndpoint),
                Math.Pow(Math.E, linPrediction.RightEndpoint)
                );
        }

        internal override Interval[] GetConfidenceInterval(double[] x, double level)
        {
            Interval[] linPrediction = LogModel.GetConfidenceInterval(x, level);

            return Interval.FromEndpoints(
                Math.Pow(Math.E, linPrediction.LeftEndpoint),
                Math.Pow(Math.E, linPrediction.RightEndpoint)
                );
        }

        public override void GetReport(Report report)
        {
            report.UseEquationNumeration = true;
            report.AddParagraph("Basic equation is:");
            report.AddEquation(this.GetEquation("y", "x", "G2"));

            report.AddParagraph("Solution for equation {0} is:", report.NextEquationNumber - 1);
            report.AddEquation(LogModel.GetEquation("log(y)", "x", "G2"));

            LogModel.AddSummary(report);

            report.AddParagraph("So, equation {0} is:", report.NextEquationNumber - 2);
            report.AddEquation(this.GetEquation("G2"));
        }
    }

    public class Power : GeneralLinearRegression
    {
        public Linear LogModel;

        public double A { get { return Math.Pow(10.0, LogModel.Intercept); } }

        public double B { get { return LogModel.Slope; } }



        public Power(BivariateSample data)
            : base(data)
        {
            BivariateSample logData = data.Copy();
            logData.X.Transform((v) => { return Math.Log10(v); });
            logData.Y.Transform((v) => { return Math.Log10(v); });

            LogModel = new Linear(logData);
            fit = LogModel.Fit;
            type = TrendType.Power;
        }



        public override string GetEquation(string y, string x, string format)
        {
            return y + " = {" + A.ToString(format) + @"} \times {" + x + "^{" + B.ToString(format) + "}}";
        }

        public override double Predict(double x)
        {
            double logx = Math.Log10(x);
            double logy = LogModel.Predict(logx);
            return Math.Pow(10, logy);
        }

        public override double PredictInversed(double y)
        {
            return Math.Pow(
                LogModel.PredictInversed(Math.Log10(y)),
                10);
        }

        internal override Interval[] GetPredictionInterval(double[] x, double level)
        {
            Interval[] linPrediction = LogModel.GetPredictionInterval(Math.Log10(x), level);
            return Interval.FromEndpoints(
                Math.Pow(10, linPrediction.LeftEndpoint),
                Math.Pow(10, linPrediction.RightEndpoint)
                );
        }

        internal override Interval[] GetConfidenceInterval(double[] x, double level)
        {
            Interval[] linPrediction = LogModel.GetConfidenceInterval(Math.Log10(x), level);
            return Interval.FromEndpoints(
                Math.Pow(10, linPrediction.LeftEndpoint),
                Math.Pow(10, linPrediction.RightEndpoint)
                );
        }

        public override void GetReport(Report report)
        {
            report.UseEquationNumeration = true;
            report.AddParagraph("Basic equation is:");
            report.AddEquation(@"y = a \times x^{b}");

            report.AddParagraph("Solution for equation {0} is linear regression of:", report.NextEquationNumber - 1);
            report.AddEquation(@"log(y) = log(a) + b \times log(x)");

            LogModel.AddSummary(report);

            report.AddParagraph("So, equation {0} is:", report.NextEquationNumber - 2);
            report.AddEquation(this.GetEquation("G3"));
        }
    }

    public class Polynom : GeneralLinearRegression
    {
        public PolynomialRegressionResult PolynomialFit { get { return (PolynomialRegressionResult)fit; } }

        public Polynom(BivariateSample data, int degree)
            : base(data)
        {
            this.Type = degree == 2 ? TrendType.Quadratic : TrendType.Cubic;
            this.Fit = data.PolynomialRegression(degree);
        }


        public override string GetEquation(string y, string x, string format)
        {
            string result = y + " = ";

            result += Parameter(0).ToString(format);

            for (int i = 1; i < Fit.Parameters.Count; i++)
            {
                double b = Parameter(i);

                result += (b > 0 && i > 0) ? "+" : "-";
                result += Math.Abs(b).ToString(format);

                switch (i)
                {
                    case 1:
                        result += x;
                        break;
                    default:
                        result += x + "^{" + i.ToString() + "}";
                        break;
                }
            }

            return result;
        }

        public override double Predict(double x)
        {
            double y = 0.0;

            for (int i = 1; i < Fit.Parameters.Count; i++)
            {
                y += Parameter(i) * Math.Pow(x, i);
            }

            return y;
        }

        public override double PredictInversed(double y)
        {
            return double.NaN;
        }

        internal override Interval[] GetConfidenceInterval(double[] x, double level)
        {
            throw new NotImplementedException();
        }

        internal override Interval[] GetPredictionInterval(double[] x, double level)
        {
            throw new NotImplementedException();
        }

        public void AddSummary(Report report)
        {
            Report.Table table1 = new Report.Table("Parameters");
            table1.AddHeader("Parameter", "Value", "Standard Error", "p");

            table1.StartRow();
            table1.AddCell(PolynomialFit.Parameters[0].Name);
            table1.AddCell(PolynomialFit.Intercept.Value);
            table1.AddCell(PolynomialFit.Intercept.Uncertainty);
            table1.AddCell(double.NaN);
            table1.EndRow();

            for (int i = 1; i < Fit.Parameters.Count; i++)
            {
                table1.StartRow();
                table1.AddCell(PolynomialFit.Parameters[i].Name);
                table1.AddCell(PolynomialFit.Parameters[i].Estimate.Value);
                table1.AddCell(PolynomialFit.Parameters[i].Estimate.Uncertainty);
                table1.AddCell(double.NaN);
                table1.EndRow();
            }

            report.AddTable(table1);

            Report.Table table2 = new Report.Table("Summary");
            table2.StartRow();
            table2.AddCellPrompt("Data points number (<i>n</i>)", this.Data.X.Count);
            table2.EndRow();
            table2.StartRow();
            table2.AddCellPrompt("Regresson standard error", double.NaN);
            table2.EndRow();
            table2.StartRow();
            table2.AddCellPrompt("Determination (<i>r</i>²)", string.Format("{0:N3} ({1})", PolynomialFit.RSquared, this.DeterminationStrength));
            table2.EndRow();
            report.AddTable(table2);
        }

        public override void GetReport(Report report)
        {
            report.AddParagraph("Basic equation is:");
            report.AddEquation(this.GetEquation("y", "x", "G2"));

            AddSummary(report);

            report.AddParagraph("So, equation is:");
            report.AddEquation(this.GetEquation("G2"));
        }
    }
}