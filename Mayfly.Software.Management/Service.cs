using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace Mayfly.Software.Management
{
    public abstract class Service
    {
        public static UpdateServer ProductSchemeServer;

        public static string FtpUpdatesServer = "ftps://" + Server.Domain + "/get/updates";

        public static string PackFiles(string[] files)
        {
            string result = IO.GetTempFileName(".zip");
            ZipArchive zip = ZipFile.Open(result, ZipArchiveMode.Create);
            foreach (string file in files)
            {
                zip.CreateEntryFromFile(file, file);
            }
            zip.Dispose();

            FileStream stream = File.OpenRead(result);

            try
            {
                ZipArchive zipArchive = new ZipArchive(stream);

                foreach (ZipArchiveEntry entry in zipArchive.Entries)
                {
                    long l = entry.CompressedLength;
                }
            }
            catch
            {
                throw new FileLoadException(string.Format("File {0} is not packed correctly.", result));
            }

            stream.Close();

            return result;
        }

        public static void Move(Uri source, Uri destination)
        {
            try
            {
                EnsurePathToLoad(destination);

                Uri targeturi = source.MakeRelativeUri(destination);

                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(source);
                ftp.Credentials = GetAppCredentials();
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = Uri.UnescapeDataString(targeturi.OriginalString);
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            }
            catch
            {

            }
        }

        public static string[] GetFilenames(Uri uri)
        {
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = GetAppCredentials();
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            using (FtpWebResponse respone = (FtpWebResponse)request.GetResponse())
            using (Stream stream = respone.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        //public static string RandomString(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    var random = new Random();
        //    return new string(Enumerable.Repeat(chars, length)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //}

        //public static string GetFriendlyString(string sn, int groupSize, char separator)
        //{
        //    string result = string.Empty;

        //    int counter = 0;
        //    for (int i = 0; i < sn.Length; i++ )
        //    {
        //        result += sn[i];
        //        counter++;
        //        if (i < sn.Length - 1 && counter == groupSize)
        //        {
        //            result += separator;
        //            counter = 0;
        //        }
        //    }

        //    return result.ToUpperInvariant();
        //}

        //public static void CombineOldSchemes()
        //{
        //    Scheme en = new Scheme(); // Software.Service.GetScheme(Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile));
        //    Scheme ru = new Scheme(); // Software.Service.GetScheme(Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile, new CultureInfo("ru")));

        //    en.ReadXml("scheme//en.xml");
        //    ru.ReadXml("scheme//ru.xml");

        //    foreach (Scheme.FileRow fileRow in en.File)
        //    {
        //        if (fileRow.IsShortcutTipNull()) continue;
        //        if (ru.File.FindByID(fileRow.ID) == null) continue;
        //        if (ru.File.FindByID(fileRow.ID).IsShortcutTipNull()) continue;
        //        fileRow.ShortcutTip += Environment.NewLine + "ru: " + ru.File.FindByID(fileRow.ID).ShortcutTip;
        //    }

        //    foreach (Scheme.FileTypeRow filetypeRow in en.FileType)
        //    {
        //        if (filetypeRow.IsFriendlyNameNull()) continue;
        //        if (ru.FileType.FindByExtension(filetypeRow.Extension) == null) continue;
        //        if (ru.FileType.FindByExtension(filetypeRow.Extension).IsFriendlyNameNull()) continue;
        //        filetypeRow.FriendlyName += Environment.NewLine + "ru: " + ru.FileType.FindByExtension(filetypeRow.Extension).FriendlyName;
        //    }

        //    foreach (Scheme.VersionRow versionRow in en.Version)
        //    {
        //        if (versionRow.IsChangesNull()) continue;
        //        if (ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version) == null) continue;
        //        if (ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version).IsChangesNull()) continue;
        //        versionRow.Changes += Environment.NewLine + "ru: " + ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version).Changes;
        //    }

        //    en.WriteXml(Path.Combine(Application.StartupPath, "scheme", "combined.xml"));
        //}

        public static NetworkCredential GetAppCredentials()
        {
            return new NetworkCredential("u0851741_mayfly", "_Ysel266");
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

        public static Commit[] GetCommits(string pathRepository, string pathProject)
        {
            Repository repo = new Repository(pathRepository);
            List<Commit> result = new List<Commit>();

            foreach (var commit in repo.Commits)
            {
                foreach (var t in commit.Tree)
                {
                    if (t.Path.Contains(pathProject) && !result.Contains(commit))
                    {
                        result.Add(commit);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
