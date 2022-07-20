using Mayfly.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Mayfly
{
    public static class UserSettings
    {
        public static string Feature;

        public static string FeatureKey {
            get {
                return GetFeatureKey(Feature);
            }
        }



        public static Image AppBanner;

        public static Image SupportLogo;

        public static string SupportText;

        public static void SetApp(Image banner, Image supportLogo, string supportText) {

            AppBanner = banner;
            SupportLogo = supportLogo;
            SupportText = supportText;
        }


        private static NetworkCredential credential;
        private static readonly string keyGeneral = @"Software\Mayfly";



        public static string Product {
            get {
                return Path.GetFileName(Application.StartupPath);
            }
        }

        public static string Username {
            get {
                return GetValue(keyGeneral, nameof(Username), Resources.Interface.UserUnknown).ToString();
            }

            set { SetValue(keyGeneral, nameof(Username), value); }
        }

        public static bool ShareDiagnostics {
            get { return Convert.ToBoolean(GetValue(keyGeneral, nameof(ShareDiagnostics), true)); }
            set { SetValue(keyGeneral, nameof(ShareDiagnostics), value.ToString()); }
        }

        public static UpdatePolicy UpdatePolicy {
            get { return (UpdatePolicy)Convert.ToInt32(GetValue(keyGeneral, nameof(UpdatePolicy), UpdatePolicy.CheckAndNotice)); }
            set {
                SetValue(keyGeneral, nameof(UpdatePolicy), (int)value);
                Server.CheckUpdates(Product);
            }
        }

        public static bool UseUnsafeConnection {
            get { return Convert.ToBoolean(GetValue(keyGeneral, nameof(UseUnsafeConnection), true)); }
            set { SetValue(keyGeneral, nameof(UseUnsafeConnection), value.ToString()); }
        }

        public static NetworkCredential Credential {
            get {
                if (credential == null) {
                    try {
                        object o = GetValue(keyGeneral, nameof(Credential), null);
                        if (o == null) return null;
                        string value = o.ToString();
                        if (string.IsNullOrEmpty(value)) return null;
                        value = StringCipher.Decrypt(value, Username);
                        string[] values = value.Split(';');
                        credential = new NetworkCredential(values[0], values[1]);
                    } catch { return null; }
                }

                return credential;
            }

            set {
                if (value == null) {
                    SetValue(keyGeneral, nameof(Credential), string.Empty);
                } else {
                    string cred = value.UserName + ";" + value.Password;
                    cred = StringCipher.Encrypt(cred, Username);
                    SetValue(keyGeneral, nameof(Credential), cred);
                }
            }
        }



        public static string GetFeatureKey(string feature) {
            return @"Software\Mayfly\Features\Mayfly." + feature;
        }

        public static object GetValue(string path, string key, object defaultValue) {
            RegistryKey subKey = Registry.CurrentUser.CreateSubKey(path);
            try { return subKey.GetValue(key, null) ?? defaultValue; } catch { return defaultValue; }
        }

        public static object GetValue(string path, string folder, string key, object defaultValue) {
            return GetValue(path, new string[] { folder }, key, defaultValue);
        }

        public static object GetValue(string path, string[] folders, string key, object defaultValue) {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(path);
            List<string> subKeyNames = new List<string>(rk.GetSubKeyNames());
            if (subKeyNames.Contains(folders[0])) {
                path += "\\" + folders[0];
                if (folders.Length == 1) {
                    return GetValue(path, key, defaultValue);
                } else {
                    List<string> rest = new List<string>();
                    for (int i = 1; i < folders.Length; i++) {
                        rest.Add(folders[i]);
                    }

                    return GetValue(path, rest.ToArray(), key, defaultValue);
                }
            }

            return defaultValue;
        }

        public static string[] GetKeys(string path, string folder) {
            return GetKeys(path, new List<string>() { folder });
        }

        public static string[] GetKeys(string path, List<string> folders) {
            List<string> result = new List<string>();

            foreach (string folder in folders) {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey(path + "\\" + folder);
                result.AddRange(rk.GetValueNames());
            }

            return result.ToArray();
        }

        public static string[] GetSubfolders(string path, string folder) {
            return GetSubfolders(path, new List<string>() { folder });
        }

        public static string[] GetSubfolders(string path, List<string> folders) {
            List<string> result = new List<string>();

            foreach (string folder in folders) {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey(path + "\\" + folder);
                result.AddRange(rk.GetSubKeyNames());
            }

            return result.ToArray();
        }

        public static void SetValue(string path, string key, object value) {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(path);
            regKey.SetValue(key, value == null ? string.Empty : value);
        }

        public static void ClearFolder(string path, string folder) {
            ClearFolder(path, new List<string> { folder });
        }

        public static void ClearFolder(string path, List<string> folders) {
            List<string> subKeyNames = new List<string>(Registry.CurrentUser.CreateSubKey(path).GetSubKeyNames());

            if (subKeyNames.Contains(folders[0])) {
                if (folders.Count == 1) {
                    Registry.CurrentUser.CreateSubKey(path).DeleteSubKeyTree(folders[0], false);
                } else {
                    path += "\\" + folders[0];
                    List<string> rest = new List<string>();
                    for (int i = 1; i < folders.Count; i++) {
                        rest.Add(folders[i]);
                    }

                    ClearFolder(path, rest);
                }
            }
        }

        public static void SetValue(string path, string folder, string key, object value) {
            SetValue(path, new string[] { folder }, key, value);
        }

        public static void SetValue(string path, string[] folders, string key, object value) {
            GetKey(path, folders).SetValue(key, value);
        }

        public static void Remove(string path, string key) {
            RegistryKey regKey = GetKey(path, new string[] { });
            if (regKey.GetValue(key) != null) regKey.DeleteValue(key);
        }

        public static void Remove(string path, string[] folders, string key) {
            RegistryKey regKey = GetKey(path, folders);
            if (regKey.GetValue(key) != null) regKey.DeleteValue(key);
        }

        public static RegistryKey GetKey(string path, string[] folders) {
            if (folders.Length == 0) return Registry.CurrentUser.CreateSubKey(path);

            return Registry.CurrentUser.CreateSubKey(path + "\\" + folders.Merge("\\"));
        }
    }
}
