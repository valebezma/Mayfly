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
        public SpeciesKey.StateRow StateRow { get; private set; }
        public SpeciesKey.StepRow NextStep { get; private set; }

        public bool IsSpeciesAttached { get { return !StateRow.IsSpcIDNull(); } }
        public SpeciesKey.SpeciesRow SpeciesRow { get { return StateRow.SpeciesRow; } }

        public bool IsTaxonAttached { get { return !StateRow.IsTaxIDNull(); } }
        public SpeciesKey.TaxaRow TaxaRow { get { return StateRow.TaxaRow; } }
    }

    public delegate void StateClickedEventHandler(object sender, StateClickedEventArgs e);
}
