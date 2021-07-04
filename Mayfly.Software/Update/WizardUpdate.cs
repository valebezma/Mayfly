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

namespace Mayfly.Software
{
    public partial class WizardUpdate : Form
    {
        public UpdateArgs UpdateArgs { get; set; }

        List<Uri> DownloadLinks = new List<Uri>();
        List<string> TempLinks = new List<string>();
        int progress = 0;

        private delegate void StatusUpdater(string status);

                

        public WizardUpdate(UpdateArgs e)
        {
            InitializeComponent();

            UpdateArgs = e;

            if (e == null || !e.IsUpdateAvailable)
            {
                Application.Exit();
                return;
            }

            wizardPageStart.AllowNext = true;
            wizardPageStart.Suppress = true;
            labelUpdate.Text = string.Format(Resources.Interface.UpdatesReady, e.Product);
            textBoxReleaseNotes.Text = UpdateArgs.GetReleaseNotes();
        }



        private void UpdateStatus(string status)
        {
            if (labelStatus.InvokeRequired)
            {
                StatusUpdater statusUpdater = new StatusUpdater(UpdateStatus);
                labelStatus.Invoke(statusUpdater, new object[] { status });
            }
            else
            {
                labelStatus.Text = status;
            }
        }



        private void wizardControlUpdate_Finished(object sender, EventArgs e)
        {
            //UserSettingHandling.SetValue(Path, UserSettingPaths.UpdatePolicy,
            //    checkBoxAutoUpdate.Checked ? (int)UpdatePolicy.AutoUpdate : (int)UpdatePolicy.CheckAndNotice);
            //Close();
        }

        private void wizardPageNotes_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            textBoxReleaseNotes.ScrollToEnd();
        }

        private void wizardPageNotes_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            progressBar.Maximum = UpdateArgs.Components.Length*2;

            DownloadLinks.Clear();
            DownloadLinks.AddRange(UpdateArgs.GetDownloadLinks());

            progress = 0;
            backgroundDownloader.RunWorkerAsync(UpdateArgs.Product);
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = @"BUILTIN\Administrators")]
        //[PermissionSet(SecurityAction.LinkDemand, Name="FullTrust")]
        private void backgroundDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            string product = (string)e.Argument;

            // Download dats
            while (DownloadLinks.Count > 0)
            {
                Uri uri = DownloadLinks[0];
                
                string feature = Path.GetFileName(uri.LocalPath);
                UpdateStatus(string.Format(Resources.Interface.UpdateDownload, feature));

                // Downloading basic feature
                WebClient webClient = new WebClient();
                string tempFileName = Path.Combine(FileSystem.TempFolder, feature);
                webClient.DownloadFile(uri, tempFileName);

                TempLinks.Add(tempFileName);

                // Downloading localization

                if (Service.GetAvailableCultures().Contains(CultureInfo.CurrentCulture))
                {
                    CultureInfo ci = CultureInfo.CurrentCulture;

                    Uri loc = Server.GetLocalizedUri(DownloadLinks[0], ci);

                    //if (Server.FileExists(loc))
                    //{
                        UpdateStatus(string.Format(Resources.Interface.UpdateDownloadLoc, feature, ci.Name));

                        webClient = new WebClient();
                        string tempLocFileName = Path.Combine(FileSystem.TempFolder, ci.Name, feature);
                        if (!Directory.Exists(Path.Combine(FileSystem.TempFolder, ci.Name)))
                        {
                            Directory.CreateDirectory(Path.Combine(FileSystem.TempFolder, ci.Name));
                        }

                        try { webClient.DownloadFile(loc, tempLocFileName); }
                        catch (WebException) { }

                        //TempLinks.Add(tempLocFileName);
                    //}
                }
                // Signal
                DownloadLinks.RemoveAt(0);
                progress++; backgroundDownloader.ReportProgress(progress);
            }

            // Unpack dats

            foreach (string tempFile in TempLinks)
            {
                FileInfo fi = new FileInfo(tempFile);
                UpdateStatus(string.Format(Resources.Interface.UpdateInstalling, fi.Name));
                FileSystem.UnpackFiles(tempFile, UpdateArgs.ProductFolder);

                if (Service.GetAvailableCultures().Contains(CultureInfo.CurrentCulture))
                {
                    CultureInfo ci = CultureInfo.CurrentCulture;
                    fi = new FileInfo(tempFile.Insert(tempFile.LastIndexOf('\\'), '\\' + ci.Name));
                    if (fi.Exists)
                    {
                        FileSystem.UnpackFiles(fi.FullName, UpdateArgs.ProductFolder);
                    }
                }

                progress++; backgroundDownloader.ReportProgress(progress);
            }
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
    }
}