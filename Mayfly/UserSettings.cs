using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Mayfly
{
    public abstract class UserSettings
    {
        static readonly string keyGeneral = @"Software\Mayfly";

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
                return UserSetting.GetValue(keyGeneral, nameof(Username), Resources.Interface.UserUnknown).ToString();
            }

            set { UserSetting.SetValue(keyGeneral, nameof(Username), value); }
        }

        public static bool ShareDiagnostics
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(keyGeneral, nameof(ShareDiagnostics), true)); }
            set { UserSetting.SetValue(keyGeneral, nameof(ShareDiagnostics), value.ToString()); }
        }

        public static UpdatePolicy UpdatePolicy
        {
            get { return (UpdatePolicy)Convert.ToInt32(UserSetting.GetValue(keyGeneral, nameof(UpdatePolicy), UpdatePolicy.CheckAndNotice)); }
            set
            {
                UserSetting.SetValue(keyGeneral, nameof(UpdatePolicy), (int)value);
                Server.CheckUpdates(Product);
            }
        }

        public static bool UseUnsafeConnection
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(keyGeneral, nameof(UseUnsafeConnection), true)); }
            set { UserSetting.SetValue(keyGeneral, nameof(UseUnsafeConnection), value.ToString()); }
        }

        static NetworkCredential credential;

        public static NetworkCredential Credential
        {
            get
            {
                if (credential == null)
                {
                    try
                    {
                        object o = UserSetting.GetValue(keyGeneral, nameof(Credential), null);
                        if (o == null) return null;
                        string value = o.ToString();
                        if (string.IsNullOrEmpty(value)) return null;
                        value = StringCipher.Decrypt(value, Username);
                        string[] values = value.Split(';');
                        credential = new NetworkCredential(values[0], values[1]);
                    }
                    catch { return null; }
                }

                return credential;
            }

            set
            {
                if (value == null)
                {
                    UserSetting.SetValue(keyGeneral, nameof(Credential), string.Empty);
                }
                else
                {
                    string cred = value.UserName + ";" + value.Password;
                    cred = StringCipher.Encrypt(cred, UserSettings.Username);
                    UserSetting.SetValue(keyGeneral, nameof(Credential), cred);
                }
            }
        }
    }
}
