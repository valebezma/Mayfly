using System;

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

        public static FileSystemInterface Interface = new FileSystemInterface(null, ".sps", ".html");

        public static bool UseClassicKeyReport 
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(UseClassicKeyReport), false)); }
            set { UserSetting.SetValue(Path, nameof(UseClassicKeyReport), value); }
        }

        public static string CoupletChar 
        {
            get { return (string)UserSetting.GetValue(Path, nameof(CoupletChar), "'"); }
            set { UserSetting.SetValue(Path, nameof(CoupletChar), value); }
        }

        public static int AllowableSpeciesListLength 
        {
            get { return (int)UserSetting.GetValue(Path, nameof(AllowableSpeciesListLength), 50); }
            set { UserSetting.SetValue(Path, nameof(AllowableSpeciesListLength), value); }
        }
    }
}
