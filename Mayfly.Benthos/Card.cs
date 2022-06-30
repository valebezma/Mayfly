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

namespace Mayfly.Benthos
{
    public partial class Card : Form
    {
        #region Properties

        private string filename;

        private SpeciesKey.TaxonRow SpeciesToOpen;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
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

        //public string SelectedSamplerType
        //{
        //    get
        //    {
        //        if (SelectedSampler == null || SelectedSampler.IsKindNull())
        //        {
        //            return string.Empty;
        //        }
        //        else
        //        {
        //            return SelectedSampler.Kind;
        //        }
        //    }
        //}

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
                    if (gridRow.Cells[ColumnMass.Name].Value is double @double)
                    {
                        result += @double;
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
                    if (gridRow.Cells[ColumnQuantity.Name].Value is int @int)
                    {
                        result += @int;
                    }
                }
                return result;
            }
        }

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

        #endregion



        public Card()
        {
            InitializeComponent();

            Data = new Data(UserSettings.SpeciesIndex, UserSettings.SamplersIndex);
            FileName = null;

            waterSelector.CreateList();
            waterSelector.Index = Wild.UserSettings.WatersIndex;

            if (Wild.UserSettings.WatersIndex != null && UserSettings.SelectedWaterID != 0)
            {
                WatersKey.WaterRow selectedWater = Wild.UserSettings.WatersIndex.Water.FindByID(
                    UserSettings.SelectedWaterID);

                if (selectedWater != null)
                {
                    waterSelector.WaterObject = selectedWater;
                }
            }
            else
            {
                SetCombos(WaterType.None);
            }

            comboBoxSampler.DataSource = UserSettings.SamplersIndex.Sampler.Select();
            comboBoxSampler.SelectedIndex = -1;

            if (UserSettings.SelectedSamplerID != 0)
            {
                SelectedSampler = Service.Sampler(UserSettings.SelectedSamplerID);
            }

            if (UserSettings.SelectedDate != null)
            {
                waypointControl1.Waypoint.TimeMark = UserSettings.SelectedDate;
            }

            ColumnSpecies.ValueType = typeof(string);
            ColumnQuantity.ValueType = typeof(int);
            ColumnMass.ValueType = typeof(double);

            speciesLogger.RecentListCount = UserSettings.RecentSpeciesCount;
            speciesLogger.IndexPath = UserSettings.SpeciesIndexPath;

            ColumnQuantity.ReadOnly = UserSettings.FixTotals;
            ColumnMass.ReadOnly = UserSettings.FixTotals;

            tabPageEnvironment.Parent = null;

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);

            MenuStrip.SetMenuIcons();

            ToolStripMenuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = UserSettings.SpeciesIndexPath != null;

            IsChanged = false;
        }



        #region Methods

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

            Data = new Data(UserSettings.SpeciesIndex, UserSettings.SamplersIndex);
        }

        private void Clear(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnID.Index].Value != null)
            {
                Data.LogRow logRow = Data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);

                if (logRow != null)
                {
                    Data.SpeciesRow spcRow = logRow.SpeciesRow;
                    logRow.Delete();

                    if (spcRow.GetLogRows().Length == 0)
                    {
                        spcRow.Delete();
                    }
                }
            }
        }

        public void UpdateStatus()
        {
            statusCard.Default = StatusLog.Text =
                SpeciesCount.ToString(Mayfly.Wild.Resources.Interface.Interface.SpeciesCount);
            StatusMass.ResetFormatted(Mass);
            StatusCount.ResetFormatted(Quantity);
        }

        private void Write(string filename)
        {
            if (UserSettings.SpeciesAutoExpand) // If it is set to automatically expand global index
            {
                speciesLogger.UpdateIndex(Data.GetSpeciesKey(), UserSettings.SpeciesAutoExpandVisual);
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

            SaveLog();

            Data.ClearUseless();
        }

        public Data.CardRow SaveCardRow()
        {
            #region Header

            if (!waterSelector.IsWaterSelected)
            {
                UserSettings.SelectedWaterID = 0;
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
                    UserSettings.SelectedWaterID = waterSelector.WaterObject.ID;
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
            UserSettings.SelectedWaterID = waterSelector.WaterObject.ID;

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
                UserSettings.SelectedSamplerID = 0;
            }
            else
            {
                Data.Solitary.Sampler = UserSettings.SelectedSamplerID = SelectedSampler.ID;
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

                if (object.Equals(gridRow.Cells[ColumnSpecies.Index].Value, 
                    currentGridRow.Cells[ColumnSpecies.Index].Value))
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
                            newIndividualRow.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
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

                    foreach (Data.IndividualRow individualRow in 
                        LogRow(currentGridRow).GetIndividualRows())
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
                statusCard.Default = SpeciesCount.ToString(Mayfly.Wild.Resources.Interface.Interface.SpeciesCount);
                statusCard.Message(Wild.Resources.Interface.Messages.DuplicateSummed, gridRow.Cells[ColumnSpecies.Index].Value);
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

            SpeciesKey.TaxonRow speciesRow = UserSettings.SpeciesIndex.FindBySpecies(
                    gridRow.Cells[ColumnSpecies.Index].Value.ToString());

            if (speciesRow == null)
            {
                // There is no such species in index
                if ((string)gridRow.Cells[ColumnSpecies.Index].Value ==
                    Species.Resources.Interface.UnidentifiedTitle)
                {
                    result.SetSpcIDNull();
                }
                else
                {
                    Data.SpeciesRow newSpeciesRow = (Data.SpeciesRow)data.Species.Rows.Add(
                        null, (string)gridRow.Cells[ColumnSpecies.Index].Value.ToString());
                    result.SpcID = newSpeciesRow.ID;
                }
            }
            else
            {
                // There is such species in index you using
                Data.SpeciesRow existingSpeciesRow = data.Species.FindBySpecies(speciesRow.Name);
                if (existingSpeciesRow == null)
                {
                    existingSpeciesRow = (Data.SpeciesRow)data.Species.Rows.Add(null, speciesRow.Name);
                }
                result.SpeciesRow = existingSpeciesRow;
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

        public void LoadData(string filename)
        {
            Clear();
            Data = new Data(UserSettings.SpeciesIndex, UserSettings.SamplersIndex);
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
                SelectedSampler = Service.Sampler(Data.Solitary.Sampler);
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

        public int InsertSpecies(SpeciesKey.TaxonRow species)
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

        public void OpenSpecies(string species)
        {
            SpeciesToOpen = UserSettings.SpeciesIndex.FindBySpecies(species);
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
            if (UserSettings.AutoLogOpen) ToolStripMenuItemIndividuals_Click(spreadSheetLog, new EventArgs());

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
                tabControl.SelectedTab = tabPageCollect;
                IsChanged = false;
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (UserSettings.Interface.OpenDialog.FileName == FileName)
                {
                    statusCard.Message(Wild.Resources.Interface.Messages.AlreadyOpened);
                }
                else
                {
                    if (CheckAndSave() != DialogResult.Cancel)
                    {
                        LoadData(UserSettings.Interface.OpenDialog.FileName);
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

            UserSettings.Interface.ExportDialog.FileName =
                IO.SuggestName(IO.FolderName(UserSettings.Interface.SaveDialog.FileName),
                Data.Solitary.GetSuggestedName());

            if (UserSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK)
            {
                Write(UserSettings.Interface.ExportDialog.FileName);
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
            IO.RunFile(UserSettings.SpeciesIndexPath, "-edit");
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            string currentWaters = Wild.UserSettings.WatersIndexPath;
            string currentSpc = UserSettings.SpeciesIndexPath;
            int currentRecentCount = UserSettings.RecentSpeciesCount;

            Settings settings = new Settings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                if (currentWaters != Wild.UserSettings.WatersIndexPath)
                {
                    Wild.UserSettings.WatersIndex = null;
                    waterSelector.Index = Wild.UserSettings.WatersIndex;
                }

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
            }

            ColumnQuantity.ReadOnly = UserSettings.FixTotals;
            ColumnMass.ReadOnly = UserSettings.FixTotals;
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

        #region Sampling tab logics

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
            UpdateStatus();
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

        #endregion

        #region Substrates tab logics

        private NumericUpDown[] MineralUDs
        {
            get
            {
                return new NumericUpDown[] {
                    numericUpDownBoulder, numericUpDownCobble, numericUpDownGravel,
                    numericUpDownSand, numericUpDownSilt, numericUpDownClay };
            }
        }

        private void substrate_ValueChanged(object sender, EventArgs e)
        {
            value_Changed(sender, e);

            if (sender is NumericUpDown down && down.ContainsFocus)
            {
                int selectedValue = (int)down.Value;

                int rest = 100 - selectedValue;

                int currentRest = 0;

                foreach (NumericUpDown numericUpDown in MineralUDs)
                {
                    if (numericUpDown == down) continue;
                    currentRest += (int)numericUpDown.Value;
                }

                if (currentRest > rest)
                {
                    foreach (NumericUpDown numericUpDown in MineralUDs)
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

        #endregion

        #region Weather tab logics

        //private void trackBarClouds_Scroll(object sender, EventArgs e)
        //{
        //    toolTipAttention.ToolTipTitle = checkBoxCloudage.Text;
        //    toolTipAttention.Show(Mayfly.Geographics.Service.CloudageName(trackBarCloudage.Value),
        //        trackBarCloudage, 0, trackBarCloudage.Height, 1500);
        //    ValueChanged(sender, e);
        //}

        //private void checkBoxCloudage_CheckedChanged(object sender, EventArgs e)
        //{
        //    trackBarCloudage.Enabled = checkBoxCloudage.Checked;
        //    ValueChanged(sender, e);
        //}

        //private void comboBoxEvent_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    comboBoxEventDegree.Enabled = Geographics.Service.Weather.IsDegreeAvailable(
        //        (WeatherEvents.EventRow)comboBoxEvent.SelectedValue);
        //    comboBoxEventDegree.DataSource = Geographics.Service.Weather.AvailableDegrees(
        //        (WeatherEvents.EventRow)comboBoxEvent.SelectedValue);
        //    comboBoxEventDegree.SelectedIndex = -1;

        //    comboBoxEventDiscretion.Enabled = Geographics.Service.Weather.IsDiscretionAvailable(
        //        (WeatherEvents.EventRow)comboBoxEvent.SelectedValue);
        //    comboBoxEventDiscretion.SelectedIndex = -1;

        //    comboBoxAdditionalEvent.Enabled = Geographics.Service.Weather.IsAdditionalEventAvailable(
        //        (WeatherEvents.EventRow)comboBoxEvent.SelectedValue);
        //    comboBoxAdditionalEvent.DataSource = Geographics.Service.Weather.AvailableAdditionalEvents(
        //        (WeatherEvents.EventRow)comboBoxEvent.SelectedValue);
        //    comboBoxAdditionalEvent.SelectedIndex = -1;
        //    ValueChanged(sender, e);
        //}

        //private void comboBoxEventDegree_EnabledChanged(object sender, EventArgs e)
        //{
        //    labelEventDegree.Enabled = comboBoxEventDegree.Enabled;
        //}

        //private void comboBoxEventDiscretion_EnabledChanged(object sender, EventArgs e)
        //{
        //    labelEventDiscretion.Enabled = comboBoxEventDiscretion.Enabled;
        //}

        //private void comboBoxAdditionalEvent_EnabledChanged(object sender, EventArgs e)
        //{
        //    labelAdditionalEvent.Enabled = comboBoxAdditionalEvent.Enabled;
        //}

        //private void textBoxWindRate_TextChanged(object sender, EventArgs e)
        //{
        //    ValueChanged(sender, e);
        //    textBoxWindDirection.Enabled = textBoxWindRate.Text.IsDoubleConvertible();
        //}

        //private void textBoxWindDirection_EnabledChanged(object sender, EventArgs e)
        //{
        //    labelWindDirection.Enabled = textBoxWindRate.Enabled;
        //}

        #endregion

        #region Log tab logics

        private void speciesLogger_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            value_Changed(sender, e);

            if (UserSettings.AutoLogOpen)
            {
                spreadSheetLog.ClearSelection();
                e.Row.Selected = true;
                ToolStripMenuItemIndividuals_Click(ToolStripMenuItemIndividuals, new EventArgs());
            }
        }

        private void speciesLogger_DuplicateDetected(object sender, DuplicateFoundEventArgs e)
        {
            int q = 0;
            double w = 0;

            Data.LogRow editedLogRow = SaveLogRow(e.EditedRow);
            Data.LogRow duplicateLogRow = LogRow(e.DuplicateRow);

            // If quantity is already set in edited row
            if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null)
            {
                // Add it to q
                q += (int)e.EditedRow.Cells[ColumnQuantity.Name].Value;
            }

            // If mass is already set in edited row
            if (e.EditedRow.Cells[ColumnMass.Index].Value != null)
            {
                // Add it to w
                w += (double)e.EditedRow.Cells[ColumnMass.Name].Value;

                // If quantity equals 1 and no detailed individuals
                if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null &&
                    (int)e.EditedRow.Cells[ColumnQuantity.Index].Value == 1 &&
                    editedLogRow.GetIndividualRows().Length == 0)
                {
                    // Create new individual record with that mass
                    Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = editedLogRow;
                    newIndividualRow.Mass = (double)e.EditedRow.Cells[ColumnMass.Index].Value;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }
            }

            // If quantity is already set in previously added row
            if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null)
            {
                // Add it to q
                q += (int)e.DuplicateRow.Cells[ColumnQuantity.Name].Value;
            }

            // If mass is already set in previously added row
            if (e.DuplicateRow.Cells[ColumnMass.Index].Value != null)
            {
                // Add it to w
                w += (double)e.DuplicateRow.Cells[ColumnMass.Name].Value;

                // If quantity equals 1 and no detailed individuals
                if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null &&
                    (int)e.DuplicateRow.Cells[ColumnQuantity.Index].Value == 1 &&
                    duplicateLogRow.GetIndividualRows().Length == 0)
                {
                    // Create new individual record with that mass and add it to new log row
                    Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = editedLogRow;
                    if (e.EditedRow.Cells[ColumnMass.Index].Value != null)
                    {
                        newIndividualRow.Mass = (double)e.EditedRow.Cells[ColumnMass.Index].Value;
                    }
                    newIndividualRow.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }
            }

            if (q > 0)
            {
                e.EditedRow.Cells[ColumnQuantity.Index].Value = q;
            }

            if (w > 0)
            {
                e.EditedRow.Cells[ColumnMass.Index].Value = w;
            }

            foreach (Data.IndividualRow individualRow in duplicateLogRow.GetIndividualRows())
            {
                individualRow.LogRow = editedLogRow;
            }

            Clear(e.DuplicateRow);
            spreadSheetLog.Rows.Remove(e.DuplicateRow);

            statusCard.Message(string.Format(Wild.Resources.Interface.Messages.DuplicateSummed,
                e.EditedRow.Cells[ColumnSpecies.Index].Value));
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

        private void spreadSheetLog_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow gridRow = ((DataGridView)sender).Rows[e.RowIndex];
            //gridRow.HeaderCell.Value = gridRow.IsNewRow ? string.Empty : (e.RowIndex + 1).ToString();
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
            spreadSheetLog.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                if (gridRow.Cells[ColumnSpecies.Name].Value != null)
                {
                    Individuals individuals = new Individuals(SaveLogRow(gridRow));
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
                            individualRow.CopyTo(clipLogRow);
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

                SpeciesKey.TaxonRow clipSpeciesRow = UserSettings.SpeciesIndex.FindBySpecies(clipLogRow.SpeciesRow.Species);

                if (clipSpeciesRow == null)
                {
                    Data.SpeciesRow newSpeciesRow = Data.Species.AddSpeciesRow(
                        clipLogRow.SpeciesRow.Species);

                    logRow.SpcID = newSpeciesRow.ID;
                }
                else
                {
                    if (Data.Species.FindBySpecies(clipSpeciesRow.Name) == null)
                    {
                        Data.Species.Rows.Add(clipSpeciesRow.ID, clipSpeciesRow.Name);
                    }
                    logRow.SpcID = clipSpeciesRow.ID;
                }

                Data.Log.AddLogRow(logRow);

                foreach (Data.IndividualRow clipIndividualRow in clipLogRow.GetIndividualRows())
                {
                    clipIndividualRow.CopyTo(logRow);
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