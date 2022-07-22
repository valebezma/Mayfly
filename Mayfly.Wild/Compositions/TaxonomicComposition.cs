using Mayfly.Species;
using Meta.Numerics.Statistics;
using System.Collections.Generic;

namespace Mayfly.Wild
{
    public class TaxonomicComposition : Composition
    {
        public TaxonomicComposition(SpeciesComposition speciesComposition, TaxonomicIndex index, TaxonomicRank rank, bool includeEmpty)
            : base("Taxonomic")
        {
            List<SpeciesSwarm> essentials = new List<SpeciesSwarm>();

            foreach (TaxonomicIndex.TaxonRow taxonRow in index.GetTaxonRows(rank))
            {
                SpeciesSwarmPool taxonCategory = new SpeciesSwarmPool(taxonRow);
                List<SpeciesSwarm> swarms = new List<SpeciesSwarm>();

                List<double> abundances = new List<double>();
                List<double> biomasses = new List<double>();

                foreach (SpeciesSwarm speciesSwarm in speciesComposition)
                {
                    if (!taxonRow.Includes(speciesSwarm.TaxonRow, true)) continue;

                    swarms.Add(speciesSwarm);

                    taxonCategory.Quantity += speciesSwarm.Quantity;
                    taxonCategory.Mass += speciesSwarm.Mass;
                    taxonCategory.Abundance += speciesSwarm.Abundance;
                    taxonCategory.Biomass += speciesSwarm.Biomass;

                    abundances.Add(speciesSwarm.Abundance);
                    biomasses.Add(speciesSwarm.Biomass);

                    essentials.Add(speciesSwarm);
                }

                taxonCategory.SpeciesSwarms = swarms.ToArray();
                taxonCategory.DiversityA = new Sample(abundances).Diversity();
                taxonCategory.DiversityB = new Sample(biomasses).Diversity();

                if (includeEmpty || taxonCategory.Quantity > 0) this.AddCategory(taxonCategory);
            }

            SpeciesSwarmPool varia = new SpeciesSwarmPool(Species.Resources.Interface.Varia);

            //SpeciesKey.TaxonRow[] various = index.GetVaria(rank);
            List<SpeciesSwarm> variaswarms = new List<SpeciesSwarm>();

            foreach (SpeciesSwarm speciesSwarm in speciesComposition)
            {
                if (essentials.Contains(speciesSwarm)) continue;

                variaswarms.Add(speciesSwarm);

                varia.Quantity += speciesSwarm.Quantity;
                varia.Mass += speciesSwarm.Mass;
                varia.Abundance += speciesSwarm.Abundance;
                varia.Biomass += speciesSwarm.Biomass;
            }

            varia.SpeciesSwarms = variaswarms.ToArray();
            if (includeEmpty || varia.Quantity > 0) this.AddCategory(varia);

            this.SamplesCount = speciesComposition.SamplesCount;
        }

        public TaxonomicComposition(SpeciesComposition speciesComposition, TaxonomicIndex index, TaxonomicRank rank)
            : this(speciesComposition, index, rank, false)
        { }



        public new SpeciesSwarmPool this[int i]
        {
            get { return (SpeciesSwarmPool)base[i]; }
            set { base[i] = value; }
        }
    }

    public class SpeciesSwarmPool : Category
    {
        public TaxonomicIndex.TaxonRow DataRow { get; set; }

        public TaxonomicIndex.TaxonRow[] SpeciesRows
        {
            get
            {
                List<TaxonomicIndex.TaxonRow> result = new List<TaxonomicIndex.TaxonRow>();

                foreach (SpeciesSwarm swarm in SpeciesSwarms)
                {
                    result.Add(swarm.TaxonRow);
                }

                return result.ToArray();
            }
        }

        public SpeciesSwarm[] SpeciesSwarms { get; set; }

        public double DiversityA { get; internal set; }

        public double DiversityB { get; internal set; }



        public SpeciesSwarmPool(string name)
        {
            Name = name;
        }

        public SpeciesSwarmPool(TaxonomicIndex.TaxonRow dataRow) : this(dataRow.CommonName)
        {
            DataRow = dataRow;
        }
    }
}
