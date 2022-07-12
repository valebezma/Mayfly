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

        private void InitializeComponent() {
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
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.comboBox1.Location = new System.Drawing.Point(348, 114);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 21);
            this.comboBox1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(270, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Bank";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(45, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Section";
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(122, 114);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(142, 21);
            this.comboBox2.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1.Location = new System.Drawing.Point(214, 141);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(50, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox2.Location = new System.Drawing.Point(214, 115);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(50, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(45, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sampling volume, l";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(45, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mesh opening, µm";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox3.Location = new System.Drawing.Point(214, 89);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(50, 20);
            this.textBox3.TabIndex = 8;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(45, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Open diameter, mm";
            // 
            // numericUpDownPortion
            // 
            this.numericUpDownPortion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownPortion.Location = new System.Drawing.Point(434, 90);
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
            this.numericUpDownPortion.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownPortion.TabIndex = 6;
            this.numericUpDownPortion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownPortion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortion.Visible = false;
            this.numericUpDownPortion.ValueChanged += new System.EventHandler(this.numericUpDownReps_ValueChanged);
            this.numericUpDownPortion.VisibleChanged += new System.EventHandler(this.numericUpDownReps_VisibleChanged);
            // 
            // labelPortion
            // 
            this.labelPortion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelPortion.AutoSize = true;
            this.labelPortion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelPortion.Location = new System.Drawing.Point(270, 93);
            this.labelPortion.Name = "labelPortion";
            this.labelPortion.Size = new System.Drawing.Size(45, 13);
            this.labelPortion.TabIndex = 5;
            this.labelPortion.Text = "Portions";
            this.labelPortion.Visible = false;
            // 
            // PlanktonCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(564, 631);
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
