using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Extensions;
using System.Drawing;

namespace Mayfly.Wild
{
    public abstract class Artefact
    {
        public Artefact() { }

        public abstract string[] GetNotices(bool incluDeChildren);

        public string[] GetNotices()
        {
            return GetNotices(false);
        }

        public int FactsCount
        {
            get { return GetNotices(true).Length; }
        }

        public override string ToString() { return string.Empty; }

        public string ToString(string starter)
        {
            if (FactsCount > 0)
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

        public static Image GetImage(ArtefactCriticality criticality)
        {
            switch (criticality)
            {
                //case ArtefactCriticality.Normal:
                //    return Mathematics.Properties.Resources.Check;

                case ArtefactCriticality.Allowed:
                    return Mathematics.Properties.Resources.CheckGray;

                case ArtefactCriticality.NotCritical:
                    return Mathematics.Properties.Resources.None;

                case ArtefactCriticality.Critical:
                    return Mathematics.Properties.Resources.NoneRed;
            }

            return null;
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
