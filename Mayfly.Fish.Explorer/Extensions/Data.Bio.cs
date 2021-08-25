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

            data.Stratified.Clear();
            data.FactorValue.Clear();
            data.Factor.Clear();
            data.Intestine.Clear();
            data.Organ.Clear();

            data.InitializeBio();

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

            foreach (Data.IndividualRow individualRow in data.Individual)
            {
                individualRow.SetConsumedMassNull();
                individualRow.SetEggSizeNull();
                individualRow.SetFatnessNull();
                individualRow.SetGonadMassNull();
                individualRow.SetGonadSampleMassNull();
                individualRow.SetGonadSampleNull();
                individualRow.SetPreyNull();
                individualRow.SetSexNull();
                individualRow.SetIntermatureNull();
                individualRow.SetMaturityNull();
            }

            for (int i = 0; i < data.Species.Count; i++)
            {
                if (data.FindGrowthModel(data.Species[i].Species) == null && data.FindMassModel(data.Species[i].Species) == null)
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
