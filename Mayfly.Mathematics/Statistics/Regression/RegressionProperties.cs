using System;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Charts;
using Mayfly.Extensions;
using System.Collections.Generic;
using Meta.Numerics;
using Meta.Numerics.Statistics.Distributions;

namespace Mayfly.Mathematics.Statistics
{
    public partial class RegressionProperties : Form
    {
        public Regression Regression;



        public RegressionProperties(Regression regression, bool enabled)
        {
            InitializeComponent();

            comboBoxType.Items.AddRange(Service.GetRegressionTypes());
            comboBoxType.Enabled = enabled;

            Regression = regression;
            UpdateValues();
        }



        public void ChangeRegression(object sender, ScatterplotEventArgs e)
        {
            if (IsDisposed) return;
            Regression = e.Sample.Regression;

            UpdateValues(); 
        }

        private void UpdateValues()
        {
            Text = Regression.Name;

            buttonX.Enabled =
                buttonY.Enabled =
                buttonCopy.Enabled =
                buttonReport.Enabled = true;

            comboBoxType.SelectedIndex = (int)Regression.Type;
            webBrowserEquation.DocumentText = Report.GetHtmlFromLatex(Regression.Equation);
            textBoxN.Text = Regression.Data.Count.ToString();
            domainParameters.Items.Clear();

            for (int i = 0; i < Regression.Parameters.Count; i++)
            {
                domainParameters.Items.Add(Regression.Parameters[i].ToString("G3"));
            }

            domainParameters.Text = string.Empty;
            domainParameters.SelectedIndex = 0;


            //textBoxSSR.Text = Regression.ResidualSS.ToString(Textual.Mask(Regression.Data.Y.Precision()));
            //textBoxSSY.Text = Regression.TotalSS.ToString(Textual.Mask(Regression.Data.Y.Precision()));
            //textBoxSE.Text = Regression.StandardError.ToString(Textual.Mask(4));

            //double r = Regression.Determination;
            //textBoxDC.Text = r.ToString("N4");
            ////chartR2.Series[0].AnimateChart(r, new Label(), .75);
            //labelR2.Text = Regression.DeterminationStrength.Replace(" ", Environment.NewLine);
            ////labelR2.CenterWith(chartR2);

            //TestResult test = Regression.Fit.GoodnessOfFit;
            //textBoxF.Text = test.Statistic.ToString(Textual.Mask(4));

            //if (test.IsPassed())
            //{
            //    pictureBoxF.Image = Mathematics.Properties.Resources.Check;
            //    toolTip1.SetToolTip(pictureBoxF,
            //        Resources.Statistics.RegressionPositive);
            //}
            //else
            //{
            //    pictureBoxF.Image = Mathematics.Properties.Resources.None;
            //    toolTip1.SetToolTip(pictureBoxF,
            //        Resources.Statistics.RegressionNegative);
            //}

            buttonX.Text = Regression.Data.X.Name;
            buttonY.Text = Regression.Data.Y.Name;
            buttonX.AutoSize = buttonY.AutoSize = true;
            toolTip1.SetToolTip(buttonX, string.Format(Resources.Interface.DescriptiveTitle, Regression.Data.X.Name));
            toolTip1.SetToolTip(buttonY, string.Format(Resources.Interface.DescriptiveTitle, Regression.Data.Y.Name));
        }



        private void comboBoxTrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContainsFocus && comboBoxType.Enabled)
            {
                Regression reg = Regression.GetRegression(Regression.Data,
                    (TrendType)comboBoxType.SelectedIndex);

                if (reg == null)
                {
                    webBrowserEquation.DocumentText = Constants.Null;
                    domainParameters.Items.Clear();
                    textBoxSE.Text =
                        //textBoxDC.Text =
                        //textBoxP.Text =
                        Constants.Null;

                    buttonX.Enabled =
                        buttonY.Enabled =
                        buttonCopy.Enabled =
                        buttonReport.Enabled = false;

                    return;
                }

                Regression = reg;
                UpdateValues();
            }
        }

        private void pictureBoxF_DoubleClick(object sender, EventArgs e)
        {
            //new TestProperties(Regression.Fit.GoodnessOfFit, "Fisher test", pictureBoxF).ShowDialog();
        }

        private void domainParameters_SelectedItemChanged(object sender, EventArgs e)
        {
            if (domainParameters.SelectedIndex == -1)
            {
                pictureBoxParameter.Image = null;
                toolTip1.SetToolTip(pictureBoxParameter, string.Empty);
            }
            else
            {
                //UncertainValue coefficient = Regression.Parameter(domainParameters.SelectedIndex);
                //TestResult test = Regression.ParameterTest(domainParameters.SelectedIndex);

                //if (test.Probability < UserSettings.DefaultAlpha)
                //{
                //    pictureBoxParameter.Image = Mathematics.Properties.Resources.Check;
                //    toolTip1.SetToolTip(pictureBoxParameter,
                //        "Coefficient is statistically confident");
                //}
                //else
                //{
                //    pictureBoxParameter.Image = Mathematics.Properties.Resources.None;
                //    toolTip1.SetToolTip(pictureBoxParameter,
                //        "Статистическая значимость коэффициента регрессии не подтверждается " +
                //        "(принимаем гипотезу о равенстве нулю этого коэффициента). " +
                //        "Это означает, что в данном случае коэффициентом можно пренебречь.");
                //}
            }
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            SampleProperties DF = new SampleProperties(Regression.Data.X);
            DF.Show();
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            SampleProperties DF = new SampleProperties(Regression.Data.Y);
            DF.Show();
        }

        private void numX_TextChanged(object sender, EventArgs e)
        {
            numYp.Format = numX.Format;
            numYp.Value = Regression.Predict(numX.Value);
        }

        private void numY_TextChanged(object sender, EventArgs e)
        {
            numXp.Format = numY.Format;
            numXp.Value = Regression.PredictInversed(numY.Value);
        }



        private void buttonCopy_Click(object sender, EventArgs e)
        {
            string result = string.Empty;


            Clipboard.SetText(result);
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Regression.GetReport().Run();
        }
    }
}
