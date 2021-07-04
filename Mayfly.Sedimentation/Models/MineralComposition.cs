using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Mayfly.Sedimentation
{
    public class MineralComposition : List<MineralFraction>
    {
        public double NaturalDensity
        {
            get;
            set;
        }

        public double ModestGrainSize
        {
            get
            {
                double sum = 0;
                double result = 0;

                foreach (MineralFraction fract in this)
                {
                    sum += fract.Value;
                    result += fract.Value * fract.GrainSize;
                }

                return sum == 0 ? double.NaN : result / sum;
            }
        }

        public double Total
        {
            get
            {
                double sum = 0;

                foreach (MineralFraction fract in this)
                {
                    sum += fract.Value;
                }

                return sum;
            }
        }



        public MineralComposition() : base() { }

        public MineralComposition(int capacity) : base(capacity) { }



        public double GetSedimentDensity()
        {
            double DesintegrationCoefficient = 1;
            return BestFit.NaturalDensity * DesintegrationCoefficient;
        }

        public double GetVolume()
        {
            return this.Total / this.GetSedimentDensity();
        }

        public void Add(double min, double max)
        {
            Add(new MineralFraction(min, max));
        }

        public void Add(double min, double max, double separate)
        {
            Add(new MineralFraction(min, max, separate));
        }

        public MineralFraction Find(double size)
        {
            foreach (MineralFraction separate in this)
            {
                if (separate.GrainSize == size)
                {
                    return separate;
                }
            }

            return null;
        }

        public double Separate(MineralFraction fraction)
        {
            double sum = 0;

            foreach (MineralFraction fract in this)
            {
                sum += fract.Value;
            }

            return fraction.Value / sum;
        }



        public MineralComposition Copy()
        {
            MineralComposition result = new MineralComposition();
            for (int i = 0; i < Count; i++)
            {
                result.Add(this[i].Copy());
            }
            return result;
        }



        public override string ToString()
        {
            return string.Format("{0}: {1:N3} — {2:N3}", Count, this[0].MinGrainSize, this.Last().MaxGrainSize);
        }



        public MineralComposition BestFit
        {
            get
            {
                MineralComposition result = new MineralComposition();
                double fit = 100;

                foreach (MineralComposition variant in new MineralComposition[] { 
                    FineSilt, Silt, SandySilt, FineSiltySand, FineAndModestSiltySand,
                    ModestSiltySand, FineSand, ModestSand, ModestAndCoarseSand, 
                    GravelSand, Gravel, GravelCobble })
                {
                    double currentFit = Math.Abs(ModestGrainSize - variant.ModestGrainSize);
                    if (currentFit < fit)
                    {
                        result = variant;
                        fit = currentFit;
                    }
                }

                return result;
            }
        }

        public static MineralComposition FineSilt
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.0001, .001, .1);
                result.Add(.001, .005, .4);
                result.Add(.005, .01, .4);
                result.Add(.01, .05, .1);
                result.NaturalDensity = .75;
                return result;
            }
        }

        public static MineralComposition Silt
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.001, .01, .7);
                result.Add(.01, .1, .3);
                result.NaturalDensity = .85;
                return result;
            }
        }

        public static MineralComposition SandySilt
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.001, .005, .15);
                result.Add(.005, .05, .7);
                result.Add(.05, .2, .15);
                result.NaturalDensity = 1;
                return result;
            }
        }

        public static MineralComposition FineSiltySand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.005, .01, .15);
                result.Add(.01, .1, .55);
                result.Add(.1, .2, .3);
                result.NaturalDensity = 1.15;
                return result;
            }
        }

        public static MineralComposition FineAndModestSiltySand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.005, .05, .15);
                result.Add(.05, .2, .55);
                result.Add(.2, .5, .3);
                result.NaturalDensity = 1.25;
                return result;
            }
        }

        public static MineralComposition ModestSiltySand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.005, .01, .15);
                result.Add(.01, .1, .3);
                result.Add(.1, .5, .55);
                result.NaturalDensity = 1.4;
                return result;
            }
        }

        public static MineralComposition FineSand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.01, .05, .3);
                result.Add(.05, .2, .55);
                result.Add(.2, .5, .15);
                result.NaturalDensity = 1.55;
                return result;
            }
        }

        public static MineralComposition ModestSand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.05, .1, .15);
                result.Add(.1, .2, .3);
                result.Add(.2, .5, .55);
                result.NaturalDensity = 1.65;
                return result;
            }
        }

        public static MineralComposition ModestAndCoarseSand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.05, .2, .3);
                result.Add(.2, 1, .55);
                result.Add(1, 2, .15);
                result.NaturalDensity = 1.7;
                return result;
            }
        }

        public static MineralComposition GravelSand
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.1, 1, .7);
                result.Add(1, 10, .3);
                result.NaturalDensity = 1.8;
                return result;
            }
        }

        public static MineralComposition Gravel
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.5, 1, .15);
                result.Add(1, 10, .55);
                result.Add(10, 20, .15);
                result.NaturalDensity = 1.95;
                return result;
            }
        }

        public static MineralComposition GravelCobble
        {
            get
            {
                MineralComposition result = new MineralComposition();
                result.Add(.5, 1, .15);
                result.Add(1, 10, .3);
                result.Add(10, 100, .55);
                result.NaturalDensity = 2.1;
                return result;
            }
        }
    }
}
