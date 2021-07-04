using Mayfly.Software;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly
{
    public abstract class Server
    {
        public static string ServerHttps = "https://mayfly.ru";

        public static string ServerLog = "ftp://mayfly.ru/log/" + UserSettings.Product + "/" + UserSettings.Username + "/" + Environment.MachineName;


        public static NetworkCredential GetAppCredentials()
        {
            return new NetworkCredential("u0851741_mayfly", "_Ysel266");
        }

        public static Uri GetUri(string server, string filename)
        {
            return new Uri(server + "/" + filename);
        }

        public static Uri GetUri(string server, string filename, CultureInfo ci)
        {
            Uri uri = GetUri(server, filename);
            Uri localizedUri = GetUri(server + "/" + ci.Name.ToLowerInvariant(), filename);
            return ci.Equals(CultureInfo.InvariantCulture) ? uri :
                FileExists(localizedUri) ? localizedUri : uri;
        }

        public static Uri GetLocalizedUri(Uri original, CultureInfo ci)
        {
            Uri loc = new Uri(original.OriginalString.Insert(
                        original.OriginalString.LastIndexOf('/'), '/' + ci.Name.ToLowerInvariant())
                        );
            return loc;
        }

        public static bool FileExists(Uri uri)
        {
            bool result = true;

            WebRequest webRequest = WebRequest.Create(uri);
            //webRequest.Credentials = GetAppCredentials();
            webRequest.Timeout = 1200;
            //webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch (WebException)
            {
                webRequest.Abort();
                result = false;
            }

            return result;
        }

        public static void UploadFile(string localpath, Uri ftppath)
        {
            UploadFile(File.ReadAllBytes(localpath), ftppath);
        }

        public static void UploadFile(byte[] content, Uri ftppath)
        {
            try
            {
                FtpWebResponse response = null;

                EnsurePathToLoad(ftppath);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftppath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = GetAppCredentials();
                request.ContentLength = content.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(content, 0, content.Length);
                stream.Close();
                response = (FtpWebResponse)request.GetResponse();

                Log.Write(EventType.Maintenance, "File {0} sent with response {1}", Path.GetFileName(ftppath.LocalPath), response.StatusDescription.Trim());
            }
            catch (WebException e)
            {
                Log.Write(EventType.ExceptionThrown, ((FtpWebResponse)e.Response).StatusDescription.Trim());
            }
        }

        public static void EnsurePathToLoad(Uri ftppath)
        {
            try
            {
                FtpWebResponse response = null;

                Uri curruri = new Uri("ftp://" + ftppath.Host + "/");

                for (int i = 1; i < ftppath.Segments.Length - 1; i++)
                {
                    try
                    {
                        curruri = new Uri(curruri.OriginalString + ftppath.Segments[i]);
                        FtpWebRequest requestf = (FtpWebRequest)WebRequest.Create(curruri);
                        requestf.Method = WebRequestMethods.Ftp.MakeDirectory;
                        requestf.Credentials = GetAppCredentials();
                        response = (FtpWebResponse)requestf.GetResponse();
                    }
                    catch //(WebException e)
                    {
                        //Log.Write(EventType.ExceptionThrown, "Path not created: {0}", ((FtpWebResponse)e.Response).StatusDescription.Trim());
                    }
                }

                Log.Write(EventType.Maintenance, "Path [{0}] successfully created", ftppath.OriginalString);
            }
            catch (WebException e)
            {
                Log.Write(EventType.ExceptionThrown, ((FtpWebResponse)e.Response).StatusDescription.Trim());
            }
        }

        public static void UploadFileAsinc(string localpath, Uri ftppath)
        {
            Task.Run(() => UploadFile(localpath, ftppath));
        }

        public static void UploadFileAsinc(byte[] content, Uri ftppath)
        {
            Task.Run(() => UploadFile(content, ftppath));
        }



        //public static Development GetScheme()
        //{
        //    return GetScheme(GetUri(ServerHttps, SchemeFile));
        //}

        //public static Development GetScheme(CultureInfo ci)
        //{
        //    return GetScheme(GetUri(ServerHttps, SchemeFile, ci));
        //}

        //public static Development GetScheme(Uri uri)
        //{
        //    WebRequest request = WebRequest.Create(uri);
        //    request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

        //    try
        //    {
        //        using (WebResponse response = request.GetResponse())
        //        {
        //            Stream stream = response.GetResponseStream();

        //            if (stream == null) return null;

        //            Development result = new Development();
        //            result.ReadXml(stream);
        //            return result;
        //        }
        //    }
        //    catch// (Exception e)
        //    {
        //        return null;
        //    }
        //}



        public static void SendEmail(string mailto, string subject, string body)
        {
            SendEmail(mailto, subject, body, null);
        }

        public static void SendEmail(string mailto, string subject, string body, string attachment)
        {
            string cmd = string.Format("mailto:{0}?subject={1}&body={2}", mailto, subject, body.Replace(Environment.NewLine, "%0A"));

            if (attachment != null)
            {
                cmd += string.Format("&attachment={0}", attachment);
            }
            FileSystem.RunFile(cmd);

        }



        public static void CheckUpdates(string product)
        {
            FileSystem.RunFile(Path.Combine(FileSystem.ProgramFolder, "mayflysoftware.exe"), "-checkup", product);
        }

        public static void DoUpdates(string product)
        {
            FileSystem.RunFile(Path.Combine(FileSystem.ProgramFolder, "mayflysoftware.exe"), "-update", product);
        }

        public static UpdatePolicy GetUpdatePolicy(string product)
        {
            return (UpdatePolicy)Convert.ToInt32(UserSetting.GetValue(UserSettingPaths.KeyFeatures, product, UserSettingPaths.UpdatePolicy));
        }

        public static void SetUpdatePolicy(string product, UpdatePolicy policy)
        {
            UserSetting.SetValue(UserSettingPaths.KeyFeatures, product, UserSettingPaths.UpdatePolicy, (int)policy);
            CheckUpdates(product);
        }

        //public static string UpdaterPath
        //{
        //    get
        //    {
        //        return Path.Combine(new DirectoryInfo(Application.StartupPath).Parent.FullName,
        //            "mayflyinstall.exe");
        //    }
        //}

        //public static void PerformUpdates()
        //{
        //    PerformUpdates(UserSettings.Product);
        //}

        //public static void PerformUpdates(string product)
        //{
        //    if (GetUpdatePolicy(product) == UpdatePolicy.Off)
        //        return;

        //    UpdateInspector updateInspector = new UpdateInspector(product);
        //    updateInspector.CheckedEvent += updateInspector_CheckedEvent;
        //    updateInspector.Run();
        //}

        //private static void updateInspector_CheckedEvent(UpdateEventArgs e)
        //{
        //    if (e == null || !e.IsUpdateAvailable)
        //        return;

        //    switch (GetUpdatePolicy(e.Product))
        //    {
        //        case UpdatePolicy.CheckAndNotice:
        //            Notification.ShowNotificationAction(
        //                string.Format(Resources.Interface.UpdateAvailability, e.Product),
        //                Resources.Interface.UpdateInstruction,
        //                () => { FileSystem.RunFile(UpdaterPath, "\"-update\" \"" + e.Product + "\""); });
        //            break;

        //        case UpdatePolicy.AutoUpdate:
        //            FileSystem.RunFile(UpdaterPath, "\"-silent\" \"" + e.Product + "\"");
        //            //Application.Exit();

        //            break;
        //    }
        //}
    }

    public enum UpdatePolicy
    {
        Off,
        CheckAndNotice,
        AutoUpdate
    }
}
