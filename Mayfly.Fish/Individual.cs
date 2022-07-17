using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static Mayfly.Wild.ReaderSettings;

namespace Mayfly.Fish
{
    public partial class Individual : Form
    {
        #region Properties



        public Wild.Survey.IndividualRow IndividualRow;

        private Wild.Survey Data;

        //public DataGridViewRow LogLine;

        private Wild.Survey.IntestineRow intestineRow;

        private Wild.Survey.IntestineRow IntestineRow {
            get {
                return intestineRow;
            }

            set {
                intestineRow = value;
            }
        }

        public GutSection SelectedSection = GutSection.WholeIntestine;



        private Wild.Survey.OrganRow organRow;

        private Wild.Survey.OrganRow OrganRow {
            get {
                return organRow;
            }

            set {
                organRow = value;
            }
        }

        public int SelectedOrgan = -1;



        public bool IsChanged { get; set; }



        private Wild.Survey Consumed {
            get;
            set;
        }

        public double ConsumedDetailedMass {
            get {
                double result = 0;

                foreach (Wild.Survey.IntestineRow intestineRow in IndividualRow.GetIntestineRows()) {
                    if (intestineRow == IntestineRow) {
                        continue;
                    }

                    if (!intestineRow.IsConsumedNull()) {
                        Wild.Survey consumed = new Wild.Survey();
                        consumed.ReadXml(new StringReader(intestineRow.Consumed));
                        result += consumed.Solitary.Mass;
                    }
                }

                return result + ConsumedDetailedMassCurrent;
            }
        }

        public double ConsumedDetailedMassCurrent {
            get {
                double result = 0;
                foreach (DataGridViewRow CurrentRecord in spreadSheetTrophics.Rows) {
                    if (CurrentRecord.Cells[ColumnTrpMass.Name].Value != null) {
                        result += (double)CurrentRecord.Cells[ColumnTrpMass.Name].Value;
                    }
                }
                return result;
            }
        }



        private Wild.Survey Infection {
            get;
            set;
        }



        public EventHandler ValueChanged {
            get;
            set;
        }

        #endregion



