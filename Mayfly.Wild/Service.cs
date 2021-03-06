using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Waters;
using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static Mayfly.UserSettings;

namespace Mayfly.Wild
{
    public abstract class Service
    {
        public static bool IsRowEmpty(DataGridViewRow gridRow) {
            foreach (DataGridViewColumn gridColumn in gridRow.DataGridView.Columns) {
                if (!gridColumn.Visible) continue;

                if (gridColumn.Name == "ColumnSpecies") {
                    if (gridRow.Cells[gridColumn.Index].Value != null &&
                        gridRow.Cells[gridColumn.Index].Value is string s &&
                        s != Species.Resources.Interface.UnidentifiedTitle) {
                        return false;
                    }
                } else if (gridRow.Cells[gridColumn.Index].Value != null &&
                      gridRow.Cells[gridColumn.Index].Value != System.DBNull.Value) {
                    return false;
                }
            }

            return true;
        }

        public static string GroupMask(string sourceValue) {
            string result = string.Empty;
            switch (sourceValue) {
                case "Wealth":
                case "Quantity":
                    result = "N0";
                    break;
                case "Abundance":
                    result = "N1";
                    break;
                case "DiversityA":
                case "DiversityB":
                case "Biomass":
                case "Mass":
                    result = "N3";
                    break;
            }

            return result;
        }



        public static string[] GetDiversityIndices() {
            List<string> result = new List<string>();

            foreach (DiversityIndex type in Enum.GetValues(typeof(DiversityIndex))) {
                string name = Resources.Interface.Diversity.ResourceManager.GetString(type.ToString());
                result.Add(string.IsNullOrWhiteSpace(name) ? type.ToString() : name);
            }

            return result.ToArray();
        }



        public static Report.Table GetBlankTable(string title, string massCaption, int lines) {
            Report.Table table = new Report.Table(title);

            table.AddHeader(new string[]{
                            Resources.Reports.Caption.Species,
                            Resources.Reports.Caption.QuantityUnit,
                            massCaption }, new double[] { .5 });

            for (int i = 1; i <= lines; i++) {
                table.StartRow();
                table.AddCell(i);
                table.AddCell();
                table.AddCell();
                table.EndRow();
            }

            table.StartRow();
            table.AddCell(global::Mayfly.Resources.Interface.Total);
            table.AddCell();
            table.AddCell();
            table.EndRow();

            return table;
        }



        //public static void AddStratifiedNote(Report report, double min, double max, double interval)
        //{
        //    AddStratifiedNote(report, string.Format(Wild.Resources.Reports.Data.StratifiedSample, string.Empty), min, max, interval, (l) => { return 0; });
        //}

        public static Report.Table GetStratifiedNote(double min, double max, double interval, int stratesInRow) {
            return GetStratifiedNote(string.Format(Resources.Reports.Header.StratifiedSample, string.Empty), min, max, interval, (l) => { return ""; }, stratesInRow);
        }

        public static Report.Table GetStratifiedNote(string title, double min, double max, double interval, Func<double, int> getQ) {
            return GetStratifiedNote(title, min, max, interval, getQ, 10);
        }

        public static Report.Table GetStratifiedNote(string title, double min, double max, double interval, Func<double, object> getQ) {
            return GetStratifiedNote(title, min, max, interval, getQ, 10);
        }

        public static Report.Table GetStratifiedNote(string title, double min, double max, double interval, Func<double, int> getQ, int stratesInRow) {
            return GetStratifiedNote(title, min, max, interval, (l) => {
                int q = getQ == null ? 0 : getQ.Invoke(l);
                return q == 0 ? string.Empty : q.ToStratifiedDots();
            }, stratesInRow);
        }

