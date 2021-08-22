using Mayfly.Extensions;
using Meta.Numerics.Statistics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Mayfly.Mathematics.Statistics;
using System.Globalization;
using Meta.Numerics;
using RDotNet;

namespace Mayfly.Mathematics
{
    public static class Service
    {
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int n)
        {
            if (n == 0)
                yield return Enumerable.Empty<T>();


            int count = 1;
            foreach (T item in source)
            {
                foreach (var innerSequence in source.Skip(count).Combinations(n - 1))
                {
                    yield return new T[] { item }.Concat(innerSequence);
                }
                count++;
            }
        }
        
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int n, int length)
        {
            if (n == 0)
                yield return Enumerable.Empty<T>();


            int count = 1;
            foreach (T item in source)
            {
                foreach (var innerSequence in source.Skip(count).Combinations(n - 1))
                {
                    yield return new T[] { item }.Concat(innerSequence);
                }
                count++;
                if (count == length) break;
            }
        }

        /// <summary>
        /// Gets first row which have given values in given columns
        /// </summary>
        /// <param name="gridColumns"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataGridViewRow GetRow(DataGridViewColumn[] gridColumns, double[] values)
        {
            foreach (DataGridViewRow gridRow in gridColumns[0].DataGridView.Rows)
            {
                if (!gridRow.Visible) continue;

                bool fit = true;

                for (int i = 0; i < gridColumns.Length; i++)
                {
                    if (gridRow.Cells[gridColumns[i].Index].Value == null)
                    {
                        fit = false;
                        continue;
                    }

                    if (gridColumns[i].ValueType == typeof(double))
                    {
                        if ((double)gridRow.Cells[gridColumns[i].Index].Value != values[i])
                        {
                            fit = false;
                            continue;
                        }
                    }
                    else
                    {
                        MethodInfo parseMethod = gridColumns[i].ValueType.GetMethod("FromDouble");
                        object cellValue = parseMethod.Invoke(null, new object[] { values[i] });

                        if (!gridRow.Cells[gridColumns[i].Index].Value.Equals(cellValue))
                        {
                            fit = false;
                            continue;
                        }
                    }
                }

                if (fit)
                {
                    return gridRow;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns values from Grouper column referring with non NULL values of column
        /// </summary>
        /// <param name="gridColumn">Where to check values</param>
        /// <param name="groupColumn">Where from to get results</param>
        /// <param name="exploreInvisibles">If true searches in invisible rows</param>
        /// <returns>List of Grouper values</returns>
        public static List<string> GetColumnGroups(DataGridViewColumn gridColumn, DataGridViewColumn groupColumn,
            bool exploreInvisibles)
        {
            List<string> values = new List<string>();

            foreach (DataGridViewRow gridRow in gridColumn.DataGridView.Rows)
            {
                if (exploreInvisibles || gridRow.Visible)
                {
                    if (gridRow.Cells[gridColumn.Index].Value != null)
                    {
                        values.Add(gridRow.Cells[groupColumn.Index].Value.ToString());
                    }
                }
            }

            return values;
        }

        public static List<object>[] GetColumnPairedValues(DataGridViewColumn Column1, DataGridViewColumn Column2,
            bool exploreInvisibles)
        {
            List<object> values1 = new List<object>();
            List<object> values2 = new List<object>();

            foreach (DataGridViewRow gridRow in Column1.DataGridView.Rows)
            {
                if (exploreInvisibles || gridRow.Visible)
                {
                    if (gridRow.Cells[Column1.Index].Value != null && gridRow.Cells[Column2.Index].Value != null)
                    {
                        values1.Add(gridRow.Cells[Column1.Index].Value);
                        values2.Add(gridRow.Cells[Column2.Index].Value);
                    }
                }
            }

            return new List<object>[] { values1, values2 };
        }

        public static List<object> GetColumnPairedValues(DataGridViewColumn Column1, DataGridViewColumn Column2,
            DataGridViewColumn groupColumn, bool exploreInvisibles)
        {
            List<object> result = new List<object>();

            foreach (DataGridViewRow gridRow in Column1.DataGridView.Rows)
            {
                if (exploreInvisibles || gridRow.Visible)
                {
                    if (gridRow.Cells[Column1.Index].Value != null && gridRow.Cells[Column2.Index].Value != null)
                    {
                        result.Add(gridRow.Cells[groupColumn.Index].Value);
                    }
                }
            }

            return result;
        }

        public static BivariateSample GetSample(DataGridViewColumn xColumn, DataGridViewColumn yColumn)
        {
            BivariateSample result = new BivariateSample();

            foreach (DataGridViewRow gridRow in xColumn.DataGridView.Rows)
            {
                if (!gridRow.Visible) continue;
                if (!gridRow.Cells[xColumn.Index].Value.IsDoubleConvertible()) continue;
                if (!gridRow.Cells[yColumn.Index].Value.IsDoubleConvertible()) continue;
                result.Add(gridRow.Cells[xColumn.Index].Value.ToDouble(),
                    gridRow.Cells[yColumn.Index].Value.ToDouble());
            }

            result.X.Name = xColumn.HeaderText;
            result.Y.Name = yColumn.HeaderText;

            return result;
        }

        public static List<BivariateSample> GetSamples(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> groupColumns)
        {
            // 2 - If groupers are not presented - return array from Values column
            if (groupColumns.Count == 0)
            {
                return new List<BivariateSample>() { GetSample(xColumn, yColumn) };
            }
            else
            {
                // 1 - Select rows
                List<DataGridViewRow> gridRows = new List<DataGridViewRow>();
                foreach (DataGridViewRow gridRow in xColumn.DataGridView.Rows)
                {
                    //if (!gridRow.Visible) continue;
                    if (!gridRow.Cells[xColumn.Index].Value.IsDoubleConvertible()) continue;
                    if (!gridRow.Cells[yColumn.Index].Value.IsDoubleConvertible()) continue;
                    gridRows.Add(gridRow);
                }
                // 3 - If groupers are presented - launch recursive function
                return GetSamples(xColumn, yColumn, groupColumns, groupColumns[0], gridRows, string.Empty);
            }
        }

        public static List<BivariateSample> GetSamples(DataGridViewColumn xColumn, DataGridViewColumn yColumn,
            List<DataGridViewColumn> groupColumns, DataGridViewColumn groupColumn,
            List<DataGridViewRow> gridRows, string name)
        {
            List<BivariateSample> result = new List<BivariateSample>();

            foreach (string group in groupColumn.GetStrings(true))
            {
                List<DataGridViewRow> rows = groupColumn.GetRows(group, gridRows);

                if (groupColumn == groupColumns.Last())
                {
                    BivariateSample bivariateSample = new BivariateSample();

                    foreach (DataGridViewRow gridRow in rows)
                    {
                        bivariateSample.Add(gridRow.Cells[xColumn.Index].Value.ToDouble(),
                            gridRow.Cells[yColumn.Index].Value.ToDouble());
                    }

                    if (name.IsAcceptable())
                    {
                        bivariateSample.X.Name = name + Constants.ElementSeparator + group;
                    }
                    else
                    {
                        bivariateSample.X.Name = group;
                    }

                    result.Add(bivariateSample);
                }
                else
                {
                    if (name.IsAcceptable())
                    {
                        result.AddRange(GetSamples(xColumn, yColumn, groupColumns,
                            groupColumns[groupColumns.IndexOf(groupColumn) + 1],
                            rows, name + Constants.ElementSeparator + group));
                    }
                    else
                    {
                        result.AddRange(GetSamples(xColumn, yColumn, groupColumns,
                            groupColumns[groupColumns.IndexOf(groupColumn) + 1],
                            rows, group));
                    }
                }
            }

            return result;
        }







        public static double t_cr(int df, double alpha)
        {
            return new StudentDistribution(df).InverseRightProbability(alpha);
        }

        public static double q_cr(int k, int df, double alpha)
        {
            return Math.Sqrt(2) * t_cr(df, alpha / 2);
            //double t05 = StudentCriticalT(df, alpha / 2);
            //double value1 = val1(k, alpha);
            //double value2 = val2(k, alpha);
            //return value1 * t05 - value2;
        }

        public static double val1(int k, double alpha)
        {
            double q05 = 0;
            double q01 = 0;

            // .05  y = 1,6898 * ln(k) + 0,2483
            // .01  y = 1,3047 * ln(k) + 0,5027

            if (k == 3)
            {
                q05 = 2.106973;
                q01 = 1.946821;
            }

            if (k == 4)
            {
                q05 = 2.59017;
                q01 = 2.303537;
            }

            if (k == 5)
            {
                q05 = 2.972018;
                q01 = 2.587002;
            }

            if (k == 6)
            {
                q05 = 3.27313260295899;
                q01 = 2.85765551110653;
            }

            if (k == 7)
            {
                q05 = 3.53785626144803;
                q01 = 3.04988915773087;
            }

            if (k == 8)
            {
                q05 = 3.75342544999978;
                q01 = 3.21818903063174;
            }

            if (k == 9)
            {
                q05 = 3.94085173471745;
                q01 = 3.37614586515416;
            }

            if (k == 10)
            {
                q05 = 4.1063693875735;
                q01 = 3.4911129101093;
            }

            if (alpha == .01) return q01;

            return q05;
        }

        public static double val2(int k, double alpha)
        {
            double q05 = 0;
            double q01 = 0;

            if (k == 3)
            {
                q05 = 0.817944;
                q01 = 0.8996111;
            }

            if (k == 4)
            {
                q05 = 1.443969;
                q01 = 1.534593;
            }

            if (k == 5)
            {
                q05 = 1.96814;
                q01 = 2.064423;
            }

            if (k == 6)
            {
                q05 = 2.38279010928828;
                q01 = 2.61819815698612;
            }

            if (k == 7)
            {
                q05 = 2.76274478043922;
                q01 = 2.98527857882349;
            }

            if (k == 8)
            {
                q05 = 3.06529742825939;
                q01 = 3.31378857989615;
            }

            if (k == 9)
            {
                q05 = 3.32653946392437;
                q01 = 3.64658532621697;
            }

            if (k == 10)
            {
                q05 = 3.559775633569;
                q01 = 3.8429755454712;
            }

            if (alpha == .01) return q01;

            return q05;
        }

        public static DataTable LoadFromFile(string fileName)
        {
            DataTable table = new DataTable();

            List<string> lines = new List<string>(File.ReadAllLines(fileName, Encoding.Default));

            string[] headers = new string[] { };
            switch (Path.GetExtension(fileName))
            {
                case ".csv":
                    headers = lines[0].Split(';');
                    break;
                case ".txt":
                    headers = lines[0].Split('\t');
                    break;
            }

            foreach (string header in headers)
            {
                table.Columns.Add(header.Trim('\"'), typeof(double));
            }

            string[] valueLines = new string[lines.Count - 1];
            lines.CopyTo(1, valueLines, 0, lines.Count - 1);


            foreach (string valueLine in valueLines)
            {
                string[] values = new string[] { };

                switch (Path.GetExtension(fileName))
                {
                    case ".csv":
                        values = valueLine.Split(';');
                        break;
                    case ".txt":
                        values = valueLine.Split('\t');
                        break;
                }

            Repeat:
                DataRow dataRow = table.NewRow();

                for (int i = 0; i < values.Length; i++)
                {
                    if (!values[i].IsAcceptable()) continue;

                    try
                    {
                        dataRow[i] = Convert.ChangeType(values[i], table.Columns[i].DataType);
                    }
                    catch (FormatException)
                    {
                        DataTable dtCloned = table.Clone();
                        dtCloned.Columns[i].DataType = dtCloned.Columns[i].DataType == typeof(DateTime) ? typeof(string) : typeof(DateTime);
                        foreach (DataRow row in table.Rows)
                        {
                            dtCloned.ImportRow(row);
                        }

                        table = dtCloned;
                        goto Repeat;
                    }
                }

                table.Rows.Add(dataRow);
            }

        Revise:
            foreach (DataColumn dataColumn in table.Columns)
            {
                if (dataColumn.DataType != typeof(double)) continue;

                List<double> values = dataColumn.GetDoubles();

                if (values.Precision() == 0)
                {
                    DataTable dtCloned = table.Clone();
                    int columnIndex = table.Columns.IndexOf(dataColumn);
                    dtCloned.Columns[columnIndex].DataType = typeof(int);
                    foreach (DataRow row in table.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }

                    table = dtCloned;

                    goto Revise;
                }
            }

            return table;
        }

        public static int GetGCD(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            if (a > b)
                return GetGCD(a % b, b);
            else
                return GetGCD(a, b % a);
        }

        public static Interval GetStrate(double length, double interval)
        {
            double min = Math.Floor(length / interval) * interval;
            return Interval.FromEndpointAndWidth(min, interval);
        }



        public static string[] GetRegressionTypes()
        {
            List<string> result = new List<string>();

            foreach (TrendType type in Enum.GetValues(typeof(TrendType)))
            {
                string name = Resources.RegressionType.ResourceManager.GetString(type.ToString());
                result.Add(string.IsNullOrWhiteSpace(name) ? type.ToString() : name);
            }

            return result.ToArray();
        }



        public static string PresentError(double value)
        {
            return PresentError(value, CultureInfo.CurrentCulture);
        }

        public static string PresentError(double value, string format)
        {
            return PresentError(value, CultureInfo.CurrentCulture, format);
        }

        public static string PresentError(double value, IFormatProvider provider)
        {
            return PresentError(value, CultureInfo.CurrentCulture, string.Empty);
        }

        public static string PresentError(double value, IFormatProvider provider, string format)
        {
            if (value > 1)
            {
                double fraction = value - (long)value;
                return ((long)value).ToString(provider) + PresentError(fraction, provider, format).TrimStart('0');
            }

            if (string.IsNullOrEmpty(format))
            {
                if (value.ToString("e1", provider).StartsWith("1"))
                {
                    return value.ToString("G2", provider);
                }
                else
                {
                    return value.ToString("G1", provider);
                }
            }
            else
            {
                return value.ToString(format, provider);
            }
        }


        public static void InstallRequiredPackages(this REngine engine, params string[] package)
        {
            var packagelist = engine.CreateCharacterVector(package);
            engine.SetSymbol("list.of.packages", packagelist);
            engine.Evaluate("new.packages = list.of.packages[!(list.of.packages %in% installed.packages()[, 'Package'])]");
            engine.Evaluate("if (length(new.packages)) install.packages(new.packages)");
        }
    }
}

// TODO i

//foreach (Sample sample in Samples)
//{
//    if (sample.Count >= UserSettings.SampleSizeMinimum)
//    {
//        // Create chart area and set visual attributes
//        ChartArea areaPoint = new ChartArea();
//        areaPoint.BackColor = Color.Transparent;
//        areaPoint.BorderColor = Color.Black;
//        areaPoint.BorderWidth = 1;
//        areaPoint.AxisX.LineWidth = 0;
//        areaPoint.AxisY.LineWidth = 0;
//        areaPoint.AxisX.MajorGrid.Enabled = false;
//        areaPoint.AxisX.MajorTickMark.Enabled = false;
//        areaPoint.AxisX.LabelStyle.Enabled = false;
//        areaPoint.AxisY.MajorGrid.Enabled = false;
//        areaPoint.AxisY.MajorTickMark.Enabled = false;
//        areaPoint.AxisY.LabelStyle.Enabled = false;

//        // Set chart area position and inner plot position

//        areaPoint.InnerPlotPosition = new ElementPosition((float)(i + interval),
//            (float)sample.Maximum, (float)interval, (float)(sample.Maximum - sample.Minimum));

//        //areaPoint.Position = new ElementPosition((float)Convert.ToDouble(sample.Name),
//        //    (float)sample.Maximum, (float)Interval, (float)(sample.Maximum - sample.Minimum));

//        // Create data series in the chart area
//        Histogram histogram = new Histogram(sample.Name, sample);
//        if (ParentStatChart != null)
//        {
//            histogram.BuildDataSeries(sample.Minimum, ParentStatChart.Properties.IntervalY);
//            ParentStatChart.chart.Series.Add(histogram.DataSeries);
//        }
//        else
//        {
//            histogram.BuildDataSeries(sample.Minimum, 1);
//        }
//        histogram.DataSeries.ChartType = SeriesChartType.Bar;

//        histogram.DataSeries.ChartArea = areaPoint.Name;

//    }
//}
