﻿using Mayfly.Software;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Mayfly
{
    public static class Licensing
    {
        public static bool Verify(string feature)
        {
            return true;

            //foreach (License lic in InstalledLicenses)
            //{
            //    if (lic.Feature != feature) continue;
            //    if (lic.IsValid) return true;
            //}

            //return false;
        }

        internal static List<License> installedLicenses;

        public static License[] InstalledLicenses
        {
            get
            {
                if (installedLicenses == null)
                {
                    installedLicenses = new List<License>();

                    RegistryKey reg = Registry.CurrentUser.OpenSubKey(UserSettingPaths.KeyLicenses);

                    if (reg == null) return installedLicenses.ToArray();

                    foreach (string value in reg.GetSubKeyNames())
                    {
                        try
                        {
                            RegistryKey regLic = reg.OpenSubKey(value);
                            string feature = value;
                            License lic = new License(StringCipher.Decrypt((string)regLic.GetValue("License"), feature));
                            installedLicenses.Add(lic);
                        }
                        catch { continue; }
                    }
                }

                return installedLicenses.ToArray();
            }
        }

        internal static void InspectLicenses()
        {
            bool shouldUpdate = false;

            foreach (License lic in InstalledLicenses)
            {
                shouldUpdate = lic.ExpiresIn.TotalDays < (lic.Autorenewal ? 0 : 8);
                if (shouldUpdate) break;
            }

            if (!shouldUpdate) shouldUpdate |= (DateTime.Now - UserSettings.LastLicenseCheckup).TotalHours > 12;

            if (shouldUpdate)
            {
                InstallLicenses();

                foreach (License lic in InstalledLicenses)
                {
                    double u = lic.ExpiresIn.TotalDays;

                    if (u < (lic.Autorenewal ? -1 : 0))
                    {
                        lic.Uninstall();
                        Notification.ShowNotification(
                            Resources.License.LicenseExpired, string.Format(Resources.License.LicenseExpiredInstruction, lic.Feature));
                    }
                    else if (!lic.Autorenewal && u < 8)
                    {
                        Notification.ShowNotification(
                            Resources.License.LicenseExpiresSoon, string.Format(
                                Resources.License.LicenseExpiresSoonInstruction,
                                lic.Feature, lic.ExpiresIn.TotalDays < 1 ?
                                Resources.License.LicenseExpiresSoonInstructionToday :
                                string.Format(Resources.License.LicenseExpiresSoonInstructionIn, (int)lic.ExpiresIn.TotalDays)
                                )
                            );
                    }
                }
            }
        }

        internal static void InstallLicenses()
        {
            InstallLicenses(GetLicenses(UserSettings.Credential));
        }

        internal static void InstallLicenses(License[] lics)
        {
            if (lics != null)
            {
                foreach (License lic in lics)
                {
                    lic.Install();
                }
            }
        }

        internal static License[] GetLicenses(NetworkCredential credentials)
        {
            if (credentials == null)
            {
                return null;
            }

            Uri uri = Server.GetUri(Server.ServerHttps, "php/software/get_license.php");
            Dictionary<string, string> licenseRequestParameters = new Dictionary<string, string>();
            licenseRequestParameters.Add("email", credentials.UserName);
            licenseRequestParameters.Add("password", credentials.Password);
            licenseRequestParameters.Add("hid", Hardware.HardwareID);
            licenseRequestParameters.Add("hname", Environment.MachineName);
            licenseRequestParameters.Add("uninstall", "0");
            string[] response = Server.GetText(uri, licenseRequestParameters);

            UserSettings.LastLicenseCheckup = DateTime.Now;

            if (response == null)
            {
                return null;
            }

            List<License> result = new List<License>();

            foreach (string lic in response)
            {
                result.Add(new License(lic));
            }

            Log.Write("Licenses successfully received");

            return result.ToArray();
        }

        internal static void SendUninstall(NetworkCredential credentials)
        {
            if (UserSettings.Credential == null) return;

            Uri uri = Server.GetUri(Server.ServerHttps, "php/customer/getlicense.php");
            Dictionary<string, string> licenseRequestParameters = new Dictionary<string, string>();
            licenseRequestParameters.Add("email", credentials.UserName);
            licenseRequestParameters.Add("password", credentials.Password);
            licenseRequestParameters.Add("hid", Hardware.HardwareID);
            licenseRequestParameters.Add("uninstall", "1");
            string[] response = Server.GetText(uri, licenseRequestParameters);

            //if (response == null) return;

            //if (response.Length == 0) return;

            //return response.Length > 0 && response[0].Length > 1 && response[0].Substring(0, 2) == "OK";
        }



        public static void SetMenuAvailability(bool available, params ToolStripItem[] items)
        {
            foreach (ToolStripItem item in items)
            {
                item.Visible = available;
            }
        }

        public static void SetMenuAvailability(string feature, params ToolStripItem[] items)
        {
            SetMenuAvailability(Verify(feature), items);
        }

        public static void SetControlsAvailability(bool available, params Control[] controls)
        {
            //foreach (Control control in controls)
            //{
            //    control.Visible = available;
            //}
        }

        public static void SetControlsAvailability(string feature, params Control[] controls)
        {
            //SetControlsAvailability(Verify(feature), controls);
        }

        public static void SetTabsAvailability(bool available, params TabPage[] tabs)
        {
            foreach (TabPage tab in tabs)
            {
                if (!available) tab.Parent = null;
            }
        }

        public static void SetTabsAvailability(string feature, params TabPage[] tabs)
        {
            SetTabsAvailability(Verify(feature), tabs);
        }

        public static void SetMenuClickability(bool available, params ToolStripItem[] items)
        {
            //foreach (ToolStripItem item in items)
            //{
            //    item.Enabled = available;
            //}
        }

        public static void SetControlsClickability(bool available, Control.ControlCollection ctrls)
        {
            foreach (Control c in ctrls)
            {
                if (c is Label) continue;

                //if (c is Label) continue; // TextBox || c is MaskedTextBox || c is DataGridView || c is DateTimePicker || c is ComboBox)
                if (c is DataGridView)
                {
                    foreach (DataGridViewRow r in ((DataGridView)c).Rows)
                    {
                        r.ReadOnly = !available;
                    }
                }
                else if (c is TextBox)
                {
                    ((TextBox)c).ReadOnly = !available;
                }
                //else if (c is DateTimePicker)
                //{
                //    ((DateTimePicker)c).ReadOnly = !available;
                //}
                //else if (c is ComboBox)
                //{
                //    ((ComboBox)c).ReadOnly = !available;
                //}
                else if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).ReadOnly = !available;
                }
                else if (c is Panel)
                {
                    SetControlsClickability(available, ((Panel)c).Controls);
                }
                else if (c is Geographics.WaypointControl)
                {
                    ((Geographics.WaypointControl)c).ReadOnly = !available;
                }
                else
                {
                    c.Enabled = available;
                }
            }
        }

        public static void SetControlsClickability(bool available, params TabPage[] tabs)
        {
            foreach (TabPage tab in tabs)
            {
                SetControlsClickability(available, tab.Controls);
            }
        }
    }
}
