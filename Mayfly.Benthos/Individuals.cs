using Mayfly.Wild;
using Mayfly.Wild.Controls;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Controls;
using static Mayfly.Wild.UserSettings;
using static Mayfly.Wild.SettingsReader;
using static Mayfly.UserSettings;
using static Mayfly.Waters.UserSettings;

namespace Mayfly.Benthos
{
    public partial class Individuals : Form
    {
        #region Properties

        public Wild.Survey.LogRow LogRow;

        private Wild.Survey Data { get; set; }

        public bool IsChanged;

        public bool ChangesWereMade;

        public DataGridViewRow LogLine;

        public double Mass
        {
            get
            {
                if (numericUpDownMass.Enabled)
                {
                    return (double)numericUpDownMass.Value;
                }
                else
                {
                    return double.NaN;
                }
            }

            set
            {
                if (double.IsNaN(value))
                {
                    numericUpDownMass.Enabled = false;
                }
                else
                {
                    numericUpDownMass.Enabled = true;
                    numericUpDownMass.Value = (decimal)value;
                }
            }
        }

        public double DetailedMass
        {
            get
            {
                double result = 0;

                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    if (gridRow.Cells[ColumnMass.Index].Value == null) continue;

                    if (gridRow.Cells[ColumnMass.Index].Value is double @double)
                    {
                        int ic = 1;

                        if (gridRow.Cells[ColumnFrequency.Index].Value != null)
                            ic = (int)gridRow.Cells[ColumnFrequency.Index].Value;

                        result += ic * @double;
                    }
                }

                return Math.Round(result, 3);
            }
        }

        public int Quantity
        {
            get
            {
                return (int)numericUpDownQuantity.Value;
            }

            set
            {
                numericUpDownQuantity.Value = (decimal)value;
            }
        }

