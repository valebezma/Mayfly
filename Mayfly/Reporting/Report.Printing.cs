﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Mayfly
{
    partial class Report
    {
        public void WriteToFile(string filename)
        {
            WriteToFile(filename, false);
        }

        public void WriteToFile(string filename, bool printable)
        {
            switch (Path.GetExtension(filename))
            {
                case ".html":
                    File.WriteAllText(filename, this.GetFileContent(printable), System.Text.Encoding.UTF8);
                    break;

                //case ".pdf":
                //    File.WriteAllBytes(filename, this.GetPDF());
                //    break;
            }
        }

        public void Run()
        {
            Log.Write(EventType.ReportBuilt, String.Format("Report is built: {0}.", this.Title));            

            if (Form.ModifierKeys.HasFlag(Keys.Control | Keys.Shift))
            {
                PrintNow();
            }
            else
            {
                if (Form.ModifierKeys.HasFlag(Keys.Control))
                {
                    Save();
                }
                else if (Form.ModifierKeys.HasFlag(Keys.Shift))
                {
                    Print();
                }
                else
                {
                    Preview();
                }
            }
        }

        void Save()
        {
            FileSystem.InterfaceReport.SaveDialog.FileName = Title;

            if (FileSystem.InterfaceReport.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                this.WriteToFile(FileSystem.InterfaceReport.SaveDialog.FileName);
                FileSystem.RunFile(FileSystem.InterfaceReport.SaveDialog.FileName);
            }
        }

        public void Preview()
        {
            //ReportForm preview = new ReportForm(this);
            //preview.ShowDialog(owner);

            string filename = FileSystem.GetTempFileName(".html");
            this.WriteToFile(filename);
            FileSystem.RunFile(filename);
        }

        public void Print()
        {
            string filename = FileSystem.GetTempFileName(".html");
            this.WriteToFile(filename, true);
            FileSystem.RunFile(filename);
        }

        void PrintNow()
        {
            string filename = FileSystem.GetTempFileName(".html");
            this.WriteToFile(filename, true);
            FileSystem.RunFile(filename);

            //WebBrowser webBrowser = new WebBrowser();
            //webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDocumentNow);
            //webBrowser.DocumentText = this.GetFileContent();
        }
    }
}