﻿using Mayfly.Wild;
using System;
using System.Collections.Generic;
using Meta.Numerics.Statistics;
using Meta.Numerics;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static double MassSampled(this CardStack stack)
        {
            double result = 0;

            foreach (Data.SpeciesRow speciesRow in stack.GetSpecies())
            {
                result += stack.MassSampled(speciesRow);
            }

            return result;
        }

        public static double MassSampled(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            double result = 0.0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.MassIndividual();
                result += logRow.MassStratified();
            }

            return result;
        }



        public static double MassIndividual(this Data.LogRow logRow)
        {
            double result = 0;

            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull())
                {
                    if (individualRow.IsLengthNull())
                    {
                        // It is impossible to recover mass
                        return double.NaN;
                    }

                    double mass = ((Data)logRow.CardRow.Table.DataSet).FindMassModel(logRow.SpeciesRow.Species).GetValue(individualRow.Length);

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

        public static double MassIndividual(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            double result = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.MassIndividual();
            }

            return result;
        }

        public static double MassIndividual(this CardStack stack)
        {
            double result = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.MassIndividual();
            }

            return result;
        }



        public static double MassStratified(this Data.LogRow logRow)
        {
            double result = 0;
            var cb = ((Data)logRow.CardRow.Table.DataSet).FindMassModel(logRow.SpeciesRow.Species);
            if (cb == null) return double.NaN;

            foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
            {
                double mass = cb.GetValue(stratifiedRow.SizeClass.Midpoint);
                if (double.IsNaN(mass)) return double.NaN;
                result += mass * stratifiedRow.Count;
            }

            return result / 1000;
        }

        public static double MassStratified(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            double result = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.MassStratified();
            }

            return result;
        }

        public static double MassStratified(this CardStack stack)
        {
            double result = 0;

            foreach (Data.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.MassStratified();
            }

            return result;
        }



        public static Sample MassSample(this CardStack stack, Data.SpeciesRow speciesRow)
        {
            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, Data.SpeciesRow speciesRow, Interval size)
        {
            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, Data.SpeciesRow speciesRow, Age age)
        {
            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                if (individualRow.IsMassNull()) continue;
                if (!age.Contains(individualRow.Age)) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, Data.SpeciesRow speciesRow, Sex sex)
        {
            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                if (individualRow.IsMassNull()) continue;
                if (individualRow.Sex != sex.Value) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }

        public static Sample MassSample(this CardStack stack, Data.SpeciesRow speciesRow, Interval size, Age age)
        {
            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
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
