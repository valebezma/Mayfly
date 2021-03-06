using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Mayfly.TaskDialogs;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Net.Security;

namespace Mayfly.Software
{
    public abstract class Service
    {
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



        public delegate void StatusUpdater(Label labelStatus, string status);

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
            foreach (Process p in Process.GetProcessesByName(exe))
            {
                if (p.MainModule.FileName.ToLower() == path.ToLower()) return true;
            }

            return false;
        }

        public static bool CheckInstalled(string c_name)
        {
            string displayName;

            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(c_name))
                    {
                        return true;
                    }
                }
                key.Close();
            }

            registryKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(c_name))
                    {
                        return true;
                    }
                }
                key.Close();
            }
            return false;
        }
    }
}
