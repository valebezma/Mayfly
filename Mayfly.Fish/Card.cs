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
using static Mayfly.Fish.UserSettings;

namespace Mayfly.Fish
{
    public partial class Card : Form
    {
        private string filename;
        private bool preciseMode;
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

        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(ReaderSettings.Interface.Extension), EntryAssemblyInfo.Title);
                itemAboutCard.Visible = value != null;
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

            LoadEquipment();

            Data = new Data(ReaderSettings.TaxonomicIndex, ReaderSettings.SamplersIndex);
            Logger.Data = Data;
            FileName = null;

            waterSelector.CreateList();
            waterSelector.Index = Wild.UserSettings.WatersIndex;
            AllowEffortCalculation = false;

            if (Wild.UserSettings.WatersIndex != null && ReaderSettings.SelectedWaterID != 0)
            {
                WatersKey.WaterRow selectedWater = Wild.UserSettings.WatersIndex.Water.FindByID(
                   ReaderSettings.SelectedWaterID);

                if (selectedWater != null)
                {
                    waterSelector.WaterObject = selectedWater;
                }
            }

            comboBoxSampler.DataSource =ReaderSettings.SamplersIndex.Sampler.Select();
            comboBoxSampler.SelectedIndex = -1;
            sampler_Changed(comboBoxSampler, new EventArgs());

            if (ReaderSettings.SelectedSampler != null)
            {
                SelectedSampler = ReaderSettings.SelectedSampler;
            }


            if (ReaderSettings.SelectedDate != null)
            {
                waypointControl1.Waypoint.TimeMark =ReaderSettings.SelectedDate;
            }

            labelDuration.ResetFormatted(0, 0, 0);

            //ColumnSpecies.ValueType = typeof(string);
            ColumnQuantity.ValueType = typeof(int);
            ColumnMass.ValueType = typeof(double);

            Logger.Provider.RecentListCount =ReaderSettings.RecentSpeciesCount;
            Logger.Provider.IndexPath =ReaderSettings.TaxonomicIndexPath;

            ColumnQuantity.ReadOnly =ReaderSettings.FixTotals;
            ColumnMass.ReadOnly =ReaderSettings.FixTotals;

