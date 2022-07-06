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
        protected delegate void DataSaver();
        protected DataSaver saveData;


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

        public Data.CardRow cardRow { get; set; }

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

        public ReaderUserSettings UserSettings;



        public Card()
        {
            InitializeComponent();

            Data = new Data(UserSettings.TaxonomicIndex, UserSettings.SamplersIndex);
            Logger.Data = Data;
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

            tabPageEnvironment.Parent = null;

            tabPageFactors.Parent = null;
            spreadSheetAddt.StringVariants = Wild.UserSettings.AddtFactors;
            ColumnAddtFactor.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);

            MenuStrip.SetMenuIcons();

            ToolStripMenuItemWatersRef.Enabled = Wild.UserSettings.WatersIndexPath != null;
            ToolStripMenuItemSpeciesRef.Enabled = UserSettings.TaxonomicIndexPath != null;

            Logger.UpdateStatus();

            IsChanged = false;
        }



        private void Clear()
        {
            FileName = null;
            Data = null;

            textBoxLabel.Text = string.Empty;
            waterSelector.WaterObject = null;
            waypointControl1.Clear();
            textBoxComments.Text = string.Empty;
            aquaControl1.Clear();
            weatherControl1.Clear();
            spreadSheetLog.Rows.Clear();
            spreadSheetAddt.Rows.Clear();
        }

        private void Write(string filename)
        {
            if (UserSettings.SpeciesAutoExpand) // If it is set to automatically expand global index
            {
                Logger.Provider.UpdateIndex(Data.GetSpeciesKey(), UserSettings.SpeciesAutoExpandVisual);
            }

            string ext = Path.GetExtension(filename);

            if (ext == UserSettings.Interface.Extension)
            {
                Data.WriteToFile(filename);
            }
            else if (ext == ".html")
            {
                Data.GetReport().WriteToFile(filename);
            }

            statusCard.Message(Resources.Interface.Messages.Saved);
            FileName = filename;
            IsChanged = false;
        }



        private void saveCollect()
        {
            if (textBoxLabel.Text.IsAcceptable())
            {
                Data.Solitary.Label = textBoxLabel.Text;
            }
            else
            {
                Data.Solitary.SetLabelNull();
            }

            if (waterSelector.IsWaterSelected)
            {
                if (Data.Solitary.WaterID == waterSelector.WaterObject.ID)
                {                }
                else
                {
                    if (Data.Solitary.IsWaterIDNull())
                    {


                    Data.WaterRow newWaterRow = Data.Water.NewWaterRow();
                    newWaterRow.ID = waterSelector.WaterObject.ID;
                    newWaterRow.Type = waterSelector.WaterObject.Type;
                    if (!waterSelector.WaterObject.IsWaterNull())
                    {
                        newWaterRow.Water = waterSelector.WaterObject.Water;
                    }

                    Data.Water.AddWaterRow(newWaterRow);
                    Data.Solitary.WaterRow = newWaterRow;
                    }
                }
                
                UserSettings.SelectedWaterID = waterSelector.WaterObject.ID;
            }
            else
            {
                UserSettings.SelectedWaterID = 0;
                Data.Solitary.SetWaterIDNull();
                Data.Water.Clear();
            }



            waypointControl1.Save();
            if (!waypointControl1.Waypoint.IsEmpty)
                Data.Solitary.Where = waypointControl1.Waypoint.Protocol;


            if (textBoxComments.Text.IsAcceptable())
            {
                Data.Solitary.Comments = textBoxComments.Text.Trim();
            }
            else
            {
                Data.Solitary.SetCommentsNull();
            }
        }

        private void saveSampler()
        {
            if (SelectedSampler == null) {
                cardRow.SetSamplerNull();
            } else {
                cardRow.Sampler = SelectedSampler.ID;
            }
        }

        private void saveEnvironment()
        {
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

        }

        private void saveLog()
        {
            Logger.SaveLog();
        }

        private void saveAddt()
        {
            Data.FactorValue.Clear();
            Data.Factor.Clear();

            foreach (DataGridViewRow gridRow in spreadSheetAddt.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnAddtFactor.Index].Value == null) continue;

                if (gridRow.Cells[ColumnAddtValue.Index].Value == null) continue;

                string factorName = (string)gridRow.Cells[ColumnAddtFactor.Index].Value;

                double factorValue = (double)gridRow.Cells[ColumnAddtValue.Index].Value;

                Data.FactorValue.AddFactorValueRow(cardRow, Data.Factor.AddFactorRow(factorName), factorValue);

                if (!Wild.UserSettings.AddtFactors.Contains(factorName))
                {
                    List<string> factors = new List<string>();
                    factors.AddRange(Wild.UserSettings.AddtFactors);
                    factors.Add(factorName);
                    Wild.UserSettings.AddtFactors = factors.ToArray();
                }
            }
        }



        private void SaveData()
        {
            Data.CardRow cardRow = SaveCardRow();

            if (saveData != null) saveData.Invoke();

            Data.ClearUseless();
        }

        public void LoadData(string filename)
        {
            Clear();
            Data = new Data(UserSettings.TaxonomicIndex, UserSettings.SamplersIndex);
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

            #endregion

            #region Location

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



        public Data.CardRow SaveCardRow()
        {
            if (FileName == null) // If file is saving at the fisrt time
            {
                Data.Solitary.When = waypointControl1.Waypoint.TimeMark;
                Data.Solitary.AttachSign();
            }
            else // If file is resaving
            {
                Data.Solitary.RenewSign(waypointControl1.Waypoint.TimeMark);
            }

            return Data.Solitary;
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
            IO.RunFile(UserSettings.TaxonomicIndexPath, "-edit");
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            string currentWaters = Wild.UserSettings.WatersIndexPath;
            string currentSpc = UserSettings.TaxonomicIndexPath;
            int currentRecentCount = UserSettings.RecentSpeciesCount;

            Settings settings = new Settings();
            if (settings.ShowDialog() == DialogResult.OK)
            {
                if (currentWaters != Wild.UserSettings.WatersIndexPath)
                {
                    Wild.UserSettings.WatersIndex = null;
                    waterSelector.Index = Wild.UserSettings.WatersIndex;
                }

                if (currentSpc != UserSettings.TaxonomicIndexPath)
                {
                    UserSettings.TaxonomicIndex = null;
                    Logger.Provider.IndexPath = UserSettings.TaxonomicIndexPath;
                }

                if (currentSpc != UserSettings.TaxonomicIndexPath ||
                    currentRecentCount != UserSettings.RecentSpeciesCount)
                {
                    Logger.Provider.RecentListCount = UserSettings.RecentSpeciesCount;
                    Logger.UpdateRecent();
                }
            }

            ColumnQuantity.ReadOnly = UserSettings.FixTotals;
            ColumnMass.ReadOnly = UserSettings.FixTotals;
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
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

        private void spreadSheetAddt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
            HandleFactorRow(spreadSheetAddt.Rows[e.RowIndex]);
        }
    }
}