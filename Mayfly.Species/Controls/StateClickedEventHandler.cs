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

        public bool IsSpeciesAttached { get { return !StateRow.IsTaxIDNull(); } }
        //public SpeciesKey.TaxonRow SpeciesRow { get { return StateRow.SpeciesRow; } }

        public bool IsTaxonAttached { get { return !StateRow.IsTaxIDNull(); } }
        public SpeciesKey.TaxonRow TaxonRow { get { return StateRow.TaxonRow; } }
    }

    public delegate void StateClickedEventHandler(object sender, StateClickedEventArgs e);
}
