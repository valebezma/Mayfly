using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Mayfly.Extensions;

namespace Mayfly
{
    public abstract class Server
    {
        public static Uri GetUri(Uri server, string localPath)
        {
            return new Uri(server, localPath);
        }

        public static Uri GetUri(Uri server, string localPath, CultureInfo ci)
        {
            Uri uri = GetUri(server, localPath);
            Uri localizedUri = uri.Localize(ci);
            return ci.Equals(CultureInfo.InvariantCulture) ? uri :
                FileExists(localizedUri) ? localizedUri : uri;
        }



        public readonly static string Domain = "mayfly.ru";

        public readonly static Uri MainUri = new Uri("https://" + Domain);

        public static Uri GetUri(string localPath)
        {
            return GetUri(MainUri, localPath);
        }

        public static Uri GetUri(string localPath, CultureInfo ci)
        {
            return GetUri(MainUri, localPath, ci);
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

        public static void SetUnsafeCertifications()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            {
                return true;
            };
        }


        public static string[] GetText(Uri uri, Dictionary<string, string> values)
        {
            if (UserSettings.UseUnsafeConnection) SetUnsafeCertifications();
            WebRequest webRequest = WebRequest.CreateHttp(uri);
            webRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            List<string> result = new List<string>();

            try
            {
                if (values.Count > 0)
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
                }

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine();
                    result.Add(s);

                    if (string.IsNullOrWhiteSpace(s)) continue;
                    Console.WriteLine(s);
                }

            }
            catch
            {
                return null; 
            }

            return result.ToArray();
        }

        public static string[] GetText(Uri uri)
        {
            return GetText(uri, new Dictionary<string, string>() { });
        }
    }

    public enum UpdatePolicy
    {
        Off,
        CheckAndNotice,
        CheckAndRun
    }
}
