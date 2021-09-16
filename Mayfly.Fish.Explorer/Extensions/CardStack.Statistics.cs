using Mayfly.Wild;
using System;
using System.Collections.Generic;
using Meta.Numerics.Statistics;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static double Mesh(this CardStack stack)
        {
            Sample result = new Sample();

            foreach (Data.CardRow cardRow in stack)
            {
                if (!cardRow.IsMeshNull()) result.Add(cardRow.Mesh);
            }

            return result.Mean;
        }

        public static Sample LengthSample(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            List<double> result = new List<double>();

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsLengthNull()) continue;
                    result.Add(individualRow.Length);
                }
            }

            return new Sample(result.ToArray());
        }

        public static Sample LengthSample(this CardStack stack, Data.SpeciesRow speciesRow, Age age)
        {
            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (new Age(individualRow.Age).Years != age.Years) continue;
                result.Add(individualRow.Length);
            }

            return new Sample(result.ToArray());
        }

        public static Sample LengthSample(this CardStack stack, Data.SpeciesRow speciesRow, Sex sex)
        {
            List<double> result = new List<double>();


            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.Sex != sex.Value) continue;
                result.Add(individualRow.Length);
            }

            return new Sample(result.ToArray());
        }

        public static double LengthMin(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            Sample lengths = stack.LengthSample(speciesRow);

            double result = lengths.Count > 0 ? lengths.Minimum : 0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                {
                    if (result > stratifiedRow.SizeClass.RightEndpoint)
                    {
                        result = stratifiedRow.SizeClass.LeftEndpoint;
                    }
                }
            }

            return result;
        }

        public static double LengthMax(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            Sample lengths = stack.LengthSample(speciesRow);

            double result = lengths.Count > 0 ? lengths.Maximum : 50;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                {
                    if (result < stratifiedRow.SizeClass.Midpoint)
                    {
                        result = stratifiedRow.SizeClass.Midpoint;
                    }
                }
            }

            return result;
        }

        public static double LengthMaxOfNonAged(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            double result = double.MinValue;
            int i = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (!individualRow.IsAgeNull()) continue;

                    if (individualRow.IsLengthNull()) continue;

                    if (result < individualRow.Length)
                    {
                        result = individualRow.Length;
                        i++;
                    }

                }

                foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                {
                    if (result < stratifiedRow.SizeClass.Midpoint)
                    {
                        result = stratifiedRow.SizeClass.Midpoint;
                        i++;
                    }
                }
            }

            return (i > 1) ? result : double.NaN;
        }



        public static Age AgeMin(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            return stack.AgeMin(speciesRow, UserSettings.SuggestAge);
        }

        public static Age AgeMin(this CardStack stack, Data.SpeciesRow speciesRow, bool key)
        {
            double a = 50;

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                a = Math.Min(a, individualRow.Age);
            }


            ContinuousBio bio = stack.Parent.FindGrowthModel(speciesRow.Species);

            if (key && bio != null)
            {
                double l = stack.LengthMin(speciesRow);
                double t = bio.GetValue(l, true);
                if (!double.IsNaN(t)) a = Math.Min(a, t);
            }

            return new Age(a);
        }

        public static Age AgeMax(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            return stack.AgeMax(speciesRow, UserSettings.SuggestAge);
        }

        public static Age AgeMax(this CardStack stack, Data.SpeciesRow speciesRow, bool key)
        {
            double a = 0.0;

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                a = Math.Max(a, individualRow.Age);
            }

            ContinuousBio bio = stack.Parent.FindGrowthModel(speciesRow.Species);

            if (key && bio != null)
            {
                double l = stack.LengthMaxOfNonAged(speciesRow);
                double t = bio.GetValue(l, true);
                if (!double.IsNaN(t)) a = Math.Max(a, t);
            }

            return new Age(a);
        }

        //public static double LengthMinStrat(Data.SpeciesRow speciesRow)
        //{
        //    double result = double.MaxValue;
        //    int i = 0;

        //    foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
        //    {
        //        foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
        //        {
        //            if (result > stratifiedRow.SizeClass.Midpoint)
        //            {
        //                result = stratifiedRow.SizeClass.Midpoint;
        //                i++;
        //            }
        //        }
        //    }

        //    if (i > 0)
        //    {
        //        return result;
        //    }
        //    else
        //    {
        //        return double.NaN;
        //    }
        //}

        //public static double LengthMaxStrat(Data.SpeciesRow speciesRow)
        //{
        //    double result = double.MinValue;
        //    int i = 0;

        //    foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
        //    {
        //        foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
        //        {
        //            if (result < stratifiedRow.SizeClass.Midpoint)
        //            {
        //                result = stratifiedRow.SizeClass.Midpoint;
        //                i++;
        //            }

        //        }
        //    }

        //    if (i > 0)
        //    {
        //        return result;
        //    }
        //    else
        //    {
        //        return double.NaN;
        //    }
        //}
    }
}
