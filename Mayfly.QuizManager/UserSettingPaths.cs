using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly;

namespace QuizManager
{
    class UserSettingPaths
    {
        public static string Path
        {
            get
            {
                return UserSettingInterface.GetFeatureKey("QuizManager");
            }
        }

        public static void InitializeUserSettings()
        {
            UserSettingInterface.InitializeRegistry(UserSettingPaths.Path, System.Reflection.Assembly.GetCallingAssembly(),
                new UserSetting[] {                 });
        }
    }
}