        public Individual() {
            InitializeComponent();
            Log.Write("Open individual fish profile.");

            Consumed = new Wild.Survey();
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

        public Individual(Wild.Survey.IndividualRow individualRow)
            : this() {
            IndividualRow = individualRow;

            IndividualRow.AcceptChanges();
            Data = (Wild.Survey)individualRow.Table.DataSet;

            Text = string.Format(Wild.Resources.Interface.Interface.IndWindow, individualRow.Species);

            LoadData();
        }



        #region Methods

        public void ShowFecundity() {
            tabControlInd.SelectedTab = tabPageFecundity;
        }

        public void ShowGrowth() {
            tabControlInd.SelectedTab = tabPageGrowth;
        }

        public void ShowTrophics() {
            tabControlInd.SelectedTab = tabPageTrophics;
            spreadSheetTrophics.Focus();
        }

        public void ShowParasites() {
            tabControlInd.SelectedTab = tabPageParasites;
            spreadSheetInfection.Focus();
        }



        private void LoadData() {
            #region General

            if (IndividualRow.IsLengthNull()) numericLength.Clear();
            else numericLength.Value = IndividualRow.Length;

            double tl = IndividualRow.GetTotalLength();
            if (double.IsNaN(tl)) numericTL.Clear();
            else numericTL.Value = tl;

            if (IndividualRow.IsMassNull()) numericMass.Clear();
            else numericMass.Value = IndividualRow.Mass;

            if (IndividualRow.IsSomaticMassNull()) numericSomaticMass.Clear();
            else numericSomaticMass.Value = IndividualRow.SomaticMass;

            if (IndividualRow.IsAgeNull()) textBoxAge.Clear();
            else textBoxAge.Text = new Age(IndividualRow.Age).ToString();

            if (IndividualRow.IsTallyNull()) textBoxTally.Clear();
            else textBoxTally.Text = IndividualRow.Tally;

            if (IndividualRow.IsCommentsNull()) textBoxComments.Clear();
            else textBoxComments.Text = IndividualRow.Comments;

            #endregion

            #region Fecundity

            if (IndividualRow.IsSexNull()) comboBoxGender.SelectedIndex = -1;
            else comboBoxGender.SelectedIndex = new Sex(IndividualRow.Sex).Value;

            if (IndividualRow.IsMaturityNull()) comboBoxMaturity.SelectedIndex = -1;
            else comboBoxMaturity.SelectedIndex = IndividualRow.Maturity - 1;

            if (IndividualRow.IsIntermatureNull()) checkBoxIntermediate.CheckState = CheckState.Indeterminate;
            else checkBoxIntermediate.CheckState = IndividualRow.Intermature ? CheckState.Checked : CheckState.Unchecked;

            if (IndividualRow.IsGonadMassNull()) numericGonadMass.Clear();
            else numericGonadMass.Value = IndividualRow.GonadMass;

            if (IndividualRow.IsGonadSampleMassNull()) numericGonadSampleMass.Clear();
            else numericGonadSampleMass.Value = IndividualRow.GonadSampleMass;

            if (IndividualRow.IsGonadSampleNull()) numericGonadSample.Clear();
            else numericGonadSample.Value = IndividualRow.GonadSample;

            if (IndividualRow.IsEggSizeNull()) numericEggSize.Clear();
            else numericEggSize.Value = IndividualRow.EggSize;

            #endregion

            #region Trophics

            if (IndividualRow.IsFatnessNull()) comboBoxFatness.SelectedIndex = -1;
            else comboBoxFatness.SelectedIndex = IndividualRow.Fatness;

            if (IndividualRow.IsConsumedMassNull()) numericConsumedMass.Clear();
            else numericConsumedMass.Value = IndividualRow.ConsumedMass;

            Wild.Survey.IntestineRow[] Guts = IndividualRow.GetIntestineRows();

            if (Guts.Length == 0) {
                ClearIntestine();
            } else {
                if (comboBoxSection.SelectedIndex == Guts[0].Section) {
                    comboBoxSection_ValueChanged(comboBoxSection, new EventArgs());
                } else {
                    comboBoxSection.SelectedIndex = Guts[0].Section;
                }
            }

            #endregion

            #region Parasite

            Wild.Survey.OrganRow[] Organs = IndividualRow.GetOrganRows();
            if (Organs.Length > 0) {
                comboBoxOrgan.SelectedIndex = Organs[0].Organ;
            }

            #endregion

            spreadSheetAddt.Rows.Clear();

            foreach (Wild.Survey.ValueRow valueRow in IndividualRow.GetValueRows()) {
                LoadAddtValue(valueRow.VariableRow.Variable, valueRow.Value);
            }
        }

        private void SaveData() {
            #region General            

            IndividualRow.SetAddtValue("TL", numericTL.Value);

            if (numericLength.IsSet) IndividualRow.Length = numericLength.Value;
            else IndividualRow.SetLengthNull();

            if (numericMass.IsSet) IndividualRow.Mass = numericMass.Value;
            else IndividualRow.SetMassNull();

            if (numericSomaticMass.IsSet) IndividualRow.SomaticMass = numericSomaticMass.Value;
            else IndividualRow.SetSomaticMassNull();

            if (textBoxAge.Text.IsAcceptable()) IndividualRow.Age = new Age(textBoxAge.Text).Value;
            else IndividualRow.SetAgeNull();

            if (textBoxTally.Text.IsAcceptable()) IndividualRow.Tally = textBoxTally.Text;
            else IndividualRow.SetTallyNull();

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

            if (numericGonadMass.IsSet) IndividualRow.GonadMass = numericGonadMass.Value;
            else IndividualRow.SetGonadMassNull();

            if (numericGonadSampleMass.IsSet) IndividualRow.GonadSampleMass = numericGonadSampleMass.Value;
            else IndividualRow.SetGonadSampleMassNull();

            if (numericGonadSample.IsSet) IndividualRow.GonadSample = (int)numericGonadSample.Value;
            else IndividualRow.SetGonadSampleNull();

            if (numericEggSize.IsSet) IndividualRow.EggSize = numericEggSize.Value;
            else IndividualRow.SetEggSizeNull();

            #endregion

            #region Trophics

            if (comboBoxFatness.SelectedIndex == -1) IndividualRow.SetFatnessNull();
            else IndividualRow.Fatness = comboBoxFatness.SelectedIndex;

            if (numericConsumedMass.IsSet) IndividualRow.ConsumedMass = numericConsumedMass.Value;
            else IndividualRow.SetConsumedMassNull();

            SaveIntestine();

            #endregion

            #region Organ

            SaveOrgan();

            #endregion

            #region Save Addts

            foreach (DataGridViewRow gridRow in spreadSheetAddt.Rows) {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnAddtVariable.Index].Value == null) continue;

                string variableName = (string)gridRow.Cells[ColumnAddtVariable.Index].Value;

                if (!variableName.IsAcceptable()) continue;

                if (gridRow.Cells[ColumnAddtValue.Index].Value == null) {
                    IndividualRow.SetAddtValueNull(variableName);
                } else {
                    IndividualRow.SetAddtValue(variableName, (double)gridRow.Cells[ColumnAddtValue.Index].Value);
                }
            }

            #endregion

            if (ValueChanged != null) {
                ValueChanged.Invoke(this, new EventArgs());
            }
        }



        private void UpdateCondition() {
            numericCondition.Value = IndividualRow.GetCondition();
            numericConditionSoma.Value = IndividualRow.GetConditionSomatic();
        }

        private void UpdateFecundityTab() {
            numericMeanEggMass.Value = IndividualRow.GetAveEggMass();

            numericGonadIndex.Value = IndividualRow.GetGonadIndex();
            numericGonadIndexSoma.Value = IndividualRow.GetGonadIndexSomatic();

            numericRelativeFecundity.Value = IndividualRow.GetRelativeFecundity();

            numericRelativeFecunditySoma.Value = IndividualRow.GetRelativeFecunditySomatic();
            numericAbsoluteFecundity.Value = IndividualRow.GetAbsoluteFecundity();
        }

