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
        public TaxonEventArgs(TaxonomicIndex.TaxonRow taxonRow)
        {
            this.TaxonRow = taxonRow;
        }

        public TaxonomicIndex.TaxonRow TaxonRow { get; private set; }
    }

    public delegate void TaxonEventHandler(object sender, TaxonEventArgs e);
}
