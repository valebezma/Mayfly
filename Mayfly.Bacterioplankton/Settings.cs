using Mayfly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Bacterioplankton
{
    public class Settings : Mayfly.Wild.Settings
    {
        protected Label label3;
        private Label label4;
        private NumericUpDown numericUpDownSquare;
        private Label label7;
        private NumericUpDown numericUpDownConsistence;
        private TabPage tabPageValues;

        public Settings()
            : base()
        {
            SaveSettings = saveSettings;

            #region references

            textBoxWaters.Text = Wild.UserSettings.WatersIndexPath;
            textBoxSpecies.Text = UserSettings.SpeciesIndexPath;

            //checkBoxSpeciesExpand.Checked = UserSettings.SpeciesAutoExpand;
            //checkBoxSpeciesExpandVisualControl.Checked = UserSettings.SpeciesAutoExpandVisual;

            #endregion

            #region input

            checkBoxAutoLog.Checked = UserSettings.AutoLogOpen;
            checkBoxFixTotals.Checked = UserSettings.FixTotals;
            checkBoxAutoIncreaseBio.Checked = UserSettings.AutoIncreaseBio;
            checkBoxAutoDecreaseBio.Checked = UserSettings.AutoDecreaseBio;

            foreach (string item in UserSettings.AddtVariables)
            {
                ListViewItem li = new ListViewItem(item);
                listViewAddtVars.Items.Add(li);
            }

            foreach (ListViewItem item in listViewAddtVars.Items)
            {
                if (UserSettings.CurrentVariables.Contains(item.Text))
                    item.Checked = true;
            }

            numericUpDownRecentCount.Value = UserSettings.RecentSpeciesCount;

            #endregion

            #region print

            checkBoxCardOdd.Checked = UserSettings.OddCardStart;
            checkBoxBreakBeforeIndividuals.Checked = UserSettings.BreakBeforeIndividuals;
            checkBoxBreakBetweenSpecies.Checked = UserSettings.BreakBetweenSpecies;

            #endregion

            InitializeComponent();

            numericUpDownSquare.Value = (decimal)UserSettings.Square;
            numericUpDownConsistence.Value = (decimal)UserSettings.Consistence;
        }

        private void saveSettings()
        {
            #region References

            Wild.UserSettings.WatersIndexPath = textBoxWaters.Text;
            UserSettings.SpeciesIndexPath = textBoxSpecies.Text;

            //UserSettings.SpeciesAutoExpand = checkBoxSpeciesExpand.Checked;
            //UserSettings.SpeciesAutoExpandVisual = checkBoxSpeciesExpandVisualControl.Checked;

            #endregion

            #region Input

            UserSettings.FixTotals = checkBoxFixTotals.Checked;
            UserSettings.AutoLogOpen = checkBoxAutoLog.Checked;
            UserSettings.AutoIncreaseBio = checkBoxAutoIncreaseBio.Checked;
            UserSettings.AutoDecreaseBio = checkBoxAutoDecreaseBio.Checked;

            UserSettings.RecentSpeciesCount = (int)numericUpDownRecentCount.Value;

            List<string> addvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.Items)
                addvars.Add(item.Text);
            UserSettings.AddtVariables = addvars.ToArray();

            List<string> currvars = new List<string>();
            foreach (ListViewItem item in listViewAddtVars.CheckedItems)
                currvars.Add(item.Text);
            UserSettings.CurrentVariables = currvars.ToArray();

            #endregion

            #region Print

            UserSettings.OddCardStart = checkBoxCardOdd.Checked;
            UserSettings.BreakBeforeIndividuals = checkBoxBreakBeforeIndividuals.Checked;
            UserSettings.BreakBetweenSpecies = checkBoxBreakBetweenSpecies.Checked;

            #endregion

            UserSettings.Square = (double)numericUpDownSquare.Value;
            UserSettings.Consistence = (double)numericUpDownConsistence.Value;
        }

        private void InitializeComponent()
        {
            this.tabPageValues = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownConsistence = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSquare = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).BeginInit();
            this.tabPageReferences.SuspendLayout();
            this.tabPageIndividuals.SuspendLayout();
            this.tabPageFactors.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPagePrint.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSpecies.SuspendLayout();
            this.tabPageValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConsistence)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSquare)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPagePrint
            // 
            this.tabPagePrint.Size = new System.Drawing.Size(452, 381);
            // 
            // tabPageInput
            // 
            this.tabPageInput.Size = new System.Drawing.Size(452, 381);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageValues);
            this.tabControl1.Size = new System.Drawing.Size(416, 345);
            this.tabControl1.Controls.SetChildIndex(this.tabPageValues, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPageIndividuals, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPageSpecies, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPageFactors, 0);
            // 
            // checkBoxSpeciesExpand
            // 
            this.checkBoxSpeciesExpand.Visible = false;
            // 
            // checkBoxSpeciesExpandVisualControl
            // 
            this.checkBoxSpeciesExpandVisualControl.Visible = false;
            // 
            // tabPageValues
            // 
            this.tabPageValues.Controls.Add(this.label7);
            this.tabPageValues.Controls.Add(this.label4);
            this.tabPageValues.Controls.Add(this.numericUpDownConsistence);
            this.tabPageValues.Controls.Add(this.numericUpDownSquare);
            this.tabPageValues.Controls.Add(this.label3);
            this.tabPageValues.Location = new System.Drawing.Point(4, 22);
            this.tabPageValues.Name = "tabPageValues";
            this.tabPageValues.Padding = new System.Windows.Forms.Padding(25);
            this.tabPageValues.Size = new System.Drawing.Size(408, 320);
            this.tabPageValues.TabIndex = 15;
            this.tabPageValues.Text = "Local values";
            this.tabPageValues.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(45, 91);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Microbial consistence, mg/mm3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(45, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Filter suface, mm²";
            // 
            // numericUpDownConsistence
            // 
            this.numericUpDownConsistence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownConsistence.DecimalPlaces = 3;
            this.numericUpDownConsistence.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownConsistence.Location = new System.Drawing.Point(265, 89);
            this.numericUpDownConsistence.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownConsistence.Name = "numericUpDownConsistence";
            this.numericUpDownConsistence.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownConsistence.TabIndex = 4;
            this.numericUpDownConsistence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownConsistence.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownSquare
            // 
            this.numericUpDownSquare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSquare.DecimalPlaces = 2;
            this.numericUpDownSquare.Location = new System.Drawing.Point(265, 63);
            this.numericUpDownSquare.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSquare.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSquare.Name = "numericUpDownSquare";
            this.numericUpDownSquare.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownSquare.TabIndex = 2;
            this.numericUpDownSquare.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownSquare.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(28, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Local Values";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Name = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecentCount)).EndInit();
            this.tabPageReferences.ResumeLayout(false);
            this.tabPageReferences.PerformLayout();
            this.tabPageIndividuals.ResumeLayout(false);
            this.tabPageIndividuals.PerformLayout();
            this.tabPageFactors.ResumeLayout(false);
            this.tabPageFactors.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabPagePrint.ResumeLayout(false);
            this.tabPagePrint.PerformLayout();
            this.tabPageInput.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSpecies.ResumeLayout(false);
            this.tabPageSpecies.PerformLayout();
            this.tabPageValues.ResumeLayout(false);
            this.tabPageValues.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConsistence)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSquare)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
