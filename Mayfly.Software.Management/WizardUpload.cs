using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using Mayfly.Extensions;
using LibGit2Sharp;
using Version = System.Version;

namespace Mayfly.Software.Management
{
    public partial class WizardUpload : Form
    {
        string[] serverfiles;



        public WizardUpload()
        {
            InitializeComponent();
            buttonScheme.Enabled = ServerData.SchemeData.GetChanges() != null;
        }

        

        private void CheckMissing()
        {
            List<string> missing = new List<string>();

            foreach (Scheme.FileRow fileRow in ServerData.SchemeData.File)
            {
                foreach (Scheme.SatelliteRow satRow in fileRow.GetSatelliteRows())
                {
                    if (Directory.Exists(satRow.Path)) continue;
                    if (File.Exists(satRow.Path)) continue;
                    missing.Add(satRow.Path);
                }

                if (File.Exists(fileRow.File)) continue;
                missing.Add(fileRow.File);
            }

            missing.Sort();

            textBoxMissingFiles.Text = string.Empty;

            foreach (string filename in missing)
            {
                textBoxMissingFiles.AppendText(filename + Environment.NewLine);
            }

            if (missing.Count == 0) wizardPageMissing.Suppress = true;
        }

        private void CheckNews()
        {
            int n = 0;
            foreach (Scheme.FileRow fileRow in ServerData.SchemeData.File)
            {
                if (!File.Exists(fileRow.File)) continue;

                FileInfo localFile = new FileInfo(fileRow.File);

                Version newversion = new Version(FileVersionInfo.GetVersionInfo(localFile.FullName).FileVersion);
                Version oldversion = fileRow.GetLatestVersion();

                if (newversion > oldversion)
                {
                    spreadSheetNews.Rows.Add(fileRow.File, newversion, oldversion, fileRow.GetLatestChanges());
                    n++;
                }
            }

            if (n == 0)
            {
                wizardPageStart.ShowNext = false;

                labelStart.Visible = false;
                labelNoChanges.Visible = 
                    buttonForce.Visible = 
                    buttonScheme.Visible = true;
                //wizardPageStart.IsFinishPage = true;
            }
            else
            {
                wizardControl.NextPage();
                labelUpdate.ResetFormatted(n);
            }
        }

        int binaryIndex = 0;

        private void ChangeNote()
        {
            // Check 

            textBoxBin.Text = (string)spreadSheetNews[ColumnBinary.Index, binaryIndex].Value;
            textBoxPub.Text = ((Version)spreadSheetNews[ColumnVersionPublished.Index, binaryIndex].Value).ToString();
            textBoxCur.Text = ((Version)spreadSheetNews[ColumnVersionCurrent.Index, binaryIndex].Value).ToString();
            textBoxNoteLast.Text = (string)spreadSheetNews[ColumnChanges.Index, binaryIndex].Value;
            Scheme.FileRow fileRow = ServerData.SchemeData.File.FindByFile(textBoxBin.Text);

            if (fileRow != null)
            {
                textBoxNote.ReadOnly = false;

                if (fileRow.GetLatestVersion().ToString() == textBoxCur.Text) // if these notes already saved
                {
                    Scheme.VersionRow latest = ServerData.SchemeData.File.FindByFile(textBoxBin.Text).GetLatestVersionRow();
                    if (!latest.IsChangesNull())
                        textBoxNote.Text = latest.Changes;
                }
                else // If not:
                {
                    textBoxNote.Text = string.Empty;
                }
            }
            else
            {
                textBoxNote.ReadOnly = true;
            }

            textBoxNote.Focus();
        }

