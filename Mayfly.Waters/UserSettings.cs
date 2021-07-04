using System;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Waters
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Waters");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, System.Reflection.Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.SearchItemsCount, 15)
                });
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path, Assembly.GetCallingAssembly()))
            {
                Initialize();
            }

            return UserSetting.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".wtr");

        public static int SearchItemsCount
        {
            get { return (int)GetValue(Path, UserSettingPaths.SearchItemsCount); }
            set { UserSetting.SetValue(Path, UserSettingPaths.SearchItemsCount, value); }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string SearchItemsCount = "SearchItemsCount";
    }
}
