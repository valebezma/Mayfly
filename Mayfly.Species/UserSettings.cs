using System;
using System.Drawing;
using static Mayfly.UserSettings;

namespace Mayfly.Species
{
    public static class UserSettings
    {
        public static string FeatureKey {
            get {
                return GetFeatureKey("Species");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".txn", ".html");

        public static bool UseClassicKeyReport 
        {
            get { return Convert.ToBoolean(GetValue(FeatureKey, nameof(UseClassicKeyReport), false)); }
            set { SetValue(FeatureKey, nameof(UseClassicKeyReport), value); }
        }

        public static string CoupletChar 
        {
            get { return (string)GetValue(FeatureKey, nameof(CoupletChar), "'"); }
            set { SetValue(FeatureKey, nameof(CoupletChar), value); }
        }

        public static int AllowableSpeciesListLength 
        {
            get { return (int)GetValue(FeatureKey, nameof(AllowableSpeciesListLength), 50); }
            set { SetValue(FeatureKey, nameof(AllowableSpeciesListLength), value); }
        }

        public static string HigherTaxonNameFormat
        {
            get { return (string)GetValue(FeatureKey, nameof(HigherTaxonNameFormat), "T"); }
            set { SetValue(FeatureKey, nameof(HigherTaxonNameFormat), value); }
        }

        public static string LowerTaxonNameFormat
        {
            get { return (string)GetValue(FeatureKey, nameof(LowerTaxonNameFormat), "F"); }
            set { SetValue(FeatureKey, nameof(LowerTaxonNameFormat), value); }
        }

        public static Color LowerTaxonColor
        {
            get { return Color.FromArgb((int)GetValue(FeatureKey, nameof(LowerTaxonColor), Color.Teal.ToArgb())); }
            set { SetValue(FeatureKey, nameof(LowerTaxonColor), value.ToArgb()); }
        }

        public static bool FillTreeWithLowerTaxon
        {
            get { return Convert.ToBoolean(GetValue(FeatureKey, nameof(FillTreeWithLowerTaxon), false)); }
            set { SetValue(FeatureKey, nameof(FillTreeWithLowerTaxon), value); }
        }
    }
}
