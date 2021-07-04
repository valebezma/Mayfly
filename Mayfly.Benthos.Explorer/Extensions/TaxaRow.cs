using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Species;
using Mayfly.Benthos;
using Mayfly.Benthos.Explorer;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class TaxaRowExtensions
    {
        public static double Occurrence(this SpeciesKey.TaxaRow taxaRow, Data data)
        {
            int present = 0;

            foreach (Data.CardRow cardRow in data.Card)
            {
                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    if (taxaRow.Includes(logRow.SpeciesRow.Species))
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
