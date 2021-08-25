using Mayfly.Mathematics.Charts;
using System;
using System.IO;
using System.Text;
using Mayfly.Wild;
using System.Windows.Forms;
using System.ComponentModel;
using Mayfly.Fish;
using Mayfly.Fish.Explorer;
using System.Data;
using System.Collections.Generic;

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

            data.RefreshBios();

            for (int i = 0; i < data.Species.Count; i++)
            {
                ContinuousBio cbm = data.FindMassModel(data.Species[i].Species);
                bool hasMass = cbm != null && cbm.InternalData.IsRegressionOK;

                ContinuousBio cbg = data.FindGrowthModel(data.Species[i].Species);
                bool hasGrowth = cbg != null && cbg.InternalData.IsRegressionOK;

                if (!hasMass && !hasMass)
                {
                    data.Species.RemoveSpeciesRow(data.Species[i]);
                    i--;
                }
                else
                {
                    for (int j = 0; j < data.Individual.Count; j++)
                    {
                        if (data.Individual[j].Species != data.Species[i].Species) continue;

                        if (!hasMass) data.Individual[j].SetMassNull();
                        if (!hasGrowth) data.Individual[j].SetAgeNull();

                        if (data.Individual[j].IsLengthNull() || (data.Individual[j].IsMassNull() && data.Individual[j].IsAgeNull()))
                        {
                            data.Individual.Rows.RemoveAt(j);
                            j--;
                            continue;
                        }

                        data.Individual[j].SetConsumedMassNull();
                        data.Individual[j].SetEggSizeNull();
                        data.Individual[j].SetFatnessNull();
                        data.Individual[j].SetGonadMassNull();
                        data.Individual[j].SetGonadSampleMassNull();
                        data.Individual[j].SetGonadSampleNull();
                        data.Individual[j].SetPreyNull();
                        data.Individual[j].SetSexNull();
                        data.Individual[j].SetIntermatureNull();
                        data.Individual[j].SetMaturityNull();
                        data.Individual[j].SetRegIDNull();
                        data.Individual[j].SetCommentsNull();
                    }
                }
            }

            for (int i = 0; i < data.Log.Count; i++)
            {
                data.Log[i].SetQuantityNull();
                data.Log[i].SetMassNull();
                data.Log[i].SetIntervalNull();
            }

            for (int i = 0; i < data.Card.Count; i++)
            {
                data.Card[i].SetBankNull();
                data.Card[i].SetChemicalsNull();
                data.Card[i].SetCommentsNull();
                data.Card[i].SetCrossSectionNull();
                data.Card[i].SetDepthNull();
                data.Card[i].SetExactAreaNull();
                data.Card[i].SetExposureNull();
                data.Card[i].SetHeightNull();
                data.Card[i].SetHookNull();
                data.Card[i].SetLabelNull();
                data.Card[i].SetLengthNull();
                data.Card[i].SetMeshNull();
                data.Card[i].SetOpeningNull();
                data.Card[i].SetOrganolepticsNull();
                data.Card[i].SetPhysicalsNull();
                data.Card[i].SetSamplerNull();
                data.Card[i].SetSpanNull();
                data.Card[i].SetSquareNull();
                data.Card[i].SetSubstrateNull();
                data.Card[i].SetVelocityNull();
                data.Card[i].SetVolumeNull();
                data.Card[i].SetWeatherNull();
            }

            if (data.Species.Count == 0)
                throw new ArgumentNullException("Regression data", "Data is unsufficient to be used as a bio.");
            
            return data;
        }

        //public static void ExportBio(this Data data, string filename)
        //{
        //    data.ExportBio(filename, true);
        //}

        //public static void ExportBio(this Data data, string filename, bool prepare)
        //{
        //    File.WriteAllText(filename,
        //        StringCipher.Encrypt((prepare ? data.GetBio() : data).GetXml(), 
        //        "bio"));
        //}

        //public static void ImportBio(this Data data, string filename)
        //{
        //    ImportBio(data, filename, true);
        //}

        //public static void ImportBio(this Data data, string filename, bool clear)
        //{
        //    Data spcData = new Data();
        //    string contents = StringCipher.Decrypt(File.ReadAllText(filename), "bio");
        //    spcData.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(contents)));
        //    spcData.Solitary.Investigator = Mayfly.StringCipher.Decrypt(spcData.Solitary.Sign, spcData.Solitary.When.ToString("s"));
        //    spcData.InitializeBio();
        //    spcData.WeightModels.VisualConfirmation = false;
        //    data.WeightModels.Involve(spcData.WeightModels, clear);

        //    data.GrowthModels.VisualConfirmation = false;
        //    data.GrowthModels.Involve(spcData.GrowthModels, clear);
        //    Log.Write("Bio {0} is loaded.", Path.GetFileNameWithoutExtension(filename));
        //}
    }
}
