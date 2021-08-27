using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public abstract class Artefact
    {
        public Artefact() { }

        public abstract string[] GetNotices();

        public int GetFacts() { return GetNotices().Length; }

        public override string ToString() { return string.Empty; }

        public string ToString(string starter)
        {
            if (GetFacts() >0)
            {
                return  "<span class = 'hl'>" + starter + ": </span>" + GetNotices().Merge();
            }
            else
            {
                return string.Empty;
            }
        }

        //public abstract ArtefactCriticality Criticality;

        public static ArtefactCriticality GetWorst(params ArtefactCriticality[] criticalities)
        {
            return criticalities.Max();
        }
    }

    public enum ArtefactCriticality
    {
        Normal,
        Allowed,
        NotCritical,
        Critical
    }
}
