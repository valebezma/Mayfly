using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Controls
{
    public partial class ColumnProperties : Form
    {
        public DataGridViewColumn Column { set; get; }

        public Type SelectedType
        {
            set
            {
                if (value == typeof(string))
                    comboBoxType.SelectedIndex = 0;
                else if (value == typeof(DateTime))
                    comboBoxType.SelectedIndex = 1;
                else if (value == typeof(int))
                    comboBoxType.SelectedIndex = 2;
                else if (value == typeof(double))
                    comboBoxType.SelectedIndex = 3;
                else comboBoxType.SelectedIndex = 4;
            }

            get
            {
                switch (comboBoxType.SelectedIndex)
                {
                    case 0:
                        return typeof(string);
                    case 1:
                        return typeof(DateTime);
                    case 2:
                        return typeof(int);
                    case 3:
                        return typeof(double);
                }

                return null;
            }
        }

        public string Format
        {
            get
            {
                return textBoxFormat.Text;
            }

            set
            {
                textBoxFormat.Text = value;
                ApplyControls(value);
            }
        }

        public DataGridViewContentAlignment Alignment
        {
            get
            {
                switch (comboBoxHorizontal.SelectedIndex)
                {
                    case 0:
                        switch (comboBoxVertical.SelectedIndex)
                        {
                            case 2:
                                return DataGridViewContentAlignment.BottomLeft;
                            case 1:
                                return DataGridViewContentAlignment.MiddleLeft;
                            case 0:
                                return DataGridViewContentAlignment.TopLeft;
                        }
                        break;

                    case 1:
                        switch (comboBoxVertical.SelectedIndex)
                        {
                            case 2:
                                return DataGridViewContentAlignment.BottomCenter;
                            case 1:
                                return DataGridViewContentAlignment.MiddleCenter;
                            case 0:
                                return DataGridViewContentAlignment.TopCenter;
                        }
                        break;

                    case 2:
                        switch (comboBoxVertical.SelectedIndex)
                        {
                            case 2:
                                return DataGridViewContentAlignment.BottomRight;
                            case 1:
                                return DataGridViewContentAlignment.MiddleRight;
                            case 0:
                                return DataGridViewContentAlignment.TopRight;
                        }
                        break;
                }

                return DataGridViewContentAlignment.NotSet;
            }

            set
            {
                switch (value)
                {
                    case DataGridViewContentAlignment.BottomLeft:
                        comboBoxHorizontal.SelectedIndex = 0;
                        comboBoxVertical.SelectedIndex = 2;
                        break;
                    case DataGridViewContentAlignment.BottomCenter:
                        comboBoxHorizontal.SelectedIndex = 1;
                        comboBoxVertical.SelectedIndex = 2;
                        break;
                    case DataGridViewContentAlignment.BottomRight:
                        comboBoxHorizontal.SelectedIndex = 2;
                        comboBoxVertical.SelectedIndex = 2;
                        break;
                    case DataGridViewContentAlignment.MiddleLeft:
                        comboBoxHorizontal.SelectedIndex = 0;
                        comboBoxVertical.SelectedIndex = 1;
                        break;
                    case DataGridViewContentAlignment.MiddleCenter:
                        comboBoxHorizontal.SelectedIndex = 1;
                        comboBoxVertical.SelectedIndex = 1;
                        break;
                    case DataGridViewContentAlignment.MiddleRight:
                        comboBoxHorizontal.SelectedIndex = 2;
                        comboBoxVertical.SelectedIndex = 1;
                        break;
                    case DataGridViewContentAlignment.TopLeft:
                        comboBoxHorizontal.SelectedIndex = 0;
                        comboBoxVertical.SelectedIndex = 0;
                        break;
                    case DataGridViewContentAlignment.TopCenter:
                        comboBoxHorizontal.SelectedIndex = 1;
                        comboBoxVertical.SelectedIndex = 0;
                        break;
                    case DataGridViewContentAlignment.TopRight:
                        comboBoxHorizontal.SelectedIndex = 2;
                        comboBoxVertical.SelectedIndex = 0;
                        break;
                    default:
                        comboBoxHorizontal.SelectedIndex = -1;
                        comboBoxVertical.SelectedIndex = -1;
                        break;
                }
            }
        }



        public ColumnProperties(DataGridViewColumn column)
        {
            InitializeComponent();

            comboBoxDateFormatOption.SelectedIndex = 0;

            Column = column;
            textBoxHeader.Text = column.HeaderText;

            if (column.DefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet)
                Alignment = column.DataGridView.DefaultCellStyle.Alignment;
            else Alignment = column.DefaultCellStyle.Alignment;

            comboBoxType.Enabled = labelType.Enabled = column.GetValues(false, true).Count == 0;
            SelectedType = column.ValueType;

            if (column.DefaultCellStyle.Format.IsAcceptable()) Format = column.DefaultCellStyle.Format;
            else Format = column.DataGridView.DefaultCellStyle.Format;
        }



        private void ApplyControls(string value)
        {
            if (SelectedType == typeof(double))
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrWhiteSpace(value)) return;

                try
                {
                    numericUpDownDecimals.Value = decimal.Parse(value.Substring(1));
                }
                catch
                {
                    numericUpDownDecimals.Value = 2;
                }
                checkBoxPercentage.Checked = (value[0] == 'P');
            }

            if (SelectedType == typeof(DateTime))
            {
                string[] args = value.Split(' ');

                if (args.Length > 1 && args[0] == "collapse")
                {
                    checkBoxDateCollapse.Checked = true;

                    switch (args[1])
                    {
                        case "daytime":
                            comboBoxDateFormat.SelectedIndex = 4;
                            break;

                        case "day":
                            comboBoxDateFormat.SelectedIndex = 5;
                            break;

                        case "week":
                            comboBoxDateFormat.SelectedIndex = 6;
                            break;

                        case "month":
                            comboBoxDateFormat.SelectedIndex = 7;
                            break;

                        case "season":
                            comboBoxDateFormat.SelectedIndex = 8;
                            break;

                        case "year":
                            comboBoxDateFormat.SelectedIndex = 9;
                            break;

                        case "decade":
                            comboBoxDateFormat.SelectedIndex = 10;
                            break;
                    }

                    if (args.Length == 4)
                    {
                        switch (args[3])
                        {
                            case "week":
                                comboBoxDateFormatOption.SelectedIndex = 0;
                                break;
                            case "month":
                                comboBoxDateFormatOption.SelectedIndex = 1;
                                break;
                            case "year":
                                comboBoxDateFormatOption.SelectedIndex = 2;
                                break;
                        }
                    }
                }
                else
                {
                    switch (value)
                    {
                        case "G":
                            comboBoxDateFormat.SelectedIndex = 0;
                            break;
                        case "sec":
                            comboBoxDateFormat.SelectedIndex = 0;
                            checkBoxDateCollapse.Checked = true;
                            break;
                        case "g":
                            comboBoxDateFormat.SelectedIndex = 1;
                            break;
                        case "min":
                            comboBoxDateFormat.SelectedIndex = 1;
                            checkBoxDateCollapse.Checked = true;
                            break;
                        case "hour":
                            comboBoxDateFormat.SelectedIndex = 2;
                            break;
                        case "H":
                            comboBoxDateFormat.SelectedIndex = 2;
                            checkBoxDateCollapse.Checked = true;
                            break;
                        case "time":
                            comboBoxDateFormat.SelectedIndex = 3;
                            break;
                        case "T":
                            comboBoxDateFormat.SelectedIndex = 1;
                            checkBoxDateCollapse.Checked = true;
                            break;
                        case "daytime":
                            comboBoxDateFormat.SelectedIndex = 4;
                            break;
                        case "d":
                            comboBoxDateFormat.SelectedIndex = 5;
                            break;
                        case "week":
                            comboBoxDateFormat.SelectedIndex = 6;
                            break;
                        case "y":
                            comboBoxDateFormat.SelectedIndex = 7;
                            break;
                        case "season":
                            comboBoxDateFormat.SelectedIndex = 8;
                            break;
                        case "yyyy":
                            comboBoxDateFormat.SelectedIndex = 9;
                            break;
                        case "decade":
                            comboBoxDateFormat.SelectedIndex = 10;
                            break;
                    }
                }
            }
        }

        private string GetFormatFromControls()
        {
            if (SelectedType == typeof(int))
            {
                return "N0";
            }

            if (SelectedType == typeof(double))
            {
                if (checkBoxPercentage.Checked) {
                    return "P" + numericUpDownDecimals.Value;
                } else {
                    return "N" + numericUpDownDecimals.Value;
                }
            }

            if (SelectedType == typeof(DateTime))
            {
                return Extensions.DateTimeExtensions.GetFormat(
                    (PeriodType)comboBoxDateFormat.SelectedIndex, checkBoxDateCollapse.Checked,
                    (PeriodType)comboBoxDateFormatOption.SelectedIndex);
            }

            return string.Empty;
        }



        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelDecimal.Visible = numericUpDownDecimals.Visible = checkBoxPercentage.Visible =
                SelectedType == typeof(double);

            labelDatePart.Visible = comboBoxDateFormat.Visible = checkBoxDateCollapse.Visible =
                SelectedType == typeof(DateTime);


            if (sender != comboBoxHorizontal)
            {
                if (SelectedType == typeof(string))
                {
                    comboBoxHorizontal.SelectedIndex = 0;
                }
                else if (SelectedType == typeof(double) || SelectedType == typeof(int))
                {
                    comboBoxHorizontal.SelectedIndex = 2;
                }
            }

            textBoxFormat.ReadOnly = SelectedType != null;
            Format = GetFormatFromControls();
        }

        private void comboBoxDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxDateFormatOption.Visible = checkBoxDateCollapse.Checked &&
                (comboBoxDateFormat.SelectedIndex == 5 || comboBoxDateFormat.SelectedIndex == 6);

            Format = GetFormatFromControls();
        }

        private void comboBoxDateFormatOption_VisibleChanged(object sender, EventArgs e)
        {
            if (comboBoxDateFormatOption.Visible)
            {
                comboBoxDateFormat.Width = Width - 374;
            }
            else
            {
                comboBoxDateFormat.Width = Width - 268;
            }
        }

        private void comboBoxDateFormatOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDateFormat.SelectedIndex == 5 && comboBoxDateFormatOption.SelectedIndex == 0)
            {
                comboBoxDateFormatOption.SelectedIndex = 1;
            }

            Format = GetFormatFromControls();
        }

        private void numericUpDownDecimals_ValueChanged(object sender, EventArgs e)
        {
            Format = GetFormatFromControls();
        }


        private void buttonApply_Click(object sender, EventArgs e)
        {
            Column.HeaderText = textBoxHeader.Text;
            if (comboBoxType.Enabled) Column.ValueType = SelectedType;
            Column.DefaultCellStyle.Format = Format;
            Column.DefaultCellStyle.Alignment = Alignment;

            Column.SaveFormat();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
            Close();
        }
    }
}