        public static Report.Table GetStratifiedNote(string title, double min, double max, double interval, Func<double, object> getQ, int stratesInRow) {
            if (max <= min) return null;

            Report.Table table = new Report.Table(title);
            Interval strate = Interval.FromEndpointAndWidth(min, interval);
            int lineLimiter = 0;
            table.AddHeaderColumns(stratesInRow * 2 + 2);
            table.StartRow();
            table.AddCell();
            string subline = string.Empty;

            for (double l = Mathematics.Service.GetStrate(min, interval).LeftEndpoint; l <= max; l += interval) {
                strate = Interval.FromEndpointAndWidth(l, interval);
                table.WriteLine("<td class='strate value' colspan='2'>{0}</td>", getQ.Invoke(l));
                subline += string.Format("<td class='strate' colspan='2'>{0}</td>", strate.LeftEndpoint);

                lineLimiter++;

                if (lineLimiter == stratesInRow) {
                    table.AddCell();
                    table.EndRow();

                    table.StartRow();
                    subline += strate.ClosedContains(max) ?
                        string.Format("<td class='strate' colspan='2'>{0}<br>{1}</td>", strate.RightEndpoint, Resources.Common.StratifiedUnits) :
                        string.Format("<td class='strate' colspan='2'>{0}</td>", strate.RightEndpoint);
                    table.WriteLine(subline);
                    table.EndRow();
                    subline = string.Empty;
                    lineLimiter = 0;

                    if (strate.RightEndpoint <= max) {
                        table.StartRow();
                        table.AddCell();
                    }
                }
            }

            if (lineLimiter < stratesInRow && lineLimiter > 0) // finish line
            {
                strate = Interval.FromEndpointAndWidth(strate.RightEndpoint, interval);

                while (lineLimiter < stratesInRow) {
                    table.WriteLine("<td class='strate value' colspan='2'></td>");
                    subline += string.Format("<td class='strate' colspan='2'>{0}</td>", strate.LeftEndpoint);

                    strate = Interval.FromEndpointAndWidth(strate.RightEndpoint, interval);
                    lineLimiter++;
                }

                table.EndRow();


                table.StartRow();
                subline += string.Format("<td class='strate' colspan='2'>{0}<br>{1}</td>", strate.LeftEndpoint, Resources.Common.StratifiedUnits);
                table.WriteLine(subline);
                table.EndRow();
            }

            return table;
        }


        public static void AssignAsFactors(List<DataGridViewColumn> factorColumns, bool ignoreZeros) {
            SpreadSheet sheet = (SpreadSheet)factorColumns[0].DataGridView;

            SelectionValue selectionValue = new SelectionValue(factorColumns);
            selectionValue.Picker.SelectedColumns = factorColumns;

            if (selectionValue.ShowDialog() != DialogResult.OK) return;

            string columnName = string.Empty;
            string format = factorColumns[0].GetDoubles().MeanFormat();

            foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns) {
                columnName += gridColumn.HeaderText + " × ";
                if (new OmniSorter().Compare(gridColumn.GetDoubles().MeanFormat(), format) > 0)
                    format = gridColumn.GetDoubles().MeanFormat();
            }

            columnName = columnName.TrimEnd(" ×".ToCharArray());

            DataGridViewColumn assignedColumn = sheet.InsertColumn(columnName, columnName,
                typeof(string), selectionValue.Picker.SelectedColumns[0].Index);

            foreach (DataGridViewRow gridRow in sheet.Rows) {
                string variant = string.Empty;

                foreach (DataGridViewColumn gridColumn in selectionValue.Picker.SelectedColumns) {
                    double value = (double)gridRow.Cells[gridColumn.Index].Value;

                    variant += value == 0 && ignoreZeros ? string.Empty :
                        string.Format("[{0} = {1:" + format + "}] × ", gridColumn.HeaderText, value);
                }

                variant = variant.TrimEnd(" ×".ToCharArray());

                if (variant == string.Empty) variant = Resources.Interface.Interface.Control;

                gridRow.Cells[assignedColumn.Index].Value = variant;
            }
        }

        public static void HandleAgeInput(DataGridViewCell cell) {
            if (cell.DataGridView == null) return;
            HandleAgeInput(cell,
                cell.OwningColumn.DefaultCellStyle.Padding.All != 0 ?
                cell.OwningColumn.DefaultCellStyle : cell.DataGridView.DefaultCellStyle);
        }

        delegate void AgeHandler(DataGridViewCell cell, DataGridViewCellStyle basicStyle);

