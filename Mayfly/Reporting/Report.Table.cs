using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using Mayfly.Extensions;

namespace Mayfly
{
    partial class Report
    {
        public bool UseTableNumeration { get; set; }

        public int NextTableNumber { get; private set; }

        public void AddTable(Table table) => AddTable(table, classid: table.Class == ReportTableClass.None ? string.Empty : table.Class.ToString().ToLower());

        public void AddTable(Table table, string classid)
        {
            if (table == null) return;


            this.WriteLine(string.IsNullOrWhiteSpace(classid) ? "<table>" : "<table class='{0}'>", classid);

            if (!string.IsNullOrWhiteSpace(table.Caption))
            {
                if (this.UseTableNumeration)
                {
                    this.WriteLine("<caption>{0}:</caption>", 
                        string.Format(Resources.Report.TableHeader, this.NextTableNumber, table.Caption));
                    this.NextTableNumber++;
                }
                else
                {
                    this.WriteLine("<caption>{0}:</caption>",
                        table.Caption);
                }
            }

            this.WriteLine(table.ToString());
            this.WriteLine("</table>");
        }

        public void AddAppendix(Appendix appendix)
        {
            WriteLine("<table class='{0}'>", arg0: appendix.Class.ToString().ToLower());

            if (!string.IsNullOrWhiteSpace(appendix.Caption))
            {
                this.WriteLine("<caption>{0}:</caption>", string.Format(Resources.Report.AppendixTableHeader,
                    Service.GetLetter(NextAppendixNumber), appendix.Caption));

                NextAppendixNumber++;
            }

            this.WriteLine(appendix.ToString());
            this.WriteLine("</table>");
        }

        public class Table : StringWriter
        {
            public ReportTableClass Class; // = ReportTableClass.None;

            public string Caption;



            public Table(ReportTableClass tableclass, string caption)
            {
                Class = tableclass;
                if (!string.IsNullOrWhiteSpace(caption)) Caption = caption;
            }

            public Table(ReportTableClass tableclass, string captionformat, params object[] values)
                : this(tableclass, string.Format(captionformat, values))
            { }

            public Table()
                : this(ReportTableClass.None, string.Empty)
            { }

            public Table(ReportTableClass tableclass)
                : this(tableclass, string.Empty)
            { }

            public Table(string caption)
                : this(ReportTableClass.None, caption)
            { }

            public Table(string format, params object[] values)
                : this(ReportTableClass.None, format, values)
            { }



            // Row logics

            public void StartRow()
            {
                WriteLine("<tr>");
            }

            public void EndRow()
            {
                WriteLine("</tr>");
            }

            // Table header

            public void StartHeader()
            {
                WriteLine("<thead>");
            }

            private void AddColumn(double width)
            {
                if (width > 1)
                    throw new ArgumentException("Width can not exceed 100% (1).");

                if (width == 0) this.WriteLine("<col />");
                else this.WriteLine(string.Format(CultureInfo.InvariantCulture, "<col style='width: {0}%'>", width * 100));
            }

            public void StartHeader(params double[] widths)
            {
                StartHeader();

                this.WriteLine("<colgroup>");

                for (int i = 0; i < widths.Length; i++)
                {
                    AddColumn(widths[i]);
                }

                this.WriteLine("</colgroup>");
            }

            private void AddColumn(int width)
            {
                if (width < 0)
                    throw new ArgumentException("Width can not be negative.");

                if (width == 0) this.WriteLine("<col />");
                else this.WriteLine("<col style='width: {0}ch'>", width);
            }

            public void StartHeader(params int[] widths)
            {
                StartHeader();

                this.WriteLine("<colgroup>");

                for (int i = 0; i < widths.Length; i++)
                {
                    AddColumn(widths[i]);
                }

                this.WriteLine("</colgroup>");
            }

            public void StartHeaderColumns(int count)
            {
                StartHeader();

                this.WriteLine("<colgroup>");

                for (int i = 0; i < count; i++)
                {
                    AddColumn(0D);
                }

                this.WriteLine("</colgroup>");
            }

