using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Mayfly
{
    public abstract class Server
    {
        public static string Domain = "mayfly.software";

        public static string ServerHttps = "http://" + Domain;


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
            webRequest.Timeout = 1200;

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


        public static string GetEmail(string box)
        {
            return box + "@" + Domain;
        }

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
            IO.RunFile(cmd);

        }


        public static void CheckUpdates(string product)
        {
            IO.RunFile(Path.Combine(IO.ProgramFolder, "mayflysoftware.exe"), "-checkup", product);
        }

        public static void DoUpdates(string product)
        {
            IO.RunFile(Path.Combine(IO.ProgramFolder, "mayflysoftware.exe"), "-update", product);
        }

        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public static string[] GetText(Uri uri)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            if (UserSettings.UseUnsafeConnection) { 
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            }

            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            List<string> result = new List<string>();

            try
            {
                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    result.Add(reader.ReadLine());
                }
            }
            catch { return null; }

            return result.ToArray();
        }

        public static string[] GetText(Uri uri, Dictionary<string, string> values)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            if (UserSettings.UseUnsafeConnection)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            }
            

            WebRequest webRequest = WebRequest.CreateHttp(uri);
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            List<string> result = new List<string>();

            try
            {
                webRequest.Method = "POST";
                string sName = "";
                foreach (var value in values)
                {
                    sName += value.Key + "=" + value.Value + "&";
                }
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(sName);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    result.Add(reader.ReadLine());
                }
            }
            catch// (Exception ex)
            {
                return null; 
            }

            return result.ToArray();
        }
    }

    public enum UpdatePolicy
    {
        Off,
        CheckAndNotice,
        CheckAndRun
    }
}
