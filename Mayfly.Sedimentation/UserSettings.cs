using System;
using System.Windows.Forms;
using System.Reflection;

namespace Mayfly.Sedimentation
{
    public abstract class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Sedimentation");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".mud");

        public static double WaterDensity
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(WaterDensity), 1000) / 1000; }
            set { UserSetting.SetValue(Path, nameof(WaterDensity), (int)(value * 1000)); }
        }

        public static double SolidShape
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(SolidShape), 800) / 1000; }
            set { UserSetting.SetValue(Path, nameof(SolidShape), (int)(value * 1000)); }
        }

        public static double Gravity
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(Gravity), 9800) / 1000; }
            set { UserSetting.SetValue(Path, nameof(Gravity), (int)(value * 1000)); }
        }

        public static double DensityValue
        {
            get { return (SolidDensity - WaterDensity) / WaterDensity; }
        }

        public static double SolidDensity
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(SolidDensity), 2650) / 1000; }
            set { UserSetting.SetValue(Path, nameof(SolidDensity), (int)(value * 1000)); }
        }

        public static GrainSizeType SizeSource
        {
            get { return (GrainSizeType)(int)UserSetting.GetValue(Path, nameof(SizeSource), 0); }
            set { UserSetting.SetValue(Path, nameof(SizeSource), (int)value); }
        }

        public static double ControlSize
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(ControlSize), 50) / 1000; }
            set { UserSetting.SetValue(Path, nameof(ControlSize), (int)(value * 1000)); }
        }

        public static double ControlPart
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(ControlPart), 50) / 1000; }
            set { UserSetting.SetValue(Path, nameof(ControlPart), (int)(value * 1000)); }
        }

        public static double CriticalSediment
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(CriticalSediment), 50); }
            set { UserSetting.SetValue(Path, nameof(CriticalSediment), (int)value); }
        }

        public static double CriticalLoad
        {
            get { return (double)(int)UserSetting.GetValue(Path, nameof(CriticalLoad), 100); }
            set { UserSetting.SetValue(Path, nameof(CriticalLoad), (int)value); }
        }
    }

    public enum GrainSizeType
    {
        Lower,
        Moda,
        Upper
    };
}
