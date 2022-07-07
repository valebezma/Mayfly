using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.IO;
using System.Windows.Forms;
using static Mayfly.UserSettings;

namespace Mayfly.Fish
{
    public static class UserSettings
    {
        private static Equipment equiment;

        public static Equipment Equipment {
            get {
                if (equiment == null) {
                    equiment = new Equipment();

                    string path = Path.Combine(IO.UserFolder, "equipment.ini");

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



        public static string ParasitesIndexPath {
            get {
                string filepath = IO.GetPath(GetValue(ReaderSettings.FeatureKey, nameof(ParasitesIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath)) {
                    ParasitesIndexPath = Wild.Service.GetReferencePathSpecies("Fish Parasites");
                    return ParasitesIndexPath;
                } else {
                    return filepath;
                }
            }
            set {
                SetValue(ReaderSettings.FeatureKey, nameof(ParasitesIndexPath), value);
            }
        }

        private static TaxonomicIndex parasitesIndex;

        public static TaxonomicIndex ParasitesIndex {
            get {
                if (parasitesIndex == null) {
                    parasitesIndex = new TaxonomicIndex();

                    if (ParasitesIndexPath != null) {
                        parasitesIndex.ReadXml(ParasitesIndexPath);
                    }
                }

                return parasitesIndex;
            }
        }



        public static string DietIndexPath {
            get {
                string filepath = IO.GetPath(GetValue(ReaderSettings.FeatureKey, nameof(DietIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath)) {
                    DietIndexPath = Wild.Service.GetReferencePathSpecies("Fish Diet");
                    return DietIndexPath;
                } else {
                    return filepath;
                }
            }

            set {
                SetValue(ReaderSettings.FeatureKey, nameof(DietIndexPath), value);
            }
        }

        private static TaxonomicIndex dietIndex;

        public static TaxonomicIndex DietIndex {
            get {
                if (dietIndex == null) {
                    dietIndex = new TaxonomicIndex();

                    if (DietIndexPath != null) {
                        dietIndex.ReadXml(DietIndexPath);
                    }
                }

                return dietIndex;
            }
        }



        public static double DefaultOpening {
            get { return (double)(int)GetValue(ReaderSettings.FeatureKey, nameof(DefaultOpening), 60) / 100; }
            set { SetValue(ReaderSettings.FeatureKey, nameof(DefaultOpening), (int)(value * 100)); }
        }

        public static double GillnetStdLength {
            get { return (double)((int)GetValue(ReaderSettings.FeatureKey, nameof(GillnetStdLength), 3750)) / 100; }
            set { SetValue(ReaderSettings.FeatureKey, nameof(GillnetStdLength), (int)(value * 100)); }
        }

        public static double GillnetStdHeight {
            get { return (double)((int)GetValue(ReaderSettings.FeatureKey, nameof(GillnetStdHeight), 200)) / 100; }
            set { SetValue(ReaderSettings.FeatureKey, nameof(GillnetStdHeight), (int)(value * 100)); }
        }

        public static int GillnetStdExposure {
            get { return (int)GetValue(ReaderSettings.FeatureKey, nameof(GillnetStdExposure), 24); }
            set { SetValue(ReaderSettings.FeatureKey, nameof(GillnetStdExposure), value); }
        }



        public static double DefaultStratifiedInterval {
            get { return (double)((int)GetValue(ReaderSettings.FeatureKey, nameof(DefaultStratifiedInterval), 1000)) / 100; }
            set { SetValue(ReaderSettings.FeatureKey, nameof(DefaultStratifiedInterval), (int)(value * 100)); }
        }
    }
}