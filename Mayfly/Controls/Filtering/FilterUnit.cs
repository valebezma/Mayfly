using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Controls
{
    public partial class FilterUnit : UserControl
    {
        private SpreadSheet sheet;
        bool pressed = false;

        #region Properties

        public SpreadSheet Sheet
        {
            get
            {
                return sheet;
            }

            set
            {
                sheet = value;
                //comboBoxFilterable.DataSource = Sheet.Columns;
                comboBoxFilterable.DataSource = Sheet.GetVisibleColumns();
            }
        }

        public DataGridViewColumn Filterable
        {
            get
            {
                return comboBoxFilterable.SelectedItem as DataGridViewColumn;
            }

            set
            {
                comboBoxFilterable.SelectedItem = value;
            }
        }

        private Type ValueType { get; set; }

        public int Index { get; set; }

        public double From { get; set; }

        public double To { get; set; }

        public string Category { get; set; }

        public bool IsFilterSet
        {
            get;
            set;
        }

        public bool IsNegative
        {
            get
            {
                return checkBoxNot.Checked;
            }

            set
            {
                checkBoxNot.Checked = value;
            }
        }

        #endregion

        public event EventHandler ValueChanged;

        public FilterUnit()
        {
            InitializeComponent();
            From = double.NaN;
            To = double.NaN;
            contextRemove.Image = Filtering.Filtering.FunnelMinus;
        }

        public FilterUnit(SpreadSheet adapter) : this()
        {
            Sheet = adapter;
            Filterable = null;
        }

        public FilterUnit(DataGridViewColumn gridColumn) : this()
        {
            Sheet = (SpreadSheet)gridColumn.DataGridView;
            Filterable = gridColumn;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (Filterable == null)
            {
                comboBoxFilterable.Focus();
            }
            else
            {
                if (comboBoxCategory.Visible)
                {
                    comboBoxCategory.Focus();
                }

                if (textBoxFrom.Visible)
                {
                    textBoxFrom.Focus();
                }
            }
        }

        #region Methods

        public bool DoesRowFit(DataGridViewRow gridRow)
        {
            if (ValueType.IsDoubleConvertible())
            {
                if (gridRow.Cells[Index].Value == null)
                {
                    return false;
                }

                double rowValue = gridRow.Cells[Index].Value.ToDouble();

                if (double.IsNaN(From) && double.IsNaN(To))
                {
                    return true;
                }

                if (IsNegative)
                {
                    if (!double.IsNaN(From) && double.IsNaN(To))
                    {
                        return rowValue < From;
                    }

                    if (double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue > To;
                    }

                    if (!double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue < From || rowValue > To;
                    }
                }
                else
                {
                    if (!double.IsNaN(From) && double.IsNaN(To))
                    {
                        return rowValue >= From;
                    }

                    if (double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue <= To;
                    }

                    if (!double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue >= From && rowValue <= To;
                    }
                }
            }
            else
            {
                if (Category == null)
                {
                    return (gridRow.Cells[Index].Value != null);
                }

                if (IsNegative)
                {
                    if (gridRow.Cells[Index].Value == null)
                    {
                        return Category != Resources.Interface.EmptyValue;
                    }
                    else
                    {
                        return gridRow.Cells[Index].FormattedValue.ToString() != Category;
                    }
                }
                else
                {
                    if (gridRow.Cells[Index].Value == null)
                    {
                        return Category == Resources.Interface.EmptyValue;
                    }
                    else
                    {
                        return gridRow.Cells[Index].FormattedValue.ToString() == Category;
                    }
                }
            }

            return true;
        }

        public bool DoesRowFit(DataRow dataRow)
        {
            return DoesFit(dataRow[Index]);
        }

        public bool DoesFit(object value)
        {
            if (ValueType.IsDoubleConvertible())
            {
                if (value == DBNull.Value)
                {
                    return false;
                }

                double rowValue = value.ToDouble();

                if (double.IsNaN(From) && double.IsNaN(To))
                {
                    return true;
                }

                if (IsNegative)
                {
                    if (!double.IsNaN(From) && double.IsNaN(To))
                    {
                        return rowValue < From;
                    }

                    if (double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue > To;
                    }

                    if (!double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue < From || rowValue > To;
                    }
                }
                else
                {
                    if (!double.IsNaN(From) && double.IsNaN(To))
                    {
                        return rowValue >= From;
                    }

                    if (double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue <= To;
                    }

                    if (!double.IsNaN(From) && !double.IsNaN(To))
                    {
                        return rowValue >= From && rowValue <= To;
                    }
                }
            }
            else
            {
                if (Category == null)
                {
                    return (value != null);
                }

                if (IsNegative)
                {
                    if (value == null)
                    {
                        return Category != Resources.Interface.EmptyValue;
                    }
                    else
                    {
                        return (string)value != Category;
                    }
                }
                else
                {
                    if (value == null)
                    {
                        return Category == Resources.Interface.EmptyValue;
                    }
                    else
                    {
                        return (string)value == Category;
                    }
                }
            }

            return true;
        }

        internal void FocusInput()
        {
            if (comboBoxCategory.Visible)
            {
                comboBoxCategory.Focus();
            }
            else
            {
                textBoxFrom.Focus();
            }
        }

        #endregion

        #region Interface logics

        private void comboBoxFilterable_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxCategory.Items.Clear();

            labelIs.Visible = checkBoxNot.Visible = Filterable != null;

            textBoxFrom.Text = string.Empty;
            textBoxTo.Text = string.Empty;

            if (Filterable == null)
            {
                ValueType = null;
                Index = -1;

                comboBoxCategory.SelectedValue = null;
                comboBoxCategory.Visible =
                    labelFrom.Visible = labelTo.Visible =
                    textBoxFrom.Visible = textBoxTo.Visible =
                    false;
            }
            else
            {
                ValueType = Filterable.ValueType;
                Index = Filterable.Index;

                bool isNumeric = Filterable.ValueType.IsDoubleConvertible();

                labelFrom.Visible = labelTo.Visible = textBoxFrom.Visible = textBoxTo.Visible = isNumeric;
                comboBoxCategory.Visible = !isNumeric;

                if (!isNumeric)
                {
                    comboBoxCategory.Items.AddRange(Filterable.GetStrings(true, true).ToArray());
                }

                FocusInput();
            }

            IsFilterSet = (Filterable != null);

            valueChanged(sender, e);
        }

        private void contextRemove_Click(object sender, EventArgs e)
        {
            Filterable = null;
        }

        private void contextRepeat_Click(object sender, EventArgs e)
        {
            if (IsFilterSet)
            {
                Sheet.Filter.ClearEmpties();
                FilterUnit fu = Sheet.Filter.AddFilter(Filterable);
            }
        }

        private void input_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!pressed && e.KeyCode == Keys.Insert)
            {
                pressed = true;
                contextRepeat_Click(sender, e);
            }
        }

        private void input_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((TextBox)sender).HandleInput(e, Filterable.ValueType);
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                e.Handled = true;
            }
        }

        private void input_KeyUp(object sender, KeyEventArgs e)
        {
            pressed = false;
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category = (string)comboBoxCategory.Text;
            valueChanged(sender, e);
        }

        private void comboBoxCategory_TextChanged(object sender, EventArgs e)
        {
            Category = (string)comboBoxCategory.Text;
            valueChanged(sender, e);
        }

        private void textBoxFrom_TextChanged(object sender, EventArgs e)
        {
            try { From = Convert.ToDouble(textBoxFrom.Text); }
            catch { From = double.NaN; }

            valueChanged(sender, e);
        }

        private void textBoxTo_TextChanged(object sender, EventArgs e)
        {
            try { To = Convert.ToDouble(textBoxTo.Text); }
            catch { To = double.NaN; }

            valueChanged(sender, e);
        }

        private void valueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, e);
            }
        }

        #endregion
    }
}
