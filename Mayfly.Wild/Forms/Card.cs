using Mayfly.Extensions;
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
using Mayfly.Waters.Controls;

namespace Mayfly.Wild
{
    public partial class Card : Form
    {
        private string filename;
        private Data data;
        private bool isChanged;
        protected EventHandler saved;
        protected EventHandler waterSelected;

        [Browsable(false)]
        public Data Data {
            get {

                if (data == null) {
                    data = new Data(UserSettings.TaxonomicIndex, UserSettings.SamplersIndex);
                }

                return data;
            }
        }

        [Browsable(false)]
        public Samplers.SamplerRow SelectedSampler {
            get {
                return comboBoxSampler.SelectedItem as Samplers.SamplerRow;
            }
            set {
                comboBoxSampler.SelectedItem = value;
            }
        }

        [Category("Mayfly Events"), Browsable(true)]
        public event EventHandler OnSaved {
            add { saved += value; }
            remove { saved -= value; }
        }

        [Category("Mayfly Events"), Browsable(true)]
        public event EventHandler OnWaterSelected {
            add { waterSelected += value; }
            remove { waterSelected -= value; }
        }



        public Card() {

            InitializeComponent();

            Logger.Data = Data;

            this.ResetText(UserSettings.Interface.NewFilename, EntryAssemblyInfo.Title);

            waterSelector.CreateList();
            waterSelector.Index = Wild.UserSettings.WatersIndex;

            if (Wild.UserSettings.WatersIndex != null && UserSettings.SelectedWaterID != 0) {
                WatersKey.WaterRow selectedWater = Wild.UserSettings.WatersIndex.Water.FindByID(
                    UserSettings.SelectedWaterID);

                if (selectedWater != null) {
                    waterSelector.WaterObject = selectedWater;
                }
            }

            comboBoxSampler.DataSource = UserSettings.SamplersIndex.Sampler.Select();
            comboBoxSampler.SelectedIndex = -1;

            SelectedSampler = UserSettings.SelectedSampler;

            if (UserSettings.SelectedDate != null) {
                waypointControl1.Waypoint.TimeMark = UserSettings.SelectedDate;
            }

            Logger.Provider.RecentListCount = UserSettings.RecentSpeciesCount;
            Logger.Provider.IndexPath = UserSettings.TaxonomicIndexPath;

            ColumnQuantity.ReadOnly = UserSettings.FixTotals;
            ColumnMass.ReadOnly = UserSettings.FixTotals;

            ToolStripMenuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = UserSettings.TaxonomicIndexPath != null;

            tabPageEnvironment.Parent = null;

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);

            MenuStrip.SetMenuIcons();

            Logger.UpdateStatus();

            isChanged = false;
        }



        private void clear() {

            this.ResetText(UserSettings.Interface.NewFilename, EntryAssemblyInfo.Title);
            data = null;

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;
            waypointControl1.Clear();
            textBoxComments.Text = string.Empty;
            aquaControl1.Clear();
            weatherControl1.Clear();
            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();
        }

