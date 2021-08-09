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
            if (x.IsIndexNull() || y.IsIndexNull())
            {
                return string.Compare(
                    x.Species.Replace(" gr.", string.Empty),
                    y.Species.Replace(" gr.", string.Empty));
            }
            else
            {
                int phr = string.Compare(
                    x.Index, y.Index);
                return phr == 0 ? string.Compare(
                    x.Species.Replace(" gr.", string.Empty),
                    y.Species.Replace(" gr.", string.Empty)) : phr;
            }
        }
    }
}
