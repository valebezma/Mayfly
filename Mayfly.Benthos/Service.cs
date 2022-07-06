using Mayfly.Waters;
using Mayfly.Wild;
using System;
using System.Resources;
using System.Windows.Forms;

namespace Mayfly.Benthos
{
    public abstract class Service
    {
        public static double GetStrate(double length)
        {
            return Math.Floor(length);
        }

        static bool instarHandling = false;

        public static void HandleInstarInput(DataGridViewCellEventArgs e, 
            DataGridViewColumn columnGrade, DataGridViewColumn columnInstar)
        {
            if (instarHandling) return;

            instarHandling = true;
 
            DataGridView grid = columnGrade.DataGridView;

            if (e.ColumnIndex == columnGrade.Index)
            {
                bool emptyValue = grid[columnGrade.Index, e.RowIndex].Value == null;

                if (emptyValue)
                {
                    grid[columnInstar.Index, e.RowIndex].ReadOnly = false;
                }
                else
                {
                    bool allowInstar = ((Grade)grid[columnGrade.Index, e.RowIndex].Value).Value == 1;
                    grid[columnInstar.Index, e.RowIndex].ReadOnly = !allowInstar;
                    if (!allowInstar) grid[columnInstar.Index, e.RowIndex].Value = null;
                }
            }

            if (e.ColumnIndex == columnInstar.Index)
            {
                bool emptyValue = grid[columnInstar.Index, e.RowIndex].Value == null;

                if (!emptyValue) grid[columnGrade.Index, e.RowIndex].Value = (Grade)1;
            }

            instarHandling = false;
        }

        //public static void RecoverWeight(IEnumerable gridRows, DataGridViewColumn columnMass,
        //    string species, string[] variables)
        //{
        //    SpreadSheet spreadSheet = (SpreadSheet)columnMass.DataGridView;
        //    MassRecovery massRecovery = new MassRecovery();
        //    massRecovery.SetFriendlyDesktopLocation(columnMass);

        //    massRecovery.OriginalSpecies = species;
        //    massRecovery.OriginalVars.AddRange(variables);

        //    if (massRecovery.ShowDialog(spreadSheet.FindForm()) == DialogResult.OK)
        //    {
        //        if (massRecovery.ApprovedModel == null) return;

        //        DataGridViewColumn selectedColumn = spreadSheet.GetColumn(massRecovery.SelectedVariable);

        //        if (selectedColumn == null)
        //        {
        //            throw new NotImplementedException("Ask for SelectedColumn");
        //        }

        //        foreach (DataGridViewRow gridRow in gridRows)
        //        {
        //            if (gridRow.Cells[selectedColumn.Index].Value == null) continue;
        //            if (gridRow.Cells[columnMass.Index].Value != null) continue;
        //            gridRow.Cells[columnMass.Index].Value =
        //                massRecovery.ApprovedModel.Regression.GetValue(
        //                (double)gridRow.Cells[selectedColumn.Index].Value);
        //        }
        //    }
        //}

        //public static void RecoverWeight(IEnumerable gridRows, DataGridViewColumn columnMass,
        //    DataGridViewColumn columnSpecies)
        //{
        //    SpreadSheet spreadSheet = (SpreadSheet)columnSpecies.DataGridView;
        //    MassRecovery massRecovery = new MassRecovery();
        //    massRecovery.SetFriendlyDesktopLocation(columnMass);

        //    string[] species = columnSpecies.GetStrings(true).ToArray();
        //    if (species.Length == 1)
        //    {
        //        massRecovery.OriginalSpecies = species[0];
        //    }

        //    foreach (DataGridViewColumn gridColumn in spreadSheet.GetVisibleColumns())
        //    {
        //        if (gridColumn == columnMass) continue;
        //        if (gridColumn.ValueType == typeof(double))
        //            massRecovery.OriginalVars.Add(gridColumn.HeaderText);
        //    }

        //    if (massRecovery.ShowDialog(columnSpecies.DataGridView.FindForm()) == DialogResult.OK)
        //    {
        //        if (massRecovery.ApprovedModel == null) return;

        //        DataGridViewColumn selectedColumn = spreadSheet.GetColumn(massRecovery.SelectedVariable);

        //        if (selectedColumn == null)
        //        {
        //            throw new NotImplementedException("Ask for SelectedColumn");
        //        }

        //        foreach (DataGridViewRow gridRow in gridRows)
        //        {
        //            if (gridRow.Cells[selectedColumn.Index].Value == null) continue;
        //            if (gridRow.Cells[columnMass.Index].Value != null) continue;
        //            gridRow.Cells[columnMass.Index].Value =
        //                massRecovery.ApprovedModel.Regression.GetValue(
        //                (double)gridRow.Cells[selectedColumn.Index].Value);
        //        }
        //    }
        //}
    }
}
