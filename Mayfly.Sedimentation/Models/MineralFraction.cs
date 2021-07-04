using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Mayfly.Sedimentation
{
    public class MineralFraction : IFormattable
    {
        public double MinGrainSize { set; get; }

        public double MaxGrainSize { set; get; }

        public double Value { set; get; }

        public double GrainSize
        {
            get
            {
                switch (UserSettings.SizeSource)
                {
                    case GrainSizeType.Lower: return MinGrainSize;
                    case GrainSizeType.Upper: return MaxGrainSize;
                    default: return (MaxGrainSize + MinGrainSize) / 2d;
                }

            }
        }



        public MineralFraction(double min, double max, double separate)
        {
            MinGrainSize = min;
            MaxGrainSize = max;
            Value = separate;
        }

        public MineralFraction(double min, double max)
        {
            MinGrainSize = min;
            MaxGrainSize = max;
        }



        public MineralFraction Copy()
        {
            return new MineralFraction(MinGrainSize, MaxGrainSize, Value);
        }



        public double GetHydraulicSize(double temperature)
        {
            return Service.HydraulicSize(this.GrainSize, temperature);
        }

        public double GetSedimentationLongitude(double depth, double velocity, double temperature)
        {
            return depth * velocity / (this.GetHydraulicSize(temperature) * .001);

        }



        public override string ToString()
        {
            return ToString("G2");
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return string.Format("{0:" + format + "} — {1:" + format + "}", MinGrainSize, MaxGrainSize);
        }
    }
}
