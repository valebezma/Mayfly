using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mayfly.Fish.Explorer
{
    public class Cohort : AgeComposition
    {
        /// <summary>
        /// Year of cohort release
        /// </summary>
        public int Birth { get; set; }

        //public double[] Survivors { get; internal set; }
        //public AgeGroup[] Survivors { get; internal set; }
        public AgeComposition Survivors { get; internal set; }

        /// <summary>
        /// Fishery mortality rates
        /// </summary>
        public double[] F { get; internal set; }


        public Cohort(int birth, AgeComposition composition)
            : base(string.Format(Resources.Interface.Interface.CohortPresentation, birth),
            composition.Youngest, composition.Oldest)
        {
            Birth = birth;
            F = new double[Count];
            Survivors = new AgeComposition(
                string.Format("Survivors of {0} generation", birth),
                composition.Youngest,
                composition.Oldest);
        }



        public int GetCatchYear(int i)
        {
            return Birth + this[i].Age.Years;
        }

        public int GetYear(Age age)
        {
            return Birth + age.Years;
        }

        public double GetFisheryMortality(int i)
        {
            return this.F[i];
        }

        public Cohort GetNextGeneration()
        {
            Cohort result = new Cohort(Birth + 1, this);
            return result;
        }

        public AgeGroup GetInitialState()
        {
            if (this.Count < 1) return null;

            if (this.Survivors[0].Quantity > 0)
            {
                return this.Survivors[0];
            }
            else
            {
                try
                {
                    BivariateSample data = new BivariateSample();
                    for (int i = 0; i < this.Count; i++) {
                        if (this.Survivors[i].Quantity == 0) continue;
                        double x = (this[i]).Age;
                        data.Add(x, this.Survivors[i].Quantity);
                    }

                    Exponent exp = new Exponent(data);
                    double n = exp.Predict(0.5);                    
                    return new AgeGroup((Age)0) { Quantity = (int)n };
                }
                catch
                {
                    return null;
                }
            }
        }

        public Report.Table GetTable()
        {
            Report.Table table1 = new Report.Table(Resources.Reports.Sections.VPA.Table3, this.Name);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Sections.Growth.Column1, .2);
            table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column2);
            table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column3);
            table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column4);
            table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column5);
            table1.EndRow();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Quantity == 0) continue;

                table1.StartRow();
                table1.AddCellValue(this[i].Name);
                table1.AddCellValue(this.GetCatchYear(i));
                table1.AddCellRight(this[i].Quantity, "N0");
                table1.AddCellRight(this.F[i], "N4");
                table1.AddCellRight(this.Survivors[i].Quantity, "N0");
                table1.EndRow();
            }

            return table1;
        }

        public Scatterplot GetGrowthScatterplot()
        {
            return this.GetGrowthScatterplot(false);
        }

        public Scatterplot GetGrowthScatterplot(bool isChronic)
        {
            if (isChronic)
                throw new ArgumentException("Only thiss provide chronic growth charts.");

            BivariateSample sample = new BivariateSample(
                isChronic ? "Year" : Wild.Resources.Reports.Caption.Age,
                Wild.Resources.Reports.Caption.Length);

            foreach (AgeGroup cat in this)
            {
                if (cat.LengthSample == null) continue;
                if (cat.LengthSample.Count == 0) continue;

                double x = cat.Age.Years;
                double y = cat.LengthSample.Mean;

                if (isChronic)
                {
                    x += this.Birth;
                    x = new DateTime((int)x, 3, 1).ToOADate();
                }

                sample.Add(x, y);
            }

            if (sample.Count == 0) return null;

            Scatterplot result = new Scatterplot(sample, this.Name);
            result.IsChronic = isChronic;
            result.Properties.ShowTrend = true;
            result.Properties.SelectedApproximationType = TrendType.Growth;
            return result;
        }

        public Scatterplot GetHistory(bool isChronic)
        {
            BivariateSample data = new BivariateSample();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Quantity == 0) continue;

                double x = this[i].Age;

                if (isChronic)
                {
                    x += this.Birth;
                    x = Fish.Explorer.Service.GetCatchDate((int)x).ToOADate();
                }

                data.Add(x, (double)this[i].Quantity / 1000);
            }

            Scatterplot result = new Scatterplot(data, this.Name);
            result.Series.ChartType = SeriesChartType.Line;
            result.IsChronic = isChronic;

            return result;
        }

        public Scatterplot GetSurvivorsHistory(bool isChronic)
        {
            BivariateSample data = new BivariateSample();

            for (int i = 0; i < this.Count; i++)
            {
                if (this.Survivors[i].Quantity == 0) continue;

                double x = this[i].Age;

                if (isChronic)
                {
                    x += this.Birth;
                    x = Fish.Explorer.Service.GetCatchDate((int)x).ToOADate();
                }

                data.Add(x, (double)this.Survivors[i].Quantity / 1000);
            }

            Scatterplot result = new Scatterplot(data, this.Name);
            result.Series.ChartType = SeriesChartType.Line;
            result.IsChronic = isChronic;

            return result;
        }
    }
}
