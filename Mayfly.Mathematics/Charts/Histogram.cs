using Mayfly.Extensions;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms.DataVisualization.Charting;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;
using Meta.Numerics;

namespace Mayfly.Mathematics.Charts
{
    public class Histogramma : List<HistogramBin>
    {
        public Sample Data { get; set; }

        public ContinuousDistribution Distribution;


        public Plot Container { set; get; }

        public HistogramProperties Properties { get; set; }

        public Series Series { get; set; }

        public Functor Fit { get; set; }

        //public Series FitSeries { get; set; }



        public CalloutAnnotation FitAnnotation { get; set; }

        public ChartArea ChartArea
        {
            get
            {
                ChartArea result = Container.ChartAreas.FindByName(Series.ChartArea);
                if (result == null) result = Container.ChartAreas[0];
                return result;
            }

            set
            {
                Series.ChartArea = value.Name;
                //if (FitSeries != null) FitSeries.ChartArea = value.Name;
            }
        }

        public double Left { get { return Data.Minimum; } }

        public double Right { get { return Data.Maximum; } }

        public int Top
        {
            get
            {
                int result = 0;

                foreach (HistogramBin bin in this)
                {
                    result = Math.Max(result, bin.Frequency);
                }

                return result;
            }
        }

        public bool IsChronic { get; set; }



        public event HistogramEventHandler Updated;

        

        public Histogramma(string name)
        {
            Properties = new HistogramProperties(this);
            Properties.HistogramName = name;
        }

        public Histogramma(Sample sample) : this(sample.Name)
        {
            Data = sample;
        }

        public Histogramma(string name, IEnumerable<double> values, bool isChronic) : this(new Sample(values) { Name = name })
        {
            Data = new Sample(values);
            IsChronic = isChronic;
        }



        public void Add(double start, double end)
        {
            Add(new HistogramBin(start, end));
        }

        public void Add(double value)
        {
            foreach (HistogramBin bin in this)
            {
                switch (Properties.SelectedClassBorderType)
                {
                    case BalanceSide.Left:
                        if (bin.Interval.LeftClosedContains(value))
                        {
                            bin.Increment();
                            return;
                        }
                        break;

                    case BalanceSide.Right:
                        if (bin.Interval.RightClosedContains(value))
                        {
                            bin.Increment();
                            return;
                        }
                        break;
                }
            }
        }

        public void Update()
        {
            if (Container == null) return;

            if (Series == null)
            {
                Series = new Series(Properties.HistogramName)
                { 
                    ChartType = SeriesChartType.Column
                };

                Container.Series.Add(Series);
            }
            else
            {
                Series.Points.Clear();
            }

            if (Data == null) return;

            //if (IsChronic)
            //{
            //    UpdateDataPoints(Container.AxisXMin, Container.TimeInterval);
            //}
            //else
            //{
            UpdateDataPoints();
            //}

            //if (Count > 150)
            //{
            //    Properties.PointWidth = 1;
            //}

            Series.Name = Properties.HistogramName;

            if (!Container.IsDistinguishingMode)
            {
                Series.Color = Properties.DataPointColor;

                if (Properties.Borders)
                {
                    Series.BorderWidth = 1;
                    Series.BorderColor = Color.Black;
                    Series.BorderDashStyle = ChartDashStyle.Solid;
                }
                else
                {
                    Series.BorderWidth = 0;
                    Series.BorderDashStyle = ChartDashStyle.NotSet;
                }
            }

            Series.SetCustomProperty("PointWidth", Properties.PointWidth.ToString("0.00", CultureInfo.InvariantCulture));

            if (Properties.ShowFit)
            {
                Distribution = DistributionExtensions.GetDistribution(Properties.SelectedApproximationType, Data);

                if (Distribution != null)
                {
                    if (Fit == null)
                    {
                        Fit = new Functor(Properties.FitName, (x) => { return Container.AxisXInterval * Data.Count * Distribution.ProbabilityDensity(x); });
                        Container.AddSeries(Fit);
                    }
                    else
                    {
                        Fit.Properties.FunctionName = Properties.FitName;
                    }

                    Fit.Properties.TrendWidth = Properties.FitWidth;
                    Fit.Properties.TrendColor = Properties.FitColor;
                    Fit.Update();

                    if (Properties.ShowAnnotation)
                    {
                        if (FitAnnotation == null)
                        {
                            FitAnnotation = new CalloutAnnotation
                            {
                                BackColor = Container.ChartAreas[0].BackColor,
                                Name = Properties.HistogramName,
                                IsSizeAlwaysRelative = false,
                                CalloutStyle = CalloutStyle.Rectangle,
                                Alignment = ContentAlignment.MiddleCenter,
                                AllowMoving = true,
                                AxisX = Container.ChartAreas[0].AxisX,
                                AxisY = Container.ChartAreas[0].AxisY,
                                X = Left + 3 * (Right - Left) / 4,
                                Y = Fit.Predict(FitAnnotation.X)
                            };
                            Container.Annotations.Add(FitAnnotation);
                        }

                        FitAnnotation.Font = Container.Font;
                        FitAnnotation.Visible = true;
                        FitAnnotation.Text = Properties.FitName;

                        if (Properties.ShowCount)
                        {
                            FitAnnotation.Text += Constants.Break + "n = " + Data.Count;
                        }

                        if (Properties.ShowFitResult)
                        {
                            TestResult testResult = Data.KolmogorovSmirnovTest(Distribution);
                            FitAnnotation.Text += Constants.Break + string.Format(Resources.Interface.FitAnnotation, testResult.Probability);
                        }
                    }
                    else
                    {
                        if (FitAnnotation != null)
                        {
                            FitAnnotation.Visible = false;
                        }
                    }
                }
                else
                {
                    Properties.ShowFit = false;
                    Properties.SelectedApproximationType = DistributionType.Auto;
                    if (Fit != null) Fit.Series.Points.Clear();
                    //return;
                }
            }
            else
            {
                if (Fit != null)
                {
                    Container.Series.Remove(Fit.Series);
                    Container.Functors.Remove(Fit);
                    Fit = null;
                }
            }

            if (Updated != null)
            {
                Updated.Invoke(this, new HistogramEventArgs(this));
            }
        }

