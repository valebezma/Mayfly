using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Meta.Numerics;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using Mayfly.Mathematics.Charts;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Statistics
{
    public partial class SampleProperties : Form
    {
        public Sample Sample;

        public SampleDisplay SampleDisplay;

        public double ConfidenceLevel
        {
            get { return (double)numericUpDownCL.Value / 100; }
            set { numericUpDownCL.Value = 100 * (decimal)value; }
        }
        
        Histogramma histogram;



        public SampleProperties(IEnumerable<double> values)
        {
            InitializeComponent();
            Sample = new Sample(values);
            SampleDisplay = new SampleDisplay(Sample);
            ConfidenceLevel = 1 - UserSettings.DefaultAlpha;

            UpdateValues();
        }

        public SampleProperties(Sample sample) : this((IEnumerable<double>)sample)
        {
            UpdateValues();
        }



        private void UpdateValues()
        {
            statChart1.Text = Text = string.Format(Resources.Interface.DescriptiveTitle, Sample.Name);

            string f = Sample.MeanFormat();

            textBoxCount.Text = Sample.Count.ToString();
            textBoxMin.Text = Sample.Minimum.ToString(f);
            textBoxMax.Text = Sample.Maximum.ToString(f);
            textBoxRange.Text = (Sample.Maximum - Sample.Minimum).ToString(f);
            textBoxSum.Text = Sample.Sum().ToString(f);

            textBoxMean.Text = Sample.Mean.ToString(f);
            textBoxSE.Text = Service.PresentError(Sample.PopulationMean.Uncertainty);
            textBoxSEP.Text = Sample.PopulationMean.RelativeUncertainty.ToString("P1");
            textBoxVariance.Text = Service.PresentError(Sample.Variance);
            //textBoxCV.Text = Sample.GetVariation().ToString("P2");
            textBoxStdDeviation.Text = Service.PresentError(Sample.StandardDeviation);

            textBoxSkewness.Text = Sample.Skewness.ToString(Mayfly.Service.Mask(4));
            //textBoxKurtosis.Text = Sample.GetKurtosis().ToString(Mayfly.Service.Mask(4));

            if (Sample.Count >= 3)
            {
                TestResult testResult = Sample.KolmogorovSmirnovTest(
                    new NormalDistribution(Sample.Mean, Sample.CorrectedStandardDeviation));

                textBoxNormality.Text = testResult.Statistic.Value.ToString(Mayfly.Service.Mask(4));

                if (testResult.Probability > UserSettings.DefaultAlpha)
                {
                    pictureBoxNormality.Image = Properties.Resources.Check;
                    toolTip1.SetToolTip(pictureBoxNormality,
                        string.Format(Resources.Statistics.NormalityPositive,
                        UserSettings.NormalityTestName, testResult.Statistic,
                        testResult.Probability));
                }
                else
                {
                    pictureBoxNormality.Image = Mathematics.Properties.Resources.None;
                    toolTip1.SetToolTip(pictureBoxNormality,
                        string.Format(Resources.Statistics.NormalityNegative,
                        UserSettings.NormalityTestName, testResult.Statistic,
                        testResult.Probability));
                }

                histogram = new Histogramma(Sample);
                //histogram.Properties.SelectedApproximationType = DistributionType.Normal;
                histogram.Properties.HistogramName = Sample.Name;
                //histogram.Properties.ShowFitResult = histogram.Properties.ShowFit = true;

                statChart1.Clear();
                statChart1.AddSeries(histogram);
                statChart1.SetColorScheme();

                statChart1.Remaster();

                numericUpDownCL_ValueChanged(numericUpDownCL, new EventArgs());
            }

            UpdateDiversity();
        }

        private void UpdateDiversity()
        {
            double N = Sample.Sum();
            double s = Sample.Count;

            double shannon = 0;
            double errorcomponent = 0;

            foreach (double n in Sample)
            {
                if (n == 0) continue;

                double part = n / N;
                shannon += part * Math.Log(part, 2);
                errorcomponent += part * Math.Pow(Math.Log(part, 2), 2);
            }

            shannon = -shannon;
            double error = Math.Sqrt(((errorcomponent - shannon * shannon) / N) + ((s - 1) / (2 * N * N)));
            double max = Math.Log(s, 2);
            double min = (N * Math.Log(N) - (N - s + 1) * Math.Log(N - s + 1)) / N;

            
            textBoxSIValue.Text = shannon.ToString("N2");
            textBoxSIError.Text = Service.PresentError(error);
            textBoxSIMax.Text = max.ToString("N2");
            textBoxSIMin.Text = min.ToString("N2");
        }

        
        private void pictureBoxNormality_DoubleClick(object sender, EventArgs e)
        {
            //new TestProperties(Sample.Normality(), UserSettings.NormalityTestName, pictureBoxNormality).ShowDialog();
        }

        private void numericUpDownCL_ValueChanged(object sender, EventArgs e)
        {
            //double me = Sample.MarginOfError(ConfidenceLevel);
            //textBoxInterval.Text = Service.PresentError(me);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                Clipboard.SetText(SampleDisplay.ToLongString());
            }
            else
            {
                Clipboard.SetText(SampleDisplay.ToString());
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            SampleDisplay.GetReport().Run();
        }

                
        
        private void statChart1_StructureValueChanged(object sender, EventArgs e)
        {
            histogram.Update(sender, e);
        }
    }
}
