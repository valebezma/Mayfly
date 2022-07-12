using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Plankton.Explorer
{
    public abstract class Service
    {
        public static string Localize(string field)
        {
            ResourceManager resources = new ResourceManager(typeof(Card));
            ResourceManager exResources = new ResourceManager(typeof(MainForm));
            string result = string.Empty;
            switch (field)
            {
                case "Investigator":
                    result = Wild.Resources.Reports.Card.Investigator;
                    break;
                case "Wealth":
                case "Abundance":
                case "Biomass":
                case "DiversityA":
                case "DiversityB":
                    result = (string)exResources.GetObject("columnCard" + field + ".HeaderText");
                    break;
                case "Time":
                    result = resources.GetString("labelOperation.Text");
                    break;
                case "DateTime":
                    result = resources.GetString("labelDate.Text");
                    break;
                case "WaterID":
                    result = resources.GetString("labelWater.Text");
                    break;
                case "Substrate":
                    result = resources.GetString("tabPageSubstrate.Text");
                    break;
                default:
                    result = resources.GetString("label" + field + ".Text");
                    if (result == null) { result = resources.GetString("checkBox" + field + ".Text"); }
                    if (result == null) { result = (string)exResources.GetObject("columnInd" + field + ".HeaderText"); }
                    if (result == null) { result = field; }
                    break;
            }

            return result;
        }

        public static ListViewItem LocalizedItem(string field)
        {
            ListViewItem result = new ListViewItem();
            result.Name = field;
            result.Text = Localize(field);
            return result;
        }

        public static ListViewItem[] CardValueItems(Wild.Survey data)
        {
            List<ListViewItem> result = new List<ListViewItem>();

            foreach (DataColumn DataColumn in data.Card.Columns)
            {
                if ((new string[] { "ID" }).Contains(DataColumn.ColumnName)) continue;
                result.Add(LocalizedItem(DataColumn.ColumnName));
            }

            result.Add(Service.LocalizedItem("Substrate"));
            result.Add(Service.LocalizedItem("Wealth"));
            result.Add(Service.LocalizedItem("Abundance"));
            result.Add(Service.LocalizedItem("Biomass"));
            result.Add(Service.LocalizedItem("DiversityA"));
            result.Add(Service.LocalizedItem("DiversityB"));

            foreach (Wild.Survey.FactorRow Factor in data.Factor)
            {
                result.Add(LocalizedItem(Factor.Factor));
            }

            return result.ToArray();
        }



        public static string[] GetAssociates(string species)
        {
            object result = UserSettings.GetValue(UserSettings.Path,
                new string[] { "Restoration", "Assosiciation" },
                species, new string[0]);

            if (result == null)
            {
                return new string[0];
            }
            else
            {
                return (string[])result;
            }
        }

        public static void SaveAssociates(Wild.Survey.DefinitionRow speciesRow, Wild.Survey.DefinitionRow[] associates)
        {
            if (associates != null)
            {
                List<string> savedAssociates = new List<string>();

                foreach (Wild.Survey.DefinitionRow spcRow in associates)
                {
                    if (spcRow.Species == speciesRow.Species) continue;
                    savedAssociates.Add(spcRow.Species);
                }

                if (savedAssociates.Count > 0)
                {
                    Service.SaveAssociates(speciesRow.Species, savedAssociates.ToArray());
                }
            }
        }

        public static void SaveAssociates(string species, string[] associates)
        {
            UserSettings.SetValue(UserSettings.Path,
                new string[] { "Restoration", "Assosiciation" },
                species, associates);
        }
    }
}
