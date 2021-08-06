using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Mayfly.Extensions
{
    public static class DataGridViewExtensions
    {
        public static void SaveFormat(this DataGridViewColumn gridColumn)
        {
            UI.SaveFormat(gridColumn.DataGridView.Name, gridColumn.Name, gridColumn.DefaultCellStyle.Format);
        }

        public static void RestoreFormat(this DataGridViewColumn gridColumn)
        {
            string stored = UI.GetFormat(gridColumn.DataGridView.Name, gridColumn.Name, string.Empty);
            if (stored != string.Empty) 
                gridColumn.DefaultCellStyle.Format = stored;
        }

        public static string ExtendFormat(this DataGridViewColumn gridColumn, string units)
        {
            string inheritedFormat = gridColumn.DefaultCellStyle.Format;
            if (string.IsNullOrWhiteSpace(inheritedFormat)) inheritedFormat = gridColumn.DataGridView.DefaultCellStyle.Format;
            if (string.IsNullOrWhiteSpace(inheritedFormat)) inheritedFormat = "0";

            if (inheritedFormat.StartsWith("N"))
            {
                int decimals = Convert.ToInt32(inheritedFormat.Substring(1));
                inheritedFormat = "0.";

                while (decimals > 0)
                {
                    inheritedFormat += "0";
                    decimals--;
                }
            }


            return  inheritedFormat + " \"" + units + "\"";
        }



        public static void SaveToFile(this DataGridView sheet, string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.Default);

            switch (Path.GetExtension(fileName))
            {
                case ".csv":
                    streamWriter.Write(sheet.SeparatedValues(CultureInfo.CurrentCulture.TextInfo.ListSeparator));
                    //streamWriter.Write(sheet.SeparatedValues(";"));
                    break;
                case ".txt":
                    streamWriter.Write(sheet.SeparatedValues(Constants.Tab));
                    break;
                case ".prn":
                    streamWriter.Write(sheet.PrintableFile());
                    break;
            }

            streamWriter.Flush();
            streamWriter.Close();
        }

        public static string SeparatedValues(this DataGridView sheet, string separator)
        {
            return sheet.SeparatedValues(separator, !Form.ModifierKeys.HasFlag(Keys.Control), false, false);
        }

        public static string SeparatedValues(this DataGridView sheet, string separator, bool formatted,
            bool includeInvisibleColumns, bool includeInvisibleRows)
        {
            StringWriter result = new StringWriter();

            string headerText = string.Empty;

            for (int i = 0; i <= sheet.ColumnCount - 1; i++)
            {
                if (includeInvisibleColumns || sheet.Columns[i].Visible)
                {
                    if (i > 0) headerText += separator;
                    headerText += ValueToFile(sheet.Columns[i].HeaderText);
                }
            }

            result.WriteLine(headerText);

            foreach (DataGridViewRow gridRow in sheet.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (!(includeInvisibleRows || gridRow.Visible)) continue;

                string rowValues = string.Empty;

                for (int i = 0; i <= sheet.ColumnCount - 1; i++)
                {
                    if (!(includeInvisibleColumns || sheet.Columns[i].Visible)) continue;

                    if (i > 0) rowValues += separator;

                    if (gridRow.Cells[i].Value == null) continue;

                    rowValues += ValueToFile(formatted ? gridRow.Cells[i].FormattedValue : gridRow.Cells[i].Value);
                }

                result.WriteLine(rowValues);
            }

            return result.ToString();
        }

        public static string PrintableFile(this DataGridView sheet)
        {
            StringWriter result = new StringWriter();

            List<int> lengths = new List<int>();

            string headerText = string.Empty;

            for (int i = 0; i <= sheet.ColumnCount - 1; i++)
            {
                if (!sheet.Columns[i].Visible) continue;

                object[] values = sheet.Columns[i].GetValues(true, false).ToArray();

                int length = sheet.Columns[i].HeaderText.Length;

                foreach (object value in values)
                {
                    length = System.Math.Max(length, value.ToString().Length);
                }

                lengths.Add(length);
            }

            for (int i = 0; i <= sheet.ColumnCount - 1; i++)
            {
                if (!sheet.Columns[i].Visible) continue;

                headerText += string.Format("{0,-" + lengths[i] + "}", sheet.Columns[i].HeaderText);
            }

            result.WriteLine(headerText);

            foreach (DataGridViewRow gridRow in sheet.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (!gridRow.Visible) continue;

                string rowValues = string.Empty;

                for (int i = 0; i <= sheet.ColumnCount - 1; i++)
                {
                    if (!sheet.Columns[i].Visible) continue;

                    rowValues += string.Format("{0,-" + lengths[i] + "}", gridRow.Cells[i].Value);
                }

                result.WriteLine(rowValues);
            }

            return result.ToString();
        }

        private static string ValueToFile(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }





        public static List<DataGridViewRow> GetRows(this DataGridViewColumn gridColumn)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                //if (!gridRow.Visible) continue;
                if (gridRow.Cells[gridColumn.Index].Value == null) continue;
                result.Add(gridRow);
            }

            return result;
        }

        public static List<DataGridViewRow> GetRows(this DataGridViewColumn groupColumn, string group)
        {
            return GetRows(groupColumn, group, groupColumn.DataGridView.Rows);
        }

        public static List<DataGridViewRow> GetRows(this DataGridViewColumn groupColumn, string group, bool exploreInvisibles)
        {
            return GetRows(groupColumn, group, groupColumn.DataGridView.Rows, exploreInvisibles);
        }

        public static List<DataGridViewRow> GetRows(this DataGridViewColumn groupColumn, string group, IList gridRows)
        {
            return GetRows(groupColumn, group, gridRows, false);
        }

        public static List<DataGridViewRow> GetRows(this DataGridViewColumn groupColumn, string group, IList gridRows, bool exploreInvisibles)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in gridRows)
            {
                if (gridRow.IsNewRow) continue;
                if (!gridRow.Visible && !exploreInvisibles) continue;
                if (gridRow.Cells[groupColumn.Index].FormattedValue.ToString() != group) continue;
                result.Add(gridRow);
            }

            return result;
        }

        public static DataGridViewRow GetRow(this DataGridViewColumn groupColumn, int id)
        {
            return GetRow(groupColumn, id, groupColumn.DataGridView.Rows);
        }

        public static DataGridViewRow GetRow(this DataGridViewColumn groupColumn, int id, bool exploreInvisibles)
        {
            return GetRow(groupColumn, id, groupColumn.DataGridView.Rows, exploreInvisibles, false);
        }

        public static DataGridViewRow GetRow(this DataGridViewColumn groupColumn, int id, bool exploreInvisibles, bool createIfAbsent)
        {
            return GetRow(groupColumn, id, groupColumn.DataGridView.Rows, exploreInvisibles, createIfAbsent);
        }

        public static DataGridViewRow GetRow(this DataGridViewColumn groupColumn, int id, IList gridRows)
        {
            return GetRow(groupColumn, id, gridRows, false, false);
        }

        public static DataGridViewRow GetRow(this DataGridViewColumn groupColumn, int id, IList gridRows, bool exploreInvisibles, bool createIfAbsent)
        {
            foreach (DataGridViewRow gridRow in gridRows)
            {
                if (gridRow.IsNewRow) continue;
                if (!gridRow.Visible && !exploreInvisibles) continue;
                if (!(gridRow.Cells[groupColumn.Index].Value is int)) continue;
                if ((int)gridRow.Cells[groupColumn.Index].Value != id) continue;
                return gridRow;
            }

            if (createIfAbsent)
            {
                DataGridViewRow result = new DataGridViewRow();
                result.CreateCells(groupColumn.DataGridView);
                result.Cells[groupColumn.Index].Value = id;
                return result;
            }

            return null;
        }

        public static List<double> GetDoubles(this DataGridViewColumn gridColumn)
        {
            return gridColumn.GetDoubles(gridColumn.DataGridView.Rows);
        }

        public static List<double> GetDoubles(this DataGridViewColumn gridColumn, IList gridRows)
        {
            List<double> sample = new List<double>();

            foreach (DataGridViewRow gridRow in gridRows)
            {
                if (gridRow.IsNewRow) continue;
                //if (!gridRow.Visible) continue;
                if (gridRow.Cells[gridColumn.Index].Value == null) continue;
                if (!gridRow.Cells[gridColumn.Index].Value.IsDoubleConvertible()) continue;
                sample.Add(gridRow.Cells[gridColumn.Index].Value.ToDouble());
            }

            return sample;
        }

        public static List<string> GetStrings(this DataGridViewColumn gridColumn)
        {
            return gridColumn.GetStrings(false);
        }

        public static List<string> GetStrings(this DataGridViewColumn gridColumn, bool distinct)
        {
            return GetStrings(gridColumn, distinct, false);
        }

        public static List<string> GetStrings(this DataGridViewColumn gridColumn, bool distinct, bool exploreInvisibles)
        {
            List<object> f = gridColumn.GetValues(distinct, exploreInvisibles);

            List<string> values = new List<string>();

            if (gridColumn.DataGridView == null) return values;

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                if (gridRow.IsNewRow) continue;
                
                string value = string.Empty;

                //if (gridRow.Cells[gridColumn.Index].Value == null)
                //{
                //    continue;
                //}

                if (exploreInvisibles || gridRow.Visible)
                {
                    if (gridRow.Cells[gridColumn.Index].Value == null) value = Mayfly.Resources.Interface.EmptyValue;
                    else value = gridRow.Cells[gridColumn.Index].FormattedValue.ToString();

                    //if (string.IsNullOrWhiteSpace(value)) continue;

                    if (distinct)
                    {
                        if (!values.Contains(value))
                        {
                            values.Add(value);
                        }
                    }
                    else
                    {
                        values.Add(value);
                    }
                }
            }

            if (distinct)
            {
                values.Sort(new OmniSorter());
            }

            return values;
        }

        public static List<object> GetValues(this DataGridViewColumn gridColumn, bool distinct, bool exploreInvisibles)
        {
            List<object> values = new List<object>();

            if (gridColumn.DataGridView == null) return values;

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (exploreInvisibles || gridRow.Visible)
                {
                    object value = gridColumn.DataGridView[gridColumn.Index, gridRow.Index].Value;

                    if (value == null) continue;

                    if (distinct)
                    {
                        if (!values.Contains(value))
                        {
                            values.Add(value);
                        }
                    }
                    else
                    {
                        values.Add(value);
                    }
                }
            }

            return values;
        }

        public static List<DateTime> GetDates(this DataGridViewColumn gridColumn, bool exploreInvisibles)
        {
            List<DateTime> values = new List<DateTime>();

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                if (exploreInvisibles || gridRow.Visible)
                {
                    if (gridRow.Cells[gridColumn.Index].Value is DateTime)
                    {
                        DateTime value = (DateTime)gridRow.Cells[gridColumn.Index].Value;

                        if (!values.Contains(value))
                        {
                            values.Add(value);
                        }
                    }
                }
            }

            values.Sort();

            return values;
        }

        public static Rectangle GetCurrentCellRectangle(this DataGridView grid, bool cutOverflow)
        {
            return grid.GetCellDisplayRectangle(grid.CurrentCell.ColumnIndex,
                grid.CurrentCell.RowIndex, cutOverflow);
        }

        public static object EnterValue(this DataGridViewCell gridCell, string value)
        {
            if (gridCell.OwningColumn.ValueType == typeof(string))
            {
                gridCell.Value = value;
            }
            else
            {
                try
                {
                    gridCell.Value = Convert.ChangeType(value, gridCell.OwningColumn.ValueType);
                }
                catch //(InvalidCastException)
                {
                    //    gridCell.Value = null;
                    try
                    {
                        MethodInfo parseMethod = gridCell.OwningColumn.ValueType.GetMethod("Parse");
                        gridCell.Value = parseMethod.Invoke(null, new object[] { value });
                    }
                    catch
                    {
                        gridCell.Value = null;
                    }
                }
            }

            return gridCell.Value;
        }
    }
}
