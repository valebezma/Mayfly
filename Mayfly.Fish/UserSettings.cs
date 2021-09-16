using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Fish
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Fish");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, ".fcd", ".html");


        private static Samplers samplersIndex;

        public static Samplers SamplersIndex
        {
            get
            {
                if (samplersIndex == null)
                {
                    samplersIndex = new Samplers();
                    samplersIndex.SetAttributable();
                    samplersIndex.ReadXml(System.IO.Path.Combine(Application.StartupPath, @"interface\samplerfish.ini"));
                }
                return samplersIndex;
            }
        }

        public static int SelectedSamplerID
        {
            get { return (int)UserSetting.GetValue(Path, nameof(SelectedSamplerID), 7); }
            set { UserSetting.SetValue(Path, nameof(SelectedSamplerID), value); }
        }


        private static Equipment equiment;

        public static Equipment Equipment
        {
            get
            {
                if (equiment == null)
                {
                    equiment = new Equipment();

                    string path = System.IO.Path.Combine(IO.UserFolder, "equipment.ini");

                    if (File.Exists(path))
                    {
                        equiment.ReadXml(path);
                    }
                }

                return equiment;
            }

            set
            {
                equiment = value;
            }
        }


        public static string SpeciesIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(SpeciesIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    SpeciesIndexPath = Wild.Service.GetReferencePathSpecies("Fish");
                    return SpeciesIndexPath;
                }
                else
                {
                    return filepath;
                }
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



        public static string ParasitesIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(ParasitesIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    ParasitesIndexPath = Wild.Service.GetReferencePathSpecies("Fish Parasites");
                }

                return ParasitesIndexPath;
            }
            set
            {
                UserSetting.SetValue(Path, nameof(ParasitesIndexPath), value);
            }
        }

        private static SpeciesKey parasitesIndex;

        public static SpeciesKey ParasitesIndex
        {
            get
            {
                if (parasitesIndex == null)
                {
                    parasitesIndex = new SpeciesKey();

                    if (ParasitesIndexPath != null)
                    {
                        parasitesIndex.ReadXml(ParasitesIndexPath);
                    }
                }

                return parasitesIndex;
            }
        }


        public static string DietIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(DietIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    DietIndexPath = Wild.Service.GetReferencePathSpecies("Fish Diet");
                }

                return DietIndexPath;
            }

            set 
            {
                UserSetting.SetValue(Path, nameof(DietIndexPath), value);
            }
        }

        private static SpeciesKey dietIndex;

        public static SpeciesKey DietIndex
        {
            get
            {
                if (dietIndex == null)
                {
                    dietIndex = new SpeciesKey();

                    if (DietIndexPath != null)
                    {
                        dietIndex.ReadXml(DietIndexPath);
                    }
                }

                return dietIndex;
            }
        }



        public static int SelectedWaterID
        {
            get { return (int)UserSetting.GetValue(Path, nameof(SelectedWaterID), 0); }
            set { UserSetting.SetValue(Path, nameof(SelectedWaterID), value); }
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
                return (string[])UserSetting.GetValue(Path, nameof(AddtVariables), new string[] { "TL", "FL" });
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


        public static double DefaultOpening
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(DefaultOpening), 60) / 100; }
            set { UserSetting.SetValue(Path, nameof(DefaultOpening), (int)(value * 100)); }
        }


        public static double GillnetStdLength
        {
            get { return (double)((int)UserSetting.GetValue(Path, nameof(GillnetStdLength), 3750)) / 100; }
            set { UserSetting.SetValue(Path, nameof(GillnetStdLength), (int)(value * 100)); }
        }

        public static double GillnetStdHeight
        {
            get { return (double)((int)UserSetting.GetValue(Path, nameof(GillnetStdHeight), 200)) / 100; }
            set { UserSetting.SetValue(Path, nameof(GillnetStdHeight), (int)(value * 100)); }
        }

        public static int GillnetStdExposure
        {
            get { return (int)UserSetting.GetValue(Path, nameof(GillnetStdExposure), 24); }
            set { UserSetting.SetValue(Path, nameof(GillnetStdExposure), value); }
        }



        public static double DefaultStratifiedInterval
        {
            get { return (double)((int)UserSetting.GetValue(Path, nameof(DefaultStratifiedInterval), 1000)) / 100; }
            set { UserSetting.SetValue(Path, nameof(DefaultStratifiedInterval), (int)(value * 100)); }
        }
    }
}
