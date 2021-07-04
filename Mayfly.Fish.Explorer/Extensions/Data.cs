using Mayfly.Fish;
using System.Collections.Generic;
using System.Linq;
using Mayfly.Wild;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    public static partial class DataExtensions
    {
        public static string[] GetVariantsOf(this Data data, string field)
        {
            return data.GetVariantsOf(field, string.Empty);
        }

        public static string[] GetVariantsOf(this Data data, string field, string format)
        {
            List<string> result = new List<string>();

            foreach (Data.CardRow cardRow in data.Card)
            {
                object value = cardRow.Get(field);

                if (value == null) continue;

                string formatted = value.Format(format);

                if (!result.Contains(formatted))
                {
                    result.Add(formatted);
                }
            }

            result.Sort(new OmniSorter());

            return result.ToArray();
        }

        public static Data.IndividualRow[] GetIndividuals(this Data data, Data.SpeciesRow spcRow,
            string field, object value)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (Data.IndividualRow indRow in spcRow.GetIndividualRows())
            {
                if (indRow[field].Equals(value)) {
                    result.Add(indRow);
                }
            } 

            return result.ToArray();
        }

        public static Data.IndividualRow[] GetIndividuals(this Data data, Data.SpeciesRow spcRow,
            string[] field, object[] value)
        {
            List<Data.IndividualRow[]> packs = new List<Data.IndividualRow[]>();
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            for (int i = 0; i < field.Length; i++)
            {
                Data.IndividualRow[] pack = data.GetIndividuals(spcRow, field[i], value[i]);
                packs.Add(pack);

                foreach (Data.IndividualRow indRow in pack)
                {
                    if (!result.Contains(indRow))
                    {
                        result.Add(indRow);
                    }
                }
            }

            for (int i = 0 ; i < result.Count; i++)// (Data.IndividualRow indRow in result)
            {
                foreach (Data.IndividualRow[] pack in packs)
                {
                    if (!pack.Contains(result[i]))
                    {
                        result.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            return result.ToArray();
        }
    }
}

