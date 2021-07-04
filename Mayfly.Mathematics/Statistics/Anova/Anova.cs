using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Resources;
using System.Globalization;

namespace Mayfly.Mathematics.Statistics
{
    public class Anova
    {
        public Sample AllValues { get; private set; }

        public List<AnovaFactorRow> Factors { set; get; }

        public List<InteractionRow> Interactions { set; get; }

        public int HarmonicGroupSize
        {
            get
            {
                double totalCount = 0;

                foreach (Sample sample in AllVariants)
                {
                    totalCount += 1D / (double)sample.Count;
                }

                return (int)(AllVariants.Count / totalCount);
            }
        }

        public AnovaRow Residual { get; private set; }

        public AnovaRow Total { get; private set; }

        public TestResult Homoscedasticity;

        public List<Sample> AllVariants
        {
            get;
            set;
        }

        private Anova(Sample sample)
        {
            Interactions = new List<InteractionRow>();
            AllValues = sample;
        }

        private Anova(Sample sample, string name)
        {
            Interactions = new List<InteractionRow>();
            AllValues = sample;
            AllValues.Name = name;
        }

        /// <summary>
        /// Initiates one-way anova
        /// </summary>
        /// <param name="samples">Separate samples</param>
        /// <param name="factor">Name of splitting factor</param>
        /// <param name="variable">Name of numerical value</param>
        public Anova(List<Sample> samples, string factor, string variable)
            : this(SampleExtensions.Cumulated(samples), factor)
        {
            Factors = new List<AnovaFactorRow>();
            Factors.Add(new AnovaFactorRow(factor, samples));
            AllVariants = samples;
            Homoscedasticity = SampleExtensions.VarianceHomogeneity(samples);

            OneWayAnovaResult result = Sample.OneWayAnovaTest(samples.ToArray());
            Residual = result.Residual;
            Total = result.Total;
        }

        /// <summary>
        /// Initiates multi-way anova
        /// </summary>
        /// <param name="dataColumn">DataColumn containing numerical values</param>
        public Anova(DataColumn dataColumn)
            : this(dataColumn.GetSample())
        {
            Factors = dataColumn.GetAnovaResults();
            AllVariants = dataColumn.GetSamples();
            Homoscedasticity = SampleExtensions.VarianceHomogeneity(AllVariants);

            if (Factors.Count == 1)
            {
                OneWayAnovaResult result = Sample.OneWayAnovaTest(Factors[0].Levels.ToArray());
                Residual = result.Residual;
                Total = result.Total;
            }
            else
            {
                GrandTotal = AllValues.Sum();

                foreach (double d in AllValues)
                {
                    SumOfSquaredObservations += d * d;
                }

                MeanSquaredTotal = 0;

                foreach (Sample sample in AllVariants)
                {
                    MeanSquaredTotal += Math.Pow(sample.Sum(), 2);
                }

                MeanSquaredTotal /= HarmonicGroupSize;

                CorrectionTerm = Math.Pow(GrandTotal, 2) / AllValues.Count;

                double SST = SumOfSquaredObservations - CorrectionTerm;

                double SSB = MeanSquaredTotal - CorrectionTerm;

                double SSW = SST - SSB;

                int DFT = HarmonicGroupSize;
                int DFW = 1;

                foreach (AnovaFactorRow factorRow in Factors)
                {
                    DFT *= factorRow.Levels.Count;
                    DFW *= factorRow.Levels.Count;
                }

                DFT -= 1;
                DFW *= (HarmonicGroupSize - 1);

                Total = new AnovaRow(SST, DFT);
                Residual = new AnovaRow(SSW, DFW);

                for (int i = 2; i <= Factors.Count; i++)
                {
                    var interactions = Service.Combinations<AnovaFactorRow>(Factors, i);

                    foreach (IEnumerable<AnovaFactorRow> interaction in interactions)
                    {
                        InteractionRow interactionRow = new InteractionRow(this, interaction, dataColumn);
                        Interactions.Add(interactionRow);
                    }
                }
            }
        }

        internal double GrandTotal;

