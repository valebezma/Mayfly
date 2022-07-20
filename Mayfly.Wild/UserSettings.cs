using Mayfly.Extensions;
using Mayfly.Waters;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Mayfly.Species;
using Mayfly;
using static Mayfly.UserSettings;

namespace Mayfly.Wild
{
    public static class UserSettings
    {
        public static string FeatureKey {
            get {
                return GetFeatureKey("Wild");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(new string[] { ".fcd", ".bcd", ".pcd" }, new string[] { ".html" }) {
            FolderPath = FieldDataFolder
        };

        public static FileSystemInterface InterfaceBio = new FileSystemInterface(".bio") {
            FolderPath = FieldDataFolder
        };

        public static string FieldDataFolder {
            get {
                string result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "FieldData");

                if (!Directory.Exists(result)) {
                    Directory.CreateDirectory(result).Attributes = FileAttributes.System;

                    string desktopIniFeatureKey = Path.Combine(result, "desktop.ini");
                    File.Create(desktopIniFeatureKey).Close();
                    File.WriteAllLines(desktopIniFeatureKey, new string[] {
                            "[.ShellClassInfo]",
                            string.Format("LocalizedResourceName={0}", Wild.Resources.Interface.Interface.FieldFolder),
                            string.Format("InfoTip={0}", Wild.Resources.Interface.Interface.FieldFolderTip),
                            string.Format(@"IconResource={0},0", Assembly.GetExecutingAssembly().Location) },
                            Encoding.Default);
                    File.SetAttributes(desktopIniFeatureKey, FileAttributes.Hidden);

                    Directory.CreateDirectory(result);
                }

                return result;
            }
        }



        public static DiversityIndex Diversity {
            get { return (DiversityIndex)(int)GetValue(FeatureKey, nameof(Diversity), (int)DiversityIndex.D1963_Shannon); }
            set { SetValue(FeatureKey, nameof(Diversity), (int)value); }
        }

        public static int Dominance {
            get { return (int)GetValue(FeatureKey, nameof(Dominance), 0); }
            set { SetValue(FeatureKey, nameof(Dominance), value); }
        }

        //public static string DominanceIndexName
        //{
        //    get
        //    {
        //        ResourceManager resources = new ResourceManager(typeof(Settings));
        //        switch (Dominance)
        //        {
        //            case 0:
        //                return resources.GetString("comboBoxDominance.Items");
        //            default:
        //                return resources.GetString("comboBoxDominance.Items" + Wild.Dominance);
        //        }
        //    }
        //}

        private static WeatherEvents weatherIndex;

        public static WeatherEvents WeatherIndex {
            get {
                if (weatherIndex == null) {
                    weatherIndex = new WeatherEvents();
                    weatherIndex.SetAttributable();
                    try { weatherIndex.ReadXml(Path.Combine(Application.StartupPath, @"interface\weatherevts.ini")); } catch { Log.Write(EventType.Maintenance, "Can't read weather file."); }
                }

                return weatherIndex;
            }
        }



        public static string WatersIndexPath {
            get {
                string filepath = IO.GetPath(GetValue(FeatureKey, nameof(WatersIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath)) {
                    WatersIndexPath = IO.GetIndexPath(Interface.OpenDialog,
                      "Waters (auto).wtr", Server.GetUri("get/index/waters/waters_default.wtr", Application.CurrentCulture));
                    return WatersIndexPath;
                } else {
                    return filepath;
                }
            }
            set {
                SetValue(FeatureKey, nameof(WatersIndexPath), value);
            }
        }

        private static WatersKey watersIndex;

        public static WatersKey WatersIndex {
            get {
                if (watersIndex == null) {
                    watersIndex = new WatersKey();

                    try {
                        watersIndex.ReadXml(WatersIndexPath);
                    } catch {
                        Log.Write(EventType.Maintenance, "First call for {0}. File is empty and will be rewritten.", WatersIndexPath);
                    }
                }

                return watersIndex;
            }

            set {
                watersIndex = value;
            }
        }



        public static int SelectedWaterID {
            get {
                return (int)GetValue(FeatureKey, nameof(SelectedWaterID), 0);
            }

            set {
                SetValue(FeatureKey, nameof(SelectedWaterID), value);
            }
        }

        public static DateTime SelectedDate {
            get {
                object SavedDate = GetValue(FeatureKey, nameof(SelectedDate), DateTime.Today);
                if (SavedDate == null) return DateTime.Now.AddSeconds(-DateTime.Now.Second);
                else return Convert.ToDateTime(SavedDate);
            }
            set { SetValue(FeatureKey, nameof(SelectedDate), value.ToShortDateString()); }
        }

        public static string[] AddtFactors {
            get { return (string[])GetValue(FeatureKey, nameof(AddtFactors), new string[0]); }
            set { SetValue(FeatureKey, nameof(AddtFactors), value); }
        }
    }
}
