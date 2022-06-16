using Mayfly.Benthos;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mayfly.Mathematics.Charts;
using System.IO;
using System.Text;
using Mayfly.Wild;
using Mayfly.Species;

namespace Mayfly.Extensions
{
    public static class DataExtensionsBio
    {
        public static void ApplyMassRecoveryModel(this Data data, Data.SpeciesRow speciesRow, Data.VariableRow variableRow, Regression model)
        {
            data.ApplyMassRecoveryModel(speciesRow.GetIndividualRows(), variableRow, model);
        }

        public static void ApplyMassRecoveryModel(this Data data, IList<Data.IndividualRow> individualRows, Data.VariableRow variableRow, Regression model)
        {
            variableRow = data.Variable.FindByVarName(variableRow.Variable);
            foreach (Data.IndividualRow individualRow in individualRows)
            {
                if (!individualRow.IsMassNull()) continue;
                Data.ValueRow valueRoow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);
                if (valueRoow == null) continue;
                individualRow.Mass = Math.Round(model.Predict(valueRoow.Value), 2);
            }
        }

        public static List<Data.IndividualRow> GetIndividualsThatAreMeasuredToo(this DataColumn dataColumn, List<Data.IndividualRow> naturalRows, List<Data.IndividualRow> foodRows)
        {
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            if (naturalRows.Count == 0) return result;

            if (foodRows.Count == 0) return result;

            List<object> availableValues = ((Data)naturalRows[0].Table.DataSet).Individual.Columns[dataColumn.ColumnName]
                .GetValues(naturalRows, true);

            foreach (Data.IndividualRow individualRow in foodRows)
            {
                if (individualRow.IsNull(dataColumn)) continue;
                if (availableValues.Contains(individualRow[dataColumn]))
                {
                    result.Add(individualRow);
                }
            }

            return result;
        }


