using System;
using System.Drawing;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Runtime.InteropServices;

namespace Mayfly.Wild
{
    public partial class Calculator : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);


        public DataGridViewCell Cell;

        private DataGridView SpreadSheet;

        public decimal TransfocatorMagnification
        {
            get { return numericUpDownTransfocatorMagnification.Value; }
            set { numericUpDownTransfocatorMagnification.Value = value; }
        }

        public int ScaleTicks
        {
            get { return (int)numericUpDownTicks.Value; }
            set { numericUpDownTicks.Value = value; }
        }

        public double TickValue
        {
            get { return (double)numericUpDownTickValue.Value; }
            set { numericUpDownTickValue.Value = (decimal)value; }
        }

        public int Units
        {
            get { return domainUpDownUnits.SelectedIndex; }
            set { domainUpDownUnits.SelectedIndex = value; }
        }

        public int Precision
        {
            get { return (int)numericUpDownPrecision.Value; }
            set { numericUpDownPrecision.Value = value; }
        }

        public bool IsCellSet
        {
            get { return Cell != null; }
        }



        public Calculator()
        {
            InitializeComponent();

            //Precision = UserSettings.Precision;
            //TransfocatorMagnification = UserSettings.MagnificationTransfocator;
            //Units = UserSettings.MagnificationUnits;
        }

        public Calculator(DataGridViewCell gridCell) : this()
        {
            Cell = gridCell;
            SpreadSheet = Cell.DataGridView;

            buttonNext.Visible = true;
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



        public void ResetCounters(double value)
        {
            TickValue = (double)1000 / (double)TransfocatorMagnification / (double)10;
            textBoxValue.Text = string.Format("{0:N" + Precision + "} {1}", value, domainUpDownUnits.Text);

            double ValueInMicrons = value;
            switch (Units)
            {
                case 0:
                    ValueInMicrons *= 1;
                    break;
                case 1:
                    ValueInMicrons *= 1000;
                    break;
                case 2:
                    ValueInMicrons *= 10000;
                    break;
                case 3:
                    ValueInMicrons *= 100000;
                    break;
                case 4:
                    ValueInMicrons *= 1000000;
                    break;
            }

            try
            {
                ScaleTicks = (int)Math.Round(ValueInMicrons / TickValue);
            }
            catch 
            {
                //textBoxValue.Text = Resources.Interface.Interface.InterfaceInvalidValue;
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
                numericUpDown_ValueChanged(numericUpDownTicks, new EventArgs());
            }
        }



        private void Calculator_Load(object sender, EventArgs e)
        {
            ResetCounters();
        }

        private void Calculator_FormClosing(object sender, FormClosingEventArgs e)
        {
            //UserSettings.MagnificationTransfocator = TransfocatorMagnification;
            //UserSettings.MagnificationUnits = Units;
        }



        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (((Control)sender).ContainsFocus)
            {
                TickValue = (double)1000 / (double)TransfocatorMagnification / (double)10;    
            
                double result = ScaleTicks * TickValue; // In micrometers

                switch (Units)
                {
                    case 0:
                        break;
                    case 1:
                        result /= 1000;
                        break;
                    case 2:
                        result /= 10000;
                        break;
                    case 3:
                        result /= 100000;
                        break;
                    case 4:
                        result /= 1000000;
                        break;
                }

                textBoxValue.Text = string.Format("{0:N" + Precision + "} {1}", result, domainUpDownUnits.Text);

                if (IsCellSet)
                {
                    if (result == 0)
                    {
                        Cell.Value = null;
                    }
                    else
                    {
                        Cell.Value = Math.Round(result, Precision);
                    }

                    SpreadSheet.RefreshEdit();
                    SpreadSheet.NotifyCurrentCellDirty(true);
                }
            }
        }

        private void numericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

            }
        }

        private void numericUpDownPrecision_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown_ValueChanged(sender, e);

            if (IsCellSet)
            {
                Cell.OwningColumn.DefaultCellStyle.Format = "N" + Precision;
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //if (SpreadSheet.RowCount == Cell.RowIndex + 1)
            //{
            //    int x = Cell.ColumnIndex;
            //    int y = Cell.RowIndex;
            //    SpreadSheet.Rows.Add();
            //    SpreadSheet.CurrentCell = SpreadSheet[x, y + 1];
            //}
            //else
            //{
            //    SpreadSheet.CurrentCell = SpreadSheet[Cell.ColumnIndex, Cell.RowIndex + 1];
            //}
        }
    }
}
