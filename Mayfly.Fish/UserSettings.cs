using Mayfly.Wild;
using Mayfly.Species;
using Mayfly.Waters;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Mayfly.Extensions;

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

        public static void Initialize()
        {
            Wild.UserSettings.Initialize();

            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] { new UserSetting(Wild.UserSettingPaths.Sampler, 710),
                    new UserSetting(Wild.UserSettingPaths.Water, 0),
                    new UserSetting(Wild.UserSettingPaths.FixTotals, true),
                    new UserSetting(Wild.UserSettingPaths.AutoIncreaseTotals, true),
                    new UserSetting(Wild.UserSettingPaths.AutoDecreaseTotals, true),
                    new UserSetting(Wild.UserSettingPaths.AutoLogOpen, true),
                    new UserSetting(Wild.UserSettingPaths.BreakBeforeIndividuals, true),
                    new UserSetting(Wild.UserSettingPaths.BreakBetweenSpecies, true),
                    new UserSetting(Wild.UserSettingPaths.OddCardStart, true),
                    new UserSetting(Wild.UserSettingPaths.AddtVars, new string[] { "FL", "TL" }),
                    new UserSetting(UserSettingPaths.InheritGrowth, true),
                    new UserSetting(UserSettingPaths.ApproveGrowthModel, true),
                    new UserSetting(UserSettingPaths.Opening, 60, true),
                    new UserSetting(UserSettingPaths.GillnetStdLength, 3750),
                    new UserSetting(UserSettingPaths.GillnetStdHeight, 200),
                    new UserSetting(UserSettingPaths.GillnetStdExposure, 24),
                    new UserSetting(Species.UserSettingPaths.RecentItemsCount, 15),
                    new UserSetting(Wild.UserSettingPaths.DefaultStratifiedInterval, 1000, true),
                    new UserSetting(Species.UserSettingPaths.SpeciesAutoExpand, true),
                    new UserSetting(Species.UserSettingPaths.SpeciesAutoExpandVisual, true)
                });

            UserSetting.SetValue(Path, UserSettingPaths.EffortVariant, "Gillnet", 2);
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
            get { return (int)GetValue(Path, Wild.UserSettingPaths.Sampler); }
            set { UserSetting.SetValue(Path, Wild.UserSettingPaths.Sampler, value); }
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
                return Wild.Service.GetReferencePathSpecies(Path, Wild.UserSettingPaths.Species, "Fish");
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


        public static string ParasitesIndexPath
        {
            get
            {
                return Wild.Service.GetReferencePathSpecies(Path, UserSettingPaths.Parasites, "Fish Parasites");
            }

            set 
            {
                UserSetting.SetValue(Path, UserSettingPaths.Parasites, value);
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
                return Wild.Service.GetReferencePathSpecies(Path, UserSettingPaths.Diet, "Fish Diet");
            }

            set 
            {
                UserSetting.SetValue(Path, UserSettingPaths.Diet, value);
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
            get { return (int)GetValue(Path, Wild.UserSettingPaths.Water); }
            set { UserSetting.SetValue(Path, Wild.UserSettingPaths.Water, value); }
        }

        public static DateTime SelectedDate
        {
            get {
                object SavedDate = GetValue(Path, Wild.UserSettingPaths.Date);
                if (SavedDate == null) return DateTime.Now.AddSeconds(-DateTime.Now.Second);
                else return Convert.ToDateTime(SavedDate);
            }

            set 
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.Date, value.ToShortDateString()); 
            }
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


        public static double DefaultOpening
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.Opening) / 100; }
            set { UserSetting.SetValue(Path, UserSettingPaths.Opening, (int)(value * 100)); }
        }


        public static double GillnetStdLength
        {
            get { return (double)((int)GetValue(Path, UserSettingPaths.GillnetStdLength)) / 100; }
            set { UserSetting.SetValue(Path, UserSettingPaths.GillnetStdLength, (int)(value * 100)); }
        }

        public static double GillnetStdHeight
        {
            get { return (double)((int)GetValue(Path, UserSettingPaths.GillnetStdHeight)) / 100; }
            set { UserSetting.SetValue(Path, UserSettingPaths.GillnetStdHeight, (int)(value * 100)); }
        }

        public static int GillnetStdExposure
        {
            get { return (int)GetValue(Path, UserSettingPaths.GillnetStdExposure); }
            set { UserSetting.SetValue(Path, UserSettingPaths.GillnetStdExposure, value); }
        }

        public static int RecentSpeciesCount
        {
            get { return (int)GetValue(Path, Species.UserSettingPaths.RecentItemsCount); }
            set { UserSetting.SetValue(Path, Species.UserSettingPaths.RecentItemsCount, value); }
        }



        public static double DefaultStratifiedInterval
        {
            get { return (double)((int)GetValue(Path, Wild.UserSettingPaths.DefaultStratifiedInterval)) / 100; }
            set { UserSetting.SetValue(Path, Wild.UserSettingPaths.DefaultStratifiedInterval, (int)(value * 100)); }
        }
    }


    public abstract class UserSettingPaths
    {
        public static string Diet = "RefDiet";

        public static string Parasites = "RefParasites";

        public static string Opening = "DefaultOpening";

        public static string InheritGrowth = "MemInheritGrowth";

        public static string ApproveGrowthModel = "MemApproveGrowthModel";

        public static string EffortVariant = "EffortVariant";

        public static string GillnetStdLength = "GillnetStdLength";

        public static string GillnetStdHeight = "GillnetStdHeight";

        public static string GillnetStdExposure = "GillnetStdExposure";

        //public static string Equipment = "Equipment";
    }
}
