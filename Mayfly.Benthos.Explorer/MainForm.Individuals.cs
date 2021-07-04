using Mayfly.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Wild;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        Data.SpeciesRow individualSpecies;

        private void LoadIndLog()
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();
            foreach (Data.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }
            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync();
        }

        private void LoadIndLog(Data.SpeciesRow speciesRow)
        {
            individualSpecies = speciesRow;

            IsBusy = true;
            spreadSheetInd.StartProcessing(FullStack.Quantity(speciesRow),
                Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            foreach (Data.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync(speciesRow);
        }

        private Data.IndividualRow[] GetIndividuals(IList rows)
        {
            spreadSheetInd.EndEdit();
            List<Data.IndividualRow> result = new List<Data.IndividualRow>();

            foreach (DataGridViewRow gridRow in rows)
            {
                if (!gridRow.Visible) continue;
                if (gridRow.IsNewRow) continue;
                Data.IndividualRow individualRow = IndividualRow(gridRow);
                if (individualRow == null) continue;

                result.Add(individualRow);
            }

            return result.ToArray();
        }


        private DataGridViewRow GetLine(Data.IndividualRow individualRow)
        {
            DataGridViewRow result = new DataGridViewRow();
            result.CreateCells(spreadSheetInd);
            result.Cells[columnIndID.Index].Value = individualRow.ID;
            UpdateIndividualRow(result, individualRow);
            //SetIndividualAgeTip(result, individualRow);
            //SetIndividualMassTip(result, individualRow);
            SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());
            return result;
        }


        private DataGridViewRow FindIndividualRow(Data.IndividualRow individualRow)
        {
            return columnIndID.GetRow(individualRow.ID, true, true);
        }

        private Data.IndividualRow IndividualRow(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[columnIndID.Name].Value == null) { return null; }
            else {
                int ID = (int)gridRow.Cells[columnIndID.Name].Value;
                return data.Individual.FindByID(ID);
            }
        }

        private Data.IndividualRow SaveIndRow(DataGridViewRow gridRow)
        {
            Data.IndividualRow individualRow = IndividualRow(gridRow);

            if (individualRow == null) return null;

            object length = gridRow.Cells[columnIndLength.Name].Value;
            if (length == null) individualRow.SetLengthNull();
            else individualRow.Length = (double)length;

            object mass = gridRow.Cells[columnIndMass.Name].Value;
            if (mass == null)
            {
                individualRow.SetMassNull();
                individualRow.LogRow.SetMassNull();
            }
            else
            {
                if (individualRow.LogRow.IsMassNull())
                {
                    individualRow.LogRow.Mass = individualRow.LogRow.DetailedMass;
                }
                else
                {
                    if (!individualRow.IsMassNull()) individualRow.LogRow.Mass -= individualRow.Mass;
                    individualRow.LogRow.Mass += (double)mass;
                }

                UpdateLogRow(individualRow.LogRow);

                individualRow.Mass = (double)mass;
            }

            Sex sex = (Sex)gridRow.Cells[columnIndSex.Name].Value;
            if (sex == null) individualRow.SetSexNull();
            else individualRow.Sex = sex.Value;

            object instar = gridRow.Cells[columnIndInstar.Name].Value;
            if (instar == null) individualRow.SetInstarNull();
            else individualRow.Instar = (int)instar;

            Grade grade = (Grade)gridRow.Cells[columnIndGrade.Name].Value;
            if (grade == null) individualRow.SetGradeNull();
            else individualRow.Grade = grade.Value;

            object comments = gridRow.Cells[columnIndComments.Name].Value;
            if (comments == null) individualRow.SetCommentsNull();
            else individualRow.Comments = (string)comments;

            // Additional variables
            foreach (DataGridViewColumn gridColumn in spreadSheetInd.GetColumns("Var_"))
            {
                Data.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                object varValue = gridRow.Cells[gridColumn.Name].Value;

                if (varValue == null)
                {
                    if (variableRow == null) continue;

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) continue;

                    valueRow.Delete();
                }
                else
                {
                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Data.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null)
                    {
                        data.Value.AddValueRow(individualRow, variableRow, (double)varValue);
                    }
                    else
                    {
                        valueRow.Value = (double)varValue;
                    }
                }
            }

            RememberChanged(individualRow.LogRow.CardRow);

            return individualRow;
        }

        private void UpdateIndividualRow(Data.IndividualRow individualRow)
        {
            UpdateIndividualRow(FindIndividualRow(individualRow), individualRow);
        }

        private DataGridViewRow UpdateIndividualRow(DataGridViewRow result, Data.IndividualRow individualRow)
        {
            result.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.IsSpcIDNull() ? null : individualRow.LogRow.SpeciesRow.Species;
            result.Cells[ColumnIndFrequency.Index].Value = individualRow.IsFrequencyNull() ? null : (object)individualRow.Frequency;
            result.Cells[columnIndLength.Index].Value = individualRow.IsLengthNull() ? null : (object)individualRow.Length;
            result.Cells[columnIndMass.Index].Value = individualRow.IsMassNull() ? null : (object)individualRow.Mass;
            result.Cells[columnIndSex.Index].Value = individualRow.IsSexNull() ? null : (Sex)individualRow.Sex;
            result.Cells[columnIndGrade.Index].Value = individualRow.IsGradeNull() ? null : (Grade)individualRow.Grade;
            result.Cells[columnIndInstar.Index].Value = individualRow.IsInstarNull() ? null : (object)individualRow.Instar;
            result.Cells[columnIndComments.Index].Value = individualRow.IsCommentsNull() ? null : individualRow.Comments;

            SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());

            foreach (Data.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (valueRow.IsValueNull()) continue; 
                result.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
            }

            return result;
        }

        private DataGridViewRow[] IndividualRows(Data.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (IndividualRow(gridRow).LogRow.CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private DataGridViewRow[] IndividualRows(Data.LogRow logRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (IndividualRow(gridRow).LogRow == logRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }
    }
}
