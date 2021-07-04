using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using Mayfly.Extensions;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace Mayfly.Mathematics.Statistics
{
    public partial class TestProperties : Form
    {
        public ContinuousTestStatistic Result { get; set; }

        public double Critical 
        {
            get
            {
                return Result.d.Distribution.InverseRightProbability(Alpha);
            }
        }

        public double Alpha 
        {
            set { numericUpDownAlpha.Value = (decimal)value; }
            get { return (double)numericUpDownAlpha.Value; }
        }

        //public TestDirection Direction { get; set; }



        public TestProperties(TestResult testResult, string caption)
        {
            InitializeComponent();

            Text = caption;
            Result = testResult;

            UpdateValues();
        }

        public TestProperties(TestResult testResult, string caption, Control control) 
            : this(testResult, caption)
        {
            this.SetFriendlyDesktopLocation(control.FindForm(), FormLocation.NextToHost);
        }



        private void UpdateValues()
        {
            textBoxStatistic.Text = Result.Value.ToString("G5");
            textBoxP.Text = Result.Probability.ToString("G5");

            UpdateChart();
            numericUpDownAlpha_ValueChanged(numericUpDownAlpha, new EventArgs());
        }

        private void UpdateChart()
        {
            statChart1.Series.Clear();

            double min = Result.Distribution.InverseLeftProbability(.001);
            double max = Result.Distribution.InverseRightProbability(.001);

            statChart1.AxisXMin = min;
            statChart1.AxisXMax = max;
            statChart1.AxisYMin = 0.0;

            Series dist = new Series("Distribution");
            dist.ChartType = SeriesChartType.Line;

            double ymax = 0;
            for (double x = min; x <= max; x += (max - min) / 1000)
            {
                double y = Result.Distribution.ProbabilityDensity(x);
                dist.Points.AddXY(x, y);
                ymax = Math.Max(ymax, y);
            }

            Series crit = new Series("Critical area");
            crit.ChartType = SeriesChartType.Area;
            crit.BackHatchStyle = ChartHatchStyle.Percent25;
            crit.Color = Color.Transparent;

            statChart1.AxisYMax = ymax;
            statChart1.AxisYInterval = Mayfly.Service.GetAutoInterval(statChart1.AxisYMax - statChart1.AxisYMin);
            statChart1.AxisXInterval = Mayfly.Service.GetAutoInterval(statChart1.AxisXMax - statChart1.AxisXMin);

            statChart1.Series.Add(dist);
            statChart1.ApplyPaletteColors();
            crit.BackSecondaryColor = dist.Color;
            statChart1.Series.Add(crit);

            statChart1.Remaster();
        }

        public void SetAlpha(double alpha)
        {
            Alpha = alpha;
            numericUpDownAlpha_ValueChanged(numericUpDownAlpha, new EventArgs());
        }

        private void numericUpDownAlpha_ValueChanged(object sender, EventArgs e)
        {
            textBoxFCrit.Text = Critical.ToString(Mayfly.Service.Mask(5));

            Series crit = statChart1.Series["Critical area"];
            crit.Points.Clear();
            for (double x = Critical; x <= statChart1.AxisXMax; x += (statChart1.AxisXMax - statChart1.AxisXMin) / 1000)
            {
                double y = Result.Distribution.ProbabilityDensity(x);
                crit.Points.AddXY(x, y);
            }

            if (Result.Probability < Alpha)
            {
                textBoxP.ForeColor = Color.Red;
            }
            else
            {
                textBoxP.ForeColor = SystemColors.WindowText;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(string.Format("{0} = {1:N3}, p = {2:N5}",
                Text, Result.Statistic, Result.Probability));
        }

        private void statChart1_Click(object sender, EventArgs e)
        {

        }
    }
}
