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
        public GeneralLinearRegression(BivariateSample data, string formula)
            : base(data)
        {
            fit = engine.Evaluate("lm(formula = " + formula + ")");
            engine.SetSymbol("fit", fit);
            Parameters.Clear();
            Parameters.AddRange(engine.Evaluate("params = coef(fit)").AsNumeric());

            RSquared = engine.Evaluate("summary(fit)$r.squared").AsNumeric()[0];
        }

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
        public double Intercept { get { return Parameters[0]; } }

        public double Slope { get { return Parameters[1]; } }

        public Linear(BivariateSample data)
            : base(data, "yvalues ~ xvalues")
        {
            Type = TrendType.Linear;

            //fit = engine.Evaluate("lm(formula = yvalues ~ xvalues)");
            //engine.SetSymbol("fit", fit);
            //Parameters.Clear();
            //Parameters.AddRange(engine.Evaluate("params = coef(fit)").AsNumeric());

            //RSquared = engine.Evaluate("summary(fit)$r.squared").AsNumeric()[0];
        }

        public override double Predict(double x)
        {
            return Intercept + Slope * x;
        }

        public override double PredictInversed(double y)
        {
            return (y - Intercept) / Slope;
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + " = " + Intercept.ToString(format) + (Slope > 0 ? "+" : "-") +
                Math.Abs(Slope).ToString(format) + " " + x;
        }

        //public void AddSummary(Report report)
        //{
        //    //Report.Table table1 = new Report.Table("Parameters");
        //    //table1.AddHeader("Parameter", "Value", "Standard Error", "p");

        //    //table1.StartRow();
        //    //table1.AddCellValue("a");
        //    //table1.AddCellRight(Intercept.Value, "G3");
        //    //table1.AddCellRight(Intercept.Uncertainty, "G3");
        //    //table1.AddCellRight(double.NaN, "G3");
        //    //table1.EndRow();

        //    //table1.StartRow();
        //    //table1.AddCellValue("b");
        //    //table1.AddCellRight(LinearFit.Parameters[1].Estimate.Value, "G3");
        //    //table1.AddCellRight(LinearFit.Parameters[1].Estimate.Uncertainty, "G3");
        //    //table1.AddCellRight(LinearFit.Anova.Factor.Result.Probability, "G3");
        //    //table1.EndRow();

        //    //report.AddTable(table1);

        //    //Report.Table table2 = new Report.Table("Summary");
        //    //table2.StartRow();
        //    //table2.AddCellPrompt("Data points number (<i>n</i>)", this.Data.X.Count);
        //    //table2.EndRow();
        //    //table2.StartRow();
        //    //table2.AddCellPrompt("Regresson standard error", double.NaN);
        //    //table2.EndRow();
        //    //table2.StartRow();
        //    //table2.AddCellPrompt("Determination (<i>r</i>²)", string.Format("{0:N3} ({1})", LinearFit.RSquared, this.DeterminationStrength));
        //    //table2.EndRow();
        //    //report.AddTable(table2);
        //}

        //public override void GetReport(Report report)
        //{
        //    report.AddParagraph("Basic equation is:");
        //    report.AddEquation(@"y = a + b \times x");

        //    AddSummary(report);

        //    report.AddParagraph("So, equation is:");
        //    report.AddEquation(this.GetEquation("G3"));
        //}
    }

    public class Polynom : GeneralLinearRegression
    {
        public int Degree;

        public Polynom(BivariateSample data, int degree)
            : base(data, GetFormula(degree))
        {
            Degree = degree;
            Type = degree == 2 ? TrendType.Quadratic : TrendType.Cubic;
        }

        public static string GetFormula(int degree)
        {
            if (degree < 2)
                throw new ArgumentException("Polynom should be at lesat in second degree");

            string result = "yvalues ~ xvalues";

            for (int i = 2; i <= degree; i++)
            {
                result += " + I(xvalues ^ " + i + ")";
            }

            return result;
        }

        public override string GetEquation(string y, string x, string format)
        {
            string result = y + " = ";

            result += Parameters[0].ToString(format);

            for (int i = 1; i < Parameters.Count; i++)
            {
                double b = Parameters[i];

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
            double y = Parameters[0];

            for (int i = 1; i < Parameters.Count; i++)
            {
                y += Parameters[i] * Math.Pow(x, i);
            }

            return y;
        }

        public override double PredictInversed(double y)
        {
            return double.NaN;
        }

        //public void AddSummary(Report report)
        //{
        //    Report.Table table1 = new Report.Table("Parameters");
        //    table1.AddHeader("Parameter", "Value", "Standard Error", "p");

        //    table1.StartRow();
        //    table1.AddCell(PolynomialFit.Parameters[0].Name);
        //    table1.AddCell(PolynomialFit.Intercept.Value);
        //    table1.AddCell(PolynomialFit.Intercept.Uncertainty);
        //    table1.AddCell(double.NaN);
        //    table1.EndRow();

        //    for (int i = 1; i < Fit.Parameters.Count; i++)
        //    {
        //        table1.StartRow();
        //        table1.AddCell(PolynomialFit.Parameters[i].Name);
        //        table1.AddCell(PolynomialFit.Parameters[i].Estimate.Value);
        //        table1.AddCell(PolynomialFit.Parameters[i].Estimate.Uncertainty);
        //        table1.AddCell(double.NaN);
        //        table1.EndRow();
        //    }

        //    report.AddTable(table1);

        //    Report.Table table2 = new Report.Table("Summary");
        //    table2.StartRow();
        //    table2.AddCellPrompt("Data points number (<i>n</i>)", this.Data.X.Count);
        //    table2.EndRow();
        //    table2.StartRow();
        //    table2.AddCellPrompt("Regresson standard error", double.NaN);
        //    table2.EndRow();
        //    table2.StartRow();
        //    table2.AddCellPrompt("Determination (<i>r</i>²)", string.Format("{0:N3} ({1})", PolynomialFit.RSquared, this.DeterminationStrength));
        //    table2.EndRow();
        //    report.AddTable(table2);
        //}

        //public override void GetReport(Report report)
        //{
        //    report.AddParagraph("Basic equation is:");
        //    report.AddEquation(this.GetEquation("y", "x", "G2"));

        //    AddSummary(report);

        //    report.AddParagraph("So, equation is:");
        //    report.AddEquation(this.GetEquation("G2"));
        //}
    }

    public abstract class TransformedLinearRegression : GeneralLinearRegression
    {
        public virtual double Intercept { get { return LinearizedModel.Intercept; } }

        public double Slope { get { return LinearizedModel.Slope; } }

        public Linear LinearizedModel;

        public TransformedLinearRegression(BivariateSample data)
            : base(data, "yvalues ~ xvalues")
        {
            BivariateSample logData = data.Copy();
            logData.X.Transform(TransformX);
            logData.Y.Transform(TransformY);

            LinearizedModel = new Linear(logData);
            Parameters.Clear();
            Parameters.Add(Intercept);
            Parameters.Add(Slope);

            RSquared = LinearizedModel.RSquared;
        }

        public virtual double TransformX(double x)
        {
            return x;
        }

        public virtual double BackTransformX(double x)
        {
            return x;
        }

        public virtual double TransformY(double y)
        {
            return y;
        }

        public virtual double BackTransformY(double y)
        {
            return y;
        }

        public override double Predict(double x)
        {
            return BackTransformY(LinearizedModel.Predict(TransformX(x)));
        }

        public override double PredictInversed(double y)
        {
            return BackTransformX(LinearizedModel.PredictInversed(TransformY(y)));
        }

        public Interval[] BackTransform(Interval[] linPrediction)
        {
            List<Interval> result = new List<Interval>();
            foreach (Interval c in linPrediction)
            {
                result.Add(Interval.FromEndpoints(BackTransformY(c.LeftEndpoint), BackTransformY(c.RightEndpoint)));
            }
            return result.ToArray();
        }

        internal override Interval[] getInterval(double[] x, double level, IntervalType type)
        {
            Sample s = new Sample(x);
            s.Transform(TransformX);
            Interval[] result = BackTransform(LinearizedModel.getInterval(s.ToArray(), level, type));
            return result;
        }

        //public override void GetReport(Report report)
        //{
        //    report.UseEquationNumeration = true;
        //    report.AddParagraph("Basic equation is:");
        //    report.AddEquation(this.GetEquation("y", "x", "G2"));

        //    report.AddParagraph("Solution for equation {0} is:", report.NextEquationNumber - 1);
        //    report.AddEquation(LinearizedModel.GetEquation("y", "log(x)", "G2"));

        //    LinearizedModel.AddSummary(report);

        //    report.AddParagraph("So, equation {0} is:", report.NextEquationNumber - 2);
        //    report.AddEquation(this.GetEquation("G2"));
        //}
    }

    public class Logarithm : TransformedLinearRegression
    {
        public Logarithm(BivariateSample data)
            : base(data)
        {
            type = TrendType.Logarithmic;
        }

        public override double TransformX(double x)
        {
            return Math.Log10(x);
        }

        public override double BackTransformX(double x)
        {
            return Math.Pow(10, x);
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + " = " + Intercept.ToString(format) +
                (Slope > 0 ? "+" : "-") + Math.Abs(Slope).ToString(format) +
                @" \times \log{" + x + "}";
        }
    }

    public class Exponent : TransformedLinearRegression
    {
        public Exponent(BivariateSample data)
            : base(data)
        {
            type = TrendType.Exponential;
        }

        public override double TransformY(double y)
        {
            return Math.Log(y);
        }

        public override double BackTransformY(double y)
        {
            return Math.Pow(Math.E, y);
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + " = " + Intercept.ToString(format) + @" \times {e}^{" + Slope.ToString(format) + @" " + x + "}";
        }
    }

    public class Power : TransformedLinearRegression
    {
        public override double Intercept { get { return BackTransformX(LinearizedModel.Intercept); } }

        public Power(BivariateSample data)
            : base(data)
        {
            BivariateSample logData = data.Copy();
            logData.X.Transform(TransformX);
            logData.Y.Transform(TransformX);

            LinearizedModel = new Linear(logData);
            //fit = LinearizedModel.Fit;
            type = TrendType.Power;
        }

        public override double TransformX(double x)
        {
            return Math.Log10(x);
        }

        public override double BackTransformX(double x)
        {
            return Math.Pow(10, x);
        }

        public override double TransformY(double x)
        {
            return Math.Log10(x);
        }

        public override double BackTransformY(double x)
        {
            return Math.Pow(10, x);
        }

        public override string GetEquation(string y, string x, string format)
        {
            return y + " = {" + Intercept.ToString(format) + @"} \times {" + x + "^{" + Slope.ToString(format) + "}}";
        }
    }
}