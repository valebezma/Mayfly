using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Species.Controls
{
    public class StateClickedEventArgs : EventArgs
    {
        public StateClickedEventArgs(State state)
        {
            this.StateRow = state.StateRow;
            this.NextStep = state.StateRow.Next;
        }
        public TaxonomicIndex.StateRow StateRow { get; private set; }
        public TaxonomicIndex.StepRow NextStep { get; private set; }

        public bool IsSpeciesAttached { get { return !StateRow.IsTaxIDNull(); } }

        public bool IsTaxonAttached { get { return !StateRow.IsTaxIDNull(); } }
        public TaxonomicIndex.TaxonRow TaxonRow { get { return StateRow.TaxonRow; } }
    }

    public delegate void StateClickedEventHandler(object sender, StateClickedEventArgs e);
}
