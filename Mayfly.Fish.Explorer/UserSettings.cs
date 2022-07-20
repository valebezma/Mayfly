using Mayfly.Extensions;
using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Wild;
using static Mayfly.UserSettings;

namespace Mayfly.Fish.Explorer
{
    public static class UserSettings
    {
        public static bool SuggestAge {
            get {
                return Convert.ToBoolean(GetValue(SettingsExplorer.FeatureKey, nameof(SuggestAge), true));
            }

            set {
                SetValue(SettingsExplorer.FeatureKey, nameof(SuggestAge), value);
            }
        }

        public static FishSamplerType MemorizedSamplerType {
            get {
                object o = GetValue(SettingsExplorer.FeatureKey, nameof(MemorizedSamplerType), null);
                if (o == null) return FishSamplerType.None;
                else return (FishSamplerType)(int)o;
            }
            set { SetValue(SettingsExplorer.FeatureKey, nameof(MemorizedSamplerType), (int)value); }
        }

        public static double MemorizedWaterArea {
            get {
                return (double)(int)GetValue(SettingsExplorer.FeatureKey, nameof(MemorizedWaterArea), 1);
            }
            set {
                SetValue(SettingsExplorer.FeatureKey, nameof(MemorizedWaterArea), (int)value);
            }
        }

        public static double MemorizedWaterDepth {
            get {
                return 0.01 * (double)(int)GetValue(SettingsExplorer.FeatureKey, nameof(MemorizedWaterDepth), 100);
            }
            set {
                SetValue(SettingsExplorer.FeatureKey, nameof(MemorizedWaterDepth), (int)value * 100);
            }
        }

        public static double DefaultCatchability {
            get {
                return (double)(int)GetValue(SettingsExplorer.FeatureKey, nameof(DefaultCatchability), 20) / 100;
            }
            set {
                SetValue(SettingsExplorer.FeatureKey, nameof(DefaultCatchability), (int)(value * 100));
            }
        }

        public static double SizeInterval {
            get {
                return (double)(int)GetValue(SettingsExplorer.FeatureKey, nameof(SizeInterval), 1000) / 100;
            }
            set {
                SetValue(SettingsExplorer.FeatureKey, nameof(SizeInterval), (int)(value * 100));
            }
        }

        public static int RequiredClassSize {
            get {
                return 10;
            }
        }

        public static AgeLengthKeyType SelectedAgeLengthKeyType {
            get {
                object o = GetValue(SettingsExplorer.FeatureKey, nameof(SelectedAgeLengthKeyType), null);
                if (o == null) return AgeLengthKeyType.Raw;
                else return (AgeLengthKeyType)(int)o;
            }
            set { SetValue(SettingsExplorer.FeatureKey, nameof(SelectedAgeLengthKeyType), (int)value); }
        }
    }
}
