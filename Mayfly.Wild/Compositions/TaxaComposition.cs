﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Species;
using Meta.Numerics.Statistics;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public class TaxaComposition : Composition
    {
        private TaxaComposition(string name)
            : base(name)
        { }

        public TaxaComposition(SpeciesComposition speciesComposition, SpeciesKey.BaseRow baseRow, bool includeEmpty)
            : base(baseRow.Base)
        {

            foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows())
            {
                SpeciesSwarmPool taxaCategory = new SpeciesSwarmPool(taxaRow);

                SpeciesKey.SpeciesRow[] reps = taxaRow.GetSpecies();
                List<SpeciesSwarm> swarms = new List<SpeciesSwarm>();

                List<double> abundances = new List<double>();
                List<double> biomasses = new List<double>();

                foreach (SpeciesSwarm speciesSwarm in speciesComposition)
                {
                    if (!taxaRow.Includes(speciesSwarm.Name)) continue;

                    swarms.Add(speciesSwarm);

                    taxaCategory.Quantity += speciesSwarm.Quantity;
                    taxaCategory.Mass += speciesSwarm.Mass;
                    taxaCategory.Abundance += speciesSwarm.Abundance;
                    taxaCategory.Biomass += speciesSwarm.Biomass;

                    abundances.Add(speciesSwarm.Abundance);
                    biomasses.Add(speciesSwarm.Biomass);
                }

                taxaCategory.SpeciesSwarms = swarms.ToArray();
                taxaCategory.DiversityA = new Sample(abundances).Diversity();
                taxaCategory.DiversityB = new Sample(biomasses).Diversity();

                if (includeEmpty || taxaCategory.Quantity > 0) this.AddCategory(taxaCategory);
            }


            SpeciesSwarmPool varia = new SpeciesSwarmPool(Species.Resources.Interface.Varia);

            SpeciesKey.SpeciesRow[] various = baseRow.Varia;
            List<SpeciesSwarm> variaswarms = new List<SpeciesSwarm>();

            foreach (SpeciesSwarm speciesSwarm in speciesComposition)
            {
                if (!various.Contains(speciesSwarm.DataRow)) continue;

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

        public TaxaComposition(SpeciesComposition speciesComposition, SpeciesKey.BaseRow baseRow)
            : this(speciesComposition, baseRow, false)
        { }



        public new SpeciesSwarmPool this[int i]
        {
            get { return (SpeciesSwarmPool)base[i]; }
            set { base[i] = value; }
        }
    }

    public class SpeciesSwarmPool : Category
    {
        public SpeciesKey.TaxaRow DataRow { get; set; }

        public SpeciesKey.SpeciesRow[] SpeciesRows
        {
            get
            {
                List<SpeciesKey.SpeciesRow> result = new List<SpeciesKey.SpeciesRow>();

                foreach (SpeciesSwarm swarm in SpeciesSwarms)
                {
                    if (swarm.DataRow != null) result.Add(swarm.DataRow);
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

        public SpeciesSwarmPool(SpeciesKey.TaxaRow dataRow) : this(dataRow.Taxon)
        {
            DataRow = dataRow;
        }
    }
}