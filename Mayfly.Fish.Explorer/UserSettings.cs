﻿using Mayfly.Extensions;
using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Fish.Explorer
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Fish.Explorer");
            }
        }

        public static void Initialize()
        {
            Wild.UserSettings.Initialize();

            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.DefaultCatchability, 20),
                    new UserSetting(UserSettingPaths.MemCategorization, 1),
                    new UserSetting(UserSettingPaths.MemWaterArea, 10000000),
                    new UserSetting(UserSettingPaths.MemWaterDepth, 250),
                    new UserSetting(Wild.UserSettingPaths.Dominance, 2),
                    new UserSetting(Wild.UserSettingPaths.Diversity, DiversityIndex.D1963_Shannon),
                    new UserSetting(UserSettingPaths.SuggestAge, true),
                    new UserSetting(Wild.UserSettingPaths.SuggestMass, true),
                    //new UserSetting(Wild.UserSettingPaths.VisualConfirmation, true),
                    new UserSetting(Wild.UserSettingPaths.AutoLoadBio, false),
                    new UserSetting(UserSettingPaths.SizeInterval, 1000),
                    new UserSetting(UserSettingPaths.KeepWizard, false),
                    new UserSetting(UserSettingPaths.ReportCriticality, ArtifactCriticality.Bad),
                    new UserSetting(UserSettingPaths.SelectedAgeLengthKeyType, 1)
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

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, Fish.UserSettings.Interface.Extension + "s");



        public static bool AgeSuggest
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.SuggestAge));
            }

            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.SuggestAge, value);
            }
        }

        public static bool MassSuggest
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.SuggestMass));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.SuggestMass, value);
            }
        }

        //public static bool VisualConfirmation
        //{
        //    get
        //    {
        //        return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.VisualConfirmation));
        //    }

        //    set
        //    {
        //        UserSetting.SetValue(Path, Wild.UserSettingPaths.VisualConfirmation, value);
        //    }
        //}

        public static bool AutoLoadBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, Wild.UserSettingPaths.AutoLoadBio));
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.AutoLoadBio, value);
            }
        }

        public static bool KeepWizard
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.KeepWizard));
            }
            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.KeepWizard, value);
            }
        }

        public static ArtifactCriticality ReportCriticality
        {
            get
            {
                object o = GetValue(Path, UserSettingPaths.ReportCriticality);
                if (o == null) return ArtifactCriticality.Critical;
                else return (ArtifactCriticality)(int)o;
            }
            set { UserSetting.SetValue(Path, UserSettingPaths.ReportCriticality, (int)value); }
        }

        public static bool CheckConsistency
        {
            get
            {
                return Convert.ToBoolean(GetValue(Path, UserSettingPaths.CheckConsistency));
            }
            set
            {
                UserSetting.SetValue(Path, UserSettingPaths.CheckConsistency, value);
            }
        }

        public static string[] Bios
        {
            get
            {
                string[] values = (string[])UserSetting.GetValue(Path, Wild.UserSettingPaths.Bios);
                return values.GetOperableFilenames(Wild.UserSettings.InterfaceBio.Extension);
            }

            set
            {
                UserSetting.SetValue(Path, Wild.UserSettingPaths.Bios, value);
            }
        }



        public static string DominanceIndexName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (Wild.UserSettings.Dominance)
                {
                    case 0:
                        return resources.GetString("comboBoxDominance.Items");
                    default:
                        return resources.GetString("comboBoxDominance.Items" + Wild.UserSettings.Dominance);
                }
            }
        }



        public static FishSamplerType MemorizedSamplerType
        {
            get
            {
                object o = GetValue(Path, UserSettingPaths.MemSamplerType);
                if (o == null) return FishSamplerType.None;
                else return (FishSamplerType)(int)o;
            }
            set { UserSetting.SetValue(Path, UserSettingPaths.MemSamplerType, (int)value); }
        }

        public static double MemorizedWaterArea
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.MemWaterArea); }
            set { UserSetting.SetValue(Path, UserSettingPaths.MemWaterArea, (int)value); }
        }

        public static double MemorizedWaterDepth
        {
            get { return 0.01 * (double)(int)GetValue(Path, UserSettingPaths.MemWaterDepth); }
            set { UserSetting.SetValue(Path, UserSettingPaths.MemWaterDepth, (int)value * 100); }
        }


        public static double DefaultCatchability
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.DefaultCatchability) / 100; }
            set { UserSetting.SetValue(Path, UserSettingPaths.DefaultCatchability, (int)(value * 100)); }
        }

        public static double SizeInterval
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.SizeInterval) / 100; }
            set { UserSetting.SetValue(Path, UserSettingPaths.SizeInterval, (int)(value * 100)); }
        }



        public static int RequiredClassSize { get { return 10; } }

        public static AgeLengthKeyType SelectedAgeLengthKeyType
        {
            get
            {
                object o = GetValue(Path, UserSettingPaths.SelectedAgeLengthKeyType);
                if (o == null) return AgeLengthKeyType.Raw;
                else return (AgeLengthKeyType)(int)o;
            }
            set { UserSetting.SetValue(Path, UserSettingPaths.SelectedAgeLengthKeyType, (int)value); }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string Equipment = "Equipment";

        public static string KeepWizard = "KeepWizard";

        public static string CheckConsistency = "CheckConsistency";

        public static string ReportCriticality = "ReportCriticality";



        public static string SuggestAge = "SuggestAge";

        public static string SelectedAgeLengthKeyType = "SelectedAgeLengthKeyType";



        #region Fisheries issues

        public static string DefaultCatchability = "DefaultCatchability";

        public static string Catchability = "Catchability";

        public static string GamingAge = "GamingAge";

        public static string GamingLength = "GamingLength";

        public static string GearClass = "GearClass";

        public static string SizeInterval = "SizeInterval";

        public static string NaturalMortality = "NaturalMortality";

        public static string FishingMortality = "FishingMortality";

        #endregion



        #region Stock calculation memorized values

        public static string MemCategorization = "MemCategorization";

        public static string MemSamplerType = "MemSamplerType";

        public static string MemWeightType = "MemWeightType";

        public static string MemWaterArea = "MemWaterArea";

        public static string MemWaterDepth = "MemWaterDepth";

        #endregion
    }
}
