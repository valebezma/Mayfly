using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using Mayfly.Mathematics.Statistics;
using System.Collections.Generic;
using Mayfly.Extensions;
using System.IO;

namespace Mayfly.Mathematics
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Mathematics");
            }
        }

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.SampleAppearance, 0),
                    new UserSetting(UserSettingPaths.ColorAccent, Color.RoyalBlue.ToArgb()),
                    new UserSetting(UserSettingPaths.ColorSelected, Color.DarkSalmon.ToArgb()),
                    new UserSetting(UserSettingPaths.Alpha, 50),
                    new UserSetting(UserSettingPaths.ConfidenceLevel, 950),
                    new UserSetting(UserSettingPaths.RequiredSampleSize, 5, true),
                    new UserSetting(UserSettingPaths.RunOutDistance, 250),
                    new UserSetting(UserSettingPaths.DefaultTrendType, 2),
                    new UserSetting(UserSettingPaths.NormalityTest, 0),
                    new UserSetting(UserSettingPaths.DiffSignificance, 0),
                    new UserSetting(UserSettingPaths.HomogeneityTest, 1),
                    new UserSetting(UserSettingPaths.DefaultTrendType, (int)TrendType.Power)
                });
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path, Assembly.GetCallingAssembly()))
            {
                Initialize();
            }
            return UserSetting.GetValue(path, key);
        }

        //public static string[] SaveExtensions
        //{
        //    get
        //    {
        //        return new string[] { ".csv", ".prn", ".txt" };
        //    }
        //}

        //public static string[] OpenExtensions
        //{
        //    get
        //    {
        //        return new string[] { ".csv", ".txt" };
        //    }
        //}

        //private static OpenFileDialog openDialog;

        //public static OpenFileDialog OpenDialog
        //{
        //    get
        //    {
        //        if (openDialog == null)
        //        {
        //            openDialog = FileSystem.OpenDialog(Resources.Info.Open, OpenExtensions);
        //        }
        //        return openDialog;
        //    }
        //}

        //private static SaveFileDialog saveDialog;

        //public static SaveFileDialog SaveDialog
        //{
        //    get
        //    {
        //        if (saveDialog == null)
        //        {
        //            saveDialog = FileSystem.SaveDialog(Resources.Info.Save, SaveExtensions);
        //            saveDialog.FileOk += new System.ComponentModel.CancelEventHandler(saveDialog_FileOk);
        //        }
        //        return saveDialog;
        //    }
        //}

        //static void saveDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    SaveDialog.InitialDirectory = FileSystem.FolderName(((SaveFileDialog)sender).FileName);
        //}


        public static int SampleAppearance
        {
            get { return (int)GetValue(Path, UserSettingPaths.SampleAppearance); }
            set { UserSetting.SetValue(Path, UserSettingPaths.SampleAppearance, value); }
        }

        public static Color ColorSelected
        {
            get { return Color.FromArgb((int)GetValue(Path, UserSettingPaths.ColorSelected)); }
            set { UserSetting.SetValue(Path, UserSettingPaths.ColorSelected, value.ToArgb()); }
        }

        public static Color ColorAccent
        {
            get { return Color.FromArgb((int)GetValue(Path, UserSettingPaths.ColorAccent)); }
            set { UserSetting.SetValue(Path, UserSettingPaths.ColorAccent, value.ToArgb()); }
        }

        static Color[] colors;

        public static Color[] Pallette
        {
            get
            {
                if (colors == null)
                {
                    List<Color> result = new List<Color>();

                    foreach (string hex in File.ReadAllLines(System.IO.Path.Combine(Application.StartupPath, @"interface\pallette.ini")))
                    {
                        result.Add(ColorTranslator.FromHtml(hex));
                    }

                    colors = result.ToArray();
                }

                return colors;
            }
        }
        


        public static double DefaultAlpha
        {
            get { return (double)((int)GetValue(Path, UserSettingPaths.Alpha)) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.Alpha, (int)(value * 1000)); }
        }


        public static int RequiredSampleSize
        {
            get { return (int)GetValue(Path, UserSettingPaths.RequiredSampleSize); }
            set { UserSetting.SetValue(Path, UserSettingPaths.RequiredSampleSize, value); }
        }
        

        public static int NormalityTest
        {
            get { return (int)GetValue(Path, UserSettingPaths.NormalityTest); }
            set { UserSetting.SetValue(Path, UserSettingPaths.NormalityTest, value); }
        }

        public static string NormalityTestName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (UserSettings.NormalityTest)
                {
                    case 0:
                        return resources.GetString("comboBoxNormality.Items");
                    default:
                        return resources.GetString("comboBoxNormality.Items" + UserSettings.NormalityTest);
                }
            }
        }

        public static int HomogeneityTest
        {
            get { return (int)GetValue(Path, UserSettingPaths.HomogeneityTest); }
            set { UserSetting.SetValue(Path, UserSettingPaths.HomogeneityTest, value); }
        }

        public static string HomogeneityTestName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (UserSettings.HomogeneityTest)
                {
                    case 0:
                        return resources.GetString("comboBoxHomogeneity.Items");
                    default:
                        return resources.GetString("comboBoxHomogeneity.Items" + UserSettings.HomogeneityTest);
                }
            }
        }

        public static string LsdIndexName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (UserSettings.LsdIndex)
                {
                    case 0:
                        return resources.GetString("comboBoxLSD.Items");
                    default:
                        return resources.GetString("comboBoxLSD.Items" + UserSettings.HomogeneityTest);
                }
            }
        }

        public static int LsdIndex
        {
            get { return (int)GetValue(Path, UserSettingPaths.DiffSignificance); }
            set { UserSetting.SetValue(Path, UserSettingPaths.DiffSignificance, value); }
        }
    }

    public abstract class UserSettingPaths
    {
        public static string SampleAppearance = "SampleAppearance";

        public static string ColorSelected = "ColorSelected";

        public static string ColorAccent = "ColorAccent";


        public static string Alpha = "Alpha";

        public static string ConfidenceLevel = "ConfidenceLevel";


        public static string RequiredSampleSize = "RequiredSampleSize";


        public static string RunOutDistance = "RunOutDistance";

        public static string DefaultTrendType = "DefaultTrendType";

        public static string DefaultGrowthType = "DefaultGrowthType";


        public static string NormalityTest = "NormalityTest";

        public static string HomogeneityTest = "HomogeneityTest";

        public static string DiffSignificance = "DiffSignificance";

    }
}
