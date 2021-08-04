﻿using System;
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
        public Data.SpeciesRow SpeciesRow { get; set; }

        public new string Name { get { return SpeciesRow.KeyRecord.Name; } }



        public SpeciesSwarm(Data.SpeciesRow dataRow)
        {
            SpeciesRow = dataRow;
        }

        public override Category GetEmptyCopy()
        {
            SpeciesSwarm result = new SpeciesSwarm(SpeciesRow);
            return result;
        }

        public override string ToString()
        {
            return (SpeciesRow.KeyRecord.ShortName);
        }
    }
}
