using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;
using Mayfly.Species;

namespace Mayfly.Wild
{
    partial class CardStack
    {
        public Sample Lengths(SpeciesKey.SpeciesRow speciesRow)
        {
            List<double> result = new List<double>();

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsLengthNull()) continue;
                    result.Add(individualRow.Length);
                }
            }

            return new Sample(result.ToArray()) { Name = Resources.Reports.Caption.LengthUnit };
        }

        public double LengthAverage(SpeciesKey.SpeciesRow speciesRow)
        {
            double result = 0;
            double divider = 0;

            foreach (Data.IndividualRow individualRow in this.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                result += individualRow.Length;
                divider++;
            }

            if (divider == 0)
            {
                return double.NaN;
            }
            else
            {
                return result / divider;
            }
        }

        public double LengthAverage(SpeciesKey.SpeciesRow speciesRow, Sex G)
        {
            double result = 0;
            double divider = 0;

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    result += individualRow.Length;
                    divider++;
                }
            }

            if (divider == 0)
            {
                return double.NaN;
            }
            else
            {
                return result / divider;
            }
        }

        public Sample Lengths(SpeciesKey.SpeciesRow speciesRow, Sex G)
        {
            List<double> result = new List<double>();

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    result.Add(individualRow.Length);
                }
            }

            return new Sample(result.ToArray());
        }

        public double LengthMin(SpeciesKey.SpeciesRow speciesRow, Sex G)
        {
            double result = double.MaxValue;
            int i = 0;

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    if (result > individualRow.Length)
                    {
                        result = individualRow.Length;
                        i++;
                    }
                }
            }

            if (i > 0)
            {
                return result;
            }
            else
            {
                return double.NaN;
            }
        }

        public double LengthMax(SpeciesKey.SpeciesRow speciesRow, Sex G)
        {
            double result = double.MinValue;
            int i = 0;

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsSexNull()) continue;
                    if (individualRow.IsLengthNull()) continue;
                    if (individualRow.Sex != G.Value) continue;
                    if (result < individualRow.Length)
                    {
                        result = individualRow.Length;
                        i++;
                    }
                }
            }

            if (i > 0)
            {
                return result;
            }
            else
            {
                return double.NaN;
            }
        }



        public double Mass()
        {
            double result = 0;

            foreach (SpeciesKey.SpeciesRow speciesRow in this.GetSpecies())
            {
                result += this.Mass(speciesRow);
            }

            return result;
        }

        public double Mass(SpeciesKey.SpeciesRow speciesRow)
        {
            double result = 0.0;

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                // TODO:
                // In old cards there were  [Mass = double.NaN] when stratified sample is presented.
                // In future just check for [IsMassNull()]

                // If mass is not set - calculate sample mass
                if (logRow.IsMassNull() || double.IsNaN(logRow.Mass))
                {
                    //result += this.MassStratified(logRow);
                    //result += this.MassIndividual(logRow);
                }
                else
                {
                    result += logRow.Mass;
                }
            }

            return result;
        }

        public Sample Masses(SpeciesKey.SpeciesRow speciesRow)
        {
            List<double> result = new List<double>();

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                {
                    if (individualRow.IsMassNull()) continue;
                    result.Add(individualRow.Mass);
                }
            }

            return new Sample(result.ToArray()) { Name = Resources.Reports.Caption.MassUnit };
        }



        public int Quantity()
        {
            int result = 0;

            foreach (Species.SpeciesKey.SpeciesRow speciesRow in GetSpecies())
            {
                result += Quantity(speciesRow);
            }

            return result;
        }

        public int Quantity(Species.SpeciesKey.SpeciesRow speciesRow)
        {
            int result = 0;

            foreach (Data.LogRow logRow in this.GetLogRows(speciesRow))
            {
                if (logRow.IsQuantityNull())
                {
                    // TODO: break? return Null?

                    // 1 - logRow has mass but does not have quantity

                    // 2 - logRow has no mass and quantity - qualitative sample

                    // 3 - 

                    //// It is just notice of species presence
                    //if (logRow.GetIndividualRows().Length > 0) 
                    //{
                    //    continue;
                    //}
                    //else
                    //{

                    //}
                    ////        else return double.NaN; // It is artifact

                    logRow.Quantity = logRow.GetIndividualRows().Length;
                }

                result += logRow.Quantity;
            }

            return result;
        }

        public int Quantity(SpeciesKey.SpeciesRow speciesRow, Sex G)
        {
            int result = 0;

            foreach (Data.IndividualRow individualRow in this.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsSexNull()) continue;
                if (individualRow.Sex != G.Value) continue;
                result++;
            }

            return result;
        }

        public int Weighted(SpeciesKey.SpeciesRow speciesRow)
        {
            int result = 0;


            foreach (Data.IndividualRow individualRow in this.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                result++;
            }

            return result;
        }

        public int Measured(SpeciesKey.SpeciesRow speciesRow)
        {
            int result = 0;


            foreach (Data.IndividualRow individualRow in this.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }
    }
}