        internal double SumOfSquaredObservations;

        internal double MeanSquaredTotal;

        internal double CorrectionTerm;

        public int IndexOf(string factor)
        {
            for (int i = 0; i < Factors.Count; i++)
            {
                if (Factors[i].Factor == factor)
                {
                    return i;
                }
            }

            return -1;
        }

        public List<Sample> Find(string factor)
        {
            int index = IndexOf(factor);
            return index == -1 ? null : Factors[index].Levels;
        }


        public Report Report(double alpha)
        {
            Report report = new Report(Resources.Reports.Anova.Title);

            report.UseTableNumeration = true;

            report.AddParagraph(Resources.Reports.Anova.parStart,
                Factors.Merge(", "), AllValues.Name);

            report.AddSubtitle(Resources.Reports.Anova.TitleAssumptions);

            report.AddSubtitle3(Resources.Reports.Anova.HeaderNormality);

            report.AddParagraph(Resources.Reports.Anova.parNormality, 
                UserSettings.NormalityTestName, report.NextTableNumber);
            
            Report.Table table1 = new Report.Table(Resources.Reports.Anova.tbNormality);

            ResourceManager sampleRes = new ResourceManager(typeof(SampleProperties));

            table1.AddHeader(new string[] { Resources.Reports.Anova.thSubgroup, 
                sampleRes.GetString("labelCount.Text"),
                sampleRes.GetString("labelSum.Text"),
                sampleRes.GetString("labelMean.Text"), 
                sampleRes.GetString("labelVariance.Text"), 
                string.Format(Resources.Reports.Anova.thNormality, UserSettings.NormalityTestName) },
                new double[] { .25 });

            foreach (Sample sample in AllVariants)
            {
                table1.StartRow();
                table1.AddCell(sample.Name);
                table1.AddCellValue(sample.Count);
                table1.AddCellValue(sample.Sum().ToString(sample.MeanFormat()));
                table1.AddCellValue(sample.Mean.ToString(sample.MeanFormat()));
                table1.AddCellValue(Service.PresentError(sample.Variance));
                table1.AddCellValue(sample.Normality().Probability.ToString("N4"));

                table1.EndRow();
            }
            report.AddTable(table1);


            report.AddSubtitle3(Resources.Reports.Anova.HeaderHomogeneity);

            if (this.Homoscedasticity == null ||
                double.IsNaN(this.Homoscedasticity.Statistic) ||
                double.IsInfinity(this.Homoscedasticity.Statistic))
            {
                report.AddParagraph(Resources.Statistics.UnableToCalculate);
            }
            else if (this.Homoscedasticity.IsPassed())
            {
                report.AddParagraph(Resources.Statistics.HomoscedasticityPositive,
                    UserSettings.HomogeneityTestName, this.Homoscedasticity.Statistic,
                    this.Homoscedasticity.Probability,
                    this.Homoscedasticity.Probability);
            }
            else
            {
                report.AddParagraph(Resources.Statistics.HomoscedasticityNegative,
                    UserSettings.HomogeneityTestName, this.Homoscedasticity.Statistic,
                    this.Homoscedasticity.Probability);
            }



            report.AddSubtitle(Resources.Reports.Anova.TitleResults);

            report.AddParagraph(Resources.Reports.Anova.parAnova,
                AllValues.Name, report.NextTableNumber, AllValues.Name);

            Report.Table table2 = new Report.Table(Resources.Reports.Anova.tbResults);
            
            ResourceManager anovaRes = new ResourceManager(typeof(AnovaProperties));

            table2.AddHeader(new string[] {
                    anovaRes.GetString("columnSource.HeaderText"),
                    anovaRes.GetString("columnSS.HeaderText"), 
                    anovaRes.GetString("columnDF.HeaderText"),
                    anovaRes.GetString("columnMS.HeaderText"), 
                    anovaRes.GetString("columnF.HeaderText"), 
                    anovaRes.GetString("columnP.HeaderText")
                },
                new double[] { .25 });

            bool hasImpact = false;

            foreach (AnovaFactorRow factorRow in Factors)
            {
                TestResult testResult = factorRow.Result(Residual);

                bool hasimpact = testResult.IsPassed(alpha);

                hasImpact |= hasimpact;

                table2.StartRow();
                table2.AddCell(factorRow.Factor, hasimpact);
                table2.AddCellRight(factorRow.SumOfSquares, "N4");
                table2.AddCellRight(factorRow.DegreesOfFreedom);
                table2.AddCellRight(factorRow.Variance, "N4");

                table2.AddCellRight(testResult.Statistic, "N4");
                table2.AddCellRight(testResult.Probability, "N4", hasimpact);
                table2.EndRow();
            }

            foreach (InteractionRow interactionRow in Interactions)
            {
                TestResult testResult = interactionRow.Result;

                bool hasimpact = testResult.IsPassed(alpha);

                hasImpact |= hasimpact;

                table2.StartRow();
                table2.AddCell(interactionRow.Interaction, hasimpact);
                table2.AddCellRight(interactionRow.SumOfSquares, "N4");
                table2.AddCellRight(interactionRow.DegreesOfFreedom);
                table2.AddCellRight(interactionRow.Variance, "N4");
                table2.AddCellRight(testResult.Statistic, "N4");
                table2.AddCellRight(testResult.Probability, "N4", hasimpact);
                table2.EndRow();
            }

            table2.StartRow();
            table2.AddCell(Resources.Interface.AnovaResidual);
            table2.AddCellRight(Residual.SumOfSquares, "N4");
            table2.AddCellRight(Residual.DegreesOfFreedom);
            table2.AddCellRight(Residual.Variance(), "N4");
            table2.AddCell();
            table2.AddCell();
            table2.EndRow();

            table2.StartRow();
            table2.AddCell(Resources.Interface.AnovaTotal);
            table2.AddCellRight(Total.SumOfSquares, "N4");
            table2.AddCellRight(Total.DegreesOfFreedom);
            table2.AddCell();
            table2.AddCell();
            table2.AddCell();
            table2.EndRow();

            report.AddTable(table2);

            string format = AllValues.MeanFormat();

            if (hasImpact)
            {
                report.AddSubtitle(Resources.Reports.Anova.TitlePairwise);

                report.AddParagraph(Resources.Reports.Anova.parPairwise, 
                    UserSettings.LsdIndexName, alpha);

                foreach (AnovaFactorRow factorRow in Factors)
                {
                    TestResult testResult = factorRow.Result(Residual);

                    if (testResult.IsPassed(alpha))
                    {
                        double significantDifference = 1;

                        switch (UserSettings.LsdIndex)
                        {
                            case 0:
                                significantDifference = Test.FisherLSD(this.Residual, alpha, this.HarmonicGroupSize);
                                break;
                            case 1:
                                significantDifference = Test.TukeyHSD(this.Residual, alpha, factorRow.Levels.Count, this.HarmonicGroupSize);
                                break;
                        }

                        report.AddSubtitle3(string.Format(Resources.Reports.Anova.HeaderPairwise,
                            factorRow.Factor));

                        report.AddParagraph(Resources.Reports.Anova.parFactor,
                            significantDifference, report.NextTableNumber);


                        Report.Table table3 = new Report.Table(Resources.Reports.Anova.tbPairwise, 
                            factorRow.Factor);

                        table3.StartHeader();

                        table3.StartRow();
                        table3.AddHeaderCell(Resources.Reports.Anova.thSubgroup, 2, CellSpan.Rows);
                        table3.AddHeaderCell(sampleRes.GetString("labelMean.Text"), 2, CellSpan.Rows);
                        table3.AddHeaderCell(Resources.Reports.Anova.thSubgroup, factorRow.Levels.Count);
                        table3.EndRow();

                        table3.StartRow();
                        for (int i = factorRow.Levels.Count - 1; i >= 0; i--)
                        { table3.AddHeaderCell(factorRow.Levels[i].Name); }
                        table3.EndRow();

                        table3.EndHeader();

                        for (int i = 0; i < factorRow.Levels.Count; i++)
                        {
                            table3.StartRow();

                            table3.AddCell(factorRow.Levels[i].Name);
                            table3.AddCellRight(factorRow.Levels[i].Mean, format);

                            for (int j = factorRow.Levels.Count - 1; j >= 0; j--)
                            {
                                if (i == j)
                                {
                                    table3.AddCellValue(Constants.Null);
                                }
                                else if (i > j)
                                {
                                    table3.AddCell();
                                }
                                else
                                {
                                    Sample sample1 = factorRow.Levels[i];
                                    Sample sample2 = factorRow.Levels[j];
                                    double d = Math.Abs(sample1.Mean - sample2.Mean);
                                    table3.AddCellRight(d, format, d >= significantDifference);
                                }
                            }

                            table3.EndRow();
                        }
                    }
                }
            }

            return report;
        }
    }

