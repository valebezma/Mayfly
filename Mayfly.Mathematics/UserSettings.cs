using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using Mayfly.Mathematics.Statistics;
using System.Collections.Generic;
using Mayfly.Extensions;
using System.IO;
using static Mayfly.UserSettings;

namespace Mayfly.Mathematics
{
    public static class UserSettings
    {
        public static string Path
        {
            get
            {
                return GetFeatureKey("Mathematics");
            }
        }

        public static int SampleAppearance
        {
            get { return (int)GetValue(Path, nameof(SampleAppearance), 0); }
            set { SetValue(Path, nameof(SampleAppearance), value); }
        }

        public static Color ColorSelected
        {
            get { return Color.FromArgb((int)GetValue(Path, nameof(ColorSelected), Color.DarkSalmon.ToArgb())); }
            set { SetValue(Path, nameof(ColorSelected), value.ToArgb()); }
        }

        public static Color ColorAccent
        {
            get { return Color.FromArgb((int)GetValue(Path, nameof(ColorAccent), Color.RoyalBlue.ToArgb())); }
            set { SetValue(Path, nameof(ColorAccent), value.ToArgb()); }
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
            get { return (double)((int)GetValue(Path, nameof(DefaultAlpha), 50)) / 1000; }
            set { SetValue(Path, nameof(DefaultAlpha), (int)(value * 1000)); }
        }

        public static int RequiredSampleSize
        {
            get { return (int)GetValue(Path, nameof(RequiredSampleSize), 5); }
            set { SetValue(Path, nameof(RequiredSampleSize), value); }
        }

        public static int NormalityTest
        {
            get { return (int)GetValue(Path, nameof(NormalityTest), 0); }
            set { SetValue(Path, nameof(NormalityTest), value); }
        }

        public static string NormalityTestName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (NormalityTest)
                {
                    case 0:
                        return resources.GetString("comboBoxNormality.Items");
                    default:
                        return resources.GetString("comboBoxNormality.Items" + NormalityTest);
                }
            }
        }

        public static int HomogeneityTest
        {
            get { return (int)GetValue(Path, nameof(HomogeneityTest), 1); }
            set { SetValue(Path, nameof(HomogeneityTest), value); }
        }

        public static string HomogeneityTestName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (HomogeneityTest)
                {
                    case 0:
                        return resources.GetString("comboBoxHomogeneity.Items");
                    default:
                        return resources.GetString("comboBoxHomogeneity.Items" + HomogeneityTest);
                }
            }
        }

        public static string LsdIndexName
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Settings));
                switch (LsdIndex)
                {
                    case 0:
                        return resources.GetString("comboBoxLSD.Items");
                    default:
                        return resources.GetString("comboBoxLSD.Items" + HomogeneityTest);
                }
            }
        }

        public static int LsdIndex
        {
            get { return (int)GetValue(Path, nameof(LsdIndex), 0); }
            set { SetValue(Path, nameof(LsdIndex), value); }
        }
    }
}
