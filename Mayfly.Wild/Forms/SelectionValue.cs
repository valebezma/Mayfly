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

namespace Mayfly.Wild
{
    public partial class SelectionValue : Form
    {
        public ColumnPicker Picker;

        public SelectionValue(DataGridView grid)
        {
            InitializeComponent();
            Picker = new ColumnPicker(listViewColumns);
            Picker.PolluteControl(grid.Columns);
            listViewColumns.Shine();
        }

        public SelectionValue(List<DataGridViewColumn> columns)
        {
            InitializeComponent();
            Picker = new ColumnPicker(listViewColumns);
            Picker.PolluteControl(columns);
            listViewColumns.Shine();
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
    }
}
