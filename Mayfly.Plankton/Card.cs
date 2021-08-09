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

namespace Mayfly.Plankton
{
    public partial class Card : Form
    {
        private string fileName;

        private string SpeciesToOpen;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
                fileName = value;
            }

            get
            {
                return fileName;
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
                    textBoxVolume.Text = (value * 1000d).ToString(Textual.Mask(2));

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



        public Card()
        {
            InitializeComponent();

            Data = new Data();
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
            ColumnSubsample.ValueType = typeof(double);
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

            Data = new Data();
        }

        private void Clear(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnID.Index].Value != null)
            {
                Data.LogRow logRow = Data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
                Data.SpeciesRow spcRow = logRow.SpeciesRow;
                logRow.Delete();
                spcRow.Delete();
            }
        }

        public void UpdateStatus()
        {
            statusCard.Default = StatusLog.Text =
                SpeciesCount.ToString(Mayfly.Wild.Resources.Interface.Interface.SpeciesCount);
            StatusMass.ResetFormatted(Mass);
            StatusCount.ResetFormatted(Quantity);
        }

        private void Write(string fileName)
        {
            if (UserSettings.SpeciesAutoExpand) // If it is set to automatically expand global reference
            {
                speciesLogger.UpdateIndex(Data.GetSpeciesKey(), UserSettings.SpeciesAutoExpandVisual);

                //    if (speciesLogger.UpdateIndex(Data.GetSpeciesKey(), "Plankton (auto)",
                //    UserSettings.SpeciesAutoExpandVisual) == AutoExpandResult.Created)
                //{
                //    UserSettings.SpeciesIndexPath = speciesLogger.IndexPath;
                //}
            }

            Data.WriteToFile(fileName);
            statusCard.Message(Wild.Resources.Interface.Messages.Saved);
            FileName = fileName;
            IsChanged = false;
        }

        private void SaveData()
        {
            Data.CardRow cardRow = SaveCardRow();

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

            SpeciesKey.SpeciesRow speciesRow = UserSettings.SpeciesIndex.Species.FindBySpecies(
                    gridRow.Cells[ColumnSpecies.Index].Value.ToString());

            if (speciesRow == null)
            {
                // There is no such species in reference
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
                // There is such species in reference you using
                Data.SpeciesRow existingSpeciesRow = data.Species.FindBySpecies(speciesRow.Name);
                if (existingSpeciesRow == null)
                {
                    existingSpeciesRow = (Data.SpeciesRow)data.Species.Rows.Add(null, speciesRow.Name);
                }
                result.SpeciesRow = existingSpeciesRow;
            }

            if (gridRow.Cells[ColumnSubsample.Index].Value == null)
            {
                result.SetSubsampleNull();
            }
            else
            {
                result.Subsample = (double)gridRow.Cells[ColumnSubsample.Index].Value;
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
            if (!logRow.IsSubsampleNull()) gridRow.Cells[ColumnSubsample.Index].Value = logRow.Subsample;
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

        private void SetCombos(WaterType type)
        {
            comboBoxCrossSection.Items.Clear();
            comboBoxCrossSection.Items.AddRange(Wild.Service.CrossSection(type));
            comboBoxBank.Enabled = type != WaterType.Lake;
        }

        public void OpenSpecies(string species)
        {
            SpeciesToOpen = species;
            Load += new EventHandler(CardOpenSpecies_Load);
        }

        private void SetSubsample(double subsample)
        {
            spreadSheetLog.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                gridRow.Cells[ColumnSubsample.Index].Value = subsample;
            }
        }

        #endregion



        private void Card_Load(object sender, EventArgs e)
        {
            IsChanged = false;
            UpdateStatus();
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

            UserSettings.Interface.SaveDialog.FileName =
                IO.SuggestName(IO.FolderName(UserSettings.Interface.SaveDialog.FileName),
                Data.GetSuggestedName());

            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Write(UserSettings.Interface.SaveDialog.FileName);
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
            IO.RunFile(UserSettings.SpeciesIndexPath);
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
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
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
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

            if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null)
            {
                q += (int)e.EditedRow.Cells[ColumnQuantity.Name].Value;
            }

            if (e.EditedRow.Cells[ColumnMass.Index].Value != null)
            {
                if (e.EditedRow.Cells[ColumnQuantity.Index].Value != null &&
                    (int)e.EditedRow.Cells[ColumnQuantity.Index].Value == 1 &&
                    LogRow(e.EditedRow).GetIndividualRows().Length == 0)
                {
                    Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = LogRow(e.EditedRow);
                    newIndividualRow.Mass = (double)e.EditedRow.Cells[ColumnMass.Index].Value;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }

                w += (double)e.EditedRow.Cells[ColumnMass.Name].Value;
            }

            if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null)
            {
                q += (int)e.DuplicateRow.Cells[ColumnQuantity.Name].Value;
            }

            if (e.DuplicateRow.Cells[ColumnMass.Index].Value != null)
            {
                if (e.DuplicateRow.Cells[ColumnQuantity.Index].Value != null &&
                    (int)e.DuplicateRow.Cells[ColumnQuantity.Index].Value == 1 &&
                    LogRow(e.DuplicateRow).GetIndividualRows().Length == 0)
                {
                    Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = LogRow(e.EditedRow);
                    if (e.EditedRow.Cells[ColumnMass.Index].Value != null)
                    {
                        newIndividualRow.Mass = (double)e.EditedRow.Cells[ColumnMass.Index].Value;
                    }
                    newIndividualRow.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }

                w += (double)e.DuplicateRow.Cells[ColumnMass.Name].Value;
            }

            if (q > 0)
            {
                e.EditedRow.Cells[ColumnQuantity.Index].Value = q;
            }

            if (w > 0)
            {
                e.EditedRow.Cells[ColumnMass.Index].Value = w;
            }

            foreach (Data.IndividualRow individualRow in LogRow(e.DuplicateRow).GetIndividualRows())
            {
                individualRow.LogRow = LogRow(e.EditedRow);
            }

            spreadSheetLog.Rows.Remove(e.DuplicateRow);
            Clear(e.DuplicateRow);

            statusCard.Default = SpeciesCount.ToString(Wild.Resources.Interface.Interface.SpeciesCount);
            statusCard.Message(Wild.Resources.Interface.Messages.DuplicateSummed,
                e.EditedRow.Cells[ColumnSpecies.Index].Value);
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

        private void contextSubSecond_Click(object sender, EventArgs e)
        {
            SetSubsample(.5);
        }

        private void contextSubThird_Click(object sender, EventArgs e)
        {
            SetSubsample(.333);
        }

        private void contextSubTenth_Click(object sender, EventArgs e)
        {
            SetSubsample(.1);
        }

        private void ToolStripMenuItemSpeciesKey_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                speciesLogger.FindInKey(gridRow);
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

                SpeciesKey.SpeciesRow clipSpeciesRow = UserSettings.SpeciesIndex.Species.FindBySpecies(clipLogRow.SpeciesRow.Species);

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