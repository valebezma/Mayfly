using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Wild;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Mayfly.Benthos;

namespace Mayfly.Fish
{
    public partial class Individual : Form
    {
        #region Properties



        public Data.IndividualRow IndividualRow;

        private Data Data;

        //public DataGridViewRow LogLine;

        private Data.IntestineRow intestineRow;

        private Data.IntestineRow IntestineRow
        {
            get
            {
                return intestineRow;
            }

            set
            {
                intestineRow = value;
            }
        }

        public GutSection SelectedSection = GutSection.WholeIntestine;

        

        private Data.OrganRow organRow;

        private Data.OrganRow OrganRow
        {
            get
            {
                return organRow;
            }

            set
            {
                organRow = value;
            }
        }

        public int SelectedOrgan = -1;



        public bool IsChanged { get; set; }



        private Data Consumed
        {
            get;
            set;
        }

        public double ConsumedDetailedMass
        {
            get
            {
                double result = 0;

                foreach (Data.IntestineRow intestineRow in IndividualRow.GetIntestineRows())
                {
                    if (intestineRow == IntestineRow)
                    {
                        continue;
                    }

                    if (!intestineRow.IsConsumedNull())
                    {
                        Data consumed = new Data();
                        consumed.ReadXml(new StringReader(intestineRow.Consumed));
                        result += consumed.Solitary.Mass;
                    }
                }

                return result + ConsumedDetailedMassCurrent;
            }
        }

        public double ConsumedDetailedMassCurrent
        {
            get
            {
                double result = 0;
                foreach (DataGridViewRow CurrentRecord in spreadSheetTrophics.Rows)
                {
                    if (CurrentRecord.Cells[ColumnTrpMass.Name].Value != null)
                    {
                        result += (double)CurrentRecord.Cells[ColumnTrpMass.Name].Value;
                    }
                }
                return result;
            }
        }



        private Data Infection
        {
            get;
            set;
        }



        public EventHandler ValueChanged
        {
            get;
            set;
        }

        #endregion



        public Individual()
        {
            InitializeComponent();
            Log.Write("Open individual fish profile.");

            Consumed = new Data();
            ColumnTrpSpecies.ValueType = typeof(string);
            ColumnTrpQuantity.ValueType = typeof(int);
            ColumnTrpMass.ValueType = typeof(double);

            speciesParasites.IndexPath = UserSettings.ParasitesIndexPath;
            comboBoxSection.SelectedIndex = (int)SelectedSection;

            ColumnInfSpecies.ValueType = typeof(string);
            ColumnInfQuantity.ValueType = typeof(int);
            ColumnInfComments.ValueType = typeof(string);

            speciesTrophics.IndexPath = UserSettings.DietIndexPath;

            comboBoxOrgan.SelectedIndex = -1;

            ColumnAddtVariable.ValueType = typeof(string);
            ColumnAddtValue.ValueType = typeof(double);
        }

        public Individual(Data.IndividualRow individualRow) 
            : this()
        {
            IndividualRow = individualRow;
            
            IndividualRow.AcceptChanges();
            Data = (Data)individualRow.Table.DataSet;

            Text = string.Format(Wild.Resources.Interface.Interface.IndWindow, individualRow.Species);

            LoadData();
        }



        #region Methods

        public void ShowFecundity()
        {
            tabControlInd.SelectedTab = tabPageFecundity;
        }

        public void ShowGrowth()
        {
            tabControlInd.SelectedTab = tabPageGrowth;
        }

        public void ShowTrophics()
        {
            tabControlInd.SelectedTab = tabPageTrophics;
            spreadSheetTrophics.Focus();
        }

        public void ShowParasites()
        {
            tabControlInd.SelectedTab = tabPageParasites;
            spreadSheetInfection.Focus();
        }



        private void LoadData()
        {
            #region General

            if (IndividualRow.IsLengthNull()) textBoxLength.Text = string.Empty;
            else textBoxLength.Text = IndividualRow.Length.ToString();

            double tl = IndividualRow.GetTotalLength();
            if (double.IsNaN(tl)) textBoxTL.Text = string.Empty;
            else textBoxTL.Text = tl.ToString();

            if (IndividualRow.IsMassNull()) textBoxMass.Text = string.Empty;
            else textBoxMass.Text = IndividualRow.Mass.ToString();

            if (IndividualRow.IsSomaticMassNull()) textBoxSomaticMass.Text = string.Empty;
            else textBoxSomaticMass.Text = IndividualRow.SomaticMass.ToString();

            if (IndividualRow.IsAgeNull()) textBoxAge.Text = string.Empty;
            else textBoxAge.Text = new Age(IndividualRow.Age).ToString();

            if (IndividualRow.IsRegIDNull()) textBoxRegID.Text = string.Empty;
            else textBoxRegID.Text = IndividualRow.RegID.ToString();

            if (IndividualRow.IsCommentsNull()) textBoxComments.Text = string.Empty;
            else textBoxComments.Text = IndividualRow.Comments;

            #endregion

            #region Fecundity

            if (IndividualRow.IsSexNull()) comboBoxGender.SelectedIndex = -1;
            else comboBoxGender.SelectedIndex = new Sex(IndividualRow.Sex).Value;

            if (IndividualRow.IsMaturityNull()) comboBoxMaturity.SelectedIndex = -1;
            else comboBoxMaturity.SelectedIndex = IndividualRow.Maturity - 1;

            if (IndividualRow.IsIntermatureNull()) checkBoxIntermediate.CheckState = CheckState.Indeterminate;
            else checkBoxIntermediate.CheckState = IndividualRow.Intermature ? CheckState.Checked : CheckState.Unchecked;

            if (IndividualRow.IsGonadMassNull()) textBoxGonadMass.Text = string.Empty;
            else textBoxGonadMass.Text = IndividualRow.GonadMass.ToString();

            if (IndividualRow.IsGonadSampleMassNull()) textBoxGonadSampleMass.Text = string.Empty;
            else textBoxGonadSampleMass.Text = IndividualRow.GonadSampleMass.ToString();

            if (IndividualRow.IsGonadSampleNull()) textBoxGonadSample.Text = string.Empty;
            else textBoxGonadSample.Text = IndividualRow.GonadSample.ToString();

            if (IndividualRow.IsEggSizeNull()) textBoxEggSize.Text = string.Empty;
            else textBoxEggSize.Text = IndividualRow.EggSize.ToString();

            #endregion

            #region Trophics

            if (IndividualRow.IsFatnessNull()) comboBoxFatness.SelectedIndex = -1;
            else comboBoxFatness.SelectedIndex = IndividualRow.Fatness;

            if (IndividualRow.IsConsumedMassNull()) textBoxConsumedMass.Text = string.Empty;
            else textBoxConsumedMass.Text = IndividualRow.ConsumedMass.ToString();

            Data.IntestineRow[] Guts = IndividualRow.GetIntestineRows();

            if (Guts.Length == 0)
            {
                ClearIntestine();
            }
            else
            {
                if (comboBoxSection.SelectedIndex == Guts[0].Section) {
                    comboBoxSection_ValueChanged(comboBoxSection, new EventArgs());
                }
                else {
                    comboBoxSection.SelectedIndex = Guts[0].Section;
                }
            }

            #endregion

            #region Parasite

            Data.OrganRow[] Organs = IndividualRow.GetOrganRows();
            if (Organs.Length > 0)
            {
                comboBoxOrgan.SelectedIndex = Organs[0].Organ;
            }

            #endregion

            spreadSheetAddt.Rows.Clear();

            foreach (Data.ValueRow valueRow in IndividualRow.GetValueRows())
            {
                LoadAddtValue(valueRow.VariableRow.Variable, valueRow.Value);
            }
        }

