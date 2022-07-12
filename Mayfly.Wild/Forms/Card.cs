using Mayfly.Extensions;
using Mayfly.TaskDialogs;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Mayfly.Wild.ReaderSettings;
using static Mayfly.Wild.UserSettings;

namespace Mayfly.Wild
{
    public partial class Card : Form
    {
        private string filename;
        protected Data data = new Data();
        protected bool isChanged;
        protected EventHandler saved;
        protected EventHandler waterSelected;
        protected EventHandler cleared;

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

        [Category("Mayfly Events"), Browsable(true)]
        public event EventHandler OnCleared {
            add { cleared += value; }
            remove { cleared -= value; }
        }



        public Card() {

            InitializeComponent();

            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);
        }


        protected void Initiate() {

            Logger.Data = data;

            this.ResetText(Interface.NewFilename, EntryAssemblyInfo.Title);

            waterSelector.CreateList();
            waterSelector.Index = WatersIndex;

            if (WatersIndex != null && SelectedWaterID != 0) {
                WatersKey.WaterRow selectedWater = WatersIndex.Water.FindByID(SelectedWaterID);

                if (selectedWater != null) {
                    waterSelector.WaterObject = selectedWater;
                }
            }

            if (SamplersIndex != null) {
                comboBoxSampler.DataSource = SamplersIndex.Sampler.Select();
                comboBoxSampler.SelectedIndex = -1;
            }

            if (SelectedDate != null) {
                waypointControl1.Waypoint.TimeMark = SelectedDate;
            }

            Logger.Provider.RecentListCount = RecentSpeciesCount;
            Logger.Provider.IndexPath = TaxonomicIndexPath;

            ColumnQuantity.ReadOnly = FixTotals;
            ColumnMass.ReadOnly = FixTotals;

            ToolStripMenuItemWatersRef.Enabled = WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = TaxonomicIndexPath != null;
            spreadSheetAddt.StringVariants = AddtFactors;

            tabPageEnvironment.Parent = null;
            tabPageFactors.Parent = null;

            MenuStrip.SetMenuIcons();

            Logger.UpdateStatus();

            isChanged = false;
        }


        private void clear() {

            this.ResetText(Interface.NewFilename, EntryAssemblyInfo.Title);
            data = null;

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;
            waypointControl1.Clear();
            textBoxComments.Text = string.Empty;
            aquaControl1.Clear();
            weatherControl1.Clear();
            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();

            if (cleared != null) cleared.Invoke(this, EventArgs.Empty);
        }

