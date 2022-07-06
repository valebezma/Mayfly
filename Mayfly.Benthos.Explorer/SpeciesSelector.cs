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

        public Data.DefinitionRow[] SelectedSpecies
        {
            set
            {
                listViewColumns.SelectedItems.Clear();
                foreach (Data.DefinitionRow speciesRow in value)
                {
                    ListViewItem item = listViewColumns.FindItemWithText(speciesRow.Taxon);
                    item.Selected = true;
                    listViewColumns.EnsureVisible(item.Index);
                }
            }

            get
            {
                List<Data.DefinitionRow> result = new List<Data.DefinitionRow>();
                foreach (ListViewItem item in listViewColumns.SelectedItems)
                {
                    result.Add(Data.Definition.FindByName(item.Name));
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

            foreach (Data.DefinitionRow speciesRow in Data.Definition)
            {
                listViewColumns.CreateItem(speciesRow.Taxon);
            }

            listViewColumns.Sort();
        }

        private void ResetItems(string pattern)
        {
            listViewColumns.Items.Clear();

            foreach (Data.DefinitionRow speciesRow in Data.Definition)
            {
                if (!speciesRow.Taxon.ToLowerInvariant().Contains(pattern.ToLowerInvariant())) continue;
                listViewColumns.CreateItem(speciesRow.Taxon);
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
