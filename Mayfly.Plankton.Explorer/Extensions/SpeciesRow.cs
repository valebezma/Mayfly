using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Plankton;
using Mayfly.Plankton.Explorer;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class SpeciesRowExtensions
    {
        public static List<Data.IndividualRow> GetUnweightedIndividualRows(this Data.DefinitionRow speciesRow)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (!individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static List<Data.IndividualRow> GetWeightedIndividualRows(this Data.DefinitionRow speciesRow)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static List<Data.IndividualRow> GetWeightedAndMeasuredIndividualRows(this IEnumerable<Data.IndividualRow> individualRows)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static List<Data.IndividualRow> GetUnweightedAndMeasuredIndividualRows(this IEnumerable<Data.IndividualRow> individualRows)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsLengthNull()) continue;
                if (!individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static int Unweighted(this Data.DefinitionRow speciesRow)
        {
            return speciesRow.UnweightedIndividuals() + speciesRow.AbstractIndividuals();
        }

        public static int UnweightedIndividuals(this Data.DefinitionRow speciesRow)
        {
            return speciesRow.GetUnweightedIndividualRows().GetCount();
        }

        public static int AbstractIndividuals(this Data.DefinitionRow speciesRow)
        {
            int result = 0;

            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                if (logRow.GetIndividualRows().Length == 0)
                {
                    result += logRow.Quantity;
                }
            }

            return result;
        }
    }
}
