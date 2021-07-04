using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using Mayfly.Mathematics.Statistics;
using Mayfly.Mathematics.Charts;

namespace Mayfly.Mathematics.Statistics
{
    public partial class RegressionComparison : Form
    {
        List<Regression> Regressions;

        List<Regression> SelectedRegressions;

        //public RegressionPool Pool;



        public RegressionComparison()
        {
            InitializeComponent();
            listViewRegressions.Shine();

            Regressions = new List<Regression>();
            SelectedRegressions = new List<Regression>();
        }

        public RegressionComparison(Regression[] regressions)
            : this()
        {
            Regressions.AddRange(regressions);

            listViewRegressions.Items.Clear();

            foreach (Regression regression in Regressions)
            {
                AddItem(regression);
            }

            Run(Regressions);
        }


        public void Run(List<BivariateSample> samples)
        {
            listViewRegressions.Items.Clear();
            listViewRegressions.Enabled = false;
            backCalc.RunWorkerAsync(samples.ToArray());
        }

        private void Run(List<Regression> regressions)
        {
            if (regressions.Count < 2)
            {
                buttonCommon.Enabled = false;

                textBoxCoincidenceP.Text =
                    string.Empty;

                pictureBoxCoincidence.Image =
                    null;
            }
            else
            {
                //Pool = new RegressionPool(regressions);

                //buttonCommon.Enabled = true;

                //textBoxCoincidenceP.Text = Pool.Coincidence.Probability.ToString(Mayfly.Service.Mask(4));

                //if (Pool.Coincidence.Probability > UserSettings.DefaultAlpha)
                //{
                //    pictureBoxCoincidence.Image = Mathematics.Properties.Resources.Check;
                //    toolTip1.SetToolTip(pictureBoxCoincidence, 
                //        "Null hypothesis can't be rejected, so regressions are coincidental.");
                //}
                //else
                //{
                //    pictureBoxCoincidence.Image = Mathematics.Properties.Resources.None;
                //    toolTip1.SetToolTip(pictureBoxCoincidence, 
                //        "Null hypothesis of slopes equality is rejected, so regressionas are not coincidental.");
                //}
            }
        }

        delegate void ItemEventHandler(Regression regression);

        private void AddItem(Regression regression)
        {
            if (listViewRegressions.InvokeRequired)
            {
                ItemEventHandler itemHandler = new ItemEventHandler(AddItem);
                listViewRegressions.Invoke(itemHandler, new object[] { regression });                
            }
            else
            {
                ListViewItem item = new ListViewItem();

                item.Text = regression.Name;
                item.SubItems.Add(regression.GetEquation("N3"));
                item.SubItems.Add(regression.Data.Count.ToString());
                //item.SubItems.Add(regression.ResidualSS.ToString("N1"));
                //item.SubItems.Add(regression.Determination.ToString("P1"));
                //item.SubItems.Add(regression.Fit.GoodnessOfFit.Probability.ToString("P1"));

                listViewRegressions.Items.Add(item);

                listViewRegressions.EnsureVisible(listViewRegressions.Items.Count - 1);
            }
        }
        
        

        private void listViewRegressions_ItemActivate(object sender, EventArgs e)
        {
            foreach (Regression regression in SelectedRegressions)
            {
                RegressionProperties properties = 
                    new RegressionProperties(regression, false);
                properties.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
                properties.Show();
            }
        }

        private void showOnPlotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Scatterplot> scatterplots = new List<Scatterplot>();

            foreach (Linear line in SelectedRegressions)
            {
                Scatterplot scatterplot = new Scatterplot(line.Data, line.Name);
                scatterplot.CalculateApproximation(line.Type);

                scatterplot.Properties.ShowTrend = 
                    scatterplot.Properties.ShowCount =
                    scatterplot.Properties.ShowExplained = 
                    true;

                if (Form.ModifierKeys.HasFlag(Keys.Control) &&
                    scatterplot.Data.Count < UserSettings.SoftSampleSize)
                {
                    continue;
                }

                if (Form.ModifierKeys.HasFlag(Keys.Shift))
                {
                    Plot statChart = scatterplot.ShowOnChart(Form.ModifierKeys.HasFlag(Keys.Alt));
                    statChart.AxisXTitle = line.Data.X.Name;
                    statChart.AxisYTitle = line.Data.Y.Name;
                    statChart.Update(statChart, new EventArgs());
                }
                else
                {
                    scatterplots.Add(scatterplot);
                }
            }

