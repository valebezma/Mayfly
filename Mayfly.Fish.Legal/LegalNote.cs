using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Extensions;
using System.Drawing;
using System.Globalization;

namespace Mayfly.Fish.Legal
{
    public class LegalNote : Report
    {
        public static int RowLength
        {
            get { return 110; } // UserSettings.HandWrite ? 150 : 77; }
        }

        public static ReportCellClass InputClass
        {
            get
            {
                ReportCellClass result = ReportCellClass.Input;
                if (UserSettings.HandWrite) result = result | ReportCellClass.Hand;
                return result;
            }
        }



        public LegalNote(string title)
            : base(title, "fill.css", @"wild\legal.css")
        {
            //this.AddHeader(title);

            if (UserSettings.HandWrite)
            {
                this.AddStyleSheet("handwrite.css");
            }
        }

        public LegalNote()
            : this(string.Empty)
        { }

        internal void AddReferenceNotes(string note1, string note2, string note3, string note4, string note5)
        {
            Table table = new Table();

            table.StartRow();
            table.AddCellAsk(note1);
            table.AddCell(note2, LegalNote.InputClass | ReportCellClass.Centered);
            table.AddCellAsk(note3);
            table.AddCell(note4, LegalNote.InputClass | ReportCellClass.Centered);
            table.AddCellAsk(note5);
            table.EndRow();

            this.AddTable(table, "fill");
        }
    }

    public static class ReportExtensions
    {
        public static Report.Table GetSignatureBlock(DateTime dt, string undersign, string underdate)
        {
            Report.Table result = new Report.Table();

            result.AddHeader(.6, 0, .3);

            result.StartRow();
            result.AddCell(string.Empty, ReportCellClass.Input);
            result.AddCell();
            result.AddCell(dt.ToString("d"), LegalNote.InputClass | ReportCellClass.Centered);
            result.EndRow();

            result.StartRow();
            result.AddCellComment(undersign);
            result.AddCell();
            result.AddCellComment(underdate);
            result.EndRow();

            return result;
        }

        public static Report.Table GetAskBlock(string ask, string answer, string hint)
        {
            Report.Table result = new Report.Table();

            string[] asklines = ask.Split(LegalNote.RowLength);

            if (asklines.Length == 1)
            {
                result.AddHeader(asklines.Last().Length + 2);

                while (ask.Length > LegalNote.RowLength)
                {
                    result.StartRow();
                    result.AddCell(asklines[0], 2, CellSpan.Columns);
                    result.EndRow();

                    ask = string.Empty;
                    for (int i = 1; i < asklines.Length; i++)
                        ask += asklines[i];
                }

                result.StartRow();
                result.AddCell(ask);

                if (answer.IsAcceptable())
                {
                    string[] typelines = answer.Split(LegalNote.RowLength - ask.Length - 2, LegalNote.RowLength);

                    result.AddCell(typelines[0], LegalNote.InputClass);
                    result.EndRow();

                    for (int i = 1; i < typelines.Length; i++)
                    {
                        result.StartRow();
                        result.AddCell(typelines[i], LegalNote.InputClass, 2, CellSpan.Columns);
                        result.EndRow();
                    }
                }
                else
                {
                    result.AddCell(string.Empty, LegalNote.InputClass);
                    result.EndRow();
                }

                result.StartRow();
                result.AddCellComment(hint, 2);
                result.EndRow();
            }
            else
            {
                foreach (string askline in asklines)
                {
                    result.StartRow();
                    result.AddCell(askline);
                    result.EndRow();
                }

                if (answer.IsAcceptable())
                {
                    string[] typelines = answer.Split(LegalNote.RowLength);

                    result.StartRow();
                    result.AddCell(typelines[0], LegalNote.InputClass);
                    result.EndRow();

                    for (int i = 1; i < typelines.Length; i++)
                    {
                        result.StartRow();
                        result.AddCell(typelines[i], LegalNote.InputClass);
                        result.EndRow();
                    }
                }
                else
                {
                    result.AddCell(string.Empty, LegalNote.InputClass);
                    result.EndRow();
                }

                result.StartRow();
                result.AddCellComment(hint);
                result.EndRow();
            }

            return result;
        }

        public static void AddCellAsk(this Report.Table table, string value)
        {
            table.WriteLine("<td style='width:" + (int)(value.Length * 1.15) + "ch'>" + value + "</td>");
        }

        public static void AddCellComment(this Report.Table table, string comment, int span)
        {
            table.AddCell("(" + comment + ")", ReportCellClass.Comment, span, CellSpan.Columns);
        }

        public static void AddCellComment(this Report.Table table, string comment)
        {
            table.AddCellComment(comment, 1);
        }

        public static void AddStamp(this Report.Table table, string filename, double angle)
        {
            Image img = Image.FromFile(filename);
            double height = (img.Height / img.VerticalResolution);
            double width = (img.Width / img.HorizontalResolution);
            table.AddCell("<img src='" + filename + "' data-rotate='" + angle + "' style='" +
                "height: " + height.ToString("N2", CultureInfo.InvariantCulture) + "in; " +
                "width: " + width.ToString("N2", CultureInfo.InvariantCulture) + "in; " +
                "top: -" + (height / 2).ToString("N2", CultureInfo.InvariantCulture) + "in'/>", 
                ReportCellClass.Overlay);
        }

        public static void AddSignature(this Report.Table table, string filename)
        {
            Image img = Image.FromFile(filename);
            double height = (img.Height / img.VerticalResolution);
            double width = (img.Width / img.HorizontalResolution);
            table.AddCell("<img src='" + filename + "' style='" +
                "height: " + height.ToString("N2", CultureInfo.InvariantCulture) + "in; " +
                "width: " + width.ToString("N2", CultureInfo.InvariantCulture) + "in; " +
                "top: -" + (height / 2 - 0.2).ToString("N2", CultureInfo.InvariantCulture) + "in'/>",
                ReportCellClass.Overlay | ReportCellClass.Input);
        }
    }
}
