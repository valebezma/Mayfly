using Mayfly.Extensions;
using Mayfly.Waters;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Mayfly.Species;

namespace Mayfly.Wild
{
    public abstract class UserSettings
    {
        private static string type = "Wild";
        private static FileSystemInterface _interface = new FileSystemInterface(new string[] { ".fcd", ".bcd", ".pcd" }, new string[] { ".html" });

        public static string ObjectType {
            get {
                return type;
            }
            protected set {
                type = value;
            }
        }

        public static string Path {
            get {
                return UserSetting.GetFeatureKey("Mayfly." + ObjectType);
            }
        }

        public static FileSystemInterface Interface  {
            get {
                return _interface;
            }
            protected set {
                _interface = value;
            }
        }



        protected static void setGlobalValues(string _type, string ext) {
            type = _type; 
            _interface = new FileSystemInterface(FieldDataFolder, ext, ".html");
        }


        //interfa

        public static FileSystemInterface InterfaceBio = new FileSystemInterface(".bio");

        public static string FieldDataFolder {
            get {
                string result = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "FieldData");

                if (!Directory.Exists(result)) {
                    Directory.CreateDirectory(result).Attributes = FileAttributes.System;

                    string desktopIniPath = System.IO.Path.Combine(result, "desktop.ini");
                    File.Create(desktopIniPath).Close();
                    File.WriteAllLines(desktopIniPath, new string[] {
                            "[.ShellClassInfo]",
                            string.Format("LocalizedResourceName={0}", Resources.Interface.Interface.FieldFolder),
                            string.Format("InfoTip={0}", Resources.Interface.Interface.FieldFolderTip),
                            string.Format(@"IconResource={0},0", Assembly.GetExecutingAssembly().Location) },
                            Encoding.Default);
                    File.SetAttributes(desktopIniPath, FileAttributes.Hidden);

                    Directory.CreateDirectory(result);
                }

                return result;
            }
        }



        public static DiversityIndex Diversity {
            get { return (DiversityIndex)(int)UserSetting.GetValue(Path, nameof(Diversity), (int)DiversityIndex.D1963_Shannon); }
            set { UserSetting.SetValue(Path, nameof(Diversity), (int)value); }
        }

        public static int Dominance {
            get { return (int)UserSetting.GetValue(Path, nameof(Dominance), 0); }
            set { UserSetting.SetValue(Path, nameof(Dominance), value); }
        }

