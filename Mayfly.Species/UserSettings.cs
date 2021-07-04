using System;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Species
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Species");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] { new UserSetting(UserSettingPaths.UseClassicKeyReport, false),
                    new UserSetting(UserSettingPaths.CoupletChar, "'"),
                    new UserSetting(UserSettingPaths.AllowableSpeciesListLength, 50),
                    new UserSetting(UserSettingPaths.RecentItemsCount, 5)
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

        public static FileSystemInterface Interface = new FileSystemInterface(null, ".sps", ".html");

        public static bool UseClassicKeyReport 
        {
            get { return Convert.ToBoolean(GetValue(Path, UserSettingPaths.UseClassicKeyReport)); }
            set { UserSetting.SetValue(Path, UserSettingPaths.UseClassicKeyReport, value); }
        }

        public static string CoupletChar 
        {
            get { return (string)GetValue(Path, UserSettingPaths.CoupletChar); }
            set { UserSetting.SetValue(Path, UserSettingPaths.CoupletChar, value); }
        }

        public static int AllowableSpeciesListLength 
        {
            get { return (int)GetValue(Path, UserSettingPaths.AllowableSpeciesListLength); }
            set { UserSetting.SetValue(Path, UserSettingPaths.AllowableSpeciesListLength, value); }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string CoupletChar = "CoupletChar";

        public static string UseClassicKeyReport = "UseClassicKeyReport";

        public static string AllowableSpeciesListLength = "AllowableSpeciesListLength";

        public static string RecentItemsCount = "RecentItemsCount";

        public static string SpeciesAutoExpand = "SpeciesAutoExpand";

        public static string SpeciesAutoExpandVisual = "SpeciesAutoExpandVisual";
    }
}
