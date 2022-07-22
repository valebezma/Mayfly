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

        public SpeciesComposition GetEmptyCopy()
        {
            SpeciesComposition result = new SpeciesComposition(string.Empty, Count);

            foreach (SpeciesSwarm category in this)
            {
                result.AddCategory(category.GetEmptyCopy());
            }

            return result;
        }
    }

    public class SpeciesSwarm : Category
    {
        public TaxonomicIndex.TaxonRow TaxonRow { get; set; }



        public SpeciesSwarm(TaxonomicIndex.TaxonRow taxonRow)
        {
            TaxonRow = taxonRow;
        }

        public override Category GetEmptyCopy()
        {
            SpeciesSwarm result = new SpeciesSwarm(TaxonRow)
            {
                Name = TaxonRow.Name
            };
            return result;
        }

        public override string ToString()
        {
            return TaxonRow.ToString();
        }
    }
}