        private void SaveNote()
        {
            Scheme.FileRow fileRow = ServerData.SchemeData.File.FindByFile(textBoxBin.Text);

            if (fileRow == null) return;

            Scheme.VersionRow versionRow = ServerData.SchemeData.Version.FindByFileVersion(textBoxBin.Text, textBoxCur.Text);

            if (versionRow == null)
            {
                versionRow = ServerData.SchemeData.Version.NewVersionRow();
                versionRow.FileRow = fileRow;
                versionRow.Version = textBoxCur.Text;
                versionRow.Published = DateTime.Today.AddDays(1);

                ServerData.SchemeData.Version.AddVersionRow(versionRow);
            }

            if (string.IsNullOrWhiteSpace(textBoxNote.Text)) { versionRow.SetChangesNull(); }
            else { versionRow.Changes = textBoxNote.Text.Trim(); }
        }



        private void WizardUpload_Load(object sender, EventArgs e)
        {
            CheckMissing();
            CheckNews();
        }

        private void wizardControl_Cancelling(object sender, EventArgs e)
        {
            Close();
        }

        private void wizardControl_Finished(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }


        private void buttonForce_Click(object sender, EventArgs e)
        {
            checkBoxBackup.Checked = false;
            wizardPageStart.NextPage = wizardPageUpload;
            wizardControl.NextPage();
        }

        private void buttonScheme_Click(object sender, EventArgs e)
        {
            //Server.UploadFile(Encoding.UTF8.GetBytes(SchemeData.GetXml()), Service.SchemeFtpUri);

            ServerData.UpdateDatabase();

            wizardPageStart.NextPage = wizardPageDone;
            wizardControl.NextPage();
        }


        private void wizardPageNews_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (spreadSheetNews.RowCount > 1)
            {
                wizardPageNotes.NextPage = wizardPageNotes;
            }

            binaryIndex = 0;

            ChangeNote();
        }
        

