using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Win32;
using Mayfly.Extensions;
using System.Diagnostics;
using System.Reflection;

namespace Mayfly
{
    public class UserSetting
    {
        public string Path { get; set; }

        public string Parameter { get; set; }

        public object Value { get; set; }

        public bool Overwrite { get; set; }



        public UserSetting(string parameter, object value, bool overwrite)
        {
            Parameter = parameter;
            Value = value;
            Overwrite = overwrite;
        }

        public UserSetting(string parameter, object value)
            : this(parameter, value, false)
        { }


        
        public static string GetFeatureKey(string feature)
        {
            return UserSettingPaths.KeyFeatures /*+ "\\" + UserSettings.Product*/ + "\\" + feature;
        }

        public static bool InitializationRequired(string path, Assembly assembly)
        {
            Version sourceVersion = new Version(
                FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion);
            RegistryKey key = Registry.CurrentUser.CreateSubKey(path);
            object currVersion = key.GetValue(string.Empty);
            if (string.IsNullOrWhiteSpace((string)currVersion)) return true;
            if (Version.Parse((string)currVersion) < sourceVersion) return true;
            return false;
        }

        public static void InitializeRegistry(string path, Assembly assembly, UserSetting[] settings)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(path);

            RegistrySecurity security = new RegistrySecurity();
            security.SetAccessRule(new RegistryAccessRule(Environment.UserName, RegistryRights.SetValue, AccessControlType.Allow));
            security.SetAccessRule(new RegistryAccessRule(Environment.UserName, RegistryRights.WriteKey, AccessControlType.Allow));
            security.SetAccessRule(new RegistryAccessRule(Environment.UserName, RegistryRights.ReadKey, AccessControlType.Allow));
            key.SetAccessControl(security);

            if (assembly != null) key.SetValue(string.Empty, FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion);

            foreach (UserSetting userSetting in settings)
            {
                if (userSetting.Overwrite || !key.GetSubKeyNames().Contains(userSetting.Parameter))
                {
                    SetValue(path, userSetting.Parameter, userSetting.Value);
                }
            }
        }

        public static object GetValue(string path, string key)
        {
            RegistryKey subKey = Registry.CurrentUser.CreateSubKey(path);
            return subKey.GetValue(key, null);
        }

        public static object GetValue(string path, string folder, string key)
        {
            return GetValue(path, new string[] { folder }, key);
        }

        public static object GetValue(string path, string[] folders, string key)
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(path);
            List<string> subKeyNames = new List<string>(rk.GetSubKeyNames());
            if (subKeyNames.Contains(folders[0]))
            {
                path += "\\" + folders[0];
                if (folders.Length == 1)
                {
                    return GetValue(path, key);
                }
                else
                {
                    List<string> rest = new List<string>();
                    for (int i = 1; i < folders.Length; i++)
                    {
                        rest.Add(folders[i]);
                    }

                    return GetValue(path, rest.ToArray(), key);
                }
            }

            return null;
        }

        public static string[] GetKeys(string path, string folder)
        {
            return GetKeys(path, new List<string>() { folder });
        }

        public static string[] GetKeys(string path, List<string> folders)
        {
            List<string> result = new List<string>();

            foreach (string folder in folders)
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey(path + "\\" + folder);
                result.AddRange(rk.GetValueNames());
            }

            return result.ToArray();
        }

        public static string[] GetSubfolders(string path, string folder)
        {
            return GetSubfolders(path, new List<string>() { folder });
        }

        public static string[] GetSubfolders(string path, List<string> folders)
        {
            List<string> result = new List<string>();

            foreach (string folder in folders)
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey(path + "\\" + folder);
                result.AddRange(rk.GetSubKeyNames());
            }

            return result.ToArray();
        }

        public static void SetValue(string path, string key, object value)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(path);
            regKey.SetValue(key, value == null ? string.Empty : value);
        }

        public static void ClearFolder(string path, string folder)
        {
            ClearFolder(path, new List<string> { folder });
        }

        public static void ClearFolder(string path, List<string> folders)
        {
            List<string> subKeyNames = new List<string>(Registry.CurrentUser.CreateSubKey(path).GetSubKeyNames());

            if (subKeyNames.Contains(folders[0]))
            {
                if (folders.Count == 1)
                {
                    Registry.CurrentUser.CreateSubKey(path).DeleteSubKeyTree(folders[0], false);
                }
                else
                {
                    path += "\\" + folders[0];
                    List<string> rest = new List<string>();
                    for (int i = 1; i < folders.Count; i++)
                    {
                        rest.Add(folders[i]);
                    }

                    ClearFolder(path, rest);
                }
            }
        }

        public static void SetValue(string path, string folder, string key, object value)
        {
            SetValue(path, new string[] { folder }, key, value);
        }

        public static void SetValue(string path, string[] folders, string key, object value)
        {
            GetKey(path, folders).SetValue(key, value);
        }

        public static void Remove(string path, string key)
        {
            RegistryKey regKey = GetKey(path, new string[] { });
            if (regKey.GetValue(key) != null) regKey.DeleteValue(key);
        }

        public static void Remove(string path, string[] folders, string key)
        {
            RegistryKey regKey = GetKey(path, folders);
            if (regKey.GetValue(key) != null) regKey.DeleteValue(key);
        }

        public static RegistryKey GetKey(string path, string[] folders)
        {
            if (folders.Length == 0) return Registry.CurrentUser.CreateSubKey(path);

            return Registry.CurrentUser.CreateSubKey(path + "\\" + folders.Merge("\\"));
        }
    }

    public abstract class UserSettingInterface
    {
        public string Path;

        public UserSetting[] DefaultSettings;



        public UserSettingInterface(string path, UserSetting[] settings)
        {
            Path = path;
            DefaultSettings = settings;
        }



        public void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(), DefaultSettings);
        }

        public object GetValue(string key)
        {
            if (UserSetting.InitializationRequired(UserSetting.GetFeatureKey(Path), Assembly.GetCallingAssembly()))
            {
                Initialize();
            }

            return UserSetting.GetValue(UserSetting.GetFeatureKey(Path), key);
        }
    }

    public abstract class ExtendedUserSettingInterface : UserSettingInterface
    {
        public FileSystemInterface Interface;

        public ExtendedUserSettingInterface(string path, UserSetting[] settings) : base(path, settings)
        { }

        public ExtendedUserSettingInterface(string path, UserSetting[] settings, string extension) : base(path, settings)
        {
            Interface = new Mayfly.FileSystemInterface(extension);
        }
    }
}