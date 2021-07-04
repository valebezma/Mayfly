using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System;
using Mayfly.Extensions;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Plankton.Explorer");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(Wild.UserSettingPaths.Dominance, 2),
                    new UserSetting(Wild.UserSettingPaths.Diversity, DiversityIndex.D1963_Shannon)});
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path,
                Assembly.GetCallingAssembly()))
            {
                Initialize();
            }

            return UserSetting.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, Plankton.UserSettings.Interface.Extension + "s");



        public static bool AutoLoadBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.AutoLoadBio));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.AutoLoadBio, value);
            }
        }

        public static string[] Bios
        {
            get
            {
                string[] values = (string[])UserSetting.GetValue(Path, Wild.UserSettingPaths.Bios);
                return values.GetOperableFilenames(Wild.UserSettings.InterfaceBio.Extension);
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.Bios, value);
            }
        }



        public static bool MassRecoveryUseRaw
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path,
                    Wild.UserSettingPaths.MassRestoration, Wild.UserSettingPaths.UseRaw));
            }
            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.MassRestoration,
                    Wild.UserSettingPaths.UseRaw, value);
            }
        }

        public static bool MassRecoveryProtocol
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path,
                    Wild.UserSettingPaths.MassRestoration, Wild.UserSettingPaths.Protocol));
            }
            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.MassRestoration,
                    Wild.UserSettingPaths.Protocol, value);
            }
        }
    }
}
