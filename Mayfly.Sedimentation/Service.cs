using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Mayfly.Extensions;

namespace Mayfly.Sedimentation
{
    public static class Service
    {
        /// <summary>
        /// Calculates hydraulic size of particles, in mm / sec
        /// </summary>
        /// <param name="size">Size of particles, in mm</param>
        /// <param name="temperature">Water tempereature, in Celcius</param>
        /// <returns>Returns hydraulic size, in mm / sec</returns>
        public static double HydraulicSize(double size, double temperature)
        {
            if (size < 0.1)
            {
                return LaminarHydraulicSize(size, temperature);
            }
            else
            {
                if (size < (2 / UserSettings.SolidShape))
                {
                    return TransitionalHydraulicSize(size, temperature);
                }
                else
                {
                    return TurbulentHydraulicSize(size);
                }
            }
        }

        /// <summary>
        /// Calculates hydraulic size of particles, in mm / sec
        /// </summary>
        /// <param name="size">Size of particles, in mm</param>
        /// <returns>Returns hydraulic size, in mm / sec</returns>
        public static double HydraulicSize(double size)
        {
            if (size < 0.1)
            {
                return LaminarHydraulicSize(size);
            }
            else
            {
                if (size < (2 / UserSettings.SolidShape))
                {
                    return TransitionalHydraulicSize(size);
                }
                else
                {
                    return TurbulentHydraulicSize(size);
                }
            }
        }

        public static double TurbulentHydraulicSize(double size)
        {
            return (2.4 * UserSettings.SolidShape - 0.7) * 
                Math.Sqrt(UserSettings.Gravity * UserSettings.DensityValue * size * 1000);
        }

        public static double TransitionalHydraulicSize(double size)
        {
            return TransitionalHydraulicSize(size, 15);
        }

        public static double TransitionalHydraulicSize(double size, double t)
        {
            return (1.6 * UserSettings.SolidShape - 0.16) *
                (68 * size - 0.003) * UserSettings.DensityValue *
                TemperatureCorrectionFactor(size, t);
        }

        public static double TemperatureCorrectionFactor(double size, double t)
        { 
            double kt = 1;

            if (size < 2)
            {
                double a = -.0124 * t + .1842;
                double b = .0248 * t + .634;
                kt = a * size + b;
            }

            return kt;
        }

        public static double LaminarHydraulicSize(double size)
        {
            return .22 * UserSettings.Gravity *
                size * size * UserSettings.DensityValue / (4 * WaterViscosity());
        }

        public static double LaminarHydraulicSize(double size, double t)
        {
            return .22 * UserSettings.Gravity *
                size * size * UserSettings.DensityValue / (4 * WaterViscosity(t));
        }

        public static double WaterViscosity()
        {
            return WaterViscosity(15);
        }

        public static double WaterViscosity(double t)
        {
            return 0.00002414 * Math.Pow(10, 247.8 / (t + 273.15 - 140));
        }



        public static double SoilEntrainmentNomogram(double v, double b, double fi, SandSize size)
        {
            // 1 - Calculate f(v), scale from V
            double fv = Math.Sin(v * ((Math.PI / 2) / 1.4));

            // 2 - Calculate f(b), scale from B
            //double fb = .33 + .0136 * Math.Exp(3.9 * b);
            double fb = .0136 * Math.Exp(3.9 * b);

            // 3 - Multiply, e. g. final scale from B and V
            double fvb = fv * (.37 + fb);

            // 4 - Find scale of fi
            double fip = 1 / (1 + Math.Exp(.058 * (fi - 100)));

            // 5 - Find value on fi scale
            double ffi = .2018 + fip * 1.023;

            // 6 - Find resulting value of v, b and fi
            double result = fvb * ffi;

            // 7 - Perform final scale
            switch (size)
            {
                case SandSize.Fine:
                    return .4747 * result * result + .6401 * result + .1989;

                case SandSize.Medium:
                    return .1717 * result * result + .8004 * result + .0855;

                case SandSize.Coarse:
                    return .1285 * result * result + .8359 * result - .0195;

                default: return .5;
            }
        }

        public static double GetX(double x1, double x2, double y1, double y2, double criticalY)
        {
            double deltaX = x2 - x1;
            double deltaY = Math.Abs(y2 - y1);
            return x1 + (deltaX * (Math.Max(y1, y2) - criticalY)) / deltaY;
        }


        
        public static void RefreshAxes(this Chart chart, double xRange)
        {
            //double xRange = double.IsNaN(chart.ChartAreas[0].CursorX.SelectionEnd) ? chart.ChartAreas[0].AxisX.Maximum :
            //    chart.ChartAreas[0].CursorX.SelectionEnd - chart.ChartAreas[0].CursorX.SelectionStart;

            if (xRange == 0) xRange = chart.ChartAreas[0].AxisX.Maximum;

            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                double yRight = chartArea.AxisY.Maximum;

                double xInterval = Mayfly.Service.GetAutoInterval(xRange);
                double yInterval = Mayfly.Service.GetAutoInterval(yRight);

                try
                {
                    double chartHeight = chartArea.AxisY.ValueToPixelPosition(0) - chartArea.AxisY.ValueToPixelPosition(yRight);
                    chartArea.AxisY.AdjustIntervals(chartHeight, yInterval);
                }
                catch
                {
                    chartArea.AxisY.AdjustIntervals(chart.Parent.Height, yInterval);
                }

                try
                {
                    double chartWidth = chartArea.AxisX.ValueToPixelPosition(xRange);
                    chartArea.AxisX.AdjustIntervals(chartWidth, xInterval);
                }
                catch
                {
                    chartArea.AxisX.AdjustIntervals(chart.Parent.Width, xInterval);
                }
            }
        }

    }

    public enum SandSize
    {
        Fine,
        Medium,
        Coarse
    }
}
