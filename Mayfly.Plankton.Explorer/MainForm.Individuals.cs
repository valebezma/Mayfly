using Mayfly.Extensions;
using Mayfly.Wild;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mayfly.Plankton.Explorer
{
    partial class MainForm
    {
        Wild.Survey.DefinitionRow individualSpecies;

        private void UpdateIndTotals()
        {
            labelIndCount.UpdateStatus(spreadSheetInd.VisibleRowCount);
        }

        private void LoadIndLog()
        {
            IsBusy = true;
            spreadSheetInd.StartProcessing(data.Individual.Count, Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();
            foreach (Wild.Survey.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }
            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync();
        }

        private void LoadIndLog(Wild.Survey.DefinitionRow speciesRow)
        {
            individualSpecies = speciesRow;

            IsBusy = true;
            spreadSheetInd.StartProcessing((int)FullStack.Quantity(speciesRow),
                Wild.Resources.Interface.Process.IndividualsProcessing);
            spreadSheetInd.Rows.Clear();

            foreach (Wild.Survey.VariableRow variableRow in data.Variable)
            {
                spreadSheetInd.InsertColumn("Var_" + variableRow.Variable,
                    variableRow.Variable, typeof(double), spreadSheetInd.ColumnCount - 1);
            }

            if (spreadSheetInd.Filter != null) spreadSheetInd.Filter.Close();
            loaderInd.RunWorkerAsync(speciesRow);
        }



        private DataGridViewRow GetIndividualRow(Wild.Survey.IndividualRow individualRow)
        {
            return columnIndID.GetRow(individualRow.ID, true, true);
        }

        private DataGridViewRow GetLine(Wild.Survey.IndividualRow individualRow)
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

        private DataGridViewRow[] GetCardIndividualRows(Wild.Survey.CardRow cardRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (GetIndividualRow(gridRow).LogRow.CardRow == cardRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private DataGridViewRow[] GetLogIndividualRows(Wild.Survey.LogRow logRow)
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>();

            foreach (DataGridViewRow gridRow in spreadSheetInd.Rows)
            {
                if (GetIndividualRow(gridRow).LogRow == logRow)
                {
                    result.Add(gridRow);
                }
            }

            return result.ToArray();
        }

        private Wild.Survey.IndividualRow GetIndividualRow(DataGridViewRow gridRow)
        {
            int ID = (int)gridRow.Cells[columnIndID.Name].Value;
            return data.Individual.FindByID(ID);
        }

        private void SaveIndRow(DataGridViewRow gridRow)
        {
            Wild.Survey.IndividualRow individualRow = GetIndividualRow(gridRow);

            if (individualRow == null) return;

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
                    individualRow.LogRow.Mass = individualRow.LogRow.Mass;
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

            Grade grade = (Grade)gridRow.Cells[columnIndGrade.Name].Value;
            if (grade == null) individualRow.SetGradeNull();
            else individualRow.Grade = grade.Value;

            object comments = gridRow.Cells[columnIndComments.Name].Value;
            if (comments == null) individualRow.SetCommentsNull();
            else individualRow.Comments = (string)comments;

            // Additional variables
            foreach (DataGridViewColumn gridColumn in spreadSheetInd.GetColumns("Var_"))
            {
                Wild.Survey.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                object varValue = gridRow.Cells[gridColumn.Name].Value;

                if (varValue == null)
                {
                    if (variableRow == null) continue;

                    Wild.Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) continue;

                    valueRow.Delete();
                }
                else
                {
                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Wild.Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

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
        }

        private DataGridViewRow UpdateIndividualRow(DataGridViewRow result, Wild.Survey.IndividualRow individualRow)
        {
            // = GetLine(individualRow);

            if (individualRow.LogRow.IsSpcIDNull())
            {
                result.Cells[columnIndSpecies.Index].Value = null;
            }
            else
            {
                result.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.SpeciesRow.Species;
            }

            if (individualRow.IsLengthNull()) { }
            else
            {
                result.Cells[columnIndLength.Index].Value = individualRow.Length;
            }

            if (individualRow.IsMassNull()) { }
            else
            {
                result.Cells[columnIndMass.Index].Value = individualRow.Mass;
            }

            if (individualRow.IsSexNull()) { }
            else
            {
                result.Cells[columnIndSex.Index].Value = (Sex)individualRow.Sex;
            }

            if (individualRow.IsGradeNull()) { }
            else
            {
                result.Cells[columnIndGrade.Index].Value = (Grade)individualRow.Grade;
            }

            if (individualRow.IsCommentsNull()) { }
            else
            {
                result.Cells[columnIndComments.Index].Value = individualRow.Comments;
            }

            SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());

            foreach (Wild.Survey.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (!valueRow.IsValueNull())
                {
                    result.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
                }
            }

            return result;
        }

        private DataGridViewRow UpdateIndividualRow(Wild.Survey.IndividualRow individualRow)
        {
            DataGridViewRow result = GetIndividualRow(individualRow);

            if (individualRow.LogRow.IsSpcIDNull())
            {
                result.Cells[columnIndSpecies.Index].Value = null;
            }
            else
            {
                result.Cells[columnIndSpecies.Index].Value = individualRow.LogRow.SpeciesRow.Species;
            }

            if (individualRow.IsLengthNull()) { }
            else
            {
                result.Cells[columnIndLength.Index].Value = individualRow.Length;
            }

            if (individualRow.IsMassNull())
            {
                //result.Cells[columnIndMass.Index].Style.NullValue =
                //    individualRow.RecoveredMass == 0 ? null :
                //    individualRow.RecoveredMass.ToString(columnIndMass.DefaultCellStyle.Format);
            }
            else
            {
                result.Cells[columnIndMass.Index].Value = individualRow.Mass;
            }

            if (individualRow.IsSexNull()) { }
            else
            {
                result.Cells[columnIndSex.Index].Value = (Sex)individualRow.Sex;
            }

            if (individualRow.IsGradeNull()) { }
            else
            {
                result.Cells[columnIndGrade.Index].Value = (Grade)individualRow.Grade;
            }

            if (individualRow.IsCommentsNull()) { }
            else
            {
                result.Cells[columnIndComments.Index].Value = individualRow.Comments;
            }

            SetCardValue(individualRow.LogRow.CardRow, result, spreadSheetInd.GetInsertedColumns());

            foreach (Wild.Survey.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (!valueRow.IsValueNull())
                {
                    result.Cells[spreadSheetInd.GetColumn("Var_" + valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
                }
            }

            return result;
        }

        private void SetIndividualMassTip(DataGridViewRow gridRow)
        {
            SetIndividualMassTip(gridRow, GetIndividualRow(gridRow));
        }

        private void SetIndividualMassTip(DataGridViewRow gridRow, Wild.Survey.IndividualRow individualRow)
        {
            if (individualRow.IsLengthNull())
            {
                return;
            }

            if (gridRow.Cells[columnIndMass.Index].Value != null)
            {
                return;
            }

            double mass = data.FindMassModel(individualRow.Species).GetValue(individualRow.Length);

            gridRow.Cells[columnIndMass.Index].SetNullValue(double.IsNaN(mass) ?
                Wild.Resources.Interface.Interface.SuggestionUnavailable :
                mass.ToString(columnIndMass.DefaultCellStyle.Format));
        }
    }
}
