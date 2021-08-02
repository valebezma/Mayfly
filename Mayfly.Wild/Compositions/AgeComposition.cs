using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;

namespace Mayfly.Wild
{
    public class AgeComposition : Composition
    {
        public Age Youngest { get; set; }

        public Age Oldest { get; set; }

        public bool IsRecovered { get; set; }



        public AgeComposition(string name, Age start, Age end)
        {
            if (start > end)
            {
                Mayfly.Notification.ShowNotification(Resources.Interface.Messages.AgeInacceptable, Resources.Interface.Messages.AgeInacceptableInstruction);
                throw new AgeArgumentException("Inacceptable age limits");
            }

            this.Name = name;
            this.Youngest = start;
            this.Oldest = end;

            for (int i = this.Youngest.Years; i <= this.Oldest.Years; i++)
            {
                AgeGroup ageGroup = new AgeGroup((Age)i);
                this.AddCategory(ageGroup);
            }
        }



        public new AgeGroup this[int i]
        {
            get { return (AgeGroup)base[i]; }
            set { base[i] = value; }
        }

        public void AddCategory(AgeGroup item)
        {
            base.AddCategory(item);

            Youngest = (Age)Math.Min(Youngest.Years, item.Age.Years);
            Oldest = (Age)Math.Max(Oldest.Years, item.Age.Years);
        }

        public Histogramma GetHistogram()
        {
            Sample s = new Sample();
            s.Name = this.Name;

            foreach (AgeGroup age in this)
            {
                for (int i = 0; i < age.Quantity; i++)
                {
                    s.Add(age.Age + .5);
                }
            }

            return new Mathematics.Charts.Histogramma(s);
        }



        public double AbundanceFractionUnder(Age age)
        {
            double result = 0;

            foreach (AgeGroup ageGroup in this)
            {
                if (ageGroup.Age < age)
                {
                    result += ageGroup.AbundanceFraction;
                }
            }

            return result;
        }

        public double BiomassFractionUnder(Age age)
        {
            double result = 0;

            foreach (AgeGroup ageGroup in this)
            {
                if (ageGroup.Age < age)
                {
                    result += ageGroup.BiomassFraction;
                }
            }

            return result;
        }


        public double AbundanceFractionStartingFrom(Age age)
        {
            double result = 0;

            foreach (AgeGroup ageGroup in this)
            {
                if (ageGroup.Age >= age)
                {
                    result += ageGroup.AbundanceFraction;
                }
            }

            return result;
        }

        public double BiomassFractionStartingFrom(Age age)
        {
            double result = 0;

            foreach (AgeGroup ageGroup in this)
            {
                if (ageGroup.Age >= age)
                {
                    result += ageGroup.BiomassFraction;
                }
            }

            return result;
        }



        public Scatterplot GetWeightScatterplot(string massAxis)
        {
            BivariateSample sample = new BivariateSample(
                Resources.Reports.Caption.Length,
                massAxis);

            foreach (Category cat in this)
            {
                if (cat.LengthSample.Count == 0) continue;
                if (cat.MassSample.Count == 0) continue;

                double x = cat.LengthSample.Mean;
                double y = cat.MassSample.Mean;

                sample.Add(x, y);
            }

            if (sample.Count == 0) return null;

            Scatterplot result = new Scatterplot(sample, this.Name);
            result.Properties.ShowTrend = true;
            result.Properties.SelectedApproximationType = TrendType.Power;
            return result;
        }
    }

    public class AgeGroup : Category
    {
        public Age Age { get; set; }



        public AgeGroup(string name) : base(name) { }

        public AgeGroup(Age age) : base(age.Group) 
        {
            this.Age = age;
        }



        public override Category GetEmptyCopy()
        {
            return new AgeGroup(this.Age);
        }
    }

    public class AgeArgumentException : ArgumentException
    {
        public AgeArgumentException(string p) 
            : base(p)
        { }
    }
}
