using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Software;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;

namespace Mayfly.Software
{
    public partial class WizardUpdate : Form
    {
        public UpdateArgs UpdateArgs { get; set; }



        public WizardUpdate(UpdateArgs e) : this(e, false) { }

        public WizardUpdate(UpdateArgs e, bool minimized)
        {
            InitializeComponent();

            listViewRun.Shine();

            UpdateArgs = e;

            if (UpdateArgs == null || !UpdateArgs.IsUpdateAvailable)
            {
                Application.Exit();
                return;
            }

            wizardPageStart.AllowNext = true;
            wizardPageStart.Suppress = true;
            labelCheckRun.ResetFormatted(e.Product);
            labelUpdate.Text = string.Format(Resources.Interface.UpdatesReady, UpdateArgs.Product);
            textBoxReleaseNotes.Text = UpdateArgs.GetReleaseNotes();
            progressBar.Maximum = UpdateArgs.Components.Length * 2;

            if (minimized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                backgroundKiller.RunWorkerAsync();
            }
        }



        public void Zeig()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true; // some reason it becomes black in header
        }

        private void wizardPageNotes_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            textBoxReleaseNotes.ScrollToEnd();
        }

        private void wizardPageNotes_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            buttonRunCheck_Click(this, new EventArgs());
        }

        private void buttonRunCheck_Click(object sender, EventArgs e)
        {
            List<Process> instances = new List<Process>();
            foreach (UpdateFeatureArgs arg in UpdateArgs.Components)
            {
                FileInfo fi = new FileInfo(arg.Filename);
                if (fi.Extension == ".exe")
                {
                    Process[] prc = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(fi.Name));
                    instances.AddRange(prc);
                }
            }

            listViewRun.Items.Clear();
            foreach (Process prc in instances)
            {
                listViewRun.CreateItem(prc.Id.ToString(), prc.MainWindowTitle);
            }

            wizardPageRun.AllowNext =
            wizardPageRun.Suppress =
                listViewRun.Items.Count == 0;

            if (wizardControlUpdate.SelectedPage == wizardPageRun && wizardPageRun.Suppress)
            {
                wizardControlUpdate.NextPage();
            }
        }

        private void wizardPageGet_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            backgroundDownloader.RunWorkerAsync(UpdateArgs);
        }

        private void backgroundDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            Software.Update.InstallUpdates(
                (UpdateArgs)e.Argument,
                labelStatus,
                backgroundDownloader);
        }

        private void backgroundDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                wizardPageError.Suppress = true;
                wizardPageGet.AllowNext = true;
                wizardControlUpdate.NextPage();

                FileSystem.ClearTemp();
            }
            else
            {
                labelError.ResetFormatted(e.Error.Message);

                wizardPageError.Suppress = false;
                wizardPageDone.Suppress = true;
                wizardControlUpdate.NextPage();
            }
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