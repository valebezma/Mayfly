using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Plankton
{
    public partial class Card : Form
    {
        private string filename;
        private double Volume // In cubic meters
        {
            get
            {
                if (textBoxVolume.Text.IsDoubleConvertible())
                {
                    return Convert.ToDouble(textBoxVolume.Text) / 1000d;
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
                    textBoxVolume.Text = string.Empty;
                }
                else
                {
                    textBoxVolume.Text = (value * 1000d).ToString("N2");

                    if (SelectedSampler != null && !SelectedSampler.IsEffortValueNull())
                    {
                        switch (SelectedSampler.GetSamplerType())
                        {
                            case PlanktonSamplerType.Bathometer:
                                // Equals sample volume in liters divided by gear volume (given in sq meters)
                                numericUpDownPortion.Value = (decimal)Math.Round(value / SelectedSampler.EffortValue, 0);
                                break;

                            case PlanktonSamplerType.Filter:
                                // Equals sample volume divided by square of gears ring (given in meters)
                                double s = Math.PI * (0.5 * SelectedSampler.EffortValue) * (0.5 * SelectedSampler.EffortValue);
                                numericUpDownPortion.Value = (decimal)Math.Round(value / s, 1);
                                break;
                        }
                    }
                }
            }
        }


        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(ReaderSettings.Interface.Extension), EntryAssemblyInfo.Title);
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        public Wild.Survey Data { get; set; }

        public bool IsChanged { get; set; }



        public Card()
        {
            InitializeComponent();

            Data = new Data(ReaderSettings.TaxonomicIndex, ReaderSettings.SamplersIndex);
            Logger.Data = Data;
            FileName = null;

            waterSelector.CreateList();
            waterSelector.Index = Wild.UserSettings.WatersIndex;

            if (Wild.UserSettings.WatersIndex != null && ReaderSettings.SelectedWaterID != 0)
            {
                WatersKey.WaterRow selectedWater = Wild.UserSettings.WatersIndex.Water.FindByID(
                    ReaderSettings.SelectedWaterID);

                if (selectedWater != null)
                {
                    waterSelector.WaterObject = selectedWater;
                }
            }
            else
            {
                SetCombos(WaterType.None);
            }

            comboBoxSampler.DataSource = ReaderSettings.SamplersIndex.Sampler.Select();
            comboBoxSampler.SelectedIndex = -1;

            if (ReaderSettings.SelectedSamplerID != 0)
            {
                SelectedSampler = Service.Sampler(ReaderSettings.SelectedSamplerID);
            }

            if (ReaderSettings.SelectedDate != null)
            {
                waypointControl1.Waypoint.TimeMark = ReaderSettings.SelectedDate;
            }

            ColumnSpecies.ValueType = typeof(string);
            ColumnQuantity.ValueType = typeof(int);
            ColumnSubsample.ValueType = typeof(double);
            ColumnMass.ValueType = typeof(double);

            Logger.Provider.RecentListCount = ReaderSettings.RecentSpeciesCount;
            Logger.Provider.IndexPath = ReaderSettings.TaxonomicIndexPath;

            ColumnQuantity.ReadOnly = ReaderSettings.FixTotals;
            ColumnMass.ReadOnly = ReaderSettings.FixTotals;

            tabPageEnvironment.Parent = null;

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);

            ToolStripMenuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = ReaderSettings.TaxonomicIndexPath != null;

            IsChanged = false;
        }



        private void Clear()
        {
            FileName = null;

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;

            waypointControl1.Clear();

            #region Sampler info

            comboBoxSampler.SelectedIndex = -1;
            numericUpDownPortion.Value = 1;
            textBoxMesh.Text = string.Empty;
            textBoxDepth.Text = string.Empty;

            #endregion

            comboBoxCrossSection.SelectedIndex = -1;
            comboBoxBank.SelectedIndex = -1;

            textBoxComments.Text = string.Empty;

            aquaControl1.Clear();
            weatherControl1.Clear();

            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();

            Data = new Data(ReaderSettings.SpeciesIndex, ReaderSettings.SamplersIndex);
        }

        private void Write(string filename)
        {
            if (ReaderSettings.SpeciesAutoExpand) // If it is set to automatically expand global index
            {
                Logger.Provider.UpdateIndex(Data.GetSpeciesKey(), ReaderSettings.SpeciesAutoExpandVisual);

                //    if (Logger.Provider.UpdateIndex(Data.GetSpeciesKey(), "Plankton (auto)",
                //    ReaderSettings.SpeciesAutoExpandVisual) == AutoExpandResult.Created)
                //{
                //    ReaderSettings.TaxonomicIndexPath = Logger.Provider.IndexPath;
                //}
            }

            Data.WriteToFile(filename);
            statusCard.Message(Wild.Resources.Interface.Messages.Saved);
            FileName = filename;
            IsChanged = false;
        }

        private void SaveData()
        {
            Wild.Survey.CardRow cardRow = SaveCardRow();

            #region Save environment data

            aquaControl1.Save();

            if (aquaControl1.IsStateAvailable)
            {
                if (aquaControl1.AquaState.IsPhysicalsAvailable) cardRow.Physicals = aquaControl1.AquaState.PhysicalsProtocol;
                else cardRow.SetPhysicalsNull();

                if (aquaControl1.AquaState.IsChemicalsAvailable) cardRow.Chemicals = aquaControl1.AquaState.ChemicalsProtocol;
                else cardRow.SetChemicalsNull();

                if (aquaControl1.AquaState.IsOrganolepticsAvailable) cardRow.Organoleptics = aquaControl1.AquaState.OrganolepticsProtocol;
                else cardRow.SetOrganolepticsNull();
            }
            else
            {
                cardRow.SetPhysicalsNull();
                cardRow.SetChemicalsNull();
                cardRow.SetOrganolepticsNull();
            }

            weatherControl1.Save();

            if (weatherControl1.IsWeatherAvailable)
            {
                cardRow.Weather = weatherControl1.Weather.Protocol;
            }

            #endregion

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

            Logger.SaveLog();

            Data.ClearUseless();
        }

        public Wild.Survey.CardRow SaveCardRow()
        {
            #region Header

            if (!waterSelector.IsWaterSelected)
            {
                ReaderSettings.SelectedWaterID = 0;
                Data.Solitary.SetWaterIDNull();
                goto WaterSkip;
            }
            else
            {
                if (Data.Solitary.IsWaterIDNull())
                {
                    goto WaterSave;
                }

                if (Data.Solitary.WaterID == waterSelector.WaterObject.ID)
                {
                    ReaderSettings.SelectedWaterID = waterSelector.WaterObject.ID;
                    goto WaterSkip;
                }

                goto WaterSave;
            }

        WaterSave:

            Wild.Survey.WaterRow newWaterRow = Data.Water.NewWaterRow();
            newWaterRow.ID = waterSelector.WaterObject.ID;
            newWaterRow.Type = waterSelector.WaterObject.Type;
            if (!waterSelector.WaterObject.IsWaterNull())
            {
                newWaterRow.Water = waterSelector.WaterObject.Water;
            }

            Data.Water.AddWaterRow(newWaterRow);
            Data.Solitary.WaterRow = newWaterRow;
            ReaderSettings.SelectedWaterID = waterSelector.WaterObject.ID;

        WaterSkip:

            if (textBoxLabel.Text.IsAcceptable())
            {
                Data.Solitary.Label = textBoxLabel.Text;
            }
            else
            {
                Data.Solitary.SetLabelNull();
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

            SaveSamplerValues();

            #region Location

            if (comboBoxCrossSection.SelectedIndex == -1)
            {
                Data.Solitary.SetCrossSectionNull();
            }
            else
            {
                Data.Solitary.CrossSection = comboBoxCrossSection.SelectedIndex;
            }

            if (!comboBoxBank.Enabled || comboBoxBank.SelectedIndex == -1)
            {
                Data.Solitary.SetBankNull();
            }
            else
            {
                Data.Solitary.Bank = comboBoxBank.SelectedIndex;
            }

            #endregion

            #region Comments

            textBoxComments.Text = textBoxComments.Text.Trim();

            if (textBoxComments.Text.IsAcceptable())
            {
                Data.Solitary.Comments = textBoxComments.Text;
            }
            else
            {
                Data.Solitary.SetCommentsNull();
            }

            #endregion

            return Data.Solitary;
        }

        private void SaveSamplerValues()
        {
            if (SelectedSampler == null)
            {
                Data.Solitary.SetSamplerNull();
                ReaderSettings.SelectedSamplerID = 0;
            }
            else
            {
                Data.Solitary.Sampler = ReaderSettings.SelectedSamplerID = SelectedSampler.ID;
            }

            if (double.IsNaN(Volume))
            {
                Data.Solitary.SetVolumeNull();
            }
            else
            {
                Data.Solitary.Volume = Volume;
            }

            if (textBoxMesh.Text.IsDoubleConvertible())
            {
                Data.Solitary.Mesh = (int)double.Parse(textBoxMesh.Text);
            }
            else
            {
                Data.Solitary.SetMeshNull();
            }

            if (textBoxDepth.Text.IsDoubleConvertible())
            {
                Data.Solitary.Depth = double.Parse(textBoxDepth.Text);
            }
            else
            {
                Data.Solitary.SetDepthNull();
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
                statusCard.Message(Wild.Resources.Interface.Messages.FactorNameRequired);
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

        public void LoadData(string filename)
        {
            Clear();
            Data = new Data(ReaderSettings.SpeciesIndex, ReaderSettings.SamplersIndex);
            Data.Read(filename);
            LoadData();
            FileName = filename;

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

            if (Data.Solitary.IsWaterIDNull())
            {
                waterSelector.WaterObject = null;
            }
            else
            {
                WatersKey.WaterRow waterRow = null;

                if (Wild.UserSettings.WatersIndex != null)
                {
                    waterRow = Wild.UserSettings.WatersIndex.Water.FindByID(Data.Solitary.WaterID);
                }

                if (waterRow == null)
                {
                    waterRow = waterSelector.Index.Water.NewWaterRow();
                    waterRow.ID = Data.Solitary.WaterRow.ID;
                    if (!Data.Solitary.WaterRow.IsWaterNull()) waterRow.Water = Data.Solitary.WaterRow.Water;
                    waterRow.Type = Data.Solitary.WaterRow.Type;
                }

                waterSelector.WaterObject = waterRow;
            }

            #endregion

            waypointControl1.Waypoint = Data.Solitary.Position;
            waypointControl1.UpdateValues();

            #region Sampling

            if (Data.Solitary.IsSamplerNull())
            {
                comboBoxSampler.SelectedIndex = -1;
            }
            else
            {
                SelectedSampler = Service.Sampler(Data.Solitary.Sampler);
            }

            if (Data.Solitary.IsVolumeNull())
            {
                Volume = double.NaN;
            }
            else
            {
                Volume = Data.Solitary.Volume;
            }

            if (Data.Solitary.IsMeshNull())
            {
                textBoxMesh.Text = string.Empty;
            }
            else
            {
                textBoxMesh.Text = Data.Solitary.Mesh.ToString();
            }

            if (Data.Solitary.IsDepthNull())
            {
                textBoxDepth.Text = string.Empty;
            }
            else
            {
                textBoxDepth.Text = Data.Solitary.Depth.ToString();
            }

            #endregion

            #region Location

            if (Data.Solitary.IsCrossSectionNull())
            {
                comboBoxCrossSection.SelectedIndex = -1;
            }
            else
            {
                comboBoxCrossSection.SelectedIndex = Data.Solitary.CrossSection;
            }

            if (Data.Solitary.IsBankNull())
            {
                comboBoxBank.SelectedIndex = -1;
            }
            else
            {
                comboBoxBank.SelectedIndex = Data.Solitary.Bank;
            }

            #endregion

            if (Data.Solitary.IsCommentsNull())
            {
                textBoxComments.Text = string.Empty;
            }
            else
            {
                textBoxComments.Text = Data.Solitary.Comments;
            }

            if (Data.Solitary.IsEnvironmentDescribed)
            {
                tabPageEnvironment.Parent = tabControl;

                if (Data.Solitary.StateOfWater == null)
                {
                    aquaControl1.Clear();
                }
                else
                {
                    aquaControl1.AquaState = Data.Solitary.StateOfWater;
                    aquaControl1.UpdateValues();
                }

                if (Data.Solitary.IsWeatherNull())
                {
                    weatherControl1.Clear();
                }
                else
                {
                    weatherControl1.Weather = Data.Solitary.WeatherConditions;
                    weatherControl1.UpdateValues();
                }
            }
            else
            {
                tabPageEnvironment.Parent = null;
            }

            spreadSheetLog.Rows.Clear();
            Logger.InsertLogRows(Data, 0);

            #region Factors

            spreadSheetAddt.Rows.Clear();
            if (Data.Factor.Count > 0)
            {
                tabPageFactors.Parent = tabControl;
                foreach (Wild.Survey.FactorValueRow factorValueRow in Data.FactorValue)
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

            Logger.UpdateStatus();

            IsChanged = false;
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



        private void SetVolume()
        {
            if (SelectedSampler != null && !SelectedSampler.IsEffortValueNull())
            {
                switch (SelectedSampler.GetSamplerType())
                {
                    case PlanktonSamplerType.Bathometer:
                        // Effort is volume in liters, portion means number of portions
                        Volume = (double)numericUpDownPortion.Value * SelectedSampler.EffortValue;
                        break;

                    case PlanktonSamplerType.Filter:
                        // Effort is diameter of filtering ring, portion means exposure in meters
                        double h = (double)numericUpDownPortion.Value;
                        double s = Math.PI * (0.5 * SelectedSampler.EffortValue) * (0.5 * SelectedSampler.EffortValue);
                        Volume = h * s;
                        break;
                }
            }
            else
            {
                Volume = double.NaN;
            }
        }

        private void SetCombos(WaterType type)
        {
            comboBoxCrossSection.Items.Clear();
            comboBoxCrossSection.Items.AddRange(Wild.Service.CrossSection(type));
            comboBoxBank.Enabled = type != WaterType.Lake;
        }

        private void SetSubsample(double subsample)
        {
            spreadSheetLog.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                gridRow.Cells[ColumnSubsample.Index].Value = subsample;
            }
        }



        private void Card_Load(object sender, EventArgs e)
        {
            IsChanged = false;
            Logger.UpdateStatus();
        }

        private void CardOpenSpecies_Load(object sender, EventArgs e)
        {

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
            if (ReaderSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (ReaderSettings.Interface.OpenDialog.FileName == FileName)
                {
                    statusCard.Message(Wild.Resources.Interface.Messages.AlreadyOpened);
                }
                else
                {
                    if (CheckAndSave() != DialogResult.Cancel)
                    {
                        LoadData(ReaderSettings.Interface.OpenDialog.FileName);
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
                SaveData();
                Write(FileName);
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            SaveData();

            ReaderSettings.Interface.SaveDialog.FileName =
                IO.SuggestName(IO.FolderName(ReaderSettings.Interface.SaveDialog.FileName),
                Data.Solitary.GetSuggestedName());

            if (ReaderSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Write(ReaderSettings.Interface.SaveDialog.FileName);
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

        private void addEnvironmentalDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPageEnvironment.Parent = tabControl;
            tabControl.SelectedTab = tabPageEnvironment;
        }

        private void addFactorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPageFactors.Parent = tabControl;
            tabControl.SelectedTab = tabPageFactors;
        }

        private void menuItemLocation_Click(object sender, EventArgs e)
        {
            if (IO.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                waypointControl1.SelectGPS(IO.InterfaceLocation.OpenDialog.FileNames);
            }
        }

        #endregion

        #region Service menu

        private void ToolStripMenuItemWatersRef_Click(object sender, EventArgs e)
        {
            IO.RunFile(Wild.UserSettings.WatersIndexPath);
        }

        private void ToolStripMenuItemSpeciesRef_Click(object sender, EventArgs e)
        {
            IO.RunFile(ReaderSettings.TaxonomicIndexPath);
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            string currentWaters = Wild.UserSettings.WatersIndexPath;
            string currentSpc = ReaderSettings.TaxonomicIndexPath;
            int currentRecentCount = ReaderSettings.RecentSpeciesCount;

            Settings settings = new Settings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                if (currentWaters != Wild.UserSettings.WatersIndexPath)
                {
                    Wild.UserSettings.WatersIndex = null;
                    waterSelector.Index = Wild.UserSettings.WatersIndex;
                }

                if (currentSpc != ReaderSettings.TaxonomicIndexPath)
                {
                    ReaderSettings.SpeciesIndex = null;
                    Logger.Provider.IndexPath = ReaderSettings.TaxonomicIndexPath;
                }

                if (currentSpc != ReaderSettings.TaxonomicIndexPath ||
                    currentRecentCount != ReaderSettings.RecentSpeciesCount)
                {
                    Logger.Provider.RecentListCount = ReaderSettings.RecentSpeciesCount;
                    Logger.UpdateRecent();
                }
            }

            ColumnQuantity.ReadOnly = ReaderSettings.FixTotals;
            ColumnMass.ReadOnly = ReaderSettings.FixTotals;
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
            SetCombos(waterSelector.IsWaterSelected ? waterSelector.WaterObject.WaterType : WaterType.None);
            statusCard.Message(Wild.Resources.Interface.Messages.WaterSet);
        }

        private void sampler_Changed(object sender, EventArgs e)
        {
            PlanktonSamplerType type = SelectedSampler.GetSamplerType();

            textBoxVolume.ReadOnly =
                labelPortion.Visible = numericUpDownPortion.Visible =
                //labelVolume.Visible = 
                type != PlanktonSamplerType.Manual;

            switch (type)
            {
                case PlanktonSamplerType.Bathometer:
                    numericUpDownPortion.DecimalPlaces = 0;
                    labelPortion.Text = Resources.Interface.Portion;
                    break;

                case PlanktonSamplerType.Filter:
                    numericUpDownPortion.DecimalPlaces = 2;
                    labelPortion.Text = Resources.Interface.Expanse;
                    break;
            }

            SetVolume();
            IsChanged = true;
        }

        private void numericUpDownReps_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPortion.ContainsFocus)
            {
                SetVolume();
            }
        }

        private void numericUpDownReps_VisibleChanged(object sender, EventArgs e)
        {
            if (!numericUpDownPortion.Visible)
            {
                //numericUpDownReps.Value = 0;
                textBoxVolume.Text = string.Empty;
            }
        }

        private void textBoxVolume_TextChanged(object sender, EventArgs e)
        {
            value_Changed(sender, e);
            Logger.UpdateStatus();
        }

        private void comboBoxCrossSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCrossSection.SelectedIndex == -1 ||
                comboBoxCrossSection.SelectedIndex == comboBoxCrossSection.Items.Count - 1)
            {
                labelBank.Enabled = comboBoxBank.Enabled = false;
            }
            else
            {
                labelBank.Enabled = comboBoxBank.Enabled = true;
            }

            value_Changed(sender, e);
        }

        private void pictureBoxWarnOpening_MouseHover(object sender, EventArgs e)
        {
            if (comboBoxCrossSection.SelectedIndex == -1 ||
                comboBoxCrossSection.SelectedIndex == comboBoxCrossSection.Items.Count - 1)
            {
                labelBank.Enabled = comboBoxBank.Enabled = false;
            }
            else
            {
                labelBank.Enabled = comboBoxBank.Enabled = true;
            }

            value_Changed(sender, e);
        }
                


        private void logger_IndividualsRequired(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.Cells[ColumnSpecies.Name].Value != null)
                {
                    Individuals individuals = new Individuals(Logger.SaveLogRow(gridRow));
                    individuals.SetColumns(ColumnSpecies, ColumnQuantity, ColumnMass);
                    individuals.LogLine = gridRow;
                    individuals.SetFriendlyDesktopLocation(gridRow);
                    individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
                    individuals.Show(this);
                }
            }
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e)
        {
            Individuals individuals = sender as Individuals;
            if (individuals.DialogResult == DialogResult.OK)
            {
                IsChanged |= individuals.ChangesWereMade;
            }
        }



        private void spreadSheetAddt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
            HandleFactorRow(spreadSheetAddt.Rows[e.RowIndex]);
        }
    }
}