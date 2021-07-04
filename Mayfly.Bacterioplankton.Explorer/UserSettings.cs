using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System;
using Mayfly.Wild;

namespace Mayfly.Bacterioplankton.Explorer
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Bacterioplankton.Explorer");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, System.Reflection.Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(Wild.UserSettingPaths.Dominance, 2),
                    new UserSetting(Wild.UserSettingPaths.Diversity, DiversityIndex.D1963_Shannon)});
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path, Assembly.GetCallingAssembly()))
            {
                Initialize();
            }

            return UserSetting.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".bpcds");
    }
}
