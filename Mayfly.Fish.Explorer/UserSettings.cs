using Mayfly.Extensions;
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

        public static FileSystemInterface Interface = new FileSystemInterface(Wild.UserSettings.FieldDataFolder, Fish.UserSettings.Interface.Extension + "s");

        public static bool SuggestAge
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(SuggestAge), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(SuggestAge), value);
            }
        }

        public static bool SuggestMass
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(SuggestMass), true));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(SuggestMass), value);
            }
        }

        public static bool AutoLoadBio
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoLoadBio), false));
            }

            set
            {
                UserSetting.SetValue(Path, nameof(AutoLoadBio), value);
            }
        }

        public static bool KeepWizard
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(KeepWizard), false));
            }
            set
            {
                UserSetting.SetValue(Path, nameof(KeepWizard), value);
            }
        }

        public static ArtifactCriticality ReportCriticality
        {
            get
            {
                object o = UserSetting.GetValue(Path, nameof(ReportCriticality), ArtifactCriticality.Allowed);
                if (o == null) return ArtifactCriticality.Critical;
                else return (ArtifactCriticality)(int)o;
            }
            set { UserSetting.SetValue(Path, nameof(ReportCriticality), (int)value); }
        }

        public static bool CheckConsistency
        {
            get
            {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(CheckConsistency), true));
            }
            set
            {
                UserSetting.SetValue(Path, nameof(CheckConsistency), value);
            }
        }

        public static string[] Bios
        {
            get
            {
                string[] values = (string[])UserSetting.GetValue(Path, nameof(Bios), new string[0]);
                return values.GetOperableFilenames(Wild.UserSettings.InterfaceBio.Extension);
            }

            set
            {
                UserSetting.SetValue(Path, nameof(Bios), value);
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
                object o = UserSetting.GetValue(Path, nameof(MemorizedSamplerType), null);
                if (o == null) return FishSamplerType.None;
                else return (FishSamplerType)(int)o;
            }
            set { UserSetting.SetValue(Path, nameof(MemorizedSamplerType), (int)value); }
        }

        public static double MemorizedWaterArea
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(MemorizedWaterArea), 1); }
            set { UserSetting.SetValue(Path, nameof(MemorizedWaterArea), (int)value); }
        }

        public static double MemorizedWaterDepth
        {
            get { return 0.01 * (double)(int)UserSetting.GetValue(Path, nameof(MemorizedWaterDepth), 100); }
            set { UserSetting.SetValue(Path, nameof(MemorizedWaterDepth), (int)value * 100); }
        }

        public static double DefaultCatchability
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(DefaultCatchability), 200) / 100; }
            set { UserSetting.SetValue(Path, nameof(DefaultCatchability), (int)(value * 100)); }
        }

        public static double SizeInterval
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(SizeInterval), 1000) / 100; }
            set { UserSetting.SetValue(Path, nameof(SizeInterval), (int)(value * 100)); }
        }

        public static int RequiredClassSize { get { return 10; } }

        public static AgeLengthKeyType SelectedAgeLengthKeyType
        {
            get
            {
                object o = UserSetting.GetValue(Path, nameof(SelectedAgeLengthKeyType), null);
                if (o == null) return AgeLengthKeyType.Raw;
                else return (AgeLengthKeyType)(int)o;
            }
            set { UserSetting.SetValue(Path, nameof(SelectedAgeLengthKeyType), (int)value); }
        }
    }

    public abstract class UserSettingPaths
    {
        //public static string DefaultCatchability = "DefaultCatchability";

        //public static string Catchability = "Catchability";

        //public static string GamingAge = "GamingAge";

        //public static string GamingLength = "GamingLength";

        //public static string GearClass = "GearClass";

        //public static string SizeInterval = "SizeInterval";

        //public static string NaturalMortality = "NaturalMortality";

        //public static string FishingMortality = "FishingMortality";
    }
}