        public static string WatersIndexPath {
            get {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(WatersIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath)) {
                    WatersIndexPath = Service.GetReferencePath(Waters.UserSettings.Interface.OpenDialog,
                      "Waters (auto).wtr", Server.GetUri("get/index/waters/waters_default.wtr", Application.CurrentCulture));
                    return WatersIndexPath;
                } else {
                    return filepath;
                }
            }
            set {
                UserSetting.SetValue(Path, nameof(WatersIndexPath), value);
            }
        }

        private static WatersKey watersIndex;

        public static WatersKey WatersIndex {
            get {
                if (watersIndex == null) {
                    watersIndex = new WatersKey();

                    try { watersIndex.ReadXml(WatersIndexPath); } catch { Log.Write(EventType.Maintenance, "First call for {0}. File is empty and will be rewritten.", WatersIndexPath); }
                }

                return watersIndex;
            }

            set {
                watersIndex = value;
            }
        }

        private static WeatherEvents weatherIndex;

        public static WeatherEvents WeatherIndex {
            get {
                if (weatherIndex == null) {
                    weatherIndex = new WeatherEvents();
                    weatherIndex.SetAttributable();
                    try { weatherIndex.ReadXml(System.IO.Path.Combine(Application.StartupPath, @"interface\weatherevts.ini")); } catch { Log.Write(EventType.Maintenance, "Can't read weather file."); }
                }

                return weatherIndex;
            }
        }



        protected static Samplers samplersIndex;

        public static Samplers SamplersIndex {
            get {
                if (samplersIndex == null) {
                    samplersIndex = new Samplers();
                    samplersIndex.SetAttributable();
                    samplersIndex.ReadXml(string.Format(@"interface\sampler{0}.ini", ObjectType.ToLowerInvariant()));
                }
                return samplersIndex;
            }
        }

        public static Samplers.SamplerRow SelectedSampler {
            get {
                return SamplersIndex.Sampler.FindByID((int)UserSetting.GetValue(Path, nameof(SelectedSampler), 7));
            }

            set {
                UserSetting.SetValue(Path, nameof(SelectedSampler), value.ID);
            }
        }



        public static string TaxonomicIndexPath {
            get {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(TaxonomicIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath)) {
                    TaxonomicIndexPath = Service.GetReferencePathSpecies(ObjectType);
                    return TaxonomicIndexPath;
                } else {
                    return filepath;
                }
            }
            set {
                UserSetting.SetValue(Path, nameof(TaxonomicIndexPath), value);
            }
        }

        protected static TaxonomicIndex taxonomicIndex;

        public static TaxonomicIndex TaxonomicIndex {
            get {
                if (taxonomicIndex == null) {
                    taxonomicIndex = new TaxonomicIndex();

                    if (TaxonomicIndexPath != null) {
                        taxonomicIndex.Read(TaxonomicIndexPath);
                    }
                }

                return taxonomicIndex;
            }

            set {
                taxonomicIndex = value;
            }
        }

        public static bool SpeciesAutoExpand {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(SpeciesAutoExpand), true)); }
            set { UserSetting.SetValue(Path, nameof(SpeciesAutoExpand), value); }
        }

        public static bool SpeciesAutoExpandVisual {
            get { return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(SpeciesAutoExpandVisual), true)); }
            set { UserSetting.SetValue(Path, nameof(SpeciesAutoExpandVisual), value); }
        }

        public static int RecentSpeciesCount {
            get { return (int)UserSetting.GetValue(Path, nameof(RecentSpeciesCount), 15); }
            set { UserSetting.SetValue(Path, nameof(RecentSpeciesCount), value); }
        }



        public static int SelectedWaterID {
            get {
                return (int)UserSetting.GetValue(Path, nameof(SelectedWaterID), 0);
            }

            set {
                UserSetting.SetValue(Path, nameof(SelectedWaterID), value);
            }
        }

        public static DateTime SelectedDate {
            get {
                object SavedDate = UserSetting.GetValue(Path, nameof(SelectedDate), DateTime.Today);
                if (SavedDate == null) return DateTime.Now.AddSeconds(-DateTime.Now.Second);
                else return Convert.ToDateTime(SavedDate);
            }
            set { UserSetting.SetValue(Path, nameof(SelectedDate), value.ToShortDateString()); }
        }

        public static string[] AddtFactors {
            get { return (string[])UserSetting.GetValue(Path, nameof(AddtFactors), new string[0]); }
            set { UserSetting.SetValue(Path, nameof(AddtFactors), value); }
        }



        public static bool AutoLogOpen {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoLogOpen), false));
            }

            set {
                UserSetting.SetValue(Path, nameof(AutoLogOpen), value);
            }
        }

        public static bool FixTotals {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(FixTotals), false));
            }

            set {
                UserSetting.SetValue(Path, nameof(FixTotals), value);
            }
        }

        public static bool AutoIncreaseBio {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoIncreaseBio), true));
            }

            set {
                UserSetting.SetValue(Path, nameof(AutoIncreaseBio), value);
            }
        }

        public static bool AutoDecreaseBio {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(AutoDecreaseBio), true));
            }

            set {
                UserSetting.SetValue(Path, nameof(AutoDecreaseBio), value);
            }
        }

        public static string[] AddtVariables {
            get {
                return (string[])UserSetting.GetValue(Path, nameof(AddtVariables), new string[0]);
            }

            set {
                UserSetting.SetValue(Path, nameof(AddtVariables), value);
            }
        }

        public static string[] CurrentVariables {
            get {
                return (string[])UserSetting.GetValue(Path, nameof(CurrentVariables), new string[0]);
            }

            set {
                UserSetting.SetValue(Path, nameof(CurrentVariables), value);
            }
        }



        public static LogOrder LogOrder {
            get { return (LogOrder)(int)UserSetting.GetValue(Path, nameof(LogOrder), LogOrder.Alphabetically); }
            set { UserSetting.SetValue(Path, nameof(LogOrder), (int)value); }
        }

        public static bool BreakBeforeIndividuals {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(BreakBeforeIndividuals), true));
            }

            set {
                UserSetting.SetValue(Path, nameof(BreakBeforeIndividuals), value);
            }
        }

        public static bool BreakBetweenSpecies {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(BreakBetweenSpecies), false));
            }

            set {
                UserSetting.SetValue(Path, nameof(BreakBetweenSpecies), value);
            }
        }

        public static bool OddCardStart {
            get {
                return Convert.ToBoolean(UserSetting.GetValue(Path, nameof(OddCardStart), true));
            }

            set {
                UserSetting.SetValue(Path, nameof(OddCardStart), value);
            }
        }
    }

    public enum LogOrder
    {
        AsInput = -1,
        Alphabetically = 0,
        ByQuantity = 1,
        ByMass = 2,
        Philogenetically = 3
    }
}
