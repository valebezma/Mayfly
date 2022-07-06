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
    public partial class Diagnosis : Form
    {
        Controls.DefinitionPanel Panel;

        public Diagnosis(Controls.DefinitionPanel panel)
        {
            InitializeComponent();
            listViewHistory.Shine();

            Panel = panel;
            Panel.StepChanged += Panel_StepChanged;

            UpdateHistory();
        }

        private void Panel_StepChanged(object sender, Controls.DefinitionEventArgs e)
        {
            UpdateHistory();
        }

        public void UpdateHistory()
        {
            listViewHistory.Items.Clear();

            foreach (TaxonomicIndex.StateRow stateRow in Panel.Selections)
            {
                ListViewItem stepItem = new ListViewItem();
                stepItem.Name = stateRow.ID.ToString();
                stepItem.Text = stateRow.FeatureRow.Title;
                stepItem.SubItems.Add(stateRow.Description);

                listViewHistory.Items.Add(stepItem);
            }
        }

        private void listViewHistory_ItemActivate(object sender, EventArgs e)
        {
            int stateID = Convert.ToInt32(listViewHistory.SelectedItems[0].Name);
            Panel.GetBack(((TaxonomicIndex)Panel.CurrentStep.Table.DataSet).State.FindByID(stateID));
        }
    }
}
