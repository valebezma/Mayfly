using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Species;

namespace Mayfly.Wild
{
    public class SpeciesComposition : Composition
    {
        public SpeciesComposition() 
            : base()
        { }

        public SpeciesComposition(string name)
            : base(name)
        { }

        public SpeciesComposition(string name, int capacity)
            : base(name, capacity)
        { }



        public new SpeciesSwarm this[int i]
        {
            get { return (SpeciesSwarm)base[i]; }
            set { base[i] = value; }
        }

        public string[] GetDominantNames()
        {
            List<string> result = new List<string>();

            foreach (Category cat in GetDominants())
            {
                result.Add(((SpeciesSwarm)cat).Name);
            }

            return result.ToArray();
        }
    }

    public class SpeciesSwarm : Category
    {
        public SpeciesKey.SpeciesRow DataRow { get; set; }



        public SpeciesSwarm(string species)
        {
            Name = species;
        }

        public override Category GetEmptyCopy()
        {
            SpeciesSwarm result = new SpeciesSwarm(this.Name);
            if (this.DataRow != null) result.DataRow = this.DataRow;
            return result;
        }

        public override string ToString()
        {
            return (DataRow == null ? base.ToString() : DataRow.ShortNameReport);
        }
    }
}
