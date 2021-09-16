using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;

namespace Mayfly
{
    public class License
    {
        static readonly string keyLicenses = @"Software\Mayfly\Licenses";

        public string Licensee { get; set; }

        public string Feature { get; set; }

        public DateTime Expiration { get; private set; }

        public bool Autorenewal { get; set; }

        public string HardwareID { get; set; }

        public TimeSpan ExpiresIn { get { return Expiration - DateTime.Today; } }

        public bool IsValid
        {
            get
            {
                return HardwareID == Hardware.HardwareID && 
                    ExpiresIn.TotalDays > (Autorenewal ? -1 : 0);
            }
        }



        public License(string licstring)
        {
            string[] licvalues = licstring.Split(';');

            Licensee = licvalues[0];
            Feature = licvalues[1];
            Expiration = DateTime.Parse(licvalues[2]);
            Autorenewal = bool.Parse(licvalues[3]);
            if (licvalues.Length > 4) HardwareID = licvalues[4];
        }

        internal void Install()
        {
            if (HardwareID == null)
            {
                HardwareID = Hardware.HardwareID;
            }
            else if (HardwareID != Hardware.HardwareID)
            {
                return;
            }

            UserSetting.SetValue(
                keyLicenses,
                this.Feature,
                "License",
                StringCipher.Encrypt(this.ToString(), this.Feature)
                       );

            installedLicenses = null;
        }

        internal void Uninstall()
        {
            UserSetting.ClearFolder(keyLicenses, this.Feature);
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4}", Licensee, Feature, Expiration, Autorenewal, HardwareID);
        }


        internal static List<License> installedLicenses;

        public static License[] InstalledLicenses
        {
            get
            {
                if (installedLicenses == null)
                {
                    installedLicenses = new List<License>();

                    RegistryKey reg = Registry.CurrentUser.OpenSubKey(keyLicenses);

                    if (reg == null) return installedLicenses.ToArray();

                    foreach (string value in reg.GetSubKeyNames())
                    {
                        try
                        {
                            RegistryKey regLic = reg.OpenSubKey(value);
                            string feature = value;
                            License lic = new License(StringCipher.Decrypt((string)regLic.GetValue("License"), feature));
                            if (!lic.IsValid) continue;
                            installedLicenses.Add(lic);
                        }
                        catch { continue; }
                    }
                }

                return installedLicenses.ToArray();
            }
        }



        internal static void InspectLicensesExpiration()
        {
            bool shouldUpdate = false;

            foreach (License lic in InstalledLicenses)
            {
                shouldUpdate = lic.ExpiresIn.TotalDays < (lic.Autorenewal ? 0 : 8);
                if (shouldUpdate) break;
            }

            if (!shouldUpdate) shouldUpdate |= (DateTime.Now - LastLicenseCheckup).TotalHours > 12;

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

            Uri uri = Server.GetUri("php/software/get_license.php");
            Dictionary<string, string> licenseRequestParameters = new Dictionary<string, string>();
            licenseRequestParameters.Add("email", credentials.UserName);
            licenseRequestParameters.Add("password", credentials.Password);
            licenseRequestParameters.Add("hid", Hardware.HardwareID);
            licenseRequestParameters.Add("hname", Environment.MachineName);
            licenseRequestParameters.Add("uninstall", "0");
            string[] response = Server.GetText(uri, licenseRequestParameters);

            LastLicenseCheckup = DateTime.Now;

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

            Uri uri = Server.GetUri("php/customer/getlicense.php");
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



        public static FeatureLevel AllowedFeaturesLevel
        {
            get
            {
                if (Verify("Insider")) return FeatureLevel.Insider;
                if (Verify("Advanced")) return FeatureLevel.Advanced;
                return FeatureLevel.Basic;
            }
        }

        public static bool Verify(string feature)
        {
            foreach (License lic in InstalledLicenses)
            {
                if (lic.Feature != feature) continue;
                if (lic.IsValid) return true;
            }

            return false;
        }

        public static DateTime LastLicenseCheckup
        {
            get { return Convert.ToDateTime(UserSetting.GetValue(keyLicenses, nameof(LastLicenseCheckup), DateTime.Today.AddDays(-2))); }
            set { UserSetting.SetValue(keyLicenses, nameof(LastLicenseCheckup), value.ToString("s")); }
        }
    }

    public enum FeatureLevel
    {
        Basic = 0,
        Advanced = 1,
        Insider = 2
    }
}
