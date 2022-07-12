using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Benthos;
using Mayfly.Benthos.Explorer;
using Mayfly.Wild;
using Mayfly.Species;

namespace Mayfly.Extensions
{
    public static class SpeciesRowExtensions
    {
        public static List<Wild.Survey.IndividualRow> GetUnweightedIndividualRows(this Wild.Survey.DefinitionRow speciesRow)
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            foreach (Wild.Survey.IndividualRow individualRow in speciesRow.GetIndividualRows())
            {
                if (!individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static List<Wild.Survey.IndividualRow> GetWeightedIndividualRows(this TaxonomicIndex.TaxonRow speciesRow)
        {
            return speciesRow.GetWeightedIndividualRows(new CardStack((Wild.Survey)speciesRow.Table.DataSet));
        }

        public static List<Wild.Survey.IndividualRow> GetWeightedIndividualRows(this TaxonomicIndex.TaxonRow speciesRow, CardStack stack)
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            foreach (Wild.Survey.IndividualRow individualRow in stack.GetIndividualRows(speciesRow))
            {
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static List<Wild.Survey.IndividualRow> GetWeightedAndMeasuredIndividualRows(this IEnumerable<Wild.Survey.IndividualRow> individualRows)
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            foreach (Wild.Survey.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsLengthNull()) continue;
                if (individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static List<Wild.Survey.IndividualRow> GetUnweightedAndMeasuredIndividualRows(this IEnumerable<Wild.Survey.IndividualRow> individualRows)
        {
            List<Wild.Survey.IndividualRow> result = new List<Wild.Survey.IndividualRow>();

            foreach (Wild.Survey.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsLengthNull()) continue;
                if (!individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static int Unweighted(this Wild.Survey.DefinitionRow speciesRow)
        {
            return speciesRow.UnweightedIndividuals() + speciesRow.AbstractIndividuals();
        }

        public static int UnweightedIndividuals(this Wild.Survey.DefinitionRow speciesRow)
        {
            return speciesRow.GetUnweightedIndividualRows().GetCount();
        }

        public static int AbstractIndividuals(this Wild.Survey.DefinitionRow speciesRow)
        {
            int result = 0;

            foreach (Wild.Survey.LogRow logRow in speciesRow.GetLogRows())
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
