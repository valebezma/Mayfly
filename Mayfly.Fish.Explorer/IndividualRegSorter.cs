using System;
using System.Collections.Generic;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public class IndividualRegSorter : IComparer<Data.IndividualRow>
    {
        public int Compare(Data.IndividualRow x, Data.IndividualRow y)
        {
            return new OmniSorter().Compare(x.Tally, y.Tally);
        }
    }
}
