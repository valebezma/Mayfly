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

        public static void Initialize()
        {
            UserSetting.InitializeRegistry(Path, Assembly.GetCallingAssembly(),
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.Gravity, 9800),
                    new UserSetting(UserSettingPaths.WaterDensity, 1000),
                    new UserSetting(UserSettingPaths.SolidShape, 800),
                    new UserSetting(UserSettingPaths.SolidDensity, 2650),
                    new UserSetting(UserSettingPaths.LaminarK, 220),
                    new UserSetting(UserSettingPaths.SizeSource, 0),
                    new UserSetting(UserSettingPaths.ControlSize, 50),
                    new UserSetting(UserSettingPaths.ControlPart, 50),
                    new UserSetting(UserSettingPaths.CriticalLoad, 100),
                    new UserSetting(UserSettingPaths.CriticalSediment, 50),
                });
        }

        public static object GetValue(string path, string key)
        {
            if (UserSetting.InitializationRequired(Path,
                Assembly.GetCallingAssembly()))
            {
                Initialize();
            }

            return UserSetting.GetValue(path, key);
        }

        public static FileSystemInterface Interface = new FileSystemInterface(".mud");

        public static double WaterDensity
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.WaterDensity) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.WaterDensity, (int)(value * 1000)); }
        }

        public static double SolidShape
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.SolidShape) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.SolidShape, (int)(value * 1000)); }
        }

        public static double Gravity
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.Gravity) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.Gravity, (int)(value * 1000)); }
        }

        public static double DensityValue
        {
            get { return (SolidDensity - WaterDensity) / WaterDensity; }
        }

        public static double SolidDensity
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.SolidDensity) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.SolidDensity, (int)(value * 1000)); }
        }

        public static GrainSizeType SizeSource
        {
            get { return (GrainSizeType)(int)GetValue(Path, UserSettingPaths.SizeSource); }
            set { UserSetting.SetValue(Path, UserSettingPaths.SizeSource, (int)value); }
        }

        public static double ControlSize
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.ControlSize) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.ControlSize, (int)(value * 1000)); }
        }

        public static double ControlPart
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.ControlPart) / 1000; }
            set { UserSetting.SetValue(Path, UserSettingPaths.ControlPart, (int)(value * 1000)); }
        }

        public static double CriticalSediment
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.CriticalSediment); }
            set { UserSetting.SetValue(Path, UserSettingPaths.CriticalSediment, (int)value); }
        }

        public static double CriticalLoad
        {
            get { return (double)(int)GetValue(Path, UserSettingPaths.CriticalLoad); }
            set { UserSetting.SetValue(Path, UserSettingPaths.CriticalLoad, (int)value); }
        }


    }

    public abstract class UserSettingPaths
    {
        public static string CriticalSediment = "CriticalSediment";

        public static string CriticalLoad = "CriticalLoad";

        public static string WaterDensity = "WaterDensity";

        public static string SolidDensity = "SolidDensity";

        public static string Gravity = "Gravity";

        public static string SolidShape = "SolidShape";

        public static string LaminarK = "LaminarK";

        public static string SizeSource = "SizeSource";

        public static string ControlSize = "ControlSize";

        public static string ControlPart = "ControlPart";
    }

    public enum GrainSizeType
    {
        Lower,
        Moda,
        Upper
    };
}
