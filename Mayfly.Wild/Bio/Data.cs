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
        public List<ContinuousBio> MassModels;

        public List<ContinuousBio> GrowthModels;

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

        public double GetIndividualValue(IndividualRow individualRow, string field)
        {
            if (individualRow.Table.Columns[field] != null && !individualRow.IsNull(field))
            {
                return (double)individualRow[field];
            }
            else if (Variable.FindByVarName(field) != null)
            {
                return (double)Value.FindByIndIDVarID(individualRow.ID, Variable.FindByVarName(field).ID).Value;
            }
            else
            {
                return double.NaN;
            }
        }

        public void InitializeBio()
        {
            MassModels = new List<ContinuousBio>();
            GrowthModels = new List<ContinuousBio>();

            foreach (SpeciesRow speciesRow in this.Species.Rows)
            {
                MassModels.Add(
                    new ContinuousBio(this, speciesRow, Individual.LengthColumn, Individual.MassColumn, TrendType.Power) 
                    {
                        DisplayNameX = Resources.Reports.Caption.Length, 
                        DisplayNameY = Resources.Reports.Caption.Mass
                    }
                );

                GrowthModels.Add(
                    new ContinuousBio(this, speciesRow, Individual.AgeColumn, Individual.LengthColumn, TrendType.Growth)
                    {
                        DisplayNameX = Resources.Reports.Caption.Age, 
                        DisplayNameY = Resources.Reports.Caption.Length
                    }
                );
            }
        }

        public void RefreshBios()
        {
            foreach (ContinuousBio bio in MassModels)
            {
                bio.RefreshModel();
            }

            foreach (ContinuousBio bio in GrowthModels)
            {
                bio.RefreshModel();
            }
        }

        public void ImportBio(string fileName)
        {
            Data data = new Data();
            string contents = StringCipher.Decrypt(File.ReadAllText(fileName), "bio");
            data.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
            data.Solitary.Investigator = StringCipher.Decrypt(data.Solitary.Sign, data.Solitary.When.ToString("s"));
            data.InitializeBio();

            foreach (ContinuousBio exbio in data.MassModels)
            {
                ContinuousBio inbio = FindMassModel(exbio.Species);
                if (inbio != null) inbio.Involve(exbio);
            }

            foreach (ContinuousBio exbio in data.GrowthModels)
            {
                ContinuousBio inbio = FindGrowthModel(exbio.Species);
                if (inbio != null) inbio.Involve(exbio);
            }

            Mayfly.Log.Write("Bio {0} is loaded.", Path.GetFileNameWithoutExtension(fileName));
        }

        public void ExportBio(string fileName)
        {
            File.WriteAllText(fileName, StringCipher.Encrypt(this.GetXml(), "bio"));
        }

        public ContinuousBio FindMassModel(string speceis)
        {
            return Bio.Find(MassModels, speceis);
        }

        public ContinuousBio FindGrowthModel(string speceis)
        {
            return Bio.Find(GrowthModels, speceis);
        }
    }
}
