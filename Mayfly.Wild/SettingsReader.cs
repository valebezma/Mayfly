using Mayfly.Extensions;
using Mayfly.Species;
using System;
using System.IO;
using static Mayfly.UserSettings;
using Mayfly.Wild.Controls;

namespace Mayfly.Wild
{
    public enum LogSortOrder
    {
        AsInput = -1,
        Alphabetically = 0,
        ByQuantity = 1,
        ByMass = 2,
        Philogenetically = 4
    }

    public static class SettingsReader
    {
        public static FileSystemInterface Interface;

        public static MassDegree IndividualMassDegree;

        public static MassDegree LogMassDegree;

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

        public static void SetFeature(string feature, string ext, MassDegree logMassDegree, MassDegree indMassDegree) {

            Feature = feature;
            Interface = new FileSystemInterface(ext, ".html") {
                FolderPath = UserSettings.FieldDataFolder
            };
            LogMassDegree = logMassDegree;
            IndividualMassDegree = indMassDegree;

            ExpandSettings(
                typeof(SettingsPageIndices),
                typeof(SettingsPageEquipment),
                typeof(SettingsPageLog),
                typeof(SettingsPageFactors),
                typeof(SettingsPageVariables),
                typeof(SettingsPagePrint)
                );
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
                SetValue(FeatureKey, nameof(SelectedSampler), value?.ID);
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
}
