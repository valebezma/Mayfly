using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Mayfly.Software;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Mayfly.Publication
{
    public partial class WizardUpload : Form
    {
        public UpdateArgs UpdateArgs { get; set; }

        public Scheme PublishedUpdates { get; set; }

        public List<Scheme> LocalizedUpdates { get; set; }

        public List<CultureInfo> AvailableCultures { get; set; }

        int binaryIndex = 0;

        private List<Scheme.VersionRow> VersionRows;



        public WizardUpload()
        {
            InitializeComponent();

            VersionRows = new List<Scheme.VersionRow>();

            spreadSheetReleaseNotes.Rows.Add(CultureInfo.InvariantCulture);
            AvailableCultures = Software.Service.GetAvailableCultures();
            foreach (CultureInfo ci in AvailableCultures)
            {
                spreadSheetReleaseNotes.Rows.Add(ci);
            }

            Application.UseWaitCursor = true;
            uploadInspector.RunWorkerAsync();
        }



        #region Methods

        private void RoundUp()
        {
            textBoxBin.Text = (string)spreadSheetBinaries[ColumnBinary.Index, binaryIndex].Value;
            textBoxPub.Text = ((Version)spreadSheetBinaries[ColumnVersionPublished.Index, binaryIndex].Value).ToString();
            textBoxCur.Text = ((Version)spreadSheetBinaries[ColumnVersionCurrent.Index, binaryIndex].Value).ToString();

            // Check 
            Scheme.FileRow fileRow = PublishedUpdates.File.FindByFile(textBoxBin.Text);

            if (fileRow != null)
            {
                spreadSheetReleaseNotes[ColumnNote.Index, 0].ReadOnly = false;
                if (fileRow.GetLatestVersion().ToString() ==
                    textBoxCur.Text) // if these notes already saved
                {
                    Scheme.VersionRow latest = PublishedUpdates.File.FindByFile(textBoxBin.Text).GetLatestVersionRow();
                    if (!latest.IsChangesNull())
                        spreadSheetReleaseNotes[ColumnNote.Index, 0].Value = latest.Changes;
                }
                else // If not:
                {
                    spreadSheetReleaseNotes[ColumnNote.Index, 0].Value = null;
                }
            }
            else
            {
                spreadSheetReleaseNotes[ColumnNote.Index, 0].ReadOnly = true;
            }

            for (int i = 1; i < spreadSheetReleaseNotes.RowCount; i++)
            {
                CultureInfo ci = new CultureInfo(spreadSheetReleaseNotes[ColumnCulture.Index, i].Value.ToString());

                // Check
                Scheme localizedUpdates = Updates(ci);

                Scheme.FileRow locFileRow = localizedUpdates.File.FindByFile(textBoxBin.Text);

                if (locFileRow != null)
                {
                    spreadSheetReleaseNotes[ColumnNote.Index, 0].ReadOnly = false;
                    if (locFileRow.GetLatestVersion().ToString() ==
                        textBoxCur.Text) // if these notes already saved
                    {
                        Scheme.VersionRow latest = localizedUpdates.File.FindByFile(textBoxBin.Text).GetLatestVersionRow();
                        if (!latest.IsChangesNull())
                            spreadSheetReleaseNotes[ColumnNote.Index, i].Value = latest.Changes;
                    }
                    else // If not:
                    {
                        spreadSheetReleaseNotes[ColumnNote.Index, i].Value = null;
                    }
                }
                else
                {
                    spreadSheetReleaseNotes[ColumnNote.Index, 0].ReadOnly = true;
                }
            }

            spreadSheetReleaseNotes.ClearSelection();
            spreadSheetReleaseNotes.CurrentCell = spreadSheetReleaseNotes[1, 0];
            spreadSheetReleaseNotes.Focus();
        }

        private void SaveNotes()
        {
            SaveNotes(0, PublishedUpdates);

            for (int i = 1; i < spreadSheetReleaseNotes.RowCount; i++)
            {
                SaveNotes(i, Updates((CultureInfo)spreadSheetReleaseNotes[0, i].Value));
            }
        }

        private void SaveNotes(int index, Scheme updates)
        {

            Scheme.VersionRow versionRow = updates.Version.FindByFileVersion(
                textBoxBin.Text, textBoxCur.Text);

            Scheme.FileRow fileRow = updates.File.FindByFile(textBoxBin.Text);

            if (fileRow == null) return;

            if (versionRow == null)
            {
                versionRow = updates.Version.NewVersionRow();
                versionRow.FileRow = fileRow;
                versionRow.Version = textBoxCur.Text;
                versionRow.Published = DateTime.Now;
                updates.Version.AddVersionRow(versionRow);

                VersionRows.Add(versionRow);
            }

            if (spreadSheetReleaseNotes[ColumnNote.Index, index].Value == null ||
                string.IsNullOrWhiteSpace(spreadSheetReleaseNotes[ColumnNote.Index, index].Value.ToString()))
            {
                versionRow.SetChangesNull();
            }
            else
            {
                versionRow.Changes = spreadSheetReleaseNotes[ColumnNote.Index, index].Value.ToString();
            }
        }

        private Scheme Updates(CultureInfo ci)
        {
            return LocalizedUpdates[AvailableCultures.IndexOf(ci)];
        }

        private delegate void StatusUpdater(string status);

        private void UpdateStatus(string status)
        {
            if (labelUpStatus.InvokeRequired)
            {
                StatusUpdater statusUpdater = new StatusUpdater(UpdateStatus);
                labelUpStatus.Invoke(statusUpdater, new object[] { status });
            }
            else
            {
                labelUpStatus.Text = status;
            }
        }

        #endregion


        
        private void uploadInspector_DoWork(object sender, DoWorkEventArgs e)
        {
            LocalizedUpdates = new List<Scheme>();

            foreach (CultureInfo ci in AvailableCultures)
            {
                Scheme updates = Software.Service.GetScheme(ci);
                LocalizedUpdates.Add(updates);
            }

            PublishedUpdates = Software.Service.GetScheme();

            if (PublishedUpdates == null) return;

            UpdateArgs = new UpdateArgs();

            List<UpdateFeatureArgs> componentArgs = new List<UpdateFeatureArgs>();

            foreach (Scheme.FileRow fileRow in PublishedUpdates.File)
            {
                UpdateFeatureArgs args = new UpdateFeatureArgs();
                args.Filename = fileRow.File;

                FileInfo existingFile = new FileInfo(fileRow.File);

                if (existingFile == null || !existingFile.Exists)
                {
                    throw new FileNotFoundException(string.Format(
                        "There is missing file in project files. Look after {0}.",
                        fileRow.File));
                }
                else
                {
                    args.OldVersion = fileRow.GetLatestVersion();
                    args.NewVersion = new Version(FileVersionInfo.GetVersionInfo(existingFile.FullName).FileVersion);
                }
                componentArgs.Add(args);
            }

            UpdateArgs.Components = componentArgs.ToArray();

            //foreach (Scheme updates in LocalizedUpdates)
            //{
            //    foreach (Scheme.FileRow fileRow in updates.File)
            //    {
            //        if (!UpdateArgs.Contains(fileRow.File))
            //        {
            //            ComponentEventArgs args = new ComponentEventArgs();
            //            args.Filename = fileRow.File;

            //            FileInfo existingFile = new FileInfo(fileRow.File);

            //            if (existingFile == null || !existingFile.Exists)
            //            {
            //                throw new FileNotFoundException(string.Format(
            //                    "There is missing file in project files. Look after {0}.",
            //                    fileRow.File));
            //            }
            //            else
            //            {
            //                args.OldVersion = fileRow.GetLatestVersion();
            //                args.NewVersion = new Version(FileVersionInfo.GetVersionInfo(existingFile.FullName).FileVersion);
            //            }
            //            componentArgs.Add(args);
            //        }

            //    }
            //}

            UpdateArgs.Components = componentArgs.ToArray();
        }

        private void uploadInspector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Application.UseWaitCursor = false;

            if (UpdateArgs.IsUpdateAvailable)
            {
                int updates = 0;

                foreach (UpdateFeatureArgs feature in UpdateArgs.Components)
                {
                    if (feature.IsUpdateAvailable)
                    {
                        //UpdateServer.FileRow fileRow = Published.File.FindByFile(feature.Filename);
                        spreadSheetBinaries.Rows.Add(feature.Filename,
                            feature.NewVersion,
                            feature.OldVersion);
                        updates++;
                    }
                }

                labelUpdate.Text = string.Format(
                    new ResourceManager(typeof(WizardUpload)).GetString("labelUpdate.Text"),
                    updates);

                wizardControl.NextPage();
            }
            else
            {
                wizardPageStart.ShowNext = false;
                labelStart.Visible = false;
                labelNoChanges.Visible = buttonForce.Visible = true;
                //wizardPageStart.IsFinishPage = true;
            }
        }


        private void wizardControl_Cancelling(object sender, EventArgs e)
        {
            Close();
        }

        private void wizardControl_Finished(object sender, EventArgs e)
        {
            Close();
        }


        private void wizardPageChanges_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {

        }

        private void wizardPageBinaries_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (spreadSheetBinaries.RowCount > 1)
            {
                wizardPageNotes.NextPage = wizardPageNotes;
            }

            binaryIndex = 0;
            RoundUp();
        }

        private void spreadSheetReleaseNotes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1 && e.RowIndex == 0)
            //    wizardPageNotes.AllowNext = spreadSheetReleaseNotes[ColumnNote.Index, 0].Value != null; //allow;
        }

        private void wizardPageNotes_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            // Save release notes
            SaveNotes();

            if (binaryIndex == spreadSheetBinaries.RowCount - 1)
            {
                wizardPageNotes.NextPage = wizardPageUpload;
            }
            else
            {
                // Increase counter
                binaryIndex++;

                // Load new values
                RoundUp();
            }
        }

        private void wizardPageNotes_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            // Save release notes
            SaveNotes();

            if (binaryIndex == 0)
            {
                //wizardPageNotes.Pre = wizardPageUpload;
            }
            else
            {
                // Increase counter
                binaryIndex--;

                // Load new values
                RoundUp();
            }
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            textBoxFiles.Visible = true;
            buttonDetails.Visible = false;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                PublishedUpdates.WriteXml(Path.Combine(folderBrowserDialog.SelectedPath,
                    Software.Service.SchemeFile));

                foreach (Scheme updates in LocalizedUpdates)
                {
                    CultureInfo ci = AvailableCultures[LocalizedUpdates.IndexOf(updates)];
                    string path = Path.Combine(folderBrowserDialog.SelectedPath, ci.Name.ToLowerInvariant());
                    Directory.CreateDirectory(path);
                    updates.WriteXml(Path.Combine(path, Software.Service.SchemeFile));
                }
            }
        }

        private void wizardPageUpload_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            List<string> filenames = new List<string>();

            foreach (Scheme.FileRow fileRow in PublishedUpdates.File)
            {
                filenames.AddRange(fileRow.GetFilesList());

                foreach (CultureInfo ci in AvailableCultures)
                {
                    filenames.AddRange(fileRow.GetFilesList(ci));
                }
            }

            foreach (Scheme updates in LocalizedUpdates)
            {
                CultureInfo ci = AvailableCultures[LocalizedUpdates.IndexOf(updates)];

                foreach (Scheme.FileRow fileRow in updates.File)
                {
                    if (!filenames.Contains(fileRow.File))
                    {
                        filenames.AddRange(fileRow.GetFilesList());
                    }
                }
            }

            filenames.Sort();

            textBoxFiles.Text = string.Empty;

            foreach (string filename in filenames)
            {
                textBoxFiles.AppendText(filename + Environment.NewLine);
            }
        }

        private void wizardPageUpload_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            progressUpload.Maximum = (1 + PublishedUpdates.File.Count) * (1 + LocalizedUpdates.Count);

            //foreach (Scheme updates in LocalizedUpdates)
            //{
            //    progressUpload.Maximum += updates.File.Count;
            //}

            foreach (Scheme.VersionRow versionRow in VersionRows)
            {
                DateTime dt = DateTime.Now;
                dt = dt.AddMilliseconds(-dt.Millisecond);
                dt = dt.AddSeconds(-dt.Second);
                versionRow.Published = dt;
            }

            uploader.RunWorkerAsync();
        }

        private void uploader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressUpload.Value = e.ProgressPercentage;
        }

        private void uploader_DoWork(object sender, DoWorkEventArgs e)
        {
            int fileCount = 0;

            List<string> sent = new List<string>();

            // Create directory for history
            UpdateStatus("Laying out backup structure");

            Uri backupUriDir = Server.GetUri(Service.FtpServer, "history/" +  DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "/");
            FtpWebRequest ftpDir = (FtpWebRequest)FtpWebRequest.Create(backupUriDir);
            ftpDir.Method = WebRequestMethods.Ftp.MakeDirectory;
            ftpDir.GetResponse();


            foreach (CultureInfo ci in AvailableCultures)
            {
                ftpDir = (FtpWebRequest)FtpWebRequest.Create(Server.GetLocalizedUri(backupUriDir, ci));
                ftpDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpDir.GetResponse();
            }

            foreach (Scheme.FileRow fileRow in PublishedUpdates.File)
            {
                UpdateStatus(string.Format("Backing up, packing and sending feature ({0})", fileRow.File));

                string alias = fileRow.File.Replace(new FileInfo(fileRow.File).Extension, ".zip");

                string[] files = fileRow.GetFilesList();

                Uri uri = Server.GetUri(Service.FtpServer, "current/" + alias);
                Uri hisUri = Server.GetUri(Service.FtpServer, backupUriDir.LocalPath.Substring(1) + alias);
                Service.Move(uri, hisUri);
                Server.UploadFile(Service.PackFiles(files), uri);

                sent.Add(fileRow.File);
                fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);

                foreach (CultureInfo ci in AvailableCultures)
                {
                    UpdateStatus(string.Format("Backing up, packing and sending feature ({0}) localization files ({1})", fileRow.File, ci.DisplayName));

                    string[] locFiles = fileRow.GetFilesList(ci);

                    Uri locUri = Server.GetLocalizedUri(uri, ci);
                    Uri backupLocUri = Server.GetLocalizedUri(hisUri, ci);

                    Service.Move(locUri, backupLocUri);
                    Server.UploadFile(Service.PackFiles(locFiles), locUri);
                    fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);
                }
            }

            //foreach (Scheme updates in LocalizedUpdates)
            //{
            //    CultureInfo ci = AvailableCultures[LocalizedUpdates.IndexOf(updates)];

            //    foreach (Scheme.FileRow fileRow in updates.File)
            //    {
            //        if (!sent.Contains(fileRow.File))
            //        {
            //            UpdateStatus(string.Format("Packing and sending feature ({0})", fileRow.File));

            //            string alias = fileRow.File.Replace(new FileInfo(fileRow.File).Extension, ".zip");

            //            string[] files = fileRow.GetFilesList();
            //            Service.UploadFile(
            //                Server.GetUri(Service.FtpServer, alias),
            //                credentialDialogFtp.Credentials, Service.PackFiles(files));
            //        }

            //        fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);
            //    }
            //}

            if (UpdateArgs.IsUpdateAvailable)
            {
                UpdateStatus("International versions configuration");

                Server.UploadFile(Encoding.UTF8.GetBytes(PublishedUpdates.GetXml()),
                        Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile));
                fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);

                foreach (Scheme updates in LocalizedUpdates)
                {
                    CultureInfo ci = AvailableCultures[LocalizedUpdates.IndexOf(updates)];

                    UpdateStatus(string.Format("Local versions configuration ({0})", ci.DisplayName));

                    Server.UploadFile(Encoding.UTF8.GetBytes(updates.GetXml()),
                        Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile, ci));

                    fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);
                }
            }

            FileSystem.ClearTemp();
        }

        private void uploader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                buttonRetry.Visible = true;
            }
            else
            {
                progressUpload.Visible = labelUploading.Visible = labelUpStatus.Visible = false;
                labelDone.Visible = buttonWebsite.Visible = buttonFtp.Visible = true;
            }
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            uploader.RunWorkerAsync();
        }

        private void buttonWebsite_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(Server.ServerHttps);
        }

        private void buttonFtp_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(Service.FtpServer);
        }


        private void buttonForce_Click(object sender, EventArgs e)
        {
            wizardPageStart.NextPage = wizardPageUpload;
            wizardControl.NextPage();
        }

        private void WizardUpload_Load(object sender, EventArgs e)
        { }
    }
}