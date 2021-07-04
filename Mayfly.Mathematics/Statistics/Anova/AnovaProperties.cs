using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Mayfly.Mathematics.Charts;
using System.Data;

namespace Mayfly.Mathematics.Statistics
{
    public partial class AnovaProperties : Form
    {
        public string FactorName
        {
            get { return textBoxFactor.Text; }
            set { textBoxFactor.Text = value; }
        }

        public double Alpha
        {
            set;
            get;
        }

        //Evaluation evaluation;

        OneWayAnovaResult Result;
        

        
        public AnovaProperties(string variable, string factor, List<Sample> samples)
        {
            InitializeComponent();

            FactorName = factor;
            columnP.DefaultCellStyle.NullValue = null;
            Alpha = UserSettings.DefaultAlpha;
            Result = Sample.OneWayAnovaTest(samples);

            UpdateValues();

            //evaluation = new Evaluation(FactorName);
            //evaluation.AddRange(samples);
        }

        public AnovaProperties(DataColumn factorColumn, DataColumn dataColumn)
            : this(dataColumn.Caption, factorColumn.Caption, dataColumn.GetSamples(factorColumn))
        { }


        
        public void UpdateValues()
        {
            //flowSamples.Controls.Clear();
            //flowSamples.Controls.Add(Anova.AllValues.GetButton());

            //foreach (Sample sample in Anova.AllVariants)
            //{
            //    Button button = sample.GetButton();
            //    button.Enabled = sample.Count >= UserSettings.StrongSampleSize && sample.StandardDeviation != 0;
            //    flowSamples.Controls.Add(button);
            //}

            //if (Anova.Homoscedasticity == null || 
            //    double.IsNaN(Anova.Homoscedasticity.Statistic) ||
            //    double.IsInfinity(Anova.Homoscedasticity.Statistic))
            //{
            //    pictureBoxCompatibility.Image = Mayfly.Properties.Resources.Warning;
            //    toolTip1.SetToolTip(pictureBoxCompatibility, Resources.Statistics.UnableToCalculate);
            //}
            //else if (Anova.Homoscedasticity.IsPassed())
            //{
            //    pictureBoxCompatibility.Image = Mathematics.Properties.Resources.Check;
            //    toolTip1.SetToolTip(pictureBoxCompatibility,
            //        string.Format(Resources.Statistics.HomoscedasticityPositive,
            //        UserSettings.HomogeneityTestName, Anova.Homoscedasticity.Statistic,
            //        Anova.Homoscedasticity.Probability,
            //        Anova.Homoscedasticity.Probability));
            //}
            //else
            //{
            //    pictureBoxCompatibility.Image = Mathematics.Properties.Resources.None;
            //    toolTip1.SetToolTip(pictureBoxCompatibility,
            //        string.Format(Resources.Statistics.HomoscedasticityNegative,
            //        UserSettings.HomogeneityTestName, Anova.Homoscedasticity.Statistic,
            //        Anova.Homoscedasticity.Probability));
            //}

            GridResults.Rows.Clear();



            foreach (AnovaFactorRow factorRow in Anova.Factors)
            {
                AddRow(factorRow);
            }

            foreach (InteractionRow interactionRow in Anova.Interactions)
            {
                AddRow(interactionRow);
            }

            AddRow(Anova.Residual, Resources.Interface.AnovaResidual, true);
            AddRow(Anova.Total, Resources.Interface.AnovaTotal, false);
        }

        //private DataGridViewRow AddRow(AnovaFactorRow factorRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();
        //    result.CreateCells(GridResults);
        //    result.Cells[columnSource.Index].Value = factorRow.Factor;
        //    GridResults.Rows.Add(result);

        //    result.Cells[columnDF.Index].Value = factorRow.DegreesOfFreedom;
        //    result.Cells[columnSS.Index].Value = factorRow.SumOfSquares;
        //    result.Cells[columnMS.Index].Value = factorRow.Variance;
        //    TestResult testResult = factorRow.Result(Anova.Residual);
        //    result.Cells[columnF.Index].Value = testResult.Statistic;
        //    result.Cells[columnP.Index].Value = testResult.Probability;
        //    ((TextAndImageCell)result.Cells[columnP.Index]).Image = testResult.IsPassed(Alpha) ?
        //        Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;
        //    result.Cells[columnP.Index].Tag = testResult;

        //    return result;
        //}

