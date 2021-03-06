using System;
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

        void Save()
        {
            IO.InterfaceReport.SaveDialog.FileName = Title;

            if (IO.InterfaceReport.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                this.WriteToFile(IO.InterfaceReport.SaveDialog.FileName);
                IO.RunFile(IO.InterfaceReport.SaveDialog.FileName);
            }
        }

        public void Preview()
        {
            string filename = IO.GetTempFileName(".html");
            this.WriteToFile(filename, false);
            IO.RunFile(filename);

            //Reporting.PreviewForm preview = new Reporting.PreviewForm(this, false);
            //preview.ShowDialog();
        }

        public void Print()
        {
            string filename = IO.GetTempFileName(".html");
            this.WriteToFile(filename, true);
            IO.RunFile(filename);

            //Reporting.PreviewForm preview = new Reporting.PreviewForm(this, true);
            //preview.ShowDialog();
        }
    }
}
