using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Mayfly.Software
{
    public class License
    {
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
            HardwareID = licvalues[4];
        }

        internal void Install()
        {
            UserSetting.SetValue(
                UserSettingPaths.KeyLicenses,
                this.Feature,
                "License",
                StringCipher.Encrypt(this.ToString(), this.Feature)
                       );

            Licensing.installedLicenses = null;
        }

        internal void Uninstall()
        {
            UserSetting.ClearFolder(UserSettingPaths.KeyLicenses, this.Feature);
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4}", Licensee, Feature, Expiration, Autorenewal, HardwareID);
        }
    }
}
