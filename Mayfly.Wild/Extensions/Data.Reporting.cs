using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Data;

namespace Mayfly.Wild
{
    public static partial class DataExtensions
    {
        public static Report.Table GetIndividualsLogReportTable(this Data.IndividualRow[] individualRows, string title)
        {
            if (individualRows.Length > 0)
            {
                Data data = (Data)individualRows[0].Table.DataSet;

                Data.VariableRow[] variables = data.Variable.FindByIndividuals(individualRows);

                Report.Table table = new Report.Table(title);

                #region Compiling headers

                List<string> captions = new List<string> { "№" };

                foreach (DataColumn col in data.Individual.Columns) {
                    if (data.IsDataPresented(col, individualRows)) {
                        captions.Add(col.Caption);
                    }
                }

                captions.AddRange(data.Variable.Names(variables));
                captions.Add(Resources.Reports.Card.Comments);

                List<double> widths = new List<double>
                {
                    .05
                };

                for (int i = 0; i < captions.Count - 2; i++)
                {
                    widths.Add(.1);
                }

                table.AddHeader(captions.ToArray(), widths.ToArray());

                #endregion

                int no = 1;
                double mass = 0.0;
                int count = 0;

                foreach (Data.IndividualRow individualRow in individualRows)
                {
                    table.StartRow();

                    table.AddCellRight(no);

                    foreach (DataColumn col in data.Individual.Columns)
                    {
                        if (data.IsDataPresented(col, individualRows))
                        {
                            if (individualRow.IsNull(col))
                            {
                                table.AddCell();
                            }
                            else
                            {
                                switch (col.ColumnName)
                                {
                                    case "Age":
                                        table.AddCellValue((Age)individualRow[col]);
                                        break;

                                    case "Sex":
                                        table.AddCellValue((Sex)individualRow[col], "C");
                                        break;

                                    //case "Grade":
                                    //    table.AddCellValue(new Grade((int)individualRow[col]).ToString("C"));
                                    //    break;

                                    default:
                                        table.AddCellRight(individualRow[col]);
                                        //if (individualRow[col] is double) table.AddCellRight((double)individualRow[col], "N0");
                                        //else table.AddCellRight(individualRow[col]);
                                        break;
                                }
                            }
                        }
                    }

                    foreach (Data.VariableRow variable in variables) {
                        if (data.Value.FindByIndIDVarID(individualRow.ID, variable.ID) == null) {
                            table.AddCell();
                        } else {
                            table.AddCellRight(data.Value.FindByIndIDVarID(individualRow.ID, variable.ID).Value, 2);
                        }
                    }

                    table.AddCell(individualRow.IsCommentsNull() ? string.Empty : individualRow.Comments);

                    table.EndRow();

                    no++;
                    mass += individualRow.IsMassNull() ? 0 : individualRow.Mass;
                    count += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
                }

                table.StartFooter();
                table.StartRow();

                for (int i = 0; i < captions.Count; i++)
                {
                    if (i == captions.IndexOf(resources.GetString("ColumnMass.HeaderText")))
                    {
                        table.AddCellRight(mass, "N0", true);
                    }
                    else if (i == captions.IndexOf(resources.GetString("ColumnFrequency.HeaderText")))
                    {
                        table.AddCellRight(count, "N0", true);
                    }
                    else
                    {
                        table.AddCell();
                    }
                }

                table.EndRow();
                table.EndFooter();

                return table;
            }
            else return null;
        }

        public static Report.Table GetSpeciesLogReportTable(this Data.LogRow[] logRows, string massCaption, string logTitle)
        {
            Report.Table table = new Report.Table(logTitle);

            table.AddHeader(new string[]{
                            Resources.Reports.Caption.Species,
                            Resources.Reports.Caption.QuantityUnit,
                            string.IsNullOrEmpty(massCaption) ? Resources.Reports.Caption.Mass : massCaption },
                        new double[] { .50 });

            double q = 0;
            double w = 0;

            foreach (Data.LogRow logRow in logRows)
            {
                table.StartRow();

                string logEntry = logRow.DefinitionRow.KeyRecord.FullNameReport;

                if (!logRow.IsCommentsNull())
                {
                    logEntry += string.Format(
                        "<br><span class='leftcomment'>{0}: {1}</span>",
                        Resources.Reports.Card.Comments, logRow.Comments);
                }

                table.AddCell(logEntry);

                if (logRow.IsQuantityNull())
                {
                    table.AddCell();
                }
                else
                {
                    table.AddCellRight(logRow.Quantity);
                    q += logRow.Quantity;
                }

                if (logRow.IsMassNull())
                {
                    table.AddCell();
                }
                else
                {
                    table.AddCellRight(logRow.Mass, 2);
                    w += logRow.Mass;
                }

                table.EndRow();
            }

            table.StartFooter();
            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);
            table.AddCellRight(q);
            table.AddCellRight(w, 2);
            table.EndRow();
            table.EndFooter();

            return table;
        }

