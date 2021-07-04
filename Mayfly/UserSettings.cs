using Mayfly.Software;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using Mayfly.Geographics;
using System.Windows.Forms;

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

        public static bool Log
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.Log)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.Log, value.ToString()); }
        }

        public static int LogSpan
        {
            get { return Convert.ToInt32(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.LogSpan)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.LogSpan, value); }
        }

        public static bool LogSend
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.LogSend)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.LogSend, value.ToString()); }
        }

        public static DateTime LogSentLast
        {
            get { return Convert.ToDateTime(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.LogSentLast)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.LogSentLast, value.ToString("s")); }
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
            get  { return Server.GetUpdatePolicy(Product); }
            set { Server.SetUpdatePolicy(Product, value);  }
        }

        public static bool UseUnsafeConnection
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.UseUnsafeConnection)); }
            set { UserSetting.SetValue(UserSettingPaths.KeyGeneral, UserSettingPaths.UseUnsafeConnection, value.ToString()); }
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

        public static string Log = "Log";
        public static string LogSpan = "LogSpan";
        public static string LogSend = "LogSend";
        public static string LogSentLast = "LogSentLast";

        public static string UpdatePolicy = "UpdatePolicy";
        public static string UseUnsafeConnection = "UseUnsafeConnection";

        public static string FormatColumn = "FormatColumn";
        public static string FormatCoordinate = "FormatCoordinate";
    }
}
