using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;

namespace Mayfly.Software.Management
{
    public abstract class Service
    {
        public static string FtpServer = "ftp://mayfly.ru/get/updates";

        public static Uri SchemeFtpUri { get { return new Uri(FtpServer + "/scheme.xml"); } }

        public static string PackFiles(string[] files)
        {
            string result = FileSystem.GetTempFileName(".zip");
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
                Server.EnsurePathToLoad(destination);

                Uri targeturi = source.MakeRelativeUri(destination);

                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(source);
                ftp.Credentials = Server.GetAppCredentials();
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = Uri.UnescapeDataString(targeturi.OriginalString);
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            }
            catch
            {

            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string[] GetFilenames(Uri uri)
        {
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = Server.GetAppCredentials();
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

        public static string GetFriendlyString(string sn, int groupSize, char separator)
        {
            string result = string.Empty;

            int counter = 0;
            for (int i = 0; i < sn.Length; i++ )
            {
                result += sn[i];
                counter++;
                if (i < sn.Length - 1 && counter == groupSize)
                {
                    result += separator;
                    counter = 0;
                }
            }

            return result.ToUpperInvariant();
        }

        public static void CombineOldSchemes()
        {
            Scheme en = new Scheme(); // Software.Service.GetScheme(Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile));
            Scheme ru = new Scheme(); // Software.Service.GetScheme(Server.GetUri(Server.ServerHttps, Software.Service.SchemeFile, new CultureInfo("ru")));

            en.ReadXml("scheme//en.xml");
            ru.ReadXml("scheme//ru.xml");

            foreach (Scheme.FileRow fileRow in en.File)
            {
                if (fileRow.IsShortcutTipNull()) continue;
                if (ru.File.FindByID(fileRow.ID) == null) continue;
                if (ru.File.FindByID(fileRow.ID).IsShortcutTipNull()) continue;
                fileRow.ShortcutTip += Environment.NewLine + "ru: " + ru.File.FindByID(fileRow.ID).ShortcutTip;
            }

            foreach (Scheme.FileTypeRow filetypeRow in en.FileType)
            {
                if (filetypeRow.IsFriendlyNameNull()) continue;
                if (ru.FileType.FindByExtension(filetypeRow.Extension) == null) continue;
                if (ru.FileType.FindByExtension(filetypeRow.Extension).IsFriendlyNameNull()) continue;
                filetypeRow.FriendlyName += Environment.NewLine + "ru: " + ru.FileType.FindByExtension(filetypeRow.Extension).FriendlyName;
            }

            foreach (Scheme.VersionRow versionRow in en.Version)
            {
                if (versionRow.IsChangesNull()) continue;
                if (ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version) == null) continue;
                if (ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version).IsChangesNull()) continue;
                versionRow.Changes += Environment.NewLine + "ru: " + ru.Version.FindByFileIDVersion(versionRow.FileID, versionRow.Version).Changes;
            }

            en.WriteXml(Path.Combine(Application.StartupPath, "scheme", "combined.xml"));
        }
    }
}
