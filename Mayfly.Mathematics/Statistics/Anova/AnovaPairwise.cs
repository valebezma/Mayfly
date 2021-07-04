using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Mathematics.Charts;
using Mayfly.Controls;
using Mayfly.Extensions;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Statistics
{
    public partial class AnovaPairwise : Form
    {
        private double tresholdDifference;

        public double TresholdDifference
        {
            get
            {
                return tresholdDifference;
            }

            set
            {
                tresholdDifference = value;
                textBoxSD.Text = value.ToString();
            }
        }

        private AnovaFactorRow Factor;

        private Anova Anova;

        public double Alpha
        {
            set { numericUpDownAlpha.Value = (decimal)value; }
            get { return (double)numericUpDownAlpha.Value; }
        }

        public double SD
        {
            set;
            get;
        }

        private Evaluation evaluation;

        private string Format;

        Report Report
        {
            get
            {

                Report report = new Report("Anova pairwise comparisons");


                Report.Table table1 = new Report.Table("Index calculation");
                table1.StartRow();
                table1.AddCellPrompt("Procedure", UserSettings.LsdIndexName);
                table1.EndRow();
                table1.StartRow();
                table1.AddCellPrompt("Significant difference", SD.ToString(Format));
                table1.EndRow();
                table1.StartRow();
                table1.AddCellPrompt("Alpha level", Alpha);
                table1.EndRow();
                report.AddTable(table1);

                Report.Table table2 = new Report.Table("Comparisons");
                table2.AddHeader(new string[] { "#", "Samples", "Means", "Difference", "Significance" },
                    new double[] { .05, .40, .15, .1, .2 });

                foreach (DataGridViewRow gridRow in spreadSheetPairs.Rows)
                {
                    Sample sample1 = (Sample)gridRow.Cells[columnSample1Pres.Index].Value;
                    Sample sample2 = (Sample)gridRow.Cells[columnSample2Pres.Index].Value;
                    double d = (double)gridRow.Cells[columnDifference.Index].Value;

                    table2.StartRow();
                    table2.AddCellValue(gridRow.Index + 1, 2);
                    table2.AddCell(sample1.Name);
                    table2.AddCellValue(sample1.Mean.ToString(Format));
                    table2.AddCellRight(d, Format, 2, CellSpan.Rows);
                    table2.AddCell((d > SD ? "Significant" : "Non significant"), 2, CellSpan.Columns);
                    table2.EndRow();

                    table2.StartRow();
                    table2.AddCell(sample2.Name);
                    table2.AddCellValue(sample2.Mean.ToString(Format));
                    table2.EndRow();
                }

                report.AddTable(table2);
                report.EndSign();

                return report;
            }
        }

        public AnovaPairwise(Anova anova, int factorIndex)
        {
            InitializeComponent();

            columnDifference.ValueType = typeof(double);
            columnSample1Pres.ValueType =
                columnSample2Pres.ValueType = typeof(Sample);

            Factor = anova.Factors[factorIndex];
            Anova = anova;

            Format = "N" + (Anova.AllValues.Precision() + 1);

            evaluation = new Evaluation(Factor.Factor);
            evaluation.AddRange(Factor.Levels);
            evaluation.ShowOnChart(statChart1);
            statChart1.AxisYTitle = Anova.AllValues.Name;
            statChart1.AxisXTitle = Factor.Factor;
            statChart1.Remaster();
            evaluation.ValueChanged += evaluation_ValueChanged;

            columnSample1Pres.HeaderText = columnSample2Pres.HeaderText = 
                Anova.AllValues.Name;

            foreach (IEnumerable<Sample> pair in Factor.Levels.Combinations(2))
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetPairs);
                gridRow.Height = spreadSheetPairs.RowTemplate.Height;
                gridRow.Cells[columnSample1.Index].Value = pair.ElementAt(0).Name;
                gridRow.Cells[columnSample1Pres.Index].Value = pair.ElementAt(0);
                gridRow.Cells[columnSample2.Index].Value = pair.ElementAt(1).Name;
                gridRow.Cells[columnSample2Pres.Index].Value = pair.ElementAt(1);
                gridRow.Cells[columnDifference.Index].Value = Math.Abs(pair.ElementAt(0).Mean - pair.ElementAt(1).Mean);

                spreadSheetPairs.Rows.Add(gridRow);
            }

            ValueChanged(numericUpDownAlpha, new EventArgs());
        }

        void evaluation_ValueChanged(object sender, EvaluationEventArgs e)
        {
            if (spreadSheetPairs.SelectedRows.Count == 0) return;

            Sample sample1 = (Sample)spreadSheetPairs.SelectedRows[0].Cells[columnSample1Pres.Index].Value;
            Sample sample2 = (Sample)spreadSheetPairs.SelectedRows[0].Cells[columnSample2Pres.Index].Value;

            int index1 = evaluation.IndexOf(sample1);
            int index2 = evaluation.IndexOf(sample2);

            evaluation.DataSeries.Points[index1].Color = evaluation.Properties.DataPointColor.Darker();
            evaluation.DataSeries.Points[index2].Color = evaluation.Properties.DataPointColor.Darker();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            switch (UserSettings.LsdIndex)
            {
                case 0:
                    SD = Test.FisherLSD(Anova.Residual, Alpha, Anova.HarmonicGroupSize);
                    break;
                case 1:
                    SD = Test.TukeyHSD(Anova.Residual, Alpha, Factor.Levels.Count, Anova.HarmonicGroupSize);
                    break;
            }

            textBoxSD.Text = SD.ToString(Format);

            foreach (DataGridViewRow gridRow in spreadSheetPairs.Rows)
            {
                double d = (double)gridRow.Cells[columnDifference.Index].Value;
                ((TextAndImageCell) gridRow.Cells[columnDifference.Index]).Image =
                    d > SD ? Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
            }
        }

        private void spreadSheetPairs_SelectionChanged(object sender, EventArgs e)
        {
            statChart1.Remaster();
            evaluation_ValueChanged(sender, new EvaluationEventArgs(evaluation));
        }

        private void statChart1_AppearanceValueChanged(object sender, EventArgs e)
        {
            evaluation_ValueChanged(sender, new EvaluationEventArgs(evaluation));
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Report.Run();
        }
    }
}