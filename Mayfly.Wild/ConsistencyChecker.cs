using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Extensions;
using System.Drawing;

namespace Mayfly.Wild
{
    public abstract class ConsistencyChecker
    {
        public ConsistencyChecker() { }

        public abstract string[] GetNotices(bool includeChildren);

        public string[] GetNotices()
        {
            return GetNotices(false);
        }

        public int ArtifactsCount
        {
            get { return GetNotices(true).Length; }
        }

        //public override string ToString() { return string.Empty; }

        public string ToString(string starter)
        {
            if (ArtifactsCount > 0)
            {
                return  "<span class = 'hl'>" + starter + ": </span>" + GetNotices(true).Merge();
            }
            else
            {
                return string.Empty;
            }
        }

        public static ArtifactCriticality GetWorst(params ArtifactCriticality[] criticalities)
        {
            return criticalities.Max();
        }

        public static Image GetImage(ArtifactCriticality criticality)
        {
            switch (criticality)
            {
                //case ArtifactCriticality.Normal:
                //    return Mathematics.Properties.Resources.Check;

                case ArtifactCriticality.Allowed:
                    return Mathematics.Properties.Resources.CheckGray;

                case ArtifactCriticality.Bad:
                    return Mathematics.Properties.Resources.None;

                case ArtifactCriticality.Critical:
                    return Mathematics.Properties.Resources.NoneRed;
            }

            return null;
        }

        //public static ConsistencyChecker GetFiltered(ConsistencyChecker[] checkers, ArtifactCriticality criticality)
        //{
        //    for (int i = 0; i < checkers.Length; i++)
        //    {
                
        //    }

        //}
    }

    public enum ArtifactCriticality
    {
        Normal,
        Allowed,
        Bad,
        Critical
    }
}
