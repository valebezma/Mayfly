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



        public void AddTable(Table table, string classid, string caption)
        {
            if (table == null) return;

            WriteLine(string.IsNullOrWhiteSpace(classid) ? "<table>" : "<table class='{0}'>", classid);

            if (!string.IsNullOrWhiteSpace(caption))
            {
                this.WriteLine("<caption>{0}:</caption>", caption);
            }

            WriteLine(table.ToString());

            WriteLine("</table>");

            string combinedNotice = string.Empty;
            foreach (Table.Notice notice in table.Notices)
            {
                combinedNotice += notice + ";<br />";
            }

            if (!string.IsNullOrWhiteSpace(combinedNotice))
            {
                combinedNotice = combinedNotice.TrimEnd(";<br />".ToCharArray()) + ".";
                AddComment(combinedNotice, true);
            }
        }

        public void AddTable(Table table, string classid)
        {
            AddTable(table, classid, UseTableNumeration ? string.Format(Resources.Report.TableHeader, this.NextTableNumber, table.Caption) : table.Caption);

            if (this.UseTableNumeration)
            {
                NextTableNumber++;
            }
        }

        public void AddTable(Table table)
        {
            AddTable(table, string.Empty);
        }

        public void AddAppendix(Table table)
        {
            AddTable(table, "big", string.Format(Resources.Report.AppendixTableHeader, NextAppendixNumber.ToLetter(), table.Caption));
            NextAppendixNumber++;
        }



        public class Table : StringWriter
        {
            public string Caption;

            public List<Notice> Notices;



            public Table(string caption)
            {
                if (!string.IsNullOrWhiteSpace(caption)) Caption = caption;

                Notices = new List<Notice>();
            }

            public Table(string captionformat, params object[] values)
                : this(string.Format(captionformat, values)) { }

            public Table()
                : this(string.Empty) { }



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
                    (classid == ReportCellClass.None ? string.Empty : "class='" + classid.ToString().Replace(",", string.Empty).ToLower() + "'") +
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

            public void AddCellRight(double value, int decimals)
            {
                AddCell(value.ToSmallValueString(decimals), ReportCellClass.Right);
            }

            public void AddCellRight(double value, int decimals, bool bold)
            {
                AddCellRight(value.ToSmallValueString(decimals), bold);
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

            public Notice AddNotice(string notice)
            {
                foreach (Notice _notice in Notices)
                {
                    if (_notice.Text == notice) return _notice;
                }

                Notice result = new Notice(Notices.Count, notice);
                Notices.Add(result);
                return result;
            }

            public Notice AddNotice(string format, params object[] values)
            {
                return AddNotice(string.Format(format, values));
            }



            public static Table GetLinedTable(string[] prompts, string[] values)
            {
                Table result = new Table();

                for (int i = 0; i < prompts.Length; i++)
                {
                    result.StartRow();
                    result.AddCellPrompt(prompts[i], values[i]);
                    result.EndRow();
                }

                return result;
            }

            public class Notice
            {
                public int Number;

                public string Text;

                public string Holder { get { return string.Format("<sup>{0}</sup>", Number.ToLetter().ToLowerInvariant()); } }

                public Notice(int no, string text)
                {
                    Number = no;
                    Text = text;
                }

                public override string ToString()
                {
                    return string.Format("{0} {1}", Holder, Text);
                }
            }
        }
    }

    //public enum ReportTableClass
    //{
    //    None,
    //    Big,
    //    Fill
    //}

    public enum CellSpan
    {
        Rows,
        Columns
    }

    [Flags]
    public enum ReportCellClass
    {
        None = 1,       // Left
        Centered = 2,   // Center
        Right = 4,      // Right
        Bold = 8,       // Bold
        Comment = 16,    // Little font
        Input = 32,      // User input on paper forms
        Hand = 64,        // User input appears in handwritten manner
        Overlay = 128,
        Sider = 256
    }
}
