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
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(MassRecoveryUseRaw), true));
            }
            set {
                SetValue(FeatureKey, nameof(MassRecoveryUseRaw), value);
            }
        }
    }
}
