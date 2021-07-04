using Mayfly.Extensions;
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
using Mayfly.Waters.Controls;

namespace Mayfly.Reader
{
    public partial class Card : Form
    {
        #region Properties

        protected bool AutoLogOpen
        {
            get;
            set;
        }

        private string speciesIndexPath;

        protected string SpeciesIndexPath
        {
            get { return speciesIndexPath; }

            set
            {
                speciesIndexPath = value;
                speciesLogger.IndexPath = value;

                if (value == null) return;

                SpeciesIndex = new SpeciesKey();
                SpeciesIndex.ReadXml(value);
            }
        }

        protected OpenFileDialog OpenDialog
        {
            get;
            set;
        }

        protected SaveFileDialog SaveDialog
        {
            get;
            set;
        }

        private string fileName;

        public string FileName
        {
            set
            {
                this.ResetText(value == null ? FileSystem.GetNewFileCaption(UserSettings.Interface.Extension) : value, EntryAssemblyInfo.Title);
                fileName = value;
            }

            get
            {
                return fileName;
            }
        }

        private string SpeciesToOpen;

        public Data Data { get; set; }

        public bool IsChanged { get; set; }

        public int SpeciesCount
        {
            get
            {
                return SpeciesList.Length;
            }
        }

        public string[] SpeciesList
        {
            get
            {
                List<string> result = new List<string>();
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.Cells[ColumnSpecies.Name].Value != null)
                    {
                        string speciesName = gridRow.Cells[ColumnSpecies.Name].Value.ToString();
                        if (!result.Contains(speciesName))
                        {
                            result.Add(speciesName);
                        }
                    }
                }
                return result.ToArray();
            }
        }

        private double Mass
        {
            get
            {
                double result = 0;
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.Cells[ColumnMass.Name].Value is double)
                    {
                        result += (double)gridRow.Cells[ColumnMass.Name].Value;
                    }
                }
                return result;
            }
        }

        private double Quantity
        {
            get
            {
                double result = 0;
                foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
                {
                    if (gridRow.Cells[ColumnQuantity.Name].Value is int)
                    {
                        result += (int)gridRow.Cells[ColumnQuantity.Name].Value;
                    }
                }
                return result;
            }
        }

        private Species.SpeciesKey speciesIndex;

        protected Species.SpeciesKey SpeciesIndex
        {
            get
            {
                return speciesIndex;
            }

            set
            {
                speciesIndex = value;
            }
        }

        protected delegate void LogRowHandler(DataGridViewRow gridRow, 
            Func<DataGridViewRow, Data.LogRow> logRowGetter);

        protected LogRowHandler logRowHandler;

        #endregion



        public Card()
        {
            InitializeComponent();

            Data = new Data();
            FileName = null;

            ColumnSpecies.ValueType = typeof(string);
            ColumnQuantity.ValueType = typeof(int);
            ColumnMass.ValueType = typeof(double);

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);

            IsChanged = false;
        }



        #region Methods

        private void Clear()
        {
            FileName = null;

            textBoxLabel.Text = string.Empty;

            waypointControl1.Clear();

            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();

            Data = new Data();
        }

        private void Clear(DataGridViewRow LogGridRow)
        {
            if (LogGridRow.Cells[ColumnID.Index].Value != null)
            {
                Data.LogRow logRow = Data.Log.FindByID((int)LogGridRow.Cells[ColumnID.Index].Value);
                Data.SpeciesRow spcRow = logRow.SpeciesRow;
                logRow.Delete();
                spcRow.Delete();
            }
        }

        public void UpdateStatus()
        {
            statusCard.Default = StatusLog.Text =
                SpeciesCount.ToString(Wild.Resources.Interface.SpeciesCount);
            StatusMass.ResetFormatted(Mass);
            StatusCount.ResetFormatted(Quantity);
        }

        private void Save(string fileName)
        {
            SaveData();
            Data.SaveToFile(fileName);
            statusCard.Message(Wild.Resources.Messages.Saved);
            FileName = fileName;
            IsChanged = false;
        }

        private void SaveData()
        {
            Data.CardRow cardRow = SaveCardRow();

            #region Save factors values

            Data.FactorValue.Clear();
            Data.Factor.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetAddt.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnAddtFactor.Index].Value == null) continue;

                if (gridRow.Cells[ColumnAddtValue.Index].Value == null) continue;

                string factorName = (string)gridRow.Cells[ColumnAddtFactor.Index].Value;

                double factorValue = (double)gridRow.Cells[ColumnAddtValue.Index].Value;

                Data.FactorValue.AddFactorValueRow(Data.Solitary, Data.Factor.AddFactorRow(factorName), factorValue);

                if (!Wild.UserSettings.AddtFactors.Contains(factorName))
                {
                    List<string> factors = new List<string>();
                    factors.AddRange(Wild.UserSettings.AddtFactors);
                    factors.Add(factorName);
                    Wild.UserSettings.AddtFactors = factors.ToArray();
                }
            }

            #endregion

            SaveLog();

            Data.ClearUseless();
        }

        public Data.CardRow SaveCardRow()
        {
            #region Header

            if (textBoxLabel.Text.IsAcceptable())
            {
                Data.Solitary.SetLabelNull();
            }
            else
            {
                Data.Solitary.Label = textBoxLabel.Text;
            }

            waypointControl1.Save();

            if (FileName == null) // If file is saving at the fisrt time
            {
                Data.Solitary.When = waypointControl1.Waypoint.TimeMark;
                Data.Solitary.AttachSign();
            }
            else // If file is resaving
            {
                Data.Solitary.RenewSign(waypointControl1.Waypoint.TimeMark);
            }

            if (!waypointControl1.Waypoint.IsEmpty)
                Data.Solitary.Where = waypointControl1.Waypoint.Protocol;

            #endregion

            #region Comments

            textBoxComments.Text = textBoxComments.Text.Trim();

            if (textBoxComments.Text.IsAcceptable())
            {
                Data.Solitary.SetCommentsNull();
            }
            else
            {
                Data.Solitary.Comments = textBoxComments.Text;
            }

            #endregion

            return Data.Solitary;
        }

        private void SaveLog()
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SaveLogRow(gridRow);
            }
        }

        private void HandleLogRow(DataGridViewRow gridRow)
        {
            // If it is new row - end of function.
            if (gridRow.IsNewRow)
            {
                return;
            }

            // If row is empty - delete the row and end the function
            if (Wild.Service.IsRowEmpty(gridRow))
            {
                spreadSheetLog.Rows.Remove(gridRow);
                return;
            }

            // If species is not set - light 'Not identified'
            if (gridRow.Cells[ColumnSpecies.Index].Value == null)
            {
                gridRow.Cells[ColumnSpecies.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }

            // Searching for the duplicates (on 'Species' column)
            bool containsDuplicates = false;

            for (int i = 0; i < spreadSheetLog.RowCount; i++)
            {
                DataGridViewRow currentGridRow = spreadSheetLog.Rows[i];

                if (currentGridRow.IsNewRow)
                {
                    continue;
                }

                if (currentGridRow == gridRow)
                {
                    continue;
                }

                if (object.Equals(gridRow.Cells[ColumnSpecies.Index].Value, currentGridRow.Cells[ColumnSpecies.Index].Value))
                {
                    int Q = 0;
                    double W = 0;

                    if (gridRow.Cells[ColumnQuantity.Index].Value != null)
                    {
                        Q += (int)gridRow.Cells[ColumnQuantity.Name].Value;
                    }

                    if (gridRow.Cells[ColumnMass.Index].Value != null)
                    {
                        if (gridRow.Cells[ColumnQuantity.Index].Value != null &&
                            (int)gridRow.Cells[ColumnQuantity.Index].Value == 1 &&
                            LogRow(gridRow).GetIndividualRows().Length == 0)
                        {
                            Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                            newIndividualRow.LogRow = LogRow(gridRow);
                            newIndividualRow.Mass = (double)gridRow.Cells[ColumnMass.Index].Value;
                            Data.Individual.AddIndividualRow(newIndividualRow);
                        }

                        W += (double)gridRow.Cells[ColumnMass.Name].Value;
                    }

                    if (currentGridRow.Cells[ColumnQuantity.Index].Value != null)
                    {
                        Q += (int)currentGridRow.Cells[ColumnQuantity.Name].Value;
                    }

                    if (currentGridRow.Cells[ColumnMass.Index].Value != null)
                    {
                        if (currentGridRow.Cells[ColumnQuantity.Index].Value != null &&
                            (int)currentGridRow.Cells[ColumnQuantity.Index].Value == 1 &&
                            LogRow(currentGridRow).GetIndividualRows().Length == 0)
                        {
                            Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                            newIndividualRow.LogRow = LogRow(gridRow);
                            if (gridRow.Cells[ColumnMass.Index].Value != null)
                            {
                                newIndividualRow.Mass = (double)gridRow.Cells[ColumnMass.Index].Value;
                            }
                            newIndividualRow.Comments = Wild.Resources.Interface.DuplicateDefaultComment;
                            Data.Individual.AddIndividualRow(newIndividualRow);
                        }

                        W += (double)currentGridRow.Cells[ColumnMass.Name].Value;
                    }

                    if (Q > 0)
                    {
                        gridRow.Cells[ColumnQuantity.Index].Value = Q;
                    }

                    if (W > 0)
                    {
                        gridRow.Cells[ColumnMass.Index].Value = W;
                    }

                    foreach (Data.IndividualRow individualRow in LogRow(currentGridRow).GetIndividualRows())
                    {
                        individualRow.LogRow = LogRow(gridRow);
                    }

                    spreadSheetLog.Rows.Remove(currentGridRow);
                    Clear(currentGridRow);
                    i--;
                    containsDuplicates = true;
                }
            }

            if (containsDuplicates)
            {
                statusCard.Default = SpeciesCount.ToString(Mayfly.Wild.Resources.Interface.SpeciesCount);
                statusCard.Message(Wild.Resources.Messages.DuplicateSummed, gridRow.Cells[ColumnSpecies.Index].Value);
            }
        }

        private void HandleFactorRow(DataGridViewRow gridRow)
        {
            if (gridRow.IsNewRow)
            {
                return;
            }

            if (gridRow.Cells[ColumnAddtFactor.Index].Value == null ||
                !((string)gridRow.Cells[ColumnAddtFactor.Index].Value).IsAcceptable())
            {
                statusCard.Message(Wild.Resources.Messages.FactorNameRequired);
            }

            for (int i = 0; i < spreadSheetAddt.RowCount; i++)
            {
                DataGridViewRow currentGridRow = spreadSheetAddt.Rows[i];

                if (currentGridRow.IsNewRow)
                {
                    continue;
                }

                if (currentGridRow == gridRow)
                {
                    continue;
                }

                if (object.Equals(gridRow.Cells[ColumnAddtFactor.Index].Value,
                    currentGridRow.Cells[ColumnAddtFactor.Index].Value))
                {
                    if (gridRow.Cells[ColumnAddtValue.Index].Value == null)
                    {
                        gridRow.Cells[ColumnAddtValue.Index].Value =
                            currentGridRow.Cells[ColumnAddtValue.Index].Value;
                    }

                    spreadSheetAddt.Rows.Remove(currentGridRow);
                    i--;
                }
            }
        }

        private Data.LogRow LogRow(DataGridViewRow gridRow)
        {
            return LogRow(Data, gridRow);
        }

        private Data.LogRow LogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result;

            if (data == Data)
            {
                if (gridRow.Cells[ColumnID.Index].Value != null)
                {
                    result = data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
                    if (result != null)
                    {
                        goto Saving;
                    }
                }
            }

            result = data.Log.NewLogRow();
            result.CardRow = data.Solitary;

        Saving:

            // If undefined
            if ((string)gridRow.Cells[ColumnSpecies.Index].Value == Species.Resources.Interface.UnidentifiedTitle)
            {
                // Set SpcID to null
                result.SetSpcIDNull();
            }
            else // if defined
            {
                string species = gridRow.Cells[ColumnSpecies.Index].Value.ToString();

                // If there is no reference of such species in reference
                if (SpeciesIndex== null || SpeciesIndex.Species.FindBySpecies(species) == null)
                {
                    // Create new SpeciesRow

                    Data.SpeciesRow newSpeciesRow = (Data.SpeciesRow)data.Species.Rows.Add(null, species);
                    result.SpcID = newSpeciesRow.ID;
                }
                else
                {
                    // There is such species in reference you using
                    Data.SpeciesRow existingSpeciesRow = data.Species.FindBySpecies(species);

                    if (existingSpeciesRow == null)
                    {
                        existingSpeciesRow = (Data.SpeciesRow)data.Species.Rows.Add(null, species);
                    }

                    result.SpeciesRow = existingSpeciesRow;
                }
            }

            if (gridRow.Cells[ColumnQuantity.Index].Value == null)
            {
                result.SetQuantityNull();
            }
            else
            {
                result.Quantity = (int)gridRow.Cells[ColumnQuantity.Index].Value;
            }

            if (gridRow.Cells[ColumnMass.Index].Value == null)
            {
                result.SetMassNull();
            }
            else
            {
                result.Mass = (double)gridRow.Cells[ColumnMass.Index].Value;
            }

            return result;
        }

        private Data.LogRow SaveLogRow(DataGridViewRow gridRow)
        {
            return SaveLogRow(Data, gridRow);
        }

        private Data.LogRow SaveLogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result = LogRow(data, gridRow);
            if (data.Log.Rows.IndexOf(result) == -1) data.Log.AddLogRow(result);
            if (data == Data) gridRow.Cells[ColumnID.Index].Value = result.ID;
            return result;
        }

        public void LoadData(string fileName)
        {
            Clear();
            Data = new Data();
            Data.Read(fileName);
            LoadData();
            FileName = fileName;

            IsChanged = false;
        }

        private void LoadData()
        {
            #region Header

            if (Data.Solitary.IsLabelNull())
            {
                textBoxLabel.Text = String.Empty;
            }
            else
            {
                textBoxLabel.Text = Data.Solitary.Label;
            }

            #endregion

            waypointControl1.Waypoint = Data.Solitary.Position;
            waypointControl1.UpdateValues();

            if (Data.Solitary.IsCommentsNull())
            {
                textBoxComments.Text = string.Empty;
            }
            else
            {
                textBoxComments.Text = Data.Solitary.Comments;
            }

            spreadSheetLog.Rows.Clear();
            InsertLogRows(Data, 0);

            #region Factors

            spreadSheetAddt.Rows.Clear();
            if (Data.Factor.Count > 0)
            {
                tabPageFactors.Parent = tabControl;
                foreach (Data.FactorValueRow factorValueRow in Data.FactorValue)
                {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetAddt);
                    gridRow.Cells[ColumnAddtFactor.Index].Value = factorValueRow.FactorRow.Factor;
                    gridRow.Cells[ColumnAddtValue.Index].Value = factorValueRow.Value;

                    spreadSheetAddt.Rows.Add(gridRow);
                }
            }
            else
            {
                tabPageFactors.Parent = null;
            }

            #endregion

            UpdateStatus();
            IsChanged = false;
        }

        private void InsertLogRows(Data data, int rowIndex)
        {
            if (rowIndex == -1)
            {
                rowIndex = spreadSheetLog.RowCount - 1;
            }

            foreach (Data.LogRow logRow in data.Log.Rows)
            {
                InsertLogRow(logRow, rowIndex);

                if (rowIndex < spreadSheetLog.RowCount - 1)
                {
                    rowIndex++;
                }
            }
        }

        private void InsertLogRow(Data.LogRow logRow, int rowIndex)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetLog);
            gridRow.Cells[ColumnID.Index].Value = logRow.ID;
            gridRow.Cells[ColumnSpecies.Index].Value = logRow.SpeciesRow.Species;
            if (!logRow.IsQuantityNull()) gridRow.Cells[ColumnQuantity.Index].Value = logRow.Quantity;
            if (!logRow.IsMassNull()) gridRow.Cells[ColumnMass.Index].Value = logRow.Mass;
            if (!logRow.IsCommentsNull())
            {
                gridRow.Cells[ColumnMass.Index].ToolTipText = logRow.Comments;
            }
            spreadSheetLog.Rows.Insert(rowIndex, gridRow);
            HandleLogRow(gridRow);
        }

        public static bool IsDataPresented(string text)
        {
            try
            {
                Data data = new Data();
                data.ReadXml(new StringReader(text));
                return data.Log.Count > 0;
            }
            catch { return false; }
        }

        private DialogResult CheckAndSave()
        {
            DialogResult result = DialogResult.None;

            if (IsChanged)
            {
                TaskDialogButton tdbPressed = taskDialogSaveChanges.ShowDialog(this);

                if (tdbPressed == tdbSave)
                {
                    menuItemSave_Click(ToolStripMenuItemSave, new EventArgs());
                    result = DialogResult.OK;
                }
                else if (tdbPressed == tdbDiscard)
                {
                    IsChanged = false;
                    result = DialogResult.No;
                }
                else if (tdbPressed == tdbCancelClose)
                {
                    result = DialogResult.Cancel;
                }
            }

            return result;
        }

        public int InsertSpecies(string species)
        {
            int speciesIndex = -1;

            // Try to find species in the list
            foreach (DataGridViewRow gridRow in spreadSheetLog.Rows)
            {
                if (gridRow.Cells[ColumnSpecies.Index].Value == null) continue;

                if (object.Equals(gridRow.Cells[ColumnSpecies.Index].Value, species))
                {
                    speciesIndex = gridRow.Index;
                    break;
                }
            }

            // If there is no such species - insert the new row
            if (speciesIndex == -1)
            {
                Data.Species.Rows.Add(null, species);
                speciesIndex = spreadSheetLog.Rows.Add(null, species);
            }

            // Select the new row and its first cell
            spreadSheetLog.ClearSelection();
            spreadSheetLog.Rows[speciesIndex].Selected = true;
            spreadSheetLog.CurrentCell = spreadSheetLog.Rows[speciesIndex].Cells[ColumnSpecies.Index];

            // Then insert species in selected cell
            speciesLogger.InsertSpeciesHere(species);

            return speciesIndex;
        }

        public void OpenSpecies(string species)
        {
            SpeciesToOpen = species;
            Load += new EventHandler(CardOpenSpecies_Load);
        }

        #endregion



        private void Card_Load(object sender, EventArgs e)
        {
            IsChanged = false;
            UpdateStatus();
        }

        private void CardOpenSpecies_Load(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPageLog;
            InsertSpecies(SpeciesToOpen);
            if (AutoLogOpen)
            {
                ToolStripMenuItemIndividuals_Click(spreadSheetLog, new EventArgs());
            }

            IsChanged = false;
        }

        private void Card_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (CheckAndSave() == DialogResult.Cancel);
        }

        private void value_Changed(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabPageLog)
            {
                spreadSheetLog.Focus();
            }
        }

        #region File menu

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                Clear();
                IsChanged = false;
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (OpenDialog.FileName == FileName)
                {
                    statusCard.Message(Wild.Resources.Messages.AlreadyOpened);
                }
                else
                {
                    if (CheckAndSave() != DialogResult.Cancel)
                    {
                        LoadData(OpenDialog.FileName);
                    }
                }
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (FileName == null)
            {
                menuItemSaveAs_Click(sender, e);
            }
            else
            {
                Save(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveData();
            SaveDialog.FileName = FileSystem.SuggestName(FileSystem.FolderName(SaveDialog.FileName),
                Data.SuggestName(Path.GetExtension(SaveDialog.FileName)));

            if (SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(SaveDialog.FileName);
            }
        }

        private void menuItemPrintPreview_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                SaveData();
            }

            //Data.HTML(System.IO.Path.GetFileNameWithoutExtension(FileName)).Preview(this);
            //Data.GetReport(System.IO.Path.GetFileNameWithoutExtension(FileName)).Run();
        }

        private void menuItemPrint_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                SaveData();
            }

            //if (ModifierKeys.HasFlag(Keys.Control))
            //{
            //    Data.HTML(System.IO.Path.GetFileNameWithoutExtension(FileName)).PrintNow();
            //}
            //else
            //{
            //    Data.HTML(System.IO.Path.GetFileNameWithoutExtension(FileName)).Print();
            //}
        }

        private void menuItemLogBlank_Click(object sender, EventArgs e)
        {
            //if (ModifierKeys.HasFlag(Keys.Control))
            //{
            //    Data.BlankLog.PrintNow();
            //}
            //else
            //{
            //    Data.BlankLog.Print();
            //}
        }

        private void menuItemCardBlank_Click(object sender, EventArgs e)
        {
            //if (ModifierKeys.HasFlag(Keys.Control))
            //{
            //    Data.BlankCard.PrintNow();
            //}
            //else
            //{
            //    Data.BlankCard.Print();
            //}
        }

        private void menuItemIndividualsLogBlank_Click(object sender, EventArgs e)
        {
            //if (ModifierKeys.HasFlag(Keys.Control))
            //{
            //    Data.BlankIndividuals.PrintNow();
            //}
            //else
            //{
            //    Data.BlankIndividuals.Print();
            //}
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Data menu

        private void addFactorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPageFactors.Parent = tabControl;
            tabControl.SelectedTab = tabPageFactors;
        }

        private void ToolStripMenuItemGPSImport_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                waypointControl1.SelectGPS(FileSystem.InterfaceLocation.OpenDialog.FileNames);
            }
        }

        #endregion

        #region Service menu

        private void ToolStripMenuItemSpeciesRef_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(SpeciesIndexPath);
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            CoordinateFormat writeMode = Mayfly.UserSettings.FormatLocation;

            string currentSpc = UserSettings.SpeciesIndexPath;
            int currentRecentCount = UserSettings.RecentSpeciesCount;

            Settings settings = new Settings();

            if (settings.ShowDialog() == DialogResult.OK)
            {
                if (currentSpc != UserSettings.SpeciesIndexPath)
                {
                    UserSettings.SpeciesIndex = null;
                    speciesLogger.IndexPath = UserSettings.SpeciesIndexPath;
                }

                if (currentSpc != UserSettings.SpeciesIndexPath ||
                    currentRecentCount != UserSettings.RecentSpeciesCount)
                {
                    speciesLogger.RecentListCount = UserSettings.RecentSpeciesCount;
                    speciesLogger.UpdateRecent();
                }

                if (writeMode != Mayfly.UserSettings.FormatLocation)
                {
                    waypointControl1.ResetAppearance(Mayfly.UserSettings.FormatLocation);
                }

                ColumnQuantity.ReadOnly = UserSettings.FixTotals;
                ColumnMass.ReadOnly = UserSettings.FixTotals;
            }
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }

        #endregion

        private void waterSelector_WaterSelected(object sender, WaterEventArgs e)
        {
            statusCard.Message(Wild.Resources.Messages.WaterSet);
        }

        #region Log tab logics

        private void speciesLogger_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            HandleLogRow(e.Row);
            value_Changed(sender, e);

            if (AutoLogOpen)
            {
                spreadSheetLog.ClearSelection();
                e.Row.Selected = true;
                ToolStripMenuItemIndividuals_Click(ToolStripMenuItemIndividuals, new EventArgs());
            }
        }

        #region Log grid logics

        private void spreadSheetLog_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
        }

        private void spreadSheetLog_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (spreadSheetLog.Focused)
                UpdateStatus();
        }

        private void spreadSheetLog_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Clear(e.Row);
            IsChanged = true;
            UpdateStatus();
        }

        private void spreadSheetLog_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateStatus();
            IsChanged = true;
        }

        private void spreadSheetLog_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateStatus();
            IsChanged = true;
        }

        #endregion

        #region Log menu logics

        private void contextMenuStripLog_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItemPaste.Enabled = Clipboard.ContainsText() &&
                Data.ContainsLog(Clipboard.GetText());
        }

        private void ToolStripMenuItemIndividuals_Click(object sender, EventArgs e)
        {
            //spreadSheetLog.EndEdit();

            if (logRowHandler == null) return;

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.Cells[ColumnSpecies.Name].Value != null)
                {                    
                    logRowHandler.Invoke(gridRow, SaveLogRow);
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
            Data clipData = new Data();
            Data.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            foreach (DataGridViewRow selectedRow in spreadSheetLog.SelectedRows)
            {
                if (selectedRow.IsNewRow)
                {
                    continue;
                }

                Data.LogRow clipLogRow = LogRow(clipData, selectedRow);

                if (selectedRow.Cells[ColumnID.Index].Value != null)
                {
                    Data.LogRow logRow = Data.Log.FindByID((int)selectedRow.Cells[ColumnID.Index].Value);

                    if (logRow != null)
                    {
                        foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                        {
                            individualRow.CopyIndividualData(clipLogRow);
                        }

                        //foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                        //{
                        //    Data.StratifiedRow clipStratifiedRow = clipData.Stratified.NewStratifiedRow();

                        //    clipStratifiedRow.Class = stratifiedRow.Class;
                        //    if (!stratifiedRow.IsCountNull()) clipStratifiedRow.Count = stratifiedRow.Count;
                        //    clipStratifiedRow.LogRow = clipLogRow;
                        //    clipData.Stratified.AddStratifiedRow(clipStratifiedRow);
                        //}
                    }
                }
            }

            Clipboard.SetText(clipData.GetXml());
        }

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            Data clipData = new Data();
            clipData.ReadXml(new StringReader(Clipboard.GetText()));

            int rowIndex = spreadSheetLog.SelectedRows[0].Index;

            foreach (Data.LogRow clipLogRow in clipData.Log)
            {
                // Copy from Clipboard Data to local Data
                Data.LogRow logRow = Data.Log.NewLogRow();
                if (!clipLogRow.IsQuantityNull()) logRow.Quantity = clipLogRow.Quantity;
                if (!clipLogRow.IsMassNull()) logRow.Mass = clipLogRow.Mass;
                logRow.CardRow = Data.Solitary;

                SpeciesKey.SpeciesRow clipSpeciesRow = SpeciesIndex.Species.FindBySpecies(clipLogRow.SpeciesRow.Species);

                if (clipSpeciesRow == null)
                {
                    Data.SpeciesRow newSpeciesRow = Data.Species.AddSpeciesRow(
                        clipLogRow.SpeciesRow.Species);

                    logRow.SpcID = newSpeciesRow.ID;
                }
                else
                {
                    if (Data.Species.FindBySpecies(clipSpeciesRow.Species) == null)
                    {
                        Data.Species.Rows.Add(clipSpeciesRow.ID, clipSpeciesRow.Species);
                    }
                    logRow.SpcID = clipSpeciesRow.ID;
                }

                Data.Log.AddLogRow(logRow);

                foreach (Data.IndividualRow clipIndividualRow in clipLogRow.GetIndividualRows())
                {
                    clipIndividualRow.CopyIndividualData(logRow);
                }

                //foreach (Data.StratifiedRow clipStratifiedRow in clipData.Stratified)
                //{
                //    Data.StratifiedRow stratifiedRow = Data.Stratified.NewStratifiedRow();
                //    stratifiedRow.Class = clipStratifiedRow.Class;
                //    if (!clipStratifiedRow.IsCountNull()) stratifiedRow.Count = clipStratifiedRow.Count;
                //    stratifiedRow.LogRow = logRow;
                //    Data.Stratified.AddStratifiedRow(stratifiedRow);
                //}

                InsertLogRow(logRow, rowIndex);

                if (rowIndex < spreadSheetLog.RowCount - 1)
                {
                    rowIndex++;
                }
            }

            IsChanged = true;
            UpdateStatus();
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
            UpdateStatus();
        }

        private void ToolStripMenuItemSpeciesKey_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in spreadSheetLog.SelectedRows)
            {
                speciesLogger.FindInKey(selectedRow);
            }
        }

        #endregion

        #endregion

        #region Factors tab logics

        private void spreadSheetAddt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
            HandleFactorRow(spreadSheetAddt.Rows[e.RowIndex]);
        }

        #endregion
    }
}