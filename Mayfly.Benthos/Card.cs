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
using static Mayfly.Benthos.UserSettings;

namespace Mayfly.Benthos
{
    public partial class Card : Form
    {
        private string filename;
        private double Square // In square meters
        {
            get
            {
                if (textBoxSquare.Text.IsDoubleConvertible())
                {
                    return Convert.ToDouble(textBoxSquare.Text);
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
                    textBoxSquare.Text = string.Empty;
                }
                else
                {
                    textBoxSquare.Text = value.ToString();//Textual.Mask(4));

                    if (SelectedSampler != null && !SelectedSampler.IsEffortValueNull())
                    {

                        switch (SelectedSampler.GetSamplerType())
                        {
                            case BenthosSamplerType.Grabber:
                                // Replies count equals to 
                                // square divided by [standard effort] of grabber
                                numericUpDownReps.Value = (decimal)Math.Round(value / SelectedSampler.EffortValue, 0);
                                break;
                            case BenthosSamplerType.Scraper:
                                // Exposure length in centimeters equals to 
                                // square divided by knife length (standard effort) of creeper multiplied by 100
                                numericUpDownReps.Value = 100 * (decimal)Math.Round(value / SelectedSampler.EffortValue, 3);
                                break;
                            default:
                                numericUpDownReps.Value = (decimal)Math.Round(value / SelectedSampler.EffortValue, 0);
                                break;
                        }
                    }
                    else
                    {
                        statusCard.Message(Resources.Interface.Messages.SamplerUnable);
                    }
                }
            }
        }
        private NumericUpDown[] MineralControls => new NumericUpDown[] { numericUpDownBoulder, numericUpDownCobble, numericUpDownGravel, numericUpDownSand, numericUpDownSilt, numericUpDownClay };


        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(ReaderSettings.Interface.Extension), EntryAssemblyInfo.Title);
                menuItemAboutCard.Visible = value != null;
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        public Data Data { get; set; }

        public Samplers.SamplerRow SelectedSampler
        {
            get
            {
                return comboBoxSampler.SelectedItem as Samplers.SamplerRow;
            }

            set
            {
                comboBoxSampler.SelectedItem = value;
            }
        }

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

            if (ReaderSettings.SelectedSampler != null) {
                SelectedSampler = ReaderSettings.SelectedSampler;
            }

            if (ReaderSettings.SelectedDate != null) {
                waypointControl1.Waypoint.TimeMark = ReaderSettings.SelectedDate;
            }

            Logger.Provider.RecentListCount = ReaderSettings.RecentSpeciesCount;
            Logger.Provider.IndexPath = ReaderSettings.TaxonomicIndexPath;

            ColumnQuantity.ReadOnly = ReaderSettings.FixTotals;
            ColumnMass.ReadOnly = ReaderSettings.FixTotals;

