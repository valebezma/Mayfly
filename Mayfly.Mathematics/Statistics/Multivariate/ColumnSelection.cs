using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Statistics.Multivariate
{
    public partial class ColumnSelection : Form
    {
        #region Properties

        private DataGridView grid;

        public DataGridView Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;

                listViewColumns.Items.Clear();
                foreach (DataGridViewColumn gridColumn in Grid.Columns)
                {
                    if (gridColumn.Visible)
                    {
                        ListViewItem item = ColumnItem(gridColumn);
                        item.Name = gridColumn.HeaderText;
                        listViewColumns.Items.Add(item);
                    }
                }
            }
        }

        private ListViewItem ColumnItem(DataGridViewColumn gridColumn)
        {
            ListViewItem result = new ListViewItem();
            result.Tag = gridColumn;
            result.Text = gridColumn.HeaderText;
            return result;
        }

        public List<DataGridViewColumn> SelectedSamples
        {
            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();
                foreach (ListViewItem item in listViewColumns.Items)
                {
                    if (item.Selected)
                    {
                        result.Add(Grid.Columns[item.Name]);
                    }
                }
                return result;
            }

            set
            {
                listViewColumns.SelectedItems.Clear();
                foreach (DataGridViewColumn gridColumn in value)
                {
                    listViewColumns.FindItemWithText(gridColumn.HeaderText).Selected = true;
                }
            }
        }

        #endregion

        #region Constructors

        public ColumnSelection()
        {
            InitializeComponent();
            listViewColumns.Shine();
        }

        #endregion

        #region Interface logics

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

        #endregion
    }
}
