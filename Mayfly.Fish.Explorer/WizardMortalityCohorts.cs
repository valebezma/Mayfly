using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardMortalityCohorts : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        public TaxonomicIndex.TaxonRow TaxonRow;

        public List<Cohort> Cohorts;

        public List<BivariatePredictiveModel> CatchModels { get; private set; }

        public List<BivariatePredictiveModel> Unused { get; private set; }



        private WizardMortalityCohorts()
        { 
            InitializeComponent();

            Cohorts = new List<Cohort>();
            CatchModels = new List<BivariatePredictiveModel>();
            Unused = new List<BivariatePredictiveModel>();

            ColumnCohAge.ValueType = typeof(Age);

            UI.SetControlAvailability(License.AllowedFeaturesLevel == FeatureLevel.Insider,
                contextMortality);

            this.RestoreAllCheckStates();
        }

        public WizardMortalityCohorts(CardStack data, TaxonomicIndex.TaxonRow speciesRow) : this()
        {
            Data = data;
            TaxonRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.CommonName);
            labelStart.ResetFormatted(TaxonRow.CommonName);
        }



        public Report GetReport()
        {
            Report report = new Report(string.Format(
                Resources.Reports.Sections.MortalityCohorts.Title,
                TaxonRow.FullNameReport));
            gearWizard.SelectedData.AddCommon(report, TaxonRow);

            report.UseTableNumeration = true;

            if (checkBoxHistory.Checked)
            {
                AddHistory(report);
            }

            if (checkBoxMortality.Checked)
            {
                AddMortality(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddHistory(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.MortalityCohorts.Paragraph1,
                TaxonRow.FullNameReport, report.NextTableNumber);

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.MortalityCohorts.Table1,
                TaxonRow.FullNameReport);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Sections.VPA.Column1, .2, 2);
            table1.AddHeaderCell(Resources.Reports.Sections.GrowthCohorts.Column1, Cohorts.Count);
            table1.EndRow();
            table1.StartRow();
            foreach (Composition composition in Cohorts) {
                table1.AddHeaderCell(composition.Name);
            }
            table1.EndRow();


            for (int i = 0; i < Cohorts[0].Count; i++) {
                table1.StartRow();
                table1.AddCellValue(Cohorts[0][i].Name);
                foreach (Composition composition in Cohorts) {
                    table1.AddCellRight(composition[i].AbundanceFraction, 
                        spreadSheetCohorts.Columns[1].DefaultCellStyle.Format);
                }
                table1.EndRow();
            }

            report.AddTable(table1);
        }

        public void AddMortality(Report report)
        {
            report.AddParagraph(Resources.Reports.Sections.MortalityCohorts.Paragraph2,
                TaxonRow.FullNameReport, report.NextTableNumber);
            report.AddEquation(@"CPUE(%) = a \times e^{-{Z} \times {t}}");

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.MortalityCohorts.Table2,
                TaxonRow.FullNameReport);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Sections.GrowthCohorts.Column1, .2);
            table1.AddHeaderCell("a");
            table1.AddHeaderCell("Z");
            table1.AddHeaderCell("ф");
            table1.AddHeaderCell("S");
            table1.EndRow();

            foreach (BivariatePredictiveModel bpm in CatchModels)
            {
                table1.StartRow();
                table1.AddCellValue(bpm.Name);

                Mathematics.Statistics.Regression model = bpm.Regression;

                if (model == null)
                { 
                    table1.AddCellValue(Constants.Null);
                    table1.AddCellValue(Constants.Null);
                    table1.AddCellValue(Constants.Null);
                    table1.AddCellValue(Constants.Null);
                }
                else
                {
                    table1.AddCellValue(model.Parameters[0]);
                    double z = -model.Parameters[1];
                    table1.AddCellValue(-z);
                    table1.AddCellValue(Math.Exp(z));
                    table1.AddCellValue(1.0 - Math.Exp(z));
                }
                table1.EndRow();
            }
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            gearWizard = new WizardGearSet(Data, TaxonRow);
            gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
            gearWizard.Replace(this);
        }

        void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            this.Replace(gearWizard);

            pageCohorts.SetNavigation(false);
            cohortsDetector.RunWorkerAsync();
        }

        private void cohortsDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            Cohorts = gearWizard.GearData.GetCohorts(TaxonRow,
                gearWizard.SelectedSamplerType, gearWizard.WeightType, gearWizard.SelectedUnit.Variant);
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
                for (int i = 0; i < Cohorts[0].Count; i++)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.Height = spreadSheetCohorts.RowTemplate.Height;
                    gridRow.CreateCells(spreadSheetCohorts);
                    gridRow.Cells[ColumnCohAge.Index].Value = Cohorts[0][i].Age;

                    spreadSheetCohorts.Rows.Add(gridRow);
                }

                foreach (Composition cohort in Cohorts)
                {
                    DataGridViewColumn cohColumn = spreadSheetCohorts.InsertColumn(cohort.Name);
                    cohColumn.ValueType = typeof(double);
                    cohColumn.DefaultCellStyle.Format = "P2";

                    for (int i = 0; i < cohort.Count; i++)
                    {
                        double v = cohort[i].AbundanceFraction;

                        if (v == 0) spreadSheetCohorts[cohColumn.Index, i].Value = null;
                        else spreadSheetCohorts[cohColumn.Index, i].Value = v;
                    }
                }

                pageCohorts.SetNavigation(true);

                AgeComposition ac = gearWizard.GearData.GetAgeComposition(TaxonRow);

                comboBoxAge.DataSource = ac;
                comboBoxAge.SelectedItem = ac.MostAbundant;
            }
        }

        private void pageCohorts_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) gearWizard.Replace(this);
        }

        private void comboBoxAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageChart.SetNavigation(false);
            statChartMortality.Clear();

            modelCalculator.RunWorkerAsync(comboBoxAge.SelectedIndex);
        }

        private void modelCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            CatchModels.Clear();
            Unused.Clear();

            foreach (Cohort cohort in Cohorts)
            {
                BivariatePredictiveModel[] cohortCurves = cohort.GetCatchCurve(true, (int)e.Argument);

                if (cohortCurves[0].Data.Count > 0) {
                    Unused.Add(cohortCurves[0]);
                }

                if (cohortCurves[1].Data.Count > 0) {
                    CatchModels.Add(cohortCurves[1]);
                }
            }
        }

        private void modelCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            statChartMortality.Clear();

            statChartMortality.Visible =
                buttonM.Enabled =
                CatchModels.Count > 0;

            while (contextMortality.Items.Count > 2)
            {
                contextMortality.Items.RemoveAt(2);
            }

            foreach (BivariatePredictiveModel bpm in CatchModels)
            {
                Scatterplot scatter = new Scatterplot(bpm);
                scatter.Properties.ShowTrend = true;
                scatter.Properties.SelectedApproximationType = TrendType.Exponential;
                statChartMortality.AddSeries(scatter);

                ToolStripMenuItem item = new ToolStripMenuItem(scatter.Properties.ScatterplotName);
                item.Click += itemMortality_Click;
                contextMortality.Items.Add(item);
            }

            foreach (BivariatePredictiveModel bpm in Unused)
            {
                statChartMortality.AddSeries(new Scatterplot(bpm));
            }

            statChartMortality.DoPlot();

            pageChart.SetNavigation(true);
        }

        private void itemMortality_Click(object sender, EventArgs e)
        {
            ((Scatterplot)statChartMortality.GetSample(((ToolStripMenuItem)sender).Name)).OpenRegressionProperties();
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            contextMortality.Show(buttonM, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            Log.Write(EventType.WizardEnded, "Growth Wizard is finished for {0} with {1} models.",
                TaxonRow.Name, CatchModels.Count);
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
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
