
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Library
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Library");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(Separator, ";", true)
                    //new UserSetting(Wild.UserSettingPaths.Water, 0),
                    //new UserSetting(Wild.UserSettingPaths.FixTotals, false),
                    //new UserSetting(Wild.UserSettingPaths.AutoIncreaseBio, false),
                    //new UserSetting(Wild.UserSettingPaths.AutoDecreaseBio, false),
                    //new UserSetting(Wild.UserSettingPaths.AutoLogOpen, false),
                    //new UserSetting(Wild.UserSettingPaths.BreakBeforeIndividuals, true),
                    //new UserSetting(Wild.UserSettingPaths.BreakBetweenSpecies, false),
                    //new UserSetting(Wild.UserSettingPaths.OddCardStart, true),
                    //new UserSetting(Wild.UserSettingPaths.AddtVars, new string[0]),
                    //new UserSetting(Species.UserSettingPaths.RecentItemsCount, 15),
                    //new UserSetting(Species.UserSettingPaths.SpeciesAutoExpand, true),
                    //new UserSetting(Species.UserSettingPaths.SpeciesAutoExpandVisual, true)
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

        public static FileSystemInterface Interface = new FileSystemInterface(".lbr");

        public static string Separator
        {
            get
            {
                return (string)UserSetting.GetValue(Path, UserSettingPaths.Separator);
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.Separator, value);
            }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string Separator = "Separator";
    }
}