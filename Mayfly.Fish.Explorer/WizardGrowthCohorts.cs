using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardGrowthCohorts : Form
    {
        public CardStack Data { get; set; }

        public TaxonomicIndex.TaxonRow TaxonRow;

        public List<Cohort> Cohorts;

        public List<Scatterplot> GrowthModels { get; private set; }

        public List<Scatterplot> WeightModels { get; private set; }



        private WizardGrowthCohorts()
        {
            InitializeComponent();

            Cohorts = new List<Cohort>();
            GrowthModels = new List<Scatterplot>();
            WeightModels = new List<Scatterplot>();

            ColumnCohAge.ValueType = typeof(Age);

            UI.SetControlAvailability(License.AllowedFeaturesLevel == FeatureLevel.Insider,
                buttonAL, 
                buttonLW, 
                contextGrowth,
                contextMass);

            this.RestoreAllCheckStates();
        }

        public WizardGrowthCohorts(CardStack data, TaxonomicIndex.TaxonRow speciesRow) : this()
        {
            Data = data;
            TaxonRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.CommonName);
        }



        public Report GetReport()
        {
            Report report = new Report(string.Format(
                Resources.Reports.Sections.GrowthCohorts.Title,
                TaxonRow.FullNameReport));
            Data.AddCommon(report, TaxonRow);

            report.UseTableNumeration = true;

            if (checkBoxHistory.Checked)
            {
                AddHistory(report);
            }

            if (checkBoxGrowth.Checked)
            {
                AddGrowth(report);
            }

            if (checkBoxMass.Checked)
            {
                AddMass(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddHistory(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.GrowthCohorts.Paragraph1,
                TaxonRow.FullNameReport, report.NextTableNumber);

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.GrowthCohorts.Table1,
                TaxonRow.FullNameReport);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column1, .2, 2);
            table1.AddHeaderCell(Resources.Reports.Sections.GrowthCohorts.Column1, spreadSheetCohorts.InsertedColumnCount);
            table1.EndRow();
            table1.StartRow();
            foreach (Composition composition in Cohorts) {
                if (composition.TotalQuantity == 0) continue;
                table1.AddHeaderCell(composition.Name);
            }
            table1.EndRow();


            for (int i = 0; i < Cohorts[0].Count; i++)
            {
                table1.StartRow();
                table1.AddCellValue(Cohorts[0][i].Name);
                foreach (Composition composition in Cohorts) {

                    if (composition.TotalQuantity == 0) continue;

                    if (composition[i].LengthSample.Count == 0) table1.AddCell();
                    else table1.AddCellValue(new SampleDisplay(composition[i].LengthSample), 
                        spreadSheetCohorts.Columns[i + 1].DefaultCellStyle.Format);
                }
                table1.EndRow();
            }

            report.AddTable(table1);
        }

        public void AddGrowth(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.GrowthCohorts.Paragraph2,
                TaxonRow.FullNameReport, report.NextTableNumber);
            report.AddEquation(@"L = {L_∞} (1 - e^{-K (t - {t_0})})");

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.GrowthCohorts.Table2,
                TaxonRow.FullNameReport);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Sections.GrowthCohorts.Column1, .2);
            table1.AddHeaderCell("L<sub>∞</sub>");
            table1.AddHeaderCell("K");
            table1.AddHeaderCell("t<sub>0</sub>");
            table1.EndRow();

            foreach (Scatterplot scatter in GrowthModels)
            {
                //Mathematics.Statistics.Regression model = scatter.Regression;

                //if (model == null) continue;

                //table1.StartRow();
                //table1.AddCell(scatter.Name);
                //table1.AddCellValue(model.Parameters[0]);
                //table1.AddCellValue(model.Parameters[1]);
                //table1.AddCellValue(model.Parameters[2]);
                //table1.EndRow();
            }

            report.AddTable(table1);
        }

        public void AddMass(Report report)
        {
            //report.AddParagraph(Resources.Reports.Sections.GrowthCohorts.Paragraph3,
            //    TaxonRow.FullNameReport, report.NextTableNumber);
            //report.AddEquation(@"W = {q} \times {L^{b}}");

            //Report.Table table1 = new Report.Table(Resources.Reports.Sections.GrowthCohorts.Table3,
            //    TaxonRow.FullNameReport);

            //table1.StartRow();
            //table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column1, .2);
            //table1.AddHeaderCell("q");
            //table1.AddHeaderCell("b");
            //table1.EndRow();

            //foreach (Scatterplot scatter in WeightModels)
            //{
            //    Mathematics.Statistics.Regression model = scatter.Regression;

            //    if (model == null) continue;

            //    table1.StartRow();
            //    table1.AddCell(scatter.Name);
            //    table1.AddCellValue(model.Parameters[0].ToString("N4"));
            //    table1.AddCellValue(model.Parameters[1].ToString("N4"));
            //    table1.EndRow();
            //}

            //report.AddTable(table1);
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            spreadSheetCohorts.Rows.Clear();
            statChartAL.Clear();
            statChartLW.Clear();
            pageCohorts.SetNavigation(false);
            cohortsDetector.RunWorkerAsync();
        }

        private void cohortsDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            Cohorts = Data.GetCohorts(TaxonRow, FishSamplerType.None, GearWeightType.None, EffortExpression.Square);

            GrowthModels.Clear();
            WeightModels.Clear();
            foreach (Cohort cohort in Cohorts)
            {
                Scatterplot growth = cohort.GetGrowthScatterplot(true);
                if (growth != null)
                {
                    GrowthModels.Add(growth);
                }

                Scatterplot weight = cohort.GetWeightScatterplot(Wild.Resources.Reports.Caption.MassUnit);
                if (weight != null)
                {
                    WeightModels.Add(weight);
                }
            }
        }

        private void cohortsDetector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelNoData.Visible = e.Error is AgeArgumentException;

            if (e.Error is AgeArgumentException)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                Cohorts[0].SetLines(ColumnCohAge);

                foreach (Composition cohort in Cohorts)
                {
                    if (cohort.TotalQuantity == 0) continue;

                    DataGridViewColumn cohColumn = spreadSheetCohorts.InsertColumn(cohort.Name);
                    cohColumn.ValueType = typeof(SampleDisplay);
                    cohColumn.DefaultCellStyle.Format = "S";

                    for (int i = 0; i < cohort.Count; i++)
                    {
                        if (cohort[i].LengthSample != null &&
                            cohort[i].LengthSample.Count > 0)
                            spreadSheetCohorts[cohColumn.Index, i].Value = new SampleDisplay(cohort[i].LengthSample);
                    }
                }

                pageCohorts.SetNavigation(true);


                statChartAL.Clear();
                statChartAL.Visible = buttonAL.Enabled = GrowthModels.Count > 0;

                while (contextGrowth.Items.Count > 2)
                {
                    contextGrowth.Items.RemoveAt(2);
                }

                foreach (Scatterplot scatter in GrowthModels)
                {
                    scatter.Properties.ShowTrend = true;
                    scatter.Properties.SelectedApproximationType = TrendType.Growth;
                    scatter.Series.ChartType = SeriesChartType.Line;
                    statChartAL.AddSeries(scatter);

                    ToolStripMenuItem item = new ToolStripMenuItem(scatter.Properties.ScatterplotName);
                    item.Click += itemGrowth_Click;
                    contextGrowth.Items.Add(item);
                }

                statChartAL.DoPlot();





                statChartLW.Clear();
                statChartLW.Visible = buttonLW.Enabled = WeightModels.Count > 0;

                while (contextMass.Items.Count > 2)
                {
                    contextMass.Items.RemoveAt(2);
                }

                foreach (Scatterplot scatter in WeightModels)
                {
                    scatter.Properties.ShowTrend = true;
                    scatter.Properties.SelectedApproximationType = TrendType.Power;
                    statChartLW.AddSeries(scatter);

                    ToolStripMenuItem item = new ToolStripMenuItem(scatter.Properties.ScatterplotName);
                    item.Click += itemMass_Click;
                    contextMass.Items.Add(item);
                }

                statChartLW.DoPlot();
            }
        }

        private void itemGrowth_Click(object sender, EventArgs e)
        {
            ((Scatterplot)statChartAL.GetSample(((ToolStripMenuItem)sender).Name)).OpenRegressionProperties();
        }

        private void itemMass_Click(object sender, EventArgs e)
        {
            ((Scatterplot)statChartLW.GetSample(((ToolStripMenuItem)sender).Name)).OpenRegressionProperties();
        }

        private void buttonAL_Click(object sender, EventArgs e)
        {
            contextGrowth.Show(buttonAL, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void buttonLW_Click(object sender, EventArgs e)
        {
            contextMass.Show(buttonLW, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void contextGrowthAll_Click(object sender, EventArgs e)
        {
            List<Mathematics.Statistics.Regression> regressions = new List<Mathematics.Statistics.Regression>();

            foreach (Scatterplot scatterplot in statChartAL.Scatterplots)
            {
                if (scatterplot.Calc.IsRegressionOK)
                {
                    regressions.Add(scatterplot.Calc.Regression);
                }
            }

            RegressionComparison regressionComparison = new RegressionComparison(regressions.ToArray());
            regressionComparison.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.Centered);
            regressionComparison.Show(this.FindForm());
        }

        private void contextMassAll_Click(object sender, EventArgs e)
        {
            List<Mathematics.Statistics.Regression> regressions = new List<Mathematics.Statistics.Regression>();

            foreach (Scatterplot scatterplot in statChartLW.Scatterplots)
            {
                if (scatterplot.Calc.IsRegressionOK)
                {
                    regressions.Add(scatterplot.Calc.Regression);
                }
            }

            RegressionComparison regressionComparison = new RegressionComparison(regressions.ToArray());
            regressionComparison.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.Centered);
            regressionComparison.Show(this.FindForm());
        }
        
        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();

            e.Cancel = true;
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetReport();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageReport.SetNavigation(true);
            ((Report)e.Result).Run();
            Log.Write(EventType.WizardEnded, "Cohorts growth wizard is finished for {0} with {1} equations.",
                TaxonRow.Name, GrowthModels.Count);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