    public class AnovaFactorRow : IFormattable
    {
        public string Factor { get; private set; }

        public List<Sample> Levels
        {
            set
            {
                levels = value;

                this.mean = SampleExtensions.MeanOf(this.levels);
                this.n = 0;
                for (int i = 0; i < Levels.Count; i++)
                {
                    this.n += Levels[i].Count;
                }
            }

            get
            {
                return this.levels;
            }
        }

        public double Mean
        {
            get { return this.mean; }
        }

        public int Count
        {
            get { return this.n; }
        }

        public int DegreesOfFreedom { get; internal set; }

        public double SumOfSquares { get; internal set; }

        public double Variance { get { return SumOfSquares / DegreesOfFreedom; } }

        public AnovaFactorRow(string name, List<Sample> samples)
        {
            Factor = name;

            levels = samples;
            DegreesOfFreedom = levels.Count - 1;

            double m = SampleExtensions.MeanOf(levels);
            double SSB = 0.0;
            for (int i = 0; i < levels.Count; i++)
            {
                SSB += levels[i].Count * Math.Pow(levels[i].Mean - m, 2);
            }

            SumOfSquares = SSB;

            Format = SampleExtensions.Cumulated(levels).MeanFormat();
        }

        public AnovaFactorRow(DataColumn dataColumn, DataColumn factorColumn) :
            this(factorColumn.Caption, dataColumn.GetSamples(factorColumn))
        {
            //ColumnName = factorColumn.ColumnName;
        }

