using Mayfly.Wild;
using System;
using System.Collections.Generic;
using Meta.Numerics.Statistics;
using Meta.Numerics;
using Mayfly.Extensions;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static double MassSampled(this CardStack stack)
        {
            double result = 0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                result += stack.MassSampled(speciesRow);
            }

            return result;
        }

        public static double MassSampled(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0.0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.MassIndividual();
                result += logRow.MassStratified();
            }

            return result;
        }



        public static double MassIndividual(this Wild.Survey.LogRow logRow)
        {
            double result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull())
                {
                    if (individualRow.IsLengthNull())
                    {
                        // It is impossible to recover mass
                        return double.NaN;
                    }

                    double mass = ((Wild.Survey)logRow.CardRow.Table.DataSet).FindMassModel(logRow.DefinitionRow.Taxon).GetValue(individualRow.Length);

                    if (double.IsNaN(mass))
                    {
                        // Mass are not recoverable
                        return double.NaN;
                    }
                    else
                    {
                        result += mass;
                    }
                }
                else
                {
                    result += individualRow.Mass;
                }
            }

            return result / 1000;
        }

        public static double MassIndividual(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.MassIndividual();
            }

            return result;
        }

        public static double MassIndividual(this CardStack stack)
        {
            double result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.MassIndividual();
            }

            return result;
        }



        public static double MassStratified(this Wild.Survey.LogRow logRow)
        {
            double result = 0;
            var cb = ((Wild.Survey)logRow.CardRow.Table.DataSet).FindMassModel(logRow.DefinitionRow.Taxon);
            if (cb == null) return double.NaN;

            foreach (Wild.Survey.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
            {
                double mass = cb.GetValue(stratifiedRow.SizeClass.Midpoint);
                if (double.IsNaN(mass)) return double.NaN;
                result += mass * stratifiedRow.Count;
            }

            return result / 1000;
        }

        public static double MassStratified(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            double result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.MassStratified();
            }

            return result;
        }

        public static double MassStratified(this CardStack stack)
        {
            double result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.MassStratified();
            }

            return result;
        }



        public static Sample MassSample(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            List<double> result = new List<double>();

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            List<double> result = new List<double>();

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Age age)
        {
            List<double> result = new List<double>();

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                if (individualRow.IsMassNull()) continue;
                if (!age.Contains(individualRow.Age)) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Sex sex)
        {
            List<double> result = new List<double>();

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                if (individualRow.IsMassNull()) continue;
                if (individualRow.Sex != sex.Value) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size, Age age)
        {
            List<double> result = new List<double>();

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsAgeNull()) continue;
                if (individualRow.IsMassNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                if (!age.Contains(individualRow.Age)) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }
    }
}
