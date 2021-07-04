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

namespace Mayfly.Mathematics.Charts
{
    public class Histogramma : List<HistogramBin>, ISerializable, IDisposable
    {
        public Plot Container { set; get; }

        public HistogramProperties Properties { get; set; }

        public Sample Data { get; set; }

        public Series DataSeries { get; set; }

        public bool IsChronic { get; set; }

        public double Left { get { return Math.Floor(Data.Minimum); } }

        public double Right { get { return Math.Ceiling(Data.Maximum); } }

        public double Top
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

        public ChartArea ChartArea
        {
            get
            {
                ChartArea result = Container.ChartAreas.FindByName(DataSeries.ChartArea);
                if (result == null) result = Container.ChartAreas[0];
                return result;
            }

            set
            {
                DataSeries.ChartArea = value.Name;
                //if (FitSeries != null) FitSeries.ChartArea = value.Name;
            }
        }

        //public CalloutAnnotation FitAnnotation { get; set; }

        public event HistogramEventHandler ValueChanged;

        public event HistogramEventHandler StructureChanged;

        

        public Histogramma()
        {
            Properties = new HistogramProperties(this);
            Properties.ValueChanged += Properties_ValueChanged;
            Properties.StructureChanged += Properties_StructureChanged;
        }

        public Histogramma(Sample sample)
        {
            Properties = new HistogramProperties(this);
            Properties.HistogramName = sample.Name;
            Properties.ValueChanged += new HistogramEventHandler(Properties_ValueChanged);
            Properties.StructureChanged += Properties_StructureChanged;
            Data = sample;
        }

        public Histogramma(string name, IEnumerable<double> values, bool isChronic)
        {
            Properties = new HistogramProperties(this);
            Properties.HistogramName = name;
            Properties.ValueChanged += new HistogramEventHandler(Properties_ValueChanged);
            Data = new Sample(values);
            IsChronic = isChronic;
        }

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~Histogramma()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Container != null)
                    {
                        if (!Container.IsDisposed && DataSeries != null)
                        {
                            Container.Series.Remove(DataSeries);
                            DataSeries.Dispose();

                            //if (FitSeries != null)
                            //{
                            //    Container.Series.Remove(FitSeries);
                            //    FitSeries.Dispose();
                            //}

                            //if (FitAnnotation != null)
                            //{
                            //    Container.Annotations.Remove(FitAnnotation);
                            //    FitAnnotation.Dispose();
                            //}
                        }

                        Container.Histograms.Remove(this);
                    }

                    Properties.Dispose();

