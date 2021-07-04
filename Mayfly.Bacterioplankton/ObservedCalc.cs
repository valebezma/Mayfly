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

namespace Mayfly.Bacterioplankton
{
    public partial class ObservedCalc : Form
    {

        public bool IsCellSet
        {
            get { return Cell != null; }
        }

        public DataGridViewCell Cell;

        private DataGridView SpreadSheet;



        public ObservedCalc()
        {
            InitializeComponent();
        }

        public ObservedCalc(DataGridViewCell gridCell) : this()
        {
            Cell = gridCell;
            SpreadSheet = Cell.DataGridView;
            SpreadSheet.CurrentCellChanged += DataGridView_CurrentCellChanged;

            this.SetFriendlyDesktopLocation(gridCell);
        }

        void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (IsCellSet)
            {
                Cell = SpreadSheet.CurrentCell;
                ResetCounters();
            }
        }



        public void ResetCounters()
        {
            if (IsCellSet)
            {
                //if (Cell.Value == null) // numericUpDown_ValueChanged(numericUpDownTicks, new EventArgs());

                if (Cell.Value is double) ResetCounters((double)Cell.Value);
                else ResetCounters(0);
            }
            else
            {
                numericUpDown_ValueChanged(numericUpDownArea, new EventArgs());
            }
        }

        public void ResetCounters(double value)
        {
            try
            {
                numericUpDownFields.Value = (decimal)value / numericUpDownArea.Value;
            }
            catch
            {
                //textBoxValue.Text = Resources.Interface.InvalidValue;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            double s = (double)numericUpDownArea.Value *
                (double)numericUpDownFields.Value;

            textBoxValue.Text = s.ToString();

            if (IsCellSet)
            {
                Cell.Value = s;
            }
        }
    }
}
