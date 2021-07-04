using System;
using System.Collections.Generic;

namespace Mayfly
{
    public class OmniSorter : IComparer<string>, IComparer<object>
    {
        //use a buffer for performance since we expect
        //the Compare method to be called a lot
        private char[] splitBuffer = new char[2048];

        public int Compare(string x, string y)
        {
            // try to parse to datetime values
            DateTime dateA = new DateTime();
            DateTime dateB = new DateTime();

            if (DateTime.TryParse(x, out dateA) && DateTime.TryParse(y, out dateB))
            {
                return dateA.CompareTo(dateB);
            }

            // If these are Month names
            DateTime monthA = new DateTime();
            DateTime monthB = new DateTime();

            if (DateTime.TryParse(x + " " + DateTime.Today.Year, out monthA) &&
                DateTime.TryParse(y + " " + DateTime.Today.Year, out monthB))
            {
                return monthA.CompareTo(monthB);
            }

            // TODO: If these are day period names
            // If these are season names


            //first split each string into segments
            //of non-numbers and numbers
            IList<string> a = SplitByNumbers(x);
            IList<string> b = SplitByNumbers(y);

            int aInt, bInt;
            int numToCompare = (a.Count < b.Count) ? a.Count : b.Count;
            for (int i = 0; i < numToCompare; i++)
            {
                if (a[i].Equals(b[i]))
                    continue;

                bool aIsNumber = Int32.TryParse(a[i], out aInt);
                bool bIsNumber = Int32.TryParse(b[i], out bInt);
                bool bothNumbers = aIsNumber && bIsNumber;
                bool bothNotNumbers = !aIsNumber && !bIsNumber;
                //do an integer compare
                if (bothNumbers) return aInt.CompareTo(bInt);
                //do a string compare
                if (bothNotNumbers) return a[i].CompareTo(b[i]);
                //only one is a number, which are
                //by definition less than non-numbers
                if (aIsNumber) return -1;
                return 1;
            }

            //only get here if one string is empty
            return a.Count.CompareTo(b.Count);            
        }

        private IList<string> SplitByNumbers(string val)
        {
            System.Diagnostics.Debug.Assert(val.Length <= 2048);
            List<string> list = new List<string>();
            int current = 0;
            int dest = 0;
            while (current < val.Length)
            {
                //accumulate non-numbers
                while (current < val.Length &&
                       !char.IsDigit(val[current]))
                {
                    splitBuffer[dest++] = val[current++];
                }
                if (dest > 0)
                {
                    list.Add(new string(splitBuffer, 0, dest));
                    dest = 0;
                }
                //accumulate numbers
                while (current < val.Length &&
                       char.IsDigit(val[current]))
                {
                    splitBuffer[dest++] = val[current++];
                }
                if (dest > 0)
                {
                    list.Add(new string(splitBuffer, 0, dest));
                    dest = 0;
                }
            }
            return list;
        }

        public int Compare(object x, object y)
        {
            return Compare(x.ToString(), y.ToString());            
        }
    }
}
