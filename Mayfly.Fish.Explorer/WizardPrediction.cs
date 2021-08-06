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
using Mayfly.Fish.Explorer;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardPrediction : Form
    {
        public CardStack Data { get; set; }

        public Data.SpeciesRow SpeciesRow;

        WizardMSY msyWizard;

        Regression Regression;

        //Scatterplot yieldFacts;

        Scatterplot yieldPrediction;

        Scatterplot yieldPredictionChanged;




        public WizardPrediction()
        {
            InitializeComponent();

            wizardExplorer.ResetTitle("species");
            labelStart.ResetFormatted("species");
        }

        public WizardPrediction(CardStack data, Data.SpeciesRow speciesRow)
            : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);
            labelStart.ResetFormatted(SpeciesRow.KeyRecord.ShortName);

            //Logger.Log(String.Format("Prediction wizard is started for {0}.", speciesRow.Species));
        }



        public Report GetReport()
        {
            Report report = new Report(
                    string.Format(Resources.Reports.Sections.MSYR.Title,
                    SpeciesRow.KeyRecord.FullNameReport))
            {
                UseTableNumeration = true
            };

            report.EndBranded();

            return report;
        }




        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            msyWizard = (SpeciesRow == null ? new WizardMSY() : new WizardMSY(Data, SpeciesRow));
            msyWizard.Returned += msyWizard_Returned;
            msyWizard.Calculated += msyWizard_Calculated;
            msyWizard.Replace(this);
            msyWizard.Run();
        }

        void msyWizard_Returned(object sender, EventArgs e)
        {
            wizardExplorer.EnsureSelected(pageStart);
            this.Replace(msyWizard);
        }

        void msyWizard_Calculated(object sender, EventArgs e)
        {
            this.Replace(msyWizard);

            Regression = new Regression(msyWizard.VirtualCohort);

            labelChangesManage.ResetFormatted(Regression.ModelYear + 1);

            #region Prediction plot

            plotPrediction.Clear();

            //plotPrediction.AxisXMin = new DateTime(vpaWizard.AnnualStacks[0].GetYears()[0], 1, 1).ToOADate();
            plotPrediction.AxisXMin = new DateTime(Regression.ModelYear, 1, 1).ToOADate();
            plotPrediction.AxisXMax = new DateTime(Regression.ModelYear + 5, 12, 31).ToOADate();

            BivariateSample yield = new BivariateSample();

            //foreach (CardStack annualStack in vpaWizard.AnnualStacks)
            //{
            //    double d = Service.GetCatchDate(int.Parse(annualStack.Name)).ToOADate();
            //    double m = annualStack.Mass(SpeciesRow) / 1000;

            //    yield.Add(d, m);
            //}

            //yieldFacts = new Scatterplot(yield, "Actual yields");
            //yieldFacts.Series.ChartType = SeriesChartType.Line;
            //yieldFacts.IsChronic = true;

            //plotPrediction.AddSeries(yieldFacts);

            yieldPrediction = new Scatterplot(yield, "Predicted yields");
            yieldPrediction.Series.ChartType = SeriesChartType.Line;
            yieldPrediction.IsChronic = true;

            plotPrediction.AddSeries(yieldPrediction);

            yieldPredictionChanged = new Scatterplot(yield, "0000");
            yieldPredictionChanged.Series.ChartType = SeriesChartType.Line;
            yieldPredictionChanged.IsChronic = true;

            plotPrediction.AddSeries(yieldPredictionChanged);

            plotPrediction.Remaster();

            #endregion

            //numericUpDownX.Value = 1.5M; // (decimal)msyWizard.MaximumSustainableYield;
            numericUpDownX_ValueChanged(sender, e);
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            if (Regression != null)
            {
                double x = (double)numericUpDownX.Value;

                yieldPrediction.Data = Regression.Predict(
                    DateTime.FromOADate(plotPrediction.AxisXMax));
                yieldPredictionChanged.Data = Regression.Predict(
                    DateTime.FromOADate(plotPrediction.AxisXMax), x);
                yieldPredictionChanged.Properties.ScatterplotName =
                    string.Format("Predicted yields (with {0:P1} change)", (x - 1.0));

                yieldPrediction.BuildSeries();
                yieldPredictionChanged.BuildSeries();

                plotPrediction.RecalculateAxesProperties();
            }
        }

        private void pagePrediction_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (this.Visible) msyWizard.Replace(this);
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
            Log.Write("Prediction wizard is finished. Species: {0}, {1}.", SpeciesRow.Species, "");
            if (!UserSettings.KeepWizard) Close();
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}