            tabPageEnvironment.Parent = null;

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);
            AllowEffortCalculation = true;

            MenuStrip.SetMenuIcons();

            //menuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            //menuItemSpeciesRef.Enabled =ReaderSettings.TaxonomicIndexPath != null;

            Logger.UpdateStatus();

            IsChanged = false;
        }



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

            Data = new Data(ReaderSettings.TaxonomicIndex, ReaderSettings.SamplersIndex);
        }

        private void Write(string filename)
        {
            if (ReaderSettings.SpeciesAutoExpand)
            {
                Logger.Provider.UpdateIndex(Data.GetSpeciesKey(),ReaderSettings.SpeciesAutoExpandVisual);
            }

            switch (Path.GetExtension(filename))
            {
                case ".fcd":
                    Data.WriteToFile(filename);
                    break;

                case ".html":
                    Data.GetReport().WriteToFile(filename);
                    break;
            }

            statusCard.Message(Wild.Resources.Interface.Messages.Saved);
            FileName = filename;
            IsChanged = false;

            SaveGear();
        }

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

            Logger.SaveLog();

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
               ReaderSettings.SelectedWaterID = 0;
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
                   ReaderSettings.SelectedWaterID = waterSelector.WaterObject.ID;
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
           ReaderSettings.SelectedWaterID = waterSelector.WaterObject.ID;
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
                SelectedSampler = Data.Solitary.SamplerRow;
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

            if (this.Height == this.MinimumSize.Height && !Data.Solitary.IsCommentsNull())
            {
                this.Height += 100;
            }

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
                    ToolStripMenuItemSave_Click(menuItemSave, new EventArgs());
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

        private void SaveGear()
        {
            if (SelectedSampler == null) return;

            Equipment.UnitsRow unitRow =ReaderSettings.Equipment.Units.NewUnitsRow();
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

            if (ReaderSettings.Equipment.Units.FindDuplicate(unitRow) == null)
            {
               ReaderSettings.Equipment.Units.AddUnitsRow(unitRow);
                Service.SaveEquipment();
                LoadEquipment();
            }
        }

        private void LoadEquipment()
        {
            contextGear.Items.Clear();

            foreach (Equipment.UnitsRow unitRow in UserSettings.Equipment.Units)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = unitRow.ToString();
                item.Click += (o, e) => {
                    comboBoxSampler.SelectedItem =ReaderSettings.SamplersIndex.Sampler.FindByID(unitRow.SamplerID);
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

        private void SetEndpoint(Waypoint waypoint)
        {
            waypointControl1.Waypoint.Latitude = waypoint.Latitude;
            waypointControl1.Waypoint.Longitude = waypoint.Longitude;

            if (!waypoint.IsTimeMarkNull)
            {
                if (Data.Solitary.SamplerRow.IsPassive() && taskDialogLocationHandle.ShowDialog(this) == tdbSinking)
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

        //private void HandlePolygon(Polygon polygon)
        //{
        //    PreciseAreaMode = true;
        //    textBoxExactArea.Text = (polygon.Area / 10000d).ToString(Textual.Mask(4));
        //    SetEndpoint(polygon.Points.Last());
        //}
        //
        //public void OpenTrophics(string species, string regID)
        //{
        //    SpeciesToOpen = species;
        //    Load += new EventHandler(cardOpenSpecies_Load);
        //}




        private void Card_Load(object sender, EventArgs e)
        {        }

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

                Data = new Data(ReaderSettings.SpeciesIndex,ReaderSettings.SamplersIndex);

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

                Data = new Data(ReaderSettings.SpeciesIndex,ReaderSettings.SamplersIndex);

                tabControl.SelectedTab = tabPageCollect;
                textBoxLabel.Focus();

                IsChanged = false;
            }
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
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

           ReaderSettings.Interface.ExportDialog.FileName =
                IO.SuggestName(IO.FolderName(ReaderSettings.Interface.SaveDialog.FileName),
                Data.Solitary.GetSuggestedName());

            if (ReaderSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK)
            {
                Write(ReaderSettings.Interface.ExportDialog.FileName);
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
            int currentRecentCount =ReaderSettings.RecentSpeciesCount;

            Settings settings = new Settings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                menuItemWatersRef.Enabled = File.Exists(Wild.UserSettings.WatersIndexPath);
                menuItemSpeciesRef.Enabled = File.Exists(ReaderSettings.TaxonomicIndexPath);

                if (currentWaters != Wild.UserSettings.WatersIndexPath)
                {
                    Wild.UserSettings.WatersIndex = null;
                    waterSelector.Index = Wild.UserSettings.WatersIndex;
                }

                if (currentSpc !=ReaderSettings.TaxonomicIndexPath)
                {
                   ReaderSettings.SpeciesIndex = null;
                    Logger.Provider.IndexPath =ReaderSettings.TaxonomicIndexPath;
                }

                if (currentSpc !=ReaderSettings.TaxonomicIndexPath ||
                    currentRecentCount !=ReaderSettings.RecentSpeciesCount)
                {
                    Logger.Provider.RecentListCount =ReaderSettings.RecentSpeciesCount;
                    Logger.UpdateRecent();
                }

                //if (writeMode != Mayfly.UserSettings.FormatLocation)
                //{
                //    waypointControl1.ResetAppearance(Mayfly.UserSettings.FormatLocation);
                //}

                ColumnQuantity.ReadOnly =ReaderSettings.FixTotals;
                ColumnMass.ReadOnly =ReaderSettings.FixTotals;

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
                textBoxExactArea.Text = (poly.Area / 10000d).ToString("N4");
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

                    textBoxExposure.Text = Track.TotalLength(tracks).ToString("N1");
                    if (SelectedSampler.EffortFormula.Contains("T")) dateTimePickerStart.Value = tracks[0].Points[0].TimeMark;
                    if (SelectedSampler.EffortFormula.Contains("V")) textBoxVelocity.Text = Track.AverageKmph(tracks).ToString("N3");
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
                    textBoxExactArea.Text = (s / 10000d).ToString("N4");
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
                textBoxOpening.NotifyInstantly(Resources.Interface.Messages.FixOpening,
                    textBoxOpening.Text, textBoxLength.Text);
            }
            else
            {
                textBoxOpening.NotifyInstantly(Resources.Interface.Messages.FixEmptyOpening);
            }
        }

        private void pictureBoxWarnOpening_MouseLeave(object sender, EventArgs e)
        {
            //toolTipAttention.Hide(textBoxOpening);
        }

        private void pictureBoxWarnOpening_DoubleClick(object sender, EventArgs e)
        {
            textBoxOpening.Text = (Convert.ToDouble(textBoxLength.Text) *
                Service.DefaultOpening(SelectedSampler.ID)).ToString("N0");
        }


        private void pictureBoxWarnExposure_MouseHover(object sender, EventArgs e)
        {
            textBoxExposure.NotifyInstantly(Resources.Interface.Messages.FixSpread,
                textBoxExposure.Text, textBoxLength.Text);
        }

        private void pictureBoxWarnExposure_MouseLeave(object sender, EventArgs e)
        {
            //toolTipAttention.Hide(textBoxExposure);
        }

        private void pictureBoxWarnExposure_DoubleClick(object sender, EventArgs e)
        {
            textBoxExposure.Text = Math.Ceiling(2 * Convert.ToDouble(textBoxLength.Text) / Math.PI).ToString("N0");
        }

        #endregion


        private void weatherControl1_Changed(object sender, EventArgs e)
        {
            IsChanged = true;
        }

        private void logger_IndividualsRequired(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows)
            {
                OpenIndividuals(Logger.SaveLogRow(gridRow));
            }
        }



        private void spreadSheetAddt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
            HandleFactorRow(spreadSheetAddt.Rows[e.RowIndex]);
        }
    }
}