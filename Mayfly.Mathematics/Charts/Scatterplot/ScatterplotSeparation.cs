using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace Mayfly.Mathematics.Charts
{
    public partial class ScatterplotSeparation : Form
    {
        #region Properties

        public int SubsamplesCount
        {
            get
            {
                return (int)numericUpDownK.Value;
            }

            set
            {
                numericUpDownK.Value = value;
            }
        }

        public List<Scatterplot> Subsamples { get; set; }

        private float PrevStatistic { get; set; }

        public Series Centroids { get; set; }

        public float Statistic { get; set; }

        public Scatterplot Sample { get; set; }

        public Plot Chart { get; set; }

        private double OverallDistance { get; set; }

        private bool isBusy;

        private bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value;

                numericUpDownK.Enabled =
                    comboBoxMethod.Enabled =
                    comboBoxMeasure.Enabled =
                    buttonRestart.Enabled =
                    buttonNext.Enabled =
                    buttonOK.Enabled =
                    buttonCopy.Enabled =
                    !value;

                if (value) { Cursor = Cursors.WaitCursor; }
                else { Cursor = Cursors.Default; }
            }
        }

        private Distance measure;

        private int method;

        List<string>[] Labels;

        #endregion

        public ScatterplotSeparation(Plot statChart, Scatterplot scatterplot)
        {
            InitializeComponent();

            //spreadSheetClusters.CellMenu = 

            comboBoxMeasure.SelectedIndex = 0;
            comboBoxMethod.SelectedIndex = 0;
            Chart = statChart;
            ColumnSeedX.HeaderText = statChart.AxisXTitle;
            ColumnSeedY.HeaderText = statChart.AxisYTitle;

            Sample = scatterplot;

            scatterplot.ValueChanged += DataSeries_ValueChanged;

            Statistic = float.MaxValue;

            OverallDistance = Distance(Sample.Data.X, Sample.Data.X.Mean) +
                Distance(Sample.Data.Y, Sample.Data.Y.Mean);

            Restart();
            scatterplot.Separator = this;
        }

        private void DataSeries_ValueChanged(object sender, ScatterplotEventArgs e)
        {
            if (this.Visible)
            {
                AutoSeparate();
            }
        }

        #region Methods

        private float Distance(DataPoint[] dps, DataPoint centroid)
        {
            float result = 0;

            foreach (DataPoint dp in dps)
            {

                switch (measure)
                {
                    case Charts.Distance.Chebyshyov:
                        result = Math.Max(result, Distance(dp, centroid));
                        break;
                    default:
                        result += Distance(dp, centroid);
                        break;
                }
            }

            return result;
        }

        private float Distance(DataPoint dp, DataPoint centroid)
        {
            double x = dp.XValue;
            double y = dp.YValues[0];

            double tx = centroid.XValue;
            double ty = centroid.YValues[0];

            if (checkBoxRelative.Checked)
            {
                double xrange = Sample.Data.X.Maximum - Sample.Data.X.Minimum;
                double yrange = Sample.Data.Y.Maximum - Sample.Data.Y.Minimum;

                return Distance(x / xrange, tx / xrange) + Distance(y / yrange, ty / yrange);
            }
            else
            {
                return Distance(x, tx) + Distance(y, ty);
            }
        }

        private float Distance(IEnumerable<double> xs, double center)
        {
            float result = 0;

            foreach (double x in xs)
            {
                switch (measure)
                {
                    case Charts.Distance.Chebyshyov:
                        result = Math.Max(result, Distance(x, center));
                        break;
                    default:
                        result += Distance(x, center);
                        break;
                }
            }

            return result;
        }

        private float Distance(double x1, double x2)
        {
            double result = double.NaN;

            switch (measure)
            {
                case Charts.Distance.Euclidean:
                    result = Math.Sqrt(Math.Pow(x2 - x1, 2));
                    break;
                case Charts.Distance.QuadraticEuclidian:
                    result = Math.Pow(x2 - x1, 2);
                    break;
                case Charts.Distance.Manhattan:
                    result = Math.Abs(x2 - x1);
                    break;
                case Charts.Distance.Chebyshyov:
                    result = Math.Abs(x2 - x1);
                    break;
            }

            return (float)result;
        }

        private float Cost()
        {
            float result = 0;

            for (int i = 0; i < SubsamplesCount; i++)
            {
                foreach (DataPoint dp in Subsamples[i].Series.Points)
                {
                    if (dp.XValue == Centroids.Points[i].XValue && dp.YValues[0] == Centroids.Points[i].YValues[0]) continue;
                    result += Cost(Centroids.Points[i], dp);
                }
            }
            return result;
        }

        private float Cost(DataPoint medoid, DataPoint dp)
        {
            double dx = Math.Abs(medoid.XValue - dp.XValue);
            double dy = Math.Abs(medoid.YValues[0] - dp.YValues[0]);

            return (float)(dx + dy);
        }
        
        public void AutoSeparate(int clusterCount)
        {
            SubsamplesCount = clusterCount;
            AutoSeparate();
        }
        
        public void AutoSeparate()
        {
            Restart();
            PrevStatistic = float.MaxValue;
            IsBusy = true;
            workerPointAssigner.RunWorkerAsync();
        }



        private void Restart()
        {
            if (Subsamples != null)
            {
                foreach (Scatterplot scatterplot in Subsamples)
                {
                    Chart.Remove(scatterplot.Properties.ScatterplotName);
                }
                Console.WriteLine("Series cleared.");
            }
            
            Centroids = Chart.Series.FindByName(Resources.Interface.Centroids);
            if (Centroids == null)
            {
                Centroids = new Series(Resources.Interface.Centroids);
                Centroids.ChartType = SeriesChartType.Point;
                Centroids.MarkerStyle = MarkerStyle.Cross;
                Centroids.Color = Constants.MotiveColor;
                Centroids.MarkerBorderColor = Color.White;
                Centroids.MarkerBorderWidth = 4;
                Centroids.MarkerSize = 20;
                Centroids.IsVisibleInLegend = false;
                Chart.Series.Add(Centroids);
                Centroids.ChartArea = Sample.ChartArea.Name;
            }
            Console.WriteLine("Centroid recreated or got from chart.");

            PrevStatistic = float.MaxValue;
            Statistic = 0;

            InitializeCentroids();
        }

        private void InitializeCentroids()
        {
            Centroids.Points.Clear();

            Random random = new Random();
            int prevnumber = 0;
            int newnumber = 0;

            switch (comboBoxMethod.SelectedIndex)
            {
                default: // k-means, k-medians, k-medoids
                    // Assign centroids to random points
                    for (int i = 0; i < SubsamplesCount; i++)
                    {
                        while (newnumber == prevnumber)
                        {
                            newnumber = random.Next(Sample.Series.Points.Count);
                        }

                        DataPoint randomPoint = Sample.Series.Points[newnumber];
                        Centroids.Points.Add(new DataPoint(randomPoint.XValue, randomPoint.YValues[0]));
                        prevnumber = newnumber;
                    }
                    break;
            }

            // Sort indices by X value
            Centroids.Sort(PointSortOrder.Ascending);
        }

        private void InitializeCentroidsDiagonally()
        {
            Centroids.Points.Clear();

            Random random = new Random();

            // Assign centroids to random points
            for (int i = 0; i < SubsamplesCount; i++)
            {
                double X = Sample.Data.X.Minimum + (i + 1) * (Sample.Data.X.Maximum - Sample.Data.X.Minimum) / (SubsamplesCount + 1);
                double Y = Sample.Data.Y.Minimum + (i + 1) * (Sample.Data.Y.Maximum - Sample.Data.Y.Minimum) / (SubsamplesCount + 1);
                Centroids.Points.Add(X, Y);
            }
        }



        private void RecalculateCentroids()
        {
            PrevStatistic = Statistic;
            Console.WriteLine("Remember statistic value.");

            switch (method)
            {
                case 2: //k-medoids
                    break;
                default:
                    Centroids.Points.Clear();
                    break;
            }

            if (Subsamples != null && Subsamples.Count == SubsamplesCount)
            {
                for (int i = 0; i < SubsamplesCount; i++)
                {
                    switch (method)
                    {
                        case 0: // k-Means
                            Centroids.Points.Add(new DataPoint(Subsamples[i].Data.X.Mean, Subsamples[i].Data.Y.Mean));
                            break;
                        case 1: // k-Medians
                            Centroids.Points.Add(new DataPoint(Subsamples[i].Data.X.Median, Subsamples[i].Data.Y.Median));
                            break;
                        case 2: //k-medoids
                            float cost = Cost();

                            foreach (DataPoint dp in Subsamples[i].Series.Points)
                            {
                                if (dp.XValue == Centroids.Points[i].XValue && dp.YValues[0] == Centroids.Points[i].YValues[0]) continue;

                                double x = dp.XValue;
                                double y = dp.YValues[0];

                                // swap
                                dp.XValue = Centroids.Points[i].XValue;
                                dp.YValues[0] = Centroids.Points[i].YValues[0];

                                if (cost > Cost())
                                {
                                    // deswap
                                    dp.XValue = x;
                                    dp.YValues[0] = y;
                                    continue;
                                }
                                else
                                {
                                    cost = Cost();
                                }
                            }
                            break;
                    }
                }

                spreadSheetClusters.Rows.Clear();
                for (int i = 0; i < SubsamplesCount; i++)
                {
                    AddClusterRow(Subsamples[i]);
                }
            }

            IsBusy = true;
            workerPointAssigner.RunWorkerAsync();
        }

        private DataGridViewRow AddClusterRow(Scatterplot Subsample)
        {
            Sample X =  new Sample(Subsample.Data.X);
            X.Name = Subsample.Data.X.Name;

            Sample Y =  new Sample(Subsample.Data.Y);
            Y.Name = Subsample.Data.Y.Name;

            int i = spreadSheetClusters.Rows.Add(Subsample.Name, Subsample.Data.Count, X, Y);
            DataGridViewRow result = spreadSheetClusters.Rows[i];
            result.Cells[ColumnSeedX.Index].ToolTipText = new SampleDisplay(X).ToLongString();
            result.Cells[ColumnSeedY.Index].ToolTipText = new SampleDisplay(Y).ToLongString();
            return result;
        }

        #endregion

        #region Interface logics

        private void numericUpDownK_ValueChanged(object sender, EventArgs e)
        {
            SubsamplesCount = (int)numericUpDownK.Value;
            AutoSeparate();
        }

        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            method = comboBoxMethod.SelectedIndex;
            if (Visible) AutoSeparate();
        }

        private void comboBoxMeasure_SelectedIndexChanged(object sender, EventArgs e)
        {
            measure = (Distance)comboBoxMeasure.SelectedIndex;
            if (Visible) AutoSeparate();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                Restart();

                PrevStatistic = float.MaxValue;
                buttonNext.Visible = true;
            }
            else if (ModifierKeys == (Keys.Control | Keys.Shift))
            {
                Restart();
                InitializeCentroidsDiagonally();

                PrevStatistic = float.MaxValue;
                buttonNext.Visible = true;
            }
            else
            {
                AutoSeparate();
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            RecalculateCentroids();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            if (ModifierKeys == Keys.Control) // If [Ctrl] is pressed
            {
                result = "Sample" + Constants.Tab;

                for (int i = 0; i < SubsamplesCount; i++)
                {
                    result += Subsamples[i].Name + Constants.Tab + Constants.Tab + Constants.Tab;
                }

                result += Constants.Return;
                result += Constants.Tab;

                for (int i = 0; i < SubsamplesCount; i++)
                {
                    result += "Count" + Constants.Tab;
                    result += Chart.AxisXTitle + Constants.Tab;
                    result += Chart.AxisYTitle + Constants.Tab;
                }

                result += Constants.Return;
                
                result += Sample.Name + Constants.Tab;

                for (int i = 0; i < SubsamplesCount; i++)
                {
                    result += Subsamples[i].Data.Count + Constants.Tab;
                    result += new Sample(Subsamples[i].Data.X).ToString() + Constants.Tab;
                    result += new Sample(Subsamples[i].Data.Y).ToString() + Constants.Tab;
                }

                result += Constants.Return;
            }
            else if (ModifierKeys == (Keys.Shift | Keys.Control)) // If [Shift + Ctrl] are pressed
            {
                result = Sample.Name + Constants.Tab;

                for (int i = 0; i < SubsamplesCount; i++)
                {
                    result += Subsamples[i].Data.Count + Constants.Tab;
                    result += new Sample(Subsamples[i].Data.X).ToString() + Constants.Tab;
                    result += new Sample(Subsamples[i].Data.Y).ToString() + Constants.Tab;
                }
            }
            else // If no modifier keys are pressed copy with header and not rounded
            {
                result = Resources.Interface.Sample + Constants.Tab +
                    Resources.Interface.Subsample + Constants.Tab +
                    Resources.Interface.Count + Constants.Tab +
                    Chart.AxisXTitle + Constants.Tab +
                    Chart.AxisYTitle + Constants.Return;

                for (int i = 0; i < SubsamplesCount; i++)
                {
                    if (i == 0)
                    {
                        result += Sample.Name + Constants.Tab;
                    }
                    else
                    {
                        result += Constants.Tab;
                    }

                    result += Subsamples[i].Name + Constants.Tab;
                    result += Subsamples[i].Data.Count + Constants.Tab;
                    result += new Sample(Subsamples[i].Data.X).ToString() + Constants.Tab;
                    result += new Sample(Subsamples[i].Data.Y).ToString() + Constants.Return;
                }
            }

            Clipboard.SetText(result);
        }

        private void workerPointAssigner_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Assigner started.");
            Subsamples = new List<Scatterplot>(SubsamplesCount);

            Labels = new List<string>[SubsamplesCount];

            // Define resulting samples
            for (int i = 0; i < SubsamplesCount; i++)
            {
                BivariateSample bivariateSample = new BivariateSample();
                bivariateSample.X.Name = Chart.AxisXTitle + ". " + string.Format(Resources.Interface.SubsampleMask, i + 1);
                bivariateSample.Y.Name = Chart.AxisYTitle + ". " + string.Format(Resources.Interface.SubsampleMask, i + 1);
                Scatterplot scatterplot = new Scatterplot(bivariateSample,
                    string.Format(Resources.Interface.SubsampleMask, i + 1));
                Subsamples.Add(scatterplot);
                Labels[i] = new List<string>();
            }

            Statistic = 0;

            for (int i = 0; i < Sample.Data.Count; i++)
            {
                // Define which centroid is closer
                float minimalDistance = float.MaxValue;
                int centroidNo = 0;

                for (int j = 0; j < Centroids.Points.Count; j++)
                {
                    // Compute the distance between current point and currently checking centroid

                    float distance = Distance(Sample.Data.X.ElementAt(i), Centroids.Points[j].XValue);
                    distance += Distance(Sample.Data.Y.ElementAt(i), Centroids.Points[j].YValues[0]);

                    if (distance < minimalDistance)
                    {
                        minimalDistance = distance;
                        centroidNo = j;
                    }
                }

                // Summarize overall statistic
                Statistic += minimalDistance;

                // Create new datapoint in defined subsample
                Subsamples[centroidNo].Data.Add(Sample.Data.X.ElementAt(i), Sample.Data.Y.ElementAt(i));

                if (Sample.Series.Points[i].Label.IsAcceptable() &&
                    !Labels[centroidNo].Contains(Sample.Series.Points[i].Label))
                {
                    Labels[centroidNo].Add(Sample.Series.Points[i].Label);
                }
            }
        }

        private void workerPointAssigner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Assigner completed");

            textBoxStat.Text = Statistic.ToString("N4");
            textBoxPerc.Text = (1 - Statistic / OverallDistance).ToString("P2");

            for (int i = 0; i < Subsamples.Count; i++)
            {
                if (Subsamples[i].Data.Count == 0)
                {
                    AutoSeparate();
                    return;
                }

                Scatterplot scatterplot = Subsamples[i];
                Chart.Remove(scatterplot.Name);

                scatterplot.BuildSeries();
                scatterplot.Series.ChartArea = Sample.ChartArea.Name;
                scatterplot.Series.MarkerStyle = MarkerStyle.Circle;
                scatterplot.Properties.DataPointBorderWidth = 1;
                scatterplot.Properties.DataPointSize = 12;
                Chart.AddSeries(scatterplot);

                if (Labels[i].Count > 0)
                {
                    scatterplot.Series.LegendText = string.Format("{0} ({1})",
                        scatterplot.Name, Labels[i].ToArray().Merge());
                }
            }

            Chart.Remaster();

            buttonNext.Visible = PrevStatistic != Statistic;

            spreadSheetClusters.Rows.Clear();
            for (int i = 0; i < SubsamplesCount; i++)
            {
                AddClusterRow(Subsamples[i]);
            }


            if (Statistic != PrevStatistic)
            {
                Console.WriteLine("Statistic changed.");
                RecalculateCentroids();
            }
            else
            {
                Console.WriteLine("Done!");
                IsBusy = false;
            }
        }

        #endregion
    }

    enum Distance
    {
        Euclidean,
        QuadraticEuclidian,
        Manhattan,
        Chebyshyov,
        Power
    }
}