        public int DetailedQuantity
        {
            get
            {
                int result = 0;

                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    if (gridRow.Cells[ColumnFrequency.Index].Value == null)
                    {
                        result++;
                    }
                    else
                    {
                        result += (int)gridRow.Cells[ColumnFrequency.Index].Value;
                    }
                }

                return result;
            }
        }

        private bool IsThereUnsavedIndividuals
        {
            get
            {
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.IsNewRow) continue;

                    if (gridRow.Visible)
                    {
                        if (gridRow.Cells[ColumnID.Index].Value == null)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        private DataGridViewColumn SelectedColumn;

        public string[] AdditionalVariables
        {
            get
            {
                List<string> result = new List<string>(AddtVariables);

                foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                {
                    if (result.Contains(gridColumn.HeaderText)) continue;
                    if (string.IsNullOrWhiteSpace(gridColumn.HeaderText)) continue;
                    result.Add(gridColumn.HeaderText);
                }

                return result.ToArray();
            }
        }

        public string[] CurrentVariables
        {
            get
            {
                List<string> result = new List<string>();

                foreach (DataGridViewColumn gridColumn in spreadSheetLog.Columns)
                {
                    if (!gridColumn.Visible) continue;
                    if (string.IsNullOrWhiteSpace(gridColumn.Name)) continue;
                    result.Add(gridColumn.Name);
                }

                return result.ToArray();
            }
        }

        private double PrevDetailedMass;

        private int PrevDetailedQuantity;

        DataGridViewColumn LogColumnSpecies { get; set; }

        DataGridViewColumn LogColumnQuantity { get; set; }

        DataGridViewColumn LogColumnMass { get; set; }

        #endregion

        BackgroundWorker okLigher;



        private Individuals()
        {
            InitializeComponent();

            ColumnLength.ValueType = typeof(double);
            ColumnMass.ValueType = typeof(double);
            ColumnSex.ValueType = typeof(Sex);
            ColumnGrade.ValueType = typeof(Grade);
            ColumnInstar.ValueType = typeof(int);
            ColumnFrequency.ValueType = typeof(int);
            ColumnComments.ValueType = typeof(string);

            AddVariableMenuItems();
            InsertCurrentVariableColumns();

            numericUpDownMass.Enabled =
                numericUpDownQuantity.Enabled = 
                !FixTotals;
        }

        public Individuals(Wild.Survey.LogRow logRow) : this()
        {
            LogRow = logRow;
            Data = (Wild.Survey)LogRow.Table.DataSet;

            Text = string.Format(Wild.Resources.Interface.Interface.IndLog,
                    logRow.IsDefIDNull() ? Species.Resources.Interface.UnidentifiedTitle :
                    logRow.DefinitionRow.Taxon);

            if (!LogRow.IsMassNull())
            {
                Mass = LogRow.Mass;
            }

            if (!LogRow.IsQuantityNull())
            {
                Quantity = LogRow.Quantity;
            }

            if (!LogRow.IsCommentsNull())
            {
                textBoxComments.Text = LogRow.Comments;
            }

            Wild.Survey.IndividualRow[] individualRows = LogRow.GetIndividualRows();


            // If no individuals described 
            if (individualRows.Length == 0)
            {
                // but mass is known and quantity equals 1
                if (!LogRow.IsQuantityNull() && LogRow.Quantity == 1 && !LogRow.IsMassNull())
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetLog);
                    gridRow.Cells[ColumnMass.Index].Value = LogRow.Mass;
                    spreadSheetLog.Rows.Add(gridRow);
                    ChangesWereMade = true;
                }
            }
            else
            {
                foreach (Wild.Survey.VariableRow variableRow in Data.Variable.Rows)
                {
                    spreadSheetLog.InsertColumn(variableRow.Variable, spreadSheetLog.ColumnCount - 1);
                }

                foreach (Wild.Survey.IndividualRow individualRow in individualRows)
                {
                    InsertIndividualRow(individualRow);
                }

                ColumnFrequency.Visible = !spreadSheetLog.IsEmpty(ColumnFrequency);
                //ColumnLength.Visible = !spreadSheetLog.IsEmpty(ColumnLength);
                ColumnSex.Visible = !spreadSheetLog.IsEmpty(ColumnSex);
                ColumnGrade.Visible = !spreadSheetLog.IsEmpty(ColumnGrade);
                ColumnInstar.Visible = !spreadSheetLog.IsEmpty(ColumnInstar);

                // Remove newly inserted but empty columns
                foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                {
                    if (spreadSheetLog.IsEmpty(gridColumn))
                    {
                        Width -= gridColumn.Width;
                        spreadSheetLog.Columns.Remove(gridColumn);
                    }
                }
            }

            spreadSheetLog.CurrentCell = spreadSheetLog.FirstClearCell();
            
            UpdateTotals();

            IsChanged = false;
        }



        #region Methods

        public void SetColumns(DataGridViewColumn columnSpecies,
            DataGridViewColumn columnQuantity, DataGridViewColumn columnMass)
        {
            LogColumnSpecies = columnSpecies;
            LogColumnQuantity = columnQuantity;
            LogColumnMass = columnMass;
        }

        private void UpdateTotals()
        {
            double MassDifference = DetailedMass - PrevDetailedMass;
            int QuantityDifference = DetailedQuantity - PrevDetailedQuantity;

            PrevDetailedMass = DetailedMass;
            PrevDetailedQuantity = DetailedQuantity;

            if (AutoIncreaseBio)
            {
                if (!double.IsNaN(DetailedMass) && DetailedMass > Mass)
                {
                    Mass = DetailedMass;
                }

                if (DetailedQuantity > Quantity)
                {
                    Quantity = DetailedQuantity;
                }
            }
            else
            {
                pictureBoxWarningMass.Visible = (DetailedMass > Mass);

                if (QuantityDifference != 0 && DetailedQuantity == Quantity)
                {
                    okLigher = new BackgroundWorker();
                    okLigher.DoWork += okLigher_DoWork;
                    okLigher.RunWorkerCompleted += okLigher_RunWorkerCompleted;

                    pictureBoxWarningQuantity.Image = Pictogram.Check;
                    pictureBoxWarningQuantity.Visible = true;
                    okLigher.RunWorkerAsync();
                }
                else
                {
                    if (okLigher == null || !okLigher.IsBusy)
                    {
                        pictureBoxWarningQuantity.Visible = (DetailedQuantity > Quantity);
                    }
                }
            }

            if (AutoDecreaseBio && MassDifference < 0)
            {
                Mass += MassDifference;
            }

            if (AutoDecreaseBio && QuantityDifference < 0)
            {
                Quantity += QuantityDifference;
            }
        }

        void okLigher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBoxWarningQuantity.Image = Pictogram.Warning;
            pictureBoxWarningQuantity.Visible = (DetailedQuantity > Quantity);
        }

        void okLigher_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
        }

        public EventHandler Updater;

        private void ClearEmptyRows()
        {
            foreach (DataGridViewRow Row in spreadSheetLog.Rows)
            {
                if (Row.IsNewRow) continue;

                if (spreadSheetLog.IsEmpty(Row))
                {
                    Clear(Row);
                    spreadSheetLog.Rows.Remove(Row);
                }
            }
        }

        private void Clear(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnID.Index].Value == null) return;
            Wild.Survey.IndividualRow individualRow = Data.Individual.FindByID(
                (int)gridRow.Cells[ColumnID.Index].Value);
            if (individualRow == null) return;
            individualRow.Delete();
        }

        private void Clear(DataGridViewColumn gridColumn)
        {
            for (int i = 0; i < spreadSheetLog.RowCount; i++)
            {
                spreadSheetLog[gridColumn.Index, i].Value = null;
            }
        }

        private void AddVariableMenuItems()
        {
            int newVarPostion = 6;

            while (contextMenuStripAdd.Items.Count > newVarPostion + 3)
            {
                contextMenuStripAdd.Items.RemoveAt(newVarPostion + 1);
            }

            if (AddtVariables != null)
            {
                foreach (string addtVar in AddtVariables)
                {
                    ToolStripMenuItem newVarMenu = new ToolStripMenuItem(addtVar);
                    newVarMenu.Click += new EventHandler(CustomVar_Click);
                    contextMenuStripAdd.Items.Insert(newVarPostion, newVarMenu);
                }
            }
        }

        private void InsertCurrentVariableColumns()
        {
            if (CurrentVariables == null) return;

            foreach (string currVar in CurrentVariables)
            {
                DataGridViewColumn currVarColumn = spreadSheetLog.GetColumn(currVar);
                if (currVarColumn == null)
                {
                    spreadSheetLog.InsertColumn(currVar, spreadSheetLog.ColumnCount - 1);
                }
                else
                {
                    spreadSheetLog.EnsureVisible(currVarColumn);
                }
            }
        }

        private void SaveData()
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (gridRow.IsNewRow)
                {
                    continue;
                }

                SaveIndividualRow(gridRow);
            }

            Data.Individual.AcceptChanges();

            UpdateLogRow();

            IsChanged = false;
        }

        private void UpdateLogRow()
        {
            if (Quantity > 0)
            {
                LogRow.Quantity = Quantity;
                LogLine.Cells[LogColumnQuantity.Name].Value = LogRow.Quantity;
            }
            else
            {
                LogRow.SetQuantityNull();
                LogLine.Cells[LogColumnQuantity.Name].Value = null;
            }

            if (!double.IsNaN(Mass) && Mass > 0)
            {
                LogRow.Mass = Mass;
                LogLine.Cells[LogColumnMass.Name].Value = LogRow.Mass;
            }
            else
            {
                LogRow.SetMassNull();
                LogLine.Cells[LogColumnMass.Name].Value = null;
            }

            if (textBoxComments.Text.IsAcceptable())
            {
                LogRow.Comments = textBoxComments.Text;
                LogLine.Cells[LogColumnSpecies.Name].ToolTipText = LogRow.Comments.InsertBreaks(35);
            }
            else
            {
                LogRow.SetCommentsNull();
                LogLine.Cells[LogColumnSpecies.Name].ToolTipText = string.Empty;
            }


            if (LogLine != null)
            {
                if (LogLine.DataGridView.FindForm() is Card card)
                {
                    card.Logger.UpdateStatus();
                }

                if (Updater != null)
                {
                    Updater.Invoke(LogLine, new EventArgs());
                }
            }
        }

        private bool IsAlreadySaved(DataGridViewRow gridRow)
        {
            return gridRow.Cells[ColumnID.Index].Value == null;
        }

        private Wild.Survey.IndividualRow IndividualRow(DataGridViewRow gridRow)
        {
            return IndividualRow(Data, LogRow, gridRow);
        }

        private Wild.Survey.IndividualRow IndividualRow(Wild.Survey data, Wild.Survey.LogRow logRow, DataGridViewRow gridRow)
        {
            Wild.Survey.IndividualRow individualRow;

            if (gridRow.Cells[ColumnID.Index].Value != null)
            {
                individualRow = data.Individual.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
                if (individualRow != null)
                {
                    goto Saving;
                }
            }

            individualRow = data.Individual.NewIndividualRow();
            data.Individual.AddIndividualRow(individualRow);
            individualRow.LogRow = logRow;

        Saving:

            if (gridRow.Cells[ColumnLength.Index].Value == null)
            {
                individualRow.SetLengthNull();
            }
            else
            {
                individualRow.Length = (double)gridRow.Cells[ColumnLength.Index].Value;
            }

            if (gridRow.Cells[ColumnMass.Index].Value == null)
            {
                individualRow.SetMassNull();
            }
            else
            {
                individualRow.Mass = (double)gridRow.Cells[ColumnMass.Index].Value;
            }

            if (gridRow.Cells[ColumnSex.Index].Value == null)
            {
                individualRow.SetSexNull();
            }
            else
            {
                individualRow.Sex = ((Sex)gridRow.Cells[ColumnSex.Index].Value).Value;
            }

            if (gridRow.Cells[ColumnGrade.Index].Value == null)
            {
                individualRow.SetGradeNull();
            }
            else
            {
                individualRow.Grade = ((Grade)gridRow.Cells[ColumnGrade.Index].Value).Value;
            }

            if (gridRow.Cells[ColumnInstar.Index].Value == null)
            {
                individualRow.SetInstarNull();
            }
            else
            {
                individualRow.Instar = (int)gridRow.Cells[ColumnInstar.Index].Value;
            }

            if (gridRow.Cells[ColumnFrequency.Index].Value == null)
            {
                individualRow.SetFrequencyNull();
            }
            else
            {
                int Frequency = (int)gridRow.Cells[ColumnFrequency.Index].Value;
                if (Frequency > 1) individualRow.Frequency = Frequency;
            }

            if (gridRow.Cells[ColumnComments.Index].Value == null)
            {
                individualRow.SetCommentsNull();
            }
            else
            {
                individualRow.Comments = (string)gridRow.Cells[ColumnComments.Index].Value;
            }

            SaveAddtValues(data, individualRow, gridRow);

            return individualRow;
        }

        private Wild.Survey.IndividualRow SaveIndividualRow(DataGridViewRow gridRow)
        {
            return SaveIndividualRow(Data, LogRow, gridRow);
        }

        private Wild.Survey.IndividualRow SaveIndividualRow(Wild.Survey data, Wild.Survey.LogRow logRow, DataGridViewRow gridRow)
        {
            Wild.Survey.IndividualRow individualRow = IndividualRow(data, logRow, gridRow);

            //if (data.Individual.Rows.IndexOf(individualRow) == -1)
            //{
            //    data.Individual.AddIndividualRow(individualRow);
            //}

            if (data == Data)
            {
                gridRow.Cells[ColumnID.Index].Value = individualRow.ID;
                gridRow.HeaderCell.Value = Math.Abs(individualRow.ID);
            }

            return individualRow;
        }

        private void SaveAddtValues(Wild.Survey data, Wild.Survey.IndividualRow individualRow, DataGridViewRow gridRow)
        {
            foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
            {
                if (gridRow.Cells[gridColumn.Index].Value == null)
                {
                    Wild.Survey.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                    if (variableRow == null) continue;

                    Wild.Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null) continue;

                    valueRow.Delete();
                }
                else
                {
                    Wild.Survey.VariableRow variableRow = data.Variable.FindByVarName(gridColumn.HeaderText);

                    if (variableRow == null)
                    {
                        variableRow = data.Variable.AddVariableRow(gridColumn.HeaderText);
                    }

                    Wild.Survey.ValueRow valueRow = data.Value.FindByIndIDVarID(individualRow.ID, variableRow.ID);

                    if (valueRow == null)
                    {
                        data.Value.AddValueRow(individualRow, variableRow, (double)gridRow.Cells[gridColumn.Index].Value);
                    }
                    else
                    {
                        valueRow.Value = (double)gridRow.Cells[gridColumn.Index].Value;
                    }
                }
            }
        }

        private DataGridViewRow InsertIndividualRow(Wild.Survey.IndividualRow individualRow)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLog);
            gridRow.HeaderCell.Value = Math.Abs(individualRow.ID);

            gridRow.Cells[ColumnID.Index].Value = individualRow.ID;

            if (individualRow.IsLengthNull()) { }
            else
            {
                gridRow.Cells[ColumnLength.Index].Value = individualRow.Length;
            }

            if (individualRow.IsMassNull()) { }
            else
            {
                gridRow.Cells[ColumnMass.Index].Value = individualRow.Mass;
            }

            if (individualRow.IsSexNull()) { }
            else
            {
                gridRow.Cells[ColumnSex.Index].Value = new Sex(individualRow.Sex);
            }

            if (individualRow.IsGradeNull()) { }
            else
            {
                gridRow.Cells[ColumnGrade.Index].Value = new Grade(individualRow.Grade);
            }

            if (individualRow.IsInstarNull()) { }
            else
            {
                gridRow.Cells[ColumnInstar.Index].Value = individualRow.Instar;
            }

            if (individualRow.IsFrequencyNull()) { }
            else
            {
                gridRow.Cells[ColumnFrequency.Index].Value = individualRow.Frequency;
            }

            if (individualRow.IsCommentsNull()) { }
            else
            {
                gridRow.Cells[ColumnComments.Index].Value = individualRow.Comments;
            }

            foreach (Wild.Survey.ValueRow valueRow in individualRow.GetValueRows())
            {
                if (valueRow.IsValueNull()) { }
                else
                {
                    gridRow.Cells[spreadSheetLog.GetColumn(valueRow.VariableRow.Variable).Index].Value = valueRow.Value;
                }
            }

            spreadSheetLog.Rows.Add(gridRow);

            return gridRow;
        }

        #endregion



        #region Interface logics

        #region Value logics

        private void Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void contextMenuStripValue_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItemPasteValue.Enabled = Clipboard.ContainsText();
        }

        private void ToolStripMenuItemClearValue_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell gridCell in spreadSheetLog.SelectedCells)
            {
                gridCell.Value = null;
            }
        }

        private void ToolStripMenuItemCopyValue_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(spreadSheetLog.GetClipboardContent());
        }

        private void ToolStripMenuItemPasteValue_Click(object sender, EventArgs e)
        {
            spreadSheetLog.PasteToGrid();
        }

        private void ToolStripMenuItemCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
        }

        #endregion

        #region Add menu

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            contextMenuStripAdd.Show(buttonAdd, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void ToolStripMenuItemNewVar_Click(object sender, EventArgs e)
        {
            spreadSheetLog.InsertColumn(spreadSheetLog.ColumnCount - 1);
        }

        private void CustomVar_Click(object sender, EventArgs e)
        {
            string variable = ((ToolStripMenuItem)sender).Text;
            spreadSheetLog.InsertColumn(variable, spreadSheetLog.ColumnCount - 1);
            spreadSheetLog.CurrentCell = spreadSheetLog[variable, spreadSheetLog.CurrentRow.Index];
            spreadSheetLog.Focus();
        }

        private void ToolStripMenuItemSex_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnSex);
        }

        private void ToolStripMenuItemGrade_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnGrade);
        }

        private void ToolStripMenuItemInstar_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnInstar);
        }

        private void menuItemAddQ_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnFrequency);
        }

        private void ToolStripMenuItemLength_Click(object sender, EventArgs e)
        {
            spreadSheetLog.EnsureVisible(ColumnLength);
        }

        #endregion

        #region Totals logics

        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            pictureBoxWarningQuantity.Visible = (DetailedQuantity > Quantity);
            IsChanged = true;
        }

        private void numericUpDownMass_ValueChanged(object sender, EventArgs e)
        {
            pictureBoxWarningMass.Visible = (DetailedMass > Mass);
            IsChanged = true;
        }

        private void pictureBoxWarningQuantity_DoubleClick(object sender, EventArgs e)
        {
            Quantity = DetailedQuantity;
            IsChanged = true;
        }

        private void pictureBoxWarningMass_DoubleClick(object sender, EventArgs e)
        {
            Mass = DetailedMass;
            IsChanged = true;
        }

        private void pictureBoxWarningM_MouseHover(object sender, EventArgs e)
        {
            toolTipAttention.ToolTipTitle = Wild.Resources.Interface.Messages.MassInequal;
            toolTipAttention.Show(string.Format(Resources.Interface.Messages.MassSetEqual,
                DetailedMass, Mass), numericUpDownMass, 0, numericUpDownMass.Height);
        }

        private void pictureBoxWarningM_MouseLeave(object sender, EventArgs e)
        {
            toolTipAttention.Hide(numericUpDownMass);
        }

        private void pictureBoxWarningQ_MouseHover(object sender, EventArgs e)
        {
            if (DetailedQuantity > Quantity)
            {
                toolTipAttention.ToolTipTitle = Wild.Resources.Interface.Messages.QuantityInequal;
                toolTipAttention.Show(string.Format(Wild.Resources.Interface.Messages.QuantitySetEqual,
                    DetailedQuantity, Quantity), numericUpDownQuantity, 0, numericUpDownQuantity.Height);
            }
            else
            {
                toolTipAttention.ToolTipTitle = Wild.Resources.Interface.Messages.QuantityEqual;
                toolTipAttention.Show(string.Format(Wild.Resources.Interface.Messages.QuantityEqualDetails,
                    Quantity), numericUpDownQuantity, 0, numericUpDownQuantity.Height);
            }
        }

        private void pictureBoxWarningQ_MouseLeave(object sender, EventArgs e)
        {
            toolTipAttention.Hide(numericUpDownQuantity);
        }

        #endregion

        #region Grid Log logics

        private void spreadSheetLog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 10 & ModifierKeys.HasFlag(Keys.Control))
            {
                buttonOK.PerformClick();
            }
        }

        private void spreadSheetLog_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnGrade.Index || e.ColumnIndex == ColumnSex.Index)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(CultureInfo.InvariantCulture);
            }
            else
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }

        private void spreadSheetLog_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == -1) return;

            if (e.ColumnIndex == ColumnFrequency.Index)
            {
                Wild.Service.HandleFrequency(spreadSheetLog, e);
            }

            if (e.ColumnIndex == ColumnInstar.Index || e.ColumnIndex == ColumnGrade.Index)
            {
                Service.HandleInstarInput(e, ColumnGrade, ColumnInstar);
            }

            UpdateTotals();
        }

        private void spreadSheetLog_CurrentCellChanged(object sender, EventArgs e)
        {
        }

        private void spreadSheetLog_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Clear(e.Row);
            IsChanged = true;
        }

        private void spreadSheetLog_RowRemoving(object sender, DataGridViewRowEventArgs e)
        {
            Clear(e.Row);
            spreadSheetLog.Rows.Remove(e.Row);
            IsChanged = true;
        }

        private void spreadSheetLog_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int columnIndex = spreadSheetLog.HitTest(e.X, e.Y).ColumnIndex;

                if (columnIndex == -1)
                {
                    SelectedColumn = null;
                }
                else
                {
                    SelectedColumn = spreadSheetLog.Columns[columnIndex];
                }
            }
        }

        private void spreadSheetLog_ColumnRenamed(object sender, GridColumnRenameEventArgs e)
        {
            Wild.Survey.VariableRow variableRow = ((Wild.Survey)LogRow.Table.DataSet).Variable.FindByVarName(e.PreviousCaption);

            if (variableRow != null)
            {
                variableRow.Variable = e.Column.HeaderText;
            }

            IsChanged = true;
        }

        private void spreadSheetLog_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            Width -= e.Column.Width;

            Wild.Survey.VariableRow variableRow = Data.Variable.FindByVarName(e.Column.HeaderText);

            if (variableRow != null)
            {
                for (int i = 0; i < Data.Value.Count; i++)
                {
                    Wild.Survey.ValueRow valueRow = Data.Value[i];
                    if (valueRow.VariableRow == variableRow &&
                        valueRow.IndividualRow.LogRow == LogRow)
                    {
                        valueRow.Delete();
                        i--;
                    }
                }
            }

            IsChanged = true;
        }

        #endregion

        #region Individual menu

        private void contextMenuStripInd_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItemPaste.Enabled = Clipboard.ContainsText() &&
                Wild.Survey.ContainsIndividuals(Clipboard.GetText());
        }

        private void ToolStripMenuItemInd_Click(object sender, EventArgs e)
        { }

        private void ToolStripMenuItemDuplicate_Click(object sender, EventArgs e)
        {
            if (inputDialogDupl.ShowDialog(this) == DialogResult.OK)
            {
                int copies = int.Parse(inputDialogDupl.Input);

                foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
                {
                    if (gridRow.IsNewRow)
                    {
                        continue;
                    }

                    for (int i = 0; i < copies; i++)
                    {
                        int newRowIndex = gridRow.Index + 1;
                        spreadSheetLog.Rows.Insert(newRowIndex, 1);

                        foreach (DataGridViewColumn gridColumn in new DataGridViewColumn[] {
                            ColumnLength, ColumnMass, ColumnSex, ColumnGrade, ColumnInstar })
                        {
                            if (gridColumn.Visible)
                            {
                                spreadSheetLog[gridColumn.Index, newRowIndex].Value = gridRow.Cells[gridColumn.Index].Value;
                            }
                        }

                        spreadSheetLog[ColumnComments.Index, newRowIndex].Value = gridRow.Cells[ColumnComments.Index].Value;

                        foreach (DataGridViewColumn gridColumn in spreadSheetLog.GetInsertedColumns())
                        {
                            spreadSheetLog[gridColumn.Index, newRowIndex].Value = gridRow.Cells[gridColumn.Index].Value;
                        }
                    }

                    spreadSheetLog.ClearEmptyRows();
                }
            }
        }

        private void ToolStripMenuItemCut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemCopy_Click(sender, e);
            ToolStripMenuItemDelete_Click(sender, e);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            Wild.Survey clipData = new Wild.Survey();

            Wild.Survey.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            Wild.Survey.DefinitionRow clipSpeciesRow = clipData.Definition.NewDefinitionRow();
            clipData.Definition.AddDefinitionRow(clipSpeciesRow);

            Wild.Survey.LogRow clipLogRow = clipData.Log.NewLogRow();
            clipLogRow.CardRow = clipCardRow;
            clipLogRow.DefinitionRow = clipSpeciesRow;
            clipData.Log.AddLogRow(clipLogRow);

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.IsNewRow) continue;
                SaveIndividualRow(clipData, clipLogRow, gridRow);
            }

            Clipboard.SetText(clipData.GetXml());
        }

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            Wild.Survey clipData = Wild.Survey.FromClipboard();

            foreach (Wild.Survey.VariableRow clipVariableRow in clipData.Variable.Rows)
            {
                spreadSheetLog.InsertColumn(clipVariableRow.Variable);
            }

            foreach (Wild.Survey.IndividualRow clipIndividualRow in clipData.Individual)
            {
                InsertIndividualRow(clipIndividualRow);
            }

            IsChanged = true;
            UpdateTotals();
            Clipboard.Clear();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            int rowsToDelete = spreadSheetLog.SelectedRows.Count;
            while (rowsToDelete > 0)
            {
                Clear(spreadSheetLog.SelectedRows[0]);
                spreadSheetLog.Rows.Remove(spreadSheetLog.SelectedRows[0]);
                rowsToDelete--;
            }

            IsChanged = true;
            UpdateTotals();
        }

        #endregion

        #region Stratified logics

        private void Range_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotals();
            IsChanged = true;
        }

        #endregion

        #region Column menu

        private void contextMenuStripVar_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItemRemove.Enabled = spreadSheetLog.GetInsertedColumns().Contains(SelectedColumn);
        }

        private void ToolStripMenuItemRemove_Click(object sender, EventArgs e)
        {
            Width -= SelectedColumn.Width;
            spreadSheetLog.Columns.Remove(SelectedColumn);

            Wild.Survey.VariableRow variableRow = Data.Variable.FindByVarName(SelectedColumn.HeaderText);
            if (variableRow != null)
            {
                for (int i = 0; i < Data.Value.Count; i++)
                {
                    Wild.Survey.ValueRow valueRow = Data.Value[i];
                    if (valueRow.VariableRow == variableRow &&
                        valueRow.IndividualRow.LogRow == LogRow)
                    {
                        valueRow.Delete();
                        i--;
                    }
                }
            }

            IsChanged = true;
        }

        private void ToolStripMenuItemClearColumn_Click(object sender, EventArgs e)
        {
            Clear(SelectedColumn);
            IsChanged = true;
        }

        private void ToolStripMenuItemSaveVars_Click(object sender, EventArgs e)
        {
            AddtVariables = AdditionalVariables;
            SettingsReader.CurrentVariables = CurrentVariables;
            AddVariableMenuItems();
        }

        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemSaveVars_Click(sender, e);

            ChangesWereMade |= IsChanged;
            SaveData();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxComments_TextChanged(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        private void Individuals_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK &&
                (IsThereUnsavedIndividuals || IsChanged))
            {
                TaskDialogButton b = taskDialogSave.ShowDialog(this);

                if (b == tdbSaveAllIndividuals)
                {
                    SaveData();
                    DialogResult = DialogResult.OK;
                    ChangesWereMade = true;
                }
                else if (b == tdbDiscard)
                {

                }
                else if (b == tdbCancelClose)
                {
                    e.Cancel = true;
                }
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            SaveData();
            LogRow.GetReport().Run();
        }

        private void buttonBlank_Click(object sender, EventArgs e)
        {
            BenthosReport.BlankIndividualsLog.Run();
        }

        #endregion
    }
}