                    Properties = null;
                }

                disposed = true;
            }
        }



        /// <summary>
        /// Adds new bin with specified borders
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void Add(double start, double end)
        {
            Add(new HistogramBin(start, end));
        }

        /// <summary>
        /// Adds value to histogram
        /// </summary>
        /// <param name="value"></param>
        public void Add(double value)
        {
            foreach (HistogramBin bin in this)
            {
                switch (Properties.SelectedClassBorderType)
                {
                    case BalanceSide.Left:
                        if (value >= bin.Start && value < bin.End)
                        {
                            bin.Increment();
                        }
                        break;
                    case BalanceSide.Right:
                        if (value > bin.Start && value <= bin.End)
                        {
                            bin.Increment();
                        }
                        break;
                }
            }
        }

        public void Update(object sender, EventArgs e)
        {
            if (Container == null) return;

            DataSeries.Name = DataSeries.LegendText =
                 Properties.HistogramName;

            if (!Container.IsDistinguishingMode)
            {
                DataSeries.Color = Properties.DataPointColor;
                if (Properties.Borders)
                {
                    DataSeries.BorderWidth = 1;
                    DataSeries.BorderColor = Color.Black;
                    DataSeries.BorderDashStyle = ChartDashStyle.Solid;
                }
                else
                {
                    DataSeries.BorderWidth = 0;
                    DataSeries.BorderDashStyle = ChartDashStyle.NotSet;
                }
            }

            DataSeries.SetCustomProperty("PointWidth",
                Properties.PointWidth.ToString("0.00", CultureInfo.InvariantCulture));

            //if (Properties.ShowFit)
            //{
            //    CalculateApproximation(Properties.SelectedApproximationType);

            //    if (Distribution != null)
            //    {
            //        BuildFit(Container.AxisXMin, Container.AxisXMax);

            //        FitSeries.Name = Properties.FitName;
            //        FitSeries.BorderWidth = Properties.FitWidth;

            //        if (Container.IsDistinguishingMode)
            //        {
            //            FitSeries.Color = Color.DarkSalmon;
            //        }
            //        else
            //        {
            //            FitSeries.Color = Properties.FitColor;
            //        }
            //    }
            //}
            //else
            //{
            //    if (FitSeries != null)
            //    {
            //        Container.Series.Remove(FitSeries);
            //        FitSeries = null;
            //    }
            //}

            //if (Properties.ShowAnnotation)
            //{
            //    if (FitAnnotation == null)
            //    {
            //        FitAnnotation = new CalloutAnnotation();

            //        FitAnnotation.BackColor = Container.ChartAreas[0].BackColor;
            //        FitAnnotation.Name = Properties.HistogramName;
            //        FitAnnotation.IsSizeAlwaysRelative = false;
            //        FitAnnotation.CalloutStyle = CalloutStyle.Rectangle;
            //        FitAnnotation.Alignment = ContentAlignment.MiddleCenter;
            //        FitAnnotation.AllowMoving = true;
            //        FitAnnotation.AxisX = Container.ChartAreas[0].AxisX;
            //        FitAnnotation.AxisY = Container.ChartAreas[0].AxisY;
            //        FitAnnotation.X = Left + 3 * (Right - Left) / 4;
            //        FitAnnotation.Y = Container.AxisXInterval * Data.Count *
            //            Distribution.ProbabilityDensity(FitAnnotation.X);
            //        Container.Annotations.Add(FitAnnotation);
            //    }

            //    FitAnnotation.Font = Container.Font;
            //    FitAnnotation.Visible = true;
            //    FitAnnotation.Text = Properties.FitName;

            //    if (Properties.ShowCount)
            //    {
            //        FitAnnotation.Text += Constants.Break + "N = " + Data.Count;
            //    }

            //    if (Properties.ShowFitResult)
            //    {
            //        TestResult testResult = Data.KolmogorovSmirnovTest(Distribution);
            //        FitAnnotation.Text += Constants.Break + string.Format(Resources.Interface.FitAnnotation, testResult.Probability);
            //    }
            //}
            //else
            //{
            //    if (FitAnnotation != null)
            //    {
            //        FitAnnotation.Visible = false;
            //    }
            //}
        }

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
            result.StatChart.IsChronic = IsChronic;
            result.Text = Properties.HistogramName;

            result.StatChart.AxisXMin = min;
            result.StatChart.AxisXInterval = interval;

            BuildChart(result.StatChart);

            result.StatChart.AddSeries(this);
            result.StatChart.Remaster();

            ValueChanged += new HistogramEventHandler(result.StatChart.Update);
            StructureChanged += new HistogramEventHandler(result.StatChart.Rebuild);

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

        //public void CalculateApproximation(DistributionType type)
        //{
        //    if (Data == null) return;
        //    Distribution = DistributionExtensions.GetDistribution(type, Data);
        //}

        //public Histogram Copy()
        //{
        //    Histogram result = new Histogram(Properties.HistogramName, Data, IsChronic);
        //    return result;
        //}

        public void BuildChart()
        {
            BuildChart(Container);
        }

        public void BuildChart(Plot statChart)
        {
            if (IsChronic)
            {
                Distribute(statChart.AxisXMin, statChart.TimeInterval);
            }
            else
            {
                Distribute(statChart.AxisXMin, statChart.AxisXInterval);
            }

            if (DataSeries == null)
            {
                DataSeries = new Series(Properties.HistogramName);
                DataSeries.ChartType = SeriesChartType.Column;
            }
            else
            {
                DataSeries.Points.Clear();
            }

            if (Count > 150)
            {
                Properties.PointWidth = 1;
            }

            foreach (HistogramBin bin in this)
            {
                DataPoint dataPoint = new DataPoint(bin.X, bin.Frequency);
                
                if (IsChronic)
                {
                    dataPoint.ToolTip = Properties.HistogramName + Constants.Return +
                        DateTime.FromOADate(bin.Start).ToString("G") + " - " + DateTime.FromOADate(bin.End).ToString("G") + Constants.Return +
                        dataPoint.YValues[0];
                }
                else
                {
                    dataPoint.ToolTip = Properties.HistogramName + Constants.Return +
                        bin.Start + " - " + bin.End + Constants.Return +
                        dataPoint.YValues[0];
                }

                DataSeries.Points.Add(dataPoint);
            }

            //if (Properties.ShowFit)
            //{
            //    BuildFit(Container.AxisXMin, Container.AxisXMax);
            //}

            if (statChart.AxisYAutoMaximum)
            {
                statChart.UpdateYMax();
                statChart.RefreshAxes();
            }
        }

        private void Distribute(double minimum, DateTimeIntervalType interval)
        {
            Clear();

            switch (interval)
            {
                case DateTimeIntervalType.Months:
                    int daysInMonth = 0;
                    for (double x = minimum; x <= Data.Maximum; x += daysInMonth)
                    {
                        DateTime firstDay = DateTime.FromOADate(x);
                        daysInMonth = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(
                            firstDay.Year, firstDay.Month);
                        Add(x, x + daysInMonth);
                    }
                    break;
                case DateTimeIntervalType.Years:
                    int daysInYear = 0;
                    for (double x = minimum; x <= Data.Maximum; x += daysInYear)
                    {
                        DateTime firstDay = DateTime.FromOADate(x);
                        daysInYear = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetDaysInYear(
                            firstDay.Year);
                        Add(x, x + daysInYear);
                    }
                    break;
                default:
                    double inter = 1 / Mayfly.Service.GetAutoInterval(interval);
                    for (double x = minimum; x <= Data.Maximum; x += inter)
                    {
                        Add(x, x + inter);
                    }
                    break;
            }

            foreach (double d in Data)
            {
                Add(d);
            }
        }

        private void Distribute(double minimum, double interval)
        {
            Clear();

            for (double x = minimum; x < Data.Maximum; x += interval)
            {
                Add(x, x + interval);
            }

            foreach (double d in Data)
            {
                Add(d);
            }
        }

        //public void BuildFit(double xMin, double xMax)
        //{
        //    if (FitSeries == null)
        //    {
        //        FitSeries = new Series(Properties.FitName);
        //        FitSeries.ChartType = SeriesChartType.Line;
        //    }
        //    else
        //    {
        //        FitSeries.Points.Clear();
        //    }

        //    if (Distribution == null) return;

        //    for (double i = xMin - Container.AxisXInterval; i <= xMax; i += (Container.AxisXInterval / 100))
        //    {
        //        DataPoint dataPoint = new DataPoint();
        //        dataPoint.XValue = i + Container.AxisXInterval / 2;
        //        double y = Container.AxisXInterval * Data.Count * Distribution.ProbabilityDensity(i);
        //        if (double.IsInfinity(y)) continue;
        //        dataPoint.YValues[0] = y;
        //        FitSeries.Points.Add(dataPoint);
        //    }

        //    if (Container.Series.FindByName(FitSeries.Name) == null)
        //    {
        //        Container.Series.Add(FitSeries);
        //    }
        //}

        private void Properties_ValueChanged(object sender, HistogramEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(sender, new HistogramEventArgs(this));
            }
        }

        private void Properties_StructureChanged(object sender, HistogramEventArgs e)
        {
            if (StructureChanged != null)
            {
                StructureChanged.Invoke(sender, new HistogramEventArgs(this));
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Data", Data);
            info.AddValue("DataSeries", DataSeries);
            //info.AddValue("FitSeries", FitSeries);
            info.AddValue("IsChronic", IsChronic);
            info.AddValue("Properties", Properties);
            info.AddValue("Top", Top);
            //info.AddValue("FitAnnotation", FitAnnotation);
            //info.AddValue("Distribution", Distribution);
        }
    }

    public class HistogramBin
    {
        public HistogramBin(double start, double end)
        {
            Start = start;
            End = end;
        }

        public HistogramBin(double start, double end, int frequency)
        {
            Start = start;
            End = end;
            Frequency = frequency;
        }

        public int Frequency
        {
            set;
            get;
        }

        public double Start
        {
            set;
            get;
        }

        public double End
        {
            set;
            get;
        }

        public double X
        {
            get
            {
                return Start + (End - Start) / 2D;
            }
        }

        public void Increment()
        {
            Frequency++;
        }

        public void Deincrement()
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
