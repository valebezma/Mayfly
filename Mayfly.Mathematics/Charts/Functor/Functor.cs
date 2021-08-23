using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
using Cursor = System.Windows.Forms.DataVisualization.Charting.Cursor;
using Meta.Numerics;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Charts
{
    public class Functor
    {
        public string Name { get; private set; }

        public bool TransposeCharting { get; set; }

        public Plot Container { set; get; }



        public Series Series { get; set; }

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
            }
        }

        public Axis AxisY
        {
            get { return Series.YAxisType == AxisType.Primary ? ChartArea.AxisY : ChartArea.AxisY2; }
        }

        public Axis AxisX
        {
            get { return Series.XAxisType == AxisType.Primary ? ChartArea.AxisX : ChartArea.AxisX2; }
        }

        public StripLine ArgumentStripLine { get; set; }

        public StripLine FunctionStripLine { get; set; }

        public bool IsCursorArgumentDragging;

        public bool IsCursorFunctionDragging;



        internal Func<double, double> Function { get; set; }

        internal Func<double, double> FunctionInverse { get; set; }



        public FunctorProperties Properties { get; set; }

        public event EventHandler Updated;

        private bool disposed;

        internal int splineStep = 500;



        public Functor(string name, Func<double, double> f)
        {
            Name = name;
            Function = f;

            Properties = new FunctorProperties(this);
            Properties.ValueChanged += new EventHandler(Update);

            this.Name = Properties.FunctionName = name;
            this.BuildSeries();
        }

        public Functor(string name, Func<double, double> f, Func<double, double> invf)
            : this(name, f)
        {
            FunctionInverse = invf;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~Functor()
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
                        if (!Container.IsDisposed && Series != null)
                        {
                            if (ArgumentStripLine != null)
                            {
                                Container.ChartAreas[Series.ChartArea].AxisX.StripLines.Remove(ArgumentStripLine);
                                ArgumentStripLine.Dispose();
                            }

                            Container.Series.Remove(Series);
                            Series.Dispose();

                            if (FunctionStripLine != null)
                            {
                                Container.ChartAreas[Series.ChartArea].AxisX.StripLines.Remove(FunctionStripLine);
                                FunctionStripLine.Dispose();
                            }

                            //if (Annotation != null)
                            //{
                            //    Container.Annotations.Remove(Annotation);
                            //    Annotation.Dispose();
                            //}

                            //if (CursorAnnotation != null)
                            //{
                            //    Container.Annotations.Remove(CursorAnnotation);
                            //    CursorAnnotation.Dispose();
                            //}
                        }

                        Container.Functors.Remove(this);
                    }

                    Properties.Dispose();
                    Properties = null;
                }

                disposed = true;
            }
        }



        public double Predict(double x)
        {
            return Function.Invoke(x);
        }

        public double PredictInversed(double x)
        {
            if (FunctionInverse == null) return double.NaN;
            else return FunctionInverse.Invoke(x);            
        }

        public void BuildSeries(AxisType axisType)
        {
            if (Series == null)
            {
                Series = new Series(this.Name);
                Series.ChartType = SeriesChartType.Line;
            }
            else
            {
                Series.Points.Clear();
            }

            Series.YAxisType = axisType;

            if (Container == null)
            {
                BuildSeries(0, 1, 0, 1);
            }
            else
            {
                BuildSeries(
                    AxisX.Minimum, AxisX.Maximum,
                    AxisY.Minimum, AxisY.Maximum);
            }
        }

        public void BuildSeries(double xMin, double xMax, double yMin, double yMax)
        {
            double xInterval = (xMax - xMin) / splineStep;
            double yInterval = (yMax - yMin) / splineStep;

            if (TransposeCharting)
            {
                for (double x = yMin - 5 * yInterval; x <= yMax + 5 * yInterval; x += yInterval)
                {
                    double y = Predict(x);
                    if (double.IsInfinity(y)) continue;
                    if (double.IsNaN(y)) continue;

                    if (x > yMin - 5 * yInterval && x < yMax + 5 * yInterval &&
                        y > xMin - 5 * xInterval && y < xMax + 5 * xInterval)
                    {
                        DataPoint dataPoint = new DataPoint(Series);
                        dataPoint.XValue = y;
                        dataPoint.YValues[0] = x;
                        Series.Points.Add(dataPoint);
                    }
                }
            }
            else
            {
                for (double x = xMin - 5 * xInterval; x <= xMax + 5 * xInterval; x += xInterval)
                {
                    double y = Predict(x);
                    if (double.IsInfinity(y)) continue;
                    if (double.IsNaN(y)) continue;

                    if (y > yMin - 5 * yInterval && y < yMax + 5 * yInterval &&
                        x > xMin - 5 * xInterval && x < xMax + 5 * xInterval)
                    {
                        DataPoint dataPoint = new DataPoint(Series);
                        dataPoint.XValue = x;
                        dataPoint.YValues[0] = y;
                        Series.Points.Add(dataPoint);
                    }
                }
            }
        }

        public void BuildSeries()
        {
            BuildSeries(AxisType.Primary);
        }

        public void Update(object sender, EventArgs e)
        {
            if (Container == null) return;

            BuildSeries();

            this.Name = Properties.FunctionName;
            Series.Name = Properties.FunctionName;

            Series.BorderWidth = Properties.TrendWidth;

            if (Container.IsDistinguishingMode)
            {
                Series.Color = Constants.MotiveColor;
            }
            else
            {
                Series.Color = Properties.TrendColor;
            }
            
            if (Properties.AllowCursors)
            {
                ShowCursors();
            }
            else
            {
                HideCursors();
            }

            if (Updated != null)
            {
                Updated.Invoke(this, new EventArgs());
            }
        }


        internal void HideCursors()
        {
            if (ArgumentStripLine != null)
            {
                AxisX.StripLines.Remove(ArgumentStripLine);
                ArgumentStripLine = null;
            }

            if (FunctionStripLine != null)
            {
                AxisY.StripLines.Remove(FunctionStripLine);
                FunctionStripLine = null;
            }
        }

        public void ShowCursors()
        {
            if (ArgumentStripLine == null)
            {
                double center = Container.AxisXMin + (Container.AxisXMax - Container.AxisXMin) / 2;

                ArgumentStripLine = AxisX.AddStripLine(center, 
                    center.ToString(Container.AxisXFormat), Series.Color);

                FunctionStripLine = AxisY.AddStripLine(Predict(center), 
                    Function.Invoke(center).ToString(Container.AxisYFormat), Series.Color);
            }

            //ResetByArgumentCursor();
        }


        public bool IsMouseAboveArgument(MouseEventArgs e)
        {
            if (Properties != null && Properties.AllowCursors 
                && ArgumentStripLine != null && Function != null)
            {
                double mouseLocationMin = 0;
                double mouseLocationMax = 0;

                try
                {
                    mouseLocationMin = AxisX.PixelPositionToValue((double)e.X - 10D);
                    mouseLocationMax = AxisX.PixelPositionToValue((double)e.X + 10D);
                }
                catch { }

                if (ArgumentStripLine.IntervalOffset < mouseLocationMax && ArgumentStripLine.IntervalOffset > mouseLocationMin)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsMouseAboveFunction(MouseEventArgs e)
        {
            if (Properties != null && Properties.AllowCursors 
                && FunctionStripLine != null && FunctionInverse != null)
            {
                double mouseLocationMin = 0;
                double mouseLocationMax = 0;

                try
                {
                    mouseLocationMin = AxisY.PixelPositionToValue((double)e.Y + 10D);
                    mouseLocationMax = AxisY.PixelPositionToValue((double)e.Y - 10D);
                }
                catch { }

                if (FunctionStripLine.IntervalOffset < mouseLocationMax && FunctionStripLine.IntervalOffset > mouseLocationMin)
                {
                    return true;
                }
            }

            return false;
        }


        public void ResetByFunctionCursor()
        {
            FunctionStripLine.Text = FunctionStripLine.IntervalOffset.ToString(AxisY.LabelStyle.Format);

            double x = PredictInversed(FunctionStripLine.IntervalOffset);
            ArgumentStripLine.IntervalOffset = x;
            ArgumentStripLine.Text = ArgumentStripLine.IntervalOffset.ToString(AxisX.LabelStyle.Format);
        }

        public void ResetByArgumentCursor()
        {
            ArgumentStripLine.Text = ArgumentStripLine.IntervalOffset.ToString(AxisX.LabelStyle.Format);

            double y = Predict(ArgumentStripLine.IntervalOffset);
            FunctionStripLine.IntervalOffset = y;
            FunctionStripLine.Text = FunctionStripLine.IntervalOffset.ToString(AxisY.LabelStyle.Format);
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (IsCursorArgumentDragging)
            {
                double x = AxisX.PixelPositionToValue(e.Location.X);
                double pace = AxisX.Interval / (Control.ModifierKeys.HasFlag(Keys.Control) ? 10 : 100);
                x = Service.GetStrate(x, pace).LeftEndpoint;
                x = Math.Min(x, AxisX.Maximum);
                x = Math.Max(x, AxisX.Minimum);

                ArgumentStripLine.IntervalOffset = x;
                ResetByArgumentCursor();
            }
            else if (IsCursorFunctionDragging)
            {
                double y = AxisY.PixelPositionToValue(e.Location.Y);
                double pace = AxisY.Interval / (Control.ModifierKeys.HasFlag(Keys.Control) ? 10 : 100);
                y = Service.GetStrate(y, pace).LeftEndpoint;
                y = Math.Min(y, AxisY.Maximum);
                y = Math.Max(y, AxisY.Minimum);

                FunctionStripLine.IntervalOffset = y;
                ResetByFunctionCursor();
            }
            else
            {
                if (IsMouseAboveArgument(e))
                {
                    Container.Cursor = Cursors.SizeWE;
                }
                else if (IsMouseAboveFunction(e))
                {
                    Container.Cursor = Cursors.SizeNS;
                }
                else
                {
                    Container.Cursor = Cursors.Default;
                }
            }
        }
    }
}