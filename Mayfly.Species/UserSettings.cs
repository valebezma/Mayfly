using System;
using System.Drawing;

namespace Mayfly.Species
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Species");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(null, ".sps", ".html");

        public static bool UseClassicKeyReport 
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(UseClassicKeyReport), false)); }
            set { UserSetting.SetValue(Path, nameof(UseClassicKeyReport), value); }
        }

        public static string CoupletChar 
        {
            get { return (string)UserSetting.GetValue(Path, nameof(CoupletChar), "'"); }
            set { UserSetting.SetValue(Path, nameof(CoupletChar), value); }
        }

        public static int AllowableSpeciesListLength 
        {
            get { return (int)UserSetting.GetValue(Path, nameof(AllowableSpeciesListLength), 50); }
            set { UserSetting.SetValue(Path, nameof(AllowableSpeciesListLength), value); }
        }

        public static string HigherTaxonNameFormat
        {
            get { return (string)UserSetting.GetValue(Path, nameof(HigherTaxonNameFormat), "T"); }
            set { UserSetting.SetValue(Path, nameof(HigherTaxonNameFormat), value); }
        }

        public static string LowerTaxonNameFormat
        {
            get { return (string)UserSetting.GetValue(Path, nameof(LowerTaxonNameFormat), "F"); }
            set { UserSetting.SetValue(Path, nameof(LowerTaxonNameFormat), value); }
        }

        public static Color LowerTaxonColor
        {
            get { return Color.FromArgb((int)UserSetting.GetValue(Path, nameof(LowerTaxonColor), Color.Teal.ToArgb())); }
            set { UserSetting.SetValue(Path, nameof(LowerTaxonColor), value.ToArgb()); }
        }

        public static bool FillTreeWithLowerTaxon
        {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(FillTreeWithLowerTaxon), false)); }
            set { UserSetting.SetValue(Path, nameof(FillTreeWithLowerTaxon), value); }
        }
    }
}
