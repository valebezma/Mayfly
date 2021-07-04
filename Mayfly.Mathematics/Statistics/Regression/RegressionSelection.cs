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

namespace Mayfly.Mathematics.Statistics
{
    public partial class RegressionSelection : Form
    {
        #region Properties

        private SpreadSheet adapter;

        public SpreadSheet Adapter
        {
            get
            {
                return adapter;
            }

            set
            {
                adapter = value;
                comboBoxArgument.DataSource = adapter.GetNumericalColumns();
                comboBoxFunction.DataSource = adapter.GetNumericalColumns();

                foreach (DataGridViewColumn gridColumn in Adapter.GetVisibleColumns())
                {
                    ListViewItem item = new ListViewItem();
                    item.Name = gridColumn.Name;
                    item.Text = gridColumn.HeaderText;
                    listViewGroupers.Items.Add(item);
                }

                comboBoxArgument.SelectedIndex = 0;
                comboBoxFunction.SelectedIndex = 1;
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

        public List<DataGridViewColumn> Groupers
        {
            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();
                foreach (ListViewItem item in listViewGroupers.Items)
                {
                    if (item.Selected)
                    {
                        result.Add(Adapter.GetColumn(((ListViewItem)item).Name));
                    }
                }
                return result;
            }

            set
            {
                foreach (DataGridViewColumn gridColumn in value)
                {
                    foreach (ListViewItem item in listViewGroupers.Items)
                    {
                        if (item.Name == gridColumn.Name)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        public bool Compare
        {
            get { return checkBoxCompare.Checked; }
            set { checkBoxCompare.Checked = value; }
        }

        #endregion

        #region Constructors

        public RegressionSelection(SpreadSheet adapter)
        {
            InitializeComponent();

            Adapter = adapter;
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

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void comboBoxArgument_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = comboBoxArgument.SelectedIndex != -1 && comboBoxFunction.SelectedIndex != -1;
        }

        private void listViewGroupers_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxCompare.Enabled = listViewGroupers.SelectedItems.Count > 0;
        }

        #endregion
    }
}