        private void load(string filename) {
            clear();
            Data.Read(filename);

            #region Header

            if (Data.Solitary.IsLabelNull()) {
                textBoxLabel.Text = String.Empty;
            } else {
                textBoxLabel.Text = Data.Solitary.Label;
            }

            if (Data.Solitary.IsWaterIDNull()) {
                waterSelector.WaterObject = null;
            } else {
                WatersKey.WaterRow waterRow = null;

                if (Wild.UserSettings.WatersIndex != null) {
                    waterRow = Wild.UserSettings.WatersIndex.Water.FindByID(Data.Solitary.WaterID);
                }

                if (waterRow == null) {
                    waterRow = waterSelector.Index.Water.NewWaterRow();
                    waterRow.ID = Data.Solitary.WaterRow.ID;
                    if (!Data.Solitary.WaterRow.IsWaterNull()) waterRow.Water = Data.Solitary.WaterRow.Water;
                    waterRow.Type = Data.Solitary.WaterRow.Type;
                }

                waterSelector.WaterObject = waterRow;
            }

            #endregion

            #region Location

            waypointControl1.Waypoint = Data.Solitary.Position;
            waypointControl1.UpdateValues();

            #endregion

            #region Sampling

            if (Data.Solitary.IsSamplerNull()) {
                comboBoxSampler.SelectedIndex = -1;
            } else {
                SelectedSampler = Data.Solitary.SamplerRow;
            }

            #endregion

            if (Data.Solitary.IsCommentsNull()) {
                textBoxComments.Text = string.Empty;
            } else {
                textBoxComments.Text = Data.Solitary.Comments;
            }

            if (Data.Solitary.IsEnvironmentDescribed) {
                tabPageEnvironment.Parent = tabControl;

                if (Data.Solitary.StateOfWater == null) {
                    aquaControl1.Clear();
                } else {
                    aquaControl1.AquaState = Data.Solitary.StateOfWater;
                    aquaControl1.UpdateValues();
                }

                if (Data.Solitary.IsWeatherNull()) {
                    weatherControl1.Clear();
                } else {
                    weatherControl1.Weather = Data.Solitary.WeatherConditions;
                    weatherControl1.UpdateValues();
                }
            } else {
                tabPageEnvironment.Parent = null;
            }

            spreadSheetLog.Rows.Clear();
            Logger.InsertLogRows(Data, 0);

            #region Factors

            spreadSheetAddt.Rows.Clear();
            if (Data.Factor.Count > 0) {
                tabPageFactors.Parent = tabControl;
                foreach (Data.FactorValueRow factorValueRow in Data.FactorValue) {
                    DataGridViewRow gridRow = new DataGridViewRow();
                    gridRow.CreateCells(spreadSheetAddt);
                    gridRow.Cells[ColumnAddtFactor.Index].Value = factorValueRow.FactorRow.Factor;
                    gridRow.Cells[ColumnAddtValue.Index].Value = factorValueRow.Value;

                    spreadSheetAddt.Rows.Add(gridRow);
                }
            } else {
                tabPageFactors.Parent = null;
            }

            #endregion

            Logger.UpdateStatus();
            isChanged = false;


            this.ResetText(filename, EntryAssemblyInfo.Title);
            menuItemAboutCard.Visible = true;
            Log.Write("Loaded from {0}.", filename);
            isChanged = false;
        }

        private void saveCollect() {
            if (textBoxLabel.Text.IsAcceptable()) {
                Data.Solitary.Label = textBoxLabel.Text;
            } else {
                Data.Solitary.SetLabelNull();
            }

            if (waterSelector.IsWaterSelected) {
                if (Data.Solitary.WaterID == waterSelector.WaterObject.ID) { } else {

                    Data.WaterRow newWaterRow = Data.Water.NewWaterRow();
                    newWaterRow.ID = waterSelector.WaterObject.ID;
                    newWaterRow.Type = waterSelector.WaterObject.Type;
                    if (!waterSelector.WaterObject.IsWaterNull()) {
                        newWaterRow.Water = waterSelector.WaterObject.Water;
                    }
                    Data.Water.AddWaterRow(newWaterRow);
                    Data.Solitary.WaterRow = newWaterRow;
                }
                UserSettings.SelectedWaterID = waterSelector.WaterObject.ID;
            } else {
                UserSettings.SelectedWaterID = 0;
                Data.Solitary.SetWaterIDNull();
                Data.Water.Clear();
            }

            waypointControl1.Save();

            Data.Solitary.When = waypointControl1.Waypoint.TimeMark;
            if (!waypointControl1.Waypoint.IsEmpty)
                Data.Solitary.Where = waypointControl1.Waypoint.Protocol;

            if (filename == null) {
                Data.Solitary.AttachSign();
            } else {
                Data.Solitary.RenewSign();
            }

            if (textBoxComments.Text.IsAcceptable()) {
                Data.Solitary.Comments = textBoxComments.Text.Trim();
            } else {
                Data.Solitary.SetCommentsNull();
            }
        }

        private void saveSampler() {
            if (SelectedSampler == null) {
                Data.Solitary.SetSamplerNull();
            } else {
                Data.Solitary.Sampler = SelectedSampler.ID;
            }
        }

        private void saveEnvironment() {

            aquaControl1.Save();

            if (aquaControl1.AquaState != null) {
                if (aquaControl1.AquaState.IsPhysicalsAvailable) Data.Solitary.Physicals = aquaControl1.AquaState.PhysicalsProtocol;
                else Data.Solitary.SetPhysicalsNull();

                if (aquaControl1.AquaState.IsChemicalsAvailable) Data.Solitary.Chemicals = aquaControl1.AquaState.ChemicalsProtocol;
                else Data.Solitary.SetChemicalsNull();

                if (aquaControl1.AquaState.IsOrganolepticsAvailable) Data.Solitary.Organoleptics = aquaControl1.AquaState.OrganolepticsProtocol;
                else Data.Solitary.SetOrganolepticsNull();
            } else {
                Data.Solitary.SetPhysicalsNull();
                Data.Solitary.SetChemicalsNull();
                Data.Solitary.SetOrganolepticsNull();
            }

            weatherControl1.Save();

            if (weatherControl1.Weather.IsAvailable) {
                Data.Solitary.Weather = weatherControl1.Weather.Protocol;
            } else {
                Data.Solitary.SetWeatherNull();
            }
        }

