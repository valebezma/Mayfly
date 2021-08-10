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

namespace Mayfly.Fish.Explorer
{
    public partial class WizardGrowth : Form
    {
        public CardStack Data { get; set; }

        public Data.SpeciesRow SpeciesRow;

        public AgeComposition AgeStructure;

        public event EventHandler Returned;

        public event EventHandler Calculated;



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

            wizardExplorer.ResetTitle(speciesRow.KeyRecord.ShortName);

            Log.Write(EventType.WizardStarted, "Growth wizard is started for {0}.", 
                speciesRow.Species);
        }



        public void SetStart()
        {
            wizardExplorer.EnsureSelected(pageCrossSection);
        }

        //public double GetWeight(Age age)
        //{
        //    //if (GrowthModel == null)
        //    //    throw new ArgumentNullException();

        //    //if (!GrowthModel.IsRegressionOK)
        //    //    throw new ArgumentNullException();

        //    //if (WeightModel == null)
        //    //    throw new ArgumentNullException();

        //    //if (!WeightModel.IsRegressionOK)
        //    //    throw new ArgumentNullException();

        //    //double l = GrowthModel.Regression.Predict(age.Years + .5);
        //    //double w = WeightModel.Regression.Predict(l);
        //    //return w;
        //}

        public void Run()
        {
            pageStart.Suppress = true;
            wizardExplorer.NextPage();
        }

        public Report GetReport()
        {
            Report report = new Report(string.Format(
                Resources.Reports.Sections.Growth.Title,
                SpeciesRow.KeyRecord.FullNameReport));
            Data.AddCommon(report, SpeciesRow);

            report.UseTableNumeration = true;

            //if (checkBoxData.Checked)
            //{
            //    AddData(report);
            //}

            //if (checkBoxGrowth.Checked)
            //{
            //    AddGrowth(report);
            //}

            //if (checkBoxMass.Checked)
            //{
            //    AddMass(report);
            //}

            report.EndBranded();

            return report;
        }



        private void categoryCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            AgeStructure = Data.GetSampleAgeComposition(SpeciesRow);
            AgeStructure.Name = string.Format("{0} stock cross section", SpeciesRow.KeyRecord.ShortName);            
        }

        private void categoryCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelNoData.Visible = (AgeStructure == null);

            if (AgeStructure == null)
            {
                this.Cursor = Cursors.Default;
            }
            else
            {
                foreach (AgeGroup ageGroup in AgeStructure)
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
        }

        private void modelCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region Age to Length

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
            if (!modelCalculator.IsBusy) modelCalculator.RunWorkerAsync();
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
            //pageReport.SetNavigation(true);
            //((Report)e.Result).Run();
            //Log.Write(EventType.WizardEnded, "Growth wizard is finished for {0} with equation {1}.",
            //    SpeciesRow.Species, GrowthModel.Regression.Equation);
            //if (!UserSettings.KeepWizard) Close();
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
