using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Extensions
{
    public static class DataTableExtensions
    {
        public static int[] SelectDistinctInteger(this DataColumn dataColumn)
        {
            List<int> result = new List<int>();
            if (dataColumn.DataType == typeof(int))
            {
                foreach (DataRow dataRow in dataColumn.Table.Rows)
                {
                    if (dataRow[dataColumn.ColumnName] != null && dataRow[dataColumn.ColumnName].ToString().IsAcceptable())
                    {
                        int currentValue = (int)dataRow[dataColumn.ColumnName];
                        if (!result.Contains(currentValue)) result.Add(currentValue);
                    }
                }
                result.Sort();
            }
            return result.ToArray();
        }

        public static void SetID(this DataRow dataRowTo, DataRow dataRowFrom)
        {
            try { dataRowTo["ID"] = dataRowFrom["ID"]; }
            catch
            {
                dataRowTo.Table.Rows.Find(dataRowFrom["ID"])["ID"] = 0;
                object t = dataRowTo["ID"];
                dataRowTo["ID"] = dataRowFrom["ID"];
                dataRowTo.Table.Rows.Find(0)["ID"] = t;
            }
        }

        public static List<DataRow> GetRows(this DataColumn dataColumn)
        {
            List<DataRow> result = new List<DataRow>();

            foreach (DataRow dataRow in dataColumn.Table.Rows)
            {
                if (dataRow[dataColumn] == null) continue;
                if (dataRow[dataColumn] is DBNull) continue;
                result.Add(dataRow);
            }

            return result;
        }

        public static List<DataRow> GetRows(this DataColumn groupColumn, string group)
        {
            List<DataRow> result = new List<DataRow>();

            foreach (DataRow dataRow in groupColumn.Table.Rows)
            {
                if (dataRow[groupColumn].ToString() != group) continue;
                result.Add(dataRow);
            }

            return result;
        }

        public static List<DataRow> GetRows(this IEnumerable<DataRow> dataRows, DataColumn groupColumn, string group)
        {
            List<DataRow> result = new List<DataRow>();

            foreach (DataRow dataRow in dataRows)
            {
                if (dataRow[groupColumn].ToString() != group) continue;
                result.Add(dataRow);
            }

            return result;
        }

        public static List<string> GetStrings(this DataColumn dataColumn)
        {
            return dataColumn.GetStrings(false);
        }

        public static List<string> GetStrings(this DataColumn dataColumn, bool distinct)
        {
            List<string> values = new List<string>();

            foreach (DataRow dataRow in dataColumn.Table.Rows)
            {
                string value = dataRow[dataColumn].ToString();                

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

            if (distinct)
            {
                values.Sort(new OmniSorter());
            }

            return values;
        }
        
        public static List<double> GetDoubles(this DataColumn dataColumn)
        {
            return dataColumn.GetDoubles(dataColumn.Table.Rows);
        }

        public static List<double> GetDoubles(this DataColumn dataColumn, IEnumerable dataRows)
        {
            if (dataColumn.DataType != typeof(double))
                throw new ArgumentException("Wrong value type");

            List<double> sample = new List<double>();

            foreach (DataRow dataRow in dataRows)
            {
                if (dataRow[dataColumn] is DBNull) continue;
                sample.Add((double)dataRow[dataColumn]);
            }

            return sample;
        }

        public static List<object> GetValues(this DataColumn dataColumn, bool distinct)
        {
            return dataColumn.GetValues(dataColumn.Table.Rows, distinct);
        }

        public static List<object> GetValues(this DataColumn dataColumn, IEnumerable dataRows, bool distinct)
        {
            List<object> values = new List<object>();

            foreach (DataRow dataRow in dataRows)
            {
                object value = dataRow[dataColumn];

                if (value is DBNull) continue;

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

            return values;
        }

        public static List<DateTime> GetDateTimes(this DataColumn dataColumn)
        {
            List<DateTime> values = new List<DateTime>();

            foreach (DataRow dataRow in dataColumn.Table.Rows)
            {
                    if (dataRow[dataColumn] is DateTime)
                    {
                        DateTime value = (DateTime)dataRow[dataColumn];

                        if (!values.Contains(value))
                        {
                            values.Add(value);
                        }
                    }
            }

            values.Sort();

            return values;
        }

        public static void SetAttributable(this DataSet dataset, params string[] exceptColumn)
        {
            foreach (DataTable dt in dataset.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (exceptColumn.Contains(dc.ColumnName)) continue;
                    dc.ColumnMapping = MappingType.Attribute;
                }
            }
        }
    }
}
