using Mayfly.Wild;
using Mayfly.Geographics;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Software;
using Mayfly.Waters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Controls;
using static Mayfly.Wild.SettingsReader;
using static Mayfly.Fish.UserSettings;

namespace Mayfly.Fish.Explorer
{
    public partial class Catch : Form
    {
        public Survey Data { get; set; }

        public bool IsChanged { get; set; }

        public int SpeciesCount {
            get {
                return SpeciesList.Length;
            }
        }

        public string[] SpeciesList {
            get {
                List<string> result = new List<string>();
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows) {
                    if (gridRow.Cells[ColumnSpecies.Name].Value != null) {
                        string speciesName = gridRow.Cells[ColumnSpecies.Name].Value.ToString();
                        if (!result.Contains(speciesName)) {
                            result.Add(speciesName);
                        }
                    }
                }
                return result.ToArray();
            }
        }

        private double Mass {
            get {
                double result = 0;
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows) {
                    if (gridRow.Cells[ColumnMass.Name].Value is double @double) {
                        result += @double;
                    }
                }
                return result;
            }
        }

        private double Quantity {
            get {
                double result = 0;
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows) {
                    if (gridRow.Cells[ColumnQuantity.Name].Value is int @int) {
                        result += @int;
                    }
                }
                return result;
            }
        }

        public Surveys.ActionRow ActionRow { get; set; }

        public TextAndImageCell GridCell { get; set; }

        public event EventHandler TotalChanged;



        public Catch(Surveys.ActionRow actionRow) {
            InitializeComponent();

            // Show in caption Gear and time

            ActionRow = actionRow;
            Data = new Survey();

            if (!ActionRow.IsCatchXMLNull()) {
                Data.ReadXml(new StringReader(ActionRow.CatchXML));
                LoadData();
            }

            Text = string.Format("{0} - {1}", ActionRow.GetShortDescription(), Text);

            ColumnSpecies.ValueType = typeof(string);
            ColumnQuantity.ValueType = typeof(int);
            ColumnMass.ValueType = typeof(double);

            speciesLogger.RecentListCount = RecentSpeciesCount;
            speciesLogger.IndexPath = TaxonomicIndexPath;

            ColumnQuantity.ReadOnly = FixTotals;
            ColumnMass.ReadOnly = FixTotals;

            UpdateStatus();
            IsChanged = false;
        }

        public Catch(TextAndImageCell gridCell)
            : this(gridCell.Tag as Surveys.ActionRow) {
            this.GridCell = gridCell;
            this.SetFriendlyDesktopLocation(gridCell);
        }



        private void SaveData() {
            SaveLog();
            Data.ClearUseless();
        }

        private void SaveLog() {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows) {
                if (gridRow.IsNewRow) continue;

                SaveLogRow(gridRow);
            }
        }

        private void Clear(DataGridViewRow gridRow) {
            if (gridRow.Cells[ColumnID.Index].Value != null) {
                Survey.LogRow logRow = Data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
                Survey.DefinitionRow spcRow = logRow.DefinitionRow;
                logRow.Delete();
                if (spcRow != null) spcRow.Delete();
            }
        }

        private Survey.LogRow LogRow(DataGridViewRow gridRow) {
            return LogRow(Data, gridRow);
        }

        private Survey.LogRow LogRow(Survey data, DataGridViewRow gridRow) {
            Survey.LogRow result = GetLogRow(data, gridRow);

            if (gridRow.Cells[ColumnSpecies.Index].Value is TaxonomicIndex.TaxonRow tr) {
                Survey.DefinitionRow existingSpeciesRow = data.Definition.FindByName(tr.Name);
                if (existingSpeciesRow == null) {
                    existingSpeciesRow = data.Definition.AddDefinitionRow(tr.Rank, tr.Name);
                }
                result.DefID = existingSpeciesRow.ID;
            } else if ((string)gridRow.Cells[ColumnSpecies.Index].Value is string s) {
                if (s == Species.Resources.Interface.UnidentifiedTitle) {
                    result.SetDefIDNull();
                } else {
                    Survey.DefinitionRow existingSpeciesRow = data.Definition.FindByName(s);
                    if (existingSpeciesRow == null) {
                        existingSpeciesRow = data.Definition.AddDefinitionRow(s);
                    }
                    result.DefID = existingSpeciesRow.ID;
                }
            }

            if (gridRow.Cells[ColumnQuantity.Index].Value == null) {
                result.SetQuantityNull();
            } else {
                result.Quantity = (int)gridRow.Cells[ColumnQuantity.Index].Value;
            }

            if (gridRow.Cells[ColumnMass.Index].Value == null) {
                result.SetMassNull();
            } else {
                result.Mass = (double)gridRow.Cells[ColumnMass.Index].Value;
            }

            return result;
        }

        private Survey.LogRow SaveLogRow(DataGridViewRow gridRow) {
            return SaveLogRow(Data, gridRow);
        }

        private Survey.LogRow SaveLogRow(Survey data, DataGridViewRow gridRow) {
            Survey.LogRow result = LogRow(data, gridRow);
            if (data.Log.Rows.IndexOf(result) == -1) data.Log.AddLogRow(result);
            if (data == Data) gridRow.Cells[ColumnID.Index].Value = result.ID;
            return result;
        }

        private Survey.LogRow GetLogRow(DataGridViewRow gridRow) {
            return GetLogRow(Data, gridRow);
        }

        private Survey.LogRow GetLogRow(Survey data, DataGridViewRow gridRow) {
            Survey.LogRow result;

            if (gridRow.Cells[ColumnID.Index].Value == null ||
                data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value) == null) {
                result = data.Log.NewLogRow();
                result.CardRow = data.Solitary;
                data.Log.AddLogRow(result);
            } else {
                result = data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
            }

            return result;
        }

        private void LoadData() {
            spreadSheetLog.Rows.Clear();
            InsertLogRows(Data, 0);
            IsChanged = false;
        }

        private void InsertLogRows(Survey data, int rowIndex) {
            if (rowIndex == -1) {
                rowIndex = spreadSheetLog.RowCount - 1;
            }

            foreach (Survey.LogRow logRow in data.Log.Rows) {
                InsertLogRow(logRow, rowIndex);

                if (rowIndex < spreadSheetLog.RowCount - 1) {
                    rowIndex++;
                }
            }
        }

        private void InsertLogRow(Survey.LogRow logRow, int rowIndex) {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLog);
            gridRow.Cells[ColumnID.Index].Value = logRow.ID;
            gridRow.Cells[ColumnSpecies.Index].Value = logRow.DefinitionRow.Taxon;

            if (!logRow.IsQuantityNull())
                gridRow.Cells[ColumnQuantity.Index].Value = logRow.Quantity;

            if (!logRow.IsMassNull())
                gridRow.Cells[ColumnMass.Index].Value = logRow.Mass;

            if (!logRow.IsCommentsNull())
                gridRow.Cells[ColumnMass.Index].ToolTipText = logRow.Comments;

            spreadSheetLog.Rows.Insert(rowIndex, gridRow);

            for (int i = 0; i < spreadSheetLog.RowCount; i++) {
                DataGridViewRow currentGridRow = spreadSheetLog.Rows[i];

                if (currentGridRow.IsNewRow) {
                    continue;
                }

                if (currentGridRow == gridRow) {
                    continue;
                }

                if (object.Equals(gridRow.Cells[ColumnSpecies.Index].Value,
                    currentGridRow.Cells[ColumnSpecies.Index].Value)) {
                    speciesLogger_DuplicateDetected(spreadSheetLog,
                        new DuplicateFoundEventArgs(gridRow, currentGridRow));
                    i--;
                }
            }
        }

        public static bool IsDataPresented(string text) {
            try {
                Survey data = new Survey();
                data.ReadXml(new StringReader(text));
                return data.Log.Count > 0;
            } catch { return false; }
        }

        private DialogResult CheckAndSave() {
            DialogResult result = DialogResult.None;

            if (IsChanged) {
                TaskDialogButton tdbPressed = taskDialogSaveChanges.ShowDialog(this);

                if (tdbPressed == tdbSave) {
                    buttonSave_Click(buttonSave, new EventArgs());
                    result = DialogResult.OK;
                } else if (tdbPressed == tdbDiscard) {
                    IsChanged = false;
                    result = DialogResult.No;
                } else if (tdbPressed == tdbCancelClose) {
                    result = DialogResult.Cancel;
                }
            }

            return result;
        }

        private void OpenIndividuals(DataGridViewRow gridRow) {
            if (gridRow.Cells[ColumnSpecies.Name].Value == null) return;
            Individuals individuals = OpenIndividuals(SaveLogRow(gridRow));
            individuals.LogLine = gridRow;
            individuals.SetFriendlyDesktopLocation(gridRow);
        }

        private Individuals OpenIndividuals(Survey.LogRow logRow) {
            Individuals individuals = new Individuals(logRow);
            individuals.Observations = (Surveys)ActionRow.Table.DataSet;
            individuals.ActionRow = this.ActionRow;
            individuals.SetFriendlyDesktopLocation(spreadSheetLog);
            individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
            individuals.Show(this);
            return individuals;
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e) {
            Individuals individuals = sender as Individuals;
            if (individuals.DialogResult == DialogResult.OK) {
                IsChanged |= individuals.ChangesWereMade;
            }
        }

        private void UpdateStatus() {
            if (TotalChanged != null) {
                TotalChanged.Invoke(this, new EventArgs());
            }
        }



        private void Card_Load(object sender, EventArgs e) {
            IsChanged = false;

        }

        private void Card_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = (CheckAndSave() == DialogResult.Cancel);
        }

        private void ValueChanged(object sender, EventArgs e) {
            IsChanged = true;
        }


        private void speciesLogger_SpeciesSelected(object sender, SpeciesSelectEventArgs e) {
            ValueChanged(sender, e);

            if (AutoLogOpen) {
                spreadSheetLog.ClearSelection();
                e.Row.Selected = true;
                OpenIndividuals(e.Row);
                //ToolStripMenuItemIndividuals_Click(ToolStripMenuItemIndividuals, new EventArgs());
            }
        }

        private void speciesLogger_DuplicateDetected(object sender, DuplicateFoundEventArgs e) {
            int q = 0;
            double w = 0;

            if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null) {
                q += (int)e.EditedRow.Cells[ColumnQuantity.Name].Value;
            }

            if (e.EditedRow.Cells[ColumnMass.Index].Value != null) {
                if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null &&
                    (int)e.EditedRow.Cells[ColumnQuantity.Index].Value == 1 &&
                    LogRow(e.EditedRow).GetIndividualRows().Length == 0) {
                    Survey.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = LogRow(e.EditedRow);
                    newIndividualRow.Mass = (double)e.EditedRow.Cells[ColumnMass.Index].Value;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }

                w += (double)e.EditedRow.Cells[ColumnMass.Name].Value;
            }

            if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null) {
                q += (int)e.DuplicateRow.Cells[ColumnQuantity.Name].Value;
            }

            if (e.DuplicateRow.Cells[ColumnMass.Index].Value != null) {
                if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null &&
                    (int)e.DuplicateRow.Cells[ColumnQuantity.Index].Value == 1 &&
                    LogRow(e.DuplicateRow).GetIndividualRows().Length == 0) {
                    Survey.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = LogRow(e.EditedRow);
                    if (e.EditedRow.Cells[ColumnMass.Index].Value != null) {
                        newIndividualRow.Mass = (double)e.EditedRow.Cells[ColumnMass.Index].Value;
                    }
                    newIndividualRow.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }

                w += (double)e.DuplicateRow.Cells[ColumnMass.Name].Value;
            }

            if (q > 0) {
                e.EditedRow.Cells[ColumnQuantity.Index].Value = q;
            }

            if (w > 0) {
                e.EditedRow.Cells[ColumnMass.Index].Value = w;
            }

            foreach (Survey.IndividualRow individualRow in LogRow(e.DuplicateRow).GetIndividualRows()) {
                individualRow.LogRow = LogRow(e.EditedRow);
            }

            spreadSheetLog.Rows.Remove(e.DuplicateRow);
            Clear(e.DuplicateRow);
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            SaveData();
            ActionRow.CatchXML = Data.GetXml();
            IsChanged = false;

            Close();
        }

        #region Log grid logics

        private void spreadSheetLog_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            IsChanged = true;
        }

        private void spreadSheetLog_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            UpdateStatus();
        }

        private void spreadSheetLog_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
            Clear(e.Row);
            IsChanged = true;
            UpdateStatus();
        }

        private void spreadSheetLog_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            UpdateStatus();
            IsChanged = true;
        }

        private void spreadSheetLog_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            UpdateStatus();
            IsChanged = true;
        }

        private void spreadSheetLog_InputFailed(object sender, DataGridViewCellEventArgs e) {
            toolTipAttention.ToolTipTitle = Wild.Resources.Interface.Messages.LogBlocked;
            Rectangle rect = spreadSheetLog.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            toolTipAttention.Show(Wild.Resources.Interface.Messages.LogInstruction, this,
                rect.Left + spreadSheetLog.Left, rect.Bottom + spreadSheetLog.Top, 5000);
        }

        #endregion

        #region Log menu logics

        private void contextMenuStripLog_Opening(object sender, CancelEventArgs e) {
            ToolStripMenuItemPaste.Enabled = Clipboard.ContainsText() &&
                Survey.ContainsLog(Clipboard.GetText());
        }

        private void ToolStripMenuItemIndividuals_Click(object sender, EventArgs e) {
            spreadSheetLog.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                OpenIndividuals(gridRow);
            }
        }

        private void ToolStripMenuItemCut_Click(object sender, EventArgs e) {
            ToolStripMenuItemCopy_Click(sender, e);
            ToolStripMenuItemDelete_Click(sender, e);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e) {
            Survey clipData = new Survey();
            Survey.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            foreach (DataGridViewRow selectedRow in spreadSheetLog.SelectedRows) {
                if (selectedRow.IsNewRow) {
                    continue;
                }

                Survey.LogRow clipLogRow = LogRow(clipData, selectedRow);

                if (selectedRow.Cells[ColumnID.Index].Value != null) {
                    Survey.LogRow logRow = Data.Log.FindByID((int)selectedRow.Cells[ColumnID.Index].Value);

                    if (logRow != null) {
                        foreach (Survey.IndividualRow individualRow in logRow.GetIndividualRows()) {
                            individualRow.CopyTo(clipLogRow);
                        }

                        foreach (Survey.StratifiedRow stratifiedRow in logRow.GetStratifiedRows()) {
                            Survey.StratifiedRow clipStratifiedRow = clipData.Stratified.NewStratifiedRow();

                            clipStratifiedRow.Class = stratifiedRow.Class;
                            if (!stratifiedRow.IsCountNull()) clipStratifiedRow.Count = stratifiedRow.Count;
                            clipStratifiedRow.LogRow = clipLogRow;
                            clipData.Stratified.AddStratifiedRow(clipStratifiedRow);
                        }
                    }
                }
            }

            Clipboard.SetText(clipData.GetXml());
        }

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e) {
            Survey clipData = new Survey();
            clipData.ReadXml(new StringReader(Clipboard.GetText()));

            int rowIndex = spreadSheetLog.SelectedRows[0].Index;

            foreach (Survey.LogRow clipLogRow in clipData.Log) {
                // Copy from Clipboard Data to local Data
                Survey.LogRow logRow = Data.Log.NewLogRow();
                if (!clipLogRow.IsQuantityNull()) logRow.Quantity = clipLogRow.Quantity;
                if (!clipLogRow.IsMassNull()) logRow.Mass = clipLogRow.Mass;
                logRow.CardRow = Data.Solitary;

                TaxonomicIndex.TaxonRow clipSpeciesRow = SettingsReader.TaxonomicIndex.FindByName(clipLogRow.DefinitionRow.Taxon);

                if (clipSpeciesRow == null) {
                    Survey.DefinitionRow newSpeciesRow = Data.Definition.AddDefinitionRow(
                        clipLogRow.DefinitionRow.Rank, clipLogRow.DefinitionRow.Taxon);

                    logRow.DefID = newSpeciesRow.ID;
                } else {
                    if (Data.Definition.FindByName(clipSpeciesRow.Name) == null) {
                        Data.Definition.AddDefinitionRow(clipSpeciesRow.Rank, clipSpeciesRow.Name);
                    }
                    logRow.DefID = clipSpeciesRow.ID;
                }

                Data.Log.AddLogRow(logRow);

                foreach (Survey.IndividualRow clipIndividualRow in clipLogRow.GetIndividualRows()) {
                    clipIndividualRow.CopyTo(logRow);
                }

                foreach (Survey.StratifiedRow clipStratifiedRow in clipData.Stratified) {
                    Survey.StratifiedRow stratifiedRow = Data.Stratified.NewStratifiedRow();
                    stratifiedRow.Class = clipStratifiedRow.Class;
                    if (!clipStratifiedRow.IsCountNull()) stratifiedRow.Count = clipStratifiedRow.Count;
                    stratifiedRow.LogRow = logRow;
                    Data.Stratified.AddStratifiedRow(stratifiedRow);
                }

                InsertLogRow(logRow, rowIndex);

                if (rowIndex < spreadSheetLog.RowCount - 1) {
                    rowIndex++;
                }
            }

            IsChanged = true;
            UpdateStatus();
            Clipboard.Clear();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e) {
            int rowsToDelete = spreadSheetLog.SelectedRows.Count;
            while (rowsToDelete > 0) {
                Clear(spreadSheetLog.SelectedRows[0]);
                spreadSheetLog.Rows.Remove(spreadSheetLog.SelectedRows[0]);
                rowsToDelete--;
            }

            IsChanged = true;
            UpdateStatus();
        }

        private void ToolStripMenuItemSpeciesKey_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow selectedRow in spreadSheetLog.SelectedRows) {
                speciesLogger.FindInKey(selectedRow);
            }
        }

        #endregion
    }
}