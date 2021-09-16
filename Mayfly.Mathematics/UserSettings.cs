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

        public static int SampleAppearance
        {
            get { return (int)UserSetting.GetValue(Path, nameof(SampleAppearance), 0); }
            set { UserSetting.SetValue(Path, nameof(SampleAppearance), value); }
        }

        public static Color ColorSelected
        {
            get { return Color.FromArgb((int)UserSetting.GetValue(Path, nameof(ColorSelected), Color.DarkSalmon.ToArgb())); }
            set { UserSetting.SetValue(Path, nameof(ColorSelected), value.ToArgb()); }
        }

        public static Color ColorAccent
        {
            get { return Color.FromArgb((int)UserSetting.GetValue(Path, nameof(ColorAccent), Color.RoyalBlue.ToArgb())); }
            set { UserSetting.SetValue(Path, nameof(ColorAccent), value.ToArgb()); }
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
            get { return (double)((int)UserSetting.GetValue(Path, nameof(DefaultAlpha), 50)) / 1000; }
            set { UserSetting.SetValue(Path, nameof(DefaultAlpha), (int)(value * 1000)); }
        }

        public static int RequiredSampleSize
        {
            get { return (int)UserSetting.GetValue(Path, nameof(RequiredSampleSize), 5); }
            set { UserSetting.SetValue(Path, nameof(RequiredSampleSize), value); }
        }

        public static int NormalityTest
        {
            get { return (int)UserSetting.GetValue(Path, nameof(NormalityTest), 0); }
            set { UserSetting.SetValue(Path, nameof(NormalityTest), value); }
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
            get { return (int)UserSetting.GetValue(Path, nameof(HomogeneityTest), 1); }
            set { UserSetting.SetValue(Path, nameof(HomogeneityTest), value); }
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
            get { return (int)UserSetting.GetValue(Path, nameof(LsdIndex), 0); }
            set { UserSetting.SetValue(Path, nameof(LsdIndex), value); }
        }
    }
}
