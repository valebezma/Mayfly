using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Mayfly.Geographics;

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
                    if (gridRow.Cells[gridColumn.Index].Value == null) value = Resources.Interface.EmptyValue;
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
                catch
                {
                    try
                    {
                        MethodInfo parseMethod = gridCell.OwningColumn.ValueType.GetMethod("Parse");
                        gridCell.Value = parseMethod?.Invoke(null, new object[] { value });
                    }
                    catch
                    {
                        gridCell.Value = null;
                    }
                }

                //if (gridCell.OwningColumn.ValueType == typeof(Waypoint))
                //{
                //    gridCell.Value = Waypoint.Parse(value);
                //}
            }

            return gridCell.Value;
        }
    }
}
