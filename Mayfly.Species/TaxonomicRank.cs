using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Species
{
    public class TaxonomicRank
    {
        public int Value { get; set; }

        public string Name { get { return Resources.Rank.ResourceManager.GetString(Value.ToString()); } }

        public TaxonomicRank(int value)
        {
            Value = value;
        }

        public static TaxonomicRank[] GetTaxonomicRanks(params int[] values)
        {
            List<TaxonomicRank> result = new List<TaxonomicRank>();
            foreach (int i in values)
            {
                result.Add(new TaxonomicRank(i));
            }
            return result.ToArray();
        }

        public static TaxonomicRank[] AllRanks
        {
            get
            {
                return GetTaxonomicRanks(11, 21, 22, 31, 32, 33, 41, 42, 50, 51, 52, 53, 60, 61, 62, 71, 72, 81, 82, 91, 92);
            }
        }

        public static TaxonomicRank[] HigherRanks
        {
            get
            {
                return GetTaxonomicRanks(11, 21, 22, 31, 32, 33, 41, 42, 50, 51, 52, 53, 60, 61, 62, 71, 72);
            }
        }

        public static TaxonomicRank[] MajorRanks
        {
            get
            {
                return GetTaxonomicRanks(11, 21, 31, 41, 51, 61, 71);
            }
        }

        public static TaxonomicRank Species
        {
            get
            {
                return new TaxonomicRank(91);
            }
        }
    }
}
