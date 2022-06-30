using AeroWizard;
using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Wild;
using Mayfly.Fish.Explorer;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardGearSet : Form
    {
        public CardStack Data { get; set; }

        public SpeciesKey.TaxonRow SpeciesOfInterest { get; set; }

        public FishSamplerType SelectedSamplerType { get; set; }

        public UnitEffort SelectedUnit { get; set; }
        
        private List<CardStack> classedStacks;

        public List<CardStack> SelectedStacks;

        public List<double> SelectedSpatialWeights;

        public CardStack GearData { get; set; }

        public CardStack SelectedData { get; set; }

        //public CardStack UnclassedData { get; set; }

        public double TotalEffort { get; set; }

        public bool IsMultipleClasses { get { return classedStacks.Count > 1; } }

        public bool IsSpatialOn { get { return WeightType.HasFlag(GearWeightType.SpatialRatio); } }

        public bool AllowSpatialRatios { set { checkBoxSpatial.Enabled = value; } }

        public GearWeightType WeightType = GearWeightType.Effort;


        public int DatasetIndex;

        //public int SelectedCount { get { return SelectedStacks.Count; } }
        public int SelectedCount { get { return SelectedSpatialWeights.Count; } }

        public CardStack CurrentStack { get { return SelectedStacks[DatasetIndex]; } }

        public double CurrentEffort { get { return CurrentStack.GetEffort(SelectedSamplerType, SelectedUnit.Variant); } }
        //public double CurrentSpatialWeight { get { return SelectedSpatialWeights[DatasetIndex]; } }



        public string AbundanceUnits { get { return Resources.Reports.Common.Ind + " / " + SelectedUnit.Unit; } }

        public string BiomassUnits { get { return Resources.Reports.Common.Kg + " / " + SelectedUnit.Unit; } }



        public bool AllowFinish
        {
            set
            {
                pageEfforts.AllowNext = value;
            }
        }

        public event EventHandler EffortUnitChanged;

        public event EventHandler AfterEffortCalculated;

        public event EventHandler AfterDataSelected;

        public event EventHandler Returned;



        private WizardGearSet()
        {
            InitializeComponent();

            ColumnSpatialWeight.ValueType =
                ColumnEffort.ValueType =
                ColumnEffortP.ValueType =
                typeof(double);

            ColumnClass.ValueType = 
                typeof(string);

            ColumnOperations.ValueType =
                typeof(int);

            this.RestoreAllCheckStates();
        }

        public WizardGearSet(CardStack data)
            : this()
        {
            Data = data;
            
            comboBoxGearType.DataSource = Data.GetSamplerTypeDisplays();
            comboBoxGearType.SelectedIndex = -1;
            comboBoxGearType.SelectedIndex = comboBoxGearType.FindString(new FishSamplerTypeDisplay(UserSettings.MemorizedSamplerType).ToString());
            if (comboBoxGearType.SelectedValue == null) { comboBoxGearType.SelectedIndex = 0; }

            //pageSampler.AllowNext = (comboBoxUE.SelectedIndex != -1);
        }

        public WizardGearSet(CardStack data, SpeciesKey.TaxonRow speciesRow)
            : this(data)
        {
            SpeciesOfInterest = speciesRow;
        }


        private DataGridViewRow DatasetRow(string title, CardStack data)
        {

            double effort = data.GetEffort(SelectedSamplerType, SelectedUnit.Variant);

            if (effort > 0)
            {
                classedStacks.Add(data);

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetEfforts);
                gridRow.Cells[ColumnClass.Index].Value = title;
                gridRow.Cells[ColumnOperations.Index].Value = data.Count;
                gridRow.Cells[ColumnEffort.Index].Value = effort;
                gridRow.Cells[ColumnSpatialWeight.Index].Value = Service.GetGearSpatialValue(SelectedSamplerType, title);

                gridRow.Tag = data;

                return gridRow;
            }
            else
            {
                return null;
            }
        }


        public void AddEffortSection(Report report)
        {
            report.AddSectionTitle(Resources.Reports.Sections.Efforts.Header);

            if (SelectedStacks.Count > 1)
            {
                string[] classes = SelectedData.SamplerClasses(SelectedSamplerType);
                report.AddParagraph(Resources.Reports.Sections.Efforts.Paragraph1,
                    SelectedSamplerType.ToDisplay(),
                    classes.Length, classes[0], classes.Last(),
                    SelectedUnit.UnitDescription, report.NextTableNumber);

                report.AddTable(SelectedStacks.GetEffortsTable(SelectedSamplerType, SelectedUnit));
            }
            else
            {
                report.AddParagraph(Resources.Reports.Sections.Efforts.Paragraph2,
                    SelectedSamplerType.ToDisplay(), SelectedUnit.UnitDescription,
                    SelectedData.GetEffort(SelectedSamplerType, SelectedUnit.Variant), SelectedUnit.Unit);
            }


            //SelectedData.AddEffortsSection(report, SelectedSamplerType, SelectedUnit);
        }


        private void RecalculateEfforts()
        {
            SelectedSpatialWeights = new List<double>();
            TotalEffort = 0;

            foreach (DataGridViewRow gridRow in spreadSheetEfforts.Rows)
            {
                if (spreadSheetEfforts.IsHidden(gridRow)) continue;

                TotalEffort += (double)gridRow.Cells[ColumnEffort.Index].Value;
                SelectedSpatialWeights.Add((double)gridRow.Cells[ColumnSpatialWeight.Index].Value);
            }

            foreach (DataGridViewRow gridRow in spreadSheetEfforts.Rows)
            {
                if (spreadSheetEfforts.IsHidden(gridRow))
                {
                    gridRow.Cells[ColumnEffortP.Index].Value = null;
                }
                else
                {
                    gridRow.Cells[ColumnEffortP.Index].Value =
                        (double)gridRow.Cells[ColumnEffort.Index].Value / TotalEffort;
                }
            }

            textBoxEffort.Text = TotalEffort.ToString(ColumnEffort.DefaultCellStyle.Format);
            pageEfforts.AllowNext = TotalEffort > 0;
        }

        private void ResetIcons()
        {
            if (SpeciesOfInterest == null) return;

            for (int i = 0; i < classedStacks.Count; i++)
            {
                bool speciesOfInterestIsPresent = classedStacks[i].GetSpecies().Contains(SpeciesOfInterest);

                ((Controls.TextAndImageCell)spreadSheetEfforts[ColumnClass.Index, i]).Image =
                    speciesOfInterestIsPresent ? null : Mathematics.Properties.Resources.None;

                spreadSheetEfforts[ColumnClass.Index, i].ToolTipText =
                    speciesOfInterestIsPresent ? string.Empty : 
                    string.Format(Resources.Interface.SpeciesOfInterestIsAbsent, SpeciesOfInterest.CommonName, 
                    classedStacks[i].Name, spreadSheetEfforts.RowVisibilityKey);

            }
        }

       

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Mayfly.Controls.Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }



        private void wizardPage1_Initialize(object sender, WizardPageInitEventArgs e)
        {
            wizardExplorer.NextPage();
        }

        private void PageEfforts_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            e.Cancel = true;

            if (Returned != null)
            {
                Returned.Invoke(sender, e);
            }
        }

        private void comboBoxGear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGearType.SelectedValue == null) { return; }
            SelectedSamplerType = ((FishSamplerTypeDisplay)comboBoxGearType.SelectedValue).Type;
            UnitEffort.SwitchUE(comboBoxUE, SelectedSamplerType);
        }

        private void comboBoxUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUE.SelectedIndex != -1)
            {
                SelectedUnit = (UnitEffort)comboBoxUE.SelectedItem;
                ColumnEffort.ResetFormatted(SelectedUnit.Unit);
                labelEffortTotal.ResetFormatted(SelectedUnit.Unit);
            }

            //pageSampler.AllowNext = (comboBoxUE.SelectedIndex != -1);

            if (EffortUnitChanged != null)
            {
                EffortUnitChanged.Invoke(sender, e);
            }

            pageEfforts.SetNavigation(false);

            spreadSheetEfforts.Rows.Clear();
            labelNoticeGears.Text = Wild.Resources.Interface.Interface.GetData;

            comboBoxGearType.Enabled = comboBoxUE.Enabled = comboBoxGearType.Enabled = false;
            labelNoticeGears.Visible = true;

            calculatorEffort.RunWorkerAsync();
        }

        private void effortCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            GearData = Data.GetStack(SelectedSamplerType);//, SelectedUnit.Variant);
            classedStacks = new List<CardStack>();
            List<DataGridViewRow> result = new List<DataGridViewRow>();
            string[] classes = GearData.Classes(SelectedSamplerType);

            //UnclassedData = new CardStack(); // GearData.GetStack(SelectedSamplerType, 0);
            //UnclassedData = Data.GetStack(SelectedSamplerType);

            foreach (string gearclass in classes)
            {
                CardStack stack = GearData.GetStack(SelectedSamplerType, gearclass);
                DataGridViewRow gridRow = DatasetRow(stack.Name, stack);
                if (gridRow != null) {
                    result.Add(gridRow);
                }
                worker.ReportProgress((int)((double)result.Count * 100d / (double)classes.Length));
            }

            if (result.Count == 0)
            {
                DataGridViewRow gridRow = DatasetRow(Resources.Interface.AllDataCombined, GearData);
                if (gridRow != null) { result.Add(gridRow); }
            }

            e.Result = result.ToArray();
        }

        private void effortCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataGridViewRow[] result = e.Result as DataGridViewRow[];

            if (result.Length > 0)
            {
                spreadSheetEfforts.Rows.AddRange(result);
                labelNoticeGears.Visible = false;
            }
            else
            {
                labelNoticeGears.Text = Wild.Resources.Interface.Interface.NoData;
            }

            comboBoxGearType.Enabled = comboBoxUE.Enabled = true;

            pageEfforts.SetNavigation(true);

            RecalculateEfforts();
            ResetIcons();

            if (AfterEffortCalculated != null)
            {
                AfterEffortCalculated.Invoke(sender, e);
            }
        }        

        private void checkBoxSpatial_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBoxSpatial.Enabled) checkBoxSpatial.Checked = false;
        }

        private void checkBoxSpatial_CheckedChanged(object sender, EventArgs e)
        {
            ColumnSpatialWeight.Visible = checkBoxSpatial.Checked;

            WeightType = checkBoxSpatial.Checked ? (GearWeightType.Effort | GearWeightType.SpatialRatio)
                : GearWeightType.Effort;
        }

        private void spreadSheetEfforts_UserChangedRowVisibility(object sender, DataGridViewRowEventArgs e)
        {
            //TotalEffort = ColumnEffort.GetDoubles().Sum();
            RecalculateEfforts();
        }

        private void spreadSheetEfforts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnClass.Index)
            {
                classedStacks[e.RowIndex].Name = (string)spreadSheetEfforts[e.ColumnIndex, e.RowIndex].Value;
            }
        }

        private void contextMenuStripSubData_Opening(object sender, CancelEventArgs e)
        {
            contextEffortMerge.Enabled = spreadSheetEfforts.SelectedRows.Count > 1;
        }

        private void ToolStripMenuItemMergeSelected_Click(object sender, EventArgs e)
        {
            CardStack mergedData = new CardStack();

            //string title = string.Empty;
            int groups = spreadSheetEfforts.SelectedRows.Count;
            int groupsCount = spreadSheetEfforts.SelectedRows.Count;
            int insertionPosition = spreadSheetEfforts.RowCount;
            double value = 0;

            string firstName = string.Empty;
            string lastName = string.Empty;

            for (int i = 0; i < spreadSheetEfforts.RowCount; i++)
            {
                if (spreadSheetEfforts.Rows[i].Selected)
                {
                    if (i < insertionPosition) insertionPosition = i;

                    if (string.IsNullOrEmpty(firstName)) firstName = classedStacks[i].Name;
                    lastName = classedStacks[i].Name;

                    mergedData.Merge(classedStacks[i]);
                    //title += classedStacks[i].Name;
                    value += (double)spreadSheetEfforts[ColumnSpatialWeight.Index, i].Value;

                    spreadSheetEfforts.Rows.RemoveAt(i);
                    classedStacks.RemoveAt(i);

                    i--;
                    groupsCount--;
                    if (groupsCount == 0) break;
                }
            }

            value /= groups;

            double effort = mergedData.GetEffort(SelectedSamplerType, SelectedUnit.Variant);

            mergedData.Name = string.Format("{0}—{1}", firstName, lastName);
            classedStacks.Insert(insertionPosition, mergedData);

            DataGridViewRow gridRow = new DataGridViewRow();
            gridRow.CreateCells(spreadSheetEfforts);
            gridRow.Cells[ColumnClass.Index].Value = mergedData.Name;
            gridRow.Cells[ColumnOperations.Index].Value = mergedData.Count;
            gridRow.Cells[ColumnEffort.Index].Value = effort;

            gridRow.Cells[ColumnSpatialWeight.Index].Value = value;

            gridRow.Tag = mergedData;
            spreadSheetEfforts.Rows.Insert(insertionPosition, gridRow);
            spreadSheetEfforts.ClearSelection();
            gridRow.Selected = true;

            RecalculateEfforts();
        }

        private void pageEfforts_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            e.Cancel = true;

            if (calculatorSelection.IsBusy) return;
            calculatorSelection.RunWorkerAsync();
        }

        private void calculatorSelection_DoWork(object sender, DoWorkEventArgs e)
        {
            SelectedData = new CardStack();
            SelectedStacks = new List<CardStack>();

            foreach (DataGridViewRow gridRow in spreadSheetEfforts.Rows)
            {
                if (spreadSheetEfforts.IsHidden(gridRow)) continue;

                SelectedStacks.Add(classedStacks[gridRow.Index]);
                SelectedData.Merge(classedStacks[gridRow.Index]);

                ((BackgroundWorker)sender).ReportProgress((int)(gridRow.Index * 100D / (spreadSheetEfforts.RowCount - 1)));
            }

            ////TODO: Should I include Unclassed cards to SelectedData?
            //// In case when I should:

            //foreach (Data.CardRow cardRow in GearData)
            //{
            //    if (cardRow.IsMeshNull())
            //    {
            //        SelectedData.Add(cardRow);
            //    }
            //}
        }

        private void calculatorSelection_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RecalculateEfforts();

            for (int i = 0; i < SelectedCount; i++)
            {
                Service.SaveGearSpatialValue(SelectedSamplerType,
                    SelectedStacks[i].Name,
                    SelectedSpatialWeights[i]);
            }

            if (AfterDataSelected != null)
            {
                AfterDataSelected.Invoke(sender, e);
            }

            Log.Write("Gear set is specified: {0}.",
                this.SelectedData.SamplerClasses(this.SelectedSamplerType).Merge());

            UserSettings.MemorizedSamplerType = ((FishSamplerTypeDisplay)comboBoxGearType.SelectedValue).Type;
        }
    }
}