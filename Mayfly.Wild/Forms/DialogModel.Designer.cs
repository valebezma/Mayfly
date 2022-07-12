namespace Mayfly.Wild
{
    partial class ModelConfirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelConfirm));
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonTrend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statChart1 = new global::Mayfly.Mathematics.Charts.Plot();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelInstruction
            // 
            resources.ApplyResources(this.labelInstruction, "labelInstruction");
            this.labelInstruction.Name = "labelInstruction";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonTrend);
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Name = "panel1";
            // 
            // buttonTrend
            // 
            resources.ApplyResources(this.buttonTrend, "buttonTrend");
            this.buttonTrend.Name = "buttonTrend";
            this.buttonTrend.UseVisualStyleBackColor = true;
            this.buttonTrend.Click += new System.EventHandler(this.buttonTrend_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // statChart1
            // 
            resources.ApplyResources(this.statChart1, "statChart1");
            this.statChart1.AxisXAutoInterval = false;
            this.statChart1.AxisXAutoMinimum = false;
            this.statChart1.AxisXInterval = 1D;
            this.statChart1.AxisYAutoInterval = false;
            this.statChart1.AxisYAutoMinimum = false;
            this.statChart1.AxisYInterval = 1D;
            this.statChart1.Name = "statChart1";
            this.statChart1.ShowLegend = false;
            this.statChart1.TimeInterval = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            // 
            // ModelConfirm
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statChart1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelInstruction);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelConfirm";
            this.Load += new System.EventHandler(this.ModelConfirm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statChart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Button buttonCancel;
        public Mathematics.Charts.Plot statChart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTrend;
        private System.Windows.Forms.Label label2;
    }
}