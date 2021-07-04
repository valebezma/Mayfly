using Mayfly.Mathematics.Charts;
using System;
using System.IO;
using System.Text;
using Mayfly.Wild;
using System.Collections.Generic;
using System.Data;
using Mayfly.Extensions;
using Mayfly.Mathematics.Statistics;
using Mayfly.Mathematics.Statistics;
using System.Linq;
using Mayfly.Plankton;
using Mayfly.Plankton.Explorer;
using Meta.Numerics.Statistics;

namespace Mayfly.Extensions
{
    public static class DataExtensionsBio
    {
        //public static void ApplyMassRecoveryModel(this Data data, Data.SpeciesRow speciesRow,
        //    Data.VariableRow variableRow, Regression model)
        //{
        //    data.ApplyMassRecoveryModel(speciesRow.GetIndividualRows(), variableRow, model);
        //}

        //public static void ApplyMassRecoveryModel(this Data data, IList<Data.IndividualRow> individualRows,
        //    Data.VariableRow variableRow, Regression model)
        //{
        //    variableRow = data.Variable.FindByVarName(variableRow.Variable);
        //    foreach (Data.IndividualRow individualRow in individualRows)
        //    {
        //        if (!individualRow.IsMassNull()) continue;
        //        Data.ValueRow valueRoow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);
        //        if (valueRoow == null) continue;
        //        individualRow.Mass = Math.Round(model.Predict(valueRoow.Value), 5);
        //    }
        //}

        //public static List<Data.IndividualRow> GetIndividualsThatAreMeasuredToo(this DataColumn dataColumn,
        //    List<Data.IndividualRow> naturalRows, List<Data.IndividualRow> foodRows)
        //{
        //    List<Data.IndividualRow> result = new List<Data.IndividualRow>();

        //    if (naturalRows.Count == 0) return result;

        //    if (foodRows.Count == 0) return result;

        //    List<object> availableValues = ((Data)naturalRows[0].Table.DataSet).Individual.Columns[dataColumn.ColumnName]
        //        .GetValues(naturalRows, true);

        //    foreach (Data.IndividualRow individualRow in foodRows)
        //    {
        //        if (individualRow.IsNull(dataColumn)) continue;
        //        if (availableValues.Contains(individualRow[dataColumn]))
        //        {
        //            result.Add(individualRow);
        //        }
        //    }

        //    return result;
        //}


        //public static void ApplyMassRecoveryWithModelData(this Data data,
        //    List<Data.IndividualRow> individualRows, DataColumn dataColumn, List<Data.IndividualRow> naturalIndividuals)
        //{
        //    Data naturalData = (Data)naturalIndividuals[0].Table.DataSet;
        //    DataColumn naturalValues = naturalData.Individual.Columns[dataColumn.ColumnName];
        //    List<Sample> samples = naturalData.Individual.MassColumn.GetSamples(naturalValues, naturalIndividuals);

        //    foreach (Data.IndividualRow individualRow in individualRows)
        //    {
        //        if (!individualRow.IsMassNull()) continue;

        //        foreach (Sample sample in samples)
        //        {
        //            if (sample.Name == string.Empty) continue;

        //            if (individualRow[dataColumn.ColumnName].ToString() == sample.Name)
        //            {
        //                if (sample.Count > 1)
        //                {
        //                    individualRow[data.Individual.MassColumn] = Math.Round(sample.Mean, 2);
        //                }
        //                else
        //                {
        //                    individualRow[data.Individual.MassColumn] = Math.Round(sample.Sum(), 2);
        //                }
        //            }
        //        }
        //    }
        //}

        ////public static void RestoreMass(this Data data)
        ////{
        ////    foreach (Data.SpeciesRow speciesRow in data.Species)
        ////    {
        ////        data.RestoreMass(speciesRow);
        ////    }
        ////}

        ////public static void RestoreMass(this Data data, Data.SpeciesRow speciesRow)
        ////{
        ////    foreach (Data.LogRow logRow in speciesRow.GetLogRows())
        ////    {
        ////        if (!logRow.IsMassNull()) continue;

        ////        data.RestoreMass(logRow);
        ////    }
        ////}

        ////public static void RestoreMass(this Data data, Data.LogRow logRow)
        ////{
        ////    double recMass = 0;
        ////    recMass = logRow.Mass;

        ////    if (!double.IsNaN(recMass)) logRow.Mass = recMass;
        ////}

        ////public static void RecalcQuantity(this Data data)
        ////{
        ////    foreach (Data.LogRow logRow in data.Log)
        ////    {
        ////        logRow.Quantity = logRow.GetQuantity();
        ////    }
        ////}




