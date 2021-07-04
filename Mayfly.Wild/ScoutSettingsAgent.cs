using Mayfly.Wild;
using Mayfly.Species;
using Mayfly.Waters;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Wild
{
    public abstract class ScoutSettingsAgent
    {
        string RegistryPath { get; set; }

        public FileSystemInterface Interface { get; private set; }



        public ScoutSettingsAgent()
        {  }

        public ScoutSettingsAgent(string path)
        {
            RegistryPath = UserSetting.GetFeatureKey(path);
        }

        public ScoutSettingsAgent(string path, string extension): this(path)
        {
            Interface = new FileSystemInterface(extension);
        }

        public ScoutSettingsAgent(string path, string extension, string samplerspath): this(path, extension)
        {
            spi = samplerspath;
        }



        public abstract void Initialize();

        public object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(this.RegistryPath, Assembly.GetCallingAssembly()))
            {
                UserSettingPaths.Initialize();
            }

            return UserSetting.GetValue(this.RegistryPath, key);
        }


        private Samplers samplersIndex;

        string spi;

        public Samplers SamplersIndex
        {
            get
            {
                if (samplersIndex == null)
                {
                    samplersIndex = new Samplers();
                    samplersIndex.ReadXml(spi);
                }
                return samplersIndex;
            }
        }

        public int SelectedSamplerID
        {
            get
            {
                return (int)GetValue(RegistryPath, UserSettingPaths.Sampler);
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.Sampler, value);
            }
        }


        public string SpeciesIndexPath
        {
            get
            {
                return Service.GetReferencePathSpecies(RegistryPath, UserSettingPaths.Species, "Plankton");
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.Species, value);
            }
        }

        private SpeciesKey speciesIndex;

        public SpeciesKey SpeciesIndex
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

        public bool SpeciesAutoExpand
        {
            get { return Convert.ToBoolean(GetValue(RegistryPath, Species.UserSettingPaths.SpeciesAutoExpand)); }
            set { UserSetting.SetValue(RegistryPath, Species.UserSettingPaths.SpeciesAutoExpand, value); }
        }

        public bool SpeciesAutoExpandVisual
        {
            get { return Convert.ToBoolean(GetValue(RegistryPath, Species.UserSettingPaths.SpeciesAutoExpandVisual)); }
            set { UserSetting.SetValue(RegistryPath, Species.UserSettingPaths.SpeciesAutoExpandVisual, value); }
        }



        public int SelectedWaterID
        {
            get
            {
                return (int)GetValue(RegistryPath, UserSettingPaths.Water);
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.Water, value);
            }
        }



        public DateTime SelectedDate
        {
            get
            {
                object SavedDate = GetValue(RegistryPath, UserSettingPaths.Date);
                if (SavedDate == null) return DateTime.Now.AddSeconds(-DateTime.Now.Second);
                else return Convert.ToDateTime(SavedDate);
            }

            set { UserSetting.SetValue(RegistryPath, UserSettingPaths.Date, value.ToShortDateString()); }
        }



        public int RecentSpeciesCount
        {
            get { return (int)GetValue(RegistryPath, Species.UserSettingPaths.RecentItemsCount); }
            set { UserSetting.SetValue(RegistryPath, Species.UserSettingPaths.RecentItemsCount, value); }
        }

        public bool AutoLogOpen
        {
            get
            {
                return Convert.ToBoolean(GetValue(RegistryPath, UserSettingPaths.AutoLogOpen));
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.AutoLogOpen, value);
            }
        }



        public bool FixTotals
        {
            get
            {
                return Convert.ToBoolean(GetValue(RegistryPath, UserSettingPaths.FixTotals));
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.FixTotals, value);
            }
        }

        public bool AutoIncreaseTotals
        {
            get
            {
                return Convert.ToBoolean(GetValue(RegistryPath, UserSettingPaths.AutoIncreaseTotals));
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.AutoIncreaseTotals, value);
            }
        }

        public bool AutoDecreaseTotals
        {
            get
            {
                return Convert.ToBoolean(GetValue(RegistryPath, UserSettingPaths.AutoDecreaseTotals));
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.AutoDecreaseTotals, value);
            }
        }

        public string[] AddtVariables
        {
            get
            {
                return (string[])GetValue(RegistryPath, UserSettingPaths.AddtVars);
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.AddtVars, value);
            }
        }

        public string[] CurrentVariables
        {
            get
            {
                return (string[])GetValue(RegistryPath, UserSettingPaths.CurrVars);
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.CurrVars, value);
            }
        }



        public bool OddCardStart
        {
            get
            {
                return Convert.ToBoolean(GetValue(RegistryPath, UserSettingPaths.OddCardStart));
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.OddCardStart, value);
            }
        }
        
        public bool BreakBetweenSpecies
        {
            get
            {
                return Convert.ToBoolean(GetValue(RegistryPath, UserSettingPaths.BreakBetweenSpecies));
            }

            set
            {
                UserSetting.SetValue(RegistryPath, UserSettingPaths.BreakBetweenSpecies, value);
            }
        }
    }
}