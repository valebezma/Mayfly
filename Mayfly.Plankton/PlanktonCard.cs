using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayfly.Plankton
{
    public class PlanktonCard : Wild.Card
    {
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label labelPortion;
        private System.Windows.Forms.NumericUpDown numericUpDownPortion;
        private System.Windows.Forms.Label label1;

        public PlanktonCard() : base() {
            InitializeComponent();
            Initiate();
        }

        public PlanktonCard(string filename) : this() {
            load(filename);
        }

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanktonCard));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownPortion = new System.Windows.Forms.NumericUpDown();
            this.labelPortion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.tabPageCollect.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageEnvironment.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortion)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageCollect
            // 
            this.tabPageCollect.Controls.Add(this.comboBox1);
            this.tabPageCollect.Controls.Add(this.comboBox2);
            this.tabPageCollect.Controls.Add(this.label2);
            this.tabPageCollect.Controls.Add(this.label1);
            this.tabPageCollect.Controls.SetChildIndex(this.labelComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.label1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxComments, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelWater, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.textBoxLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.label2, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelLabel, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waterSelector, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBox2, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.comboBox1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.waypointControl1, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelTag, 0);
            this.tabPageCollect.Controls.SetChildIndex(this.labelPosition, 0);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.textBox1);
            this.tabPageSampler.Controls.Add(this.label5);
            this.tabPageSampler.Controls.Add(this.labelPortion);
            this.tabPageSampler.Controls.Add(this.label4);
            this.tabPageSampler.Controls.Add(this.textBox3);
            this.tabPageSampler.Controls.Add(this.textBox2);
            this.tabPageSampler.Controls.Add(this.label3);
            this.tabPageSampler.Controls.Add(this.numericUpDownPortion);
            this.tabPageSampler.Controls.SetChildIndex(this.numericUpDownPortion, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label3, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBox2, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBox3, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label4, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelPortion, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.label5, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.textBox1, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.labelMethod, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.comboBoxSampler, 0);
            this.tabPageSampler.Controls.SetChildIndex(this.buttonEquipment, 0);
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1")});
            this.comboBox1.Name = "comboBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBox2
            // 
            resources.ApplyResources(this.comboBox2, "comboBox2");
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Name = "comboBox2";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numericUpDownPortion
            // 
            resources.ApplyResources(this.numericUpDownPortion, "numericUpDownPortion");
            this.numericUpDownPortion.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPortion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortion.Name = "numericUpDownPortion";
            this.numericUpDownPortion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortion.ValueChanged += new System.EventHandler(this.numericUpDownReps_ValueChanged);
            this.numericUpDownPortion.VisibleChanged += new System.EventHandler(this.numericUpDownReps_VisibleChanged);
            // 
            // labelPortion
            // 
            resources.ApplyResources(this.labelPortion, "labelPortion");
            this.labelPortion.Name = "labelPortion";
            // 
            // PlanktonCard
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "PlanktonCard";
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.tabPageCollect.ResumeLayout(false);
            this.tabPageCollect.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageEnvironment.ResumeLayout(false);
            this.tabPageEnvironment.PerformLayout();
            this.tabPageSampler.ResumeLayout(false);
            this.tabPageSampler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void numericUpDownReps_ValueChanged(object sender, EventArgs e) {

        }

        private void numericUpDownReps_VisibleChanged(object sender, EventArgs e) {

        }
    }
}
