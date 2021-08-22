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

namespace Mayfly.Fish.Explorer
{
    public partial class WizardMortalityCohorts : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        public Data.SpeciesRow SpeciesRow;

        public List<Cohort> Cohorts;

        public List<Scatterplot> CatchModels { get; private set; }

        public List<Scatterplot> Unused { get; private set; }



        private WizardMortalityCohorts()
        { 
            InitializeComponent();

            Cohorts = new List<Cohort>();
            CatchModels = new List<Scatterplot>();
            Unused = new List<Scatterplot>();

            ColumnCohAge.ValueType = typeof(Age);

            this.RestoreAllCheckStates();
        }

        public WizardMortalityCohorts(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
        }



        public Report GetReport()
        {
            Report report = new Report(string.Format(
                Resources.Reports.Sections.MortalityCohorts.Title,
                SpeciesRow.KeyRecord.FullNameReport));
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

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
                SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.MortalityCohorts.Table1,
                SpeciesRow.KeyRecord.FullNameReport);

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
                SpeciesRow.KeyRecord.FullNameReport, report.NextTableNumber);
            report.AddEquation(@"CPUE(%) = a \times e^{-{Z} \times {t}}");

            Report.Table table1 = new Report.Table(Resources.Reports.Sections.MortalityCohorts.Table2,
                SpeciesRow.KeyRecord.FullNameReport);

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Sections.GrowthCohorts.Column1, .2);
            table1.AddHeaderCell("a");
            table1.AddHeaderCell("Z");
            table1.AddHeaderCell("ф");
            table1.AddHeaderCell("S");
            table1.EndRow();

            foreach (Scatterplot scatter in CatchModels)
            {
                table1.StartRow();
                table1.AddCellValue(scatter.Name);

                Mathematics.Statistics.Regression model = scatter.Regression;

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
                    table1.AddCellValue(model.Parameters[1]);
                    table1.AddCellValue(Math.Exp(z));
                    table1.AddCellValue(1.0 - Math.Exp(z));
                }
                table1.EndRow();
            }
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            gearWizard = new WizardGearSet(Data, SpeciesRow);
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
            Cohorts = gearWizard.GearData.GetCohorts(SpeciesRow,
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

                AgeComposition ac = gearWizard.GearData.GetAgeComposition(SpeciesRow);

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
                Scatterplot[] cohortCurves = cohort.GetCatchCurve(true, (int)e.Argument);

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

            foreach (Scatterplot scatter in CatchModels)
            {
                scatter.Properties.ShowTrend = true;
                scatter.Properties.SelectedApproximationType = TrendType.Exponential;
                statChartMortality.AddSeries(scatter);

                ToolStripMenuItem item = new ToolStripMenuItem(scatter.Name);
                item.Click += itemMortality_Click;
                contextMortality.Items.Add(item);
            }

            foreach (Scatterplot scatter in Unused)
            {
                statChartMortality.AddSeries(scatter);
            }

            statChartMortality.Remaster();

            pageChart.SetNavigation(true);
        }

        private void itemMortality_Click(object sender, EventArgs e)
        {
            statChartMortality.OpenRegressionProperties(
                (Scatterplot)statChartMortality.GetSample(((ToolStripMenuItem)sender).Name)
                );
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            contextMortality.Show(buttonM, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            Log.Write(EventType.WizardEnded, "Growth Wizard is finished for {0} with {1} models.",
                SpeciesRow.Species, CatchModels.Count);
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
