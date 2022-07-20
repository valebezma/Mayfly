using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Waters;
using Mayfly.Waters.Controls;
using Mayfly.Wild;
using System;
using System.Windows.Forms;

namespace Mayfly.Plankton
{
    public class PlanktonCard : Wild.Card
    {
        private System.Windows.Forms.ComboBox comboBoxBank;
        private System.Windows.Forms.ComboBox comboBoxCrossSection;
        private System.Windows.Forms.Label labelCrossSection;
        private System.Windows.Forms.Label labelCapacity;
        private System.Windows.Forms.Label labelDiameter;
        private System.Windows.Forms.Label labelSieve;
        private Controls.NumberBox numericCapacity;
        private Controls.NumberBox numericSieve;
        private Controls.NumberBox numericDiameter;
        private System.Windows.Forms.Label label17;
        private Controls.NumberBox numericPortions;
        private System.Windows.Forms.Label labelExposure;
        private System.Windows.Forms.Label labelPortions;
        private System.Windows.Forms.Label labelVolume;
        private Controls.NumberBox numericExposure;
        private Controls.NumberBox numericVolume;
        private NumberBox numericDepth;
        private Label labelDepth;
        private Label labelSampled;
        private NumberBox numericSampled;
        private Label label2;
        private Label labelExamined;
        private NumberBox numericExamined;
        private CheckBox checkBoxWeighting;
        private System.Windows.Forms.Label labelBank;

        public PlanktonCard() : base() {

            InitializeComponent();
            Initiate();

            ColumnMass.Visible = false;
        }

