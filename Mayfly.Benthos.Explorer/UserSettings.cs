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

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, Benthos.UserSettings.Interface.Extension + "s");
                
        public static bool AutoLoadBio
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoLoadBio), false));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(AutoLoadBio), value);
            }
        }

        public static string[] Bios
        {
            get
            {
                string[] values = (string[])UserSetting.GetValue(Path, nameof(Bios), new string [0]);
                return values.GetOperableFilenames(Wild.UserSettings.InterfaceBio.Extension);
            }

            set
            {
                UserSetting.SetValue(Path, nameof(Bios), value);
            }
        }

        public static bool MassRecoveryUseRaw
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(MassRecoveryUseRaw), true));
            }
            set
            {
                UserSetting.SetValue(Path, nameof(MassRecoveryUseRaw), value);
            }
        }
    }
}
