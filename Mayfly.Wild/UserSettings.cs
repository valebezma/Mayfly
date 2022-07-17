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

    public static class ReaderSettings
    {
        public static string Feature;

        public static string FeatureKey {
            get {
                return GetFeatureKey(Feature);
            }
        }

        public static FileSystemInterface Interface;

        //public static string CardsPath {

        //    get {
        //        string filepath = IO.GetPath(GetValue(FeatureKey, nameof(CardsPath), string.Empty));

        //        if (string.IsNullOrWhiteSpace(filepath)) {
        //            return UserSettings.FieldDataFolder;
        //        } else {
        //            return filepath;
        //        }
        //    }

        //    set {
        //        SetValue(FeatureKey, nameof(CardsPath), value);
        //    }
        //}

        public static void SetFeature(string feature, string ext) {
            Feature = feature;
            Interface = new FileSystemInterface(ext, ".html") {
                FolderPath = UserSettings.FieldDataFolder
            };
        }



        private static Survey samplersIndex;

        public static Survey SamplersIndex {
            get {
                if (samplersIndex == null && !string.IsNullOrEmpty(Feature)) {
                    samplersIndex = new Survey();
                    samplersIndex.SetAttributable();
                    string path = string.Format(@"interface\sampler{0}.ini", Feature.ToLowerInvariant());
                    if (File.Exists(path)) {
                        samplersIndex.ReadXml(path);
                    }
                }
                return samplersIndex;
            }
        }

        public static Survey.SamplerRow SelectedSampler {
            get {
                return SamplersIndex.Sampler.FindByID((int)GetValue(FeatureKey, nameof(SelectedSampler), 7));
            }

            set {
                SetValue(FeatureKey, nameof(SelectedSampler), value.ID);
            }
        }


        private static Survey equiment;

        public static Survey Equipment {
            get {
                if (equiment == null) {
                    equiment = new Survey();
                    equiment.SetAttributable();
                    string path = Path.Combine(IO.UserFolder, string.Format(@"equipment{0}.ini", Feature.ToLowerInvariant()));
                    if (File.Exists(path)) {
                        equiment.ReadXml(path);
                    }
                }

                return equiment;
            }

            set {
                equiment = value;
            }
        }



        public static string TaxonomicIndexPath {

            get {
                string filepath = IO.GetPath(GetValue(FeatureKey, nameof(TaxonomicIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath)) {
                    TaxonomicIndexPath = Service.GetTaxonomicIndexPath(Feature);
                    return TaxonomicIndexPath;
                } else {
                    return filepath;
                }
            }

            set {
                SetValue(FeatureKey, nameof(TaxonomicIndexPath), value);
            }
        }

        private static TaxonomicIndex taxonomicIndex;

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
            get { return Convert.ToBoolean(GetValue(FeatureKey, nameof(SpeciesAutoExpand), true)); }
            set { SetValue(FeatureKey, nameof(SpeciesAutoExpand), value); }
        }

        public static bool SpeciesAutoExpandVisual {
            get { return Convert.ToBoolean(GetValue(FeatureKey, nameof(SpeciesAutoExpandVisual), true)); }
            set { SetValue(FeatureKey, nameof(SpeciesAutoExpandVisual), value); }
        }

        public static int RecentSpeciesCount {
            get { return (int)GetValue(FeatureKey, nameof(RecentSpeciesCount), 15); }
            set { SetValue(FeatureKey, nameof(RecentSpeciesCount), value); }
        }



        public static bool AutoLogOpen {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(AutoLogOpen), false));
            }

            set {
                SetValue(FeatureKey, nameof(AutoLogOpen), value);
            }
        }

        public static bool FixTotals {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(FixTotals), false));
            }

            set {
                SetValue(FeatureKey, nameof(FixTotals), value);
            }
        }

        public static bool AutoIncreaseBio {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(AutoIncreaseBio), true));
            }

            set {
                SetValue(FeatureKey, nameof(AutoIncreaseBio), value);
            }
        }

        public static bool AutoDecreaseBio {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(AutoDecreaseBio), true));
            }

            set {
                SetValue(FeatureKey, nameof(AutoDecreaseBio), value);
            }
        }

        public static string[] AddtVariables {
            get {
                return (string[])GetValue(FeatureKey, nameof(AddtVariables), new string[0]);
            }

            set {
                SetValue(FeatureKey, nameof(AddtVariables), value);
            }
        }

        public static string[] CurrentVariables {

            get {
                return (string[])GetValue(FeatureKey, nameof(CurrentVariables), new string[0]);
            }

            set {
                SetValue(FeatureKey, nameof(CurrentVariables), value);
            }
        }



        public static LogSortOrder LogOrder {
            get { return (LogSortOrder)(int)GetValue(FeatureKey, nameof(LogOrder), LogSortOrder.Alphabetically); }
            set { SetValue(FeatureKey, nameof(LogOrder), (int)value); }
        }

        public static bool BreakBeforeIndividuals {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(BreakBeforeIndividuals), true));
            }

            set {
                SetValue(FeatureKey, nameof(BreakBeforeIndividuals), value);
            }
        }

        public static bool BreakBetweenSpecies {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(BreakBetweenSpecies), false));
            }

            set {
                SetValue(FeatureKey, nameof(BreakBetweenSpecies), value);
            }
        }

        public static bool OddCardStart {
            get {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(OddCardStart), true));
            }

            set {
                SetValue(FeatureKey, nameof(OddCardStart), value);
            }
        }
    }

    public static class ExplorerSettings
    {
        public static string Feature;

        public static string FeatureKey {
            get {
                return GetFeatureKey(Feature);
            }
        }

        public static FileSystemInterface Interface;

        public static void SetFeature(string feature, string ext) {
            ReaderSettings.SetFeature(feature, ext);
            Feature = feature + ".Explorer";
            Interface = new FileSystemInterface(ext + "s", ".html") {
                FolderPath = UserSettings.FieldDataFolder
            };
        }

        public static bool SuggestMass
        {
            get
            {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(SuggestMass), true));
            }

            set
            {
                SetValue(FeatureKey, nameof(SuggestMass), value);
            }
        }

        public static bool AutoLoadBio
        {
            get
            {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(AutoLoadBio), false));
            }

            set
            {
                SetValue(FeatureKey, nameof(AutoLoadBio), value);
            }
        }

        public static bool KeepWizard
        {
            get
            {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(KeepWizard), false));
            }
            set
            {
                SetValue(FeatureKey, nameof(KeepWizard), value);
            }
        }

        public static ArtifactCriticality ReportCriticality
        {
            get
            {
                object o = GetValue(FeatureKey, nameof(ReportCriticality), ArtifactCriticality.Allowed);
                if (o == null) return ArtifactCriticality.Critical;
                else return (ArtifactCriticality)(int)o;
            }
            set { SetValue(FeatureKey, nameof(ReportCriticality), (int)value); }
        }

        public static bool CheckConsistency
        {
            get
            {
                return Convert.ToBoolean(GetValue(FeatureKey, nameof(CheckConsistency), true));
            }
            set
            {
                SetValue(FeatureKey, nameof(CheckConsistency), value);
            }
        }

        public static string[] Bios
        {
            get
            {
                string[] values = (string[])GetValue(FeatureKey, nameof(Bios), new string[0]);
                return values.GetOperableFilenames(UserSettings.InterfaceBio.Extension);
            }

            set
            {
                SetValue(FeatureKey, nameof(Bios), value);
            }
        }
    }

    public enum LogSortOrder
    {
        AsInput = -1,
        Alphabetically = 0,
        ByQuantity = 1,
        ByMass = 2,
        Philogenetically = 4
    }
}
