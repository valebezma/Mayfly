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
using System.Data;

namespace Mayfly.Software.Management
{
    public abstract class ServerFiles
    {
        public readonly static Uri FtpUri = new Uri("ftp://" + Server.Domain + "/get/updates");

        readonly static NetworkCredential creds = new NetworkCredential("mayfly-uploader", "A0+=Rw");



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
            EnsurePathToLoad(destination);

            Uri targeturi = source.MakeRelativeUri(destination);

            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(source);
            ftp.Credentials = creds;
            ftp.Method = WebRequestMethods.Ftp.Rename;
            ftp.RenameTo = Uri.UnescapeDataString(targeturi.OriginalString);
            ftp.GetResponse();
        }

        public static string[] GetFilenames(string localPath)
        {
            FtpWebRequest request;
            request = (FtpWebRequest)WebRequest.Create(Server.GetUri(FtpUri, localPath));
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = creds;
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
                request.Credentials = creds;
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
                        requestf.Credentials = creds;
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
    }
}
