using Mayfly.Software;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Mayfly
{
    public static class Licensing
    {
        public static bool VerifyAll(params string[] features)
        {
            bool result = true;
            for (int i = 0; i < features.Length; i++) {
                bool ver = Verify(features[i]);
                if (!ver) return false;
                result &= ver;
            }
            return result;
        }

        public static bool VerifyAny(params string[] features)
        {
            for (int i = 0; i < features.Length; i++) {
                if (Verify(features[i])) return true;
            }
            return false;
        }

        public static bool Verify(string feature)
        {
            foreach (License.UserLicenseRow licRow in InstalledLicenses.UserLicense.FindByFeature(feature))
            {
                if (licRow.IsValid) return true;
            }

            return false;
        }

        public static bool VerifyApp(string feature)
        {
            if (Verify(feature))
            {
                return true;
            }
            else
            {
                License.UserLicenseRow[] features = InstalledLicenses.UserLicense.FindByFeature(feature);
                FeatureAdd licadd = features.Length > 0 ? new FeatureAdd(features[0]) : new FeatureAdd();
                return licadd.ShowDialog() == DialogResult.OK;
            }
        }

        static License installedLicenses;

        public static License InstalledLicenses
        {
            get
            {
                if (installedLicenses == null)
                {
                    installedLicenses = new License();

                    RegistryKey reg = Registry.CurrentUser.OpenSubKey(UserSettingPaths.KeyLicenses);

                    if (reg == null) return installedLicenses;

                    foreach (string value in reg.GetValueNames())
                    {
                        try
                        {
                            string serial = StringCipher.Decrypt(value, UserSettings.Username);
                            string licxml = StringCipher.Decrypt((string)reg.GetValue(value), serial);
                            License lic = new License();
                            lic.ReadXml(new StringReader(licxml));
                            License.UserLicenseRow licRow = lic.UserLicense[0];
                            //lic.UserLicense.RemoveUserLicenseRow(licRow);
                            installedLicenses.UserLicense.AddUserLicenseRow(
                                licRow.Licensee, licRow.Feature, licRow.Expires, licRow.Serial);
                        }
                        catch { continue; }
                    }
                }

                return installedLicenses;
            }
        }


        private static void InstallLicense(string serial)
        {
            // Some logics which contacting activation server with serial and get request with 
            // treat terms: Who, What feature, When expires.

            License lic = new License();

            UserSetting.SetValue(UserSettingPaths.KeyLicenses,
                StringCipher.Encrypt(serial, UserSettings.Username),
                StringCipher.Encrypt(lic.GetXml(), serial));
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
            foreach (Control control in controls)
            {
                control.Visible = available;
            }
        }

        public static void SetControlsAvailability(string feature, params Control[] controls)
        {
            SetControlsAvailability(Verify(feature), controls);
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
            foreach (ToolStripItem item in items)
            {
                item.Enabled = available;
            }
        }

        //public static void SetControlsClickability(bool available, params Control[] ctrls)
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

//namespace Mayfly
//{
//    public enum LicenseStatus
//    {
//        Invalid,
//        Valid,
//        Expired,
//        Outlive
//    }
//}
