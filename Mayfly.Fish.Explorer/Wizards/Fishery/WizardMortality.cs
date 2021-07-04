using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer.Survey;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Mayfly.Fish.Explorer.Fishery
{
    public partial class WizardMortality : Form
    {
        public CardStack Data { get; set; }

        private WizardCatchesComposition ageCompositionWizard;

        private WizardGearSet gearWizard
        {
            get
            {
                return ageCompositionWizard.gearWizard;
            }
        }

        public Data.SpeciesRow SpeciesRow;

        public event EventHandler Calculated;

        public Scatterplot unused;

        public Scatterplot CatchCurve { get; private set; }

        public AgeComposition AgeStructure;

        public AgeGroup YoungestCaught
        {
            get
            {
                return (AgeGroup)comboBoxAge.SelectedItem;
            }
        }

        public double Z { get; set; }

        double Fi { get; set; }

        double S { get; set; }



        private WizardMortality()
        {
            InitializeComponent();
        }

        public WizardMortality(CardStack data, Data.SpeciesRow speciesRow) : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.GetFullName());
            labelStart.ResetFormatted(SpeciesRow.GetFullName());

            pageStart.SetNavigation(false);

            Log.Write(EventType.WizardStarted, "Mortality wizard is started for {0}.",
                speciesRow.Species);

            categoryCalculator.RunWorkerAsync();
        }



        public void SetStart()
        {
            wizardExplorer.EnsureSelected(pageStart);
        }



        public Report GetReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Mortality.Title,
                    SpeciesRow.ToHTML()));
            gearWizard.SelectedData.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxGears.Checked)
            {
                gearWizard.AddEffortDescription(report);
            }

            if (checkBoxAge.Checked)
            {
                ageCompositionWizard.AddCatchesDescription(report);
            }

            if (checkBoxMortality.Checked)
            {
                AddMortality(report);
            }

            report.EndBranded();

            return report;
        }

        public void AddMortality(Report report)
        {
            report.AddParagraph(Resources.Reports.Mortality.Paragraph1, CatchCurve.Regression, 
                (Age)CatchCurve.Left, (Age)CatchCurve.Right,
                SpeciesRow.ToHTML());
            report.AddEquation(CatchCurve.Regression.GetEquation("CPUE(%)", "t"));

            report.AddParagraph(Resources.Reports.Mortality.Paragraph2, Z);
            report.AddEquation(@"S = e^{-" + Z.ToString("N5") + "} = " + S.ToString("N5"));
            report.AddEquation(@"φ = 1 - " + S.ToString("N5") + " = " + Fi.ToString("N5"));
        }

        

        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            ageCompositionWizard = new WizardCatchesComposition(Data, SpeciesRow, AgeStructure);
            ageCompositionWizard.Finished += compositionWizard_Finished;
            ageCompositionWizard.Replace(this);
            ageCompositionWizard.Run();
        }
        

        private void compositionWizard_Finished(object sender, EventArgs e)
        {
            this.Replace(ageCompositionWizard);

            comboBoxAge.DataSource = ageCompositionWizard.CatchesComposition;
            comboBoxAge.SelectedItem = ageCompositionWizard.CatchesComposition.MostAbundant;
        }

        private void categoryCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            AgeStructure = Data.GetSampleAgeComposition(SpeciesRow);
        }

        private void categoryCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelNoData.Visible = AgeStructure == null;

            if (AgeStructure == null)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                pageStart.SetNavigation(true);
            }
        }

        private void comboBoxAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            Scatterplot[] res = ageCompositionWizard.CatchesComposition.GetCatchCurve(comboBoxAge.SelectedIndex);

            unused = res[0];
            CatchCurve = res[1];

            statChartMortality.Clear();

            if (unused != null)
            {
                //unused.Properties.DataPointColor = Color.LightCoral;
                statChartMortality.AddSeries(unused);
            }

            if (CatchCurve != null)
            {
                //Model.Properties.DataPointColor = Color.DarkRed;
                CatchCurve.Properties.ShowTrend = true;
                CatchCurve.Properties.SelectedApproximationType = TrendType.Exponential;
                statChartMortality.AddSeries(CatchCurve);
            }

            statChartMortality.Visible = unused != null && CatchCurve != null;

            statChartMortality.ShowLegend = false;
            statChartMortality.Remaster();

            checkBoxAge_CheckedChanged(sender, e);

        }

        private void buttonAL_Click(object sender, EventArgs e)
        {
            statChartMortality.OpenRegressionProperties(CatchCurve);
        }

        private void pageChart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (CatchCurve.IsRegressionOK)
            {
                Z = -CatchCurve.Regression.Parameter(1);
                S = Math.Exp(-Z);
                Fi = 1 - S;

                textBoxZ.Text = Z.ToString("N4");
                textBoxFi.Text = Fi.ToString("N4");
                textBoxS.Text = S.ToString("N4");
            } else {
                textBoxZ.Text = 
                    textBoxFi.Text = 
                    textBoxS.Text = Constants.Null;
            }

            if (Calculated != null) {
                Calculated.Invoke(sender, e);
            }
        }

        private void pageChart_Rollback(object sender, WizardPageConfirmEventArgs e)
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

        private void checkBoxAge_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxAppT.Enabled = checkBoxAge.Checked && gearWizard.IsMultipleClasses;
        }

        private void pageReport_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageReport.SetNavigation(false);
            reporter.RunWorkerAsync();
            e.Cancel = true;
        }

        private void reporter_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Report> result = new List<Report>();

            result.Add(GetReport());

            if (checkBoxAppT.Checked)
            {
                ageCompositionWizard.GetCatchesRoutines();
            }

            e.Result = result.ToArray();
        }

        private void reporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Report rep in (Report[])e.Result)
            {
                rep.Run();
            }
            pageReport.SetNavigation(true);
            Log.Write(EventType.WizardEnded, "Motrality wizard is finished for {0} with Z = {1:N4}, S = {2:N4} and Fi = {3:N4}.",
                SpeciesRow.Species, Z, S, Fi);
            if (!UserSettings.KeepWizard) Close();
        }


        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            if (Calculated != null)
            {
                Calculated.Invoke(sender, e);
            }
            else
            {
                Close();
            }
        }
    }
}
