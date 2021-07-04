using System;
using System.Globalization;

namespace Mayfly.Sedimentation
{
    public class ModelStretch : IFormattable
    {
        public string Name { get { return string.Format("{0:N1} — {1:N1}", this.Start, this.End); } }



        public double Start { get; set; }

        public double End { get; set; }

        public double Longitude { get { return Math.Abs(End - Start); } }

        public double Midpoint { get { return Start + .5 * Longitude; } }

        public double Depth { get; set; }

        public double Velocity { get; set; }



        public double Square { get; private set; }



        public MineralComposition Sediments { get; private set; }

        public double Weight { get { return Sediments == null ? 0 : Sediments.Total; } }

        public double TransitWeight { get; private set; }

        public double StartingAdditionalLoad { get; internal set; }

        public double StartingFullLoad { get { return StartingAdditionalLoad + (Project.IsLoadNaturalNull() ? 0 : Project.LoadNatural); } }

        public double FinalAdditionalLoad { get; internal set; }

        public double FinalFullLoad { get { return FinalAdditionalLoad + (Project.IsLoadNaturalNull() ? 0 : Project.LoadNatural); } }

        public double SedimentsDensity { get { return Sediments == null ? 0 : Sediments.GetSedimentDensity(); } }

        public double SedimentsVolume { get { return Sediments == null ? 0 : Sediments.GetVolume(); } }

        public double SedimentsMeanWidth { get { return 1000 * SedimentsVolume / Square; } }

        public double WaterVolume { get { return Square * Project.Depth; } }

        public double TransitWaterVolume { get; private set; }


        private SedimentProject.ProjectRow Project { get; set; }



        //public ModelStretch(double start, double end, SedimentProject.ProjectRow prjRow)
        //{
        //    this.Project = prjRow;

        //    this.Start = start;
        //    this.End = end;
        //    this.TransitWaterVolume = Project.WaterSpend * Project.DurationSeconds;

        //    double B = this.Project.IsWidthNull() ? double.NaN : this.Project.Width;
        //    double b = this.Project.IsWorkWidthNull() ? double.NaN : this.Project.WorkWidth;
        //    double l = Longitude;
        //    double s1 = l * (double.IsNaN(b) ? B : b);

        //    if (this.Project.IsLateralFlowNull()) { this.Square = s1; }
        //    else
        //    {
        //        //double a = this.Project.WorkLongitude;
        //        double alpha = (this.Project.LateralFlow / 360.0);
        //        double leftadded = Math.Min(start * Math.Tan(alpha), this.Project.LeftGap);
        //        double rightadded = Math.Min(start * Math.Tan(alpha), this.Project.RightGap);
        //        b += leftadded + rightadded;
        //        s1 = l * b;

        //        double hmax = l * Math.Sin(alpha);
        //        //double ls = a * Math.Min(hmax, this.Project.LeftGap);
        //        //double rs = a * Math.Min(hmax, this.Project.RightGap);

        //        double x = l * Math.Tan(alpha); // Math.Sin(alpha) * l;
        //        //double leftaddeds = l * Math.Min(x, this.Project.LeftGap - leftadded);
        //        //double rightaddeds = l * Math.Min(x, this.Project.RightGap - rightadded);
        //        double s2 = l * x; //Math.PI * a * a * (alpha / 360.0);

        //        if ((x + leftadded) > this.Project.LeftGap)
        //        {
        //            //double r2 = (this.Project.LeftGap - x) / Math.Sin(alpha);
        //            double leftextra = x + leftadded - this.Project.LeftGap;
        //            double s3 = .5 * leftextra * (leftextra * Math.Tan(alpha));// (Math.PI * r2 * r2 * (alpha / 360.0)) / 2;
        //            s2 -= s3;
        //        }

        //        if ((x + rightadded) > this.Project.RightGap)
        //        {
        //            //double r2 = (this.Project.RightGap - x) / Math.Sin(alpha);
        //            double rightextra = x + rightadded - this.Project.RightGap;
        //            double s4 = .5 * rightextra * (rightextra * Math.Tan(alpha)); //(Math.PI * r2 * r2 * (alpha / 360.0)) / 2;
        //            s2 -= s4;
        //        }

