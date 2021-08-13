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
using System.Windows.Forms.DataVisualization.Charting;
using Meta.Numerics.Statistics;
using System.Reflection;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;

namespace Mayfly.Extensions
{
    public static class DataTableExtensions
    {
        #region Getting sample of values in particular datacolumn

        public static Sample GetSample(this DataColumn dataColumn)
        {
            return dataColumn.GetSample(dataColumn.Table.Rows);
        }

        public static Sample GetSample(this DataColumn dataColumn, IEnumerable dataRows)
        {
            Sample sample = new Sample();

            foreach (DataRow dataRow in dataRows)
            {
                if (dataRow[dataColumn] == null) continue;
                if (!dataRow[dataColumn].IsDoubleConvertible()) continue;
                sample.Add(dataRow[dataColumn].ToDouble());
            }

            sample.Name = dataColumn.Caption;

            return sample;
        }

        #endregion

        #region Getting samples from column grouping by values in another column

        public static List<Sample> GetSamples(this DataColumn dataColumn) 
        {
            List<DataColumn> groupColumns = new List<DataColumn>();

            foreach (DataColumn column in dataColumn.Table.Columns)
            {
                if (column == dataColumn) continue;

                groupColumns.Add(column);
            }

            return dataColumn.GetSamples(groupColumns);
        }

        public static List<Sample> GetSamples(this DataColumn dataColumn,
            DataColumn groupColumn) 
        {
            return GetSamples(dataColumn, new List<DataColumn>() { groupColumn });
        }

        public static List<Sample> GetSamples(this DataColumn dataColumn, 
            DataColumn groupColumn, IEnumerable<DataRow> dataRows)
        {
            return dataColumn.GetSamples(new List<DataColumn>() { groupColumn }, dataRows);
        }

        public static List<Sample> GetSamples(this DataColumn dataColumn,
            List<DataColumn> groupColumns) 
        {
            if (groupColumns == null || groupColumns.Count == 0) return new List<Sample>() { dataColumn.GetSample() };            
            return dataColumn.GetSamples(groupColumns, dataColumn.GetRows());
        }

        private static List<Sample> GetSamples(this DataColumn dataColumn,
            List<DataColumn> groupColumns, IEnumerable<DataRow> dataRows) 
        {
            return dataColumn.GetSamples(groupColumns, groupColumns[0], dataRows, string.Empty);
        }

        private static List<Sample> GetSamples(this DataColumn dataColumn,
            List<DataColumn> groupColumns, DataColumn groupColumn, IEnumerable<DataRow> dataRows) 
        {
            return dataColumn.GetSamples(groupColumns, groupColumn, dataRows, string.Empty);
        }

        private static List<Sample> GetSamples(this DataColumn dataColumn,
            List<DataColumn> groupColumns, DataColumn groupColumn, IEnumerable<DataRow> dataRows, string name) 
        {
            List<Sample> result = new List<Sample>();

            foreach (string group in groupColumn.GetStrings(true))
            {
                List<DataRow> rows = dataRows.GetRows(groupColumn, group);

                if (groupColumn == groupColumns.Last())
                {
                    Sample sample = dataColumn.GetSample(rows);

                    if (name.IsAcceptable())
                    {
                        sample.Name = name + Constants.ElementSeparator + group;
                    }
                    else
                    {
                        sample.Name = group;
                    }

                    // TODO: Id empty samples are needed
                    result.Add(sample);

                    //if (sample.Count > 0) result.Add(sample);

                }
                else
                {
                    if (name.IsAcceptable())
                    {
                        result.AddRange(GetSamples(dataColumn, groupColumns,
                            groupColumns[groupColumns.IndexOf(groupColumn) + 1],
                            rows, name + Constants.ElementSeparator + group));
                    }
                    else
                    {
                        result.AddRange(GetSamples(dataColumn, groupColumns,
                            groupColumns[groupColumns.IndexOf(groupColumn) + 1],
                            rows, group));
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
