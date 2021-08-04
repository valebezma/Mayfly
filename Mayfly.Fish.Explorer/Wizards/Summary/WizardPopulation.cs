using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer.Survey;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace Mayfly.Fish.Explorer.Fishery
{
    public partial class WizardStockComposition : Form
    {
        public CardStack Data { get; set; }

        private WizardComposition ageCompositionWizard;

        private WizardGearSet gearWizard { get; set; }



        public Data.SpeciesRow SpeciesRow;

        public SpeciesSwarm Swarm;

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
            Report report = new Report(string.Format(Resources.Reports.Title.RepPopulation, SpeciesRow.KeyRecord.FullNameReport));
            
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
                report.AddChapterTitle(Resources.Reports.Title.Appendices);
            }

            if (checkBoxAppT.Checked)
            {
                ageCompositionWizard.AppendCalculationSectionTo(report, string.Format(Resources.Reports.CatchComposition.AppendixHeader1, "age", SpeciesRow.KeyRecord.FullNameReport));
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
            pageAge.Suppress = AgeStructure == null;
            checkBoxAge.Checked = AgeStructure != null;
        } 

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
        }

        private void PageBasic_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (AgeStructure != null)
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
            wizardExplorer.EnsureSelected(pageStart);
            this.Replace(gearWizard);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
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

        private void AgeCompositionWizard_Returned(object sender, EventArgs e)
        {
            gearWizard.Replace(ageCompositionWizard);
        }

        private void ageCompositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(ageCompositionWizard);
            wizardExplorer.EnsureSelected(pageAge);

            checkBoxAge_CheckedChanged(sender, e);

            plotT.Series.Clear();

            Series hist = new Series();
            hist.ChartType = SeriesChartType.Column;

            double minx = double.MaxValue;
            double maxx = 0;
            double maxy = 0;

            foreach (AgeGroup ageGroup in ageCompositionWizard.CatchesComposition)
            {
                hist.Points.AddXY(ageGroup.Age.Years + .5, ageGroup.AbundanceFraction);

                minx = Math.Min(minx, ageGroup.Age.Years);
                maxx = Math.Max(maxx, ageGroup.Age.Years);
                maxy = Math.Max(maxy, ageGroup.AbundanceFraction);
            }

            plotT.Series.Add(hist);

            plotT.AxisXMin = minx;
            plotT.AxisXMax = maxx;
            plotT.AxisYMin = 0.0;
            plotT.AxisYMax = maxy;

            plotT.Remaster();
        }

        private void pageAge_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) ageCompositionWizard.Replace(this);
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
            e.Cancel = true;
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
