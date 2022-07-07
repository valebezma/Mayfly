using System;
using System.Windows.Forms;
using System.Reflection;
using static Mayfly.UserSettings;

namespace Mayfly.Sedimentation
{
    public static class UserSettings
    {
        public static string Path
        {
            get
            {
                return GetFeatureKey("Sedimentation");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".mud");

        public static double WaterDensity
        {
            get { return (double)(int)GetValue(Path, nameof(WaterDensity), 1000) / 1000; }
            set { SetValue(Path, nameof(WaterDensity), (int)(value * 1000)); }
        }

        public static double SolidShape
        {
            get { return (double)(int)GetValue(Path, nameof(SolidShape), 800) / 1000; }
            set { SetValue(Path, nameof(SolidShape), (int)(value * 1000)); }
        }

        public static double Gravity
        {
            get { return (double)(int)GetValue(Path, nameof(Gravity), 9800) / 1000; }
            set { SetValue(Path, nameof(Gravity), (int)(value * 1000)); }
        }

        public static double DensityValue
        {
            get { return (SolidDensity - WaterDensity) / WaterDensity; }
        }

        public static double SolidDensity
        {
            get { return (double)(int)GetValue(Path, nameof(SolidDensity), 2650) / 1000; }
            set { SetValue(Path, nameof(SolidDensity), (int)(value * 1000)); }
        }

        public static GrainSizeType SizeSource
        {
            get { return (GrainSizeType)(int)GetValue(Path, nameof(SizeSource), 0); }
            set { SetValue(Path, nameof(SizeSource), (int)value); }
        }

        public static double ControlSize
        {
            get { return (double)(int)GetValue(Path, nameof(ControlSize), 50) / 1000; }
            set { SetValue(Path, nameof(ControlSize), (int)(value * 1000)); }
        }

        public static double ControlPart
        {
            get { return (double)(int)GetValue(Path, nameof(ControlPart), 50) / 1000; }
            set { SetValue(Path, nameof(ControlPart), (int)(value * 1000)); }
        }

        public static double CriticalSediment
        {
            get { return (double)(int)GetValue(Path, nameof(CriticalSediment), 50); }
            set { SetValue(Path, nameof(CriticalSediment), (int)value); }
        }

        public static double CriticalLoad
        {
            get { return (double)(int)GetValue(Path, nameof(CriticalLoad), 100); }
            set { SetValue(Path, nameof(CriticalLoad), (int)value); }
        }
    }

    public enum GrainSizeType
    {
        Lower,
        Moda,
        Upper
    };
}
