using AeroWizard;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Mayfly.Mathematics.Charts;
using Mayfly.Wild;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Mayfly.Fish.Explorer.Fishery
{
    public partial class WizardGrowth : Form
    {
        public CardStack Data { get; set; }

        public Data.SpeciesRow SpeciesRow;

        public AgeComposition PseudoCohort;

        public event EventHandler Returned;

        public event EventHandler Calculated;

        public Scatterplot GrowthModel { get; private set; }
        Scatterplot growthInternal;
        Scatterplot growthExternal;

        public Scatterplot WeightModel { get; private set; }
        Scatterplot weightInternal;
        Scatterplot weightExternal;
        
        Scatterplot weightGrowthInternal;
        Scatterplot weightGrowthExternal;



        private WizardGrowth()
        { 
            InitializeComponent();

            columnCrossAge.ValueType = typeof(Age);
            columnCrossN.ValueType = typeof(int);
            columnCrossLength.ValueType =
            columnCrossMass.ValueType = typeof(SampleDisplay);

            this.RestoreAllCheckStates();
        }

        public WizardGrowth(CardStack data, Data.SpeciesRow speciesRow)
            : this()
        {
            Data = data;
            SpeciesRow = speciesRow;

            wizardExplorer.ResetTitle(speciesRow.GetFullName());

            Log.Write(EventType.WizardStarted, "Growth wizard is started for {0}.", 
                speciesRow.Species);
        }



        public void SetStart()
        {
            wizardExplorer.EnsureSelected(pageCrossSection);
        }

        public double GetWeight(Age age)
        {
            if (GrowthModel == null)
                throw new ArgumentNullException();

            if (!GrowthModel.IsRegressionOK)
                throw new ArgumentNullException();

            if (WeightModel == null)
                throw new ArgumentNullException();

            if (!WeightModel.IsRegressionOK)
                throw new ArgumentNullException();

            double l = GrowthModel.Regression.Predict(age.Years + .5);
            double w = WeightModel.Regression.Predict(l);
            return w;
        }

        public void Run()
        {
            pageStart.Suppress = true;
            wizardExplorer.NextPage();
        }

        public Report GetReport()
        {
            Report report = new Report(string.Format(
                Resources.Reports.Growth.Title,
                SpeciesRow.ToHTML()));
            Data.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            if (checkBoxData.Checked)
            {
                AddData(report);
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

        public void AddData(Report report)
        {
            report.AddParagraph(Resources.Reports.Growth.Paragraph1,
                SpeciesRow.ToHTML(), report.NextTableNumber);

            Report.Table table = PseudoCohort.GetTable(string.Format(Resources.Reports.Growth.Table1,
                SpeciesRow.ToHTML()), CompositionColumn.SampleSize | CompositionColumn.LengthSample | CompositionColumn.MassSample, PseudoCohort.Name);

            report.AddTable(table);
        }

        public void AddGrowth(Report report)
        {
            if (GrowthModel.IsRegressionOK) {
                report.AddParagraph(Resources.Reports.Growth.Paragraph2, GrowthModel.Regression);
                report.AddEquation(GrowthModel.Regression.GetEquation("L", "t"));
            }
        }

        public void AddMass(Report report)
        {
            report.AddParagraph(Resources.Reports.Growth.Paragraph3, WeightModel.Regression);
            report.AddEquation(WeightModel.Regression.GetEquation("W", "L"));
        }



        private void categoryCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            PseudoCohort = Data.GetSampleAgeComposition(SpeciesRow);
            PseudoCohort.Name = string.Format("{0} stock cross section", SpeciesRow.GetFullName());            
        }

        private void categoryCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelNoData.Visible = (PseudoCohort == null);

            if (PseudoCohort == null)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                foreach (AgeGroup ageGroup in PseudoCohort)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.Height = spreadSheetCross.RowTemplate.Height;
                    gridRow.CreateCells(spreadSheetCross);

                    gridRow.Cells[columnCrossAge.Index].Value = ageGroup.Name;

                    if (ageGroup.LengthSample.Count > 0)
                        gridRow.Cells[columnCrossLength.Index].Value = new SampleDisplay(ageGroup.LengthSample);

                    if (ageGroup.MassSample.Count > 0)
                        gridRow.Cells[columnCrossMass.Index].Value = new SampleDisplay(ageGroup.MassSample);

                    if (ageGroup.Quantity > 0)
                        gridRow.Cells[columnCrossN.Index].Value = ageGroup.Quantity;

                    gridRow.DefaultCellStyle.ForeColor =
                        ageGroup.Quantity > 0 ? spreadSheetCross.ForeColor : Constants.InfantColor;

                    spreadSheetCross.Rows.Add(gridRow);
                }
            }
            
            pageCrossSection.SetNavigation(true);
        }





        private void modelCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            growthInternal = Data.Parent.GrowthModels.GetInternalScatterplot(SpeciesRow.Species);
            growthExternal = Data.Parent.GrowthModels.GetExternalScatterplot(SpeciesRow.Species);
            GrowthModel = Data.Parent.GrowthModels.GetCombinedScatterplot(SpeciesRow.Species);

            weightInternal = Data.Parent.MassModels.GetInternalScatterplot(SpeciesRow.Species);
            weightExternal = Data.Parent.MassModels.GetExternalScatterplot(SpeciesRow.Species);
            WeightModel = Data.Parent.MassModels.GetCombinedScatterplot(SpeciesRow.Species);

            Data.Parent.MassGrowthModels.Refresh(SpeciesRow.Species);
            weightGrowthInternal = Data.Parent.MassGrowthModels.GetInternalScatterplot(SpeciesRow.Species);
            weightGrowthExternal = Data.Parent.MassGrowthModels.GetExternalScatterplot(SpeciesRow.Species);
        }

        private void modelCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Age to Length

            statChartAL.Visible = GrowthModel != null;
            buttonAL.Enabled = checkBoxGrowth.Enabled = GrowthModel != null && GrowthModel.IsRegressionOK;

            statChartAL.Clear();

            if (growthExternal != null)
            {
                growthExternal = growthExternal.Copy();
                growthExternal.Series.Name =
                    growthExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SpeciesRow.Species);
                growthExternal.Properties.DataPointColor = Constants.InfantColor;
                statChartAL.AddSeries(growthExternal);
            }

            if (growthInternal != null)
            {
                growthInternal = growthInternal.Copy();
                growthInternal.Series.Name =
                    growthInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SpeciesRow.Species);
                growthInternal.Properties.DataPointColor = UserSettings.ModelColor;
                statChartAL.AddSeries(growthInternal);
            }

            if (GrowthModel != null)
            {
                GrowthModel = GrowthModel.Copy();
                GrowthModel.Properties.ShowTrend = true;
                GrowthModel.Properties.SelectedApproximationType = Data.Parent.GrowthModels.Nature;
                GrowthModel.Properties.DataPointColor = System.Drawing.Color.Transparent;
                GrowthModel.Properties.TrendColor = UserSettings.ModelColor.Darker();
                statChartAL.AddSeries(GrowthModel);
            }

            //statChartAL.Refresh();
            statChartAL.Update(this, new EventArgs());
            if (statChartAL.Scatterplots.Count > 0) statChartAL.Rebuild(this, new EventArgs());

            #endregion

            #region Length to Mass

            statChartLW.Visible = WeightModel != null;
            buttonLW.Enabled = checkBoxMass.Enabled = WeightModel != null && WeightModel.IsRegressionOK;

            statChartLW.Clear();

            if (weightExternal != null)
            {
                weightExternal = weightExternal.Copy();
                weightExternal.Series.Name =
                    weightExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SpeciesRow.Species);
                weightExternal.Properties.DataPointColor = Constants.InfantColor;
                statChartLW.AddSeries(weightExternal);
            }

            if (weightInternal != null)
            {
                weightInternal = weightInternal.Copy();
                weightInternal.Series.Name =
                    weightInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SpeciesRow.Species);
                weightInternal.Properties.DataPointColor = UserSettings.ModelColor;
                statChartLW.AddSeries(weightInternal);
            }

            if (WeightModel != null)
            {
                WeightModel = WeightModel.Copy();
                WeightModel.Properties.ShowTrend = true;
                WeightModel.Properties.SelectedApproximationType = Data.Parent.MassModels.Nature;
                WeightModel.Properties.DataPointColor = System.Drawing.Color.Transparent;
                WeightModel.Properties.TrendColor = UserSettings.ModelColor.Darker();
                statChartLW.AddSeries(WeightModel);
            }

            //statChartLW.Refresh();
            statChartLW.Update(this, new EventArgs());
            if (statChartLW.Scatterplots.Count > 0) statChartLW.Rebuild(this, new EventArgs());

            pageAL.SetNavigation(true);

            #endregion

            #region Age to Mass

            plotAW.Visible = (GrowthModel != null && WeightModel != null);

            plotAW.Clear();

            if (weightGrowthExternal != null)
            {
                weightGrowthExternal.Series.Name =
                    weightGrowthExternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.SpecSheet, SpeciesRow.Species);
                weightGrowthExternal.Properties.DataPointColor = Constants.InfantColor;
                plotAW.AddSeries(weightGrowthExternal);
            }

            if (weightGrowthInternal != null)
            {
                weightGrowthInternal = weightGrowthInternal.Copy();
                weightGrowthInternal.Series.Name =
                    weightGrowthInternal.Properties.ScatterplotName =
                    string.Format(Resources.Interface.Interface.IntModel, SpeciesRow.Species);
                weightGrowthInternal.Properties.DataPointColor = UserSettings.ModelColor;
                plotAW.AddSeries(weightGrowthInternal);
            }

            if (GrowthModel != null && GrowthModel.IsRegressionOK &&
                WeightModel != null && WeightModel.IsRegressionOK)
            {
                string name = string.Format(Resources.Interface.Interface.MassGrowth, SpeciesRow.Species);

                Functor weightGrowth = new Functor(name,
                    (t) =>
                    {
                        double l = GrowthModel.Regression.Predict(t);
                        return WeightModel.Regression.Predict(l);
                    },
                    (w) =>
                    {
                        double l = WeightModel.Regression.PredictInversed(w);
                        return GrowthModel.Regression.PredictInversed(l);
                    }
                        );

                weightGrowth.Properties.TrendColor = UserSettings.ModelColor.Darker();
                plotAW.AddSeries(weightGrowth);
            }

            //plotAW.Refresh();
            plotAW.Update(this, new EventArgs());
            if (plotAW.Scatterplots.Count > 0) plotAW.Rebuild(this, new EventArgs());

            pageAW.SetNavigation(true);

            #endregion
        }






        private void pageStart_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            spreadSheetCross.Rows.Clear();
            categoryCalculator.RunWorkerAsync();
        }

        private void pageCrossSection_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Returned != null)
            {
                Returned.Invoke(sender, e);
            }
        }

        private void pageCrossSection_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            pageAL.SetNavigation(false);
            statChartAL.Clear();
            statChartLW.Clear();
            buttonAL.Enabled = buttonLW.Enabled = false;
            if (!modelCalculator.IsBusy) modelCalculator.RunWorkerAsync();
        }

        private void buttonAL_Click(object sender, EventArgs e)
        {
            statChartAL.OpenRegressionProperties(GrowthModel);
        }

        private void buttonLW_Click(object sender, EventArgs e)
        {
            statChartLW.OpenRegressionProperties(WeightModel);
        }

        private void pageAW_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (Calculated != null)
            {
                Calculated.Invoke(sender, e);
                e.Cancel = true;
            }
        }



        private void checkBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Enabled)
            {
                ((CheckBox)sender).Checked = false;
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
            Log.Write(EventType.WizardEnded, "Growth wizard is finished for {0} with equation {1}.",
                SpeciesRow.Species, GrowthModel.Regression.Equation);
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
