
namespace Mayfly.Waters
{
    partial class CardLake
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardLake));
            this.labelVegetation = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelDepthMax = new System.Windows.Forms.Label();
            this.labelArea = new System.Windows.Forms.Label();
            this.labelKind = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxVegetation = new System.Windows.Forms.TextBox();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.textBoxDepthMax = new System.Windows.Forms.TextBox();
            this.textBoxArea = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxKind = new System.Windows.Forms.ComboBox();
            this.textBoxDepthAve = new System.Windows.Forms.TextBox();
            this.labelDepthAve = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelHydrologics = new System.Windows.Forms.Label();
            this.labelGeneral = new System.Windows.Forms.Label();
            this.textBoxWatershed = new System.Windows.Forms.TextBox();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.labelOutflow = new System.Windows.Forms.Label();
            this.labelMouthToMouth = new System.Windows.Forms.Label();
            this.labelBank = new System.Windows.Forms.Label();
            this.textBoxMouthToMouth = new System.Windows.Forms.TextBox();
            this.outflowSelector = new Mayfly.Waters.Controls.WaterSelector();
            this.SuspendLayout();
            // 
            // labelVegetation
            // 
            resources.ApplyResources(this.labelVegetation, "labelVegetation");
            this.labelVegetation.Name = "labelVegetation";
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // labelDepthMax
            // 
            resources.ApplyResources(this.labelDepthMax, "labelDepthMax");
            this.labelDepthMax.Name = "labelDepthMax";
            // 
            // labelArea
            // 
            resources.ApplyResources(this.labelArea, "labelArea");
            this.labelArea.Name = "labelArea";
            // 
            // labelKind
            // 
            resources.ApplyResources(this.labelKind, "labelKind");
            this.labelKind.Name = "labelKind";
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // textBoxVegetation
            // 
            resources.ApplyResources(this.textBoxVegetation, "textBoxVegetation");
            this.textBoxVegetation.Name = "textBoxVegetation";
            this.textBoxVegetation.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxVegetation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // textBoxVolume
            // 
            resources.ApplyResources(this.textBoxVolume, "textBoxVolume");
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // textBoxDepthMax
            // 
            resources.ApplyResources(this.textBoxDepthMax, "textBoxDepthMax");
            this.textBoxDepthMax.Name = "textBoxDepthMax";
            this.textBoxDepthMax.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxDepthMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // textBoxArea
            // 
            resources.ApplyResources(this.textBoxArea, "textBoxArea");
            this.textBoxArea.Name = "textBoxArea";
            this.textBoxArea.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // comboBoxKind
            // 
            resources.ApplyResources(this.comboBoxKind, "comboBoxKind");
            this.comboBoxKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKind.FormattingEnabled = true;
            this.comboBoxKind.Items.AddRange(new object[] {
            resources.GetString("comboBoxKind.Items"),
            resources.GetString("comboBoxKind.Items1")});
            this.comboBoxKind.Name = "comboBoxKind";
            this.comboBoxKind.SelectedIndexChanged += new System.EventHandler(this.comboBoxKind_SelectedIndexChanged);
            // 
            // textBoxDepthAve
            // 
            resources.ApplyResources(this.textBoxDepthAve, "textBoxDepthAve");
            this.textBoxDepthAve.Name = "textBoxDepthAve";
            this.textBoxDepthAve.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxDepthAve.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelDepthAve
            // 
            resources.ApplyResources(this.labelDepthAve, "labelDepthAve");
            this.labelDepthAve.Name = "labelDepthAve";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelHydrologics
            // 
            resources.ApplyResources(this.labelHydrologics, "labelHydrologics");
            this.labelHydrologics.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelHydrologics.Name = "labelHydrologics";
            // 
            // labelGeneral
            // 
            resources.ApplyResources(this.labelGeneral, "labelGeneral");
            this.labelGeneral.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelGeneral.Name = "labelGeneral";
            // 
            // textBoxWatershed
            // 
            resources.ApplyResources(this.textBoxWatershed, "textBoxWatershed");
            this.textBoxWatershed.Name = "textBoxWatershed";
            this.textBoxWatershed.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxWatershed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelWatershed
            // 
            resources.ApplyResources(this.labelWatershed, "labelWatershed");
            this.labelWatershed.Name = "labelWatershed";
            // 
            // textBoxAltitude
            // 
            resources.ApplyResources(this.textBoxAltitude, "textBoxAltitude");
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxAltitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelAltitude
            // 
            resources.ApplyResources(this.labelAltitude, "labelAltitude");
            this.labelAltitude.Name = "labelAltitude";
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
            // labelOutflow
            // 
            resources.ApplyResources(this.labelOutflow, "labelOutflow");
            this.labelOutflow.Name = "labelOutflow";
            // 
            // labelMouthToMouth
            // 
            resources.ApplyResources(this.labelMouthToMouth, "labelMouthToMouth");
            this.labelMouthToMouth.Name = "labelMouthToMouth";
            // 
            // labelBank
            // 
            resources.ApplyResources(this.labelBank, "labelBank");
            this.labelBank.Name = "labelBank";
            // 
            // textBoxMouthToMouth
            // 
            resources.ApplyResources(this.textBoxMouthToMouth, "textBoxMouthToMouth");
            this.textBoxMouthToMouth.Name = "textBoxMouthToMouth";
            // 
            // outflowSelector
            // 
            resources.ApplyResources(this.outflowSelector, "outflowSelector");
            this.outflowSelector.Name = "outflowSelector";
            this.outflowSelector.WaterObject = null;
            // 
            // CardLake
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.outflowSelector);
            this.Controls.Add(this.comboBoxBank);
            this.Controls.Add(this.labelOutflow);
            this.Controls.Add(this.labelMouthToMouth);
            this.Controls.Add(this.labelBank);
            this.Controls.Add(this.textBoxMouthToMouth);
            this.Controls.Add(this.labelGeneral);
            this.Controls.Add(this.labelHydrologics);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxKind);
            this.Controls.Add(this.labelVegetation);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.labelDepthAve);
            this.Controls.Add(this.labelDepthMax);
            this.Controls.Add(this.labelWatershed);
            this.Controls.Add(this.labelAltitude);
            this.Controls.Add(this.labelArea);
            this.Controls.Add(this.labelKind);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxVegetation);
            this.Controls.Add(this.textBoxVolume);
            this.Controls.Add(this.textBoxDepthAve);
            this.Controls.Add(this.textBoxDepthMax);
            this.Controls.Add(this.textBoxWatershed);
            this.Controls.Add(this.textBoxAltitude);
            this.Controls.Add(this.textBoxArea);
            this.Controls.Add(this.textBoxName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardLake";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

                private System.Windows.Forms.Label labelVegetation;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Label labelDepthMax;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.Label labelKind;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDepthAve;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelHydrologics;
        private System.Windows.Forms.Label labelGeneral;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.Label labelOutflow;
        private System.Windows.Forms.Label labelMouthToMouth;
        private System.Windows.Forms.Label labelBank;
        private Controls.WaterSelector outflowSelector;
        private System.Windows.Forms.TextBox textBoxVegetation;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.TextBox textBoxDepthMax;
        private System.Windows.Forms.TextBox textBoxArea;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxDepthAve;
        private System.Windows.Forms.TextBox textBoxWatershed;
        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.ComboBox comboBoxBank;
        private System.Windows.Forms.TextBox textBoxMouthToMouth;
        internal System.Windows.Forms.ComboBox comboBoxKind;
    }
}