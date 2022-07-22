using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.TaskDialogs;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Mayfly.UserSettings;
using static Mayfly.Wild.SettingsReader;
using Mayfly.Wild.Controls;

namespace Mayfly.Wild
{
    public partial class Card : Form
    {
        private string filename;
        protected Survey data = new Survey();
        protected bool isChanged;
        protected EventHandler saved;
        protected EventHandler waterSelected;
        protected EventHandler cleared;
        protected EquipmentEventHandler equipmentSaved;
        protected EquipmentEventHandler equipmentSelected;
        private EventHandler settingsApplied;

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

        [Category("Mayfly Events"), Browsable(true)]
        public event EquipmentEventHandler OnEquipmentSelected {
            add { equipmentSelected += value; }
            remove { equipmentSelected -= value; }
        }

        [Category("Mayfly Events"), Browsable(true)]
        public event EquipmentEventHandler OnEquipmentSaved {
            add { equipmentSaved += value; }
            remove { equipmentSaved -= value; }
        }

        [Category("Mayfly Events")]
        public event EventHandler SettingsApplied {
            add {
                settingsApplied += value;
            }

            remove {
                settingsApplied -= value;
            }
        }

        public Survey.SamplerRow SelectedSampler {
            get {
                return comboBoxSampler.SelectedItem as Survey.SamplerRow;
            }

            set {
                comboBoxSampler.SelectedItem = value;
            }
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
            waterSelector.Index = UserSettings.WatersIndex;

            if (UserSettings.WatersIndex != null && UserSettings.SelectedWaterID != 0) {
                WatersKey.WaterRow selectedWater = UserSettings.WatersIndex.Water.FindByID(UserSettings.SelectedWaterID);

                if (selectedWater != null) {
                    waterSelector.WaterObject = selectedWater;
                }
            }

            if (SamplersIndex != null) {
                comboBoxSampler.DataSource = SamplersIndex.Sampler.Select();
                comboBoxSampler.SelectedIndex = -1;
            }
            loadEquipment();

            if (UserSettings.SelectedDate != null) {
                waypointControl1.Waypoint.TimeMark = UserSettings.SelectedDate;
            }

            Logger.Provider.RecentListCount = RecentSpeciesCount;
            Logger.Provider.IndexPath = TaxonomicIndexPath;

            ColumnQty.ReadOnly = FixTotals;
            ColumnMass.ReadOnly = FixTotals;

            ToolStripMenuItemWatersRef.Enabled = UserSettings.WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = TaxonomicIndexPath != null;
            spreadSheetAddt.StringVariants = UserSettings.AddtFactors;

            tabPageEnvironment.Parent = null;
            tabPageFactors.Parent = null;

            MenuStrip.SetMenuIcons();

            Logger.UpdateStatus();

            isChanged = false;
        }


        private void clear() {

            this.ResetText(Interface.NewFilename, EntryAssemblyInfo.Title);
            data = new Survey();

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;
            waypointControl1.Clear();
            textBoxComments.Text = string.Empty;
            aquaControl1.Clear();
            weatherControl1.Clear();

            foreach (Survey.VirtueRow virtueRow in SamplersIndex.Virtue) {
                NumberBox nb = tabPageSampler.Controls.Find("numeric" + virtueRow.Name, true)?[0] as NumberBox;
                nb.Clear();
            }

            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();

            if (cleared != null) cleared.Invoke(this, EventArgs.Empty);
        }

        private void loadEquipmentVirtues(Survey.EquipmentRow eqpRow) {

            foreach (Survey.EquipmentVirtueRow row in eqpRow.GetEquipmentVirtueRows()) {
                NumberBox nb = tabPageSampler.Controls.Find("numeric" + row.VirtueRow.Name, true)?[0] as NumberBox;
                nb.Value = row.Value;
            }
        }

        private void loadEquipment() {

            contextEquipment.Items.Clear();

            foreach (Survey.SamplerTypeRow typeRow in Equipment.SamplerType) {

                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = typeRow.Name.GetLocalizedValue();

                foreach (Survey.SamplerRow samplerRow in typeRow.GetSamplerRows()) {

                    foreach (Survey.EquipmentRow eqpRow in samplerRow.GetEquipmentRows()) {

                        ToolStripMenuItem eqpItem = new ToolStripMenuItem();
                        eqpItem.Text = eqpRow.ToString();
                        eqpItem.Click += (o, e) => {
                            comboBoxSampler.SelectedItem = SamplersIndex.Sampler.FindByID(eqpRow.SmpID);
                            loadEquipmentVirtues(eqpRow);
                            saveSampler();
                            if (equipmentSelected != null) equipmentSelected.Invoke(this, new EquipmentEventArgs(eqpRow));
                        };
                        item.DropDownItems.Add(eqpItem);
                    }
                }

                if (item.HasDropDownItems) contextEquipment.Items.Add(item);
            }

            //foreach (Survey.EquipmentRow eqpRow in Equipment.Equipment) {

            //    ToolStripMenuItem item = new ToolStripMenuItem();
            //    item.Text = eqpRow.ToString();
            //    item.Click += (o, e) => {
            //        comboBoxSampler.SelectedItem = SamplersIndex.Sampler.FindByID(eqpRow.SmpID);
            //        loadEquipmentVirtues(eqpRow);
            //        saveSampler();
            //        if (equipmentSelected != null) equipmentSelected.Invoke(this, new EquipmentEventArgs(eqpRow));
            //    };
            //    contextEquipment.Items.Add(item);
            //}

            buttonEquipment.Visible = contextEquipment.Items.Count > 0;
        }