        private void load(string filename) {
            clear();
            data.Read(filename);

            #region Header

            if (data.Solitary.IsLabelNull()) {
                textBoxLabel.Text = String.Empty;
            } else {
                textBoxLabel.Text = data.Solitary.Label;
            }

            if (data.Solitary.IsWaterIDNull()) {
                waterSelector.WaterObject = null;
            } else {
                WatersKey.WaterRow waterRow = null;

                if (WatersIndex != null) {
                    waterRow = WatersIndex.Water.FindByID(data.Solitary.WaterID);
                }

                if (waterRow == null) {
                    waterRow = waterSelector.Index.Water.NewWaterRow();
                    waterRow.ID = data.Solitary.WaterRow.ID;
                    if (!data.Solitary.WaterRow.IsWaterNull()) waterRow.Water = data.Solitary.WaterRow.Water;
                    waterRow.Type = data.Solitary.WaterRow.Type;
                }

                waterSelector.WaterObject = waterRow;
            }

            #endregion

            #region Location

            waypointControl1.Waypoint = data.Solitary.Position;
            waypointControl1.UpdateValues();

            #endregion

            #region Sampling

            if (data.Solitary.IsSamplerNull()) {
                comboBoxSampler.SelectedIndex = -1;
            } else {
                SelectedSampler = data.Solitary.SamplerRow;
            }

            #endregion

            if (data.Solitary.IsCommentsNull()) {
                textBoxComments.Text = string.Empty;
            } else {
                textBoxComments.Text = data.Solitary.Comments;
            }

            if (data.Solitary.IsEnvironmentDescribed) {
                tabPageEnvironment.Parent = tabControl;

                if (data.Solitary.StateOfWater == null) {
                    aquaControl1.Clear();
                } else {
                    aquaControl1.AquaState = data.Solitary.StateOfWater;
                    aquaControl1.UpdateValues();
                }

                if (data.Solitary.IsWeatherNull()) {
                    weatherControl1.Clear();
                } else {
                    weatherControl1.Weather = data.Solitary.WeatherConditions;
                    weatherControl1.UpdateValues();
                }
            } else {
                tabPageEnvironment.Parent = null;
            }

            spreadSheetLog.Rows.Clear();
            Logger.InsertLogRows(data, 0);

            #region Factors

            spreadSheetAddt.Rows.Clear();
            if (data.Factor.Count > 0) {
                tabPageFactors.Parent = tabControl;
                foreach (Data.FactorValueRow factorValueRow in data.FactorValue) {
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

        private void loadEquipment() {
            contextSampler.Items.Clear();

            foreach (Equipment.UnitsRow unitRow in UserSettings.Equipment.Units) {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = unitRow.ToString();
                item.Click += (o, e) => {
                    comboBoxSampler.SelectedItem = ReaderSettings.SamplersIndex.Sampler.FindByID(unitRow.SamplerID);
                    if (!unitRow.IsMeshNull()) textBoxMesh.Text = unitRow.Mesh.ToString();
                    if (!unitRow.IsHookNull()) textBoxHook.Text = unitRow.Hook.ToString();
                    if (!unitRow.IsLengthNull()) textBoxLength.Text = unitRow.Length.ToString();
                    if (!unitRow.IsHeightNull()) textBoxHeight.Text = unitRow.Height.ToString();
                    if (!unitRow.IsOpeningNull()) textBoxOpening.Text = unitRow.Opening.ToString();
                };
                contextGear.Items.Add(item);
            }

            buttonGear.Visible = contextGear.Items.Count > 0;

        }

        private void saveCollect() {
            if (textBoxLabel.Text.IsAcceptable()) {
                data.Solitary.Label = textBoxLabel.Text;
            } else {
                data.Solitary.SetLabelNull();
            }

            if (waterSelector.IsWaterSelected) {
                if (data.Solitary.WaterID == waterSelector.WaterObject.ID) { } else {

                    Data.WaterRow newWaterRow = data.Water.NewWaterRow();
                    newWaterRow.ID = waterSelector.WaterObject.ID;
                    newWaterRow.Type = waterSelector.WaterObject.Type;
                    if (!waterSelector.WaterObject.IsWaterNull()) {
                        newWaterRow.Water = waterSelector.WaterObject.Water;
                    }
                    data.Water.AddWaterRow(newWaterRow);
                    data.Solitary.WaterRow = newWaterRow;
                }
                SelectedWaterID = waterSelector.WaterObject.ID;
            } else {
                SelectedWaterID = 0;
                data.Solitary.SetWaterIDNull();
                data.Water.Clear();
            }

            waypointControl1.Save();

            data.Solitary.When = waypointControl1.Waypoint.TimeMark;
            if (!waypointControl1.Waypoint.IsEmpty)
                data.Solitary.Where = waypointControl1.Waypoint.Protocol;

            if (filename == null) {
                data.Solitary.AttachSign();
            } else {
                data.Solitary.RenewSign();
            }

            if (textBoxComments.Text.IsAcceptable()) {
                data.Solitary.Comments = textBoxComments.Text.Trim();
            } else {
                data.Solitary.SetCommentsNull();
            }
        }

        private void saveSampler() {
            if (SelectedSampler == null) {
                data.Solitary.SetSamplerNull();
            } else {
                data.Solitary.Sampler = SelectedSampler.ID;
            }
        }

        private void saveEnvironment() {

            aquaControl1.Save();

            if (aquaControl1.AquaState != null) {
                if (aquaControl1.AquaState.IsPhysicalsAvailable) data.Solitary.Physicals = aquaControl1.AquaState.PhysicalsProtocol;
                else data.Solitary.SetPhysicalsNull();

                if (aquaControl1.AquaState.IsChemicalsAvailable) data.Solitary.Chemicals = aquaControl1.AquaState.ChemicalsProtocol;
                else data.Solitary.SetChemicalsNull();

                if (aquaControl1.AquaState.IsOrganolepticsAvailable) data.Solitary.Organoleptics = aquaControl1.AquaState.OrganolepticsProtocol;
                else data.Solitary.SetOrganolepticsNull();
            } else {
                data.Solitary.SetPhysicalsNull();
                data.Solitary.SetChemicalsNull();
                data.Solitary.SetOrganolepticsNull();
            }

            weatherControl1.Save();

            if (weatherControl1.Weather.IsAvailable) {
                data.Solitary.Weather = weatherControl1.Weather.Protocol;
            } else {
                data.Solitary.SetWeatherNull();
            }
        }

        private void saveLog() {
            Logger.SaveLog();
        }

        private void saveAddt() {
            data.FactorValue.Clear();
            data.Factor.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetAddt.Rows) {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnAddtFactor.Index].Value == null) continue;

                if (gridRow.Cells[ColumnAddtValue.Index].Value == null) continue;

                string factorName = (string)gridRow.Cells[ColumnAddtFactor.Index].Value;

                double factorValue = (double)gridRow.Cells[ColumnAddtValue.Index].Value;

                data.FactorValue.AddFactorValueRow(data.Solitary, data.Factor.AddFactorRow(factorName), factorValue);

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
            if (saved != null) saved.Invoke(this, EventArgs.Empty);
            data.ClearUseless();
        }

        private void write() {
            if (SpeciesAutoExpand) {
                Logger.Provider.UpdateIndex(data.GetSpeciesKey(), SpeciesAutoExpandVisual);
            }
            data.WriteToFile(filename);
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



        private void card_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = (checkAndSave() == DialogResult.Cancel);
        }

        protected void value_Changed(object sender, EventArgs e) {
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
            if (Interface.OpenDialog.ShowDialog() == DialogResult.OK) {
                if (Interface.OpenDialog.FileName == filename) {
                    statusCard.Message(Resources.Interface.Messages.AlreadyOpened);
                } else {
                    if (checkAndSave() != DialogResult.Cancel) {
                        load(Interface.OpenDialog.FileName);
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
            Interface.ExportDialog.FileName = Interface.SuggestName(data.Solitary.GetSuggestedName());
            if (Interface.ExportDialog.ShowDialog() == DialogResult.OK) {

                string ext = Path.GetExtension(Interface.ExportDialog.FileName);

                if (ext == Interface.Extension) {
                    filename = Interface.ExportDialog.FileName;
                    write();
                } else if (ext == ".html") {
                    data.Solitary.GetReport().WriteToFile(Interface.ExportDialog.FileName);
                }
            }
        }

        private void menuItemPrintPreview_Click(object sender, EventArgs e) {
            if (isChanged) {
                save();
            }

            data.Solitary.GetReport().Preview();
        }

        private void menuItemPrint_Click(object sender, EventArgs e) {
            if (isChanged) {
                save();
            }

            data.Solitary.GetReport().Print();
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
            IO.RunFile(WatersIndexPath);
        }

        private void menuItemSpecies_Click(object sender, EventArgs e) {
            IO.RunFile(TaxonomicIndexPath, "-edit");
        }

        private void menuItemSettings_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void menuItemAbout_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void itemAboutCard_Click(object sender, EventArgs e) {
            AboutData about = new AboutData(data.Solitary.Investigator);
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