        //        this.Square = s1 + s2;// + ls + rs;
        //    }

        //    this.Depth = this.Project.IsDepthNull() ? double.NaN : this.Project.Depth;
        //}

        public ModelStretch(double start, double end, SedimentProject.ProjectRow prjRow)
        {
            this.Project = prjRow;

            this.Start = start;
            this.End = end;
            this.TransitWaterVolume = Project.WaterSpend * Project.DurationSeconds;

            double B = this.Project.IsWidthNull() ? double.NaN : this.Project.Width;
            double b = this.Project.IsWorkWidthNull() ? double.NaN : this.Project.WorkWidth;
            double l = Longitude;

            if (this.Project.IsLateralFlowNull()) { this.Square = l * (double.IsNaN(b) ? B : b); }
            else { this.Square = GetArea(b, start, l, this.Project.LateralFlow / 360.0, this.Project.IsLeftGapNull() ? 0 : this.Project.LeftGap, this.Project.RightGap); }

            this.Depth = this.Project.IsDepthNull() ? double.NaN : this.Project.Depth;
        }

        public ModelStretch(SedimentProject.ZoneRow row) : this(row.From, row.To, row.ProjectRow) { }

        public ModelStretch(SedimentProject.SectionRow row) : this(0, row.Distance, row.ProjectRow)
        {
            //this.Project = row.ProjectRow;

            //this.Start = 0;
            //this.End = row.Distance;
            //this.Square = Longitude * (this.Project.IsWidthNull() ? double.NaN : this.Project.Width);
            //this.TransitWaterVolume = Project.WaterSpend * Project.DurationSeconds;

            this.Velocity = row.IsVelocityNull() ? (Project.IsVelocityNull() ? double.NaN : Project.Velocity) : row.Velocity;
            this.Depth = row.IsDepthNull() ? (Project.IsDepthNull() ? double.NaN : Project.Depth) : row.Depth;
        }




        public void ProcessSedimentation()
        {
            this.Sediments = this.Project.GetSedimentsComposition(Start, End);
            this.TransitWeight = this.Project.WeightFlushed - this.Project.GetSedimentsComposition(0, End).Total;

            this.StartingAdditionalLoad = ModelGgi.GetLoad(this.TransitWeight + this.Weight, this.Project);
            this.FinalAdditionalLoad = ModelGgi.GetLoad(this.TransitWeight, this.Project);
        }

        public static double GetArea(double w, double start, double l, double alpha, double marginleft, double marginright)
        {
            double s1 = w * l;

            double bigpizzapie = 2 * GetPizzaPieArea(start + l, alpha);
            bigpizzapie -= GetPizzaPieArea(start + l, alpha, marginleft);
            bigpizzapie -= GetPizzaPieArea(start + l, alpha, marginright);

            double alreadypizzapie = 2 * GetPizzaPieArea(start, alpha);
            alreadypizzapie -= GetPizzaPieArea(start, alpha, marginleft);
            alreadypizzapie -= GetPizzaPieArea(start, alpha, marginright);

            double s2 = bigpizzapie - alreadypizzapie;

            return s1 + s2;
        }

        public static double GetPizzaPieArea(double radius, double radians)
        {
            return radius * radius * radians;
        }

        public static double GetPizzaPieArea(double radius, double radians, double margin)
        {
            double s = 0;
            double h = radius * Math.Sin(radians);
            if (h > margin) {
                double r = (h - margin) / Math.Sin(radians);
                s = GetPizzaPieArea(r, radians);
            }
            return s;
        }




        public override string ToString()
        {
            return ToString("N1");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0:" + format + "} — {1:" + format + "} m: ({2:" + format + "} — {3:" + format + "} г/м3).", Start, End, StartingFullLoad, FinalFullLoad);
        }
    }
}