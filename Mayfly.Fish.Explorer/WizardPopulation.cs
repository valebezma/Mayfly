using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardStockComposition : Form
    {
        public CardStack Data { get; set; }

        private WizardComposition ageCompositionWizard;

        private WizardGearSet gearWizard { get; set; }



        public Data.SpeciesRow SpeciesRow;

        public SpeciesSwarm Swarm;

        SpeciesComposition SelectivityDisplay;

        public AgeComposition AgeStructure;



        private WizardStockComposition()
        {
            InitializeComponent();

            this.RestoreAllCheckStates();
        }

        public WizardStockComposition(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);
            labelBasic.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            pageStart.SetNavigation(false);

            Log.Write(EventType.WizardStarted, "Stock composition wizard is started for {0}.",
                speciesRow.Species);

            structureCalculator.RunWorkerAsync();
        }



        public Report GetReport()
        {
            Report report = new Report(string.Format(Resources.Reports.Header.SummaryPopulation, SpeciesRow.KeyRecord.FullNameReport));

            Data.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            if (checkBoxAge.Checked)
            {
                ageCompositionWizard.AppendPopulationSectionTo(report);
            }

            if (checkBoxAppT.Checked | checkBoxAppKeys.Checked)
            {
                report.BreakPage(PageBreakOption.Odd);
                report.AddChapterTitle(Resources.Reports.Chapter.Appendices);
            }

            if (checkBoxAppT.Checked)
            {
                ageCompositionWizard.AppendCalculationSectionTo(report);
            }

            if (checkBoxAppKeys.Checked)
            {
                ageCompositionWizard.AddAgeRecoveryRoutines(report);
            }

            report.EndBranded();

            return report;
        }

        public void SetGearSet(WizardGearSet _gearWizard)
        {
            gearWizard = _gearWizard;
        }



        private void structureCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            Swarm = Data.GetSwarm(SpeciesRow);

            try
            {
                AgeStructure = Data.GetAgeCompositionFrame(SpeciesRow);
                AgeStructure.Name = Wild.Resources.Reports.Caption.Age;
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void structureCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pageStart.SetNavigation(true);
            //pageBasic.SetNavigation(AgeStructure != null);

            labelBasic.ResetFormatted(Swarm);

            buttonL.Text = new SampleDisplay(Swarm.LengthSample).ToString("s");
            buttonW.Text = new SampleDisplay(Swarm.MassSample).ToString("s");

            pageAge.Suppress = AgeStructure == null;
            checkBoxAge.Checked = AgeStructure != null;
        }

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
        }

        private void PageBasic_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                if (gearWizard == null)
                {
                    gearWizard = new WizardGearSet(Data, SpeciesRow);
                    gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
                    gearWizard.Returned += gearWizard_Returned;
                }

                gearWizard.Replace(this);
            }
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageBasic);
            this.Replace(gearWizard);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageCpue);
            this.Replace(gearWizard);

            columnSelectivityNpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            columnSelectivityBpue.ResetFormatted(gearWizard.SelectedUnit.Unit);
            labelNpueUnit.ResetFormatted(gearWizard.SelectedUnit.Unit);
            labelBpueUnit.ResetFormatted(gearWizard.SelectedUnit.Unit);

            selectivityCalculator.RunWorkerAsync();
        }

        private void PageCpue_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                gearWizard.Replace(this);
            }
        }

        private void SelectivityCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            SelectivityDisplay = gearWizard.SelectedStacks.GetClassedComposition(SpeciesRow, gearWizard.SelectedSamplerType, gearWizard.SelectedUnit);
            Swarm.Abundance = SelectivityDisplay.TotalAbundance / SelectivityDisplay.Count;
            Swarm.Biomass = SelectivityDisplay.TotalBiomass / SelectivityDisplay.Count;
        }

        private void SelectivityCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            spreadSheetSelectivity.Rows.Clear();

            foreach (SpeciesSwarm species in SelectivityDisplay)
            {
                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.Height = spreadSheetSelectivity.RowTemplate.Height;
                gridRow.CreateCells(spreadSheetSelectivity);

                gridRow.Cells[columnSelectivityClass.Index].Value = species;
                gridRow.Cells[columnSelectivityL.Index].Value = new SampleDisplay(species.LengthSample);
                gridRow.Cells[columnSelectivityW.Index].Value = new SampleDisplay(species.MassSample);
                gridRow.Cells[columnSelectivityN.Index].Value = species.Quantity;
                gridRow.Cells[columnSelectivityNpue.Index].Value = species.Abundance;
                gridRow.Cells[columnSelectivityB.Index].Value = species.Mass;
                gridRow.Cells[columnSelectivityBpue.Index].Value = species.Biomass;
                gridRow.Cells[columnSelectivitySex.Index].Value = species.Sexes;

                spreadSheetSelectivity.Rows.Add(gridRow);
            }

            textBoxNpue.ResetFormatted(Swarm.Abundance);
            textBoxBpue.ResetFormatted(Swarm.Biomass);
        }

        private void PageCpue_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                checkBoxAge_CheckedChanged(sender, e);

                if (AgeStructure == null) return;

                if (ageCompositionWizard == null)
                {
                    ageCompositionWizard = new WizardComposition(Data, AgeStructure, SpeciesRow, CompositionColumn.MassSample | CompositionColumn.LengthSample);
                    ageCompositionWizard.Returned += AgeCompositionWizard_Returned;
                    ageCompositionWizard.Finished += ageCompositionWizard_Finished;
                }

                ageCompositionWizard.Replace(this);
                ageCompositionWizard.Run(gearWizard);
            }
        }

        private void AgeCompositionWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageCpue);
            this.Replace(ageCompositionWizard);
        }

        private void ageCompositionWizard_Finished(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageAge);
            this.Replace(ageCompositionWizard);

            checkBoxAge_CheckedChanged(sender, e);

            plotT.Series.Clear();

            //Series hist = new Series();
            //hist.ChartType = SeriesChartType.Column;

            //double minx = double.MaxValue;
            //double maxx = 0;
            //double maxy = 0;

            //foreach (AgeGroup ageGroup in ageCompositionWizard.CatchesComposition)
            //{
            //    hist.Points.AddXY(ageGroup.Age.Years + .5, ageGroup.AbundanceFraction);

            //    minx = Math.Min(minx, ageGroup.Age.Years);
            //    maxx = Math.Max(maxx, ageGroup.Age.Years);
            //    maxy = Math.Max(maxy, ageGroup.AbundanceFraction);
            //}

            //plotT.Series.Add(hist);

            Series all = new Series() { ChartType = SeriesChartType.StackedColumn };
            Series juv = new Series() { ChartType = SeriesChartType.StackedColumn };
            Series mal = new Series() { ChartType = SeriesChartType.StackedColumn };
            Series fem = new Series() { ChartType = SeriesChartType.StackedColumn };

            double minx = double.MaxValue;
            double maxx = 0;
            double maxy = 0;

            foreach (AgeGroup ageGroup in ageCompositionWizard.CatchesComposition)
            {
                double total = ageGroup.Quantity;

                //if (total == 0) continue;

                //if (juvN > 0) { juv.Points.AddXY(ageGroup.Age.Years + .5, juvN); }
                //if (malN > 0) { mal.Points.AddXY(ageGroup.Age.Years + .5, malN); }
                //if (femN > 0) { fem.Points.AddXY(ageGroup.Age.Years + .5, femN); }
                //all.Points.AddXY(ageGroup.Age.Years + .5, total - juvN - malN - femN);

                minx = Math.Min(minx, ageGroup.Age.Years);
                maxx = Math.Max(maxx, ageGroup.Age.Years);
                maxy = Math.Max(maxy, total);
            }

            plotT.Series.Add(all);
            plotT.Series.Add(juv);
            plotT.Series.Add(mal);
            plotT.Series.Add(fem);

            plotT.AxisXMin = minx;
            plotT.AxisXMax = maxx;
            plotT.AxisYMin = 0.0;
            plotT.AxisYMax = maxy;

            plotT.Remaster();
        }

        private void pageAge_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Visible)
            {
                ageCompositionWizard.Replace(this);
            }
        }

        private void PageReport_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            //if (AgeStructure != null)
            //{
            //if (this.Visible) ageCompositionWizard.Replace(this);
            //}
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void checkBoxAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppT.Enabled = checkBoxAge.Checked && (gearWizard == null || gearWizard.IsMultipleClasses);
            checkBoxAppKeys.Enabled = checkBoxAge.Checked && (gearWizard == null || gearWizard.IsMultipleClasses);
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetReport();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();

            pageReport.SetNavigation(true);

            Log.Write(EventType.WizardEnded, "Stock composition wizard is finished for {0}.", SpeciesRow.Species);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