        private void spreadSheetReleaseNotes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 1 && e.RowIndex == 0)
            //    wizardPageNotes.AllowNext = spreadSheetReleaseNotes[ColumnNote.Index, 0].Value != null; //allow;
        }

        private void textBoxNoteLast_DoubleClick(object sender, EventArgs e)
        {
        }

        private void textBoxNote_DoubleClick(object sender, EventArgs e)
        {
            if (textBoxNote.Text != string.Empty) return;

            if (folderRepo.ShowDialog(this) == DialogResult.OK)
            {
                List<string> result = new List<string>();

                foreach (var commit in Service.GetCommits(Path.GetDirectoryName(folderRepo.SelectedPath),
                    Path.GetFileName(folderRepo.SelectedPath)))
                {
                    result.Add(commit.Message);
                }

                textBoxNote.Lines = result.ToArray();
            }
        }

        private void wizardPageNotes_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveNote();

            if (binaryIndex == spreadSheetNews.RowCount - 1)
            {
                wizardPageNotes.NextPage = wizardPageUpload;
            }
            else
            {
                binaryIndex++;
                ChangeNote();
            }
        }

        private void wizardPageNotes_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveNote();

            if (binaryIndex > 0)
            {
                binaryIndex--;
                ChangeNote();
            }
        }


        private void wizardPageUpload_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            List<string> filenames = new List<string>();

            foreach (Scheme.FileRow fileRow in ServerData.SchemeData.File)
            {
                filenames.AddRange(fileRow.GetFilesList());

                foreach (CultureInfo ci in ServerSoftware.AvailableCultures)
                {
                    filenames.AddRange(fileRow.GetFilesList(ci));
                }
            }

            filenames.Sort();

            textBoxUpFiles.Text = string.Empty;

            foreach (string filename in filenames)
            {
                textBoxUpFiles.AppendText(filename + Environment.NewLine);
            }
        }

        private void wizardPageUpload_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            serverfiles = ServerFiles.GetFilenames("current/");

            progressUpload.Maximum =
                (checkBoxBackup.Checked ? serverfiles.Length : 0) + // For backing up files
                ServerData.SchemeData.File.Count * (1 + ServerSoftware.AvailableCultures.Count) + // For uploading features
                1 + // For scheme files
                1; // For clearing files

            uploader.RunWorkerAsync();
        }
        
        private void buttonDetails_Click(object sender, EventArgs e)
        {
            textBoxUpFiles.Visible = true;
            buttonDetails.Visible = false;
        }

        
        private void uploader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressUpload.Value = e.ProgressPercentage;
        }

        private void uploader_DoWork(object sender, DoWorkEventArgs e)
        {
            int fileCount = 0;

            if (Software.Service.IsChecked(checkBoxBackup))
            {
                Software.Service.UpdateStatus(labelUpStatus, "Backing up currently distributing version");

                //Service.Move(
                //    Server.GetUri(Service.FtpServer, "current/"),
                //    Server.GetUri(Service.FtpServer, "history/" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "/"));

                string backupUri = "history/" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "/";

                foreach (string filename in serverfiles)
                {
                    Software.Service.UpdateStatus(labelUpStatus, "Backing up feature ({0})", filename);
                    Uri uri = Server.GetUri(ServerFiles.FtpUri, "current/" + filename);
                    Uri hisUri = Server.GetUri(ServerFiles.FtpUri, backupUri + filename);
                    ServerFiles.Move(uri, hisUri);

                    fileCount++;
                    ((BackgroundWorker)sender).ReportProgress(fileCount);
                }

                //Service.Move(
                //    Server.GetUri("ftp://" + Server.Domain + "/get/software", "mayflysoftware.exe"),
                //    Server.GetUri("ftp://" + Server.Domain + "/get/software/history", backupUri + "inst.exe"));
            }

            Software.Service.UpdateStatus(labelUpStatus, "Packing and sending new features");
            
            foreach (Scheme.VersionRow versionRow in ServerData.SchemeData.Version)
            {
                if (versionRow.Published == DateTime.Today.AddDays(1))
                {
                    versionRow.Published = DateTime.Now;
                }
            }

            foreach (Scheme.FileRow fileRow in ServerData.SchemeData.File)
            {
                Software.Service.UpdateStatus(labelUpStatus, "Packing and sending feature ({0})", fileRow.File);

                string alias = fileRow.File.Replace(new FileInfo(fileRow.File).Extension, ".zip");
                string[] files = fileRow.GetFilesList();
                Uri uri = Server.GetUri(ServerFiles.FtpUri, "current/" + alias);
                ServerFiles.UploadFile(ServerFiles.PackFiles(files), uri);
                fileCount++;

                ((BackgroundWorker)sender).ReportProgress(fileCount);

                foreach (CultureInfo ci in ServerSoftware.AvailableCultures)
                {
                    Software.Service.UpdateStatus(labelUpStatus, "Packing and sending ({0}) localization files ({1})", fileRow.File, ci.DisplayName);

                    string[] locFiles = fileRow.GetFilesList(ci);

                    Uri locUri = uri.Localize(ci);
                    ServerFiles.UploadFile(ServerFiles.PackFiles(locFiles), locUri);
                    fileCount++;

                    ((BackgroundWorker)sender).ReportProgress(fileCount);
                }
            }

            Software.Service.UpdateStatus(labelUpStatus, "Clearing local temporary files");
            IO.ClearTemp();
            fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);

            Software.Service.UpdateStatus(labelUpStatus, "Updating scheme database");
            //Server.UploadFile(Encoding.UTF8.GetBytes(SchemeData.GetXml()), Service.SchemeFtpUri);
            ServerData.UpdateDatabase();
            fileCount++; ((BackgroundWorker)sender).ReportProgress(fileCount);
        }

        private void uploader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                wizardPageRoll.AllowNext = false;
                buttonRetry.Visible = true;
            }
            else
            {
                wizardPageRoll.AllowNext = true;
                wizardControl.NextPage();
            }
        }

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            buttonRetry.Visible = false;
            uploader.RunWorkerAsync();
        }


        private void buttonWebsite_Click(object sender, EventArgs e)
        {
            IO.RunFile(Server.MainUri.OriginalString);
        }

        private void buttonFtp_Click(object sender, EventArgs e)
        {
            IO.RunFile(ServerFiles.FtpUri.OriginalString);
        }
    }
}