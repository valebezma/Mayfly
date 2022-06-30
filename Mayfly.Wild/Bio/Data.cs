using Mayfly.Mathematics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Mayfly.Extensions;
using Mayfly.Species;

namespace Mayfly.Wild
{
    partial class Data
    {
        public bool IsBioLoaded
        {
            get
            {
                if (MassModels != null)
                {
                    foreach (ContinuousBio bio in MassModels)
                    {
                        if (bio.ExternalData != null) return true;
                    }
                }

                if (GrowthModels != null)
                {
                    foreach (ContinuousBio bio in GrowthModels)
                    {
                        if (bio.ExternalData != null) return true;
                    }
                }

                return false;
            }
        }

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

        public void RefreshBios()
        {
            if (MassModels == null)
            {
                MassModels = new List<ContinuousBio>();
                GrowthModels = new List<ContinuousBio>();
            }

            foreach (SpeciesKey.TaxonRow speciesRow in GetStack().GetSpecies())
            {
                ContinuousBio biom = FindMassModel(speciesRow.Name);

                if (biom == null)
                {
                    biom = new ContinuousBio(this, speciesRow, Individual.LengthColumn, Individual.MassColumn, TrendType.Power)
                    {
                        DisplayNameX = Resources.Reports.Caption.LengthUnit,
                        DisplayNameY = Resources.Reports.Caption.MassUnit
                    };

                    MassModels.Add(biom);
                }

                biom.RefreshInternal();


                ContinuousBio biog = FindGrowthModel(speciesRow.Name);

                if (biog == null)
                {
                    biog = new ContinuousBio(this, speciesRow, Individual.AgeColumn, Individual.LengthColumn, TrendType.Growth)
                    {
                        DisplayNameX = Resources.Reports.Caption.AgeUnit,
                        DisplayNameY = Resources.Reports.Caption.LengthUnit
                    };

                    GrowthModels.Add(biog);
                }

                biog.RefreshInternal();
            }
        }

        public void ImportBio(string filename, bool clearExisted)
        {
            Data data = new Data(key, null);
            data.SetAttributable();
            string contents = StringCipher.Decrypt(File.ReadAllText(filename), "bio");
            //string contents = File.ReadAllText(filename);
            data.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
            data.RefreshBios();

            foreach (ContinuousBio exbio in data.MassModels)
            {
                ContinuousBio inbio = FindMassModel(exbio.Species);
                if (inbio == null)
                {
                    exbio.Reverse();
                    exbio.Parent = this;
                    MassModels.Add(exbio);
                }
                else
                {
                    inbio.Involve(exbio, clearExisted);
                }
            }

            foreach (ContinuousBio exbio in data.GrowthModels)
            {
                ContinuousBio inbio = FindGrowthModel(exbio.Species);
                if (inbio == null)
                {
                    exbio.Reverse();
                    exbio.Parent = this;
                    GrowthModels.Add(exbio);
                }
                else
                {
                    inbio.Involve(exbio, clearExisted);
                }
            }

            data.Dispose();

            Mayfly.Log.Write("Bio {0} is loaded.", Path.GetFileNameWithoutExtension(filename));
        }

        public void ImportBio(string filename)
        {
            ImportBio(filename, false);
        }

        public void ExportBio(string filename)
        {
            string content = this.GetXml();

            foreach (string toRemove in new string[] { Environment.NewLine, "  "})
            {
                while (content.Contains(toRemove))
                {
                    content = content.Replace(toRemove, " ");
                }
            }
            //File.WriteAllText(filename, content);
            File.WriteAllText(filename, StringCipher.Encrypt(content, "bio"));
        }

        public ContinuousBio FindMassModel(string speceis)
        {
            return Bio.Find(MassModels, speceis);
        }

        public ContinuousBio FindGrowthModel(string species)
        {
            return Bio.Find(GrowthModels, species);
        }
    }
}
