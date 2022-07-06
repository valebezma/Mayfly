using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.IO;
using System.Windows.Forms;

namespace Mayfly.Fish
{
    public abstract class UserSettings
    {
        private static ReaderUserSettings settings;

        public static ReaderUserSettings ReaderSettings
        {
            get
            {
                if (settings == null)
                {
                    settings = new ReaderUserSettings(".fcd", "Fish");
                }

                return settings;
            }
        }


        private static Equipment equiment;

        public static Equipment Equipment
        {
            get
            {
                if (equiment == null)
                {
                    equiment = new Equipment();

                    string path = System.IO.Path.Combine(IO.UserFolder, "equipment.ini");

                    if (File.Exists(path))
                    {
                        equiment.ReadXml(path);
                    }
                }

                return equiment;
            }

            set
            {
                equiment = value;
            }
        }



        public static string ParasitesIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(ReaderSettings.Path, nameof(ParasitesIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    ParasitesIndexPath = Wild.Service.GetReferencePathSpecies("Fish Parasites");
                    return ParasitesIndexPath;
                }
                else
                {
                    return filepath;
                }
            }
            set
            {
                UserSetting.SetValue(ReaderSettings.Path, nameof(ParasitesIndexPath), value);
            }
        }

        private static TaxonomicIndex parasitesIndex;

        public static TaxonomicIndex ParasitesIndex
        {
            get
            {
                if (parasitesIndex == null)
                {
                    parasitesIndex = new TaxonomicIndex();

                    if (ParasitesIndexPath != null)
                    {
                        parasitesIndex.ReadXml(ParasitesIndexPath);
                    }
                }

                return parasitesIndex;
            }
        }


        public static string DietIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(ReaderSettings.Path, nameof(DietIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    DietIndexPath = Wild.Service.GetReferencePathSpecies("Fish Diet");
                    return DietIndexPath;
                }
                else
                {
                    return filepath;
                }
            }

            set 
            {
                UserSetting.SetValue(ReaderSettings.Path, nameof(DietIndexPath), value);
            }
        }

        private static TaxonomicIndex dietIndex;

        public static TaxonomicIndex DietIndex
        {
            get
            {
                if (dietIndex == null)
                {
                    dietIndex = new TaxonomicIndex();

                    if (DietIndexPath != null)
                    {
                        dietIndex.ReadXml(DietIndexPath);
                    }
                }

                return dietIndex;
            }
        }


        public static double DefaultOpening
        {
            get { return (double)(int)UserSetting.GetValue(ReaderSettings.Path, nameof(DefaultOpening), 60) / 100; }
            set { UserSetting.SetValue(ReaderSettings.Path, nameof(DefaultOpening), (int)(value * 100)); }
        }


        public static double GillnetStdLength
        {
            get { return (double)((int)UserSetting.GetValue(ReaderSettings.Path, nameof(GillnetStdLength), 3750)) / 100; }
            set { UserSetting.SetValue(ReaderSettings.Path, nameof(GillnetStdLength), (int)(value * 100)); }
        }

        public static double GillnetStdHeight
        {
            get { return (double)((int)UserSetting.GetValue(ReaderSettings.Path, nameof(GillnetStdHeight), 200)) / 100; }
            set { UserSetting.SetValue(ReaderSettings.Path, nameof(GillnetStdHeight), (int)(value * 100)); }
        }

        public static int GillnetStdExposure
        {
            get { return (int)UserSetting.GetValue(ReaderSettings.Path, nameof(GillnetStdExposure), 24); }
            set { UserSetting.SetValue(ReaderSettings.Path, nameof(GillnetStdExposure), value); }
        }



        public static double DefaultStratifiedInterval
        {
            get { return (double)((int)UserSetting.GetValue(ReaderSettings.Path, nameof(DefaultStratifiedInterval), 1000)) / 100; }
            set { UserSetting.SetValue(ReaderSettings.Path, nameof(DefaultStratifiedInterval), (int)(value * 100)); }
        }
    }
}
