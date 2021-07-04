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

        public static void Initialize()
        {
            Wild.UserSettings.Initialize();

            UserSetting.InitializeRegistry(Path, System.Reflection.Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.UseWaterAsNotingOffice, false),
                    new UserSetting(UserSettingPaths.HandWrite, true),
                    new UserSetting(UserSettingPaths.UseStamp, false),
                    new UserSetting(UserSettingPaths.PreventOvercatch, false),
                    new UserSetting(UserSettingPaths.RoundCatch, 100),
                    new UserSetting(UserSettingPaths.BindCatch, true)
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

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".flic");



        public static bool HandWrite
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.HandWrite));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.HandWrite, value);
            }
        }

        public static bool UseStamp
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.UseStamp));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.UseStamp, value);
            }
        }

        public static bool UseFaximile
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.UseFaximile));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.UseFaximile, value);
            }
        }

        public static string Stamp
        {
            get { return (string)GetValue(Path, UserSettingPaths.Stamp); }
            set { UserSetting.SetValue(Path, UserSettingPaths.Stamp, value); }
        }

        public static string Faximile
        {
            get { return (string)GetValue(Path, UserSettingPaths.Faximile); }
            set { UserSetting.SetValue(Path, UserSettingPaths.Faximile, value); }
        }

        public static bool PreventOvercatch
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.PreventOvercatch));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.PreventOvercatch, value);
            }
        }

        public static int RoundCatch
        {
            get
            {
                return Convert.ToInt32(GetValue(Path, UserSettingPaths.RoundCatch));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.RoundCatch, value);
            }
        }

        public static bool BindCatch
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.BindCatch));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.BindCatch, value);
            }
        }





        public static bool UseWaterAsNotingOffice
        {
            get { return Convert.ToBoolean(GetValue(Path, UserSettingPaths.UseWaterAsNotingOffice)); }
            set { UserSetting.SetValue(Path, UserSettingPaths.UseWaterAsNotingOffice, value); }
        }

        public static string Utilization
        {
            get { return (string)GetValue(Path, UserSettingPaths.Utilization); }
            set { UserSetting.SetValue(Path, UserSettingPaths.Utilization, value); }
        }

        public static string NoteVariant
        {
            get { return (string)GetValue(Path, UserSettingPaths.NoteVariant); }
            set { UserSetting.SetValue(Path, UserSettingPaths.NoteVariant, value); }
        }

        public static string UtilizationVariant
        {
            get { return (string)GetValue(Path, UserSettingPaths.UtilizationVariant); }
            set { UserSetting.SetValue(Path, UserSettingPaths.UtilizationVariant, value); }
        }

        public static string ProxyFace
        {
            get { return (string)GetValue(Path, UserSettingPaths.ProxyFace); }
            set { UserSetting.SetValue(Path, UserSettingPaths.ProxyFace, value); }
        }

        public static string ProxyDuty
        {
            get { return (string)GetValue(Path, UserSettingPaths.ProxyDuty); }
            set { UserSetting.SetValue(Path, UserSettingPaths.ProxyDuty, value); }
        }

        public static string TransportationRoute
        {
            get { return (string)GetValue(Path, UserSettingPaths.TransportationRoute); }
            set { UserSetting.SetValue(Path, UserSettingPaths.TransportationRoute, value); }
        }

        public static string TransportationOrg
        {
            get { return (string)GetValue(Path, UserSettingPaths.TransportationOrg); }
            set { UserSetting.SetValue(Path, UserSettingPaths.TransportationOrg, value); }
        }

        public static string TransportationAddress
        {
            get { return (string)GetValue(Path, UserSettingPaths.TransportationAddress); }
            set { UserSetting.SetValue(Path, UserSettingPaths.TransportationAddress, value); }
        }

        public static string TransportationConservation
        {
            get { return (string)GetValue(Path, UserSettingPaths.TransportationConservation); }
            set { UserSetting.SetValue(Path, UserSettingPaths.TransportationConservation, value); }
        }

        public static string TransportationDish
        {
            get { return (string)GetValue(Path, UserSettingPaths.TransportationDish); }
            set { UserSetting.SetValue(Path, UserSettingPaths.TransportationDish, value); }
        }
    }

    public abstract class UserSettingPaths
    {
        #region Noting

        public static string UseWaterAsNotingOffice = "RepUseWater";

        public static string Utilization = "RepUtil";

        public static string NoteVariant = "NoteVariant";

        public static string UtilizationVariant = "UtilizationVariant";


        public static string ProxyFace = "RepTransResp";

        public static string ProxyDuty = "RepTransDuty";


        public static string TransportationRoute = "RepTransRoute";

        public static string TransportationOrg = "RepTransOrg";

        public static string TransportationAddress = "RepTransAddress";

        public static string TransportationConservation = "RepTransConservation";

        public static string TransportationDish = "RepTransDish";

        #endregion

        #region Filling

        public static string HandWrite = "HandWrite";

        public static string UseStamp = "UseStamp";

        public static string UseFaximile = "UseFaximile";

        public static string Stamp = "Stamp";

        public static string Faximile = "Faximile";

        public static string PreventOvercatch = "PreventOvercatch";

        public static string RoundCatch = "RoundCatch";

        public static string BindCatch = "BindCatch";

        #endregion
    }
}
