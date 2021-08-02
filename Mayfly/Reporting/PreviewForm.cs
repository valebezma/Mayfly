using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Mayfly.Extensions;

namespace Mayfly.Reporting
{
    public partial class PreviewForm : Form
    {
        public Report Report { get; }

        public bool AutoPrint { get; }

        string Uri;

        public PreviewForm()
        {
            InitializeComponent();
            this.SetMaximumDesktopView(50);
        }

        public PreviewForm(string uri) : this()
        {
            Uri = uri;
        }

        public PreviewForm(Report report, bool print) : this(FileSystem.GetTempFileName(".html"))
        {
            Report = report;
            AutoPrint = print;
            this.ResetText(report.Title, EntryAssemblyInfo.Title);
            Report.WriteToFile(Uri, AutoPrint);
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            webView.Source = new Uri(Uri);
        }
    }
}
