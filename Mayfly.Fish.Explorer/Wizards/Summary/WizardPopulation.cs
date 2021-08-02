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

        private WizardComposition lengthCompositionWizard;

        private WizardComposition ageCompositionWizard;

        private WizardGearSet gearWizard { get; set; }

        public Data.SpeciesRow SpeciesRow;

        public LengthComposition LengthStructure;

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

            wizardExplorer.ResetTitle(speciesRow.GetFullName());
            labelStart.ResetFormatted(SpeciesRow.GetFullName());

            pageStart.SetNavigation(false);

            Log.Write(EventType.WizardStarted, "Stock composition wizard is started for {0}.", 
                speciesRow.Species);

            structureCalculator.RunWorkerAsync();
        }



        public Report GetReport() 
        {
            Report report = new Report(string.Format(Resources.Reports.Title.RepPopulation, SpeciesRow.ToHTML()));
            
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortSection(report);
            }

            if (checkBoxLength.Checked)
            {
                lengthCompositionWizard.AppendPopulationSectionTo(report);
            }

            if (checkBoxAge.Checked)
            {
                ageCompositionWizard.AppendPopulationSectionTo(report);
            }

            if (checkBoxAppL.Checked | checkBoxAppT.Checked | checkBoxAppKeys.Checked)
            {
                report.AddChapterTitle(Resources.Reports.Title.Appendices);
            }

            if (checkBoxAppL.Checked)
            {
                lengthCompositionWizard.AddCatchesRoutines(report);
            }

            if (checkBoxAppT.Checked)
            {
                ageCompositionWizard.AddCatchesRoutines(report);
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
            LengthStructure = Data.GetLengthCompositionFrame(SpeciesRow, UserSettings.SizeInterval);
            LengthStructure.Name = Fish.Resources.Common.SizeUnits;

            try
            {
                AgeStructure = Data.GetAgeCompositionFrame(SpeciesRow);
                AgeStructure.Name = Wild.Resources.Reports.Caption.Age;
            }
            catch
            { }
        }

        private void structureCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            plotL.AxisXInterval = LengthStructure.Interval;

            pageAge.Suppress = AgeStructure == null;
            checkBoxAge.Checked = AgeStructure != null;
            pageStart.SetNavigation(true);
        } 

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (gearWizard == null)
            {
                gearWizard = new WizardGearSet(Data, SpeciesRow);
                gearWizard.AfterDataSelected += gearWizard_AfterDataSelected;
                gearWizard.Returned += gearWizard_Returned;
            }

            gearWizard.Replace(this);
        }

        private void gearWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageStart);
            this.Replace(gearWizard);
        }

        private void gearWizard_AfterDataSelected(object sender, EventArgs e)
        {
            checkBoxAge_CheckedChanged(sender, e);
            checkBoxLength_CheckedChanged(sender, e);

            if (lengthCompositionWizard == null)
            {
                lengthCompositionWizard = new WizardComposition(Data, LengthStructure, SpeciesRow, CompositionColumn.MassSample);
                lengthCompositionWizard.Finished += lengthCompositionWizard_Finished;
                lengthCompositionWizard.Returned += LengthCompositionWizard_Returned;
            }

            lengthCompositionWizard.Replace(gearWizard);

            //this.Replace(gearWizard);
            //lengthCompositionWizard.Replace(this);

            lengthCompositionWizard.Run(gearWizard);
        }

        private void LengthCompositionWizard_Returned(object sender, EventArgs e)
        {
            //this.Replace(lengthCompositionWizard);
            //gearWizard.Replace(this);

            gearWizard.Replace(lengthCompositionWizard);
        }

        private void lengthCompositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(lengthCompositionWizard);
            wizardExplorer.EnsureSelected(pageLength);

            checkBoxLength_CheckedChanged(sender, e);

            plotL.Series.Clear();

            Series hist = new Series();
            hist.ChartType = SeriesChartType.Column;

            double minx = double.MaxValue;
            double maxx = 0;
            double maxy = 0;

            foreach (SizeClass sizeClass in lengthCompositionWizard.CatchesComposition)
            {
                hist.Points.AddXY(sizeClass.Size.Midpoint, sizeClass.AbundanceFraction);

                minx = Math.Min(minx, sizeClass.Size.LeftEndpoint);
                maxx = Math.Max(maxx, sizeClass.Size.RightEndpoint);
                maxy = Math.Max(maxy, sizeClass.AbundanceFraction);
            }

            plotL.Series.Add(hist);

            plotL.AxisXMin = minx;
            plotL.AxisXMax = maxx;
            plotL.AxisYMin = 0.0;
            plotL.AxisYMax = maxy;

            plotL.Remaster();
        }

        private void pageLength_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible)
            {
                e.Cancel = true;
                lengthCompositionWizard.Replace(this);
            }
        }

        private void pageLength_Commit(object sender, WizardPageConfirmEventArgs e)
        {
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
            wizardExplorer.EnsureSelected(pageLength);
            this.Replace(ageCompositionWizard);
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

        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        private void checkBoxLength_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppL.Enabled = checkBoxLength.Checked && (gearWizard == null ? true : gearWizard.IsMultipleClasses);
        }

        private void checkBoxAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppT.Enabled = checkBoxAge.Checked && (gearWizard == null ? true : gearWizard.IsMultipleClasses);
            checkBoxAppKeys.Enabled = checkBoxAge.Checked && (gearWizard == null ? true : gearWizard.IsMultipleClasses);
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
            
            Log.Write(EventType.WizardEnded, "Stock composition wizard is finished for {0}, dominants are {1}, so {2} are too.",
                SpeciesRow.Species, 
                lengthCompositionWizard != null ? lengthCompositionWizard.CatchesComposition.GetDominants().Merge() : Constants.Null,
                ageCompositionWizard != null ? ageCompositionWizard.CatchesComposition.GetDominants().Merge() : Constants.Null);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