        //private DataGridViewRow AddRow(InteractionRow interactionRow)
        //{
        //    DataGridViewRow result = new DataGridViewRow();
        //    result.CreateCells(GridResults);
        //    result.Cells[columnSource.Index].Value = interactionRow.Interaction;
        //    GridResults.Rows.Add(result);

        //    result.Cells[columnDF.Index].Value = interactionRow.DegreesOfFreedom;
        //    result.Cells[columnSS.Index].Value = interactionRow.SumOfSquares;
        //    result.Cells[columnMS.Index].Value = interactionRow.Variance;
        //    result.Cells[columnF.Index].Value = interactionRow.Result.Statistic;
        //    result.Cells[columnP.Index].Value = interactionRow.Result.Probability;

        //    ((TextAndImageCell)result.Cells[columnP.Index]).Image = interactionRow.Result.IsPassed(Alpha) ?
        //        Mathematics.Properties.Resources.Check : Mathematics.Properties.Resources.None;

        //    result.Cells[columnP.Index].Tag = interactionRow.Result;

        //    return result;
        //}

        //private DataGridViewRow AddRow(AnovaRow factorRow, string title, bool showVariance)
        //{
        //    DataGridViewRow result = new DataGridViewRow();
        //    result.CreateCells(GridResults);
        //    result.Cells[columnSource.Index].Value = title;
        //    GridResults.Rows.Add(result);

        //    result.Cells[columnDF.Index].Value = factorRow.DegreesOfFreedom;
        //    result.Cells[columnSS.Index].Value = factorRow.SumOfSquares;
        //    if (showVariance) result.Cells[columnMS.Index].Value = factorRow.Variance();

        //    return result;
        //}

        //public void ShowGraph()
        //{
        //    if (evaluation.Count > 25) return;
            
        //    Plot chart = evaluation.ShowOnChart();
        //    chart.AxisYTitle = Anova.AllValues.Name;
        //    chart.AxisXTitle = textBoxFactor.Text;
        //    chart.Update(this, new EventArgs());
        //    chart.Rebuild(this, new EventArgs());
        //    chart.FindForm().SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
        //}

        //private TestResult SelectedTestResult;



        #region Interface logics

        private void contextPosthoc_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in GridResults.SelectedRows)
            {
                AnovaPairwise posterior = new AnovaPairwise(Anova, Anova.IndexOf((string)gridRow.Cells[columnSource.Index].Value));
                posterior.SetFriendlyDesktopLocation(gridRow);
                posterior.Show();
            }
        }

        private void contextChart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in GridResults.SelectedRows)
            {
                Evaluation evaluation = new Evaluation(FactorName);
                List<Sample> samples = Anova.Find((string)gridRow.Cells[columnSource.Index].Value);
                if (samples == null) continue;
                evaluation.AddRange(samples);
                Plot statChart = evaluation.ShowOnChart();
                statChart.AxisYTitle = Anova.AllValues.Name;
                statChart.AxisXTitle = (string)gridRow.Cells[columnSource.Index].Value;
                statChart.Update(sender, e);
                statChart.FindForm().SetFriendlyDesktopLocation(gridRow);
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Anova.Report(Alpha).Run();
        }

        private void pictureBoxCompatibility_DoubleClick(object sender, EventArgs e)
        {
            new TestProperties(Anova.Homoscedasticity, UserSettings.HomogeneityTestName, pictureBoxCompatibility).ShowDialog();
        }

        private void GridResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow gridRow in GridResults.SelectedRows)
            {
                if (gridRow.Cells[columnP.Index].Tag is TestResult)
                {
                    TestResult testResult = (TestResult)gridRow.Cells[columnP.Index].Tag;
                    TestProperties properties = new TestProperties(testResult, "Anova Fisher test");
                    properties.SetFriendlyDesktopLocation(gridRow.Cells[columnP.Index]);
                    properties.SetAlpha(Alpha);
                    properties.ShowDialog();
                }
            }
        }

        private void GridResults_SelectionChanged(object sender, EventArgs e)
        {
            SelectedTestResult = null;

            foreach (DataGridViewRow gridRow in GridResults.SelectedRows)
            {
                if (gridRow.Cells[columnP.Index].Tag is TestResult)
                {
                    SelectedTestResult = (TestResult)gridRow.Cells[columnP.Index].Tag;
                    //contextPosthoc.Enabled = SelectedTestResult.IsPassed(Alpha);
                }
            }
        }

        #endregion
    }
}
