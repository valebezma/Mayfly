using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;

namespace Mayfly.Software
{
    public abstract class ServerSoftware
    {
        readonly static NetworkCredential credentials = new NetworkCredential("mayfly-software", "UBY-3b4-Etj-GeP");

        public static MySqlConnection Connection;

        public readonly static string[] TablesOrder = new string[] { "Product", "File", "Feature", "Satellite", "Filetype", "Version" };


        public static string DownloadFile(Uri uri)
        {
            return DownloadFile(uri, Path.Combine(IO.TempFolder, Path.GetFileName(uri.LocalPath)));
        }

        public static string DownloadFile(Uri uri, string filename)
        {
            if (UserSettings.UseUnsafeConnection) Server.SetUnsafeCertifications();
            WebClient webClient = new WebClient();
            webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

            try
            {
                webClient.DownloadFile(uri, filename);
                return filename;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        public static void Connect(NetworkCredential creds)
        {
            string connString = string.Format(
                @"server = {0}; port = 3307; uid = {1}; pwd = {2}; database = {3}; SSL Mode = None;",
                Server.Domain, creds.UserName, creds.Password, "mayfly-products");
            Connection = new MySqlConnection(connString);
        }

        public static Scheme GetScheme(NetworkCredential creds, params string[] tableNames)
        {
            if (Connection == null) Connect(creds);

            Scheme result = new Scheme();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            foreach (string tableName in tableNames)
            {
                adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + tableName.ToLowerInvariant(), Connection);
                adapter.Fill(result, tableName);
            }

            return result;
        }

        public static Scheme GetScheme(params string[] tableNames)
        {
            return GetScheme(credentials, tableNames);
        }

        public static Scheme GetScheme(NetworkCredential creds)
        {
            return GetScheme(creds, TablesOrder);
        }

        public static Scheme GetScheme()
        {
            return GetScheme(credentials, TablesOrder);
        }

        public static List<CultureInfo> AvailableCultures
        {
            get
            {
                List<CultureInfo> result = new List<CultureInfo>();
                result.Add(new CultureInfo("ru"));
                return result;
            }
        }

        /// <summary>
        /// In Short: the Method solves the Problem of broken Certificates.
        /// Sometime when requesting Data and the sending Webserverconnection
        /// is based on a SSL Connection, an Error is caused by Servers whoes
        /// Certificate(s) have Errors. Like when the Cert is out of date
        /// and much more... So at this point when calling the method,
        /// this behaviour is prevented
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns>true</returns>
        private static bool AcceptAllCertifications(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
