using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Species.Controls
{
    public class DefinitionEventArgs
    {
        public DefinitionEventArgs(TaxonomicIndex.StepRow stepRow)
        {
            this.CurrentStep = stepRow;
        }

        public TaxonomicIndex.StepRow CurrentStep { get; private set; }
    }

    public delegate void DefinitionEventHandler(object sender, DefinitionEventArgs e);
}
