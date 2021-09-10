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

        internal int splineStep = 500;



        public Functor(string name, Func<double, double> f)
        {
            Name = name;
            Properties = new FunctorProperties(this);
            Function = f;
        }

        public Functor(string name, Func<double, double> f, Func<double, double> invf)
            : this(name, f)
        {
            FunctionInverse = invf;
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

        public void UpdateDataPoints(double xMin, double xMax, double yMin, double yMax)
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
                        Series.Points.Add(new DataPoint(y, x));
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
                        Series.Points.Add(new DataPoint(x, y));
                    }
                }
            }
        }

        public void Update(AxisType axisType)
        {
            if (Container == null) return;

            if (Series == null)
            {
                Series = new Series(Properties.FunctionName)
                {
                    ChartType = SeriesChartType.Line
                };

                Container.Series.Add(Series);
            }
            else
            {
                Series.Points.Clear();
            }

            this.Name = Properties.FunctionName;
            Series.Name = Properties.FunctionName;
            Series.YAxisType = axisType;
            Series.BorderWidth = Properties.TrendWidth;
            Series.Color = Container.IsDistinguishingMode ? UserSettings.ColorSelected : Properties.TrendColor;
            UpdateDataPoints(AxisX.Minimum, AxisX.Maximum, AxisY.Minimum, AxisY.Maximum);



            if (Properties.AllowCursors)
            {
                if (ArgumentStripLine == null)
                {
                    double center = Container.AxisXMin + (Container.AxisXMax - Container.AxisXMin) / 2;

                    ArgumentStripLine = AxisX.AddStripLine(center,
                        center.ToString(Container.AxisXFormat), Series.Color);

                    FunctionStripLine = AxisY.AddStripLine(Predict(center),
                        Function.Invoke(center).ToString(Container.AxisYFormat), Series.Color);
                }
            }
            else
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

            if (Updated != null)
            {
                Updated.Invoke(this, new EventArgs());
            }
        }

        public void Update()
        {
            if (Series != null) { Update(Series.YAxisType); }
            else { Update(AxisType.Primary); }
        }

        public void OpenTrendProperties()
        {
            if (!Properties.Visible)
            {
                Properties.SetFriendlyDesktopLocation(Container.FindForm(), FormLocation.NextToHost);
                Properties.Show();
            }
            else
            {
                Properties.BringToFront();
            }
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