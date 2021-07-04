using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System;
using Mayfly.Extensions;
using Mayfly.Wild;

namespace Mayfly.Benthos.Explorer
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Benthos.Explorer");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(Wild.UserSettingPaths.Dominance, 2),
                    new UserSetting(Wild.UserSettingPaths.Diversity, Wild.DiversityIndex.D1963_Shannon),
                    new UserSetting(UserSettingPaths.FourageMark, "корм")
                });
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

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, Benthos.UserSettings.Interface.Extension + "s");

        

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



        public static string FourageMark
        {
            get
            {
                return (string)UserSetting.GetValue(Path, UserSettingPaths.FourageMark);
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.FourageMark, value);
            }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string FourageMark = "FourageMark";
    }
}
