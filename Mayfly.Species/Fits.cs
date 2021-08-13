using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species
{
    public partial class Fits : Form
    {
        Controls.DefinitionPanel Panel;

        public Fits(Controls.DefinitionPanel panel)
        {
            InitializeComponent();

            listViewFits.Shine();

            Panel = panel;
            Panel.StepChanged += Panel_StepChanged;

            UpdateList();
        }

        private void Panel_StepChanged(object sender, Controls.DefinitionEventArgs e)
        {
            UpdateList();
        }

        public void UpdateList()
        {
            listViewFits.Items.Clear();

            foreach (SpeciesKey.StateRow stateRow in
                Panel.CurrentStep.ForestandingSpeciesStates)
            {
                ListViewItem item = new ListViewItem
                {
                    Tag = stateRow,
                    Name = stateRow.ID.ToString(),
                    Text = stateRow.SpeciesRow.Species
                };

                if (stateRow.SpeciesRow.GetStateRows().Length > 1)
                {
                    item.SubItems.Add(string.Format("{0}: {1}...", 
                        stateRow.FeatureRow.Title,
                        stateRow.Description.Substring(0, Math.Min(15, stateRow.Description.Length))));
                }

                listViewFits.Items.Add(item);
            }
        }

        private void listViewFits_ItemActivate(object sender, EventArgs e)
        {
            int stateID = Convert.ToInt32(listViewFits.SelectedItems[0].Name);
            SpeciesKey.StateRow stateRow = ((SpeciesKey)Panel.CurrentStep.Table.DataSet).State.FindByID(stateID);
            Panel.GetTo(stateRow);
        }
    }
}
