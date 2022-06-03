using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Fish.Explorer;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Functions;
using System.Drawing;
using Functor = Mayfly.Mathematics.Charts.Functor;
using Meta.Numerics;
using Meta.Numerics.Statistics;
using System.Linq;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardMSY : Form
    {
        public CardStack Data { get; set; }

        public SpeciesKey.SpeciesRow SpeciesRow;

        public VirtualCohort VirtualCohort;

        public double MaximumSustainableYield { get { return msy.Y; } }

        public event EventHandler Calculated;

        public event EventHandler Returned;

        WizardVirtualPopulation vpaWizard;

        WizardPopulation growthWizard;

        private XY msy;




        public WizardMSY()
        {
            InitializeComponent();

            wizardExplorer.ResetTitle("species");
            labelStart.ResetFormatted("species");

            ColumnRefAge.ValueType = typeof(Age);
            ColumnRefF.ValueType = typeof(double);
            ColumnRefZ.ValueType = typeof(double);
            ColumnRefW.ValueType = typeof(double);

            ColumnAge.ValueType = typeof(Age);
            ColumnF.ValueType = typeof(double);
            ColumnZ.ValueType = typeof(double);
            ColumnN.ValueType = typeof(double);
            ColumnB.ValueType = typeof(double);
            ColumnC.ValueType = typeof(double);
            ColumnY.ValueType = typeof(double);
        }

        public WizardMSY(CardStack data, SpeciesKey.SpeciesRow speciesRow)
            : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.ShortName);
            labelStart.ResetFormatted(SpeciesRow.ShortName);

            buttonVpa.Enabled =
                buttonGrowth.Enabled = true;

            Log.Write(EventType.WizardStarted, "MSY wizard is started for {0}.", 
                speciesRow.Species);
        }




        public void Run()
        {
            pageStart.Suppress = true;
            wizardExplorer.NextPage();
        }

        private void CreateVirtualCohort()
        {
            VirtualCohort = new VirtualCohort(new Cohort(DateTime.Today.Year,
                new AgeComposition("MSY", (Age)0, (Age)(spreadSheetReference.RowCount - 2))));

            VirtualCohort.NaturalMortality = (double)numericUpDownM.Value;

            for (int i = 0; i < spreadSheetReference.RowCount; i++)
            {
                if (spreadSheetReference.Rows[i].IsNewRow) continue;
                double f = 0;
                if (spreadSheetReference[ColumnRefF.Index, i].Value is double @double)
                {
                    f = @double;
                }
                VirtualCohort.F[i] = f;

                double w = 0;
                if (spreadSheetReference[ColumnRefW.Index, i].Value is double double1)
                {
                    w = double1;
                }
                VirtualCohort.Masses[i] = w;
            }

            pageReference.AllowNext = VirtualCohort.Count > 1;
        }

        public Report GetReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Sections.MSYR.Title,
                    SpeciesRow.FullNameReport));

            report.UseTableNumeration = true;

            return report;
        }



        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
        }
        

        private void buttonVpa_Click(object sender, EventArgs e)
        {
            vpaWizard = new WizardVirtualPopulation(Data, SpeciesRow);
            vpaWizard.Returned += vpaWizard_Returned;
            vpaWizard.Calculated += vpaWizard_PopulationBuilt;
            vpaWizard.Replace(this);
            vpaWizard.Run();
        }

        private void vpaWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageReference);
            this.Replace(vpaWizard);
        }

        private void vpaWizard_PopulationBuilt(object sender, EventArgs e)
        {
            this.Replace(vpaWizard);

            VirtualCohort = vpaWizard.Population.GetPseudoCohort();
            VirtualCohort.SetLines(ColumnRefAge);

            for (int i = 0; i < VirtualCohort.Count; i++)
            {
                spreadSheetReference[ColumnRefF.Index, i].Value = VirtualCohort.F[i];
                spreadSheetReference[ColumnRefW.Index, i].Value = VirtualCohort[i].MassSample.Mean;
            }

            AgeGroup recruitment = VirtualCohort.GetInitialState();

            double m = vpaWizard.Population.NaturalMortality;
            numericUpDownM.Value = (decimal)m;

            if (recruitment != null)
            {
                double n = recruitment.Quantity;
                numericUpDownN.Value = (decimal)n / 1000M;
            }
        }
        

        private void buttonGrowth_Click(object sender, EventArgs e)
        {
            growthWizard = new WizardPopulation(Data, SpeciesRow);
            growthWizard.ModelsReturned += growthWizard_Returned;
            growthWizard.ModelsCalculated += growthWizard_ModelConfirmed;
            growthWizard.Replace(this);
            growthWizard.RunModels();
        }

        private void growthWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageReference);
            this.Replace(growthWizard);
        }

        private void growthWizard_ModelConfirmed(object sender, EventArgs e)
        {
            this.Replace(growthWizard);

            for (int i = 0; i < spreadSheetReference.RowCount; i++)
            {
                if (spreadSheetReference.Rows[i].IsNewRow) continue;
                spreadSheetReference[ColumnRefW.Index, i].Value = growthWizard.GetWeight(VirtualCohort[i].Age);
            }
        }


        private void numericUpDownM_ValueChanged(object sender, EventArgs e)
        {
            CreateVirtualCohort();

            for (int i = 0; i < spreadSheetReference.RowCount; i++)
            {
                if (spreadSheetReference.Rows[i].IsNewRow) continue;                
                spreadSheetReference[ColumnRefZ.Index, i].Value = VirtualCohort.Z(i);
            }
        }

        private void spreadSheetReference_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            spreadSheetReference[ColumnRefAge.Index, e.RowIndex].Value = (Age)e.RowIndex;
        }

        private void spreadSheetReference_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!spreadSheetReference.ContainsFocus) return;

            if (e.ColumnIndex == ColumnRefF.Index ||
                e.ColumnIndex == ColumnRefW.Index)
            {
                numericUpDownM_ValueChanged(sender, e);
            }
        }


        private void pageReference_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            //CreateVirtualCohort();
            VirtualCohort.SetLines(ColumnAge);
            numericUpDownN_ValueChanged(sender, e);
        }

        private void numericUpDownN_ValueChanged(object sender, EventArgs e)
        {
            VirtualCohort.Recruitment = 1000 * (double)numericUpDownN.Value;

            plotMSY.Clear();

            Functor predictC = new Functor("Catch", (x) =>
            {
                return VirtualCohort.GetAlternateState(x).TotalQuantity / 1000.0; // In thousands
            });

            Functor predictY = new Functor("Yield", (x) =>
            {
                return VirtualCohort.GetAlternateState(x).TotalMass / 1000000.0; // In tonnes
            });

            predictY.Series.YAxisType = AxisType.Secondary;

            plotMSY.AddSeries(predictC);
            plotMSY.AddSeries(predictY);

            Cohort coh = VirtualCohort.GetAlternateState(plotMSY.AxisXMax);
            plotMSY.AxisYMax = coh.TotalQuantity / 1000.0;

            predictY.AxisX.StripLines.Clear();
            predictY.AxisY.StripLines.Clear();

            msy = VirtualCohort.GetMaximumSustainableYeild();

            if (!double.IsNaN(msy.X))
            {
                predictY.AxisX.AddStripLine(msy.X, string.Format("Maximal allowed change ({0:P0})", msy.X), Mathematics.UserSettings.ColorAccent);
            }

            if (!double.IsNaN(msy.Y))
            {
                plotMSY.AxisY2Max = msy.Y / 1000000.0;
                predictY.AxisY.AddStripLine(msy.Y / 1000000.0, string.Format("MSY ({0:N3})", msy.Y / 1000000.0), Mathematics.UserSettings.ColorAccent);
            }

            plotMSY.DoPlot();

            msy_Changed(sender, e);
        }

        
        private void msy_Changed(object sender, EventArgs e)
        {
            Cohort predicted = VirtualCohort.GetAlternateState((double)numericUpDownX.Value);

            for (int i = 0; i < spreadSheetMSY.Rows.Count; i++)
            {
                spreadSheetMSY.Rows[i].DefaultCellStyle.ForeColor = predicted[i].Quantity > 0 ?
                    spreadSheetMSY.DefaultCellStyle.ForeColor : Constants.InfantColor;

                spreadSheetMSY[ColumnF.Index, i].Value = predicted.F[i];
                spreadSheetMSY[ColumnZ.Index, i].Value = predicted.F[i] + VirtualCohort.NaturalMortality;

                spreadSheetMSY[ColumnN.Index, i].Value = predicted.Survivors[i].Quantity / 1000.0;
                spreadSheetMSY[ColumnB.Index, i].Value = predicted.Survivors[i].Mass / 1000000.0;

                spreadSheetMSY[ColumnC.Index, i].Value = predicted[i].Quantity / 1000.0;
                spreadSheetMSY[ColumnY.Index, i].Value = predicted[i].Mass / 1000000.0;
            }

            textBoxYield.Text = (predicted.TotalMass / 1000000.0).ToString(ColumnY.DefaultCellStyle.Format);

        }

        private void pageResults_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Calculated != null)
            {
                e.Cancel = true;
                Calculated.Invoke(sender, e);
            }
        }

        private void pageReference_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Returned != null)
            {
                Returned.Invoke(sender, e);
            }
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
            Log.Write(EventType.WizardEnded, "MSY wizard is finished for {0} with MSY = {1}.",
                SpeciesRow.Species, MaximumSustainableYield);
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