        /// <summary>
        /// Adds Individual profiles and/or Individuals log into Report
        /// </summary>
        /// <param name="indRows"></param>
        /// <param name="report"></param>
        /// <param name="level"></param>
        public static void AddReport(this Data.IndividualRow[] indRows, Report report, CardReportLevel level, string logtitle)
        {
            if (level.HasFlag(CardReportLevel.Individuals))
            {
                Report.Table logtable = indRows.GetIndividualsLogReportTable(logtitle);
                if (logtable != null) report.AddTable(logtable);
            }

            if (level.HasFlag(CardReportLevel.Profile))
            {
                bool first = true;
                foreach (Data.IndividualRow individualRow in indRows)
                {
                    if (first) { first = false; } else { report.BreakPage(); }
                    report.AddHeader(Resources.Reports.Header.IndividualProfile);
                    individualRow.AddReport(report);
                }
            }
        }

        public static void AddCribnote(this Report report, Report.Table crib)
        {
            report.AddStyleSheet(@"wild\strates.css");
            bool tablenum = report.UseTableNumeration;
            report.UseTableNumeration = false;
            report.AddTable(crib, "ruler");
            report.UseTableNumeration = tablenum;
        }

        /// <summary>
        /// Adds Individuals log and/or Stratified sample combined for several LogRows with givn titles into Report
        /// </summary>
        /// <param name="logRows"></param>
        /// <param name="report"></param>
        /// <param name="level"></param>
        /// <param name="logtitle"></param>
        /// <param name="stratifiedtitle"></param>
        public static void AddReport(this Data.LogRow[] logRows, Report report, CardReportLevel level, string logtitle, string stratifiedtitle)
        {
            if (level.HasFlag(CardReportLevel.Individuals))
            {
                List<Data.IndividualRow> indRows = new List<Data.IndividualRow>();

                foreach (Data.LogRow logRow in logRows)
                {
                    indRows.AddRange(logRow.GetIndividualRows());
                }

                indRows.ToArray().AddReport(report, CardReportLevel.Individuals, logtitle);
            }

            int str = 0;

            foreach (Data.LogRow logRow in logRows)
            {
                str += logRow.QuantityStratified;
            }

            if (str > 0 && level.HasFlag(CardReportLevel.Stratified))
            {
                double min = double.MaxValue;
                double max = double.MinValue;

                double interval = 0.1;

                foreach (Data.LogRow logRow in logRows)
                {
                    if (logRow.QuantityStratified > 0)
                    {
                        interval = Math.Max(interval, logRow.Interval);
                        min = Math.Min(min, logRow.MinStrate.LeftEndpoint);
                        max = Math.Max(max, logRow.MaxStrate.LeftEndpoint);
                    }
                }

                report.AddCribnote(Wild.Service.GetStratifiedNote(stratifiedtitle, min, max, interval, (l) => {
                    int countSum = 0; foreach (Data.LogRow logRow in logRows)
                    {
                        foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows()) { if (stratifiedRow.SizeClass.LeftClosedContains(l)) countSum += stratifiedRow.Count; }
                    }
                    return countSum;
                }));
            }
        }




        /// <summary>
        /// Creates report containing Individuals log and/or Individual profiles
        /// </summary>
        /// <param name="indRows"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Report GetReport(this Data.IndividualRow[] indRows, CardReportLevel level)
        {
            Report report = new Report(level == CardReportLevel.Profile ? string.Empty : Wild.Resources.Reports.Header.IndividualsLog);
            indRows.AddReport(report, level, string.Empty);
            report.EndBranded();
            return report;
        }

        /// <summary>
        /// Creates report containing Individuals log and/or Stratified sample for given LogRows
        /// </summary>
        /// <param name="logRows"></param>
        /// <param name="level"></param>
        /// <param name="splitSpecies">If true - each species will be reported separately</param>
        /// <returns></returns>
        public static Report GetReport(this Data.LogRow[] logRows, CardReportLevel level, bool splitSpecies)
        {
            if (splitSpecies)
            {
                List<Data.DefinitionRow> speciesRows = new List<Data.DefinitionRow>();
                foreach (Data.LogRow logRow in logRows) { if (!speciesRows.Contains(logRow.DefinitionRow)) speciesRows.Add(logRow.DefinitionRow); }

                Report report = new Report(string.Format(Resources.Interface.Interface.IndLog, string.Empty));
                foreach (Data.DefinitionRow speciesRow in speciesRows)
                {
                    List<Data.LogRow> _logRows = new List<Data.LogRow>();
                    foreach (Data.LogRow logRow in logRows) { if (logRow.DefinitionRow == speciesRow) _logRows.Add(logRow); }

                    string speciesPresentation = speciesRow.KeyRecord.FullNameReport;
                    logRows.AddReport(report, level, speciesPresentation,
                        string.Format(Wild.Resources.Reports.Header.StratifiedSample, speciesPresentation));
                }
                return report;
            }
            else
            {
                Report report = new Report("Log ---");
                logRows.AddReport(report, level, string.Format(Resources.Interface.Interface.IndLog, string.Empty), string.Format(Resources.Reports.Header.StratifiedSample, string.Empty));
                return report;
            }
        }

        /// <summary>
        /// Creates report containing Individuals log and/or Stratified sample for given LogRows (splitting each species)
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns></returns>
        public static Report GetReport(this Data.LogRow[] logRows, CardReportLevel level)
        {
            return logRows.GetReport(level, true);
        }


    }

    public enum CardReportLevel
    {
        Note = 1,
        Species = 10,
        Stratified = 100,
        Individuals = 1000,
        Profile = 10000
    }
}
