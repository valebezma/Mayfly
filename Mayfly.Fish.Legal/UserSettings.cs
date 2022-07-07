using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using static Mayfly.UserSettings;

namespace Mayfly.Fish.Legal
{
    public static class UserSettings
    {
        public static string Path
        {
            get
            {
                return GetFeatureKey("Fish.Legal");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".flic");

        public static bool HandWrite
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, nameof(HandWrite), true));
            }

            set
            {
                SetValue(Path, nameof(HandWrite), value);
            }
        }

        public static bool UseStamp
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, nameof(UseStamp), false));
            }

            set
            {
                SetValue(Path, nameof(UseStamp), value);
            }
        }

        public static bool UseFaximile
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, nameof(UseFaximile), true));
            }

            set
            {
                SetValue(Path, nameof(UseFaximile), value);
            }
        }

        public static string Stamp
        {
            get { return (string)GetValue(Path, nameof(Stamp), string.Empty); }
            set { SetValue(Path, nameof(Stamp), value); }
        }

        public static string Faximile
        {
            get { return (string)GetValue(Path, nameof(Faximile), string.Empty); }
            set { SetValue(Path, nameof(Faximile), value); }
        }

        public static bool PreventOvercatch
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, nameof(PreventOvercatch), true));
            }

            set
            {
                SetValue(Path, nameof(PreventOvercatch), value);
            }
        }

        public static int RoundCatch
        {
            get
            {
                return Convert.ToInt32(GetValue(Path, nameof(RoundCatch), 100));
            }

            set
            {
                SetValue(Path, nameof(RoundCatch), value);
            }
        }

        public static bool BindCatch
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, nameof(BindCatch), true));
            }

            set
            {
                SetValue(Path, nameof(BindCatch), value);
            }
        }





        public static bool UseWaterAsNotingOffice
        {
            get { return Convert.ToBoolean(GetValue(Path, nameof(UseWaterAsNotingOffice), false)); }
            set { SetValue(Path, nameof(UseWaterAsNotingOffice), value); }
        }

        public static string Utilization
        {
            get { return (string)GetValue(Path, nameof(Utilization), string.Empty); }
            set { SetValue(Path, nameof(Utilization), value); }
        }

        public static string NoteVariant
        {
            get { return (string)GetValue(Path, nameof(NoteVariant), string.Empty); }
            set { SetValue(Path, nameof(NoteVariant), value); }
        }

        public static string UtilizationVariant
        {
            get { return (string)GetValue(Path, nameof(UtilizationVariant), string.Empty); }
            set { SetValue(Path, nameof(UtilizationVariant), value); }
        }

        public static string ProxyFace
        {
            get { return (string)GetValue(Path, nameof(ProxyFace), string.Empty); }
            set { SetValue(Path, nameof(ProxyFace), value); }
        }

        public static string ProxyDuty
        {
            get { return (string)GetValue(Path, nameof(ProxyDuty), string.Empty); }
            set { SetValue(Path, nameof(ProxyDuty), value); }
        }

        public static string TransportationRoute
        {
            get { return (string)GetValue(Path, nameof(TransportationRoute), string.Empty); }
            set { SetValue(Path, nameof(TransportationRoute), value); }
        }

        public static string TransportationOrg
        {
            get { return (string)GetValue(Path, nameof(TransportationOrg), string.Empty); }
            set { SetValue(Path, nameof(TransportationOrg), value); }
        }

        public static string TransportationAddress
        {
            get { return (string)GetValue(Path, nameof(TransportationAddress), string.Empty); }
            set { SetValue(Path, nameof(TransportationAddress), value); }
        }

        public static string TransportationConservation
        {
            get { return (string)GetValue(Path, nameof(TransportationConservation), string.Empty); }
            set { SetValue(Path, nameof(TransportationConservation), value); }
        }

        public static string TransportationDish
        {
            get { return (string)GetValue(Path, nameof(TransportationDish), string.Empty); }
            set { SetValue(Path, nameof(TransportationDish), value); }
        }
    }
}
