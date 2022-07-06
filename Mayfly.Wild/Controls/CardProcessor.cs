using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly;
using Mayfly.Extensions;
using System.Windows.Forms;
using Mayfly.Species;
using Mayfly.Controls;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using Mayfly.Extensions;

namespace Mayfly.Wild.Controls
{
    public partial class CardProcessor : Component
    {
        private string filename;
        private MenuStrip menu;
        private EventHandler changed;



        public string FileName
        {
            set
            {
                (Container as Form).ResetText(value ?? IO.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
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

        [Category("Behavior")]
        public MenuStrip MainMenu 
        {
            get { return menu; }
            set
            {
                menu = value;
            }
        }

        [Category("Behavior")]
        public Status Status { get; set; }

        [Category("Mayfly Events")]
        public event EventHandler Changed 
        {
            add
            {
                changed += value;
            }

            remove
            {
                changed -= value;
            }
        }



        public CardProcessor()
        {
            InitializeComponent();
        }

        public CardProcessor(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
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

            Data = new Data(UserSettings.SpeciesIndex, UserSettings.SamplersIndex);
        }

        private void Write(string filename)
        {
            if (UserSettings.SpeciesAutoExpand) // If it is set to automatically expand global index
            {
                Logger.Provider.UpdateIndex(Data.GetSpeciesKey(), UserSettings.SpeciesAutoExpandVisual);
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
    }
}
