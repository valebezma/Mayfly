﻿using Microsoft.Win32;
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
    public partial class Report : StringWriter
    {
        public string Title { get; set; }

        public bool UseEquationNumeration { get; set; }

        public int NextEquationNumber { get; private set; }

        public int NextAppendixNumber { get; private set; }

        public List<string> References { get; private set; }

        public List<string> StyleSheets { get; private set; }

        public List<string> Scripts { get; private set; }

        //public List<string> CustomHeadStrings { get; private set; }

        //private List<string> headLines = new List<string>();



        protected Report() 
        {
            Title = string.Empty;
            References = new List<string>();
            StyleSheets = new List<string>();
            Scripts = new List<string>();
            //CustomHeadStrings = new List<string>();

            NextTableNumber = 1;
            NextEquationNumber = 1;
            NextAppendixNumber = 0;

            UseEquationNumeration = true;
            UseTableNumeration = false;
        }

        public Report(string title, PageBreakOption opt, params string[] styles) : this() 
        {
            Title = title;

            foreach (string css in new string[] { "fonts.css", "report.css", "tables.css" }) {
                AddStyleSheet(css);
            }

            foreach (string css in styles) {
                AddStyleSheet(css);
            }

            StartPage(opt);

            if (!string.IsNullOrWhiteSpace(this.Title)) this.WriteLine("<h1>{0}</h1>", this.Title);
        }

        public Report(string title, params string[] styles) : this(title, PageBreakOption.None, styles) { }

        public Report(string title, PageBreakOption opt) : this(title, opt, new string[] { }) { }

        public Report(string title) : this(title, PageBreakOption.None) { }



        public static explicit operator Report(string value)
        {
            Report result = new Report();
            result.Write(value);
            return result;
        }

        public static explicit operator string(Report value)
        {
            return value.ToString();
        }



        public void AddReference(string reference)
        {
            if (!References.Contains(reference)) {
                References.Add(reference);
            }
        }

        public void AddStyleSheet(string css)
        {
            if (!StyleSheets.Contains(css)) {
                StyleSheets.Add(css);
            }
        }

        public string GetStyles()
        {
            string result = string.Empty;

            foreach (string s in StyleSheets)
            {
                result += File.ReadAllText(s);
            }

            return result;
        }

        //public void AddToHead(string format, params object[] values)
        //{
        //    AddToHead(string.Format(format, values));
        //}

        //public void AddToHead(string value)
        //{
        //    if (!headLines.Contains(value))
        //    {
        //        int headEnd = 32;
        //        headEnd = this.ToString().IndexOf("</head>");
        //        this.GetStringBuilder().Insert(headEnd, value + this.NewLine);
        //        this.headLines.Add(value);
        //    }
        //}

        public void AddScript(string script)
        {
            if (!Scripts.Contains(script)) {
                Scripts.Add(script);
            }
        }



        public void AddHeader(string text)
        {
            WriteLine("<h1>{0}</h1>", text);
        }

        public void AddHeader(string format, params string[] values)
        {
            WriteLine("<h1>{0}</h1>", string.Format(format, values));
        }

        public void AddSubtitle(string text)
        {
            WriteLine("<h2>{0}</h2>", text);
        }

        public void AddSubtitle(string format, params string[] values)
        {
            WriteLine("<h2>{0}</h2>", string.Format(format, values));
        }

        public void AddSubtitle3(string text)
        {
            WriteLine("<h3>{0}</h3>", text);
        }

        public void AddSubtitle3(string format, params string[] values)
        {
            WriteLine("<h3>{0}</h3>", string.Format(format, values));
        }



        public void AddParagraph(string text)
        {
            WriteLine("<p>{0}</p>", text);
        }

        public void AddParagraph(string format, params object[] values)
        {
            AddParagraph(string.Format(format, values));
        }

        public void AddParagraphClass(string classid, string text)
        {
            WriteLine("<p class='{0}'>{1}</p>", classid, text);
        }

        public void AddComment(string text, bool addNaticeLabel)
        {
            if (addNaticeLabel)
            {
                AddParagraphClass("notice",
                    string.Format("<strong>{0}. </strong> {1}", Resources.Interface.Comments, text));
            }
            else
            {
                AddParagraphClass("notice",
                    text);
            }
        }

        public void AddComment(string text)
        {
            AddComment(text, true);
        }

        public void AddParagraphClassValue(string text)
        {
            AddParagraphClass("value", text);
        }

        public void AddParagraphClassValue(string format, params object[] values)
        {
            AddParagraphClassValue(string.Format(format, values));
        }



        public void AddEquation(string latex)
        {
            AddEquation(latex, latex.EndsWith(".}") ? string.Empty : ".");
            //AddEquation(latex, string.Empty);
        }

        public void AddEquation(string latex, string lineEnd)
        {
            AddScript(@"math\MathJax.js?config=TeX-AMS_HTML");

            if (this.UseEquationNumeration)
            {
                WriteLine("<p>$$" + latex + lineEnd + "\\tag {" + NextEquationNumber + "} $$</p>");
                NextEquationNumber++;
            }
            else
            {
                WriteLine("<p>$$" + latex + lineEnd + "$$</p>");
            }
        }



        public void AddImage(Image jpegImage, int id)
        {
            WriteLine("<canvas id='canv{0}' width='{1}' height='{2}'></canvas>", id, jpegImage.Width, jpegImage.Height);
            WriteLine("<script>");
            WriteLine("    var canvas{0} = document.getElementById('canv{0}');", id);
            WriteLine("    var context{0} = canvas{0}.getContext('2d');", id);

            WriteLine("    var img{0} = new Image();", id);
            WriteLine("    img{0}.onload = function()", id);
            WriteLine("    {");
            WriteLine("        img{0}.setAtX = {1}", id, id);
            WriteLine("        context{0}.drawImage(img{1}, this.setAtX, 0, {2}, {3})",
                id, id, jpegImage.Width, jpegImage.Height);
            WriteLine("    };");
            WriteLine("    img{0}.src='data:image/jpeg;base64,{1}';",
                id, Convert.ToBase64String(jpegImage.ToByteArray()));

            WriteLine("</script>");
        }

        public void AddImage(Byte[] pngImage, int id)
        {
            MemoryStream memoryStream = new MemoryStream(pngImage);
            Image image = System.Drawing.Image.FromStream(memoryStream);
            WriteLine("<canvas id='canv{0}' width='{1}' height='{2}'></canvas>", id, image.Width, image.Height);

            WriteLine("<script>");
            WriteLine("    var canvas{0} = document.getElementById('canv{0}');", id);
            WriteLine("    var context{0} = canvas{0}.getContext('2d');", id);
            WriteLine("    var img{0} = new Image();", id);
            WriteLine("    img{0}.src='data:image/png;base64,{1}';", id, Convert.ToBase64String(pngImage));
            WriteLine("    img{0}.onload = function()", id);
            WriteLine("    {");
            WriteLine("        context{0}.drawImage(img{1}, 0, 0)", id, id);
            WriteLine("    };");
            WriteLine("</script>");
        }



        public void StartPage(PageBreakOption opt)
        {
            WriteLine("<div class=" + (opt.HasFlag(PageBreakOption.Landscape) ? "'sheet landscape'" : (opt.HasFlag(PageBreakOption.Odd) ? "'sheet odd'" : "'sheet'")) + ">");
        }

        public void StartPage()
        {
            StartPage(PageBreakOption.None);
        }

        public void BreakPage(PageBreakOption opt)
        {
            CloseDiv();
            StartPage(opt);
        }

        public void BreakPage()
        {
            BreakPage(PageBreakOption.None);
        }

        public void CloseDiv()
        {
            WriteLine("</div>");
        }


        public void End(string footer)
        {
            if (References.Count > 0)
            {
                this.AddSubtitle(Resources.Report.Cited);

                foreach (string reference in References)
                {
                    this.AddParagraph(reference);
                }
            }

            if (!string.IsNullOrWhiteSpace(footer))
                AddParagraphClass("footer", footer);
        }

        public void End(string format, params object[] values)
        {
            End(string.Format(format, values));
        }

        public void End()
        {
            End(string.Empty);
        }

        public void EndBranded()
        {
            End(Resources.Report.Copyright, UserSettings.Username, DateTime.Now, UserSettings.Product);
        }

        public void End(int year, string author)
        {
            End("© {0} {1}", year, author);
        }



        public string GetFileContent()
        {
            return GetFileContent(false);
        }

        public string GetFileContent(bool printable)
        {
            StringWriter result = new StringWriter();
            result.WriteLine("<!DOCTYPE html>");
            result.WriteLine("<html>");
            result.WriteLine("<head>");
            result.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>");
            result.WriteLine("<title>{0}</title>", string.IsNullOrWhiteSpace(this.Title) ? string.Format("Mayfly Report ({0:g})", DateTime.Now) : this.Title);

            foreach (string css in StyleSheets) {
                result.WriteLine(
                    "<link rel='StyleSheet' href='{0}' type='text/css'/>",
                    Path.Combine(Application.StartupPath, @"interface\reports\css\", css));
            }

            foreach (string script in Scripts) {

                try
                {
                    string path = Path.Combine(Application.StartupPath, @"interface\reports\js\", script.Substring(0, script.IndexOf(".js") + 3));

                    if (File.Exists(path)) result.WriteLine(
                        "<script type='text/javascript' src='{0}'></script>",
                        Path.Combine(Application.StartupPath, @"interface\reports\js\", script)
                        );
                }
                catch
                {
                    result.WriteLine(script);
                }
            }

            result.WriteLine("</head>");
            if (printable) WriteLine("<body onload='window.print()'>"); else result.WriteLine("<body>");
            result.Write(this.ToString());
            result.WriteLine("</body>");
            result.WriteLine("</html>");
            return result.ToString();
        }

        

        public static string GetHtmlFromLatex(string latex)
        {
            Report report = new Report();
            report.UseEquationNumeration = false;
            report.AddEquation(latex);
            return report.GetFileContent();
        }
    }

    [Flags]
    public enum PageBreakOption
    {
        None,
        Landscape,
        Odd
    }
}