        public void load(string _filename) {

            clear();
            filename = _filename;
            data = new Survey();
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

                if (UserSettings.WatersIndex != null) {
                    waterRow = UserSettings.WatersIndex.Water.FindByID(data.Solitary.WaterID);
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

            if (data.Solitary.IsEqpIDNull()) {
                comboBoxSampler.SelectedIndex = -1;
            } else {
                SelectedSampler = SamplersIndex.Sampler.FindByID(data.Solitary.SamplerRow.ID);
                loadEquipmentVirtues(data.Solitary.EquipmentRow);
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
                foreach (Survey.FactorValueRow factorValueRow in data.FactorValue) {
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
                data.Solitary.Label = textBoxLabel.Text;
            } else {
                data.Solitary.SetLabelNull();
            }

            if (waterSelector.IsWaterSelected) {
                if (data.Solitary.WaterID == waterSelector.WaterObject.ID) { } else {

                    Survey.WaterRow newWaterRow = data.Water.NewWaterRow();
                    newWaterRow.ID = waterSelector.WaterObject.ID;
                    newWaterRow.Type = waterSelector.WaterObject.Type;
                    if (!waterSelector.WaterObject.IsWaterNull()) {
                        newWaterRow.Water = waterSelector.WaterObject.Water;
                    }
                    data.Water.AddWaterRow(newWaterRow);
                    data.Solitary.WaterRow = newWaterRow;
                }
                UserSettings.SelectedWaterID = waterSelector.WaterObject.ID;
            } else {
                UserSettings.SelectedWaterID = 0;
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

        private void saveEquipmentVirtues(Survey.EquipmentRow eqpRow) {

            Survey _data = (Survey)eqpRow.Table.DataSet;
            foreach (Survey.VirtueRow indexVirtueRow in SamplersIndex.Virtue) {

                if (tabPageSampler.Controls.Find("numeric" + indexVirtueRow.Name, true)?[0] is Mayfly.Controls.NumberBox numeric) {

                    if (!numeric.Enabled) continue;
                    if (numeric.IsSet) {
                        Survey.VirtueRow virtueRow = _data.Virtue.FindByName(indexVirtueRow.Name);
                        if (virtueRow == null) virtueRow = _data.Virtue.AddVirtueRow(indexVirtueRow.Name, indexVirtueRow.Notation);
                        _data.EquipmentVirtue.AddEquipmentVirtueRow(eqpRow, virtueRow, numeric.Value);
                    }
                }
            }

            if (equipmentSaved != null) {
                equipmentSaved.Invoke(this, new EquipmentEventArgs(eqpRow));
            }
        }

        protected void saveSampler() {

            data.SamplerVirtue.Clear();
            data.EquipmentVirtue.Clear();
            data.Virtue.Clear();
            data.Equipment.Clear();
            data.Sampler.Clear();

            if (SelectedSampler == null) {
                data.Solitary.SetEqpIDNull();
                SettingsReader.SelectedSampler = null;
                return;
            }

            Survey.SamplerRow sampleRow = SelectedSampler.CopyTo(data);
            data.Solitary.EquipmentRow = data.Equipment.AddEquipmentRow(sampleRow);

            saveEquipmentVirtues(data.Solitary.EquipmentRow);
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
            this.ResetText(filename, EntryAssemblyInfo.Title);
            menuItemAboutCard.Visible = true;

            if (Equipment.Equipment.FindDuplicate(data.Solitary.EquipmentRow) == null) {

                if (SelectedSampler != null) {

                    Survey.SamplerTypeRow samplerTypeRow = Equipment.SamplerType.FindByID(SelectedSampler.Type);

                    if (samplerTypeRow == null) {
                        samplerTypeRow = Equipment.SamplerType.AddSamplerTypeRow(SelectedSampler.Type, SelectedSampler.TypeName);
                    }

                    Survey.SamplerRow samplerRow = Equipment.Sampler.FindByID(SelectedSampler.ID);
                    if (samplerRow == null) {
                        samplerRow = SelectedSampler.CopyTo(Equipment);

                        //foreach (Survey.VirtueRow virtueRow in SelectedSampler.GetVirtueRows()) {

                        //    Survey.VirtueRow nvr = Equipment.Virtue.FindByName(virtueRow.Name);
                        //    if (nvr == null) {
                        //        nvr = Equipment.Virtue.AddVirtueRow(virtueRow.Name, virtueRow.Notation);
                        //    }

                        //    foreach (Survey.SamplerVirtueRow samplerVirtueRow in virtueRow.GetSamplerVirtueRows()) {

                        //        Survey.SamplerVirtueRow nsvr = Equipment.SamplerVirtue.FindBySmpIDVrtID(samplerRow.ID, nvr.ID);
                        //        if (nsvr == null) {
                        //            nsvr = Equipment.SamplerVirtue.NewSamplerVirtueRow();
                        //            nsvr.SamplerRow = samplerRow;
                        //            nsvr.VirtueRow = nvr;
                        //            if (!samplerVirtueRow.IsClassNull()) nsvr.Class = samplerVirtueRow.Class;
                        //            Equipment.SamplerVirtue.AddSamplerVirtueRow(nsvr);
                        //        }
                        //    }
                        //}
                    }
                    Survey.EquipmentRow eqpRow = Equipment.Equipment.AddEquipmentRow(samplerRow);
                    saveEquipmentVirtues(eqpRow);

                    if (!string.IsNullOrEmpty(eqpRow.VirtueDescription)) {
                        Service.SaveEquipment();
                        loadEquipment();
                    }
                }
            }

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

            IO.RunFile(UserSettings.WatersIndexPath);
        }

        private void menuItemSpecies_Click(object sender, EventArgs e) {

            IO.RunFile(TaxonomicIndexPath, "-edit");
        }

        private void menuItemSettings_Click(object sender, EventArgs e) {

            string currentWaters = UserSettings.WatersIndexPath;
            string currentSpc = TaxonomicIndexPath;
            int currentRecentCount = RecentSpeciesCount;

            if (Mayfly.UserSettings.Settings.ShowDialog(this) == DialogResult.OK) {

                if (currentWaters != UserSettings.WatersIndexPath) {

                    UserSettings.WatersIndex = null;
                    waterSelector.Index = UserSettings.WatersIndex;
                }

                if (currentSpc != TaxonomicIndexPath) {

                    TaxonomicIndex = null;
                    Logger.Provider.IndexPath = TaxonomicIndexPath;
                }

                if (currentSpc != TaxonomicIndexPath || currentRecentCount != RecentSpeciesCount) {

                    Logger.Provider.RecentListCount = RecentSpeciesCount;
                    Logger.UpdateRecent();
                }

                ColumnQty.ReadOnly = FixTotals;
                ColumnMass.ReadOnly = FixTotals;

                if (settingsApplied != null) settingsApplied.Invoke(this, EventArgs.Empty);
            }
        }

        private void menuItemAbout_Click(object sender, EventArgs e) {

            About about = new About(AppBanner);
            about.SetPowered(SupportLogo, SupportText);
            about.ShowDialog();
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

        private void comboBoxSampler_SelectedIndexChanged(object sender, EventArgs e) {

            if (SelectedSampler == null) return;

            foreach (Survey.VirtueRow virtueRow in SamplersIndex.Virtue) {

                Label lbl = tabPageSampler.Controls.Find("label" + virtueRow.Name, true)?[0] as Label;
                NumberBox nmb = tabPageSampler.Controls.Find("numeric" + virtueRow.Name, true)?[0] as NumberBox;

                lbl.Enabled = nmb.Enabled = SelectedSampler.GetVirtueRows().Contains(virtueRow);

                Survey.SamplerVirtueRow samplerVirtueRow = SamplersIndex.SamplerVirtue.FindBySmpIDVrtID(SelectedSampler.ID, virtueRow.ID);
                if (samplerVirtueRow == null || samplerVirtueRow.IsLabelNull()) {
                    ComponentResourceManager resources = new ComponentResourceManager(lbl.FindForm().GetType());
                    resources.ApplyResources(lbl, lbl.Name);
                } else {
                    lbl.Text = samplerVirtueRow.Label.GetLocalizedValue();
                }
            }

            Label lblPrt = tabPageSampler.Controls.Find("labelPortions", true)?[0] as Label;
            NumberBox nmbPrt = tabPageSampler.Controls.Find("numericPortions", true)?[0] as NumberBox;
            lblPrt.Enabled = nmbPrt.Enabled = !SelectedSampler.IsEffortTypeNull() && SelectedSampler.EffortType == (int)EffortType.Portion;

            Label lblExp = tabPageSampler.Controls.Find("labelExposure", true)?[0] as Label;
            NumberBox nmbExp = tabPageSampler.Controls.Find("numericExposure", true)?[0] as NumberBox;
            lblExp.Enabled = nmbExp.Enabled = !SelectedSampler.IsEffortTypeNull() && SelectedSampler.EffortType == (int)EffortType.Exposure;

            if (comboBoxSampler.ContainsFocus) {
                saveSampler();
            }
        }

        private void buttonEquipment_Click(object sender, EventArgs e) {

            contextEquipment.Show(buttonEquipment, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }
    }
}