        public PlanktonCard(string filename) : this() {
            load(filename);
        }

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanktonCard));
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.labelBank = new System.Windows.Forms.Label();
            this.labelCrossSection = new System.Windows.Forms.Label();
            this.comboBoxCrossSection = new System.Windows.Forms.ComboBox();
            this.numericSieve = new Mayfly.Controls.NumberBox();
            this.numericDiameter = new Mayfly.Controls.NumberBox();
            this.numericCapacity = new Mayfly.Controls.NumberBox();
            this.labelSieve = new System.Windows.Forms.Label();
            this.labelDiameter = new System.Windows.Forms.Label();
            this.labelCapacity = new System.Windows.Forms.Label();
            this.numericVolume = new Mayfly.Controls.NumberBox();
            this.numericExposure = new Mayfly.Controls.NumberBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelPortions = new System.Windows.Forms.Label();
            this.labelExposure = new System.Windows.Forms.Label();
            this.numericPortions = new Mayfly.Controls.NumberBox();
            this.label17 = new System.Windows.Forms.Label();
            this.labelDepth = new System.Windows.Forms.Label();
            this.numericDepth = new Mayfly.Controls.NumberBox();
            this.numericSampled = new Mayfly.Controls.NumberBox();
            this.labelSampled = new System.Windows.Forms.Label();
            this.numericExamined = new Mayfly.Controls.NumberBox();
            this.labelExamined = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxWeighting = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.comboBoxBank);
            this.tabPageCollect.Controls.Add(this.comboBoxCrossSection);
            this.tabPageCollect.Controls.Add(this.labelCrossSection);
            this.tabPageCollect.Controls.Add(this.numericDepth);
            this.tabPageCollect.Controls.Add(this.labelDepth);
            this.tabPageCollect.Controls.Add(this.labelBank);
            this.tabPageCollect.Controls.SetChildIndex(this.labelComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelBank, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelWater, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelDepth, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.numericDepth, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelCrossSection, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waterSelector, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBoxCrossSection, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBoxBank, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waypointControl1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelTag, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelPosition, 0);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.checkBoxWeighting);
            this.tabPageLog.Controls.SetChildIndex(this.buttonAdd, 0);
            this.tabPageLog.Controls.SetChildIndex(this.checkBoxWeighting, 0);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            // 
            // labelComments
            // 
            resources.ApplyResources(this.labelComments, "labelComments");
            // 
            // waypointControl1
            // 
            resources.ApplyResources(this.waypointControl1, "waypointControl1");
            // 
            // waterSelector
            // 
            this.waterSelector.WaterSelected += new Mayfly.Waters.Controls.WaterEventHandler(this.waterSelector_WaterSelected);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.label2);
            this.tabPageSampler.Controls.Add(this.label17);
            this.tabPageSampler.Controls.Add(this.numericPortions);
            this.tabPageSampler.Controls.Add(this.labelExposure);
            this.tabPageSampler.Controls.Add(this.labelCapacity);
            this.tabPageSampler.Controls.Add(this.labelPortions);
            this.tabPageSampler.Controls.Add(this.labelDiameter);
            this.tabPageSampler.Controls.Add(this.labelExamined);
            this.tabPageSampler.Controls.Add(this.labelSampled);
            this.tabPageSampler.Controls.Add(this.labelVolume);
            this.tabPageSampler.Controls.Add(this.numericExposure);
            this.tabPageSampler.Controls.Add(this.labelSieve);
            this.tabPageSampler.Controls.Add(this.numericExamined);
            this.tabPageSampler.Controls.Add(this.numericSampled);
            this.tabPageSampler.Controls.Add(this.numericVolume);
            this.tabPageSampler.Controls.Add(this.numericCapacity);
            this.tabPageSampler.Controls.Add(this.numericSieve);
            this.tabPageSampler.Controls.Add(this.numericDiameter);
            this.tabPageSampler.Controls.SetChildIndex(this.numericDiameter, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericSieve, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericCapacity, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericVolume, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericSampled, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericExamined, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSieve, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelVolume, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampled, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelExamined, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelDiameter, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelPortions, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelCapacity, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelExposure, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.numericPortions, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonEquipment, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label17, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label2, 0);
            // 
            // labelPosition
            // 
            resources.ApplyResources(this.labelPosition, "labelPosition");
            // 
            // textBoxComments
            // 
            resources.ApplyResources(this.textBoxComments, "textBoxComments");
            // 
            // comboBoxSampler
            // 
            resources.ApplyResources(this.comboBoxSampler, "comboBoxSampler");
            // 
            // buttonEquipment
            // 
            resources.ApplyResources(this.buttonEquipment, "buttonEquipment");
            // 
            // Logger
            // 
            this.Logger.IndividualsRequired += new System.EventHandler(this.logger_IndividualsRequired);
            // 
            // comboBoxBank
            // 
            resources.ApplyResources(this.comboBoxBank, "comboBoxBank");
            this.comboBoxBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBank.FormattingEnabled = true;
            this.comboBoxBank.Items.AddRange(new object[] {
            resources.GetString("comboBoxBank.Items"),
            resources.GetString("comboBoxBank.Items1")});
            this.comboBoxBank.Name = "comboBoxBank";
            // 
            // labelBank
            // 
            resources.ApplyResources(this.labelBank, "labelBank");
            this.labelBank.Name = "labelBank";
            // 
            // labelCrossSection
            // 
            resources.ApplyResources(this.labelCrossSection, "labelCrossSection");
            this.labelCrossSection.Name = "labelCrossSection";
            // 
            // comboBoxCrossSection
            // 
            resources.ApplyResources(this.comboBoxCrossSection, "comboBoxCrossSection");
            this.comboBoxCrossSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrossSection.FormattingEnabled = true;
            this.comboBoxCrossSection.Name = "comboBoxCrossSection";
            this.comboBoxCrossSection.SelectedIndexChanged += new System.EventHandler(this.comboBoxCrossSection_SelectedIndexChanged);
            // 
            // numericSieve
            // 
            resources.ApplyResources(this.numericSieve, "numericSieve");
            this.numericSieve.Maximum = 5000D;
            this.numericSieve.Minimum = 0D;
            this.numericSieve.Name = "numericSieve";
            this.numericSieve.Value = -1D;
            this.numericSieve.ValueChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // numericDiameter
            // 
            resources.ApplyResources(this.numericDiameter, "numericDiameter");
            this.numericDiameter.Maximum = 1000D;
            this.numericDiameter.Minimum = 0D;
            this.numericDiameter.Name = "numericDiameter";
            this.numericDiameter.Value = -1D;
            this.numericDiameter.ValueChanged += new System.EventHandler(this.virtue_Changed);
            this.numericDiameter.TextChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // numericCapacity
            // 
            resources.ApplyResources(this.numericCapacity, "numericCapacity");
            this.numericCapacity.Maximum = 10000D;
            this.numericCapacity.Minimum = 0D;
            this.numericCapacity.Name = "numericCapacity";
            this.numericCapacity.Value = -1D;
            this.numericCapacity.ValueChanged += new System.EventHandler(this.virtue_Changed);
            // 
            // labelSieve
            // 
            resources.ApplyResources(this.labelSieve, "labelSieve");
            this.labelSieve.Name = "labelSieve";
            // 
            // labelDiameter
            // 
            resources.ApplyResources(this.labelDiameter, "labelDiameter");
            this.labelDiameter.Name = "labelDiameter";
            // 
            // labelCapacity
            // 
            resources.ApplyResources(this.labelCapacity, "labelCapacity");
            this.labelCapacity.Name = "labelCapacity";
            // 
            // numericVolume
            // 
            resources.ApplyResources(this.numericVolume, "numericVolume");
            this.numericVolume.Maximum = 100000D;
            this.numericVolume.Minimum = 0D;
            this.numericVolume.Name = "numericVolume";
            this.numericVolume.ReadOnly = true;
            this.numericVolume.Value = -1D;
            this.numericVolume.ValueChanged += new System.EventHandler(this.numericVolume_ValueChanged);
            // 
            // numericExposure
            // 
            resources.ApplyResources(this.numericExposure, "numericExposure");
            this.numericExposure.Maximum = 100D;
            this.numericExposure.Minimum = 0D;
            this.numericExposure.Name = "numericExposure";
            this.numericExposure.Value = -1D;
            this.numericExposure.ValueChanged += new System.EventHandler(this.effort_Changed);
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // labelPortions
            // 
            resources.ApplyResources(this.labelPortions, "labelPortions");
            this.labelPortions.Name = "labelPortions";
            // 
            // labelExposure
            // 
            resources.ApplyResources(this.labelExposure, "labelExposure");
            this.labelExposure.Name = "labelExposure";
            // 
            // numericPortions
            // 
            resources.ApplyResources(this.numericPortions, "numericPortions");
            this.numericPortions.Maximum = 100D;
            this.numericPortions.Minimum = 0D;
            this.numericPortions.Name = "numericPortions";
            this.numericPortions.Value = -1D;
            this.numericPortions.ValueChanged += new System.EventHandler(this.effort_Changed);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label17.Name = "label17";
            // 
            // labelDepth
            // 
            resources.ApplyResources(this.labelDepth, "labelDepth");
            this.labelDepth.Name = "labelDepth";
            // 
            // numericDepth
            // 
            resources.ApplyResources(this.numericDepth, "numericDepth");
            this.numericDepth.Maximum = 100D;
            this.numericDepth.Minimum = 0D;
            this.numericDepth.Name = "numericDepth";
            this.numericDepth.Value = -1D;
            // 
            // numericSampled
            // 
            resources.ApplyResources(this.numericSampled, "numericSampled");
            this.numericSampled.Maximum = 100D;
            this.numericSampled.Minimum = 0D;
            this.numericSampled.Name = "numericSampled";
            this.numericSampled.Value = -1D;
            this.numericSampled.ValueChanged += new System.EventHandler(this.numericSampled_ValueChanged);
            // 
            // labelSampled
            // 
            resources.ApplyResources(this.labelSampled, "labelSampled");
            this.labelSampled.Name = "labelSampled";
            // 
            // numericExamined
            // 
            resources.ApplyResources(this.numericExamined, "numericExamined");
            this.numericExamined.Maximum = 100D;
            this.numericExamined.Minimum = 0D;
            this.numericExamined.Name = "numericExamined";
            this.numericExamined.Value = -1D;
            this.numericExamined.ValueChanged += new System.EventHandler(this.numericExamined_ValueChanged);
            // 
            // labelExamined
            // 
            resources.ApplyResources(this.labelExamined, "labelExamined");
            this.labelExamined.Name = "labelExamined";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Name = "label2";
            // 
            // checkBoxWeighting
            // 
            resources.ApplyResources(this.checkBoxWeighting, "checkBoxWeighting");
            this.checkBoxWeighting.Name = "checkBoxWeighting";
            this.checkBoxWeighting.UseVisualStyleBackColor = true;
            this.checkBoxWeighting.CheckedChanged += new System.EventHandler(this.checkBoxWeighting_CheckedChanged);
            // 
            // PlanktonCard
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "PlanktonCard";
            this.OnSaved += new System.EventHandler(this.planktonCard_OnSaved);
            this.OnCleared += new System.EventHandler(this.planktonCard_OnCleared);
            this.OnEquipmentSaved += new Mayfly.Wild.EquipmentEventHandler(this.planktonCard_OnEquipmentSaved);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void clearSampler() {

            numericDiameter.Clear();
            numericCapacity.Clear();
            numericSieve.Clear();
        }

        private void clearEffort() {

            numericExposure.Clear();
            numericPortions.Clear();
            numericSampled.Clear();
            numericExamined.Clear();
        }

        private void saveEffort() {

            if (numericDepth.IsSet) {
                data.Solitary.Depth = numericDepth.Value;
            } else {
                data.Solitary.SetDepthNull();
            }

            if (SelectedSampler.EffortType == (int)EffortType.Portion) {
                if (numericPortions.IsSet) {
                    data.Solitary.Effort = numericPortions.Value;
                } else {
                    data.Solitary.SetEffortNull();
                }
            } else if (SelectedSampler.EffortType == (int)EffortType.Exposure) {
                if (numericExposure.IsSet) {
                    data.Solitary.Effort = numericExposure.Value;
                } else {
                    data.Solitary.SetEffortNull();
                }
            }

            if (numericSampled.IsSet) {
                data.Solitary.SetSampledNull();
            } else {
                data.Solitary.Sampled = numericSampled.Value;
            }

            if (numericExamined.IsSet) {
                data.Solitary.SetExaminedNull();
            } else {
                data.Solitary.Examined = numericExamined.Value;
            }
        }



        private void planktonCard_OnCleared(object sender, EventArgs e) {

            clearSampler();
            clearEffort();
        }

        private void planktonCard_OnEquipmentSaved(object sender, EquipmentEventArgs e) {

            effort_Changed(sender, e);
        }

        private void planktonCard_OnSaved(object sender, EventArgs e) {

        }

        private void waterSelector_WaterSelected(object sender, WaterEventArgs e) {

            WaterType type = waterSelector.IsWaterSelected ? waterSelector.WaterObject.WaterType : WaterType.None;
            comboBoxCrossSection.Items.Clear();
            comboBoxCrossSection.Items.AddRange(Wild.Service.CrossSection(type));
            comboBoxBank.Enabled = type != WaterType.Lake;
        }


        private void logger_IndividualsRequired(object sender, EventArgs e) {

            foreach (DataGridViewRow gridRow in spreadSheetLog.SelectedRows) {
                if (gridRow.Cells[ColumnDefinition.Name].Value != null) {

                    Survey.LogRow logRow = Logger.SaveLogRow(gridRow);
                    Individuals individuals = null;

                    foreach (Form form in Application.OpenForms) {
                        if (!(form is Individuals)) continue;
                        if (((Individuals)form).LogRow == logRow) {
                            individuals = (Individuals)form;
                        }
                    }

                    if (individuals == null) {
                        individuals = new Individuals(logRow);
                        individuals.SetColumns(ColumnDefinition, ColumnQty, ColumnMass);
                        individuals.LogLine = gridRow;
                        individuals.SetFriendlyDesktopLocation(spreadSheetLog);
                        individuals.FormClosing += new FormClosingEventHandler(individuals_FormClosing);
                        individuals.Show(this);
                    } else {
                        individuals.BringToFront();
                    }
                }
            }
        }

        private void individuals_FormClosing(object sender, FormClosingEventArgs e) {

            Individuals individuals = sender as Individuals;
            if (individuals.DialogResult == DialogResult.OK) {
                isChanged |= individuals.ChangesWereMade;
            }
        }


        private void comboBoxCrossSection_SelectedIndexChanged(object sender, EventArgs e) {

            if (comboBoxCrossSection.SelectedIndex == -1 ||
                comboBoxCrossSection.SelectedIndex == comboBoxCrossSection.Items.Count - 1) {
                labelBank.Enabled = comboBoxBank.Enabled = false;
            } else {
                labelBank.Enabled = comboBoxBank.Enabled = true;
            }

            value_Changed(sender, e);
        }

        private void virtue_Changed(object sender, EventArgs e) {

            if (SelectedSampler == null) return;

            saveSampler();

        }

        private void effort_Changed(object sender, EventArgs e) {

            saveEffort();
            numericVolume.Value = data.Solitary.GetVolume();
            numericVolume.Format = "N1";
            isChanged = true;

        }

        private void numericVolume_ValueChanged(object sender, EventArgs e) {

            numericSampled.Maximum = numericVolume.Value * 1000;
        }

        private void numericSampled_ValueChanged(object sender, EventArgs e) {

            numericExamined.Enabled = labelExamined.Enabled = numericSampled.IsSet;
            numericExamined.Maximum = numericSampled.Value;
        }

        private void numericExamined_ValueChanged(object sender, EventArgs e) {

            ColumnQtyExam.Visible = numericExamined.IsSet;
            ColumnMassExam.Visible = numericExamined.IsSet && checkBoxWeighting.Checked;
        }

        private void checkBoxWeighting_CheckedChanged(object sender, EventArgs e) {

            ColumnMass.Visible = checkBoxWeighting.Checked;
            ColumnMassExam.Visible = numericExamined.IsSet && checkBoxWeighting.Checked;
        }
    }
}