            public void AddHeaderCell(string caption, double width, int span, CellSpan direction)
            {
                this.WriteLine("<th " +
                    (width > 0 ? "width='" + (width * 100) + "%'" : string.Empty) + " " +
                    (span > 1 ? (direction == CellSpan.Columns ? "colspan" : "rowspan") + "='" + span + "'" : string.Empty) +
                    ">" + caption + "</th>");
            }

            public void AddHeaderCell(string caption, int span, CellSpan direction)
            {
                this.AddHeaderCell(caption, 0, span, direction);
            }

            public void AddHeaderCell(string caption, int span)
            {
                this.AddHeaderCell(caption, span, CellSpan.Columns);
            }

            public void AddHeaderCell(string caption, double width)
            {
                this.AddHeaderCell(caption, width, 0, CellSpan.Columns);
            }

            public void AddHeaderCell(string caption, double width, int span)
            {
                this.AddHeaderCell(caption, width, span, CellSpan.Rows);
            }

            public void AddHeaderCell(string caption)
            {
                this.AddHeaderCell(caption, 0d);
            }

            public void AddHeaderCell(params string[] captions)
            {
                this.StartRow();

                for (int i = 0; i < captions.Length; i++)
                {
                    AddHeaderCell(captions[i]);
                }

                this.EndRow();
            }

            public void EndHeader()
            {
                WriteLine("</thead>");
            }

            public void AddHeaderColumns(int count)
            {
                StartHeaderColumns(count);
                EndHeader();
            }

            public void AddHeader(params double[] widths)
            {
                StartHeader(widths);
                EndHeader();
            }

            public void AddHeader(params int[] widths)
            {
                StartHeader(widths);
                EndHeader();
            }

            public void AddHeader(params string[] captions)
            {
                this.StartHeader();// captions.Length);
                this.AddHeaderCell(captions);
                this.EndHeader();
            }

            public void AddHeader(string[] captions, double[] widths)
            {
                StartHeader(widths);
                this.AddHeaderCell(captions);
                this.EndHeader();
            }

            // Cells logics

            public void StartCellOfClass(string classid, string value)
            {
                Write("<td class='{0}'>{1}", classid, value);
            }

            public void EndCell()
            {
                WriteLine("</td>");
            }

            // Insert cell

            //public void AddCell(object value, string classid, int span, CellSpan direction)
            //{
            //    this.WriteLine("<td " +
            //        (string.IsNullOrEmpty(classid) ? string.Empty : "class='" + classid + "'") +
            //        (span > 1 ? (direction == CellSpan.Columns ? "colspan" : "rowspan") + "='" + span + "'" : string.Empty) +
            //        ">" + value + "</td>");
            //}

            public void AddCell(object value, ReportCellClass classid, int span, CellSpan direction)
            {
                this.WriteLine("<td " +
                    (classid == ReportCellClass.None ? string.Empty : "class='" + classid.ToString().Replace(",",string.Empty).ToLower() + "'") +
                    (span > 1 ? (direction == CellSpan.Columns ? "colspan" : "rowspan") + "='" + span + "'" : string.Empty) +
                    ">" + (value == null ? string.Empty : value.ToString().Replace(Environment.NewLine, "<br>")) + "</td>");
            }

            public void AddCell(object value, ReportCellClass classid)
            {
                AddCell(value, classid, 1, CellSpan.Columns);
            }

            public void AddCell(object value, int span, CellSpan direction)
            {
                AddCell(value, ReportCellClass.None, span, direction);
            }

            public void AddCell(object value)
            {
                AddCell(value, ReportCellClass.None);
            }

            public void AddCell(object value, bool bold)
            {
                AddCell(value, bold ? ReportCellClass.Bold : ReportCellClass.None);
            }

            public void AddCell()
            {
                AddCell(string.Empty, ReportCellClass.None, 1, CellSpan.Columns);
            }

            //public void AddCell(string format, params object[] values)
            //{
            //    AddCell(string.Format(format, values));
            //}



            public void AddCellSider(object value)
            {
                AddCell(value, ReportCellClass.Sider);
            }

            public void AddCellRight(object value)
            {
                AddCell(value, ReportCellClass.Right);
            }

