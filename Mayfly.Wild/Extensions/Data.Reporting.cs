using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Mayfly.Species;

namespace Mayfly.Wild
{
    public static partial class DataExtensions
    {
        public static Report.Table GetIndividualsLogReportTable(this Data.IndividualRow[] individualRows, ResourceManager resources, string title)
        {
            if (individualRows.Length > 0)
            {
                Data data = (Data)individualRows[0].Table.DataSet;

                Data.VariableRow[] variables = data.Variable.FindByIndividuals(individualRows);

                Report.Table table = new Report.Table(title);

                string[] cols = new string[] {
                        "Frequency", "Length", "Mass", "RegID", "Age",
                        "Sex", "Maturity", "Grade", "Instar" };

                #region Compiling headers

                List<string> captions = new List<string>();

                captions.Add("№");

                foreach (string col in cols)
                {
                    if (data.IsDataPresented(data.Individual.Columns[col], individualRows))
                    {
                        captions.Add(resources.GetString("Column" + col + ".HeaderText"));
                    }
                }

                captions.AddRange(data.Variable.Names(variables));
                captions.Add(Resources.Reports.Card.Comments);

                List<double> widths = new List<double>();

                widths.Add(.05);

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

                    foreach (string col in cols)
                    {
                        if (data.IsDataPresented(data.Individual.Columns[col], individualRows))
                        {
                            if (individualRow.IsNull(col))
                            {
                                table.AddCell();
                            }
                            else
                            {
                                switch (col)
                                {
                                    case "Age":
                                        table.AddCellValue(new Age(individualRow[col]));
                                        break;

                                    case "Sex":
                                        table.AddCellValue(new Sex((int)individualRow[col]).ToString("C"));
                                        break;

                                        // TODO: Grade is Mayfly.Benthos type!!!!!!
                                    //case "Grade":
                                    //    table.AddCellValue(new Grade((int)individualRow[col]).ToString("C"));
                                    //    break;

                                    default:
                                        if (individualRow[col] is double) table.AddCellRight((double)individualRow[col], "N0");
                                        else table.AddCellRight(individualRow[col]);
                                        break;
                                }
                            }
                        }
                    }

                    foreach (Data.VariableRow variable in variables)
                    {
                        if (data.Value.FindByIndIDVarID(individualRow.ID, variable.ID) == null)
                        {
                            table.AddCell();
                        }
                        else
                        {
                            table.AddCellRight(data.Value.FindByIndIDVarID(individualRow.ID, variable.ID).Value, 2);
                        }
                    }

                    table.AddCell(individualRow.IsCommentsNull() ? string.Empty : individualRow.Comments);

                    table.EndRow();

                    no++;

                    if (!individualRow.IsMassNull())
                    { mass += individualRow.Mass; }

                    int n = 1;
                    if (!individualRow.IsFrequencyNull())
                    { n = individualRow.Frequency; }
                    count += n;
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

        public static Report.Table GetSpeciesLogReportTable(this Data.LogRow[] logRows, SpeciesKey key, string massCaption, string logTitle)
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

                //table.StartCellOfClass("left", 
                //    logRow.SpeciesRow.GetKeyRecord(key).ReportShortPresentation);

                string logEntry = logRow.SpeciesRow.KeyRecord.ScientificNameReport;

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

        public static void AddCribnote(this Report report, Report.Table crib)
        {
            report.AddStyleSheet(@"wild\strates.css");
            bool tablenum = report.UseTableNumeration;
            report.UseTableNumeration = false;
            report.AddTable(crib, "ruler");
            report.UseTableNumeration = tablenum;
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