            if (scatterplots.Count > 0)
            {
                Scatterplot.ShowOnChart(Regressions[0].Data.X.Name,
                    Regressions[0].Data.Y.Name, scatterplots);
            }
        }

        private void copyParametersTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            result = "Name\tN\ta±error\tb±error\tr2\tF\tp";

            foreach (Linear line in SelectedRegressions)
            {
                result += Environment.NewLine;
                result += line.Name + "\t";
                result += line.Data.Count + "\t";
                //result += regression.EquationCoefficient(0) + "\t";
                //result += regression.EquationCoefficient(1) + "\t";
                //result += line.Determination + "\t";
                //result += regression.Fit.GoodnessOfFit.Statistic + "\t";
                //result += regression.Fit.GoodnessOfFit.RightProbability + "\t";
            }

            Clipboard.SetText(result);
        }

        private void listViewRegressions_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedRegressions.Clear();

            foreach (ListViewItem item in listViewRegressions.SelectedItems)
            {
                SelectedRegressions.Add(Regressions[item.Index]);
            }

            Run(SelectedRegressions);
        }

        private void pictureBoxCoincidence_DoubleClick(object sender, EventArgs e)
        {
            //new TestProperties(Pool.Coincidence, "Fisher test", pictureBoxCoincidence).ShowDialog();
        }

        //private void pictureBoxSlope_DoubleClick(object sender, EventArgs e)
        //{
        //    new TestProperties(
        //        pictureBoxSlope, ((LinearPool)Pool).SlopeEquality, TestDirection.Right, "Fisher test"
        //        ).ShowDialog();
        //}

        //private void pictureBoxElevation_DoubleClick(object sender, EventArgs e)
        //{
        //    new TestProperties(
        //        pictureBoxSlope, ((LinearPool)Pool).ElevationEquality, TestDirection.Right, "Fisher test"
        //        ).ShowDialog();
        //}

        private void buttonCommon_Click(object sender, EventArgs e)
        {
            //RegressionProperties properties = new RegressionProperties(Pool.TotalRegression, false);
            //properties.SetFriendlyDesktopLocation(this, FormLocation.NextToHost);
            //properties.Show(this);
        }

        private void buttonPairwise_Click(object sender, EventArgs e)
        {
            //RegressionPairwise slope = new RegressionPairwise();

            //foreach (Linear line in SelectedRegressions)
            //{
            //    slope.Add(line);
            //}

            //slope.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            //slope.Show(this);
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Report report = new Report("Regression results");

            foreach (Regression regression in Regressions)
            {
                report.AddSubtitle3(regression.Name);

                report.AddParagraph("Выборка составила {0} экз. Уравнение зависимости массы от длины имеет вид",
                    regression.Data.Count);

                report.AddEquation(regression.GetEquation("W", "L"), ",");

                //report.AddParagraph("имеет {0} (R² = {1:N4}, p = {2:N4}).", 
                //    regression.DeterminationStrength, regression.Determination, regression.Fit.GoodnessOfFit.Probability);
            }

            report.Run();
        }

        private void backCalc_DoWork(object sender, DoWorkEventArgs e)
        {
            BivariateSample[] samples = (BivariateSample[])e.Argument;

            Regressions.Clear();

            foreach (BivariateSample bivariateSample in samples)
            {
                Regression regression = Regression.GetRegression(bivariateSample);

                if (regression == null) continue;

                regression.Name = bivariateSample.X.Name;
                regression.Data.X.Name = bivariateSample.X.Name;
                regression.Data.Y.Name = bivariateSample.Y.Name;

                Regressions.Add(regression);

                AddItem(regression);
            }
        }

        private void backCalc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewRegressions.Enabled = true;
            Run(Regressions);
        }
    }
}
