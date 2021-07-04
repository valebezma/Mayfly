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

namespace Mayfly.Software.Management
{
    public partial class WizardRoll : Form
    {
        public Scheme SchemeData { get; set; }

        public List<CultureInfo> AvailableCultures { get; set; }



        public WizardRoll(Scheme scheme)
        {
            InitializeComponent();
            SchemeData = scheme;
            AvailableCultures = Software.Service.GetAvailableCultures();
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


        private void buttonDetails_Click(object sender, EventArgs e)
        {
            textBoxFiles.Visible = true;
            buttonDetails.Visible = false;
        }

        private void wizardPageUpload_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            List<string> filenames = new List<string>();

            foreach (Scheme.FileRow fileRow in SchemeData.File)
            {
                filenames.AddRange(fileRow.GetFilesList());

                foreach (CultureInfo ci in AvailableCultures)
                {
                    filenames.AddRange(fileRow.GetFilesList(ci));
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
            progressUpload.Maximum = (1 + SchemeData.File.Count)  * (1 + AvailableCultures.Count) + 1;

            foreach (Scheme.VersionRow versionRow in SchemeData.Version)
            {
                if (versionRow.Published == DateTime.Today.AddDays(1))
                {
                    //DateTime dt = DateTime.Now;
                    //dt = dt.AddMilliseconds(-dt.Millisecond);
                    //dt = dt.AddSeconds(-dt.Second);
                    //versionRow.Published = dt;

                    versionRow.Published = DateTime.Now;
                }
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
            
            Software.Service.UpdateStatus(labelUpStatus, "Backing up currently distributing version");

            string localpathhist = "history/" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "/";

            // Get files list in 

            Uri currents = new Uri(Service.FtpServer + "current/");

            foreach (string filename in Service.GetFilenames(currents))
            {
                //Software.Service.UpdateStatus(labelUpStatus, "Backing up feature ({0})", filename);

                //Uri uri = Server.GetUri(Service.FtpServer, "current/" + alias);
                //Uri hisUri = Server.GetUri(Service.FtpServer, localpathhist + alias);
                //Service.Move(uri, hisUri);
                fileCount++;

                ((BackgroundWorker)sender).ReportProgress(fileCount);

                //foreach (string foldername in Service.GetFolders(currents))
                //{
                //    Software.Service.UpdateStatus(labelUpStatus, "Backing up feature ({0}) localization files ({1})", fileRow.File, foldername);

                //    string[] locFiles = fileRow.GetFilesList(ci);

                //    Uri locUri = Server.GetLocalizedUri(uri, ci);
                //    Uri backupLocUri = Server.GetLocalizedUri(hisUri, ci);

                //    Service.Move(locUri, backupLocUri);

                //    ((BackgroundWorker)sender).ReportProgress(fileCount);
                //}
            }

            Software.Service.UpdateStatus(labelUpStatus, "Packing and sending new features");

            List<string> sent = new List<string>();

            foreach (Scheme.FileRow fileRow in SchemeData.File)
            {
                Software.Service.UpdateStatus(labelUpStatus, "Packing and sending feature ({0})", fileRow.File);

                string alias = fileRow.File.Replace(new FileInfo(fileRow.File).Extension, ".zip");
                string[] files = fileRow.GetFilesList();
                Uri uri = Server.GetUri(Service.FtpServer, "current/" + alias);                
                Server.UploadFile(Service.PackFiles(files), uri);
                sent.Add(fileRow.File);
                fileCount++;

                ((BackgroundWorker)sender).ReportProgress(fileCount);

                foreach (CultureInfo ci in AvailableCultures)
                {
                    Software.Service.UpdateStatus(labelUpStatus, "Packing and sending ({0}) localization files ({1})", fileRow.File, ci.DisplayName);

                    string[] locFiles = fileRow.GetFilesList(ci);

                    Uri locUri = Server.GetLocalizedUri(uri, ci);
                    Server.UploadFile(Service.PackFiles(locFiles), locUri);
                    fileCount++;

                    ((BackgroundWorker)sender).ReportProgress(fileCount);
                }
            }

            Software.Service.UpdateStatus(labelUpStatus, "Clearing local temporary files");
            FileSystem.ClearTemp();

            Software.Service.UpdateStatus(labelUpStatus, "Uploading scheme file");
            Server.UploadFile(Encoding.UTF8.GetBytes(SchemeData.GetXml()), Service.SchemeFtpUri);
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
    }
}