        public int Distribute()
        {
            Clear();

            if (Data == null) return 0;

            // Create bins
            for (double x = Container.AxisXMin; x < Container.AxisXMax; x += Container.AxisXInterval)
            {
                Add(x, x + Container.AxisXInterval);
            }

            // Populate bins
            foreach (double d in Data)
            {
                Add(d);
            }

            return this.Top;
        }

        private void UpdateDataPoints()
        {
            if (Distribute() == 0) return;

            // Create Datapoints
            Series.Points.Clear();

            foreach (HistogramBin bin in this)
            {
                DataPoint dataPoint = new DataPoint(bin.Interval.Midpoint, bin.Frequency);

                if (IsChronic)
                {
                    dataPoint.ToolTip = Properties.HistogramName + Constants.Return +
                        DateTime.FromOADate(bin.Interval.LeftEndpoint).ToString("G") + " - " + 
                        DateTime.FromOADate(bin.Interval.RightEndpoint).ToString("G") + Constants.Return +
                        dataPoint.YValues[0];
                }
                else
                {
                    dataPoint.ToolTip = Properties.HistogramName + Constants.Return +
                        bin.Interval.LeftEndpoint + " - " +
                        bin.Interval.RightEndpoint + Constants.Return +
                        dataPoint.YValues[0];
                }

                Series.Points.Add(dataPoint);
            }
        }

        //private void UpdateDataPoints(double minimum, DateTimeIntervalType interval)
        //{
        //    Clear();

        //    switch (interval)
        //    {
        //        case DateTimeIntervalType.Months:
        //            int daysInMonth = 0;
        //            for (double x = minimum; x <= Data.Maximum; x += daysInMonth)
        //            {
        //                DateTime firstDay = DateTime.FromOADate(x);
        //                daysInMonth = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(
        //                    firstDay.Year, firstDay.Month);
        //                Add(x, x + daysInMonth);
        //            }
        //            break;
        //        case DateTimeIntervalType.Years:
        //            int daysInYear = 0;
        //            for (double x = minimum; x <= Data.Maximum; x += daysInYear)
        //            {
        //                DateTime firstDay = DateTime.FromOADate(x);
        //                daysInYear = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetDaysInYear(
        //                    firstDay.Year);
        //                Add(x, x + daysInYear);
        //            }
        //            break;
        //        default:
        //            double inter = 1 / Mayfly.Service.GetAutoInterval(interval);
        //            for (double x = minimum; x <= Data.Maximum; x += inter)
        //            {
        //                Add(x, x + inter);
        //            }
        //            break;
        //    }

        //    foreach (double d in Data)
        //    {
        //        Add(d);
        //    }
        //}


        #region

        public Plot ShowOnChart()
        {
            return ShowOnChart(false);
        }

        public Plot ShowOnChart(bool modal)
        {
            return ShowOnChart(modal, "X");
        }

        public Plot ShowOnChart(string title)
        {
            return ShowOnChart(false, title);
        }

        public void ShowOnChart(Plot statChart)
        {
            statChart.AddSeries(this);
        }

        public Plot ShowOnChart(bool modal, string title)
        {
            return ShowOnChart(modal, title, Data.Minimum, Mayfly.Service.GetAutoInterval(Data.Maximum - Data.Minimum));
        }

        public Plot ShowOnChart(bool modal, string title, double min, double interval)
        {
            ChartForm result = new ChartForm(title);
            result.Text = Properties.HistogramName;
            result.StatChart.IsChronic = IsChronic;
            result.StatChart.AxisXMin = min;
            result.StatChart.AxisXInterval = interval;
            result.StatChart.AddSeries(this);
            result.StatChart.Remaster();
            //Updated += new HistogramEventHandler(result.StatChart.Update);

            if (modal)
            {
                result.ShowDialog();
            }
            else
            {
                result.Show();
            }

            return result.StatChart;
        }

        #endregion
    }

    public class HistogramBin
    {
        public HistogramBin(double start, double end)
        {
            Interval = Interval.FromEndpoints(start, end);
        }

        public HistogramBin(double start, double end, int frequency) : this(start, end)
        {
            Frequency = frequency;
        }

        public Interval Interval;

        public int Frequency
        {
            set;
            get;
        }

        public void Increment()
        {
            Frequency++;
        }

        public void Decrement()
        {
            Frequency--;
        }
    }

    public enum BalanceSide
    {
        Left,
        Right
    };
}
