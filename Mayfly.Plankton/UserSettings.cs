using Mayfly.Wild;
using Mayfly.Species;
using Mayfly.Waters;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Plankton
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Plankton");
            }
        }

        public static void Initialize()
        {
            Wild.UserSettings.Initialize();

            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(Wild.UserSettingPaths.Sampler, 7),
                    new UserSetting(Wild.UserSettingPaths.Water, 0),
                    new UserSetting(Wild.UserSettingPaths.FixTotals, false),
                    new UserSetting(Wild.UserSettingPaths.AutoIncreaseTotals, false),
                    new UserSetting(Wild.UserSettingPaths.AutoDecreaseTotals, false),
                    new UserSetting(Wild.UserSettingPaths.AutoLogOpen, false),
                    new UserSetting(Wild.UserSettingPaths.BreakBeforeIndividuals, true),
                    new UserSetting(Wild.UserSettingPaths.BreakBetweenSpecies, false),
                    new UserSetting(Wild.UserSettingPaths.OddCardStart, true),
                    new UserSetting(Wild.UserSettingPaths.AddtVars, new string[0]),
                    new UserSetting(Species.UserSettingPaths.RecentItemsCount, 15),
                    new UserSetting(Species.UserSettingPaths.SpeciesAutoExpand, true),
                    new UserSetting(Species.UserSettingPaths.SpeciesAutoExpandVisual, true)
                });
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path, Assembly.GetCallingAssembly()))
            {
                Initialize();
            }

            return UserSetting.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".pcd");


        private static Samplers samplersIndex;

        public static Samplers SamplersIndex
        {
            get
            {
                if (samplersIndex == null)
                {
                    samplersIndex = new Samplers();
                    samplersIndex.ReadXml(@"interface\samplerplankton.ini");
                }
                return samplersIndex;
            }
        }

        public static int SelectedSamplerID
        {
            get
            {
                return (int)GetValue(Path, Wild.UserSettingPaths.Sampler);
            }
            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.Sampler, value);
            }
        }


        public static string SpeciesIndexPath
        {
            get
            {
                return Wild.Service.GetReferencePathSpecies(Path, Wild.UserSettingPaths.Species, "Plankton");
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.Species, value);
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
                        speciesIndex.Read(SpeciesIndexPath);
                    }
                }

                return speciesIndex;
            }

            set
            {
                speciesIndex = value;
            }
        }

        public static bool SpeciesAutoExpand
        {
            get { return Convert.ToBoolean(GetValue(Path, Species.UserSettingPaths.SpeciesAutoExpand)); }
            set { UserSetting.SetValue(Path, Species.UserSettingPaths.SpeciesAutoExpand, value); }
        }

        public static bool SpeciesAutoExpandVisual
        {
            get { return Convert.ToBoolean(GetValue(Path, Species.UserSettingPaths.SpeciesAutoExpandVisual)); }
            set { UserSetting.SetValue(Path, Species.UserSettingPaths.SpeciesAutoExpandVisual, value); }
        }



        public static int SelectedWaterID
        {
            get
            {
                return (int)GetValue(Path, Wild.UserSettingPaths.Water);
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.Water, value);
            }
        }


        public static DateTime SelectedDate
        {
            get
            {
                object SavedDate = GetValue(Path, Wild.UserSettingPaths.Date);
                if (SavedDate == null) return DateTime.Now.AddSeconds(-DateTime.Now.Second);
                else return Convert.ToDateTime(SavedDate);
            }
            set { UserSetting.SetValue(Path, Wild.UserSettingPaths.Date, value.ToShortDateString()); }
        }


        public static string[] AddtVariables
        {
            get
            {
                return (string[])GetValue(Path, Wild.UserSettingPaths.AddtVars);
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.AddtVars, value);
            }
        }

        public static string[] CurrentVariables
        {
            get
            {
                return (string[])GetValue(Path, Wild.UserSettingPaths.CurrVars);
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.CurrVars, value);
            }
        }


        public static bool FixTotals
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.FixTotals));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.FixTotals, value);
            }
        }

        public static bool AutoIncreaseBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.AutoIncreaseTotals));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.AutoIncreaseTotals, value);
            }
        }

        public static bool AutoDecreaseBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.AutoDecreaseTotals));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.AutoDecreaseTotals, value);
            }
        }


        public static bool AutoLogOpen
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.AutoLogOpen));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.AutoLogOpen, value);
            }
        }

        public static bool BreakBeforeIndividuals
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.BreakBeforeIndividuals));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.BreakBeforeIndividuals, value);
            }
        }

        public static bool BreakBetweenSpecies
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.BreakBetweenSpecies));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.BreakBetweenSpecies, value);
            }
        }

        public static bool OddCardStart
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.OddCardStart));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.OddCardStart, value);
            }
        }

        public static int RecentSpeciesCount
        {
            get { return (int)GetValue(Path, Species.UserSettingPaths.RecentItemsCount); }
            set { UserSetting.SetValue(Path, Species.UserSettingPaths.RecentItemsCount, value); }
        }
    }
}