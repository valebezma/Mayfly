using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Mayfly.Species
{
    public class TaxonomicRank : IComparable, IFormattable
    {
        private int Value { get; set; }

        private TaxonomicRank(int rank)
        {
            Value = rank;
        }



        public static TaxonomicRank[] GetTaxonomicRanks(params int[] values)
        {
            List<TaxonomicRank> result = new List<TaxonomicRank>();
            foreach (int i in values)
            {
                result.Add((TaxonomicRank)i);
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
                return GetTaxonomicRanks(31, 41, 51, 61, 71);
            }
        }




        public static TaxonomicRank Species
        {
            get
            {
                return (TaxonomicRank)91;
            }
        }

        public static TaxonomicRank Subspecies
        {
            get
            {
                return (TaxonomicRank)92;
            }
        }

        public static TaxonomicRank Subtribe
        {
            get
            {
                return (TaxonomicRank)72;
            }
        }




        public override string ToString()
        {
            return ToString(string.Empty);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return Resources.Rank.ResourceManager.GetString(Value.ToString(), (CultureInfo)provider);
        }

        #region Cast implementations

        public static implicit operator TaxonomicRank(int value)
        {
            return new TaxonomicRank(value);
        }

        public static implicit operator int(TaxonomicRank value)
        {
            return value.Value;
        }

        #endregion

        #region IComparable implementations

        public static bool operator ==(TaxonomicRank a, TaxonomicRank b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Value == b.Value;
        }

        public static bool operator !=(TaxonomicRank a, TaxonomicRank b)
        {
            return !(a == b);
        }

        int IComparable.CompareTo(object obj)
        {
            return Compare(this, (TaxonomicRank)obj);
        }

        public static int Compare(TaxonomicRank value1, TaxonomicRank value2)
        {
            return value1.Value - value2.Value;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast return false.
            TaxonomicRank p = obj as TaxonomicRank;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (Value == p.Value);
        }

        public bool Equals(TaxonomicRank p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p.Value == Value);
        }

        public override int GetHashCode()
        {
            return (int)(Value * 100);
        }

        #endregion
    }
}
