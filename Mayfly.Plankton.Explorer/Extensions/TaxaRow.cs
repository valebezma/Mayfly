using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Species;
using Mayfly.Plankton;
using Mayfly.Plankton.Explorer;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class TaxonRowExtensions
    {
        public static double Occurrence(this TaxonomicIndex.TaxonRow taxonRow, Wild.Survey data)
        {
            int present = 0;

            foreach (Wild.Survey.CardRow cardRow in data.Card)
            {
                foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
                {
                    if (taxonRow.Includes(logRow.DefinitionRow.Taxon))
                    {
                        present++;
                        break;
                    }
                }
            }

            return (double)present / (double)data.Card.Count;
        }
    }
}
