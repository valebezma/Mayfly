using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;

namespace Mayfly.Fish.Legal
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Fish.Legal");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".flic");

        public static bool HandWrite
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(HandWrite), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(HandWrite), value);
            }
        }

        public static bool UseStamp
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(UseStamp), false));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(UseStamp), value);
            }
        }

        public static bool UseFaximile
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(UseFaximile), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(UseFaximile), value);
            }
        }

        public static string Stamp
        {
            get { return (string)UserSetting.GetValue(Path, nameof(Stamp), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(Stamp), value); }
        }

        public static string Faximile
        {
            get { return (string)UserSetting.GetValue(Path, nameof(Faximile), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(Faximile), value); }
        }

        public static bool PreventOvercatch
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(PreventOvercatch), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(PreventOvercatch), value);
            }
        }

        public static int RoundCatch
        {
            get
            {
                return Convert.ToInt32(UserSetting.GetValue(Path, nameof(RoundCatch), 100));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(RoundCatch), value);
            }
        }

        public static bool BindCatch
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(BindCatch), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(BindCatch), value);
            }
        }





        public static bool UseWaterAsNotingOffice
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(UseWaterAsNotingOffice), false)); }
            set { UserSetting.SetValue(Path, nameof(UseWaterAsNotingOffice), value); }
        }

        public static string Utilization
        {
            get { return (string)UserSetting.GetValue(Path, nameof(Utilization), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(Utilization), value); }
        }

        public static string NoteVariant
        {
            get { return (string)UserSetting.GetValue(Path, nameof(NoteVariant), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(NoteVariant), value); }
        }

        public static string UtilizationVariant
        {
            get { return (string)UserSetting.GetValue(Path, nameof(UtilizationVariant), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(UtilizationVariant), value); }
        }

        public static string ProxyFace
        {
            get { return (string)UserSetting.GetValue(Path, nameof(ProxyFace), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(ProxyFace), value); }
        }

        public static string ProxyDuty
        {
            get { return (string)UserSetting.GetValue(Path, nameof(ProxyDuty), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(ProxyDuty), value); }
        }

        public static string TransportationRoute
        {
            get { return (string)UserSetting.GetValue(Path, nameof(TransportationRoute), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(TransportationRoute), value); }
        }

        public static string TransportationOrg
        {
            get { return (string)UserSetting.GetValue(Path, nameof(TransportationOrg), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(TransportationOrg), value); }
        }

        public static string TransportationAddress
        {
            get { return (string)UserSetting.GetValue(Path, nameof(TransportationAddress), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(TransportationAddress), value); }
        }

        public static string TransportationConservation
        {
            get { return (string)UserSetting.GetValue(Path, nameof(TransportationConservation), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(TransportationConservation), value); }
        }

        public static string TransportationDish
        {
            get { return (string)UserSetting.GetValue(Path, nameof(TransportationDish), string.Empty); }
            set { UserSetting.SetValue(Path, nameof(TransportationDish), value); }
        }
    }
}
