using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Software;
using System.IO;
using System.Threading;

namespace Mayfly.Software
{
    public partial class ReleaseNotes : Form
    {
        public ReleaseNotes(UpdateArgs e)
        {
            InitializeComponent();
            textBoxReleaseNotes.Text = e.GetReleaseNotes();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            backgroundKiller.RunWorkerAsync();
        }



        public void Zeig()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backgroundKiller_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(300000);
        }

        private void backgroundKiller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!this.ShowInTaskbar) Close();
        }
    }
}
