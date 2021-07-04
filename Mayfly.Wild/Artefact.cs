using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Wild
{
    public abstract class Artefact
    {
        public Artefact() { }

        public int GetFacts() { return 1; }

        public override string ToString() { return string.Empty; }

        //public abstract ArtefactCriticality Criticality;
    }

    public enum ArtefactCriticality
    {
        Normal,
        Allowed,
        NotCritical,
        Critical
    }
}
