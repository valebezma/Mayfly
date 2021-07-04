using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Controls;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Charts
{
    public partial class ScatterplotColumnSelection : Form
    {
        #region Properties

        private SpreadSheet grid;

        public SpreadSheet Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;

                comboBoxArgument.DataSource = grid.GetNumericalColumns();
                comboBoxFunction.DataSource = grid.GetNumericalColumns();
                comboBoxGrouping.DataSource = grid.GetVisibleColumns();

                foreach (DataGridViewColumn gridColumn in grid.GetVisibleColumns())
                {
                    listViewLabels.CreateItem(gridColumn.Name, gridColumn.HeaderText);
                }

                comboBoxArgument.SelectedIndex = 0;
                comboBoxFunction.SelectedIndex = 1;

                Grouping = null;
            }
        }

        public DataGridViewColumn Argument
        {
            get
            {
                return comboBoxArgument.SelectedItem as DataGridViewColumn;
            }

            set
            {
                comboBoxArgument.SelectedItem = value;
            }
        }

        public DataGridViewColumn Function
        {
            get
            {
                return comboBoxFunction.SelectedItem as DataGridViewColumn;
            }

            set
            {
                comboBoxFunction.SelectedItem = value;
            }
        }

        public DataGridViewColumn Grouping
        {
            get
            {
                return comboBoxGrouping.SelectedItem as DataGridViewColumn;
            }

            set
            {
                comboBoxGrouping.SelectedItem = value;
            }
        }

        public List<DataGridViewColumn> Labels
        {
            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();
                foreach (ListViewItem item in listViewLabels.Items)
                {
                    if (item.Selected)
                    {
                        result.Add(Grid.GetColumn(item.Name));
                    }
                }
                return result;
            }
        }

        public bool AutoTrend
        {
            get
            {
                return checkBoxAutoTrend.Checked;
            }

            set
            {
                checkBoxAutoTrend.Checked = value;
            }
        }

        #endregion

        public ScatterplotColumnSelection(SpreadSheet grid)
        {
            InitializeComponent();
            listViewLabels.Shine();
            Grid = grid;
        }

        public void ForbidGroupers()
        {
            comboBoxGrouping.SelectedIndex = -1;
            comboBoxGrouping.Enabled = false;
        }

        public void ForbidAutoTrend()
        {
            checkBoxAutoTrend.Checked = false;
            checkBoxAutoTrend.Enabled = false;
        }

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

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void comboBoxArgument_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = comboBoxArgument.SelectedIndex != -1 && comboBoxFunction.SelectedIndex != -1;
        }

        #endregion

        private void checkBoxLabels_CheckedChanged(object sender, EventArgs e)
        {
            listViewLabels.Enabled = checkBoxLabels.Checked;
        }
    }
}
