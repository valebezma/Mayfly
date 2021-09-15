using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Software
{
    public abstract class Update
    {
        public readonly static Uri UpdatesUri = Server.GetUri("get/updates/current/");

        public static Uri GetUri(string localPath)
        {
            return Server.GetUri(UpdatesUri, localPath);
        }

        public static Uri GetUri(string localPath, CultureInfo ci)
        {
            return Server.GetUri(UpdatesUri, localPath, ci);
        }

        public static void RunUpdates(string product, bool force)
        {
            UpdatePolicy policy = UserSettings.UpdatePolicy;
            if (policy == UpdatePolicy.Off) return;

            //force |= (policy == UpdatePolicy.CheckAndRun);

            Scheme data = ServerSoftware.GetScheme("Product", "File", "Feature", "Version");
            if (data == null)
            {
                if (force)
                {
                    Notification.ShowNotification(
                    Resources.Update.UpdateServerUnavailable,
                    string.Format(Resources.Update.UpdateServerUnavailabilityInstruction, product));
                }
                return;
            }

            UpdateArgs e = new UpdateArgs(data, product);
            if (e == null || !e.IsUpdateAvailable)
            {
                if (force)
                {
                    Notification.ShowNotification(
                    Resources.Update.UpdateUnavailable,
                    string.Format(Resources.Update.UpdateUnavailabilityInstruction, e.Product));
                }
                return;
            }
            if (e == null) return;
            if (!e.IsUpdateAvailable) return;
            force |= (policy == UpdatePolicy.CheckAndRun);

            if (force)
            {
                Application.Run(new WizardUpdate(e));
            }
            else
            {
                //switch (policy)
                //{
                //    case UpdatePolicy.CheckAndNotice:
                Application.Run(new WizardUpdate(e, true));
                Notification.ShowNotificationAction(
                    string.Format(Resources.Update.UpdateAvailability, e.Product),
                    Resources.Update.UpdateInstruction,
                    () =>
                    {
                        ((WizardUpdate)Application.OpenForms[0]).Zeig();
                    });
                //    break;

                //case UpdatePolicy.AutoUpdate:
                //    Application.Run(new WizardUpdate(e));
                //InstallUpdates(e, null, null);
                //Notification.ShowNotificationAction(
                //    string.Format(Resources.Interface.UpdateInstalled, e.Product),
                //    Resources.Interface.UpdateInstalledInstruction,
                //    () =>
                //    {
                //        ((ReleaseNotes)Application.OpenForms[0]).Zeig();
                //    });
                //Application.Run(new ReleaseNotes(e));
                //        break;
                //}
            }
        }

        public static void InstallUpdates(UpdateArgs updates, Label label, BackgroundWorker bw)
        {
            List<Uri> downloadLinks = new List<Uri>(updates.GetDownloadLinks());
            List<string> tempLinks = new List<string>();
            int progress = 0;

            while (downloadLinks.Count > 0)
            {
                Uri uri = downloadLinks[0];

                string feature = Path.GetFileName(uri.LocalPath);
                if (label != null) Service.UpdateStatus(label, Resources.Interface.UpdateDownload, feature);

                // Downloading basic feature
                string tempFileName = ServerSoftware.DownloadFile(uri);

                tempLinks.Add(tempFileName);

                // Downloading localization
                if (ServerSoftware.AvailableCultures.Contains(CultureInfo.CurrentCulture))
                {
                    try
                    {
                        CultureInfo ci = CultureInfo.CurrentCulture;
                        Uri loc = downloadLinks[0].Localize(ci);

                        if (label != null) Service.UpdateStatus(label, Resources.Interface.UpdateDownloadLoc, feature, ci.Name);

                        if (!Directory.Exists(Path.Combine(IO.TempFolder, ci.Name)))
                        {
                            Directory.CreateDirectory(Path.Combine(IO.TempFolder, ci.Name));
                        }
                        tempFileName = ServerSoftware.DownloadFile(loc);
                    }
                    catch { }
                }

                downloadLinks.RemoveAt(0);

                // Signal
                if (bw != null)
                {
                    progress++;
                    bw.ReportProgress(progress);
                }
            }

            // Unpack dats
            foreach (string tempFile in tempLinks)
            {
                FileInfo fi = new FileInfo(tempFile);
                if (label != null) Service.UpdateStatus(label, Resources.Interface.UpdateInstalling, fi.Name);
                IO.UnpackFiles(tempFile, updates.ProductFolder);

                if (ServerSoftware.AvailableCultures.Contains(CultureInfo.CurrentCulture))
                {
                    CultureInfo ci = CultureInfo.CurrentCulture;
                    fi = new FileInfo(tempFile.Insert(tempFile.LastIndexOf('\\'), '\\' + ci.Name));
                    if (fi.Exists)
                    {
                        IO.UnpackFiles(fi.FullName, updates.ProductFolder);
                    }
                }

                if (bw != null)
                {
                    progress++;
                    bw.ReportProgress(progress);
                }
            }

            IO.ClearTemp();
        }
    }
}
