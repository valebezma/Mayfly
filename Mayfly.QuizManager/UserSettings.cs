using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly;
using System.IO;
using System.Windows.Forms;

namespace QuizManager
{
    public class UserSettings
    {
        public static object GetValue(string path, string key)
        {
            if (UserSettingInterface.InitializationRequired(UserSettingPaths.Path,
                System.Reflection.Assembly.GetCallingAssembly()))
            {
                UserSettingPaths.InitializeUserSettings();
            }

            return UserSettingInterface.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".quiz");

        public static FileSystemInterface InterfaceMedia = new FileSystemInterface(null, ".jpeg", ".png", ".avi", ".mpeg", ".mp3", ".ogg");
    }
}
