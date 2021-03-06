using Mayfly.Wild;
using System;
using System.Data;
using Meta.Numerics;
using Mayfly.Extensions;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static SampleSizeDescriptor GetDescriptor(this CardStack cs)
        {
            return new SampleSizeDescriptor()
            {
                Name = cs.Name,
                Quantity = cs.Quantity(),
                QuantityStratified = cs.QuantityStratified(),
                QuantityIndividual = cs.QuantityIndividual(),
                Measured = cs.Measured(),
                Weighted = cs.Weighted(),
                Tallied = cs.Tallied(),
                Aged = cs.Aged(),
                Sexed = cs.Sexed(),
                Matured = cs.Matured(),
                Mass = cs.Mass(),
                MassStratified = cs.MassStratified(),
                MassIndividual = cs.MassIndividual()
            };
        }
    
        public static SampleSizeDescriptor GetDescriptor(this CardStack cs, TaxonomicIndex.TaxonRow speciesRow)
        {
            return new SampleSizeDescriptor()
            {
                Name = speciesRow.ToString("s"),
                Quantity = cs.Quantity(speciesRow),
                QuantityStratified = cs.QuantityStratified(speciesRow),
                QuantityIndividual = cs.QuantityIndividual(speciesRow),
                Measured = cs.Measured(speciesRow),
                Weighted = cs.Weighted(speciesRow),
                Tallied = cs.Tallied(speciesRow),
                Aged = cs.Aged(speciesRow),
                Sexed = cs.Sexed(speciesRow),
                Matured = cs.Matured(speciesRow),
                Mass = cs.Mass(speciesRow),
                MassStratified = cs.MassStratified(speciesRow),
                MassIndividual = cs.MassIndividual(speciesRow)
            };
        }



        public static int QuantitySampled(this CardStack stack)
        {
           int result = 0;

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                result += stack.QuantitySampled(speciesRow);
            }

            return result;
        }

        public static int QuantitySampled(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.QuantitySampled();
            }

            return result;
        }

        public static int QuantitySampled(this Survey.LogRow logRow)
        {
            int result = logRow.QuantityIndividual();
            result += logRow.QuantityStratified();
            return result;
        }

        public static int QuantityIndividual(this Survey.LogRow logRow)
        {
            return logRow.GetIndividualRows().Length;
        }

        public static int QuantityStratified(this Survey.LogRow logRow)
        {
            int result = 0;

            foreach (Wild.Survey.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
            {
                result += stratifiedRow.Count;
            }

            return result;
        }

        public static int QuantityIndividual(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.QuantityIndividual();
            }

            return result;
        }

        public static int QuantityIndividual(this CardStack stack)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.QuantityIndividual();
            }

            return result;
        }

        public static int QuantityStratified(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                result += logRow.QuantityStratified();
            }

            return result;
        }

        public static int QuantityStratified(this CardStack stack)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows())
            {
                result += logRow.QuantityStratified();
            }

            return result;
        }



        public static int Quantity(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            return stack.QuantityIndividual(speciesRow, size) +
                stack.QuantityStratified(speciesRow, size);
        }

        public static int QuantityIndividual(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;

                // TODO: If individual is jimbo - skip it!
                // Jimbo is when:
                // (Log.Mass / Log.Quantity) is more than 15% not equal Individual.Mass
                // AND 
                // (There are not more than 5 individuals in card)
                // Individual.Length out of stratified sample if sample is presented.

                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result++;
            }

            return result;
        }

        public static int QuantityStratified(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in stack.GetLogRows(speciesRow))
            {
                foreach (Wild.Survey.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                {
                    if (!size.LeftClosedContains(stratifiedRow.SizeClass.Midpoint)) continue;
                    result += stratifiedRow.Count;
                }
            }

            return result;
        }



        public static int Quantity(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size, Age age)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsAgeNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                if (!age.Contains(individualRow.Age)) continue;
                result++;
            }

            return result;
        }

        public static int Quantity(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size, Sex sex)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsSexNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                if (individualRow.Sex != sex.Value) continue;
                result++;
            }

            return result;
        }

        public static int Quantity(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Age age)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                if (!age.Contains(individualRow.Age)) continue;
                result++;
            }

            return result;
        }

        public static int Quantity(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Sex sex, Age age)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                if (individualRow.IsAgeNull()) continue;
                if (individualRow.Sex != sex.Value) continue;
                if (!age.Contains(individualRow.Age)) continue;
                result++;
            }

            return result;
        }



        public static int Treated(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, DataColumn column)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsNull(column)) continue;
                result++;
            }

            return result;
        }

        public static int Treated(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, DataColumn column, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsNull(column)) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result++;
            }

            return result;
        }

        public static int QuantityRegisteredNonAged(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                if (individualRow.IsTallyNull()) continue;
                if (!individualRow.IsAgeNull()) continue;
                result++;
            }

            return result;
        }



        public static int Measured(this CardStack stack)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows())
            {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }

        public static int Measured(this Wild.Survey.LogRow logRow)
        {
            int result = 0;


            foreach (Wild.Survey.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }

        public static int MeasuredAnyhow(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            return stack.Measured(speciesRow) + stack.QuantityStratified(speciesRow);
        }


        public static int Weighted(this CardStack stack)
        {
            int result = 0;
            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result++;
            }
            return result;
        }

        public static int Weighted(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result++;
            }

            return result;
        }



        public static int Tallied(this CardStack stack)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows())
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsTallyNull()) continue;
                result++;
            }

            return result;
        }

        public static int Tallied(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsTallyNull()) continue;
                result++;
            }

            return result;
        }

        public static int Tallied(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsTallyNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result++;
            }

            return result;
        }



        public static int Aged(this CardStack stack)
        {
            int result = 0;
            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows())
            {
                if (individualRow.IsAgeNull()) continue;
                result++;
            }
            return result;
        }

        public static int Aged(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                result++;
            }

            return result;
        }

        public static int Aged(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsAgeNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result++;
            }

            return result;
        }
                


        public static int Sexed(this CardStack stack)
        {
            int result = 0;
            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows())
            {
                if (individualRow.IsSexNull()) continue;
                result++;
            }
            return result;
        }

        public static int Sexed(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                result++;
            }

            return result;
        }

        public static int Sexed(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, Interval size)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                if (individualRow.IsLengthNull()) continue;
                if (!size.LeftClosedContains(individualRow.Length)) continue;
                result++;
            }

            return result;
        }



        public static int Matured(this CardStack stack)
        {
            int result = 0;
            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows())
            {
                if (individualRow.IsMaturityNull()) continue;
                result++;

            }
            return result;
        }

        public static int Matured(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMaturityNull()) continue;
                result++;
            }

            return result;
        }
    }

    public class SampleSizeDescriptor
    {
        public string Name;

        public int Quantity;
        public int QuantityStratified;
        public int QuantityIndividual;
        public int Measured;
        public int Weighted;
        public int Tallied;
        public int Aged;
        public int Sexed;
        public int Matured;

        public double Mass;
        public double MassStratified;
        public double MassIndividual;
    }
}
