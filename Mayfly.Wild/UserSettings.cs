using Mayfly.Extensions;
using Mayfly.Waters;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Wild
{
    public class UserSettings
    {
        public static string Path
        {
            get
            {
                return UserSetting.GetFeatureKey("Mayfly.Wild");
            }
        }

        public static FileSystemInterface Interface = new FileSystemInterface(FieldDataFolder, new string[] { ".fcd", ".bcd", ".pcd" }, new string[] { ".html" });

        public static FileSystemInterface InterfaceBio = new FileSystemInterface(UserSettings.FieldDataFolder, ".bio");

        //public static FileSystemInterface InterfacePermission = new FileSystemInterface(UserSettings.FieldDataFolder, ".perm");

        //public static Permission installedPermissions;

        //public static Permission InstalledPermissions
        //{
        //    get
        //    {
        //        if (installedPermissions == null)
        //        {
        //            installedPermissions = new Permission();

        //            foreach (string key in UserSetting.GetSubfolders(Mayfly.UserSettingPaths.KeyGeneral, "Permissions"))
        //            {
        //                Permission.GrantRow grantRow = installedPermissions.Grant.NewGrantRow(); ;

        //                string granterCrypt = (string)UserSetting.GetValue(Mayfly.UserSettingPaths.KeyGeneral,
        //                    new string[] { "Permissions", key }, "Donor");
        //                string expireCrypt = (string)UserSetting.GetValue(Mayfly.UserSettingPaths.KeyGeneral,
        //                    new string[] { "Permissions", key }, "Expire");

        //                grantRow.Donor = StringCipher.Decrypt(granterCrypt, key);
        //                grantRow.Expire = DateTime.Parse(StringCipher.Decrypt(expireCrypt, key));

        //                installedPermissions.Grant.AddGrantRow(grantRow);
        //            }
        //        }

        //        return installedPermissions;
        //    }
        //}

        public static string FieldDataFolder
        {
            get
            {
                string result = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "FieldData");

                if (!Directory.Exists(result))
                {
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

        public static string[] AddtFactors
        {
            get { return (string[])UserSetting.GetValue(Path, nameof(AddtFactors), new string[0]); }
            set { UserSetting.SetValue(Path, nameof(AddtFactors), value); }
        }
        
        public static LogOrder LogOrder
        {
            get { return (LogOrder)(int)UserSetting.GetValue(Path, nameof(LogOrder), LogOrder.Alphabetically); }
            set { UserSetting.SetValue(Path, nameof(LogOrder), (int)value); }
        }

        public static DiversityIndex Diversity
        {
            get { return (DiversityIndex)(int)UserSetting.GetValue(Path, nameof(Diversity), DiversityIndex.D1963_Shannon); }
            set { UserSetting.SetValue(Path, nameof(Diversity), (int)value); }
        }

        public static int Dominance
        {
            get { return (int)UserSetting.GetValue(Path, nameof(Dominance), 0); }
            set { UserSetting.SetValue(Path, nameof(Dominance), value); }
        }

        public static string WatersIndexPath
        {
            get
            {
                string filepath = IO.GetPath(UserSetting.GetValue(Path, nameof(WatersIndexPath), string.Empty));

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    WatersIndexPath = Service.GetReferencePath(Waters.UserSettings.Interface.OpenDialog,
                      "Waters (auto).wtr", Server.GetUri("get/references/waters/waters_default.wtr", Application.CurrentCulture));
                }

                return WatersIndexPath;
            }
            set 
            {
                UserSetting.SetValue(Path, nameof(WatersIndexPath), value);
            }
        }



        private static WatersKey watersIndex;

        public static WatersKey WatersIndex
        {
            get
            {
                if (watersIndex == null)
                {
                    watersIndex = new WatersKey();

                    try { watersIndex.ReadXml(WatersIndexPath); }
                    catch { Log.Write(EventType.Maintenance, "First call for {0}. File is empty and will be rewritten.", WatersIndexPath); }
                }

                return watersIndex;
            }

            set
            {
                watersIndex = value;
            }
        }

        private static WeatherEvents weatherIndex;

        public static WeatherEvents WeatherIndex
        {
            get
            {
                if (weatherIndex == null)
                {
                    weatherIndex = new WeatherEvents();
                    weatherIndex.SetAttributable();
                    try { weatherIndex.ReadXml(System.IO.Path.Combine(Application.StartupPath, @"interface\weatherevts.ini")); }
                    catch { Log.Write(EventType.Maintenance, "Can't read weather file."); }
                }

                return weatherIndex;
            }
        }
    }

    //public abstract class UserSettingPaths
    //{
    //    public static string Species = "RefSpecies";

    //    public static string Waters = "RefWaters";

    //    public static string Date = "MemDate";

    //    public static string Water = "MemWater";

    //    public static string Sampler = "MemSampler";

    //    public static string AddtFactors = "AddtFactors";

    //    public static string AddtVars = "AddtVars";

    //    public static string CurrVars = "MemVars";

    //    public static string AutoLogOpen = "AutoOpenIndividuals";

    //    public static string FixTotals = "NumFixTotals";

    //    public static string AutoIncreaseTotals = "NumAutoAdd";

    //    public static string AutoDecreaseTotals = "NumAutoReduce";

    //    public static string BreakBeforeIndividuals = "PrintBreakBeforeIndividuals";

    //    public static string BreakBetweenSpecies = "PrintBreakBtwSpecies";

    //    public static string OddCardStart = "PrintOddCardStart";

    //    //public static string AgeFromDay = "AgeFromDay";

    //    //public static string GainMonth = "GainMonth";

    //    //public static string GainDay = "GainDay";

    //    public static string DefaultStratifiedInterval = "DefaultStratifiedInterval";


    //    public static string Dominance = "Dominance";

    //    public static string Diversity = "Diversity";

    //    public static string LogOrder = "OrderLog";



    //    public static string MassRestoration = "WeightRestoration";

    //    public static string Association = "Association";

    //    #region Recovery memorized values

    //    public static string AutoLoadBio = "AutoLoadBio";

    //    public static string Bios = "Bios";

    //    public static string SuggestMass = "SuggestMass";

    //    //public static string VisualConfirmation = "VisualConfirmation";

    //    public static string UseRaw = "UseRaw";

    //    public static string RestoreAssociation = "RestoreAssociation";

    //    public static string Protocol = "Protocol";

    //    public static string RequiredStrength = "RequiredStrength";

    //    internal static void Initialize()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}

    public enum LogOrder
    {
        AsInput = -1,
        Alphabetically = 0,
        ByQuantity = 1,
        ByMass = 2
    }
}
