using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Mayfly.Fish.Explorer.Observations;
using System.Reflection;
using System.Resources;
using Mayfly.Wild;

namespace Mayfly.Prospect
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Prospect");
            }
        }

        public static void Initialize()
        {
            Wild.UserSettings.Initialize();

            UserSetting.InitializeRegistry(Path, System.Reflection.Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.UpdateFrequency, 7, false)
                });
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path, 
                Assembly.GetCallingAssembly()))
            {
                UserSettings.Initialize();
            }

            return UserSetting.GetValue(path, key);
        }

        public static string CardsPath 
        {
            get
            {
                return FileSystem.GetPath(UserSetting.GetValue(Path, UserSettingPaths.CardsPath));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.CardsPath, value);
            }
        }

        public static int UpdateFrequency 
        {
            get { return (int)GetValue(Path, UserSettingPaths.UpdateFrequency); }
            set { UserSetting.SetValue(Path, UserSettingPaths.UpdateFrequency, value); }
        }

        public static int SelectedBenthosBase 
        {
            get
            {
                object o = GetValue(Path, UserSettingPaths.SelectedBenthosBase);
                if (o == null) return -1;
                else return (int)o;
            }
            set { UserSetting.SetValue(Path, UserSettingPaths.SelectedBenthosBase, value); }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string CardsPath = "CardsPath";

        public static string UpdateFrequency = "UpdateFrequency";

        public static string SelectedBenthosBase = "SelectedBenthosBase";


        public static string LocalPlanktonCopyPath
        {
            get
            {
                return System.IO.Path.Combine(FileSystem.UserFolder, "LocalPlankton.dat");
            }
        }

        public static string LocalBenthosCopyPath
        {
            get
            {
                return System.IO.Path.Combine(FileSystem.UserFolder, "LocalBenthos.dat");
            }
        }

        public static string LocalFishCopyPath
        {
            get
            {
                return System.IO.Path.Combine(FileSystem.UserFolder, "LocalFish.dat");
            }
        }
    }
}