        public static void HandleAgeInput(DataGridViewCell cell, DataGridViewCellStyle basicStyle) {
            if (cell.DataGridView != null && cell.DataGridView.InvokeRequired) {
                AgeHandler ageHandler = new AgeHandler(HandleAgeInput);
                cell.DataGridView.Invoke(ageHandler, new object[] { cell, basicStyle });
            } else {
                Padding example = basicStyle.Padding;
                Padding pads = new Padding(example.Left, example.Top, example.Right, example.Bottom);

                string formatted = cell.Value == null ? cell.Style.NullValue.ToString() : ((Age)cell.Value).ToString(basicStyle.Format);

                if (formatted.Contains("+")) {
                    pads.Right -= 6;
                }

                cell.Style.Padding = pads;
            }
        }




        public static string[] CrossSection(WaterType waterType) {
            switch (waterType) {
                case WaterType.Stream: return Resources.Interface.Section.Stream.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                case WaterType.Lake: return Resources.Interface.Section.Tank.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                case WaterType.Tank: return Resources.Interface.Section.Tank.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            return new string[0];
        }

        public static string CrossSection(WaterType waterType, int Value) {
            return CrossSection(waterType)[Value];
        }

        public static string Bank(int bankValue) {
            return Resources.Interface.Section.Bank.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[bankValue];
        }



        public static string CloudageName(int okts) {
            string result = string.Empty;

            switch (okts) {
                case 0:
                    result = Resources.Interface.Clouds.SKC;
                    break;
                case 1:
                case 2:
                    result = Resources.Interface.Clouds.FEW;
                    break;
                case 3:
                case 4:
                    result = Resources.Interface.Clouds.SCT;
                    break;
                case 5:
                case 6:
                case 7:
                    result = Resources.Interface.Clouds.BKN;
                    break;
                case 8:
                    result = Resources.Interface.Clouds.OVC;
                    break;
            }

            return string.Format("{0} / 8: {1} ", okts, result);
        }

        public static string WaterColorName(int value) {
            switch (value) {
                case 0: return Resources.Interface.WaterColor.NoColor;
                case 1: return Resources.Interface.WaterColor.Blue;
                case 2: return Resources.Interface.WaterColor.Gray;
                case 3: return Resources.Interface.WaterColor.Red;
                case 4: return Resources.Interface.WaterColor.Green;
                case 5: return Resources.Interface.WaterColor.Yellow;
                case 6: return Resources.Interface.WaterColor.Brown;
                case 7: return Resources.Interface.WaterColor.Black;
            }

            return string.Empty;
        }



        /// <param name="q">Quantity in individuals</param>
        public static string GetFriendlyQuantity(int q) {
            if (q < 0) {
                return Constants.Null;
            } else if (q < 1250) {
                return string.Format(Resources.Interface.FriendlyUnits.ind, q);
            } else if (q < 1250000) {
                return string.Format(Resources.Interface.FriendlyUnits.ind1000, (double)q / 1000);
            } else {
                return string.Format(Resources.Interface.FriendlyUnits.ind1000000, (double)q / 1000000);
            }
        }



        public static void HandleFrequency(DataGridView grid, DataGridViewCellEventArgs e) {
            if (grid[e.ColumnIndex, e.RowIndex].Value is int) {
                int value = (int)grid[e.ColumnIndex, e.RowIndex].Value;
                if (value < 2) {
                    grid[e.ColumnIndex, e.RowIndex].Value = null;
                    System.Media.SystemSounds.Beep.Play();
                }
            }
        }



        public static string GetTaxonomicIndexPath(string groupName) {
            return IO.GetIndexPath(
                Species.UserSettings.Interface.OpenDialog,
                groupName + " (auto).txn",
                Server.GetUri("get/index/specieslists/" + groupName.ToLower().Replace(" ", "_") + "_default.txn", Application.CurrentCulture)
                );
        }

        public static void SaveEquipment() {

            ReaderSettings.Equipment.SetAttributable();
            string path = Path.Combine(IO.UserFolder, string.Format(@"equipment{0}.ini", Feature.ToLowerInvariant()));
            ReaderSettings.Equipment.WriteToFile(path);
        }
    }
}
