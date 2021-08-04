using Mayfly.Wild;
using Meta.Numerics.Statistics;
using System.Collections.Generic;

namespace Mayfly.Wild
{
    partial class CardStack
    {
        public Sample Lengths(Data.SpeciesRow speciesRow)
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

            return new Sample(result.ToArray());
        }

        public double LengthAverage(Data.SpeciesRow speciesRow)
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

        public double LengthAverage(Data.SpeciesRow speciesRow, Sex G)
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

        public Sample Lengths(Data.SpeciesRow speciesRow, Sex G)
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

        public double LengthMin(Data.SpeciesRow speciesRow, Sex G)
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

        public double LengthMax(Data.SpeciesRow speciesRow, Sex G)
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

            foreach (Data.SpeciesRow speciesRow in this.GetSpecies())
            {
                result += this.Mass(speciesRow);
            }

            return result;
        }

        public double Mass(Data.SpeciesRow speciesRow)
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

        public Sample Masses(Data.SpeciesRow speciesRow)
        {

            if (speciesRow == null) return new Sample();

            List<double> result = new List<double>();

            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow.Mass);
            }

            return new Sample(result.ToArray());
        }



        public int Quantity()
        {
            int result = 0;

            foreach (Data.SpeciesRow speciesRow in GetSpecies())
            {
                result += Quantity(speciesRow);
            }

            return result;
        }

        public int Quantity(Data.SpeciesRow speciesRow)
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
                    ////        else return double.NaN; // It is artefact

                    logRow.Quantity = logRow.GetIndividualRows().Length;
                }

                result += logRow.Quantity;
            }

            return result;
        }

        public int Quantity(Data.SpeciesRow speciesRow, Sex G)
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

        public int Weighted(Data.SpeciesRow speciesRow)
        {
            int result = 0;


            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result++;
            }

            return result;
        }

        public int Measured(Data.SpeciesRow speciesRow)
        {
            int result = 0;


            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsLengthNull()) continue;
                result++;
            }

            return result;
        }
    }
}