        public static void ApplyMassRecoveryWithModelData(this Data data, List<Data.IndividualRow> individualRows, DataColumn dataColumn, List<Data.IndividualRow> naturalIndividuals)
        {
            Data naturalData = (Data)naturalIndividuals[0].Table.DataSet;
            DataColumn naturalValues = naturalData.Individual.Columns[dataColumn.ColumnName];
            List<Sample> samples = naturalData.Individual.MassColumn.GetSamples(naturalValues, naturalIndividuals);

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                if (!individualRow.IsMassNull()) continue;

                foreach (Sample sample in samples)
                {
                    if (sample.Name == string.Empty) continue;

                    if (individualRow[dataColumn.ColumnName].ToString() == sample.Name)
                    {
                        if (sample.Count > 1)
                        {
                            individualRow[data.Individual.MassColumn] = Math.Round(sample.Mean, 2);
                        }
                        else
                        {
                            individualRow[data.Individual.MassColumn] = Math.Round(sample.Sum(), 2);
                        }
                    }
                }
            }
        }



        public static void RestoreMass(this Data data)
        {
            foreach (Data.LogRow logRow in data.Log)
            {
                if (!logRow.IsMassNull()) continue;

                logRow.Mass = logRow.DetailedMass;
            }
        }

        public static void RecalcQuantity(this Data data)
        {
            foreach (Data.LogRow logRow in data.Log)
            {
                if (!logRow.IsQuantityNull()) continue;

                logRow.Quantity = logRow.DetailedQuantity;
            }
        }

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

        //        List<SpeciesKey.SpeciesRow> associates = new List<SpeciesKey.SpeciesRow>();

        //        SpeciesKey.SpeciesRow conSpecies = referenceData.Species.FindBySpecies(speciesRow.Species);

        //        if (conSpecies != null)
        //        {
        //            associates.Add(conSpecies);
        //        }

        //        foreach (string associate in Mayfly.Benthos.Explorer.Service.GetAssociates(speciesRow.Species))
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

        //        foreach (SpeciesKey.SpeciesRow associate in associates)
        //        {
        //            measured += referenceData.GetStack().Measured(associate);
        //        }

        //        List<Data.IndividualRow> recoverableRows = unweightedIndividuals.GetUnweightedAndMeasuredIndividualRows();
        //        int recoverableCount = recoverableRows.GetCount();

        //        if (measured > 0 && recoverableCount > 0)
        //        {
        //            List<Data.IndividualRow> natureRows = new List<Data.IndividualRow>();

        //            foreach (Data.SpeciesRow associate in associates)
        //            {
        //                natureRows.AddRange(associate.GetIndividualRows().GetWeightedAndMeasuredIndividualRows());
        //            }

        //            //Regression model = referenceData.FindMassModel(assoc.ToArray()).Regression;

        //            ////BivariateSample sample = referenceData.GetBivariate(natureRows.ToArray(),
        //            ////    referenceData.Individual.LengthColumn, referenceData.Individual.MassColumn);

        //            ////if (sample.Count >= Mayfly.Mathematics.UserSettings.StrongSampleSize)
        //            ////{
        //            ////    Power model = new Power(sample);

        //            //    if (model != null)
        //            //    {
        //            //        List<Data.IndividualRow> recoveredRows = new List<Data.IndividualRow>();

        //            //        foreach (Data.IndividualRow individualRow in unweightedIndividuals)
        //            //        {
        //            //            if (individualRow.IsLengthNull()) continue;
        //            //            individualRow.Mass = Math.Round(model.Predict(individualRow.Length), 5);
        //            //            recoveredRows.Add(individualRow);
        //            //        }

        //            //        foreach (Data.IndividualRow individualRow in recoveredRows)
        //            //        {
        //            //            unweightedIndividuals.Remove(individualRow);
        //            //        }
        //            //    }
        //            ////}
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
        //            Power model = referenceData.SearchMassModel(variableRow, weightedIndividuals.ToArray());

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
        //                        individualRow.Mass = Math.Round(model.Predict(valueRow.Value), 5);
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
        //                                individualRow.Mass = Math.Round(sample.Mean, 5);
        //                            }
        //                            else
        //                            {
        //                                individualRow.Mass = Math.Round(sample.Sum(), 5);
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
        //                weighted += referenceData.GetStack().Weighted(associate);
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
        //                    individualRow.Mass = avgMass;
        //                }

        //                unweightedIndividuals.Clear();
        //            }
        //        }

        //        #endregion
        //    }

        //    data.RestoreMass();
        //}

        public static Power SearchMassModel(this Data data, Data.VariableRow variableRow, Data.IndividualRow[] individualRows)
        {
            variableRow = data.Variable.FindByVarName(variableRow.Variable);
            BivariateSample bivariateSample = new BivariateSample();

            foreach (Data.IndividualRow individualRow in individualRows)
            {
                if (individualRow.IsMassNull()) continue;
                Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);
                if (valueRow == null) continue;
                bivariateSample.Add(valueRow.Value, individualRow.Mass);
            }

            if (bivariateSample.Count < Mathematics.UserSettings.RequiredSampleSize)
                return null;

            bivariateSample.X.Name = variableRow.Variable;
            bivariateSample.Y.Name = Benthos.Resources.Reports.Caption.Mass;

            return bivariateSample.Count > 10 ? new Power(bivariateSample) : null;
        }





        public static int Quantity(this Data.IndividualDataTable individual, Data.SpeciesRow speciesRow, Data.VariableRow variableRow)
        {
            return individual.Quantity(speciesRow.Species, variableRow);
        }

        public static int Quantity(this Data.IndividualDataTable individual, string species, Data.VariableRow variableRow)
        {
            Data.SpeciesRow speciesRow = ((Data)individual.DataSet).Species.FindBySpecies(species);



            int result = 0;
            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                result += individual.Quantity(logRow, variableRow);
            }
            return result;
        }

        public static int Quantity(this Data.IndividualDataTable individual, Data.LogRow logRow, Data.VariableRow variableRow)
        {
            int result = 0;
            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (((Data)individual.DataSet).Value.FindByIndIDVarID(individualRow.ID, variableRow.ID) == null) continue;
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }
            return result;
        }

        public static int Unweighted(this Data.IndividualDataTable individual, Data.SpeciesRow speciesRow, Data.VariableRow variableRow)
        {
            return individual.Unweighted(speciesRow.Species, variableRow);
        }

        public static int Unweighted(this Data.IndividualDataTable individual, string species, Data.VariableRow variableRow)
        {
            Data.SpeciesRow speciesRow = ((Data)individual.DataSet).Species.FindBySpecies(species);



            int result = 0;
            foreach (Data.LogRow logRow in speciesRow.GetLogRows())
            {
                result += individual.Unweighted(logRow, variableRow);
            }
            return result;
        }

        public static int Unweighted(this Data.IndividualDataTable individual, Data.LogRow logRow, Data.VariableRow variableRow)
        {
            int result = 0;
            foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
            {
                if (((Data)individual.DataSet).Value.FindByIndIDVarID(individualRow.ID, variableRow.ID) == null) continue;
                if (!individualRow.IsMassNull()) continue;
                result += individualRow.IsFrequencyNull() ? 1 : individualRow.Frequency;
            }
            return result;
        }

        

        public static Data GetBenthosBio(this Data data1)
        {
            Data data = (Data)((DataSet)data1).Copy();

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
            
            data.FactorValue.Clear();
            data.Factor.Clear();

            for (int i = 0; i < data.Individual.Count; i++)
            {
                if (!data.Individual[i].IsGradeNull() && data.Individual[i].Grade != 1)
                {
                    data.Individual.Rows.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < data.Individual.Count; i++)
            {
                if (data.Individual[i].IsMassNull())
                {
                    data.Individual.Rows.RemoveAt(i);
                    i--;
                }
            }

            SpeciesKey.SpeciesRow[] spclist = data.GetStack().GetSpecies();

            for (int i = 0; i < spclist.Length; i++)
            {
                SpeciesKey.SpeciesRow speciesRow = spclist[i];

                //if (speciesRow.Quantity < 5)
                //{
                //    data.Species.RemoveSpeciesRow(speciesRow);
                //    i--;
                //    continue;
                //}

                List<Data.IndividualRow> wRows = speciesRow.GetWeightedIndividualRows();

                if (wRows.Count == 0) continue;

                string d = "Ширина головной капсулы, мм";
                string w = "Масса, мг";
                List<Data.IndividualRow> dRows = wRows.GetMeasuredRows(d);
                BivariateSample dSample = new BivariateSample(d, w);
                foreach (Data.IndividualRow dRow in dRows)
                {
                    dSample.Add(data.GetIndividualValue(dRow, d), dRow.Mass);
                }

                //if (dSample.Count < 5)
                //{
                //    data.Species.RemoveSpeciesRow(speciesRow);
                //    i--;
                //    continue;
                //}

                //try
                //{
                //    Power dw = new Power(dSample);

                //    if (dw.Slope < 2 || dw.Slope > 4)
                //    {
                //        data.Species.RemoveSpeciesRow(speciesRow);
                //        i--;
                //        continue;
                //    }
                //}
                //catch
                //{
                //        data.Species.RemoveSpeciesRow(speciesRow);
                //        i--;
                //        continue;

                //}

                //Benthos.Explorer.HeadSample TotalSample = new Benthos.Explorer.HeadSample(wRows, "Обобщенная выборка", "Общ");

                //if (TotalSample.dw == null)
                //{
                //    data.Species.RemoveSpeciesRow(speciesRow);
                //    i--;
                //    continue;
                //}
            }

            if (data.Species.Count == 0)
                throw new ArgumentNullException("Regression data", "Data is unsufficient to be used as bio.");

            return data;
        }
    }
}

