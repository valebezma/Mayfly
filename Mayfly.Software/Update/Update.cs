using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Mayfly.Software
{
    public class Update
    {
        public static void CheckUpdates(string product)
        {
            UpdatePolicy up = Server.GetUpdatePolicy(product);

            if (up == UpdatePolicy.Off) return;

            Scheme data = Service.GetScheme();

            if (data == null) return;

            UpdateArgs e = new UpdateArgs(data, product);

            if (e == null) return;

            if (!e.IsUpdateAvailable) return; 

            switch (up)
            {
                case UpdatePolicy.CheckAndNotice:
                    Notification.ShowNotificationAction(
                        string.Format(Resources.Update.UpdateAvailability, e.Product),
                        Resources.Update.UpdateInstruction,
                        () => {
                            MessageBox.Show("Updates found.");
                            Application.Run(new WizardUpdate(e));
                        });
                    //System.Threading.Thread.Sleep(5000);
                    //new WizardUpdate(e).ShowDialog();
                    break;

                case UpdatePolicy.AutoUpdate:
                    BackgroundWorker silentUpdater = new BackgroundWorker();
                    silentUpdater.DoWork += silentUpdater_DoWork;
                    silentUpdater.RunWorkerCompleted += silentUpdater_RunWorkerCompleted;
                    silentUpdater.RunWorkerAsync(e);
                    break;
            }
        }

        public static void DoUpdates(string product)
        {
            Scheme data = Service.GetScheme();

            if (data == null)
            {
                Notification.ShowNotification(
                    Resources.Update.UpdateServerUnavailable,
                    string.Format(Resources.Update.UpdateServerUnavailabilityInstruction, product));
                return;
            }

            UpdateArgs e = new UpdateArgs(data, product);

            if (e == null || !e.IsUpdateAvailable)
            {
                Notification.ShowNotification(
                    Resources.Update.UpdateUnavailable,
                    string.Format(Resources.Update.UpdateUnavailabilityInstruction, e.Product));
                return;
            }

            Application.Run(new WizardUpdate(e));
        }

        //private static void RunUpdateWizard(UpdateArgs e)
        //{
        //    Application.Run(new WizardUpdate(e));
        //}

        private static void silentUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateArgs updates = (UpdateArgs)e.Argument;

            List<Uri> downloadLinks = new List<Uri>(updates.GetDownloadLinks());
            List<string> tempLinks = new List<string>();

            while (downloadLinks.Count > 0)
            {
                Uri uri = downloadLinks[0];

                string feature = Path.GetFileName(uri.LocalPath);

                // Downloading basic feature
                WebClient webClient = new WebClient();
                string tempFileName = Path.Combine(FileSystem.TempFolder, feature);
                webClient.DownloadFile(uri, tempFileName);
                tempLinks.Add(tempFileName);

                // Downloading localization
                try
                {
                    CultureInfo ci = CultureInfo.CurrentCulture;
                    Uri loc = Server.GetLocalizedUri(downloadLinks[0], ci);

                    webClient = new WebClient();
                    tempFileName = Path.Combine(FileSystem.TempFolder, ci.Name, feature);
                    if (!Directory.Exists(Path.Combine(FileSystem.TempFolder, ci.Name)))
                    {
                        Directory.CreateDirectory(Path.Combine(FileSystem.TempFolder, ci.Name));
                    }
                    webClient.DownloadFile(loc, tempFileName);
                }
                catch { }

                // Signal
                downloadLinks.RemoveAt(0);
            }

            // Unpack dats
            if (MessageBox.Show("Close all Mayfly application windows to complete update",
                "Mayfly software update", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Close all windows

                foreach (string tempFile in tempLinks)
                {
                    FileInfo fi = new FileInfo(tempFile);

                    //Type formType = typeof(Form);
                    //foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                    //    if (formType.IsAssignableFrom(type))
                    //    {
                    //        // type is a Form
                    //    }


                    FileSystem.UnpackFiles(tempFile, updates.Product);

                    if (Service.GetAvailableCultures().
                        Contains(CultureInfo.CurrentCulture))
                    {
                        fi = new FileInfo(tempFile.Insert(tempFile.LastIndexOf('\\'), '\\' + CultureInfo.CurrentCulture.Name));
                        if (fi.Exists)
                        {
                            FileSystem.UnpackFiles(fi.FullName, updates.Product);
                        }
                    }
                }

                e.Result = e.Argument;

                FileSystem.ClearTemp();
            }
        }

        private static void silentUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is UpdateArgs)
            {
                UpdateArgs args = (UpdateArgs)e.Result;

                Notification.ShowNotificationAction(
                    string.Format(Resources.Interface.UpdateInstalled, args.Product),
                    Resources.Interface.UpdateInstalledInstruction,
                    () => {
                        ReleaseNotes releaseNotes = new ReleaseNotes(args);
                        releaseNotes.Show();
                    });
            }
        }
    }
}
