using System;
using System.Collections.Generic;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public class IndividualRegSorter : IComparer<Wild.Survey.IndividualRow>
    {
        public int Compare(Wild.Survey.IndividualRow x, Wild.Survey.IndividualRow y)
        {
            return new OmniSorter().Compare(x.Tally, y.Tally);
        }
    }
}
