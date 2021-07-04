using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Calculus
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Calculus");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] { });
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

        public static FileSystemInterface Interface = new FileSystemInterface(null, ".csv", ".prn", ".txt");
    }
}