        public TestResult Result(AnovaRow residualsRow)
        {
            double F = (this.SumOfSquares / this.DegreesOfFreedom) / (residualsRow.SumOfSquares / residualsRow.DegreesOfFreedom);
            Distribution D = new FisherDistribution(this.DegreesOfFreedom, residualsRow.DegreesOfFreedom);
            return (new TestResult(F, D));
        }

        private List<Sample> levels;

        private double mean;

        private int n;

        private string Format;

        public void AddPairwiseComparisons(Report report, Anova anova, double alpha)
        {
            ResourceManager pairwiseRes = new ResourceManager(typeof(AnovaPairwise));

            report.AddSubtitle(string.Format("{0}. {1}", Factor, pairwiseRes.GetString("$this.Text")));

            double SD = 1;

            switch (UserSettings.LsdIndex)
            {
                case 0:
                    SD = Test.FisherLSD(anova.Residual, alpha, anova.HarmonicGroupSize);
                    break;
                case 1:
                    SD = Test.TukeyHSD(anova.Residual, alpha, Levels.Count, anova.HarmonicGroupSize);
                    break;
            }

            Report.Table table1 = new Report.Table("Index calculation");
            table1.StartRow();
            table1.AddCellPrompt("Procedure", UserSettings.LsdIndexName);
            table1.EndRow();
            table1.StartRow();
            table1.AddCellPrompt(pairwiseRes.GetString("labelSD.Text"), SD.ToString(Format));
            table1.EndRow();
            table1.StartRow();
            table1.AddCellPrompt(pairwiseRes.GetString("labelAlpha.Text"), alpha);
            table1.EndRow();
            report.AddTable(table1);

            Report.Table table2 = new Report.Table("Comparisons");
            table2.AddHeader(new string[] { "#", "Samples", "Means", "Difference", "Significance" },
                new double[] { .05, .50 });

            int index = 0;

            foreach (IEnumerable<Sample> pair in Levels.Combinations(2))
            {
                index++;
                Sample sample1 = pair.ElementAt(0);
                Sample sample2 = pair.ElementAt(1);
                double d = Math.Abs(sample1.Mean - sample2.Mean);

                table2.StartRow();
                table2.AddCellValue(index, 2);
                table2.AddCell(sample1.Name);
                table2.AddCellValue(sample1.Mean.ToString(Format));
                table2.AddCellRight(d, Format, 2, CellSpan.Rows);
                table2.AddCellValue(d > SD ? "Y" : "N", 2);
                table2.EndRow();

                table2.StartRow();
                table2.AddCell(sample2.Name);
                table2.AddCellValue(sample2.Mean.ToString(Format));
                table2.EndRow();
            }

            report.AddTable(table2);
        }


        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString(string.Empty, provider);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return this.Factor;
        }
    }

    public class InteractionRow
    {
        public Anova Parent { get; private set; }

        public string Interaction
        {
            get
            {
                string result = FactorRows.ElementAt(0).Factor;

                foreach (AnovaFactorRow factorRow in FactorRows)
                {
                    if (factorRow == FactorRows.ElementAt(0)) continue;

                    result += " × " + factorRow.Factor;
                }

                return result;
            }
        }

        public List<AnovaFactorRow> FactorRows { get; private set; }

        public DataColumn DataColumn { get; private set; }

        public DataTable Table
        {
            get { return DataColumn.Table; }
        }

        public int DegreesOfFreedom { get; private set; }

        public double SumOfSquares { get; private set; }

        public double Variance { get { return SumOfSquares / DegreesOfFreedom; } }

        public List<Sample> AllVariants
        {
            get;
            set;
        }

        public InteractionRow(Anova anova, IEnumerable<AnovaFactorRow> factorRows, DataColumn dataColumn)
        {
            this.Parent = anova;
            this.FactorRows = new List<AnovaFactorRow>(factorRows);
            this.DataColumn = dataColumn;

            List<DataColumn> factorColumns = new List<DataColumn>();
            foreach (AnovaFactorRow factorRow in FactorRows)
            {
                factorColumns.Add(dataColumn.Table.Columns[factorRow.Factor]);
            }

            AllVariants = dataColumn.GetSamples(factorColumns);

            if (FactorRows.Count == 2)
            {
                CalculateTwoWayInteraction();
            }
        }

        private void CalculateTwoWayInteraction()
        {
            // F1 SS

            double SS1 = 0;

            foreach (Sample level in FactorRows[0].Levels)
            {
                SS1 += Math.Pow(level.Sum(), 2);
            }

            SS1 /= (FactorRows[1].Levels.Count * Parent.HarmonicGroupSize);

            FactorRows[0].SumOfSquares = SS1 - Parent.CorrectionTerm;

            // F2 SS

            double SS2 = 0;

            foreach (Sample level in FactorRows[1].Levels)
            {
                SS2 += Math.Pow(level.Sum(), 2);
            }

            SS2 /= (FactorRows[0].Levels.Count * Parent.HarmonicGroupSize);

            FactorRows[1].SumOfSquares = SS2 - Parent.CorrectionTerm;

            // Interaction SS

            double SSG = Parent.MeanSquaredTotal - Parent.CorrectionTerm;

            SumOfSquares = SSG - FactorRows[0].SumOfSquares - FactorRows[1].SumOfSquares;

            // Interaction DF

            DegreesOfFreedom = FactorRows[0].DegreesOfFreedom * FactorRows[1].DegreesOfFreedom;
        }

        public TestResult Result
        {
            get
            {
                double F = (this.SumOfSquares / this.DegreesOfFreedom) / (Parent.Residual.SumOfSquares / Parent.Residual.DegreesOfFreedom);
                Distribution D = new FisherDistribution(this.DegreesOfFreedom, Parent.Residual.DegreesOfFreedom);
                return (new TestResult(F, D));
            }
        }
    }
}