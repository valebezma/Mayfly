using System;
using static Mayfly.UserSettings;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public static class SettingsExplorer
    {
        public static FileSystemInterface Interface;

        public static void SetFeature(string feature, string ext,
            MassDegree logMassDegree, MassDegree indMassDegree) {

            SettingsReader.SetFeature(feature, ext, logMassDegree, indMassDegree);
            Feature = feature + ".Explorer";
            Interface = new FileSystemInterface(ext + "s", ".html") {
                FolderPath = UserSettings.FieldDataFolder
            };
        }

        public static bool AutoLoadBio {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(AutoLoadBio), false));
            }

            set {
                SetValue(FeatureKey, nameof(AutoLoadBio), value);
            }
        }

        public static bool KeepWizard {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(KeepWizard), false));
            }
            set {
                SetValue(FeatureKey, nameof(KeepWizard), value);
            }
        }

        public static ArtifactCriticality ReportCriticality {
            get {
                object o = GetValue(FeatureKey, nameof(ReportCriticality), ArtifactCriticality.Allowed);
                if (o == null) return ArtifactCriticality.Critical;
                else return (ArtifactCriticality)(int)o;
            }
            set { SetValue(FeatureKey, nameof(ReportCriticality), (int)value); }
        }

        public static bool CheckConsistency {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(CheckConsistency), true));
            }
            set {
                SetValue(FeatureKey, nameof(CheckConsistency), value);
            }
        }

        public static string[] Bios {
            get {
                string[] values = (string[])GetValue(FeatureKey, nameof(Bios), new string[0]);
                return values.GetOperableFilenames(UserSettings.InterfaceBio.Extension);
            }

            set {
                SetValue(FeatureKey, nameof(Bios), value);
            }
        }
    }
}
