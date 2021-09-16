using Mayfly.Wild;
using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.Resources;

namespace Mayfly.Fish.Explorer
{
    public abstract class Service
    {
        public static string GamingAge;

        public static string GamingLength;

        public static string NaturalMortality;

        public static string FishingMortality;

        public static string Catchability;

        public static string GearClass;

        public static string Localize(string field)
        {
            ResourceManager resources = new ResourceManager(typeof(Card));
            ResourceManager ex_resources = new ResourceManager(typeof(MainForm));
            string result = (string)ex_resources.GetObject("columnCard" + field + ".HeaderText");
            if (result == null)
                result = (string)ex_resources.GetObject("columnInd" + field + ".HeaderText");
            if (result == null)
                result = resources.GetString("label" + field + ".Text");
            if (result == null)
                result = resources.GetString("checkBox" + field + ".Text");
            if (result == null)
                result = field;
            return result;
        }

        //public static ListViewItem LocalizedItem(string field)
        //{
        //    ListViewItem result = new ListViewItem();
        //    result.Name = field;
        //    result.Text = Localize(field);
        //    return result;
        //}

        //public static ListViewItem[] CardValueItems(Data data)
        //{
        //    List<ListViewItem> result = new List<ListViewItem>();

        //    foreach (DataColumn DataColumn in data.Card.Columns)
        //    {
        //        if ((new string[] { "ID" }).Contains(DataColumn.ColumnName)) continue;
        //        result.Add(LocalizedItem(DataColumn.ColumnName));
        //    }

        //    result.Add(Service.LocalizedItem("Wealth"));
        //    result.Add(Service.LocalizedItem("Abundance"));
        //    result.Add(Service.LocalizedItem("Biomass"));
        //    result.Add(Service.LocalizedItem("DiversityA"));
        //    result.Add(Service.LocalizedItem("DiversityB"));

        //    foreach (Data.FactorRow Factor in data.Factor)
        //    {
        //        result.Add(LocalizedItem(Factor.Factor));
        //    }

        //    return result.ToArray();
        //}

        public static Interval GetStrate(double length)
        {
            return Mathematics.Service.GetStrate(length, UserSettings.SizeInterval);
        }

        public static DateTime GetCatchDate(int year)
        {
            return new DateTime(year, 7, 15);
        }



        public static void SaveGearSpatialValue(FishSamplerType samplerType, string gearClass, double value)
        {
            UserSetting.SetValue(UserSettings.Path, new string[] { nameof(GearClass),
                samplerType.ToString() },
                gearClass, (int)(value * 1000));
        }

        public static double GetGearSpatialValue(FishSamplerType samplerType, string gearClass)
        {
            object result = UserSetting.GetValue(UserSettings.Path,
                new string[] { nameof(GearClass),
                    samplerType.ToString() },
                gearClass, null);

            if (result == null)
            {
                return 1;
            }
            else
            {
                return .001 * (double)(int)result;
            }
        }


        public static double GetCatchability(FishSamplerType samplerType, string species)
        {
            object result = UserSetting.GetValue(UserSettings.Path,
                new string[] { nameof(Catchability), samplerType.ToString() },
                species, null);

            if (result == null)
            {
                return UserSettings.DefaultCatchability;
            }
            else
            {
                return (double)(int)result / 100;
            }
        }

        public static void SaveCatchability(FishSamplerType samplerType, string species, double value)
        {
            UserSetting.SetValue(UserSettings.Path,
                new string[] { nameof(Catchability), samplerType.ToString() },
                species, (int)(value * 100));
        }


        public static double GetNaturalMortality(string species)
        {
            object result = UserSetting.GetValue(UserSettings.Path, nameof(NaturalMortality), species);

            if (result == null)
            {
                return double.NaN;
            }
            else
            {
                return (double)(int)result / 1000;
            }
        }

        public static void SaveNaturalMortality(string species, double value)
        {
            UserSetting.SetValue(UserSettings.Path, nameof(NaturalMortality), species, (int)(value * 1000));
        }


        public static double GetFishingMortality(string species)
        {
            object result = UserSetting.GetValue(UserSettings.Path, nameof(FishingMortality), species);

            if (result == null)
            {
                return double.NaN;
            }
            else
            {
                return (double)(int)result / 1000;
            }
        }

        public static void SaveFishingMortality(string species, double value)
        {
            UserSetting.SetValue(UserSettings.Path, nameof(FishingMortality), species, (int)(value * 1000));
        }


        public static double GetMeasure(string species)
        {
            object result = UserSetting.GetValue(UserSettings.Path, nameof(GamingLength), species);

            if (result == null)
            {
                return double.NaN;
            }
            else
            {
                return (double)(int)result;
            }
        }

        public static void SaveMeasure(string species, double value)
        {
            UserSetting.SetValue(UserSettings.Path, nameof(GamingLength), species, (int)value);
        }


        public static Age GetGamingAge(string species)
        {
            object result = UserSetting.GetValue(UserSettings.Path, nameof(GamingAge), species);

            if (result == null)
            {
                return null;
            }
            else
            {
                return (Age)(string)result;
            }
        }

        public static void SaveGamingAge(string species, Age age)
        {
            UserSetting.SetValue(UserSettings.Path, nameof(GamingAge), species, age.ToString());
        }

        public static List<EquipmentEvent> AvailableEvents(EquipmentEvent equipmentEvent)
        {
            List<EquipmentEvent> result = new List<EquipmentEvent>();

            switch (equipmentEvent)
            {
                case EquipmentEvent.Setting:
                case EquipmentEvent.Inspection:
                    result.Add(EquipmentEvent.Inspection);
                    result.Add(EquipmentEvent.Removing);
                    result.Add(EquipmentEvent.NoAction);
                    break;
                case EquipmentEvent.Removing:
                case EquipmentEvent.NoAction:
                    result.Add(EquipmentEvent.Setting);
                    result.Add(EquipmentEvent.NoAction);
                    break;
            }

            return result;
        }
    }
}