        ///// <summary>
        ///// Recover mass with natural or reference data using straight priority and all available associations
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="referenceData"></param>
        //public static void RecoverMasses(this Data data, Data referenceData)
        //{
        //    foreach (Data.SpeciesRow speciesRow in data.Species)
        //    {
        //        #region Associates

        //        List<Data.SpeciesRow> associates = new List<Data.SpeciesRow>();

        //        Data.SpeciesRow conSpecies = referenceData.Species.FindBySpecies(speciesRow.Species);

        //        if (conSpecies != null)
        //        {
        //            associates.Add(conSpecies);
        //        }

        //        foreach (string associate in Mayfly.Plankton.Explorer.Service.GetAssociates(speciesRow.Species))
        //        {
        //            conSpecies = referenceData.Species.FindBySpecies(associate);

        //            if (conSpecies != null)
        //            {
        //                associates.Add(conSpecies);
        //            }
        //        }

        //        if (associates.Count == 0)
        //        {
        //            Data.SpeciesRow conGenus = referenceData.Species.FindBySpecies(
        //                Species.SpeciesKey.Genus(speciesRow.Species) + " sp.");

        //            if (conGenus != null)
        //            {
        //                associates.Add(conGenus);
        //            }
        //        }

        //        #endregion

        //        if (associates.Count == 0) continue;

        //        List<string> assoc = new List<string>();

        //        foreach (Data.SpeciesRow associate in associates)
        //        {
        //            assoc.Add(associate.Species);
        //        }

        //        List<Data.IndividualRow> unweightedIndividuals = speciesRow.GetUnweightedIndividualRows();

        //        #region Try #1. Recover by length

        //        int measured = 0;

        //        foreach (Data.SpeciesRow associate in associates)
        //        { measured += referenceData.Measured(associate); }

        //        List<Data.IndividualRow> recoverableRows = unweightedIndividuals.GetUnweightedAndMeasuredIndividualRows();
        //        int recoverableCount = recoverableRows.GetCount();

        //        if (measured > 0 && recoverableCount > 0)
        //        {
        //            List<Data.IndividualRow> natureRows = new List<Data.IndividualRow>();

        //            foreach (Data.SpeciesRow associate in associates)
        //            {
        //                natureRows.AddRange(associate.GetIndividualRows().GetWeightedAndMeasuredIndividualRows());
        //            }

        //            Regression model = referenceData.WeightModels.GetInternalScatterplot(assoc.ToArray()).Regression;

        //            //BivariateSample sample = referenceData.GetBivariate(natureRows.ToArray(),
        //            //    referenceData.Individual.LengthColumn, referenceData.Individual.MassColumn);

        //            //sample.Count >= Mayfly.Mathematics.UserSettings.StrongSampleSize)
        //            //{
        //                //Power model = new Power(sample);

        //                if (model != null)
        //                {
        //                    List<Data.IndividualRow> recoveredRows = new List<Data.IndividualRow>();

        //                    foreach (Data.IndividualRow individualRow in unweightedIndividuals)
        //                    {
        //                        if (individualRow.IsLengthNull()) continue;
        //                        individualRow.RecoveredMass = Math.Round(model.Predict(individualRow.Length), 5);
        //                        recoveredRows.Add(individualRow);
        //                    }

        //                    foreach (Data.IndividualRow individualRow in recoveredRows)
        //                    {
        //                        unweightedIndividuals.Remove(individualRow);
        //                    }
        //                }
        //            //}
        //        }

        //        #endregion

        //        #region Try #2. Recover by variable

        //        List<Data.IndividualRow> weightedIndividuals = new List<Data.IndividualRow>();

        //        foreach (Data.SpeciesRow associate in associates)
        //        {
        //            weightedIndividuals.AddRange(associate.GetIndividualRows());
        //        }

        //        foreach (Data.VariableRow variableRow in referenceData.Variable)
        //        {
        //            PowerRegression model = referenceData.SearchMassModel(variableRow, weightedIndividuals.ToArray());

        //            if (model != null)
        //            {
        //                recoverableRows = unweightedIndividuals.GetMeasuredRows(data.Variable.FindByVarName(variableRow.Variable));

        //                if (recoverableRows.Count > 0)
        //                {
        //                    List<Data.IndividualRow> recoveredRows = new List<Data.IndividualRow>();

        //                    foreach (Data.IndividualRow individualRow in recoverableRows)
        //                    {
        //                        if (!individualRow.IsMassNull()) continue;
        //                        Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID,
        //                            data.Variable.FindByVarName(variableRow.Variable).ID);
        //                        if (valueRow == null) continue;
        //                        individualRow.RecoveredMass = Math.Round(model.Predict(valueRow.Value), 5);
        //                        recoveredRows.Add(individualRow);
        //                    }

