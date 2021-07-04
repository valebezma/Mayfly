using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mayfly.TaskDialogs;
using System.Diagnostics;

namespace Mayfly.Software
{
    public abstract class Service
    {
        public static string ServerUpdate = Server.ServerHttps + "/get/updates/current";

        public static Stream GetFromServer(Uri uri)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            WebRequest request = WebRequest.Create(uri);
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            if (UserSettings.UseUnsafeConnection) { ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications); }

            try { return request.GetResponse().GetResponseStream(); }
            catch (Exception)  { return null; }
        }

        public static Scheme GetScheme()
        {
            Stream stream = GetFromServer(new Uri(Server.ServerHttps + "/get/updates/scheme.xml"));
            if (stream == null) return null;
            Scheme result = new Scheme();
            result.ReadXml(stream);
            return result;
        }

        public static List<CultureInfo> GetAvailableCultures()
        {
            List<CultureInfo> result = new List<CultureInfo>();

            try
            {
                Stream stream = GetFromServer(Server.GetUri(Server.ServerHttps + "/get/updates", "locals.dat"));

                StreamReader reader = new StreamReader(stream);
                string[] culturesList = reader.ReadToEnd().TrimEnd(Environment.NewLine.ToArray()).Split(Environment.NewLine.ToArray());

                foreach (string line in culturesList)
                {
                    try
                    {
                        CultureInfo ci = new CultureInfo(line);
                        result.Add(ci);
                    }
                    catch { }
                }
            }
            catch
            { }

            return result;
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
        private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }



        public static bool IsLast(string product)
        {
            RegistryKey featureKey = Registry.CurrentUser.OpenSubKey(@"Software\Mayfly\Features\");
            if (featureKey == null) return true;
            string[] products = featureKey.GetSubKeyNames();
            return (products.Length == 1 && products[0] == product);
        }


        public static bool IsRunning(string path)
        {
            if (Path.GetExtension(path) != ".exe") return false;
            string exe = Path.GetFileNameWithoutExtension(path);
            string fld = Path.GetDirectoryName(path);

            foreach(Process p in Process.GetProcessesByName(exe))
            {
                if (p.MainModule.FileName.ToLower() == path.ToLower()) return true;
            }

            return false;
        }


        private delegate bool CheckChecker(CheckBox checkBox);

        public static bool IsChecked(CheckBox checkBox)
        {
            if (checkBox.InvokeRequired)
            {
                CheckChecker checkChecker = new CheckChecker(IsChecked);
                return (bool)checkBox.Invoke(checkChecker, new object[] { checkBox });
            }
            else
            {
                return checkBox.Checked;// && checkBox.Visible;
            }
        }

        private delegate void StatusUpdater(Label labelStatus, string status);

        public static void UpdateStatus(Label labelStatus, string status)
        {
            if (labelStatus.InvokeRequired)
            {
                StatusUpdater statusUpdater = new StatusUpdater(UpdateStatus);
                labelStatus.Invoke(statusUpdater, labelStatus, status);
            }
            else
            {
                labelStatus.Text = status;
            }
        }

        public static void UpdateStatus(Label labelStatus, string status, params string[] values)
        {
            UpdateStatus(labelStatus, string.Format(status, values));
        }

        private delegate void StatusAppender(TextBox textStatus, string status);

        public static void AppendStatus(TextBox textStatus, string status)
        {
            if (textStatus.InvokeRequired)
            {
                StatusAppender statusUpdater = new StatusAppender(AppendStatus);
                textStatus.Invoke(statusUpdater, textStatus, status);
            }
            else
            {
                textStatus.AppendText(status + Environment.NewLine);
            }
        }

        public static void AppendStatus(TextBox textStatus, string status, params string[] values)
        {
            AppendStatus(textStatus, string.Format(status, values));
        }

        private delegate TaskDialogButton ButtonGetter(TaskDialog td);

        public static TaskDialogButton GetButton(TaskDialog td)
        {
            try
            {
                return td.ShowDialog();
            }
            catch
            {
                ButtonGetter buttonGetter = new ButtonGetter(GetButton);
                return buttonGetter.Invoke(td);
            }
        }

        //private delegate string TextGetter(TextBox textBox);

        //private string GetText(TextBox textBox)
        //{
        //    if (textBox.InvokeRequired)
        //    {
        //        TextGetter textGetter = new TextGetter(GetText);
        //        return (string)textBox.Invoke(textGetter, new object[] { textBox });
        //    }
        //    else
        //    {
        //        return textBox.Text;
        //    }

        //}
    }
}
