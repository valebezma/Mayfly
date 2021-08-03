using Mayfly.Extensions;
using Mayfly.Geographics;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly.Fish
{
    public partial class Card : Form
    {
        #region Properties

        private string fileName;

        private string SpeciesToOpen;

        private bool preciseMode;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? FileSystem.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
                itemAboutCard.Visible = value != null;
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

        private bool AllowEffortCalculation { get; set; }

        private bool PreciseAreaMode
        {
            get
            {
                return preciseMode;
            }

            set
            {
                preciseMode = value;

                panelLS.Visible = !preciseMode;
                panelGeoData.Visible = preciseMode;

                //if (value)
                //{
                //    panelH.Top = 257+26;
                //}
                //else
                //{
                //    panelH.Top = 336;
                //}

            }
        }

        #endregion



        public Card()
        {
            InitializeComponent();

            LoadEquipment();

            Data = new Data(UserSettings.SpeciesIndex);
            FileName = null;

            waterSelector.CreateList();
            waterSelector.Index = Wild.UserSettings.WatersIndex;
            AllowEffortCalculation = false;

            if (Wild.UserSettings.WatersIndex != null && UserSettings.SelectedWaterID != 0)
            {
                WatersKey.WaterRow selectedWater = Wild.UserSettings.WatersIndex.Water.FindByID(
                    UserSettings.SelectedWaterID);

                if (selectedWater != null)
                {
                    waterSelector.WaterObject = selectedWater;
                }
            }

            comboBoxSampler.DataSource = UserSettings.SamplersIndex.Sampler.Select();
            comboBoxSampler.SelectedIndex = -1;
            sampler_Changed(comboBoxSampler, new EventArgs());

            if (UserSettings.SelectedSamplerID != 0)
            {
                SelectedSampler = Service.Sampler(UserSettings.SelectedSamplerID);
            }


            if (UserSettings.SelectedDate != null)
            {
                waypointControl1.Waypoint.TimeMark = UserSettings.SelectedDate;
            }

            labelDuration.ResetFormatted(0, 0, 0);

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
            AllowEffortCalculation = true;

            //menuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            //menuItemSpeciesRef.Enabled = UserSettings.SpeciesIndexPath != null;

            IsChanged = false;

            UpdateStatus();
        }



        #region Methods

        private void Clear()
        {
            FileName = null;

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;

            waypointControl1.Clear();

            ClearGear();
            ClearEffort();

            textBoxComments.Text = string.Empty;

            aquaControl1.Clear();
            weatherControl1.Clear();

            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();

            Data = new Data();
        }

        private void ClearGear()
        {
            textBoxLength.Text = string.Empty;
            textBoxOpening.Text = string.Empty;
            textBoxHeight.Text = string.Empty;
            textBoxSquare.Text = string.Empty;
            textBoxMesh.Text = string.Empty;
            textBoxHook.Text = string.Empty;
        }

        private void ClearEffort()
        {
            dateTimePickerStart.Value = waypointControl1.Waypoint.TimeMark.AddHours(-12.0);
            textBoxVelocity.Text = string.Empty;
            textBoxExposure.Text = string.Empty;
            textBoxDepth.Text = string.Empty;
        }

        private void Clear(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnID.Index].Value != null)
            {
                Data.LogRow logRow = Data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
                Data.SpeciesRow spcRow = logRow.SpeciesRow;
                logRow.Delete();
                if (spcRow != null) spcRow.Delete();
            }
        }

        public void UpdateStatus()
        {
            if (Data.Solitary.Investigator == null)
            {
                statusCard.Default = StatusLog.Text = 
                    SpeciesCount.ToString(Mayfly.Wild.Resources.Interface.Interface.SpeciesCount); ;
            }
            else
            {
                statusCard.Default = StatusLog.Text = string.Format("© {0:yyyy} {1}",
                    Data.Solitary.When, Data.Solitary.Investigator);
            }

            StatusMass.ResetFormatted(Mass);
            StatusCount.ResetFormatted(Quantity);
        }

        private void Write(string fileName)
        {
            if (UserSettings.SpeciesAutoExpand)
            {
                speciesLogger.UpdateIndex(Data.GetSpeciesKey(), UserSettings.SpeciesAutoExpandVisual);
            }

            switch (Path.GetExtension(fileName))
            {
                case ".fcd":
                    Data.WriteToFile(fileName);
                    break;

                case ".html":
                    Data.GetReport().WriteToFile(fileName);
                    break;
            }

            statusCard.Message(Wild.Resources.Interface.Messages.Saved);
            FileName = fileName;
            IsChanged = false;

            SaveGear();
        }

        private void SaveGear()
        {
            if (SelectedSampler == null) return;

            Equipment.UnitsRow unitRow = UserSettings.Equipment.Units.NewUnitsRow();
            unitRow.SamplerID = SelectedSampler.ID;

            if (SelectedSampler.EffortFormula.Contains("M"))
            {
                if (textBoxMesh.Text.IsDoubleConvertible()) { unitRow.Mesh = int.Parse(textBoxMesh.Text); }
                else return;
            }

            if (SelectedSampler.EffortFormula.Contains("J"))
            {
                if (textBoxHook.Text.IsDoubleConvertible()) { unitRow.Hook = (int)double.Parse(textBoxHook.Text); }
                else return;
            }

            if (SelectedSampler.EffortFormula.Contains("H"))
            {
                if (textBoxHeight.Text.IsDoubleConvertible()) { unitRow.Height = double.Parse(textBoxHeight.Text); }
                else return;
            }

            if (SelectedSampler.EffortFormula.Contains("L"))
            {
                if (textBoxLength.Text.IsDoubleConvertible()) { unitRow.Length = double.Parse(textBoxLength.Text); }
                else return;
            }

            if (SelectedSampler.EffortFormula.Contains("O") && textBoxOpening.Text.IsDoubleConvertible())
            {
                unitRow.Opening = double.Parse(textBoxOpening.Text);
            }

            if (UserSettings.Equipment.Units.FindDuplicate(unitRow) == null)
            {
                UserSettings.Equipment.Units.AddUnitsRow(unitRow);
                Service.SaveEquipment();
                LoadEquipment();
            }
        }

        //private void SaveOpenings()
        //{
        //    if (!Data.Solitary.IsOpeningNull() && !Data.Solitary.IsLengthNull())
        //    {
        //        double opening = Data.Solitary.Opening / Data.Solitary.Length;

        //        if (Service.DefaultOpening(Data.Solitary.Sampler) != opening)
        //        {
        //            // TODO:
        //            // What to do? Just replace?
        //            // Data.SingleCardRow.Opening / Data.SingleCardRow.Length
        //        }
        //    }
        //}

        private void SaveData()
        {
            Data.CardRow cardRow = SaveCardRow();

            #region Save environment data

            if (aquaControl1.AquaState != null)
            {
                aquaControl1.Save();

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

            SaveWaterValues();

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

        private void SaveWaterValues()
        {
            if (!waterSelector.IsWaterSelected)
            {
                UserSettings.SelectedWaterID = 0;
                Data.Solitary.SetWaterIDNull();
                return;
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
                    return;
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

                if (textBoxMesh.Enabled && textBoxMesh.Text.IsDoubleConvertible())
                {
                    Data.Solitary.Mesh = (int)double.Parse(textBoxMesh.Text);
                }
                else
                {
                    Data.Solitary.SetMeshNull();
                }
            }

            if (PreciseAreaMode)
            {
                if (textBoxExactArea.Text.IsDoubleConvertible())
                    Data.Solitary.ExactArea = 10000d * double.Parse(textBoxExactArea.Text);
            }

            if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("H") && textBoxHeight.Text.IsDoubleConvertible())
            {
                Data.Solitary.Height = double.Parse(textBoxHeight.Text);
            }
            else
            {
                Data.Solitary.SetHeightNull();
            }

            if (textBoxDepth.Text.IsDoubleConvertible())
            {
                Data.Solitary.Depth = double.Parse(textBoxDepth.Text);
            }
            else
            {
                Data.Solitary.SetDepthNull();
            }

            if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("M") && textBoxMesh.Text.IsDoubleConvertible())
            {
                Data.Solitary.Mesh = int.Parse(textBoxMesh.Text);
            }
            else
            {
                Data.Solitary.SetMeshNull();
            }

            if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("J") && textBoxHook.Text.IsDoubleConvertible())
            {
                Data.Solitary.Hook = (int)double.Parse(textBoxHook.Text);
            }
            else
            {
                Data.Solitary.SetHookNull();
            }

            if (Data.Solitary.IsExactAreaNull())
            {
                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("L") && textBoxLength.Text.IsDoubleConvertible())
                {
                    Data.Solitary.Length = double.Parse(textBoxLength.Text);
                }
                else
                {
                    Data.Solitary.SetLengthNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("O") && textBoxOpening.Text.IsDoubleConvertible())
                {
                    Data.Solitary.Opening = double.Parse(textBoxOpening.Text);
                }
                else
                {
                    Data.Solitary.SetOpeningNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("S") && textBoxSquare.Text.IsDoubleConvertible())
                {
                    Data.Solitary.Square = double.Parse(textBoxSquare.Text);
                }
                else
                {
                    Data.Solitary.SetSquareNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("T"))
                {
                    Data.Solitary.Span = (int)(waypointControl1.Waypoint.TimeMark - dateTimePickerStart.Value).TotalMinutes;
                }
                else
                {
                    Data.Solitary.SetSpanNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("V") && textBoxVelocity.Text.IsDoubleConvertible())
                {
                    Data.Solitary.Velocity = double.Parse(textBoxVelocity.Text);
                }
                else
                {
                    Data.Solitary.SetVelocityNull();
                }

                if (SelectedSampler != null && SelectedSampler.EffortFormula.Contains("E") && textBoxExposure.Text.IsDoubleConvertible())
                {
                    Data.Solitary.Exposure = double.Parse(textBoxExposure.Text);
                }
                else
                {
                    Data.Solitary.SetExposureNull();
                }
            }

            Data.Solitary.SamplerPresentation = Data.Solitary.IsSamplerNull() ? Constants.Null : Data.Solitary.GetSamplerRow().Sampler;
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
            if (gridRow.Index == -1) return;

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
            Data.LogRow result = GetLogRow(data, gridRow);

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

        public Data.LogRow SaveLogRow(DataGridViewRow gridRow)
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

        private Data.LogRow GetLogRow(DataGridViewRow gridRow)
        {
            return GetLogRow(Data, gridRow);
        }

        private Data.LogRow GetLogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result;
            if (gridRow.Cells[ColumnID.Index].Value == null ||
                data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value) == null)
            {
                result = data.Log.NewLogRow();
                result.CardRow = data.Solitary;
                data.Log.AddLogRow(result);
            }
            else
            {
                result = data.Log.FindByID((int)gridRow.Cells[ColumnID.Index].Value);
            }

            return result;
        }

        public void LoadData(string fileName)
        {
            Clear();
            Data = new Data();
            Data.Read(fileName);
            LoadData();
            FileName = fileName;
            Log.Write("Loaded from {0}.", fileName);
            IsChanged = false;
        }

        private void LoadData()
        {
            AllowEffortCalculation = false;

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
            //waypointControl1.Waypoint.TimeMark = 
                dateTimePickerEnd.Value = Data.Solitary.When;

            #region Sampling

            if (Data.Solitary.IsSamplerNull())
            {
                comboBoxSampler.SelectedIndex = -1;
            }
            else
            {
                SelectedSampler = Data.Solitary.GetSamplerRow();
            }

            if (Data.Solitary.IsExactAreaNull())
            {
                textBoxExactArea.Text = string.Empty;
            }
            else
            {
                textBoxExactArea.Text = (Data.Solitary.ExactArea / 10000).ToString();
                PreciseAreaMode = true;
            }

            if (Data.Solitary.IsMeshNull())
            {
                textBoxMesh.Text = string.Empty;
            }
            else
            {
                textBoxMesh.Text = Data.Solitary.Mesh.ToString();
            }

            if (Data.Solitary.IsHookNull())
            {
                textBoxHook.Text = string.Empty;
            }
            else
            {
                textBoxHook.Text = Data.Solitary.Hook.ToString();
            }

            if (!PreciseAreaMode)
            {
                if (Data.Solitary.IsLengthNull())
                {
                    textBoxLength.Text = string.Empty;
                }
                else
                {
                    textBoxLength.Text = Data.Solitary.Length.ToString();
                }

                if (Data.Solitary.IsOpeningNull())
                {
                    textBoxOpening.Text = string.Empty;
                }
                else
                {
                    textBoxOpening.Text = Data.Solitary.Opening.ToString();
                }

                if (Data.Solitary.IsSquareNull())
                {
                    textBoxSquare.Text = string.Empty;
                }
                else
                {
                    textBoxSquare.Text = Data.Solitary.Square.ToString();
                }

                if (Data.Solitary.IsSpanNull())
                {
                    dateTimePickerStart.Value = Data.Solitary.When.AddHours(-12);
                }
                else
                {
                    dateTimePickerStart.Value = Data.Solitary.When - Data.Solitary.Duration;
                }

                if (Data.Solitary.IsVelocityNull())
                {
                    textBoxVelocity.Text = string.Empty;
                }
                else
                {
                    textBoxVelocity.Text = Data.Solitary.Velocity.ToString();
                }

                if (Data.Solitary.IsExposureNull())
                {
                    textBoxExposure.Text = string.Empty;
                }
                else
                {
                    textBoxExposure.Text = Data.Solitary.Exposure.ToString();
                }
            }

            if (Data.Solitary.IsHeightNull())
            {
                textBoxHeight.Text = string.Empty;
            }
            else
            {
                textBoxHeight.Text = Data.Solitary.Height.ToString();
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

            AllowEffortCalculation = true;

            sampler_Changed(comboBoxSampler, new EventArgs());

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

            if (this.Height == this.MinimumSize.Height && !Data.Solitary.IsCommentsNull())
            {
                this.Height += 100;
            }

            UpdateStatus();
            IsChanged = false;
        }

        private void LoadEquipment()
        {
            contextGear.Items.Clear();

            foreach (Equipment.UnitsRow unitRow in UserSettings.Equipment.Units)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = unitRow.ToString();
                item.Click += (o, e) => {
                    comboBoxSampler.SelectedItem = UserSettings.SamplersIndex.Sampler.FindByID(unitRow.SamplerID);
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
            UpdateLogRow(gridRow, logRow);
            spreadSheetLog.Rows.Insert(rowIndex, gridRow);
            HandleLogRow(gridRow);
        }

        public void UpdateLogRow(Data.LogRow logRow)
        {
            UpdateLogRow(speciesLogger.FindLine(logRow.SpeciesRow.Species), logRow);
        }

        private void UpdateLogRow(DataGridViewRow gridRow, Data.LogRow logRow)
        {
            gridRow.Cells[ColumnSpecies.Index].Value = logRow.SpeciesRow.Species;

            if (logRow.IsQuantityNull()) gridRow.Cells[ColumnQuantity.Index].Value = null;
            else gridRow.Cells[ColumnQuantity.Index].Value = logRow.Quantity;

            if (logRow.IsMassNull()) gridRow.Cells[ColumnMass.Index].Value = null;
            else gridRow.Cells[ColumnMass.Index].Value = logRow.Mass;

            if (logRow.IsCommentsNull()) gridRow.Cells[ColumnSpecies.Index].ToolTipText = string.Empty;            
            else gridRow.Cells[ColumnSpecies.Index].ToolTipText = logRow.Comments;
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
                    ToolStripMenuItemSave_Click(ToolStripMenuItemSave, new EventArgs());
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

        private void SetEndpoint(Waypoint waypoint)
        {
            waypointControl1.Waypoint.Latitude = waypoint.Latitude;
            waypointControl1.Waypoint.Longitude = waypoint.Longitude;

            if (!waypoint.IsTimeMarkNull)
            {
                if (Data.Solitary.GetSamplerRow().IsPassive() && taskDialogLocationHandle.ShowDialog(this) == tdbSinking)
                {
                    dateTimePickerStart.Value = waypoint.TimeMark;
                    //sampler_Changed(dateTimePickerStart, new EventArgs());
                }
                else
                {
                    waypointControl1.Waypoint.TimeMark = waypoint.TimeMark;
                    dateTimePickerEnd.Value = waypoint.TimeMark;
                }
            }

            waypointControl1.UpdateValues();

            if (waypoint.IsNameNull) { }
            else
            {
                textBoxLabel.Text = waypoint.Name;
            }
        }

        //private void HandlePolygon(Polygon polygon)
        //{
        //    PreciseAreaMode = true;
        //    textBoxExactArea.Text = (polygon.Area / 10000d).ToString(Mayfly.Service.Mask(4));
        //    SetEndpoint(polygon.Points.Last());
        //}

        public void OpenSpecies(string species)
        {
            SpeciesToOpen = species;
            Load += new EventHandler(cardOpenSpecies_Load);
        }

        public void OpenIndividuals(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnSpecies.Name].Value == null) return;
            Individuals individuals = OpenIndividuals(SaveLogRow(gridRow));
            individuals.LogLine = gridRow;
            individuals.SetFriendlyDesktopLocation(gridRow);
        }

        public Individuals GetOpenedIndividuals(Data.LogRow logRow)
        {
            Individuals individuals = null;

            foreach (Form form in Application.OpenForms)
            {
                if (!(form is Individuals)) continue;
                if (((Individuals)form).LogRow == logRow)
                {
                    individuals = (Individuals)form;
                }
            }

            return individuals;
        }

        public Individuals OpenIndividuals(Data.LogRow logRow)
        {
            Individuals individuals = GetOpenedIndividuals(logRow);

            if (individuals == null)
            {
                individuals = new Individuals(logRow);
                individuals.SetFriendlyDesktopLocation(spreadSheetLog);
                individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
                individuals.Show(this);
            }
            else
            {
                //individuals.SaveData();
                //individuals.UpdateValues();
                individuals.BringToFront();
            }
            
            return individuals;
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e)
        {
            Individuals individuals = sender as Individuals;
            if (individuals.DialogResult == DialogResult.OK)
            {
                IsChanged |= individuals.ChangesWereMade;
            }
        }

        //public void OpenTrophics(string species, string regID)
        //{
        //    SpeciesToOpen = species;
        //    Load += new EventHandler(cardOpenSpecies_Load);
        //}

        #endregion



        private void Card_Load(object sender, EventArgs e)
        {        }

        private void cardOpenSpecies_Load(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabPageLog;
            if (Data.Species.FindBySpecies(SpeciesToOpen) == null) return;

            speciesLogger.InsertSpecies(SpeciesToOpen);
            if (!UserSettings.AutoLogOpen)
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

        private void menuItemEmpty_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                Clear();
                IsChanged = false;
            }
        }

        private void menuItemSpot_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                FileName = null;

                ClearGear();
                //ClearEffort();

                textBoxComments.Text = string.Empty;

                spreadSheetLog.Rows.Clear();
                spreadSheetAddt.Rows.Clear();

                Data = new Data();

                tabControl.SelectedTab = tabPageCollect;
                textBoxLabel.Focus();

                IsChanged = false;
            }
        }

        private void menuItemGear_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                FileName = null;

                waypointControl1.Save();
                waypointControl1.Clear();
                ClearEffort();

                textBoxComments.Text = string.Empty;

                spreadSheetLog.Rows.Clear();
                spreadSheetAddt.Rows.Clear();

                Data = new Data();

                tabControl.SelectedTab = tabPageCollect;
                textBoxLabel.Focus();

                IsChanged = false;
            }
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
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

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
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

            UserSettings.Interface.SaveAsDialog.FileName =
                FileSystem.SuggestName(FileSystem.FolderName(UserSettings.Interface.SaveDialog.FileName),
                Data.GetSuggestedName());

            if (UserSettings.Interface.SaveAsDialog.ShowDialog() == DialogResult.OK)
            {
                Write(UserSettings.Interface.SaveAsDialog.FileName);
            }
        }

        private void ToolStripMenuItemPrintPreview_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                SaveData();
            }

            Data.GetReport().Preview();
        }

        private void ToolStripMenuItemPrint_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                SaveData();
            }

            Data.GetReport().Print();
        }

        private void ToolStripMenuItemCardBlank_Click(object sender, EventArgs e)
        {
            FishReport.BlankCard.Run();
        }

        private void ToolStripMenuItemIndividualsLogBlank_Click(object sender, EventArgs e)
        {
            FishReport.BlankIndividualsLog.Run();
        }

        private void ToolStripMenuItemIndividualProfileBlank_Click(object sender, EventArgs e)
        {
            FishReport.BlankIndividualProfile.Run();
        }

        private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Data menu

        private void menuItemEnvironment_Click(object sender, EventArgs e)
        {
            tabPageEnvironment.Parent = tabControl;
            tabControl.SelectedTab = tabPageEnvironment;
        }

        private void menuItemFactors_Click(object sender, EventArgs e)
        {
            tabPageFactors.Parent = tabControl;
            tabControl.SelectedTab = tabPageFactors;
        }

        private void menuItemLocation_Click(object sender, EventArgs e)
        {
            if (FileSystem.InterfaceLocation.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                waypointControl1.SelectGPS(FileSystem.InterfaceLocation.OpenDialog.FileNames);
            }
        }

        #endregion

        #region Service menu

        private void ToolStripMenuItemWatersRef_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(Wild.UserSettings.WatersIndexPath);
        }

        private void ToolStripMenuItemSpeciesRef_Click(object sender, EventArgs e)
        {
            FileSystem.RunFile(UserSettings.SpeciesIndexPath);
        }

        private void ToolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            string currentWaters = Wild.UserSettings.WatersIndexPath;
            string currentSpc = UserSettings.SpeciesIndexPath;
            int currentRecentCount = UserSettings.RecentSpeciesCount;

            Settings settings = new Settings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                menuItemWatersRef.Enabled = File.Exists(Wild.UserSettings.WatersIndexPath);
                menuItemSpeciesRef.Enabled = File.Exists(UserSettings.SpeciesIndexPath);

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

                //if (writeMode != Mayfly.UserSettings.FormatLocation)
                //{
                //    waypointControl1.ResetAppearance(Mayfly.UserSettings.FormatLocation);
                //}

                ColumnQuantity.ReadOnly = UserSettings.FixTotals;
                ColumnMass.ReadOnly = UserSettings.FixTotals;

                LoadEquipment();
            }
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            about.SetPowered(Properties.Resources.sriif, Wild.Resources.Interface.Powered.SRIIF);
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
            statusCard.Message(Wild.Resources.Interface.Messages.WaterSet);
            SaveWaterValues();
            sampler_ValueChanged(sender, e);
        }

        private void waypointControl1_Changed(object sender, EventArgs e)
        {
            if (waypointControl1.ContainsFocus)
            {
                dateTimePickerEnd.Value = waypointControl1.Waypoint.TimeMark;
            }
        }

        private void waypointControl1_LocationImported(object sender, LocationDataEventArgs e)
        {
            if (!SelectedSampler.IsEffortFormulaNull() &&
                SelectedSampler.EffortFormula.Contains('E'))
            {
                ListLocation locationSelection = new ListLocation(e.Filenames);
                locationSelection.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                locationSelection.LocationSelected += new LocationEventHandler(locationData_Selected);
                locationSelection.ShowDialog(this);
            }
            else
            {
                ListWaypoints waypoints = new ListWaypoints(e.Filenames);

                if (waypoints.Count == 0)
                {  }
                else if (waypoints.Count == 1)
                {
                    PreciseAreaMode = false;
                    SetEndpoint(waypoints.Value);
                }
                else
                {
                    waypoints.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                    waypoints.WaypointSelected += new LocationEventHandler(locationData_Selected);
                    waypoints.Show(this);
                }
            }
        }

        private void locationData_Selected(object sender, LocationEventArgs e)
        {
            if (e.LocationObject is Waypoint waypoint)
            {
                PreciseAreaMode = false;
                SetEndpoint(waypoint);
            }
            else if (e.LocationObject is Polygon)
            {
                Polygon poly = (Polygon)e.LocationObject;
                PreciseAreaMode = true;
                textBoxExactArea.Text = (poly.Area / 10000d).ToString(Mayfly.Service.Mask(4));
                SetEndpoint(poly.Points.Last());

                //HandlePolygon((Polygon)e.LocationObject);
            }
            else if (e.LocationObject is Track)
            {
                tdbAsPoly.Enabled = !SelectedSampler.EffortFormula.Contains("T");

                TaskDialogButton tdb = taskDialogTrackHandle.ShowDialog();

                PreciseAreaMode = tdb == tdbAsPoly;

                //Track track = (Track)e.LocationObject;
                Track[] tracks = (Track[])e.LocationObjects;
                Waypoint[] wpts = Track.GetWaypoints(tracks);

                if (tdb == tdbSinglepoint)
                {
                    ListWaypoints waypoints = new ListWaypoints(wpts);
                    waypoints.SetFriendlyDesktopLocation(waypointControl1, FormLocation.Centered);
                    waypoints.WaypointSelected += new LocationEventHandler(locationData_Selected);
                    waypoints.Show(this);
                    //SetEndpoint(track.Points.Last());
                }
                else if (tdb == tdbExposure)
                {
                    // Just insert exposure value
                    SetEndpoint(wpts.Last());

                    textBoxExposure.Text = Track.TotalLength(tracks).ToString(Mayfly.Service.Mask(1));
                    if (SelectedSampler.EffortFormula.Contains("T")) dateTimePickerStart.Value = tracks[0].Points[0].TimeMark;
                    if (SelectedSampler.EffortFormula.Contains("V")) textBoxVelocity.Text = Track.AverageKmph(tracks).ToString(Mayfly.Service.Mask(3));
                }
                else if (tdb == tdbAsPoly)
                {
                    // Behave like polygon

                    double s = 0;
                    foreach (Track track in tracks)
                    {
                        Polygon poly = new Polygon(track);
                        s += poly.Area;
                    }

                    PreciseAreaMode = true;
                    textBoxExactArea.Text = (s / 10000d).ToString(Mayfly.Service.Mask(4));
                    SetEndpoint(wpts.Last());

                    //HandlePolygon(new Polygon(track));
                }
            }

            tabControl.SelectedTab = tabPageCollect;
        }


        private void sampler_Changed(object sender, EventArgs e)
        {
            if (SelectedSampler == null) return;

            if (SelectedSampler.IsEffortFormulaNull()) return;

            labelLength.Enabled = textBoxLength.Enabled = SelectedSampler.EffortFormula.Contains("L");
            labelOpening.Enabled = textBoxOpening.Enabled = SelectedSampler.EffortFormula.Contains("O");
            labelHeight.Enabled = textBoxHeight.Enabled = SelectedSampler.EffortFormula.Contains("H");
            labelSquare.Enabled = textBoxSquare.Enabled = SelectedSampler.EffortFormula.Contains("S");

            labelMesh.Enabled = textBoxMesh.Enabled = SelectedSampler.EffortFormula.Contains("M");
            labelHook.Enabled = textBoxHook.Enabled = SelectedSampler.EffortFormula.Contains("J");

            //labelOperation.Enabled = maskedTextBoxOperation.Enabled = SelectedSampler.EffortFormula.Contains("T");
            labelOperation.Enabled = dateTimePickerStart.Enabled = SelectedSampler.EffortFormula.Contains("T");
            labelVelocity.Enabled = textBoxVelocity.Enabled = SelectedSampler.EffortFormula.Contains("V");
            labelExposure.Enabled = textBoxExposure.Enabled = SelectedSampler.EffortFormula.Contains("E");
            textBoxExposure.ReadOnly = SelectedSampler.EffortFormula.Contains("V") && SelectedSampler.EffortFormula.Contains("T");

            sampler_ValueChanged(sender, e);
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = dateTimePickerStart.Value.AddSeconds(
                -dateTimePickerStart.Value.Second);

            //dateTimePickerEnd.MinDate = dateTimePickerStart.Value;

            sampler_ValueChanged(sender, e);

            if (!Data.Solitary.IsSpanNull())
            {
                labelDuration.ResetFormatted(Math.Floor(Data.Solitary.Duration.TotalHours),
                    Data.Solitary.Duration.Minutes, Data.Solitary.Duration.TotalHours);
            }
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.Value = dateTimePickerEnd.Value.AddSeconds(
                -dateTimePickerEnd.Value.Second);

            if (dateTimePickerEnd.ContainsFocus)
            {
                waypointControl1.SetDateTime(dateTimePickerEnd.Value);
                waypointControl1.Save();
            }

            //dateTimePickerStart.MaxDate = dateTimePickerEnd.Value;

            sampler_ValueChanged(sender, e);

            if (!Data.Solitary.IsSpanNull())
            {
                labelDuration.ResetFormatted(Math.Floor(Data.Solitary.Duration.TotalHours),
                    Data.Solitary.Duration.Minutes, Data.Solitary.Duration.TotalHours);
            }
        }

        private void dateTimePickerStart_EnabledChanged(object sender, EventArgs e)
        {
            labelDuration.Enabled = dateTimePickerStart.Enabled;
        }

        private void sampler_ValueChanged(object sender, EventArgs e)
        {
            if (AllowEffortCalculation)
            {
                SaveSamplerValues();

                if (textBoxOpening.Enabled)
                {
                    if (textBoxLength.Text.IsDoubleConvertible())
                    {
                        if (textBoxOpening.Text.IsDoubleConvertible())
                        {
                            if (Convert.ToDouble(textBoxOpening.Text) >=
                                Convert.ToDouble(textBoxLength.Text))
                            {
                                pictureBoxWarnOpening.Visible = true;
                                statusCard.Message(Resources.Interface.Messages.EffectiveError);
                            }
                            else
                            {
                                pictureBoxWarnOpening.Visible = false;
                            }
                        }
                        else
                        {
                            pictureBoxWarnOpening.Visible = true;
                            statusCard.Message(Resources.Interface.Messages.EffectiveEmpty);
                        }


                        if (textBoxExposure.Text.IsDoubleConvertible() &&
                            Convert.ToDouble(textBoxExposure.Text) < 2 * Convert.ToDouble(textBoxLength.Text) / Math.PI)
                        {
                            pictureBoxWarnExposure.Visible = true;
                            statusCard.Message(Resources.Interface.Messages.SeinSpreadError);
                        }
                        else
                        {
                            pictureBoxWarnExposure.Visible = false;
                        }
                    }
                    else
                    {
                        pictureBoxWarnOpening.Visible = false;
                        pictureBoxWarnExposure.Visible = false;
                    }
                }
                else
                {
                    pictureBoxWarnOpening.Visible = false;
                    pictureBoxWarnExposure.Visible = false;
                }

                if (textBoxExposure.Enabled && textBoxExposure.ReadOnly)
                {
                    textBoxExposure.Text = Data.Solitary.GetExposure().ToString("0.####");
                }

                textBoxEfforts.Text = Data.Solitary.GetEffort(ExpressionVariant.Efforts).ToString("0.####");
                textBoxArea.Text = Data.Solitary.GetEffort(ExpressionVariant.Square).ToString("0.####");
                textBoxVolume.Text = Data.Solitary.GetEffort(ExpressionVariant.Volume).ToString("0.####");
            }

            IsChanged = true;
        }

        private void buttonGear_Click(object sender, EventArgs e)
        {
            contextGear.Show(buttonGear, new Point(0, buttonGear.Height), ToolStripDropDownDirection.BelowRight);
        }

        private void samplerValue_EnabledChanged(object sender, EventArgs e)
        {
            if (!((TextBox)sender).Enabled)
            {
                ((TextBox)sender).Text = string.Empty;
            }
        }

        private void maskedTextBoxOperation_EnabledChanged(object sender, EventArgs e)
        {
            if (!((MaskedTextBox)sender).Enabled)
            {
                ((MaskedTextBox)sender).Text = string.Empty;
            }
        }


        private void pictureBoxWarnOpening_MouseHover(object sender, EventArgs e)
        {
            if (textBoxOpening.Text.IsDoubleConvertible())
            {
                toolTipAttention.ToolTipTitle = Resources.Interface.Messages.EffectiveError;
                toolTipAttention.Show(string.Format(Resources.Interface.Messages.FixOpening,
                    textBoxOpening.Text, textBoxLength.Text),
                    textBoxOpening, 0, textBoxOpening.Height);
            }
            else
            {
                toolTipAttention.ToolTipTitle = Resources.Interface.Messages.EffectiveEmpty;
                toolTipAttention.Show(Resources.Interface.Messages.FixEmptyOpening,
                    textBoxOpening, 0, textBoxOpening.Height);
            }
        }

        private void pictureBoxWarnOpening_MouseLeave(object sender, EventArgs e)
        {
            toolTipAttention.Hide(textBoxOpening);
        }

        private void pictureBoxWarnOpening_DoubleClick(object sender, EventArgs e)
        {
            textBoxOpening.Text = (Convert.ToDouble(textBoxLength.Text) *
                Service.DefaultOpening(SelectedSampler.ID)).ToString(Mayfly.Service.Mask(0));
        }


        private void pictureBoxWarnExposure_MouseHover(object sender, EventArgs e)
        {
            toolTipAttention.ToolTipTitle = Resources.Interface.Messages.SeinSpreadError;
            toolTipAttention.Show(string.Format(Resources.Interface.Messages.FixSpread,
                textBoxExposure.Text, textBoxLength.Text),
                textBoxExposure, 0, textBoxExposure.Height);
        }

        private void pictureBoxWarnExposure_MouseLeave(object sender, EventArgs e)
        {
            toolTipAttention.Hide(textBoxExposure);
        }

        private void pictureBoxWarnExposure_DoubleClick(object sender, EventArgs e)
        {
            textBoxExposure.Text = Math.Ceiling(2 * Convert.ToDouble(textBoxLength.Text) / Math.PI).ToString(Mayfly.Service.Mask(0));
        }

        #endregion

        #region Weather tab logics

        private void weatherControl1_Changed(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        #endregion

        #region Log tab logics

        private void speciesLogger_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            value_Changed(sender, e);

            if (UserSettings.AutoLogOpen)
            {
                spreadSheetLog.ClearSelection();
                e.Row.Selected = true;
                OpenIndividuals(e.Row);
                //ToolStripMenuItemIndividuals_Click(ToolStripMenuItemIndividuals, new EventArgs());
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

        private void spreadSheetLog_InputFailed(object sender, DataGridViewCellEventArgs e)
        {
            toolTipAttention.ToolTipTitle = Resources.Interface.Messages.LogBlocked;
            Rectangle rect = spreadSheetLog.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            toolTipAttention.Show(Resources.Interface.Messages.LogInstruction, tabPageLog,
                rect.Left + spreadSheetLog.Left, rect.Bottom + spreadSheetLog.Top, 5000);
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
                OpenIndividuals(gridRow);
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

                        foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                        {
                            Data.StratifiedRow clipStratifiedRow = clipData.Stratified.NewStratifiedRow();

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

                SpeciesKey.SpeciesRow clipSpeciesRow = UserSettings.SpeciesIndex
                    .Species.FindBySpecies(clipLogRow.SpeciesRow.Species);

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

                foreach (Data.StratifiedRow clipStratifiedRow in clipData.Stratified)
                {
                    Data.StratifiedRow stratifiedRow = Data.Stratified.NewStratifiedRow();
                    stratifiedRow.Class = clipStratifiedRow.Class;
                    if (!clipStratifiedRow.IsCountNull()) stratifiedRow.Count = clipStratifiedRow.Count;
                    stratifiedRow.LogRow = logRow;
                    Data.Stratified.AddStratifiedRow(stratifiedRow);
                }

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