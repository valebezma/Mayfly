using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Mayfly.Extensions;
using Mayfly.Controls;
using Mayfly.Wild;

namespace Mayfly.Benthos.Explorer
{
    public partial class SpeciesSelector : Form
    {
        private Data Data;

        public Data.SpeciesRow[] SelectedSpecies
        {
            set
            {
                listViewColumns.SelectedItems.Clear();
                foreach (Data.SpeciesRow speciesRow in value)
                {
                    ListViewItem item = listViewColumns.FindItemWithText(speciesRow.Species);
                    item.Selected = true;
                    listViewColumns.EnsureVisible(item.Index);
                }
            }

            get
            {
                List<Data.SpeciesRow> result = new List<Data.SpeciesRow>();
                foreach (ListViewItem item in listViewColumns.SelectedItems)
                {
                    result.Add(Data.Species.FindBySpecies(item.Name));
                }
                return result.ToArray();
            }
        }

        //public DataGridViewCell Cell;

        public SpeciesSelector(
            //DataGridViewCell gridCell, 
            Data data)
        {
            InitializeComponent();
            listViewColumns.Shine();

            Data = data;
            //Cell = gridCell;

            ResetItems();
        }

        private void ResetItems()
        {
            listViewColumns.Items.Clear();

            foreach (Data.SpeciesRow speciesRow in Data.Species)
            {
                listViewColumns.CreateItem(speciesRow.Species);
            }

            listViewColumns.Sort();
        }

        private void ResetItems(string pattern)
        {
            listViewColumns.Items.Clear();

            foreach (Data.SpeciesRow speciesRow in Data.Species)
            {
                if (!speciesRow.Species.ToLowerInvariant().Contains(pattern.ToLowerInvariant())) continue;
                listViewColumns.CreateItem(speciesRow.Species);
            }

            listViewColumns.Sort();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text)) return;

            ResetItems(textBoxSearch.Text);
        }
    }
}