            public void AddCellRight(object value, bool bold)
            {
                AddCell(value, bold ? ReportCellClass.Bold | ReportCellClass.Right : ReportCellClass.Right);
            }

            public void AddCellRight(double value, string format)
            {
                AddCell(value.ToSmallValueString(format), ReportCellClass.Right);
            }

            public void AddCellRight(decimal value, string format)
            {
                AddCellRight((double)value, format);
            }

            public void AddCellRight(int value, string format)
            {
                AddCell(value.ToString(format), ReportCellClass.Right);
            }

            public void AddCellRight(double value, string format, bool bold)
            {
                AddCell(value.ToSmallValueString(format), bold ? ReportCellClass.Bold | ReportCellClass.Right : ReportCellClass.Right);
            }

            public void AddCellRight(decimal value, string format, bool bold)
            {
                AddCell(((double)value).ToSmallValueString(format), bold ? ReportCellClass.Bold | ReportCellClass.Right : ReportCellClass.Right);
            }

            public void AddCellRight(int value, string format, bool bold)
            {
                AddCell(value.ToString(format), bold ? ReportCellClass.Bold | ReportCellClass.Right : ReportCellClass.Right);
            }

            public void AddCellRight(double value, string format, int span, CellSpan direction)
            {
                AddCell(value.ToSmallValueString(format), ReportCellClass.Right, span, direction);
            }


            public void AddCellValue(object value)
            {
                AddCell(value, ReportCellClass.Centered);
            }

            public void AddCellValue(IFormattable value, string format)
            {
                AddCell(value.ToString(format, CultureInfo.CurrentCulture), ReportCellClass.Centered);
            }

            public void AddCellValue(object value, int span, CellSpan direction)
            {
                AddCell(value, ReportCellClass.Centered, span, direction);
            }

            public void AddCellValue(object value, int span)
            {
                AddCell(value, ReportCellClass.Centered, span, CellSpan.Rows);
            }


            public void AddCellPrompt(string prompt)
            {
                AddCell(prompt + ":"); //, ReportCellClass.Prompt);
            }

            public void AddCellPrompt(string prompt, object value, int span, CellSpan direction)
            {
                AddCell(string.Format("{0}:<span class='value right'>{1}</span>", prompt, value), span, direction);
            }

            public void AddCellPrompt(string prompt, object value, int span)
            {
                AddCellPrompt(prompt, value, span, CellSpan.Columns);
            }

            public void AddCellPrompt(string prompt, object value)
            {
                AddCellPrompt(prompt, value, 1, CellSpan.Columns);
            }

            public void AddCellPrompt(string prompt, IFormattable value, string format)
            {
                AddCellPrompt(prompt, value.ToString(format, CultureInfo.CurrentCulture));
            }

            public void AddCellPromptEmpty(string prompt)
            {
                AddCellPrompt(prompt, Constants.Null);
            }

            //Table footer

            public void StartFooter()
            {
                WriteLine("<tfoot>");
            }

            public void EndFooter()
            {
                WriteLine("</tfoot>");
            }



            public static Table GetLinedTable(string[] prompts, string[] values)
            {
                Table result = new Table(ReportTableClass.Fill);

                for (int i = 0; i < prompts.Length; i++)
                {
                    result.StartRow();
                    result.AddCellPrompt(prompts[i], values[i]);
                    result.EndRow();
                }

                return result;
            }
        }

        public class Appendix : Table
        {
            public Appendix(string caption)
                : base(ReportTableClass.Big, caption)
            { }

            public Appendix(string format, params object[] values)
                : base(ReportTableClass.Big, format, values)
            { }
        }
    }

    public enum ReportTableClass
    {
        None,
        Big,
        Fill
    }

    public enum CellSpan
    {
        Rows,
        Columns
    }

    [Flags]
    public enum ReportCellClass
    {
        None = 0x0,       // Left
        Centered = 0x1,   // Center
        Right = 0x2,      // Right
        //Prompt = 0x4,     // Color of table border
        Bold = 0x8,       // Bold
        Comment = 0x10,    // Little font
        Input = 0x20,      // User input on paper forms
        Hand = 0x40,        // User input appears in handwritten manner
        Overlay = 0x80,
        Sider = 0x3
    }
}
