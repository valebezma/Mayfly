using Mayfly.Mathematics.Charts;
using System;
using System.IO;
using System.Text;
using Mayfly.Wild;
using System.Windows.Forms;
using System.ComponentModel;
using Mayfly.Fish;
using Mayfly.Fish.Explorer;

namespace Mayfly.Fish.Explorer
{
    public static partial class DataExtensions
    {
        public static Data GetBio(this Data data1)
        {
            Data data = data1.Copy();

            // Remove unsigned cards

            for (int i = 0; i < data.Card.Count; i++)
            {
                if (data.Card[i].IsSignNull())
                {
                    data.Card.Rows.RemoveAt(i);
                    i--;
                }
            }

            if (data.Card.Count == 0)
                throw new ArgumentNullException("Unsigned data", "Cards do not contain signed data.");

            // Remove irrelevant tables and fields

            data.Stratified.Clear();
            data.FactorValue.Clear();
            data.Factor.Clear();
            data.Intestine.Clear();
            data.Organ.Clear();

            foreach (Data.IndividualRow individualRow in data.Individual)
            {
                individualRow.SetConsumedMassNull();
                individualRow.SetEggSizeNull();
                individualRow.SetFatnessNull();
                individualRow.SetGonadMassNull();
                individualRow.SetGonadSampleMassNull();
                individualRow.SetGonadSampleNull();
                individualRow.SetPreyNull();
            }

            // for each species
            // try to build scatterplot with regression
            // if can't - delete species
            data.InitializeBio();

            foreach (Data.SpeciesRow speciesRow in data.Species)
            {
                Scatterplot massScatter = data.MassModels.GetInternalScatterplot(speciesRow.Species);

                //if (massScatter == null || !massScatter.IsRegressionOK || !massScatter.Regression.IsSignificant())
                //{
                //    foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
                //    {
                //        individualRow.SetMassNull();
                //    }

                    data.MassModels.Refresh(speciesRow.Species);
                //}

                Scatterplot ageScatter = data.GrowthModels.GetInternalScatterplot(speciesRow.Species);

                //if (ageScatter == null || !ageScatter.IsRegressionOK || !ageScatter.Regression.IsSignificant())
                //{
                //    foreach (Data.IndividualRow individualRow in speciesRow.GetIndividualRows())
                //    {
                //        individualRow.SetAgeNull();
                //    }

                    data.GrowthModels.Refresh(speciesRow.Species);
                //}
            }

            // Remove individuals

            for (int i = 0; i < data.Individual.Count; i++)
            {
                if (data.Individual[i].IsLengthNull() ||
                    (data.Individual[i].IsMassNull() &&
                    data.Individual[i].IsAgeNull()))
                {
                    data.Individual.Rows.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < data.Species.Count; i++)
            {
                if (data.Species[i].GetIndividualRows().Length == 0)
                {
                    data.Species.RemoveSpeciesRow(data.Species[i]);
                    i--;
                }
            }

            if (data.Species.Count == 0)
                throw new ArgumentNullException("Regression data", "Data is unsufficient to be used as a bio.");
            
            return data;
        }

        //public static void ExportBio(this Data data, string fileName)
        //{
        //    data.ExportBio(fileName, true);
        //}

        //public static void ExportBio(this Data data, string fileName, bool prepare)
        //{
        //    File.WriteAllText(fileName,
        //        StringCipher.Encrypt((prepare ? data.GetBio() : data).GetXml(), 
        //        "bio"));
        //}

        //public static void ImportBio(this Data data, string fileName)
        //{
        //    ImportBio(data, fileName, true);
        //}

        //public static void ImportBio(this Data data, string fileName, bool clear)
        //{
        //    Data spcData = new Data();
        //    string contents = StringCipher.Decrypt(File.ReadAllText(fileName), "bio");
        //    spcData.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
        //    spcData.Solitary.Investigator = Mayfly.StringCipher.Decrypt(spcData.Solitary.Sign, spcData.Solitary.When.ToString("s"));
        //    spcData.InitializeBio();
        //    spcData.WeightModels.VisualConfirmation = false;
        //    data.WeightModels.Involve(spcData.WeightModels, clear);

        //    data.GrowthModels.VisualConfirmation = false;
        //    data.GrowthModels.Involve(spcData.GrowthModels, clear);
        //    Log.Write("Bio {0} is loaded.", Path.GetFileNameWithoutExtension(fileName));
        //}
    }
}
