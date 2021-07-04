using Mayfly.Wild;
using Mayfly.Species;
using Mayfly.Waters;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Reader
{
    public abstract class UserSettings
    {
        public static object GetValue(string path, string key)
        {
            if (UserSettingInterface.InitializationRequired(UserSettingPaths.Path,
                Assembly.GetCallingAssembly()))
            {
                UserSettingPaths.InitializeUserSettings();
            }

            return UserSettingInterface.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".wcd", ".bcd", ".fcd", ".bpcd");


        public static string SpeciesIndexPath
        {
            get
            {
                return Wild.Service.GetReferencePathSpecies(UserSettingPaths.Path, Wild.UserSettingPaths.Species, "Wild");
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.Species, value);
            }
        }

        private static SpeciesKey speciesIndex;

        public static SpeciesKey SpeciesIndex
        {
            get
            {
                if (speciesIndex == null)
                {
                    speciesIndex = new SpeciesKey();

                    if (SpeciesIndexPath != null)
                    {
                        speciesIndex.ReadXml(SpeciesIndexPath);
                    }
                }

                return speciesIndex;
            }

            set
            {
                speciesIndex = value;
            }
        }

        
        public static int SelectedWaterID
        {
            get
            {
                return (int)GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.Water);
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.Water, value);
            }
        }


        public static DateTime SelectedDate
        {
            get
            {
                object SavedDate = GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.Date);
                if (SavedDate == null) return DateTime.Today;
                else return Convert.ToDateTime(SavedDate);
            }
            set { UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.Date, value.ToShortDateString()); }
        }


        public static string[] AddtVariables
        {
            get
            {
                return (string[])GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AddtVars);
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AddtVars, value);
            }
        }

        public static string[] CurrentVariables
        {
            get
            {
                return (string[])GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.CurrVars);
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.CurrVars, value);
            }
        }


        public static bool FixTotals
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.FixTotals));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.FixTotals, value);
            }
        }

        public static bool AutoIncreaseBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AutoIncreaseTotals));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AutoIncreaseTotals, value);
            }
        }

        public static bool AutoDecreaseBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AutoDecreaseTotals));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AutoDecreaseTotals, value);
            }
        }


        public static bool AutoLogOpen
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AutoLogOpen));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.AutoLogOpen, value);
            }
        }

        public static bool BreakBeforeIndividuals
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.BreakBeforeIndividuals));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.BreakBeforeIndividuals, value);
            }
        }

        public static bool BreakBetweenSpecies
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.BreakBetweenSpecies));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.BreakBetweenSpecies, value);
            }
        }

        public static bool OddCardStart
        {
            get
            {
                return Convert.ToBoolean(GetValue(UserSettingPaths.Path, Wild.UserSettingPaths.OddCardStart));
            }

            set
            {
                UserSettingInterface.SetValue(UserSettingPaths.Path, Wild.UserSettingPaths.OddCardStart, value);
            }
        }

        public static int RecentSpeciesCount
        {
            get { return (int)GetValue(UserSettingPaths.Path, Species.UserSettingPaths.RecentItemsCount); }
            set { UserSettingInterface.SetValue(UserSettingPaths.Path, Species.UserSettingPaths.RecentItemsCount, value); }
        }
    }
}