        private void saveLog() {
            Logger.SaveLog();
        }

        private void saveAddt() {
            Data.FactorValue.Clear();
            Data.Factor.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetAddt.Rows) {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnAddtFactor.Index].Value == null) continue;

                if (gridRow.Cells[ColumnAddtValue.Index].Value == null) continue;

                string factorName = (string)gridRow.Cells[ColumnAddtFactor.Index].Value;

                double factorValue = (double)gridRow.Cells[ColumnAddtValue.Index].Value;

                Data.FactorValue.AddFactorValueRow(Data.Solitary, Data.Factor.AddFactorRow(factorName), factorValue);

                if (!Wild.UserSettings.AddtFactors.Contains(factorName)) {
                    List<string> factors = new List<string>();
                    factors.AddRange(Wild.UserSettings.AddtFactors);
                    factors.Add(factorName);
                    Wild.UserSettings.AddtFactors = factors.ToArray();
                }
            }
        }

        private void save() {
            saveCollect();
            saveSampler();
            saveEnvironment();
            saveLog();
            saveAddt();

            Data.ClearUseless();
            if (saved != null) saved.Invoke(this, EventArgs.Empty);
        }

        private void write() {
            if (UserSettings.SpeciesAutoExpand) {
                Logger.Provider.UpdateIndex(Data.GetSpeciesKey(), UserSettings.SpeciesAutoExpandVisual);
            }
            Data.WriteToFile(filename);
            statusCard.Message(Resources.Interface.Messages.Saved);
            isChanged = false;
        }

        private DialogResult checkAndSave() {
            DialogResult result = DialogResult.None;

            if (isChanged) {
                TaskDialogButton tdbPressed = taskDialogSaveChanges.ShowDialog(this);

                if (tdbPressed == tdbSave) {
                    menuItemSave_Click(menuItemSave, new EventArgs());
                    result = DialogResult.OK;
                } else if (tdbPressed == tdbDiscard) {
                    isChanged = false;
                    result = DialogResult.No;
                } else if (tdbPressed == tdbCancelClose) {
                    result = DialogResult.Cancel;
                }
            }

            return result;
        }

        //private void AddFactorsMenuItems()
        //{
        //    while (contextMenuStripFactor.Items.Count > 1)
        //    {
        //        contextMenuStripFactor.Items.RemoveAt(1);
        //    }

        //    if (Wild.UserSettings.AddtFactors.Length > 0)
        //    {
        //        contextMenuStripFactor.Items.Add(new ToolStripSeparator());
        //        foreach (string addtFactor in Wild.UserSettings.AddtFactors)
        //        {
        //            ToolStripMenuItem NewVarMenu = new ToolStripMenuItem(addtFactor);
        //            NewVarMenu.Click += new EventHandler(NewFactorMenu_Click);
        //            contextMenuStripFactor.Items.Add(NewVarMenu);
        //        }
        //    }
        //}

        //private void AddFactor(string factorName)
        //{
        //    Factor factorControl = new Factor(factorName);
        //    AddFactor(factorControl);
        //    factorControl.FocusValue();
        //}

        //private void AddFactor()
        //{
        //    Factor factorControl = new Factor();
        //    AddFactor(factorControl);
        //    factorControl.FocusName();
        //}

        //private void AddFactor(Factor factorControl)
        //{
        //    factorControl.ValueChanged += new EventHandler(ValueChanged);
        //    factorControl.NameRequired += new EventHandler(FactorControl_NameRequired);
        //    factorControl.Values.AddRange(Wild.UserSettings.AddtFactors);
        //    flowLayoutPanelFactors.Controls.Add(factorControl);
        //}



        private void Card_Load(object sender, EventArgs e) {
            isChanged = false;
            Logger.UpdateStatus();
        }

        private void Card_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = (checkAndSave() == DialogResult.Cancel);
        }

        private void value_Changed(object sender, EventArgs e) {
            isChanged = true;
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e) {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e) {
            ((ComboBox)sender).HandleInput(e);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl.SelectedTab == tabPageLog) {
                spreadSheetLog.Focus();
            }
        }

        #region File menu

        private void menuItemNew_Click(object sender, EventArgs e) {
            if (checkAndSave() != DialogResult.Cancel) {
                clear();
                tabControl.SelectedTab = tabPageCollect;
                isChanged = false;
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e) {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK) {
                if (UserSettings.Interface.OpenDialog.FileName == filename) {
                    statusCard.Message(Wild.Resources.Interface.Messages.AlreadyOpened);
                } else {
                    if (checkAndSave() != DialogResult.Cancel) {
                        load(UserSettings.Interface.OpenDialog.FileName);
                    }
                }
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e) {
            if (filename == null) {
                menuItemSaveAs_Click(sender, e);
            } else {
                save();
                write();
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e) {

            save();
            UserSettings.Interface.ExportDialog.FileName = UserSettings.Interface.SuggestName(Data.Solitary.GetSuggestedName());
            if (UserSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK) {

                string ext = Path.GetExtension(UserSettings.Interface.ExportDialog.FileName);

                if (ext == UserSettings.Interface.Extension) {
                    filename = UserSettings.Interface.ExportDialog.FileName;
                    write();
                } else if (ext == ".html") {
                    Data.Solitary.GetReport().WriteToFile(UserSettings.Interface.ExportDialog.FileName);
                }
            }
        }

        private void menuItemPrintPreview_Click(object sender, EventArgs e) {
            if (isChanged) {
                save();
            }

            Data.Solitary.GetReport().Preview();
        }

        private void menuItemPrint_Click(object sender, EventArgs e) {
            if (isChanged) {
                save();
            }

            Data.Solitary.GetReport().Print();
        }

        private void menuItemCardBlank_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void menuItemIndividualsLogBlank_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void menuItemClose_Click(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region Data menu

        private void addEnvironmentalDataToolStripMenuItem_Click(object sender, EventArgs e) {
            tabPageEnvironment.Parent = tabControl;
            tabControl.SelectedTab = tabPageEnvironment;
        }

        private void addFactorsToolStripMenuItem_Click(object sender, EventArgs e) {
            tabPageFactors.Parent = tabControl;
            tabControl.SelectedTab = tabPageFactors;
        }

        private void menuItemLocation_Click(object sender, EventArgs e) {
            if (IO.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK) {
                waypointControl1.SelectGPS(IO.InterfaceLocation.OpenDialog.FileNames);
            }
        }

        #endregion

        #region Service menu

        private void menuItemWaters_Click(object sender, EventArgs e) {
            IO.RunFile(Wild.UserSettings.WatersIndexPath);
        }

        private void menuItemSpecies_Click(object sender, EventArgs e) {
            IO.RunFile(UserSettings.TaxonomicIndexPath, "-edit");
        }

        private void menuItemSettings_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void menuItemAbout_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void itemAboutCard_Click(object sender, EventArgs e) {
            AboutData about = new AboutData(Data.Solitary.Investigator);
            about.ShowDialog();
        }

        #endregion

        private void waterSelector_WaterSelected(object sender, WaterEventArgs e) {
            statusCard.Message(Resources.Interface.Messages.WaterSet);
            if (waterSelected != null) waterSelected.Invoke(this, EventArgs.Empty);
        }

        private void spreadSheetAddt_CellEndEdit(object sender, DataGridViewCellEventArgs e) {

            isChanged = true;
            DataGridViewRow gridRow = spreadSheetAddt.Rows[e.RowIndex];

            if (gridRow.IsNewRow) {
                return;
            }

            if (gridRow.Cells[ColumnAddtFactor.Index].Value == null ||
                !((string)gridRow.Cells[ColumnAddtFactor.Index].Value).IsAcceptable()) {
                this.NotifyInstantly(Resources.Interface.Messages.FactorNameRequired);
            }

            for (int i = 0; i < spreadSheetAddt.RowCount; i++) {
                DataGridViewRow currentGridRow = spreadSheetAddt.Rows[i];

                if (currentGridRow.IsNewRow) {
                    continue;
                }

                if (currentGridRow == gridRow) {
                    continue;
                }

                if (object.Equals(gridRow.Cells[ColumnAddtFactor.Index].Value,
                    currentGridRow.Cells[ColumnAddtFactor.Index].Value)) {
                    if (gridRow.Cells[ColumnAddtValue.Index].Value == null) {
                        gridRow.Cells[ColumnAddtValue.Index].Value =
                            currentGridRow.Cells[ColumnAddtValue.Index].Value;
                    }

                    spreadSheetAddt.Rows.Remove(currentGridRow);
                    i--;
                }
            }
        }
    }
}