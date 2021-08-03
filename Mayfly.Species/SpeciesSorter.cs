using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Mayfly.Species
{
    public class SpeciesSorter : IComparer<SpeciesKey.SpeciesRow>, IComparer<DataRow>
    {
        private SpeciesKey _key;

        public SpeciesSorter(SpeciesKey key)
        {
            _key = key;
        }

        public int Compare(DataRow x, DataRow y)
        {
            SpeciesKey.SpeciesRow _x = _key.Species.FindBySpecies((string)x["Species"]);
            SpeciesKey.SpeciesRow _y = _key.Species.FindBySpecies((string)y["Species"]);
            if (_x != null && _y != null) return this.Compare(_x, _y);
            else return string.Compare((string)x["Species"], (string)y["Species"]);
        }

        public int Compare(SpeciesKey.SpeciesRow x, SpeciesKey.SpeciesRow y)
        {
            if (x.IsPhilogeneticRateNull() || y.IsPhilogeneticRateNull())
            {
                return string.Compare(
                    x.Name.Replace(" gr.", string.Empty),
                    y.Name.Replace(" gr.", string.Empty));
            }
            else
            {
                int phr = string.Compare(
                    x.PhilogeneticRate, y.PhilogeneticRate);
                return phr == 0 ? string.Compare(
                    x.Name.Replace(" gr.", string.Empty),
                    y.Name.Replace(" gr.", string.Empty)) : phr;
            }
        }
    }
}