        private void SaveData()
        {
            #region General            

            if (textBoxTL.Text.IsDoubleConvertible()) IndividualRow.SetAddtValue("TL", Convert.ToDouble(textBoxTL.Text));
            else IndividualRow.SetAddtValue("TL", double.NaN);

            if (textBoxLength.Text.IsDoubleConvertible()) IndividualRow.Length = Convert.ToDouble(textBoxLength.Text);
            else IndividualRow.SetLengthNull();

            if (textBoxMass.Text.IsDoubleConvertible()) IndividualRow.Mass = Convert.ToDouble(textBoxMass.Text);
            else IndividualRow.SetMassNull();

            if (textBoxSomaticMass.Text.IsDoubleConvertible()) IndividualRow.SomaticMass = Convert.ToDouble(textBoxSomaticMass.Text);
            else IndividualRow.SetSomaticMassNull();

            if (textBoxAge.Text.IsAcceptable()) IndividualRow.Age = new Age(textBoxAge.Text).Value;
            else IndividualRow.SetAgeNull();

            if (textBoxRegID.Text.IsAcceptable()) IndividualRow.RegID = textBoxRegID.Text;
            else IndividualRow.SetRegIDNull();

            if (textBoxComments.Text.IsAcceptable()) IndividualRow.Comments = textBoxComments.Text;
            else IndividualRow.SetCommentsNull();

            #endregion

            #region Fecundity

            if (comboBoxGender.SelectedIndex == -1) IndividualRow.SetSexNull();
            else IndividualRow.Sex = comboBoxGender.SelectedIndex;

            if (comboBoxMaturity.SelectedIndex == -1) IndividualRow.SetMaturityNull();
            else IndividualRow.Maturity = comboBoxMaturity.SelectedIndex + 1;

            if (checkBoxIntermediate.CheckState == CheckState.Indeterminate) IndividualRow.SetIntermatureNull();
            else IndividualRow.Intermature = checkBoxIntermediate.Checked;

            if (textBoxGonadMass.Text.IsDoubleConvertible()) IndividualRow.GonadMass = Convert.ToDouble(textBoxGonadMass.Text);
            else IndividualRow.SetGonadMassNull();

            if (textBoxGonadSampleMass.Text.IsDoubleConvertible()) IndividualRow.GonadSampleMass = Convert.ToDouble(textBoxGonadSampleMass.Text);
            else IndividualRow.SetGonadSampleMassNull();

            if (textBoxGonadSample.Text.IsDoubleConvertible()) IndividualRow.GonadSample = Convert.ToInt32(textBoxGonadSample.Text);
            else IndividualRow.SetGonadSampleNull();

            if (textBoxEggSize.Text.IsDoubleConvertible()) IndividualRow.EggSize = Convert.ToDouble(textBoxEggSize.Text);
            else IndividualRow.SetEggSizeNull();

            #endregion

            #region Trophics

            if (comboBoxFatness.SelectedIndex == -1) IndividualRow.SetFatnessNull();
            else IndividualRow.Fatness = comboBoxFatness.SelectedIndex;

            if (textBoxConsumedMass.Text.IsDoubleConvertible()) IndividualRow.ConsumedMass = Convert.ToDouble(textBoxConsumedMass.Text);
            else IndividualRow.SetConsumedMassNull();

            SaveIntestine();

            #endregion

            #region Organ

            SaveOrgan();

            #endregion

            #region Save Addts

            foreach (DataGridViewRow gridRow in spreadSheetAddt.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnAddtVariable.Index].Value == null) continue;

                string variableName = (string)gridRow.Cells[ColumnAddtVariable.Index].Value;

                if (!variableName.IsAcceptable()) continue;

                if (gridRow.Cells[ColumnAddtValue.Index].Value == null)
                {
                    IndividualRow.SetAddtValueNull(variableName);
                }
                else
                {
                    IndividualRow.SetAddtValue(variableName, (double)gridRow.Cells[ColumnAddtValue.Index].Value);
                }
            }

            #endregion