            tabPageEnvironment.Parent = null;

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);

            MenuStrip.SetMenuIcons();

            ToolStripMenuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = ReaderSettings.TaxonomicIndexPath != null;

            Logger.UpdateStatus();

            IsChanged = false;
        }



        private void Clear()
        {
            FileName = null;

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;

            waypointControl1.Clear();

            #region Sampler info

            //comboBoxSampler.SelectedIndex = -1;
            //numericUpDownReps.Value = 1;
            //textBoxMesh.Text = string.Empty;
            textBoxDepth.Text = string.Empty;

			#endregion

            comboBoxCrossSection.SelectedIndex = -1;
            comboBoxBank.SelectedIndex = -1;

            textBoxComments.Text = string.Empty;

            #region Substrate

            checkBoxBoulder.Checked = false;
            checkBoxCobble.Checked = false;
            checkBoxGravel.Checked = false;
            checkBoxSand.Checked = false;
            checkBoxSilt.Checked = false;
            checkBoxClay.Checked = false;

            numericUpDownBoulder.Value = 0;
            numericUpDownCobble.Value = 0;
            numericUpDownGravel.Value = 0;
            numericUpDownSand.Value = 0;
            numericUpDownSilt.Value = 0;
            numericUpDownClay.Value = 0;

            checkBoxPhytal.Checked = false;
            checkBoxPhytal.Checked = false;
            checkBoxWood.Checked = false;
            checkBoxCPOM.Checked = false;
            checkBoxFPOM.Checked = false;
            checkBoxSapropel.Checked = false;

            numericUpDownPhytal.Value = 0;
            numericUpDownPhytal.Value = 0;
            numericUpDownWood.Value = 0;
            numericUpDownCPOM.Value = 0;
            numericUpDownFPOM.Value = 0;
            numericUpDownSapropel.Value = 0;

            #endregion

            aquaControl1.Clear();
            weatherControl1.Clear();

            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();

            Data = new Data(ReaderSettings.TaxonomicIndex, ReaderSettings.SamplersIndex);
        }

        private void Write(string filename)
        {
            if (ReaderSettings.SpeciesAutoExpand) // If it is set to automatically expand global index
            {
                Logger.Provider.UpdateIndex(Data.GetSpeciesKey(), ReaderSettings.SpeciesAutoExpandVisual);
            }

            switch (Path.GetExtension(filename))
            {
                case ".bcd":
                    Data.WriteToFile(filename);
                    break;

                case ".html":
                    Data.GetReport().WriteToFile(filename);
                    break;
            }

            statusCard.Message(Wild.Resources.Interface.Messages.Saved);
            FileName = filename;
            IsChanged = false;
        }

        private void SaveData()
        {
            Data.CardRow cardRow = SaveCardRow();

            #region Save environment data

            aquaControl1.Save();

            if (aquaControl1.AquaState != null)
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

            if (weatherControl1.Weather.IsAvailable) { cardRow.Weather = weatherControl1.Weather.Protocol; }
            else { cardRow.SetWeatherNull(); }

            #endregion

            #region Save substrate values

            SubstrateSample substrate = new SubstrateSample(0, 0, 0);

            #region Mineral Substrates

            if (checkBoxBoulder.Checked)
            {
                substrate.Boulder = (int)numericUpDownBoulder.Value;                
            }

            if (checkBoxCobble.Checked)
            {
                substrate.Cobble = (int)numericUpDownCobble.Value;                
            }

            if (checkBoxGravel.Checked)
            {
                substrate.Gravel = (int)numericUpDownGravel.Value;                
            }

            if (checkBoxSand.Checked)
            {
                substrate.Sand = (int)numericUpDownSand.Value;                
            }

            if (checkBoxSilt.Checked)
            {
                substrate.Silt = (int)numericUpDownSilt.Value;                
            }

            if (checkBoxClay.Checked)
            {
                substrate.Clay = (int)numericUpDownClay.Value;                
            }

            #endregion

            #region Organic Substrates

            if (checkBoxPhytal.Checked)
            {
                substrate.Phytal = (int)numericUpDownPhytal.Value;                
            }

            if (checkBoxLiving.Checked)
            {
                substrate.Living = (int)numericUpDownLiving.Value;                
            }

            if (checkBoxWood.Checked)
            {
                substrate.Wood = (int)numericUpDownWood.Value;                
            }

            if (checkBoxCPOM.Checked)
            {
                substrate.CPOM = (int)numericUpDownCPOM.Value;                
            }

            if (checkBoxFPOM.Checked)
            {
                substrate.FPOM = (int)numericUpDownFPOM.Value;                
            }

            if (checkBoxSapropel.Checked)
            {
                substrate.Sapropel = (int)numericUpDownSapropel.Value;                
            }

            if (checkBoxDebris.Checked)
            {
                substrate.Debris = (int)numericUpDownDebris.Value;                
            }

            #endregion

            if (substrate.IsAvailable)
            { 
                cardRow.Substrate = substrate.Protocol;
            }
            else
            {
                cardRow.SetSubstrateNull();
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

        public Data.CardRow SaveCardRow()
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

            Data.WaterRow newWaterRow = Data.Water.NewWaterRow();
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
                ReaderSettings.SelectedSampler = null;
            }
            else
            {
                Data.Solitary.Sampler = SelectedSampler.ID;
                ReaderSettings.SelectedSampler = SelectedSampler;
            }

            if (double.IsNaN(Square))
            {
                Data.Solitary.SetSquareNull();
            }
            else
            {
                Data.Solitary.Square = Square;
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
            Data = new Data(ReaderSettings.TaxonomicIndex, ReaderSettings.SamplersIndex);
            Data.Read(filename);
            LoadData();
            FileName = filename;
            Log.Write("Loaded from {0}.", filename);
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
                SelectedSampler = Data.Solitary.SamplerRow;
            }

            if (Data.Solitary.IsSquareNull())
            {
                Square = double.NaN;
            }
            else
            {
                Square = Data.Solitary.Square;
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

            if (!Data.Solitary.IsSubstrateNull())
            {
                #region Mineral Substrates

                SubstrateSample sub = Data.Solitary.GetSubstrate();

                if (sub.IsBoulderNull())
                {
                    checkBoxBoulder.Checked = false;
                }
                else
                {
                    checkBoxBoulder.Checked = true;
                    numericUpDownBoulder.Value = (decimal)sub.Boulder;
                }

                if (sub.IsCobbleNull())
                {
                    checkBoxCobble.Checked = false;
                }
                else
                {
                    checkBoxCobble.Checked = true;
                    numericUpDownCobble.Value = (decimal)sub.Cobble;
                }

                if (sub.IsGravelNull())
                {
                    checkBoxGravel.Checked = false;
                }
                else
                {
                    checkBoxGravel.Checked = true;
                    numericUpDownGravel.Value = (decimal)sub.Gravel;
                }

                if (sub.IsSandNull())
                {
                    checkBoxSand.Checked = false;
                }
                else
                {
                    checkBoxSand.Checked = true;
                    numericUpDownSand.Value = (decimal)sub.Sand;
                }

                if (sub.IsSiltNull())
                {
                    checkBoxSilt.Checked = false;
                }
                else
                {
                    checkBoxSilt.Checked = true;
                    numericUpDownSilt.Value = (decimal)sub.Silt;
                }

                if (sub.IsClayNull())
                {
                    checkBoxClay.Checked = false;
                }
                else
                {
                    checkBoxClay.Checked = true;
                    numericUpDownClay.Value = (decimal)sub.Clay;
                }

                #endregion

                #region Organic Substrates

                if (sub.IsPhytalNull())
                {
                    checkBoxPhytal.Checked = false;
                }
                else
                {
                    numericUpDownPhytal.Value = (decimal)sub.Phytal;
                    checkBoxPhytal.Checked = true;
                }

                if (sub.IsLivingNull())
                {
                    checkBoxLiving.Checked = false;
                }
                else
                {
                    numericUpDownLiving.Value = (decimal)sub.Living;
                    checkBoxLiving.Checked = true;
                }

                if (sub.IsWoodNull())
                {
                    checkBoxWood.Checked = false;
                }
                else
                {
                    numericUpDownWood.Value = (decimal)sub.Wood;
                    checkBoxWood.Checked = true;
                }

                if (sub.IsCPOMNull())
                {
                    checkBoxCPOM.Checked = false;
                }
                else
                {
                    numericUpDownCPOM.Value = (decimal)sub.CPOM;
                    checkBoxCPOM.Checked = true;
                }

                if (sub.IsFPOMNull())
                {
                    checkBoxFPOM.Checked = false;
                }
                else
                {
                    numericUpDownFPOM.Value = (decimal)sub.FPOM;
                    checkBoxFPOM.Checked = true;
                }

                if (sub.IsSapropelNull())
                {
                    checkBoxSapropel.Checked = false;
                }
                else
                {
                    numericUpDownSapropel.Value = (decimal)sub.Sapropel;
                    checkBoxSapropel.Checked = true;
                }

                if (sub.IsDebrisNull())
                {
                    checkBoxDebris.Checked = false;
                }
                else
                {
                    numericUpDownDebris.Value = (decimal)sub.Debris;
                    checkBoxDebris.Checked = true;
                }

                #endregion
            }


            spreadSheetLog.Rows.Clear();
            Logger.InsertLogRows(Data, 0);

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
                    menuItemSave_Click(menuItemSave, new EventArgs());
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



        private void SetSquare()
        {
            if (SelectedSampler != null && !SelectedSampler.IsEffortValueNull())
            {
                switch (SelectedSampler.GetSamplerType())
                {
                    case BenthosSamplerType.Grabber:
                        Square = (double)numericUpDownReps.Value * SelectedSampler.EffortValue;
                        break;
                    case BenthosSamplerType.Scraper:
                        Square = (double)numericUpDownReps.Value * SelectedSampler.EffortValue / 100;
                        break;
                }
            }
            else
            {
                //Square = double.NaN;
            }
        }

        private void SetCombos(WaterType type)
        {
            comboBoxCrossSection.Items.Clear();
            comboBoxCrossSection.Items.AddRange(Wild.Service.CrossSection(type));
            comboBoxBank.Enabled = type != WaterType.Lake;
        }

        private void DropSubs()
        {
            numericUpDownBoulder.Value = 0;
            numericUpDownCobble.Value = 0;
            numericUpDownGravel.Value = 0;
            numericUpDownSand.Value = 0;
            numericUpDownSilt.Value = 0;
            numericUpDownClay.Value = 0;

            numericUpDownPhytal.Value = 0;
            numericUpDownLiving.Value = 0;
            numericUpDownWood.Value = 0;
            numericUpDownCPOM.Value = 0;
            numericUpDownFPOM.Value = 0;
            numericUpDownSapropel.Value = 0;
            numericUpDownDebris.Value = 0;
        }

        private void ResetSubs()
        {
            checkBoxBoulder.Checked = (numericUpDownBoulder.Value > 0);
            checkBoxCobble.Checked = (numericUpDownCobble.Value > 0);
            checkBoxGravel.Checked = (numericUpDownGravel.Value > 0);
            checkBoxSand.Checked = (numericUpDownSand.Value > 0);
            checkBoxSilt.Checked = (numericUpDownSilt.Value > 0);
            checkBoxClay.Checked = (numericUpDownClay.Value > 0);

            checkBoxPhytal.Checked = (numericUpDownPhytal.Value > 0);
            checkBoxLiving.Checked = (numericUpDownLiving.Value > 0);
            checkBoxWood.Checked = (numericUpDownWood.Value > 0);
            checkBoxCPOM.Checked = (numericUpDownCPOM.Value > 0);
            checkBoxFPOM.Checked = (numericUpDownFPOM.Value > 0);
            checkBoxSapropel.Checked = (numericUpDownSapropel.Value > 0);
            checkBoxDebris.Checked = (numericUpDownDebris.Value > 0);
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



        private void Card_Load(object sender, EventArgs e)
        {
            IsChanged = false;
            Logger.UpdateStatus();
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
                tabControl.SelectedTab = tabPageCollect;
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

            ReaderSettings.Interface.ExportDialog.FileName =
                IO.SuggestName(IO.FolderName(ReaderSettings.Interface.SaveDialog.FileName),
                Data.Solitary.GetSuggestedName());

            if (ReaderSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK)
            {
                Write(ReaderSettings.Interface.ExportDialog.FileName);
            }
        }

        private void menuItemPrintPreview_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                SaveData();
            }

            Data.GetReport().Preview();
        }

        private void menuItemPrint_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                SaveData();
            }

            Data.GetReport().Print();
        }

        private void menuItemCardBlank_Click(object sender, EventArgs e)
        {
            BenthosReport.BlankCard.Run();
        }

        private void menuItemIndividualsLogBlank_Click(object sender, EventArgs e)
        {
            BenthosReport.BlankIndividualsLog.Run();
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

        private void menuItemWaters_Click(object sender, EventArgs e)
        {
            IO.RunFile(Wild.UserSettings.WatersIndexPath);
        }

        private void menuItemSpecies_Click(object sender, EventArgs e)
        {
            IO.RunFile(ReaderSettings.TaxonomicIndexPath, "-edit");
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
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
                    ReaderSettings.TaxonomicIndex = null;
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
            about.SetPowered(Properties.Resources.ibiw, Wild.Resources.Interface.Powered.IBIW);
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }

        private void itemAboutCard_Click(object sender, EventArgs e)
        {
            AboutData about = new AboutData(Data.Solitary.Investigator);
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
            BenthosSamplerType kind = SelectedSampler.GetSamplerType();

            textBoxSquare.ReadOnly = labelRepeats.Visible =
                numericUpDownReps.Visible = kind != BenthosSamplerType.Manual;

            switch (kind)
            {
                case BenthosSamplerType.Grabber:
                    labelRepeats.Text = Resources.Interface.Interface.Repeats;
                    break;
                case BenthosSamplerType.Scraper:
                    labelRepeats.Text = Resources.Interface.Interface.Expanse;
                    break;
            }

            SetSquare();
            IsChanged = true;
        }


        private void numericUpDownReps_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownReps.ContainsFocus)
            {
                SetSquare();
            }
        }

        private void numericUpDownReps_VisibleChanged(object sender, EventArgs e)
        {
            if (!numericUpDownReps.Visible)
            {
                //numericUpDownReps.Value = 0;
                textBoxSquare.Text = string.Empty;
            }
        }

        private void textBoxSquare_TextChanged(object sender, EventArgs e)
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



        private void substrate_ValueChanged(object sender, EventArgs e)
        {
            value_Changed(sender, e);

            if (sender is NumericUpDown down && down.ContainsFocus)
            {
                int selectedValue = (int)down.Value;

                int rest = 100 - selectedValue;

                int currentRest = 0;

                foreach (NumericUpDown numericUpDown in MineralControls)
                {
                    if (numericUpDown == down) continue;
                    currentRest += (int)numericUpDown.Value;
                }

                if (currentRest > rest)
                {
                    foreach (NumericUpDown numericUpDown in MineralControls)
                    {
                        if (numericUpDown == down) continue;
                        if (numericUpDown.Value == 0) continue;
                        numericUpDown.Value = (int)(numericUpDown.Value * (decimal)rest / (decimal)currentRest);
                    }
                }
            }

            // TODO: get substrate from sand+silt+clay IF checks are checked

            SubstrateSample substrate = new SubstrateSample(
                (double)numericUpDownSand.Value,
                (double)numericUpDownSilt.Value,
                (double)numericUpDownClay.Value);

            labelTexture.Text = substrate.TypeName;
        }

        private void checkBoxGranulae_CheckedChanged(object sender, EventArgs e)
        {
            Control control = Controls.Find("numericUpDown" +
                ((CheckBox)sender).Name.Substring(8), true)[0];

            if (control is NumericUpDown)
            {
                control.Visible = ((CheckBox)sender).Checked;
            }

            substrate_ValueChanged(sender, e);
        }

        private void buttonSubTexture_Click(object sender, EventArgs e)
        {
            contextMenuStripSubstrate.Show(buttonSubTexture, new Point(buttonSubTexture.Width, 0), ToolStripDropDownDirection.Right);
        }

        private void ToolStripMenuItemTexture_Click(object sender, EventArgs e)
        {
            DropSubs();

            string texture = ((ToolStripMenuItem)sender).Name.Substring(7);
            SubstrateSample substrate = new SubstrateSample(SubstrateSample.FromName(texture));

            numericUpDownSand.Value = 100 * (decimal)substrate.Sand;
            numericUpDownSilt.Value = 100 * (decimal)substrate.Silt;
            numericUpDownClay.Value = 100 * (decimal)substrate.Clay;

            ResetSubs();
        }



        private void logger_IndividualsRequired(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                if (gridRow.Cells[ColumnSpecies.Name].Value != null) {
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
            if (individuals.DialogResult == DialogResult.OK) {
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