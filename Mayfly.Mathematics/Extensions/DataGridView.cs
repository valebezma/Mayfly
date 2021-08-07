using Meta.Numerics.Statistics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Extensions
{
    public static class DataGridViewExtensions
    {
        public static Sample GetSample(this DataGridViewColumn gridColumn)
        {
            return gridColumn.GetSample(gridColumn.DataGridView.Rows);
        }

        public static Sample GetSample(this DataGridViewColumn gridColumn, IList gridRows)
        {
            Sample result = new Sample(gridColumn.GetDoubles(gridRows));
            result.Name = gridColumn.HeaderText;
            return result;
        }

        public static List<Sample> GetSamples(this DataGridViewColumn gridColumn, DataGridViewColumn groupColumn, bool includeSmall)
        {
            return GetSamples(gridColumn, new List<DataGridViewColumn>() { groupColumn }, includeSmall);
        }

        public static List<Sample> GetSamples(this DataGridViewColumn gridColumn, List<DataGridViewColumn> groupColumns, bool includeSmall)
        {
            if (groupColumns == null || groupColumns.Count == 0)
            {
                return new List<Sample>() { gridColumn.GetSample() };
            }
            else
            {
                return GetSamples(gridColumn, groupColumns, groupColumns[0], gridColumn.GetRows(), string.Empty, includeSmall);
            }
        }

        public static List<Sample> GetSamples(this DataGridViewColumn gridColumn, List<DataGridViewColumn> groupColumns, DataGridViewColumn groupColumn,
            IList gridRows, string name, bool includeSmall)
        {
            List<Sample> result = new List<Sample>();

            foreach (string group in groupColumn.GetStrings(true))
            {
                List<DataGridViewRow> rows = groupColumn.GetRows(group, gridRows);

                if (groupColumn == groupColumns.Last())
                {
                    Sample sample = new Sample();

                    foreach (DataGridViewRow gridRow in rows)
                    {
                        sample.Add(gridRow.Cells[gridColumn.Index].Value.ToDouble());
                    }

                    if (name.IsAcceptable())
                    {
                        sample.Name = name + Constants.ElementSeparator + group;
                    }
                    else
                    {
                        sample.Name = group;
                    }

                    if (includeSmall || sample.Count >= Mayfly.Mathematics.UserSettings.StrongSampleSize)
                    {
                        result.Add(sample);
                    }
                }
                else
                {
                    if (name.IsAcceptable())
                    {
                        result.AddRange(GetSamples(gridColumn, groupColumns,
                            groupColumns[groupColumns.IndexOf(groupColumn) + 1],
                            rows, name + Constants.ElementSeparator + group, includeSmall));
                    }
                    else
                    {
                        result.AddRange(GetSamples(gridColumn, groupColumns,
                            groupColumns[groupColumns.IndexOf(groupColumn) + 1],
                            rows, group, includeSmall));
                    }
                }
            }

            return result;
        }
    }
}