        private void UpdateTrophicsTab() {
            double w = IndividualRow.Mass;
            double wc = ConsumedDetailedMass;

            if (!numericConsumedMass.IsSet || numericConsumedMass.Value < wc) {
                numericConsumedMass.Value = wc;
            }

            bool allset = true;

            foreach (DataGridViewRow gridRow in spreadSheetTrophics.Rows) {
                if (gridRow.IsNewRow) continue;

                if (gridRow.Cells[ColumnTrpMass.Index].Value == null) {
                    gridRow.Cells[ColumnTrpConsumption.Index].Value = null;
                    allset = false;
                } else {
                    double wi = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                    gridRow.Cells[ColumnTrpConsumption.Index].Value = (wi / w * 10);
                }
            }

            numericConsumedMass.ReadOnly = allset;
        }

        private void SwitchGonads(bool isMatured) {
            labelFecundity.Enabled =

            labelGonadMass.Enabled =
            labelGonadosomatic.Enabled =

            numericGonadMass.Enabled =
            numericGonadIndex.Enabled =
            numericGonadIndexSoma.Enabled =
            numericRelativeFecundity.Enabled =
            numericRelativeFecunditySoma.Enabled =

            isMatured;
        }

        private void SwitchFecundity(bool isFemale) {
            numericGonadSample.Enabled =
            numericGonadSampleMass.Enabled =
            numericAbsoluteFecundity.Enabled =
            numericRelativeFecundity.Enabled =
            labelGonadSample.Enabled =
            labelGonadSampleMass.Enabled =
            labelFecundityAbs.Enabled =
            labelFecundityRel.Enabled =

            numericEggSize.Enabled =
            numericMeanEggMass.Enabled =
            labelEggSize.Enabled = labelEggMass.Enabled =
            labelEgg.Enabled =

            isFemale;
        }



        #region Trophics

        private void ClearIntestine() {
            comboBoxFullness.SelectedIndex = -1;
            comboBoxFermentation.SelectedIndex = -1;
            Consumed = new Wild.Survey();
            spreadSheetTrophics.Rows.Clear();
        }

        private void ClearDietLogRow(DataGridViewRow gridRow) {
            if (gridRow.Cells[ColumnTrpID.Index].Value == null) return;

            Wild.Survey.LogRow logRow = Consumed.Log.FindByID((int)gridRow.Cells[ColumnTrpID.Index].Value);
            Wild.Survey.DefinitionRow spcRowInvariant = logRow.DefinitionRow;
            logRow.Delete();
            spcRowInvariant.Delete();
        }