            if (ValueChanged != null)
            {
                ValueChanged.Invoke(this, new EventArgs());
            }
        }



        private void UpdateCondition()
        {
            textBoxCondition.Text = IndividualRow.GetCondition().ToString("N3");
            textBoxConditionSoma.Text = IndividualRow.GetConditionSomatic().ToString("N3");
        }

        private void UpdateFecundityTab()
        {
            textBoxMeanEggMass.Text = IndividualRow.GetAveEggMass().ToString("N3");

            textBoxGonadIndex.Text = IndividualRow.GetGonadIndex().ToString("P2");
            textBoxGonadIndexSoma.Text = IndividualRow.GetGonadIndexSomatic().ToString("P2");

            textBoxRelativeFecundity.Text = IndividualRow.GetRelativeFecundity().ToString("N2");

            textBoxRelativeFecunditySoma.Text = IndividualRow.GetRelativeFecunditySomatic().ToString("N2");
            textBoxAbsoluteFecundity.Text = IndividualRow.GetAbsoluteFecundity().ToString("N0");
        }

        private void UpdateTrophicsTab()
        {
            double w = IndividualRow.Mass;
            double wc = ConsumedDetailedMass;

            if (!textBoxConsumedMass.Text.IsDoubleConvertible() || Convert.ToDouble(textBoxConsumedMass.Text) < wc)
            {
                textBoxConsumedMass.Text = wc.ToString();
            }

            bool allset = true;

            foreach (DataGridViewRow gridRow in spreadSheetTrophics.Rows)
            {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnTrpMass.Index].Value == null)
                {
                    gridRow.Cells[ColumnTrpConsumption.Index].Value = null;
                    allset = false;
                }
                else
                {
                    double wi = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                    gridRow.Cells[ColumnTrpConsumption.Index].Value = (wi / w * 10);
                }
            }

            textBoxConsumedMass.ReadOnly = allset;
        }

        private void SwitchGonads(bool isMatured)
        {
            labelFecundity.Enabled =

            labelGonadMass.Enabled = 
            labelGonadosomatic.Enabled = 

            textBoxGonadMass.Enabled = 
            textBoxGonadIndex.Enabled =
            textBoxGonadIndexSoma.Enabled =
            textBoxRelativeFecundity.Enabled = 
            textBoxRelativeFecunditySoma.Enabled =

            isMatured;
        }

        private void SwitchFecundity(bool isFemale)
        {
            textBoxGonadSample.Enabled =
            textBoxGonadSampleMass.Enabled = 
            textBoxAbsoluteFecundity.Enabled =
            textBoxRelativeFecundity.Enabled =
            labelGonadSample.Enabled = 
            labelGonadSampleMass.Enabled = 
            labelFecundityAbs.Enabled = 
            labelFecundityRel.Enabled =

            textBoxEggSize.Enabled =
            textBoxMeanEggMass.Enabled =
            labelEggSize.Enabled = labelEggMass.Enabled =
            labelEgg.Enabled = 
            
            isFemale;
        }



        #region Trophics

        private void ClearIntestine()
        {
            comboBoxFullness.SelectedIndex = -1;
            comboBoxFermentation.SelectedIndex = -1;
            Consumed = new Data();
            spreadSheetTrophics.Rows.Clear();
        }

        private void ClearDietLogRow(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[ColumnTrpID.Index].Value == null) return;

            Data.LogRow logRow = Consumed.Log.FindByID((int)gridRow.Cells[ColumnTrpID.Index].Value);
            Data.SpeciesRow spcRowInvariant = logRow.SpeciesRow;
            logRow.Delete();
            spcRowInvariant.Delete();
        }

        private void ClearDietCellContent()
        {
            foreach (DataGridViewCell gridCell in spreadSheetTrophics.SelectedCells)
            {
                if (gridCell.OwningColumn == ColumnTrpQuantity)
                {
                    int q = SaveLogRow(gridCell.OwningRow).DetailedQuantity;

                    if (q > 0) {
                        System.Media.SystemSounds.Beep.Play();
                    } else {
                        gridCell.Value = null;
                    }
                }

                if (gridCell.OwningColumn == ColumnTrpSpecies) {

                    gridCell.Value = null;
                }

                if (gridCell.RowIndex != -1) {

                    HandleTrophicsRow(spreadSheetTrophics.Rows[gridCell.RowIndex]);
                }
            }
        }

        private void SaveIntestine()
        {
            if (comboBoxFullness.SelectedIndex > -1 ||
                comboBoxFermentation.SelectedIndex > -1 ||
                spreadSheetTrophics.RowCount > 1)
            {
                if (IntestineRow == null)
                {
                    IntestineRow = Data.Intestine.NewIntestineRow();
                    IntestineRow.IndividualRow = IndividualRow;
                    IntestineRow.Section = (int)SelectedSection;
                    Data.Intestine.AddIntestineRow(IntestineRow);
                }

                if (comboBoxFullness.SelectedIndex == -1) IntestineRow.SetFullnessNull();
                else IntestineRow.Fullness = comboBoxFullness.SelectedIndex;

                if (comboBoxFermentation.SelectedIndex == -1) IntestineRow.SetFermentationNull();
                else IntestineRow.Fermentation = comboBoxFermentation.SelectedIndex;

                if (spreadSheetTrophics.RowCount == (spreadSheetTrophics.AllowUserToAddRows ? 1 : 0))
                {
                    IntestineRow.SetConsumedNull();
                }
                else
                {
                    SaveConsumed();
                }
            }
            else if (IntestineRow != null)
            {
                Data.Intestine.RemoveIntestineRow(IntestineRow);
            }

            UpdateTrophicIndex();
        }

        private void SaveConsumed()
        {
            // Save consumed benthos
            foreach (DataGridViewRow gridRow in spreadSheetTrophics.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SaveLogRow(gridRow);
            }

            if (Consumed.Log.Count == 0) { IntestineRow.SetConsumedNull(); }
            else { Consumed.ClearUseless(); IntestineRow.Consumed = Consumed.GetXml().Replace(Environment.NewLine, " "); }
        }

        private void LoadIntestine(Data.IntestineRow intestineRow)
        {
            if (intestineRow.IsFullnessNull())
            {
                comboBoxFullness.SelectedIndex = -1;
            }
            else
            {
                comboBoxFullness.SelectedIndex = intestineRow.Fullness;
            }

            if (intestineRow.IsFermentationNull())
            {
                comboBoxFermentation.SelectedIndex = -1;
            }
            else
            {
                comboBoxFermentation.SelectedIndex = intestineRow.Fermentation;
            }
            
            spreadSheetTrophics.Rows.Clear();

            if (intestineRow.IsConsumedNull())
            {
                Consumed = new Data();
            }
            else
            {
                Consumed = intestineRow.GetConsumed();

                foreach (Data.LogRow logRow in Consumed.Log)
                {
                    InsertLogRow(logRow);
                }
            }
        }

        private void InsertLogRow(Data.LogRow logRow)
        {
            List<object> LogRowItems = new List<object>(4)
            {
                logRow.ID,
                logRow.SpeciesRow.Species
            };

            if (logRow.IsQuantityNull())
            {
                LogRowItems.Add(null);
            }
            else
            {
                LogRowItems.Add(logRow.Quantity);
            }

            if (logRow.IsMassNull())
            {
                LogRowItems.Add(null);
                LogRowItems.Add(null);
            }
            else
            {
                LogRowItems.Add(logRow.Mass);
                LogRowItems.Add(IndividualRow.IsMassNull() ? double.NaN : logRow.Mass / IndividualRow.Mass * 10);
            }

            //GridTrophics.Rows.Insert(RowIndex, LogRowItems.ToArray());
            int rowIndex = spreadSheetTrophics.Rows.Add(LogRowItems.ToArray());
            HandleTrophicsRow(spreadSheetTrophics.Rows[rowIndex]);

            if (!logRow.IsCommentsNull())
            {
                spreadSheetTrophics[ColumnTrpSpecies.Name, rowIndex].ToolTipText = logRow.Comments;
            }
        }

        private void HandleTrophicsRow(DataGridViewRow gridRow)
        {
            // If it is new row - end of function.
            if (gridRow.IsNewRow)
            {
                return;
            }

            // If row is empty - delete the row and end the function
            if (Wild.Service.IsRowEmpty(gridRow))
            {
                spreadSheetTrophics.Rows.Remove(gridRow);
                return;
            }

            // If species is not set - light 'Not identified'
            if (gridRow.Cells[ColumnTrpSpecies.Index].Value == null)
            {
                gridRow.Cells[ColumnTrpSpecies.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }

            // Searching for the duplicates (on 'Species' column)

            for (int i = 0; i < spreadSheetTrophics.RowCount; i++)
            {
                DataGridViewRow trpGridRow = spreadSheetTrophics.Rows[i];

                if (trpGridRow.IsNewRow)
                {
                    continue;
                }

                if (trpGridRow == gridRow)
                {
                    continue;
                }

                if (object.Equals(gridRow.Cells[ColumnTrpSpecies.Index].Value, trpGridRow.Cells[ColumnTrpSpecies.Index].Value))
                {
                    int N = 0;
                    double B = 0;

                    if (gridRow.Cells[ColumnTrpQuantity.Index].Value != null)
                    {
                        N += (int)gridRow.Cells[ColumnTrpQuantity.Name].Value;
                    }

                    if (gridRow.Cells[ColumnTrpMass.Index].Value != null)
                    {
                        if (gridRow.Cells[ColumnTrpQuantity.Index].Value != null && (int)gridRow.Cells[ColumnTrpQuantity.Index].Value == 1)
                        {
                            Data.IndividualRow newIndividualRowFish = Consumed.Individual.NewIndividualRow();
                            newIndividualRowFish.LogRow = SaveLogRow(gridRow);
                            newIndividualRowFish.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                            Consumed.Individual.AddIndividualRow(newIndividualRowFish);
                        }

                        B += (double)gridRow.Cells[ColumnTrpMass.Name].Value;
                    }

                    if (trpGridRow.Cells[ColumnTrpQuantity.Index].Value != null)
                    {
                        N += (int)trpGridRow.Cells[ColumnTrpQuantity.Name].Value;
                    }

                    if (trpGridRow.Cells[ColumnTrpMass.Index].Value != null)
                    {
                        if (trpGridRow.Cells[ColumnTrpQuantity.Index].Value != null &&
                            (int)trpGridRow.Cells[ColumnTrpQuantity.Index].Value == 1)
                        {
                            Data.IndividualRow newIndividualRowFish = Consumed.Individual.NewIndividualRow();
                            newIndividualRowFish.LogRow = SaveLogRow(gridRow);
                            newIndividualRowFish.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                            newIndividualRowFish.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
                            Consumed.Individual.AddIndividualRow(newIndividualRowFish);
                        }

                        B += (double)trpGridRow.Cells[ColumnTrpMass.Name].Value;
                    }

                    if (N > 0)
                    {
                        gridRow.Cells[ColumnTrpQuantity.Index].Value = N;
                    }

                    if (B > 0)
                    {
                        gridRow.Cells[ColumnTrpMass.Index].Value = B;
                    }

                    spreadSheetTrophics.Rows.Remove(trpGridRow);
                    i--;
                }
            }
        }



        private Data.LogRow SaveLogRow(DataGridViewRow gridRow)
        {
            return SaveLogRow(Consumed, gridRow);
        }

        private Data.LogRow SaveLogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result;
            bool IsNew = false;

            if (gridRow.Cells[ColumnTrpID.Index].Value != null)
            {
                result = data.Log.FindByID((int)gridRow.Cells[ColumnTrpID.Index].Value);
                if (result != null)
                {
                    goto Saving;
                }
            }

            result = data.Log.NewLogRow();
            result.CardRow = data.Solitary;
            IsNew = true;

        Saving:

            SpeciesKey.SpeciesRow currentSpecies = speciesTrophics.Index.Species.FindBySpecies(
                gridRow.Cells[ColumnTrpSpecies.Index].Value.ToString());

            if (currentSpecies == null)
            {
                // There is no such species in reference
                if ((string)gridRow.Cells[ColumnTrpSpecies.Index].Value ==
                    Species.Resources.Interface.UnidentifiedTitle)
                {
                    result.SetSpcIDNull();
                }
                else
                {
                    result.SpeciesRow = data.Species.AddSpeciesRow(
                        gridRow.Cells[ColumnTrpSpecies.Index].Value.ToString());
                }
            }
            else
            {
                // There is such species in reference you using
                if (data.Species.FindBySpecies(currentSpecies.Name) == null)
                {
                    data.Species.AddSpeciesRow(currentSpecies.Name);
                }
                result.SpeciesRow = data.Species.FindBySpecies(currentSpecies.Name);
            }

            if (gridRow.Cells[ColumnTrpQuantity.Index].Value == null)
            {
                result.SetQuantityNull();
            }
            else
            {
                result.Quantity = (int)gridRow.Cells[ColumnTrpQuantity.Index].Value;
            }

            if (gridRow.Cells[ColumnTrpMass.Index].Value == null)
            {
                result.SetMassNull();
            }
            else
            {
                result.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
            }

            if (IsNew)
            {
                data.Log.AddLogRow(result);
                gridRow.Cells[ColumnTrpID.Index].Value = result.ID;
            }

            return result;
        }



        private void LoadTrophicData(string filename)
        {
            Data data = new Data();
            data.Read(filename);            

            if (spreadSheetTrophics.RowCount < 2) // If only newrow is in grid
            {
                // Load card as data
                if (IntestineRow == null)
                {
                    IntestineRow = Data.Intestine.NewIntestineRow();
                    IntestineRow.IndividualRow = IndividualRow;
                    IntestineRow.Section = (int)SelectedSection;
                    Data.Intestine.AddIntestineRow(IntestineRow);
                }

                IntestineRow.Consumed = data.GetXml();
                LoadIntestine(IntestineRow);
                UpdateTrophicsTab();
            }
            else
            {
                InsertTrophicLogRows(data, -1);
            }
        }

        private void InsertTrophicLogRows(Data data, int rowIndex)
        {
            if (rowIndex == -1)
            {
                rowIndex = spreadSheetTrophics.RowCount - 1;
            }

            foreach (Data.LogRow logRow in data.Log)
            {
                InsertLogRow(logRow);

                if (rowIndex < spreadSheetTrophics.RowCount - 1)
                {
                    rowIndex++;
                }
            }

            UpdateTrophicsTab();
        }

        private void UpdateTrophicIndex()
        {
            if (Consumed != null)
            {
                SpeciesKey localIndex = Consumed.GetSpeciesKey();
                speciesTrophics.UpdateIndex(localIndex, UserSettings.SpeciesAutoExpandVisual);
            }

        }

        #endregion



        #region Parasite

        private void ClearOrgan()
        {
            Infection = null;
            spreadSheetInfection.Rows.Clear();
        }

        private void SaveOrgan()
        {

            if (spreadSheetInfection.RowCount > 1)
            {
                if (OrganRow == null)
                {
                    organRow = Data.Organ.NewOrganRow();
                    organRow.IndividualRow = IndividualRow;
                    organRow.Organ = SelectedOrgan;
                    Data.Organ.AddOrganRow(organRow);
                }

                if (spreadSheetInfection.RowCount == (spreadSheetInfection.AllowUserToAddRows ? 1 : 0))
                {
                    OrganRow.SetInfectionNull();
                }
                else
                {
                    SaveInfection();
                }
            }
            else if (OrganRow != null)
            {
                Data.Organ.RemoveOrganRow(OrganRow);
            }

            UpdateParasiteIndex();
        }

        private void SaveInfection()
        {
            foreach (DataGridViewRow gridRow in spreadSheetInfection.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SaveParasitesLogRow(gridRow);
            }

            if (Infection.Log.Count == 0) { OrganRow.SetInfectionNull(); }
            else { Infection.ClearUseless(); OrganRow.Infection = Infection.GetXml(); }
        }

        private Data.LogRow SaveParasitesLogRow(DataGridViewRow gridRow)
        {
            return SaveLogRow(Infection, gridRow);
        }

        private Data.LogRow SaveParasitesLogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result;
            bool IsNew = false;

            if (gridRow.Cells[ColumnInfID.Index].Value != null)
            {
                result = data.Log.FindByID((int)gridRow.Cells[ColumnInfID.Index].Value);
                if (result != null)
                {
                    goto Saving;
                }
            }

            result = data.Log.NewLogRow();
            result.CardRow = data.Card[0];
            IsNew = true;

            Saving:

            SpeciesKey.SpeciesRow currentSpecies = speciesParasites.Index.Species.FindBySpecies(
                gridRow.Cells[ColumnInfSpecies.Index].Value.ToString());

            if (currentSpecies == null)
            {
                // There is no such species in reference
                if ((string)gridRow.Cells[ColumnInfSpecies.Index].Value ==
                    Species.Resources.Interface.UnidentifiedTitle)
                {
                    result.SetSpcIDNull();
                }
                else
                {
                    result.SpeciesRow = data.Species.AddSpeciesRow(
                        gridRow.Cells[ColumnInfSpecies.Index].Value.ToString());
                }
            }
            else
            {
                // There is such species in reference you using
                if (data.Species.FindBySpecies(currentSpecies.Name) == null)
                {
                    data.Species.AddSpeciesRow(currentSpecies.Name);
                }
                result.SpeciesRow = data.Species.FindBySpecies(currentSpecies.Name);
            }

            if (gridRow.Cells[ColumnInfQuantity.Index].Value == null)
            {
                result.SetQuantityNull();
            }
            else
            {
                result.Quantity = (int)gridRow.Cells[ColumnInfQuantity.Index].Value;
            }

            if (IsNew)
            {
                data.Log.AddLogRow(result);
                gridRow.Cells[ColumnInfID.Index].Value = result.ID;
            }

            return result;
        }

        //private Data.InfectionRow SaveParasites(DataGridViewRow gridRow, Data.OrganRow organRow)
        //{
        //    Data.InfectionRow result;

        //    // Set row
        //    if (gridRow.Cells[ColumnInfID.Index].Value == null)
        //    {
        //        result = Data.Infection.NewInfectionRow();
        //        result.OrganRow = organRow;
        //        Data.Infection.AddInfectionRow(result);
        //        gridRow.Cells[ColumnInfID.Name].Value = result.ID;
        //    }
        //    else
        //    {
        //        result = Data.Infection.FindByID((int)gridRow.Cells[ColumnInfID.Index].Value);
        //    }


        //    if (gridRow.Cells[ColumnInfSpecies.Index].Value == null)
        //    {
        //        result.SetSpcIDNull();
        //    }
        //    else
        //    {
        //        SpeciesKey.SpeciesRow CurrentParasite = UserSettings.ParasitesIndex.Species.FindBySpecies(
        //            gridRow.Cells[ColumnInfSpecies.Index].Value.ToString());

        //        if (CurrentParasite == null)
        //        {
        //            Data.ParasiteRow newParasiteRow =
        //                Data.Parasite.AddParasiteRow(
        //                gridRow.Cells[ColumnInfSpecies.Name].Value.ToString());

        //            result.SpcID = newParasiteRow.ID;
        //        }
        //        else
        //        {
        //            if (Data.Parasite.FindByID(CurrentParasite.ID) == null)
        //            {
        //                Data.Parasite.Rows.Add(CurrentParasite.ID, CurrentParasite.Species);
        //            }
        //            result.SpcID = CurrentParasite.ID;
        //        }
        //    }

        //    if (gridRow.Cells[ColumnInfQuantity.Index].Value == null) result.SetQuantityNull();
        //    else result.Quantity = (int)gridRow.Cells[ColumnInfQuantity.Index].Value;

        //    if (gridRow.Cells[ColumnInfComments.Index].Value == null) result.SetCommentsNull();
        //    else result.Comments = (string)gridRow.Cells[ColumnInfComments.Index].Value;

        //    if (result.IsSpcIDNull() && result.IsQuantityNull() && result.IsCommentsNull())
        //    {
        //        result.Delete();
        //        if (!gridRow.IsNewRow)
        //        {
        //            spreadSheetInfection.Rows.Remove(gridRow);
        //        }
        //        return null;
        //    }
        //    else return result;
        //}

        private void HandleParasiteRow(DataGridViewRow Row)
        {
            // If it is new row - end of function.
            if (Row.IsNewRow)
            {
                return;
            }

            // If row is empty - delete the row and end the function
            if (Wild.Service.IsRowEmpty(Row))
            {
                spreadSheetInfection.Rows.Remove(Row);
                return;
            }

            // If species is not set - light 'Not identified'
            if (Row.Cells[ColumnInfSpecies.Index].Value == null)
            {
                Row.Cells[ColumnInfSpecies.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }

            // Searching for the duplicates (on 'Parasite Species' column)

            for (int i = 0; i < spreadSheetInfection.RowCount; i++)
            {
                DataGridViewRow _Row = spreadSheetInfection.Rows[i];

                if (_Row.IsNewRow)
                {
                    continue;
                }

                if (_Row == Row)
                {
                    continue;
                }

                if (object.Equals(Row.Cells[ColumnInfSpecies.Index].Value, _Row.Cells[ColumnInfSpecies.Index].Value))
                {
                    int N = 0;

                    if (Row.Cells[ColumnInfQuantity.Index].Value != null)
                    {
                        N += (int)Row.Cells[ColumnInfQuantity.Name].Value;
                    }

                    if (_Row.Cells[ColumnInfQuantity.Index].Value != null)
                    {
                        N += (int)_Row.Cells[ColumnInfQuantity.Name].Value;
                    }

                    if (N > 0)
                    {
                        Row.Cells[ColumnInfQuantity.Index].Value = N;
                    }

                    spreadSheetInfection.Rows.Remove(_Row);
                    i--;
                }
            }
        }

        private void LoadOrgan(Data.OrganRow organRow)
        {
            spreadSheetInfection.Rows.Clear();

            if (organRow.IsInfectionNull())
            {
                Infection = null;
            }
            else
            {
                Infection = organRow.GetInfection();

                foreach (Data.LogRow logRow in Infection.Log)
                {
                    InsertLogRow(logRow);
                }
            }
        }

        private void UpdateParasiteIndex()
        {
            if (Infection != null)
            {
                SpeciesKey localIndex = Infection.GetSpeciesKey();
                speciesParasites.UpdateIndex(localIndex, UserSettings.SpeciesAutoExpandVisual);
            }

        }

        #endregion



        private void LoadAddtValue(string name, double value)
        {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetAddt);
            gridRow.Cells[ColumnAddtVariable.Index].Value = name;
            gridRow.Cells[ColumnAddtValue.Index].Value = value;
            spreadSheetAddt.Rows.Add(gridRow);
        }

        #endregion

        

        private void Individual_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSaveChanges.ShowDialog(this);

                if (b == tdbSave)
                {
                    SaveData();
                    IndividualRow.AcceptChanges();
                    IsChanged = false;
                    DialogResult = DialogResult.OK;
                }
                else if (b == tdbDiscard)
                {
                    IndividualRow.RejectChanges();
                    if (ValueChanged != null) { ValueChanged.Invoke(this, new EventArgs()); }
                    IsChanged = false;
                }
                else if (b == tdbCancel)
                {
                    e.Cancel = true;
                }
            }
        }



        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void intValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((Control)sender).HandleInput(e, typeof(int));
        }

        private void value_Changed(object sender, EventArgs e)
        {
            if (((Control)sender).Focused)
            {
                IsChanged = true;
                SaveData();
            }
        }

        private void mass_Changed(object sender, EventArgs e)
        {
            // Mass of fish was changed, so
            value_Changed(sender, e); // Set general changes

            // Recalculate condition factor
            UpdateCondition();

            // Recalculate fecundity values
            UpdateFecundityTab();

            // Recalculate consumption values
            UpdateTrophicsTab();
        }



        #region Fecundity tab

        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxGender.SelectedIndex)
            {
                case -1:
                case 0:
                    comboBoxMaturity.SelectedIndex = -1;
                    labelMaturity.Enabled = comboBoxMaturity.Enabled = false;
                    SwitchGonads(false);
                    SwitchFecundity(false);
                    break;
                case 1:
                    labelMaturity.Enabled = comboBoxMaturity.Enabled = true;
                    SwitchGonads(true);
                    SwitchFecundity(false);
                    break;
                case 2:
                    labelMaturity.Enabled = comboBoxMaturity.Enabled = true;
                    SwitchGonads(true);
                    SwitchFecundity(true);
                    break;
            }
            value_Changed(sender, e);
        }

        private void comboBoxMaturity_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxIntermediate.Enabled = comboBoxMaturity.SelectedIndex != -1;
            value_Changed(sender, e);
        }

        private void fecundity_Changed(object sender, EventArgs e)
        {
            value_Changed(sender, e);
            UpdateFecundityTab();
        }

        #endregion

        #region Trophics tab

        private void TextBoxConsumedMass_TextChanged(object sender, EventArgs e)
        {
            if (textBoxConsumedMass.Text.IsDoubleConvertible())
            {
                double w = IndividualRow.Mass;
                double wc = Convert.ToDouble(textBoxConsumedMass.Text);
                textBoxConsumptionIndex.Text = (wc / w * 10).ToString("N2");
            }
            else
            {
                textBoxConsumptionIndex.Text = string.Empty;
            }
        }

        private void comboBoxSection_ValueChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                // Save current set values
                SaveIntestine();
            }

            ClearIntestine();
            // Change part index to selected
            SelectedSection = (GutSection)comboBoxSection.SelectedIndex;
            IntestineRow = Data.Intestine.FindByIndIDSection(IndividualRow.ID, (int)SelectedSection);
            if (IntestineRow != null)
            {
                LoadIntestine(IntestineRow);
            }
        }

        #region Grid logics

        private void spreadSheetTrophics_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (spreadSheetTrophics.ContainsFocus && e.ColumnIndex == ColumnTrpMass.Index)
            {
                UpdateTrophicsTab();
            }
        }

        private void GridTrophics_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsChanged = true;
            HandleTrophicsRow(spreadSheetTrophics.Rows[e.RowIndex]);
        }

        private void GridTrophics_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ClearDietLogRow(e.Row);
        }

        private void GridTrophics_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            IsChanged = true;
        }

        private void GridTrophics_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            IsChanged = true;
        }

        #endregion

        #region Menu

        private void ToolStripMenuItemIndividuals_Click(object sender, EventArgs e)
        {
            spreadSheetTrophics.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetTrophics.SelectedRows)
            {
                // In future: select individuals form depending on Log's prey type value
                // For now it is always benthic forms

                if (gridRow.Cells[ColumnTrpSpecies.Name].Value != null)
                {
                    Benthos.Individuals individuals = new Benthos.Individuals(SaveLogRow(gridRow));

                    if (gridRow.Cells[ColumnTrpMass.Index].Value != null)
                    {
                        individuals.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                    }

                    if (gridRow.Cells[ColumnTrpQuantity.Index].Value != null)
                    {
                        individuals.Quantity = (int)gridRow.Cells[ColumnTrpQuantity.Index].Value;
                    }

                    individuals.LogLine = gridRow;
                    individuals.SetColumns(ColumnTrpSpecies, ColumnTrpQuantity, ColumnTrpMass);
                    individuals.SetFriendlyDesktopLocation(gridRow);
                    //individuals.Updater = new EventHandler(UpdateTrophicsTab);
                    individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
                    individuals.Show();
                }
            }
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e)
        {
            Benthos.Individuals individuals = sender as Benthos.Individuals;

            if (individuals.DialogResult == DialogResult.OK)
            {
                IsChanged = IsChanged || individuals.IsChanged;
            }
        }

        private void ToolStripMenuItemKeys_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in spreadSheetTrophics.SelectedRows)
            {
                speciesTrophics.FindInKey(selectedRow);
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

            foreach (DataGridViewRow gridRow in spreadSheetTrophics.SelectedRows)
            {
                if (gridRow.IsNewRow) continue;

                Data.LogRow clipLogRow = SaveLogRow(clipData, gridRow);
                Data.LogRow existingLogRow = Consumed.Log.FindByID(
                    (int)gridRow.Cells[ColumnTrpID.Index].Value);

                if (existingLogRow != null)
                {
                    foreach (Data.IndividualRow existingIndividualRow in existingLogRow.GetIndividualRows())
                    {
                        Data.IndividualRow clipIndividualRow =
                            clipData.Individual.NewIndividualRow();

                        if (!existingIndividualRow.IsCommentsNull())
                            clipIndividualRow.Comments = existingIndividualRow.Comments;
                        if (!existingIndividualRow.IsMassNull())
                            clipIndividualRow.Mass = existingIndividualRow.Mass;

                        clipIndividualRow.LogRow = clipLogRow;
                        clipData.Individual.AddIndividualRow(clipIndividualRow);
                    }

                    // Don't forget about Additional variables
                }
            }

            Clipboard.SetText(clipData.GetXml());
        }

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            Data clipData = new Data();
            clipData.ReadXml(new StringReader(Clipboard.GetText()));

            int rowIndex = spreadSheetTrophics.SelectedRows[0].Index;

            foreach (Data.LogRow clipLogRow in clipData.Log)
            {
                // Copy from Clipboard Data to local Data
                Data.LogRow logRow = Consumed.Log.NewLogRow();
                if (!clipLogRow.IsQuantityNull()) logRow.Quantity = clipLogRow.Quantity;
                if (!clipLogRow.IsMassNull()) logRow.Mass = clipLogRow.Mass;
                logRow.CardRow = Consumed.Card[0];

                SpeciesKey.SpeciesRow currentSpeciesRow =
                    UserSettings.SpeciesIndex.Species.FindBySpecies(clipLogRow.SpeciesRow.Species);

                if (currentSpeciesRow == null)
                {
                    Data.SpeciesRow newSpeciesRow = Consumed.Species.AddSpeciesRow(
                        clipLogRow.SpeciesRow.Species);

                    //Data.MergeWith(UserSettings.SpeciesIndex.Species);
                    logRow.SpcID = newSpeciesRow.ID;
                }
                else
                {
                    if (Data.Species.FindBySpecies(currentSpeciesRow.Name) == null)
                    {
                        Data.Species.Rows.Add(currentSpeciesRow.ID, currentSpeciesRow.Name);
                    }
                    logRow.SpcID = currentSpeciesRow.ID;
                }

                Consumed.Log.AddLogRow(logRow);

                foreach (Data.IndividualRow clipIndividualRow in clipData.Individual)
                {
                    Data.IndividualRow individualRow = Consumed.Individual.NewIndividualRow();
                    if (!clipIndividualRow.IsCommentsNull()) individualRow.Comments = clipIndividualRow.Comments;
                    if (!clipIndividualRow.IsMassNull()) individualRow.Mass = clipIndividualRow.Mass;
                    individualRow.LogRow = logRow;
                    Consumed.Individual.AddIndividualRow(individualRow);
                }

                // TODO: Don't forget about Additional variables

                InsertLogRow(logRow);

                if (rowIndex < spreadSheetTrophics.RowCount - 1)
                {
                    rowIndex++;
                }
            }

            //IsChanged = true;
            UpdateTrophicsTab();
            Clipboard.Clear();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            int rowsToDelete = spreadSheetTrophics.SelectedRows.Count;
            while (rowsToDelete > 0)
            {
                ClearDietLogRow(spreadSheetTrophics.SelectedRows[0]);
                spreadSheetTrophics.Rows.Remove(spreadSheetTrophics.SelectedRows[0]);
                rowsToDelete--;
            }

            IsChanged = true;
            UpdateTrophicsTab();
        }

        #endregion

        private void speciesTrophics_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            HandleTrophicsRow(e.Row);
            value_Changed(spreadSheetTrophics, e);
        }

        private void currentSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //switch (IndividualRow.GetPreyType())
            //{
            //    case PreyType.Invariant:
            //        UserSettings.Interface.SaveDialog.FileName =
            //            FileSystem.SuggestName(FileSystem.FolderName(UserSettings.Interface.SaveDialog.FileName),
            //            Consumed.SuggestedName);

            //        if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK) {

            //            Consumed.SaveToFile(UserSettings.Interface.SaveDialog.FileName);
            //        }
            //        break;

            //    case PreyType.Benthos:
            //        UserSettings.Interface.SaveDialog.FileName =
            //            FileSystem.SuggestName(FileSystem.FolderName(UserSettings.Interface.SaveDialog.FileName),
            //            Consumed.GetSuggestedName());

            //        if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK) {

            //            Consumed.WriteToFile(UserSettings.Interface.SaveDialog.FileName);
            //        }
            //        break;

            //    case PreyType.Fish:
            //        UserSettings.Interface.SaveDialog.FileName =
            //            FileSystem.SuggestName(FileSystem.FolderName(UserSettings.Interface.SaveDialog.FileName),
            //            Consumed.SuggestedName);

            //        if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK) {

            //            Consumed.WriteToFile(UserSettings.Interface.SaveDialog.FileName);
            //        }
            //        break;
            //}
        }

        private void buttonTrophics_Click(object sender, EventArgs e)
        {
            contextSaveTrophics.Show(buttonTrophics, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void allSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Data consumed = IndividualRow.GetConsumedBenthos();

            //UserSettings.Interface.SaveDialog.FileName =
            //    FileSystem.SuggestName(FileSystem.FolderName(UserSettings.Interface.SaveDialog.FileName),
            //    consumed.SuggestedName);

            //if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            //{
            //    consumed.WriteToFile(UserSettings.Interface.SaveDialog.FileName);
            //}
        }

        #region Data Insertion

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            if (Wild.UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in Wild.UserSettings.Interface.OpenDialog.FileNames)
                {
                    LoadTrophicData(filename);
                }

                IsChanged = true;
            }
        }

        private void GridLog_DragDrop(object sender, DragEventArgs e)
        {
            spreadSheetTrophics.Enabled = false;

            foreach (string fileName in IO.MaskedNames(
                (string[])e.Data.GetData(DataFormats.FileDrop), 
                Wild.UserSettings.Interface.OpenExtensions))
            {
                LoadTrophicData(fileName);
            }

            IsChanged = true;
            spreadSheetTrophics.Enabled = true;
        }

        private void GridLog_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        #endregion

        #endregion

        #region Parasites tab

        private void comboBoxOrgan_SelectedIndexChanged(object sender, EventArgs e)
        {
            spreadSheetInfection.Enabled = comboBoxOrgan.SelectedIndex != -1;

            SaveOrgan();

            // Save current set values
            if (comboBoxOrgan.SelectedIndex == -1)
            {
                ClearOrgan();
            }
            else
            {
                // Change part index to selected
                SelectedOrgan = comboBoxOrgan.SelectedIndex;

                // Load new values if exist
                Data.OrganRow infectedOrganRow = Data.Organ.FindByIndIDOrgan(IndividualRow.ID, SelectedOrgan);

                if (infectedOrganRow == null)
                {
                    ClearOrgan();
                }
                else
                {
                    LoadOrgan(infectedOrganRow);
                }
            }
        }

        private void GridParasites_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            HandleParasiteRow(spreadSheetInfection.Rows[e.RowIndex]);
            value_Changed(sender, new EventArgs());
        }

        private void speciesParasites_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            HandleParasiteRow(e.Row);
            value_Changed(sender, e);
        }

        #endregion



        private void buttonBlank_Click(object sender, EventArgs e)
        {
            FishReport.BlankIndividualProfile.Run();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            SaveData();
            IndividualRow.GetReport().Run();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveData();
            IndividualRow.AcceptChanges();
            IsChanged = false;
            Close();
        }

        private void textBoxLength_TextChanged(object sender, EventArgs e)
        {
            value_Changed(sender, e);
            UpdateCondition();
        }
    }

    public enum GutSection
    {
        OralCavity,
        Oesophagus,
        Stomach,
        ForeIntestine,
        MidIntestine,
        LargeIntestine,
        WholeIntestine = -1
    }
}