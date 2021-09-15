using Mayfly.Software;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using Mayfly.Geographics;
using System.Windows.Forms;
using System.Net;

namespace Mayfly
{
    public abstract class UserSettings
    {
        public static string Product
        {
            get
            {
                return Path.GetFileName(Application.StartupPath);
            }
        }


        public static string Username
        {
            get
            {
                try { return UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.User).ToString(); }
                catch { return Resources.Interface.UserUnknown; }
            }

            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.User, value); }
        }

        public static bool ShareDiagnostics
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.ShareDiagnostics)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.ShareDiagnostics, value.ToString()); }
        }

        public static string FormatCoordinate
        {
            get
            {
                try { return UserSetting.GetValue(UserSettingPaths.KeyUI, UserSettingPaths.FormatCoordinate).ToString(); }
                catch { return "d"; }
            }

            set { UserSetting.SetValue(UserSettingPaths.KeyUI, UserSettingPaths.FormatCoordinate, value); }
        }

        
        public static CultureInfo Language
        {
            get
            {
                string s = (string)UserSetting.GetValue(UserSettingPaths.KeyUI, UserSettingPaths.Language);
                if (string.IsNullOrEmpty(s)) return CultureInfo.InvariantCulture;
                return CultureInfo.GetCultureInfo(s);
            }

            set { UserSetting.SetValue(UserSettingPaths.KeyUI, UserSettingPaths.Language, value.ToString()); }
        }


        public static UpdatePolicy UpdatePolicy
        {
            get { return (UpdatePolicy)Convert.ToInt32(UserSetting.GetValue(UserSettingPaths.KeyFeatures, UserSettingPaths.UpdatePolicy)); }
            set
            {
                UserSetting.SetValue(UserSettingPaths.KeyFeatures, UserSettingPaths.UpdatePolicy, (int)value);
                Server.CheckUpdates(Product);
            }
        }

        public static bool UseUnsafeConnection
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.UseUnsafeConnection)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.UseUnsafeConnection, value.ToString()); }
        }

        public static NetworkCredential Credential
        {
            get
            {
                try
                {
                    object o = UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.Credential);
                    if (o == null) return null;
                    string value = o.ToString();
                    if (string.IsNullOrEmpty(value)) return null;
                    value = StringCipher.Decrypt(value, UserSettings.Username);
                    string[] values = value.Split(';');
                    return new NetworkCredential(values[0], values[1]);
                }
                catch { return null; }
            }

            set
            {
                if (value == null)
                {
                    UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.Credential, string.Empty);
                }
                else
                {
                    string cred = value.UserName + ";" + value.Password;
                    cred = StringCipher.Encrypt(cred, UserSettings.Username);
                    UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.Credential, cred);
                }
            }
        }

        public static DateTime LastLicenseCheckup
        {
            get { return Convert.ToDateTime(UserSetting.GetValue(UserSettingPaths.KeyLicenses, UserSettingPaths.LastLicenseCheckup)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyLicenses, UserSettingPaths.LastLicenseCheckup, value.ToString("s")); }
        }


        public static Notification globalNotify;

        public static Notification GlobalNotification
        {
            get
            {
                if (globalNotify == null)
                {
                    globalNotify = new Notification();
                }

                return globalNotify;
            }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string KeyGeneral = @"Software\Mayfly";
        public static string KeyUI = @"Software\Mayfly\UI";
        public static string KeyFeatures = @"Software\Mayfly\Features";
        public static string KeyLicenses = @"Software\Mayfly\Licenses";

        public static string User = "User";
        public static string Language = "Language";

        public static string ShareDiagnostics = "ShareDiagnostics";
        public static string UpdatePolicy = "UpdatePolicy";
        public static string LastLicenseCheckup = "LastLicenseCheckup";
        public static string UseUnsafeConnection = "UseUnsafeConnection";
        public static string Credential = "Credential";

        public static string FormatColumn = "FormatColumn";
        public static string CheckState = "CheckState";
        public static string FormatCoordinate = "FormatCoordinate";
    }
}
