using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meta.Numerics;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;

namespace Mayfly.Wild
{
    public class LengthComposition : Composition
    {
        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public double Interval { get; set; }

        public double Ceiling { get { return this[this.Count - 1].Size.RightEndpoint; } }



        public LengthComposition(string name, double min, double max, double interval)
        {
            Name = name;
            Minimum = min;
            Maximum = max;
            Interval = interval;

            //for (Interval size = Service.GetStrate(min, interval); size.LeftEndpoint <= max; size.Shift())
            for (double l = Mathematics.Service.GetStrate(min, interval).LeftEndpoint; l <= max; l += interval)
            {
                Interval size = Meta.Numerics.Interval.FromEndpointAndWidth(l, interval);
                SizeClass category = new SizeClass(size);
                AddCategory(category);
            }
        }



        public new SizeClass this[int i]
        {
            get { return (SizeClass)base[i]; }
            set { base[i] = value; }
        }

        public void AddCategory(SizeClass item)
        {
            base.AddCategory(item);

            Minimum = Math.Min(Minimum, item.Size.LeftEndpoint);
            Maximum = Math.Max(Maximum, item.Size.LeftEndpoint);
        }

        public Sample GetHistogramSample()
        {
            Sample s = new Sample();
            s.Name = this.Name;

            foreach (SizeClass size in this)
            {
                for (int i = 0; i < size.Quantity; i++){
                    s.Add(size.Size.Midpoint);
                }
            }

            return s;
        }

        public Histogramma GetHistogram()
        {
            return new Histogramma(GetHistogramSample());
        }

        public static LengthComposition Get(double interval, double min, double max, Func<Interval, int> counter, string name)
        {
            if (max < min)
                throw new AgeArgumentException("Wrong length limits");

            LengthComposition result = new LengthComposition(name,
                min, max, interval);

            foreach (SizeClass group in result)
            {
                group.Quantity = counter.Invoke(group.Size);
            }

            return result;
        }
    }

    public class SizeClass : Category
    {
        public Interval Size { get; set; }



        public SizeClass(Interval size)
        {
            Size = size;
            Name = string.Format("{0}—{1}", size.LeftEndpoint, size.RightEndpoint);
        }



        public override Category GetEmptyCopy()
        {
            return new SizeClass(this.Size);
        }
    }
}
