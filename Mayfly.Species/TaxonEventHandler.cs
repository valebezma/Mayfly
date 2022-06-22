using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Species
{
    public class TaxonEventArgs : EventArgs
    {
        public TaxonEventArgs(SpeciesKey.TaxonRow taxonRow)
        {
            this.TaxonRow = taxonRow;
        }

        public SpeciesKey.TaxonRow TaxonRow { get; private set; }
    }

    public delegate void TaxonEventHandler(object sender, TaxonEventArgs e);
}
