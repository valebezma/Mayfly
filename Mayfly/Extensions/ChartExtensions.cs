using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Mayfly.Extensions
{
    public static class ChartExtensions
    {
        public static void AdjustIntervals(this Chart chart)
        {
            AdjustIntervals(chart.ChartAreas[0].AxisX, chart.Width);
            AdjustIntervals(chart.ChartAreas[0].AxisY, chart.Height);
            AdjustIntervals(chart.ChartAreas[0].AxisY2, chart.Height);
        }

        public static void AdjustIntervals(this Axis axis, double pixels)
        {
            AdjustIntervals(axis, pixels, Service.GetAutoInterval(axis.Maximum - axis.Minimum));
        }

        public static void AdjustIntervals(this Axis axis, double pixels, double round)
        {
            double range = axis.Maximum - axis.Minimum;

            if (axis.IntervalType == DateTimeIntervalType.Number || axis.IntervalType == DateTimeIntervalType.Auto)
            {
                double interval = Math.Ceiling(60 * range / pixels / round) * round;

                if (interval != 0)
                {
                    axis.Interval = interval;
                }
                else
                {
                    axis.Interval = round;
                }

                axis.MinorTickMark.Enabled = true;
                axis.MinorTickMark.Interval = round;
                //axis.Maximum = axis.MinorTickMark.Interval * Math.Ceiling(axis.Maximum / axis.MinorTickMark.Interval);
            }
            //else
            //{
            //    switch (axis.IntervalType)
            //    {
            //        case DateTimeIntervalType.Weeks:
            //            axis.IntervalOffsetType =
            //            axis.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Days;
            //            DateTime minDate = DateTime.FromOADate(axis.Minimum);
            //            axis.IntervalOffset =
            //            axis.LabelStyle.IntervalOffset = minDate.DayOfWeek - CultureInfo.InvariantCulture.DateTimeFormat.FirstDayOfWeek;
            //            break;
            //        case DateTimeIntervalType.Months:
            //        case DateTimeIntervalType.Years:
            //            axis.IntervalOffsetType =
            //            axis.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
            //            axis.IntervalOffset = 0;
            //            axis.LabelStyle.IntervalOffset = 0.5;
            //            break;
            //        default:
            //            axis.IntervalOffsetType =
            //            axis.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
            //            axis.IntervalOffset = axis.LabelStyle.IntervalOffset = 0;
            //            break;
            //    }

            //    range *= round;

            //    double interval = Math.Ceiling(60 * range / pixels);
            //    axis.Interval = Math.Max(interval, 1);

            //    switch (axis.IntervalType)
            //    {
            //        case DateTimeIntervalType.Weeks:
            //        case DateTimeIntervalType.Months:
            //        case DateTimeIntervalType.Years:
            //            axis.MinorTickMark.Enabled = false;
            //            break;
            //        default:
            //            axis.MinorTickMark.Enabled = true;
            //            axis.MinorTickMark.Interval = 1 / round;
            //            break;
            //    }

            //    axis.MinorTickMark.Interval = round;
            //}

        }

        public static void Format(this Axis axis)
        {
            axis.MajorGrid.LineColor = Color.Gainsboro;
            axis.MajorTickMark.TickMarkStyle = TickMarkStyle.AcrossAxis;

            axis.MinorTickMark.TickMarkStyle = TickMarkStyle.OutsideArea;
            axis.MinorTickMark.Enabled = true;

            axis.TitleAlignment = StringAlignment.Center;
        }

        public static void Format(this ChartArea area)
        {
            area.IsSameFontSizeForAllAxes = true;
            area.AxisX.Format();
            area.AxisY.Format();
            area.AxisX2.Format();
            area.AxisY2.Format();
        }

        public static void Format(this Chart chart)
        {
            foreach (ChartArea area in chart.ChartAreas)
            {
                area.Format();
            }
        }

        public static StripLine AddStripLine(this Axis axis, double value, string label)
        {
            return axis.AddStripLine(value, label, Constants.MotiveColor);
        }

        public static StripLine AddStripLine(this Axis axis, double value, string label, Color color)
        {
            StripLine strip = new StripLine();

            strip.Interval = 0;
            strip.IntervalOffset = value;
            strip.StripWidth = 0;// (value / 900);

            strip.BorderColor = color;
            strip.BorderDashStyle = ChartDashStyle.Dash;
            strip.BorderWidth = 1;

            strip.Text = label;
            //stripFMSY.TextAlignment = StringAlignment.Near;

            axis.StripLines.Add(strip);

            return strip;
        }

        public static void AnimateChart(this Series series, double fraction, Label label)
        {
            AnimateChart(series, fraction, label, .5);
        }

        public static void AnimateChart(this Series series, double fraction, Label label, double edge)
        {
            if (double.IsNaN(fraction)) return;

            if (series.ChartType != SeriesChartType.Doughnut)
                throw new ArgumentException();

            while (series.Points.Count > 2)
            {
                series.Points.RemoveAt(2);
            }

            double value = series.Points[0].YValues[0];

            Timer timer = new Timer();
            timer.Interval = 1;

            EventHandler timerTicked = (object sender, EventArgs e) =>
            {
                if (fraction > value)
                {
                    if (value + .01 < fraction)
                    {
                        value += .01;
                    }
                    else
                    {
                        value = fraction;
                    }
                }
                else
                {
                    if (value - .01 > fraction)
                    {
                        value -= .01;
                    }
                    else
                    {
                        value = fraction;
                    }
                }

                if (series.Points == null)
                {
                    timer.Enabled = false;
                }
                else
                {
                    series.Points[0].YValues[0] = value;
                    series.Points[1].YValues[0] = 1 - value;
                    series.Points[0].Color = Mayfly.Service.Map(value, edge);
                    series.Points[1].Color = Mayfly.Service.Map(value, edge).Lighter();

                    label.Text = value.ToString("P1");

                    if (value == fraction)
                    {
                        timer.Enabled = false;
                    }
                }
            };

            timer.Tick += timerTicked;
            timer.Enabled = true;
        }
    }
}