        private void ClearDietCellContent() {
            foreach (DataGridViewCell gridCell in spreadSheetTrophics.SelectedCells) {
                if (gridCell.OwningColumn == ColumnTrpQuantity) {
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

        private void SaveIntestine() {
            if (comboBoxFullness.SelectedIndex > -1 ||
                comboBoxFermentation.SelectedIndex > -1 ||
                spreadSheetTrophics.RowCount > 1) {
                if (IntestineRow == null) {
                    IntestineRow = Data.Intestine.NewIntestineRow();
                    IntestineRow.IndividualRow = IndividualRow;
                    IntestineRow.Section = (int)SelectedSection;
                    Data.Intestine.AddIntestineRow(IntestineRow);
                }

                if (comboBoxFullness.SelectedIndex == -1) IntestineRow.SetFullnessNull();
                else IntestineRow.Fullness = comboBoxFullness.SelectedIndex;

                if (comboBoxFermentation.SelectedIndex == -1) IntestineRow.SetFermentationNull();
                else IntestineRow.Fermentation = comboBoxFermentation.SelectedIndex;

                if (spreadSheetTrophics.RowCount == (spreadSheetTrophics.AllowUserToAddRows ? 1 : 0)) {
                    IntestineRow.SetConsumedNull();
                } else {
                    SaveConsumed();
                }
            } else if (IntestineRow != null) {
                Data.Intestine.RemoveIntestineRow(IntestineRow);
            }

            UpdateTrophicIndex();
        }

        private void SaveConsumed() {
            // Save consumed benthos
            foreach (DataGridViewRow gridRow in spreadSheetTrophics.Rows) {
                if (gridRow.IsNewRow) continue;

                SaveLogRow(gridRow);
            }

            if (Consumed.Log.Count == 0) { IntestineRow.SetConsumedNull(); } else { Consumed.ClearUseless(); IntestineRow.Consumed = Consumed.GetXml().Replace(Environment.NewLine, " "); }
        }

        private void LoadIntestine(Wild.Survey.IntestineRow intestineRow) {
            if (intestineRow.IsFullnessNull()) {
                comboBoxFullness.SelectedIndex = -1;
            } else {
                comboBoxFullness.SelectedIndex = intestineRow.Fullness;
            }

            if (intestineRow.IsFermentationNull()) {
                comboBoxFermentation.SelectedIndex = -1;
            } else {
                comboBoxFermentation.SelectedIndex = intestineRow.Fermentation;
            }

            spreadSheetTrophics.Rows.Clear();

            if (intestineRow.IsConsumedNull()) {
                Consumed = new Wild.Survey();
            } else {
                Consumed = intestineRow.GetConsumed();

                foreach (Wild.Survey.LogRow logRow in Consumed.Log) {
                    InsertLogRow(logRow);
                }
            }
        }

        private void InsertLogRow(Wild.Survey.LogRow logRow) {
            List<object> LogRowItems = new List<object>(4)
            {
                logRow.ID,
                logRow.DefinitionRow.Taxon
            };

            if (logRow.IsQuantityNull()) {
                LogRowItems.Add(null);
            } else {
                LogRowItems.Add(logRow.Quantity);
            }

            if (logRow.IsMassNull()) {
                LogRowItems.Add(null);
                LogRowItems.Add(null);
            } else {
                LogRowItems.Add(logRow.Mass);
                LogRowItems.Add(IndividualRow.IsMassNull() ? double.NaN : logRow.Mass / IndividualRow.Mass * 10);
            }

            //GridTrophics.Rows.Insert(RowIndex, LogRowItems.ToArray());
            int rowIndex = spreadSheetTrophics.Rows.Add(LogRowItems.ToArray());
            HandleTrophicsRow(spreadSheetTrophics.Rows[rowIndex]);

            if (!logRow.IsCommentsNull()) {
                spreadSheetTrophics[ColumnTrpSpecies.Name, rowIndex].ToolTipText = logRow.Comments;
            }
        }

        private void HandleTrophicsRow(DataGridViewRow gridRow) {
            // If it is new row - end of function.
            if (gridRow.IsNewRow) {
                return;
            }

            // If row is empty - delete the row and end the function
            if (Wild.Service.IsRowEmpty(gridRow)) {
                spreadSheetTrophics.Rows.Remove(gridRow);
                return;
            }

            // If species is not set - light 'Not identified'
            if (gridRow.Cells[ColumnTrpSpecies.Index].Value == null) {
                gridRow.Cells[ColumnTrpSpecies.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }

            // Searching for the duplicates (on 'Species' column)

            for (int i = 0; i < spreadSheetTrophics.RowCount; i++) {
                DataGridViewRow trpGridRow = spreadSheetTrophics.Rows[i];

                if (trpGridRow.IsNewRow) {
                    continue;
                }

                if (trpGridRow == gridRow) {
                    continue;
                }

                if (object.Equals(gridRow.Cells[ColumnTrpSpecies.Index].Value, trpGridRow.Cells[ColumnTrpSpecies.Index].Value)) {
                    int N = 0;
                    double B = 0;

                    if (gridRow.Cells[ColumnTrpQuantity.Index].Value != null) {
                        N += (int)gridRow.Cells[ColumnTrpQuantity.Name].Value;
                    }

                    if (gridRow.Cells[ColumnTrpMass.Index].Value != null) {
                        if (gridRow.Cells[ColumnTrpQuantity.Index].Value != null && (int)gridRow.Cells[ColumnTrpQuantity.Index].Value == 1) {
                            Wild.Survey.IndividualRow newIndividualRowFish = Consumed.Individual.NewIndividualRow();
                            newIndividualRowFish.LogRow = SaveLogRow(gridRow);
                            newIndividualRowFish.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                            Consumed.Individual.AddIndividualRow(newIndividualRowFish);
                        }

                        B += (double)gridRow.Cells[ColumnTrpMass.Name].Value;
                    }

                    if (trpGridRow.Cells[ColumnTrpQuantity.Index].Value != null) {
                        N += (int)trpGridRow.Cells[ColumnTrpQuantity.Name].Value;
                    }

                    if (trpGridRow.Cells[ColumnTrpMass.Index].Value != null) {
                        if (trpGridRow.Cells[ColumnTrpQuantity.Index].Value != null &&
                            (int)trpGridRow.Cells[ColumnTrpQuantity.Index].Value == 1) {
                            Wild.Survey.IndividualRow newIndividualRowFish = Consumed.Individual.NewIndividualRow();
                            newIndividualRowFish.LogRow = SaveLogRow(gridRow);
                            newIndividualRowFish.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                            newIndividualRowFish.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
                            Consumed.Individual.AddIndividualRow(newIndividualRowFish);
                        }

                        B += (double)trpGridRow.Cells[ColumnTrpMass.Name].Value;
                    }

                    if (N > 0) {
                        gridRow.Cells[ColumnTrpQuantity.Index].Value = N;
                    }

                    if (B > 0) {
                        gridRow.Cells[ColumnTrpMass.Index].Value = B;
                    }

                    spreadSheetTrophics.Rows.Remove(trpGridRow);
                    i--;
                }
            }
        }



        private Wild.Survey.LogRow SaveLogRow(DataGridViewRow gridRow) {
            return SaveLogRow(Consumed, gridRow);
        }

        private Wild.Survey.LogRow SaveLogRow(Wild.Survey data, DataGridViewRow gridRow) {
            Wild.Survey.LogRow result;
            bool IsNew = false;

            if (gridRow.Cells[ColumnTrpID.Index].Value != null) {
                result = data.Log.FindByID((int)gridRow.Cells[ColumnTrpID.Index].Value);
                if (result != null) {
                    goto Saving;
                }
            }

            result = data.Log.NewLogRow();
            result.CardRow = data.Solitary;
            IsNew = true;

        Saving:

            if (gridRow.Cells[ColumnTrpSpecies.Index].Value is TaxonomicIndex.TaxonRow tr) {
                Survey.DefinitionRow existingSpeciesRow = data.Definition.FindByName(tr.Name);
                if (existingSpeciesRow == null) {
                    existingSpeciesRow = data.Definition.AddDefinitionRow(tr.Rank, tr.Name);
                }
                result.DefID = existingSpeciesRow.ID;
            } else if ((string)gridRow.Cells[ColumnTrpSpecies.Index].Value is string s) {
                if (s == Species.Resources.Interface.UnidentifiedTitle) {
                    result.SetDefIDNull();
                } else {
                    Wild.Survey.DefinitionRow existingSpeciesRow = data.Definition.FindByName(s);
                    if (existingSpeciesRow == null) {
                        existingSpeciesRow = data.Definition.AddDefinitionRow(s);
                    }
                    result.DefID = existingSpeciesRow.ID;
                }
            }

            if (gridRow.Cells[ColumnTrpQuantity.Index].Value == null) {
                result.SetQuantityNull();
            } else {
                result.Quantity = (int)gridRow.Cells[ColumnTrpQuantity.Index].Value;
            }

            if (gridRow.Cells[ColumnTrpMass.Index].Value == null) {
                result.SetMassNull();
            } else {
                result.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
            }

            if (IsNew) {
                data.Log.AddLogRow(result);
                gridRow.Cells[ColumnTrpID.Index].Value = result.ID;
            }

            return result;
        }



        private void LoadTrophicData(string filename) {
            Wild.Survey data = new Wild.Survey();
            data.Read(filename);

            if (spreadSheetTrophics.RowCount < 2) // If only newrow is in grid
            {
                // Load card as data
                if (IntestineRow == null) {
                    IntestineRow = Data.Intestine.NewIntestineRow();
                    IntestineRow.IndividualRow = IndividualRow;
                    IntestineRow.Section = (int)SelectedSection;
                    Data.Intestine.AddIntestineRow(IntestineRow);
                }

                IntestineRow.Consumed = data.GetXml();
                LoadIntestine(IntestineRow);
                UpdateTrophicsTab();
            } else {
                InsertTrophicLogRows(data, -1);
            }
        }

        private void InsertTrophicLogRows(Wild.Survey data, int rowIndex) {
            if (rowIndex == -1) {
                rowIndex = spreadSheetTrophics.RowCount - 1;
            }

            foreach (Wild.Survey.LogRow logRow in data.Log) {
                InsertLogRow(logRow);

                if (rowIndex < spreadSheetTrophics.RowCount - 1) {
                    rowIndex++;
                }
            }

            UpdateTrophicsTab();
        }

        private void UpdateTrophicIndex() {
            if (Consumed != null) {
                TaxonomicIndex localIndex = Consumed.GetSpeciesKey();
                speciesTrophics.UpdateIndex(localIndex, SpeciesAutoExpandVisual);
            }

        }

        #endregion



        #region Parasite

        private void ClearOrgan() {
            Infection = null;
            spreadSheetInfection.Rows.Clear();
        }

        private void SaveOrgan() {

            if (spreadSheetInfection.RowCount > 1) {
                if (OrganRow == null) {
                    organRow = Data.Organ.NewOrganRow();
                    organRow.IndividualRow = IndividualRow;
                    organRow.Organ = SelectedOrgan;
                    Data.Organ.AddOrganRow(organRow);
                }

                if (spreadSheetInfection.RowCount == (spreadSheetInfection.AllowUserToAddRows ? 1 : 0)) {
                    OrganRow.SetInfectionNull();
                } else {
                    SaveInfection();
                }
            } else if (OrganRow != null) {
                Data.Organ.RemoveOrganRow(OrganRow);
            }

            UpdateParasiteIndex();
        }

        private void SaveInfection() {
            foreach (DataGridViewRow gridRow in spreadSheetInfection.Rows) {
                if (gridRow.IsNewRow) continue;

                SaveParasitesLogRow(gridRow);
            }

            if (Infection.Log.Count == 0) { OrganRow.SetInfectionNull(); } else { Infection.ClearUseless(); OrganRow.Infection = Infection.GetXml(); }
        }

        private Wild.Survey.LogRow SaveParasitesLogRow(DataGridViewRow gridRow) {
            return SaveLogRow(Infection, gridRow);
        }

        private Wild.Survey.LogRow SaveParasitesLogRow(Wild.Survey data, DataGridViewRow gridRow) {
            Wild.Survey.LogRow result;
            bool IsNew = false;

            if (gridRow.Cells[ColumnInfID.Index].Value != null) {
                result = data.Log.FindByID((int)gridRow.Cells[ColumnInfID.Index].Value);
                if (result != null) {
                    goto Saving;
                }
            }

            result = data.Log.NewLogRow();
            result.CardRow = data.Card[0];
            IsNew = true;

        Saving:


            if (gridRow.Cells[ColumnInfSpecies.Index].Value is TaxonomicIndex.TaxonRow tr) {
                Wild.Survey.DefinitionRow existingSpeciesRow = data.Definition.FindByName(tr.Name);
                if (existingSpeciesRow == null) {
                    existingSpeciesRow = data.Definition.AddDefinitionRow(tr.Rank, tr.Name);
                }
                result.DefID = existingSpeciesRow.ID;
            } else if ((string)gridRow.Cells[ColumnInfSpecies.Index].Value is string s) {
                if (s == Species.Resources.Interface.UnidentifiedTitle) {
                    result.SetDefIDNull();
                } else {
                    Wild.Survey.DefinitionRow existingSpeciesRow = data.Definition.FindByName(s);
                    if (existingSpeciesRow == null) {
                        existingSpeciesRow = data.Definition.AddDefinitionRow(s);
                    }
                    result.DefID = existingSpeciesRow.ID;
                }
            }

            if (gridRow.Cells[ColumnInfQuantity.Index].Value == null) {
                result.SetQuantityNull();
            } else {
                result.Quantity = (int)gridRow.Cells[ColumnInfQuantity.Index].Value;
            }

            if (IsNew) {
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
        //        SpeciesKey.TaxonRow CurrentParasite = UserSettings.ParasitesIndex.Definition.FindByName(
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

        private void HandleParasiteRow(DataGridViewRow Row) {
            // If it is new row - end of function.
            if (Row.IsNewRow) {
                return;
            }

            // If row is empty - delete the row and end the function
            if (Wild.Service.IsRowEmpty(Row)) {
                spreadSheetInfection.Rows.Remove(Row);
                return;
            }

            // If species is not set - light 'Not identified'
            if (Row.Cells[ColumnInfSpecies.Index].Value == null) {
                Row.Cells[ColumnInfSpecies.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }

            // Searching for the duplicates (on 'Parasite Species' column)

            for (int i = 0; i < spreadSheetInfection.RowCount; i++) {
                DataGridViewRow _Row = spreadSheetInfection.Rows[i];

                if (_Row.IsNewRow) {
                    continue;
                }

                if (_Row == Row) {
                    continue;
                }

                if (object.Equals(Row.Cells[ColumnInfSpecies.Index].Value, _Row.Cells[ColumnInfSpecies.Index].Value)) {
                    int N = 0;

                    if (Row.Cells[ColumnInfQuantity.Index].Value != null) {
                        N += (int)Row.Cells[ColumnInfQuantity.Name].Value;
                    }

                    if (_Row.Cells[ColumnInfQuantity.Index].Value != null) {
                        N += (int)_Row.Cells[ColumnInfQuantity.Name].Value;
                    }

                    if (N > 0) {
                        Row.Cells[ColumnInfQuantity.Index].Value = N;
                    }

                    spreadSheetInfection.Rows.Remove(_Row);
                    i--;
                }
            }
        }

        private void LoadOrgan(Wild.Survey.OrganRow organRow) {
            spreadSheetInfection.Rows.Clear();

            if (organRow.IsInfectionNull()) {
                Infection = null;
            } else {
                Infection = organRow.GetInfection();

                foreach (Wild.Survey.LogRow logRow in Infection.Log) {
                    InsertLogRow(logRow);
                }
            }
        }

        private void UpdateParasiteIndex() {
            if (Infection != null) {
                TaxonomicIndex localIndex = Infection.GetSpeciesKey();
                speciesParasites.UpdateIndex(localIndex, SpeciesAutoExpandVisual);
            }

        }

        #endregion



        private void LoadAddtValue(string name, double value) {
            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetAddt);
            gridRow.Cells[ColumnAddtVariable.Index].Value = name;
            gridRow.Cells[ColumnAddtValue.Index].Value = value;
            spreadSheetAddt.Rows.Add(gridRow);
        }

        #endregion



        private void Individual_FormClosing(object sender, FormClosingEventArgs e) {
            if (IsChanged) {
                TaskDialogButton b = taskDialogSaveChanges.ShowDialog(this);

                if (b == tdbSave) {
                    SaveData();
                    IndividualRow.AcceptChanges();
                    IsChanged = false;
                    DialogResult = DialogResult.OK;
                } else if (b == tdbDiscard) {
                    IndividualRow.RejectChanges();
                    if (ValueChanged != null) { ValueChanged.Invoke(this, new EventArgs()); }
                    IsChanged = false;
                } else if (b == tdbCancel) {
                    e.Cancel = true;
                }
            }
        }



        private void comboBox_KeyPress(object sender, KeyPressEventArgs e) {
            ((ComboBox)sender).HandleInput(e);
        }

        private void value_KeyPress(object sender, KeyPressEventArgs e) {
            ((Control)sender).HandleInput(e, typeof(double));
        }

        private void intValue_KeyPress(object sender, KeyPressEventArgs e) {
            ((Control)sender).HandleInput(e, typeof(int));
        }

        private void value_Changed(object sender, EventArgs e) {
            if (((Control)sender).Focused) {
                IsChanged = true;
                SaveData();
            }
        }

        private void mass_Changed(object sender, EventArgs e) {
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

        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e) {
            switch (comboBoxGender.SelectedIndex) {
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

        private void comboBoxMaturity_SelectedIndexChanged(object sender, EventArgs e) {
            checkBoxIntermediate.Enabled = comboBoxMaturity.SelectedIndex != -1;
            value_Changed(sender, e);
        }

        private void fecundity_Changed(object sender, EventArgs e) {
            value_Changed(sender, e);
            UpdateFecundityTab();
        }

        #endregion

        #region Trophics tab

        private void TextBoxConsumedMass_TextChanged(object sender, EventArgs e) {
            if (numericConsumedMass.IsSet) {
                double w = IndividualRow.Mass;
                double wc = numericConsumedMass.Value;
                numericConsumptionIndex.Value = wc / w * 10;
            } else {
                numericConsumptionIndex.Clear();
            }
        }

        private void comboBoxSection_ValueChanged(object sender, EventArgs e) {
            if (Visible) {
                // Save current set values
                SaveIntestine();
            }

            ClearIntestine();
            // Change part index to selected
            SelectedSection = (GutSection)comboBoxSection.SelectedIndex;
            IntestineRow = Data.Intestine.FindByIndIDSection(IndividualRow.ID, (int)SelectedSection);
            if (IntestineRow != null) {
                LoadIntestine(IntestineRow);
            }
        }

        #region Grid logics

        private void spreadSheetTrophics_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (spreadSheetTrophics.ContainsFocus && e.ColumnIndex == ColumnTrpMass.Index) {
                UpdateTrophicsTab();
            }
        }

        private void GridTrophics_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            IsChanged = true;
            HandleTrophicsRow(spreadSheetTrophics.Rows[e.RowIndex]);
        }

        private void GridTrophics_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
            ClearDietLogRow(e.Row);
        }

        private void GridTrophics_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            IsChanged = true;
        }

        private void GridTrophics_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            IsChanged = true;
        }

        #endregion

        #region Menu

        private void ToolStripMenuItemIndividuals_Click(object sender, EventArgs e) {
            spreadSheetTrophics.EndEdit();

            foreach (DataGridViewRow gridRow in spreadSheetTrophics.SelectedRows) {
                // In future: select individuals form depending on Log's prey type value
                // For now it is always benthic forms

                if (gridRow.Cells[ColumnTrpSpecies.Name].Value != null) {
                    Benthos.Individuals individuals = new Benthos.Individuals(SaveLogRow(gridRow));

                    if (gridRow.Cells[ColumnTrpMass.Index].Value != null) {
                        individuals.Mass = (double)gridRow.Cells[ColumnTrpMass.Index].Value;
                    }

                    if (gridRow.Cells[ColumnTrpQuantity.Index].Value != null) {
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

        private void individuals_FormClosing(object sender, FormClosingEventArgs e) {
            Benthos.Individuals individuals = sender as Benthos.Individuals;

            if (individuals.DialogResult == DialogResult.OK) {
                IsChanged = IsChanged || individuals.IsChanged;
            }
        }

        private void ToolStripMenuItemKeys_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow selectedRow in spreadSheetTrophics.SelectedRows) {
                speciesTrophics.FindInKey(selectedRow);
            }
        }

        private void ToolStripMenuItemCut_Click(object sender, EventArgs e) {
            ToolStripMenuItemCopy_Click(sender, e);
            ToolStripMenuItemDelete_Click(sender, e);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e) {
            Wild.Survey clipData = new Wild.Survey();
            Wild.Survey.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            foreach (DataGridViewRow gridRow in spreadSheetTrophics.SelectedRows) {
                if (gridRow.IsNewRow) continue;

                Wild.Survey.LogRow clipLogRow = SaveLogRow(clipData, gridRow);
                Wild.Survey.LogRow existingLogRow = Consumed.Log.FindByID(
                    (int)gridRow.Cells[ColumnTrpID.Index].Value);

                if (existingLogRow != null) {
                    foreach (Wild.Survey.IndividualRow existingIndividualRow in existingLogRow.GetIndividualRows()) {
                        Wild.Survey.IndividualRow clipIndividualRow =
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

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e) {
            Wild.Survey clipData = new Wild.Survey();
            clipData.ReadXml(new StringReader(Clipboard.GetText()));

            int rowIndex = spreadSheetTrophics.SelectedRows[0].Index;

            foreach (Wild.Survey.LogRow clipLogRow in clipData.Log) {
                // Copy from Clipboard Data to local Data
                Wild.Survey.LogRow logRow = Consumed.Log.NewLogRow();
                if (!clipLogRow.IsQuantityNull()) logRow.Quantity = clipLogRow.Quantity;
                if (!clipLogRow.IsMassNull()) logRow.Mass = clipLogRow.Mass;
                logRow.CardRow = Consumed.Card[0];



                TaxonomicIndex.TaxonRow clipSpeciesRow = UserSettings.DietIndex.FindByName(clipLogRow.DefinitionRow.Taxon);

                Survey.DefinitionRow newSpeciesRow = (clipSpeciesRow == null ?
                    Data.Definition.AddDefinitionRow(clipLogRow.DefinitionRow.Taxon) :
                    Data.Definition.AddDefinitionRow(clipSpeciesRow.Rank, clipSpeciesRow.Name));

                logRow.DefID = newSpeciesRow.ID;

                Consumed.Log.AddLogRow(logRow);

                foreach (Survey.IndividualRow clipIndividualRow in clipData.Individual) {
                    Survey.IndividualRow individualRow = Consumed.Individual.NewIndividualRow();
                    if (!clipIndividualRow.IsCommentsNull()) individualRow.Comments = clipIndividualRow.Comments;
                    if (!clipIndividualRow.IsMassNull()) individualRow.Mass = clipIndividualRow.Mass;
                    individualRow.LogRow = logRow;
                    Consumed.Individual.AddIndividualRow(individualRow);
                }

                // TODO: Don't forget about Additional variables

                InsertLogRow(logRow);

                if (rowIndex < spreadSheetTrophics.RowCount - 1) {
                    rowIndex++;
                }
            }

            //IsChanged = true;
            UpdateTrophicsTab();
            Clipboard.Clear();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e) {
            int rowsToDelete = spreadSheetTrophics.SelectedRows.Count;
            while (rowsToDelete > 0) {
                ClearDietLogRow(spreadSheetTrophics.SelectedRows[0]);
                spreadSheetTrophics.Rows.Remove(spreadSheetTrophics.SelectedRows[0]);
                rowsToDelete--;
            }

            IsChanged = true;
            UpdateTrophicsTab();
        }

        #endregion

        private void speciesTrophics_SpeciesSelected(object sender, SpeciesSelectEventArgs e) {
            HandleTrophicsRow(e.Row);
            value_Changed(spreadSheetTrophics, e);
        }

        private void currentSectionToolStripMenuItem_Click(object sender, EventArgs e) {
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

        private void buttonTrophics_Click(object sender, EventArgs e) {
            contextSaveTrophics.Show(buttonTrophics, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void allSectionsToolStripMenuItem_Click(object sender, EventArgs e) {
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

        private void buttonLoadData_Click(object sender, EventArgs e) {
            if (Interface.OpenDialog.ShowDialog() == DialogResult.OK) {
                foreach (string filename in Interface.OpenDialog.FileNames) {
                    LoadTrophicData(filename);
                }

                IsChanged = true;
            }
        }

        private void GridLog_DragDrop(object sender, DragEventArgs e) {
            spreadSheetTrophics.Enabled = false;

            foreach (string filename in IO.MaskedNames(
                (string[])e.Data.GetData(DataFormats.FileDrop),
                Wild.UserSettings.Interface.OpenExtensions)) {
                LoadTrophicData(filename);
            }

            IsChanged = true;
            spreadSheetTrophics.Enabled = true;
        }

        private void GridLog_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        #endregion

        #endregion

        #region Parasites tab

        private void comboBoxOrgan_SelectedIndexChanged(object sender, EventArgs e) {
            spreadSheetInfection.Enabled = comboBoxOrgan.SelectedIndex != -1;

            SaveOrgan();

            // Save current set values
            if (comboBoxOrgan.SelectedIndex == -1) {
                ClearOrgan();
            } else {
                // Change part index to selected
                SelectedOrgan = comboBoxOrgan.SelectedIndex;

                // Load new values if exist
                Wild.Survey.OrganRow infectedOrganRow = Data.Organ.FindByIndIDOrgan(IndividualRow.ID, SelectedOrgan);

                if (infectedOrganRow == null) {
                    ClearOrgan();
                } else {
                    LoadOrgan(infectedOrganRow);
                }
            }
        }

        private void GridParasites_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            HandleParasiteRow(spreadSheetInfection.Rows[e.RowIndex]);
            value_Changed(sender, new EventArgs());
        }

        private void speciesParasites_SpeciesSelected(object sender, SpeciesSelectEventArgs e) {
            HandleParasiteRow(e.Row);
            value_Changed(sender, e);
        }

        #endregion



        private void buttonBlank_Click(object sender, EventArgs e) {
            FishReport.BlankIndividualProfile.Run();
        }

        private void buttonReport_Click(object sender, EventArgs e) {
            SaveData();
            IndividualRow.GetReport().Run();
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            SaveData();
            IndividualRow.AcceptChanges();
            IsChanged = false;
            Close();
        }

        private void numericLength_TextChanged(object sender, EventArgs e) {
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