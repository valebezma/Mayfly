using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayfly.Benthos;
using Mayfly.Benthos.Explorer;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class LogRowExtensions
    {
        public static List<Data.IndividualRow> GetUnweightedIndividualRows(this Data.LogRow logRow)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (!individualRow.IsMassNull()) continue;
                result.Add(individualRow);
            }

            return result;
        }

        public static int Unweighted(this Data.LogRow logRow)
        {
            int result = 0;

            if (logRow.IsMassNull())
            {
                if (logRow.GetIndividualRows().Length == 0)
                {
                    result += logRow.Quantity;
                }
                else
                {
                    result += logRow.UnweightedIndividuals();
                }
            }

            return result;
        }

        public static int UnweightedIndividuals(this Data.LogRow logRow)
        {
            return logRow.GetUnweightedIndividualRows().GetCount();
        }
    }
}
