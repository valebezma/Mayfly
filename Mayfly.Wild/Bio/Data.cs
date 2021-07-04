using Mayfly.Mathematics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Mayfly.Wild
{
    partial class Data
    {
        public ContinuousBio MassModels;

        public ContinuousBio GrowthModels;

        public ContinuousBio MassGrowthModels;

        public string[] GetAuthors()
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (string.IsNullOrWhiteSpace(cardRow.Investigator)) continue;
                string investigator = cardRow.Investigator;
                if (result.Contains(investigator)) continue;
                result.Add(investigator);
            }
            return result.ToArray();
        }

        public string[] GetPlaces()
        {
            List<string> result = new List<string>();
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.IsWaterIDNull()) continue;
                string waterDescription = cardRow.WaterRow.Presentation;
                if (result.Contains(waterDescription)) continue;
                result.Add(waterDescription);
            }
            return result.ToArray();
        }

        public DateTime[] GetDates()
        {
            List<DateTime> result = new List<DateTime>();
            foreach (Data.CardRow cardRow in this.Card)
            {
                if (cardRow.IsWhenNull()) continue;
                if (result.Contains(cardRow.When.Date)) continue;
                result.Add(cardRow.When.Date);
            }
            result.Sort();
            return result.ToArray();
        }

        public DataRow[] GetBioRows(string species)
        {
            return Species.FindBySpecies(species).GetIndividualRows();
        }

        public double GetIndividualValue(DataRow dataRow, string field)
        {
            IndividualRow individualRow = (IndividualRow)dataRow;

            if (individualRow.Table.Columns[field] != null && !individualRow.IsNull(field))
            {
                return (double)individualRow[field];
            }
            else if (this.Variable.FindByVarName(field) != null)
            {
                return (double)this.Value.FindByIndIDVarID(individualRow.ID, this.Variable.FindByVarName(field).ID).Value;
            }
            else
            {
                return double.NaN;
            }
        }

        public bool IsBioLoaded
        {
            get
            {
                return (MassModels.ExternalScatterplots.Count) > 0;
            }
        }

        public void InitializeBio()
        {
            MassModels = new ContinuousBio(this, this.Species.GetSorted(),
                Individual.LengthColumn, Individual.MassColumn, TrendType.Power);
            MassModels.DisplayNameY = Resources.Reports.Caption.Mass;
            MassModels.DisplayNameX = Resources.Reports.Caption.Length;

            GrowthModels = new ContinuousBio(this, Species.GetSorted(),
                Individual.AgeColumn, Individual.LengthColumn, TrendType.Growth);
            GrowthModels.DisplayNameY = Resources.Reports.Caption.Length;
            GrowthModels.DisplayNameX = Resources.Reports.Caption.Age;

            MassGrowthModels = new ContinuousBio(this, Species.GetSorted(),
                Individual.AgeColumn, Individual.MassColumn, TrendType.Linear);
            MassGrowthModels.DisplayNameY = Resources.Reports.Caption.Mass;
            MassGrowthModels.DisplayNameX = Resources.Reports.Caption.Age;

            //WeightModels.RefreshMeta();
            //GrowthModels.RefreshMeta();
            //WeightGrowthModels.RefreshMeta();
        }

        public void RefreshBios()
        {
            if (!Licensing.Verify("Bios")) return;

            MassModels.Refresh();
            GrowthModels.Refresh();
            MassGrowthModels.Refresh();
        }

        public IBio GetBio(string species, string x, string y)
        {
            return null;
        }

        public void ImportBio(string fileName)
        {
            this.ImportBio(fileName, true);
        }

        public void ImportBio(string fileName, bool clear)
        {
            Data data = new Data();
            string contents = StringCipher.Decrypt(File.ReadAllText(fileName), "bio");
            data.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
            data.Solitary.Investigator = StringCipher.Decrypt(data.Solitary.Sign, data.Solitary.When.ToString("s"));
            data.InitializeBio();

            data.MassModels.VisualConfirmation = false;
            data.GrowthModels.VisualConfirmation = false;

            MassModels.Involve(data.MassModels, clear);
            GrowthModels.Involve(data.GrowthModels, clear);

            Mayfly.Log.Write("Bio {0} is loaded.", Path.GetFileNameWithoutExtension(fileName));
        }

        public void ExportBio(string fileName)
        {
            File.WriteAllText(fileName, StringCipher.Encrypt(this.GetXml(), "bio"));
        }

        public bool BioLoaded
        {
            get
            {
                return (Licensing.Verify("Bios") &&
                    ((this.GrowthModels.ExternalScatterplots.Count +
                    this.MassModels.ExternalScatterplots.Count) > 0));
            }
        }
    }
}
