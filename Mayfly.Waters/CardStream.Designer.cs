
namespace Mayfly.Waters
{
    partial class CardStream
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardStream));
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelBank = new System.Windows.Forms.Label();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.labelLength = new System.Windows.Forms.Label();
            this.textBoxWatershed = new System.Windows.Forms.TextBox();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.textBoxSpend = new System.Windows.Forms.TextBox();
            this.labelSpend = new System.Windows.Forms.Label();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.labelVolume = new System.Windows.Forms.Label();
            this.textBoxSlope = new System.Windows.Forms.TextBox();
            this.labelSlope = new System.Windows.Forms.Label();
            this.comboBoxBank = new System.Windows.Forms.ComboBox();
            this.textBoxMouthToMouth = new System.Windows.Forms.TextBox();
            this.labelMouthToMouth = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelGeneral = new System.Windows.Forms.Label();
            this.labelHydrologics = new System.Windows.Forms.Label();
            this.labelOutflow = new System.Windows.Forms.Label();
            this.outflowSelector = new Mayfly.Waters.Controls.WaterSelector();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelBank
            // 
            resources.ApplyResources(this.labelBank, "labelBank");
            this.labelBank.Name = "labelBank";
            // 
            // textBoxLength
            // 
            resources.ApplyResources(this.textBoxLength, "textBoxLength");
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelLength
            // 
            resources.ApplyResources(this.labelLength, "labelLength");
            this.labelLength.Name = "labelLength";
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
            // textBoxSpend
            // 
            resources.ApplyResources(this.textBoxSpend, "textBoxSpend");
            this.textBoxSpend.Name = "textBoxSpend";
            this.textBoxSpend.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxSpend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelSpend
            // 
            resources.ApplyResources(this.labelSpend, "labelSpend");
            this.labelSpend.Name = "labelSpend";
            // 
            // textBoxVolume
            // 
            resources.ApplyResources(this.textBoxVolume, "textBoxVolume");
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxVolume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelVolume
            // 
            resources.ApplyResources(this.labelVolume, "labelVolume");
            this.labelVolume.Name = "labelVolume";
            // 
            // textBoxSlope
            // 
            resources.ApplyResources(this.textBoxSlope, "textBoxSlope");
            this.textBoxSlope.Name = "textBoxSlope";
            this.textBoxSlope.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxSlope.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelSlope
            // 
            resources.ApplyResources(this.labelSlope, "labelSlope");
            this.labelSlope.Name = "labelSlope";
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
            this.comboBoxBank.SelectedIndexChanged += new System.EventHandler(this.elementChanged);
            // 
            // textBoxMouthToMouth
            // 
            resources.ApplyResources(this.textBoxMouthToMouth, "textBoxMouthToMouth");
            this.textBoxMouthToMouth.Name = "textBoxMouthToMouth";
            this.textBoxMouthToMouth.TextChanged += new System.EventHandler(this.elementChanged);
            this.textBoxMouthToMouth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Number_KeyPress);
            // 
            // labelMouthToMouth
            // 
            resources.ApplyResources(this.labelMouthToMouth, "labelMouthToMouth");
            this.labelMouthToMouth.Name = "labelMouthToMouth";
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelGeneral
            // 
            resources.ApplyResources(this.labelGeneral, "labelGeneral");
            this.labelGeneral.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelGeneral.Name = "labelGeneral";
            // 
            // labelHydrologics
            // 
            resources.ApplyResources(this.labelHydrologics, "labelHydrologics");
            this.labelHydrologics.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelHydrologics.Name = "labelHydrologics";
            // 
            // labelOutflow
            // 
            resources.ApplyResources(this.labelOutflow, "labelOutflow");
            this.labelOutflow.Name = "labelOutflow";
            // 
            // outflowSelector
            // 
            resources.ApplyResources(this.outflowSelector, "outflowSelector");
            this.outflowSelector.Name = "outflowSelector";
            this.outflowSelector.WaterObject = null;
            // 
            // CardStream
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.outflowSelector);
            this.Controls.Add(this.labelHydrologics);
            this.Controls.Add(this.labelGeneral);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxBank);
            this.Controls.Add(this.labelSlope);
            this.Controls.Add(this.labelVolume);
            this.Controls.Add(this.labelSpend);
            this.Controls.Add(this.labelWatershed);
            this.Controls.Add(this.labelOutflow);
            this.Controls.Add(this.labelMouthToMouth);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelBank);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxSlope);
            this.Controls.Add(this.textBoxVolume);
            this.Controls.Add(this.textBoxSpend);
            this.Controls.Add(this.textBoxWatershed);
            this.Controls.Add(this.textBoxMouthToMouth);
            this.Controls.Add(this.textBoxLength);
            this.Controls.Add(this.textBoxName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardStream";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelBank;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.Label labelSpend;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Label labelSlope;
        private System.Windows.Forms.Label labelMouthToMouth;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelGeneral;
        private System.Windows.Forms.Label labelHydrologics;
        private System.Windows.Forms.Label labelOutflow;
        private Controls.WaterSelector outflowSelector;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.TextBox textBoxWatershed;
        private System.Windows.Forms.TextBox textBoxSpend;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.TextBox textBoxSlope;
        private System.Windows.Forms.ComboBox comboBoxBank;
        private System.Windows.Forms.TextBox textBoxMouthToMouth;
    }
}