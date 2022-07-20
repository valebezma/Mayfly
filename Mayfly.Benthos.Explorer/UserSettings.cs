using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System;
using Mayfly.Extensions;
using Mayfly.Wild;
using static Mayfly.UserSettings;

namespace Mayfly.Benthos.Explorer
{
    public static class UserSettings
    {
        public static bool MassRecoveryUseRaw {
            get {
                return Convert.ToBoolean(GetValue(SettingsExplorer.FeatureKey, nameof(MassRecoveryUseRaw), true));
            }
            set {
                SetValue(SettingsExplorer.FeatureKey, nameof(MassRecoveryUseRaw), value);
            }
        }
    }
}
