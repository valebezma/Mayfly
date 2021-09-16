using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;

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

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".pcd", ".html");

        private static Samplers samplersIndex;

        public static Samplers SamplersIndex
        {
            get
            {
                if (samplersIndex == null)
                {
                    samplersIndex = new Samplers();
                    samplersIndex.SetAttributable();
                    samplersIndex.ReadXml(@"interface\samplerplankton.ini");
                    //samplersIndex.WriteXml(@"interface\samplerplankton.ini");
                }
                return samplersIndex;
            }
        }

        public static int SelectedSamplerID
        {
            get
            {
                return (int)UserSetting.GetValue(Path, nameof(SelectedSamplerID), 7);
            }
            set
            {
                UserSetting.SetValue(Path, nameof(SelectedSamplerID), value);
            }
        }

        public static string SpeciesIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(SpeciesIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    SpeciesIndexPath = Wild.Service.GetReferencePathSpecies("Plankton");
                }

                return SpeciesIndexPath;
            }
            set
            {
                UserSetting.SetValue(Path, nameof(SpeciesIndexPath), value);
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
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(SpeciesAutoExpand), true)); }
            set { UserSetting.SetValue(Path, nameof(SpeciesAutoExpand), value); }
        }

        public static bool SpeciesAutoExpandVisual
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(SpeciesAutoExpandVisual), true)); }
            set { UserSetting.SetValue(Path, nameof(SpeciesAutoExpandVisual), value); }
        }

        public static int SelectedWaterID
        {
            get
            {
                return (int)UserSetting.GetValue(Path, nameof(SelectedWaterID), 0);
            }

            set
            {
                UserSetting.SetValue(Path, nameof(SelectedWaterID), value);
            }
        }

        public static DateTime SelectedDate
        {
            get
            {
                object SavedDate = UserSetting.GetValue(Path, nameof(SelectedDate), DateTime.Today);
                if (SavedDate == null) return DateTime.Now.AddSeconds(-DateTime.Now.Second);
                else return Convert.ToDateTime(SavedDate);
            }
            set { UserSetting.SetValue(Path, nameof(SelectedDate), value.ToShortDateString()); }
        }

        public static string[] AddtVariables
        {
            get
            {
                return (string[])UserSetting.GetValue(Path, nameof(AddtVariables), new string[0]);
            }

            set
            {
                UserSetting.SetValue(Path, nameof(AddtVariables), value);
            }
        }

        public static string[] CurrentVariables
        {
            get
            {
                return (string[])UserSetting.GetValue(Path, nameof(CurrentVariables), new string[0]);
            }

            set
            {
                UserSetting.SetValue(Path, nameof(CurrentVariables), value);
            }
        }

        public static bool FixTotals
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(FixTotals), false));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(FixTotals), value);
            }
        }

        public static bool AutoIncreaseBio
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoIncreaseBio), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(AutoIncreaseBio), value);
            }
        }

        public static bool AutoDecreaseBio
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoDecreaseBio), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(AutoDecreaseBio), value);
            }
        }

        public static bool AutoLogOpen
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoLogOpen), false));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(AutoLogOpen), value);
            }
        }

        public static bool BreakBeforeIndividuals
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(BreakBeforeIndividuals), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(BreakBeforeIndividuals), value);
            }
        }

        public static bool BreakBetweenSpecies
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(BreakBetweenSpecies), false));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(BreakBetweenSpecies), value);
            }
        }

        public static bool OddCardStart
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(OddCardStart), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(OddCardStart), value);
            }
        }

        public static int RecentSpeciesCount
        {
            get { return (int)UserSetting.GetValue(Path, nameof(RecentSpeciesCount), 15); }
            set { UserSetting.SetValue(Path, nameof(RecentSpeciesCount), value); }
        }
    }
}