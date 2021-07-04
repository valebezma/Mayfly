using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Mayfly.Software
{
    public class License
    {
        public string Licensee;

        public string Feature;

        public DateTime Expiration 
        {
            get; private set;
        }

        public bool Autorenewal { get; set; }

        public TimeSpan ExpiresIn 
        {
            get
            {
                return Expiration - DateTime.Today;
            }
        }

        public bool IsValid 
        {
            get
            {
                return this.ExpiresIn.TotalDays > (Autorenewal ? -1 : 0);
            }
        }

        public License(string licstring)
        {
            string[] licvalues = licstring.Split(';');

            Licensee = licvalues[0];
            Feature = licvalues[1];
            Expiration = DateTime.Parse(licvalues[2]);
            Autorenewal = bool.Parse(licvalues[3]);
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
            return string.Format("{0};{1};{2};{3}", Licensee, Feature, Expiration, Autorenewal);
        }
    }
}