        //                    foreach (Data.IndividualRow individualRow in recoveredRows)
        //                    {
        //                        unweightedIndividuals.Remove(individualRow);
        //                    }
        //                }
        //            }
        //        }

        //        #endregion

        //        #region Try #3. Recover by individual category

        //        DataColumn[] categorialVariables = new DataColumn[] { 
        //            data.Individual.GradeColumn 
        //        };

        //        foreach (DataColumn dataColumn in categorialVariables)
        //        {
        //            List<Data.IndividualRow> naturalRows = new List<Data.IndividualRow>();

        //            foreach (Data.SpeciesRow associate in associates)
        //            {
        //                naturalRows.AddRange(associate.GetIndividualRows()
        //                    .GetMeasuredRows(referenceData.Individual.Columns[dataColumn.ColumnName]));
        //            }

        //            recoverableRows = dataColumn.GetIndividualsThatAreMeasuredToo(naturalRows,
        //                unweightedIndividuals.GetMeasuredRows(dataColumn));

        //            if (recoverableRows.Count > 0)
        //            {
        //                List<Sample> samples = referenceData.Individual.MassColumn.GetSamples(
        //                    referenceData.Individual.Columns[dataColumn.ColumnName],
        //                    naturalRows);

        //                foreach (Data.IndividualRow individualRow in recoverableRows)
        //                {
        //                    if (!individualRow.IsMassNull()) continue;

        //                    foreach (Sample sample in samples)
        //                    {
        //                        if (sample.Name == string.Empty) continue;

        //                        if (sample.Name == individualRow[dataColumn.ColumnName].ToString())
        //                        {
        //                            if (sample.Count > 1)
        //                            {
        //                                individualRow.RecoveredMass = Math.Round(sample.Mean, 5);
        //                            }
        //                            else
        //                            {
        //                                individualRow.RecoveredMass = Math.Round(sample.Sum(), 5);
        //                            }
        //                        }
        //                    }
        //                }

        //                foreach (Data.IndividualRow individualRow in recoverableRows)
        //                {
        //                    unweightedIndividuals.Remove(individualRow);
        //                }
        //            }
        //        }

        //        #endregion

        //        #region Try #4. Recover by raw average individual mass

        //        {
        //            int weighted = 0;

        //            foreach (Data.SpeciesRow associate in associates)
        //            {
        //                weighted += referenceData.Weighted(associate);
        //            }

        //            recoverableCount = unweightedIndividuals.GetCount() + speciesRow.AbstractIndividuals();

        //            if (weighted > 0 && recoverableCount > 0)
        //            {
        //                List<Data.IndividualRow> naturalRows = new List<Data.IndividualRow>();

        //                foreach (Data.SpeciesRow associate in associates)
        //                {
        //                    naturalRows.AddRange(associate.GetWeightedIndividualRows());
        //                }

        //                double avgMass = Math.Round(naturalRows.GetAverageMass(), 5);

        //                foreach (Data.IndividualRow individualRow in unweightedIndividuals)
        //                {
        //                    individualRow.RecoveredMass = avgMass;
        //                }

        //                unweightedIndividuals.Clear();
        //            }
        //        }

        //        #endregion
        //    }

        //    //data.RestoreMass();
        //}

        //public static void RecoverMasses(this Data data, string referencePath)
        //{
        //    Data referenceData = new Data();
        //    referenceData.Read(referencePath);
        //    data.RecoverMasses(referenceData);
        //}



        //public static void ExportBio(this Data data1, string fileName)
        //{
        //    Data data = data1.Copy();

        //    data.FactorValue.Clear();
        //    data.Factor.Clear();

        //    for (int i = 0; i < data.Card.Count; i++)
        //    {
        //        if (data.Card[i].IsSignNull())
        //        {
        //            data.Card.Rows.RemoveAt(i);
        //            i--;
        //        }
        //    }

        //    if (data.Card.Count == 0)
        //        throw new ArgumentNullException("Unsigned data",
        //            "Cards do not contain signed data.");

        //    for (int i = 0; i < data.Individual.Count; i++)
        //    {
        //        if (data.Individual[i].IsMassNull())
        //        {
        //            data.Individual.Rows.RemoveAt(i);
        //            i--;
        //        }
        //    }

        //    if (data.Species.Count == 0) // if some regressions are available - save
        //        throw new ArgumentNullException("Regression data",
        //            "Data is unsufficient to be used as bio.");

        //    File.WriteAllText(fileName, StringCipher.Encrypt(data.GetXml(), "bio"));
        //}
    }
}
