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
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    //public class GillnetCatchesComposition : LengthComposition
    //{
    //    public double Mesh { get; set; }



    //    public GillnetCatchesComposition(double m, LengthComposition c)
    //        : base(c.Name, c.Minimum, c.Maximum, c.Interval)
    //    {
    //        this.Mesh = m;
    //        this.AddRange(c);
    //    }
    //}

    public class GillnetPairSelectivityModel
    {
        public LengthComposition ca;
        public LengthComposition cb;
        double ma;
        double mb;

        public double StandardDeviation { get; private set; }
        public double SelectionFactor { get; private set; }

        public LengthComposition PopulationEstimateA { get; private set; }
        public LengthComposition PopulationEstimateB { get; private set; }

        internal LinearRegressionResult lin;

        public ContinuousDistribution dista;

        public ContinuousDistribution distb;

        public GillnetPairSelectivityModel(
            LengthComposition _ca, double _ma,
            LengthComposition _cb, double _mb)
        {
            ma = _ma;
            mb = _mb;
            ca = _ca;
            cb = _cb;

            if (ma >= mb)
                throw new ArgumentException("Mesh a must be less than mesh b");

            if (ca.Interval != cb.Interval)
                throw new ArgumentException("Compositions must have same interval");

            BivariateSample l = new BivariateSample();
            double s = double.MinValue;

            for (int i = 0; i < ca.Count; i++)
            {
                if (ca[i].Quantity == 0 || cb[i].Quantity == 0) continue;
                double ln = Math.Log(cb[i].Abundance / ca[i].Abundance);
                if (ln > s)
                {
                    l.Add(ca[i].Size.Midpoint, ln);
                    s = ln;
                }
            }

            //new Mayfly.Mathematics.Charts.Scatterplot(l, "www").ShowOnChart(true);

            lin = l.LinearRegression();
            double a = lin.Intercept.Value;
            double b = lin.Slope.Value;

            double sf = (-2 * a) / (b * (ma + mb));
            double sd = Math.Sqrt(sf * ((mb - ma) / b));

            this.SelectionFactor = sf;
            this.StandardDeviation = sd;

            dista = new NormalDistribution(sf * ma , sd);
            distb = new NormalDistribution(sf * mb , sd);

            PopulationEstimateA = new LengthComposition(ca.Name, ca.Minimum, ca.Maximum, ca.Interval);
            PopulationEstimateB = new LengthComposition(cb.Name, cb.Minimum, cb.Maximum, cb.Interval);

            for (int i = 0; i < ca.Count; i++)
            {
                PopulationEstimateA[i].Abundance = ca[i].Abundance / dista.ProbabilityDensity(ca[i].Size.Midpoint);
                PopulationEstimateB[i].Abundance = cb[i].Abundance / distb.ProbabilityDensity(cb[i].Size.Midpoint);
            }
        }
    }

    public class GillnetSelectivityModel
    {
        public List<double> Meshes;
        public List<LengthComposition> Catches;
        public List<ContinuousDistribution> Distributions;

        public double StandardDeviation { get; private set; }

        public double SelectionFactor { get; private set; }

        public int Dimension { get { return Catches.Count; } }

        double max;



        public GillnetSelectivityModel(List<LengthComposition> _catches, List<double> meshes)
        {
            Meshes = meshes;
            Catches = _catches;
            Distributions = new List<ContinuousDistribution>();

            double xx = 0;
            double xy = 0;
            double n = _catches.Count;
            double var = 0;

            for (int i = 0; i < _catches.Count - 1; i++)
            {
                LengthComposition ca = _catches[i];
                LengthComposition cb = _catches[i + 1];
                double ma = meshes[i];
                double mb = meshes[i + 1];

                GillnetPairSelectivityModel model = new GillnetPairSelectivityModel(ca, ma, cb, mb);

                double a = model.lin.Intercept.Value;
                double b = model.lin.Slope.Value;

                double x = ma + mb;
                double y = -2 * a / b;

                xx += x * x;
                xy += x * y;

                var += (model.StandardDeviation * model.StandardDeviation);
            }

            double sd = Math.Sqrt(var / (n - 1));
            double sf = xy / xx;

            for (int i = 0; i < _catches.Count; i++ )
            {
                ContinuousDistribution dist = new NormalDistribution(meshes[i] * sf, sd);

                //switch (UserSettings.SelectionCurveType)
                //{
                //    case DistributionType.Lognormal:
                //        dist = new LognormalDistribution(meshes[i] * sf, sd);
                //        break;

                //    case DistributionType.Normal:
                //        dist = new NormalDistribution(meshes[i] * sf, sd);
                //        break;
                //}

                Distributions.Add(dist);

                //Histogram hist = _catches[i].GetHistogram();
                //hist.CalculateApproximation(UserSettings.SelectionCurveType);

                //Distr
                //switch (UserSettings.SelectionCurveType)
                //{
                //    default:
                //    case DistributionType.Auto:
                //        return Fittest(sample);
                //    case DistributionType.Lognormal:
                //        return new LognormalDistribution(summary.Mean, sample.StandardDeviation);
                //    case DistributionType.Normal:
                //        return new NormalDistribution(sample.Mean, sample.StandardDeviation);
                //}

                //Distributions.Add(hist.Distribution);
            }

            this.StandardDeviation = sd;
            this.SelectionFactor = sf;

            max = 0;

            for (double l = Catches[0].Minimum; l < Catches[0].Ceiling; l++)
            {
                double S = 0;
                for (int i = 0; i < this.Dimension; i++)
                {
                    double s = GetSelection(i, l);
                    S += s;
                }
                max = Math.Max(max, S);
            }
        }



        public LengthComposition Catch(int i)
        {
            return Catches[i];
        }

        public double OptimumLength(int i)
        {
            return OptimumLength(Meshes[i]);
        }

        public double OptimumLength(double mesh)
        {
            return this.SelectionFactor * mesh;
        }

        public double GetSelection(int i, double l)
        {
            double d = Distributions[i].ProbabilityDensity(l);
            double m = Distributions[i].ProbabilityDensity(Distributions[i].Mean);
            return Math.Abs(d / m);
        }

        public double GetSelection(double l)
        {
            double s = 0;

            for (int i = 0; i < this.Dimension; i++)
            {
                s += GetSelection(i, l);
            }

            return s / max;
        }

        public void AddReport1(Report report, string tableheader)
        {
            Report.Table table1 = new Report.Table(tableheader);

            table1.StartHeader();
            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Selectivity.Column1_1, 2);
            table1.AddHeaderCell(Resources.Reports.Selectivity.Column1_2, 4);
            table1.EndRow();
            table1.StartRow();
            table1.AddHeaderCell("m<sub>a</sub>");
            table1.AddHeaderCell("m<sub>b</sub>");
            table1.AddHeaderCell("L<sub>m<sub>a</sub></sub>");
            table1.AddHeaderCell("L<sub>m<sub>b</sub></sub>");
            table1.AddHeaderCell("SF");
            table1.AddHeaderCell("SD");
            table1.EndRow();
            table1.EndHeader();

            for (int i = 0; i < Catches.Count - 1; i++)
            {
                LengthComposition ca = Catches[i];
                LengthComposition cb = Catches[i + 1];
                double ma = Meshes[i];
                double mb = Meshes[i + 1];

                GillnetPairSelectivityModel model = new GillnetPairSelectivityModel(ca, ma, cb, mb);

                table1.StartRow();

                table1.AddCellValue(ma);
                table1.AddCellValue(mb);
                table1.AddCellRight(model.dista.Mean, "N1");
                table1.AddCellRight(model.distb.Mean, "N1");
                table1.AddCellRight(model.SelectionFactor, "N4");
                table1.AddCellRight(model.StandardDeviation, "N4");

                table1.EndRow(); 
            }

            report.AddTable(table1);
        }

        public void AddReport2(Report report, string tableheader)
        {
            Report.Table table1 = new Report.Table(tableheader);

            table1.StartHeader();

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Selectivity.Column2_1, .2, 2);
            table1.AddHeaderCell(Resources.Reports.Caption.GearClass, Dimension);
            table1.AddHeaderCell(Resources.Reports.Selectivity.Column2_2, 2, CellSpan.Rows);
            table1.EndRow();

            table1.StartRow();

            foreach (Composition composition in Catches)
            {
                table1.AddHeaderCell(composition.Name);
            }
            table1.EndRow();

            table1.EndHeader();

            LengthComposition example = Catches[0];

            for (int i = 0; i < example.Count; i++)
            {
                table1.StartRow();
                table1.AddCellValue(example[i].Size.Midpoint);
                for (int j = 0; j < Dimension; j++)
                {
                    double s = GetSelection(j, example[i].Size.Midpoint);
                    table1.AddCellRight(s, "N4");
                }

                table1.AddCellRight(GetSelection(example[i].Size.Midpoint), "N4");
                table1.EndRow();

            }

            table1.StartRow();
            table1.AddCellValue(Resources.Reports.Selectivity.Column2_3);
            for (int j = 0; j < Dimension; j++)
            {
                double o = OptimumLength(j);
                table1.AddCellRight(o, "N1");
            }
            table1.AddCellValue(Constants.Null);
            table1.EndRow();

            report.AddTable(table1);
        }
    }
}
