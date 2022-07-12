namespace Mayfly.Wild.Controls
{
    partial class AquaControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AquaControl));
            this.checkBoxSewage = new System.Windows.Forms.CheckBox();
            this.checkBoxFoam = new System.Windows.Forms.CheckBox();
            this.checkBoxTurbidity = new System.Windows.Forms.CheckBox();
            this.checkBoxOdor = new System.Windows.Forms.CheckBox();
            this.labelColour = new System.Windows.Forms.Label();
            this.labelTemperatureBottom = new System.Windows.Forms.Label();
            this.labelTemperatureSurface = new System.Windows.Forms.Label();
            this.textBoxTempBottom = new System.Windows.Forms.TextBox();
            this.textBoxTempSurface = new System.Windows.Forms.TextBox();
            this.labelFlowRate = new System.Windows.Forms.Label();
            this.labelpH = new System.Windows.Forms.Label();
            this.labelConductivity = new System.Windows.Forms.Label();
            this.labelOxygenSaturation = new System.Windows.Forms.Label();
            this.labelDissolvedOxygen = new System.Windows.Forms.Label();
            this.labelLimpidity = new System.Windows.Forms.Label();
            this.textBoxFlowRate = new System.Windows.Forms.TextBox();
            this.textBoxpH = new System.Windows.Forms.TextBox();
            this.textBoxConductivity = new System.Windows.Forms.TextBox();
            this.textBoxOxygenSaturation = new System.Windows.Forms.TextBox();
            this.textBoxDissolvedOxygen = new System.Windows.Forms.TextBox();
            this.textBoxLimpidity = new System.Windows.Forms.TextBox();
            this.colorPicker1 = new global::Mayfly.Wild.Controls.ColorPicker();
            this.SuspendLayout();
            // 
            // checkBoxSewage
            // 
            resources.ApplyResources(this.checkBoxSewage, "checkBoxSewage");
            this.checkBoxSewage.Checked = true;
            this.checkBoxSewage.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxSewage.Name = "checkBoxSewage";
            this.checkBoxSewage.ThreeState = true;
            this.checkBoxSewage.UseVisualStyleBackColor = true;
            this.checkBoxSewage.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // checkBoxFoam
            // 
            resources.ApplyResources(this.checkBoxFoam, "checkBoxFoam");
            this.checkBoxFoam.Checked = true;
            this.checkBoxFoam.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxFoam.Name = "checkBoxFoam";
            this.checkBoxFoam.ThreeState = true;
            this.checkBoxFoam.UseVisualStyleBackColor = true;
            this.checkBoxFoam.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // checkBoxTurbidity
            // 
            resources.ApplyResources(this.checkBoxTurbidity, "checkBoxTurbidity");
            this.checkBoxTurbidity.Checked = true;
            this.checkBoxTurbidity.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxTurbidity.Name = "checkBoxTurbidity";
            this.checkBoxTurbidity.ThreeState = true;
            this.checkBoxTurbidity.UseVisualStyleBackColor = true;
            this.checkBoxTurbidity.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // checkBoxOdor
            // 
            resources.ApplyResources(this.checkBoxOdor, "checkBoxOdor");
            this.checkBoxOdor.Checked = true;
            this.checkBoxOdor.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBoxOdor.Name = "checkBoxOdor";
            this.checkBoxOdor.ThreeState = true;
            this.checkBoxOdor.UseVisualStyleBackColor = true;
            this.checkBoxOdor.CheckedChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelColour
            // 
            resources.ApplyResources(this.labelColour, "labelColour");
            this.labelColour.Name = "labelColour";
            // 
            // labelTemperatureBottom
            // 
            resources.ApplyResources(this.labelTemperatureBottom, "labelTemperatureBottom");
            this.labelTemperatureBottom.Name = "labelTemperatureBottom";
            // 
            // labelTemperatureSurface
            // 
            resources.ApplyResources(this.labelTemperatureSurface, "labelTemperatureSurface");
            this.labelTemperatureSurface.Name = "labelTemperatureSurface";
            // 
            // textBoxTempBottom
            // 
            resources.ApplyResources(this.textBoxTempBottom, "textBoxTempBottom");
            this.textBoxTempBottom.Name = "textBoxTempBottom";
            this.textBoxTempBottom.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxTempBottom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxTempSurface
            // 
            resources.ApplyResources(this.textBoxTempSurface, "textBoxTempSurface");
            this.textBoxTempSurface.Name = "textBoxTempSurface";
            this.textBoxTempSurface.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxTempSurface.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelFlowRate
            // 
            resources.ApplyResources(this.labelFlowRate, "labelFlowRate");
            this.labelFlowRate.Name = "labelFlowRate";
            // 
            // labelpH
            // 
            resources.ApplyResources(this.labelpH, "labelpH");
            this.labelpH.Name = "labelpH";
            // 
            // labelConductivity
            // 
            resources.ApplyResources(this.labelConductivity, "labelConductivity");
            this.labelConductivity.Name = "labelConductivity";
            // 
            // labelOxygenSaturation
            // 
            resources.ApplyResources(this.labelOxygenSaturation, "labelOxygenSaturation");
            this.labelOxygenSaturation.Name = "labelOxygenSaturation";
            // 
            // labelDissolvedOxygen
            // 
            resources.ApplyResources(this.labelDissolvedOxygen, "labelDissolvedOxygen");
            this.labelDissolvedOxygen.Name = "labelDissolvedOxygen";
            // 
            // labelLimpidity
            // 
            resources.ApplyResources(this.labelLimpidity, "labelLimpidity");
            this.labelLimpidity.Name = "labelLimpidity";
            // 
            // textBoxFlowRate
            // 
            resources.ApplyResources(this.textBoxFlowRate, "textBoxFlowRate");
            this.textBoxFlowRate.Name = "textBoxFlowRate";
            this.textBoxFlowRate.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxFlowRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxpH
            // 
            resources.ApplyResources(this.textBoxpH, "textBoxpH");
            this.textBoxpH.Name = "textBoxpH";
            this.textBoxpH.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxpH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxConductivity
            // 
            resources.ApplyResources(this.textBoxConductivity, "textBoxConductivity");
            this.textBoxConductivity.Name = "textBoxConductivity";
            this.textBoxConductivity.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxConductivity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxOxygenSaturation
            // 
            resources.ApplyResources(this.textBoxOxygenSaturation, "textBoxOxygenSaturation");
            this.textBoxOxygenSaturation.Name = "textBoxOxygenSaturation";
            this.textBoxOxygenSaturation.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxOxygenSaturation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxDissolvedOxygen
            // 
            resources.ApplyResources(this.textBoxDissolvedOxygen, "textBoxDissolvedOxygen");
            this.textBoxDissolvedOxygen.Name = "textBoxDissolvedOxygen";
            this.textBoxDissolvedOxygen.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxDissolvedOxygen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxLimpidity
            // 
            resources.ApplyResources(this.textBoxLimpidity, "textBoxLimpidity");
            this.textBoxLimpidity.Name = "textBoxLimpidity";
            this.textBoxLimpidity.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxLimpidity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // colorPicker1
            // 
            this.colorPicker1.BackColor = System.Drawing.Color.White;
            this.colorPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorPicker1.FormattingEnabled = true;
            resources.ApplyResources(this.colorPicker1, "colorPicker1");
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.SelectedItem = null;
            this.colorPicker1.SelectedValue = System.Drawing.Color.White;
            this.colorPicker1.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.colorPicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.colorPicker1_KeyPress);
            // 
            // AquaControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.colorPicker1);
            this.Controls.Add(this.checkBoxSewage);
            this.Controls.Add(this.checkBoxFoam);
            this.Controls.Add(this.checkBoxTurbidity);
            this.Controls.Add(this.checkBoxOdor);
            this.Controls.Add(this.labelColour);
            this.Controls.Add(this.labelTemperatureBottom);
            this.Controls.Add(this.labelTemperatureSurface);
            this.Controls.Add(this.textBoxTempBottom);
            this.Controls.Add(this.textBoxTempSurface);
            this.Controls.Add(this.labelFlowRate);
            this.Controls.Add(this.labelpH);
            this.Controls.Add(this.labelConductivity);
            this.Controls.Add(this.labelOxygenSaturation);
            this.Controls.Add(this.labelDissolvedOxygen);
            this.Controls.Add(this.labelLimpidity);
            this.Controls.Add(this.textBoxFlowRate);
            this.Controls.Add(this.textBoxpH);
            this.Controls.Add(this.textBoxConductivity);
            this.Controls.Add(this.textBoxOxygenSaturation);
            this.Controls.Add(this.textBoxDissolvedOxygen);
            this.Controls.Add(this.textBoxLimpidity);
            this.Name = "AquaControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxSewage;
        private System.Windows.Forms.CheckBox checkBoxFoam;
        private System.Windows.Forms.CheckBox checkBoxTurbidity;
        private System.Windows.Forms.CheckBox checkBoxOdor;
        private System.Windows.Forms.Label labelColour;
        private System.Windows.Forms.Label labelTemperatureBottom;
        private System.Windows.Forms.Label labelTemperatureSurface;
        private System.Windows.Forms.Label labelFlowRate;
        private System.Windows.Forms.Label labelpH;
        private System.Windows.Forms.Label labelConductivity;
        private System.Windows.Forms.Label labelOxygenSaturation;
        private System.Windows.Forms.Label labelDissolvedOxygen;
        private System.Windows.Forms.Label labelLimpidity;
        private ColorPicker colorPicker1;
        private System.Windows.Forms.TextBox textBoxTempBottom;
        private System.Windows.Forms.TextBox textBoxTempSurface;
        private System.Windows.Forms.TextBox textBoxFlowRate;
        private System.Windows.Forms.TextBox textBoxpH;
        private System.Windows.Forms.TextBox textBoxConductivity;
        private System.Windows.Forms.TextBox textBoxOxygenSaturation;
        private System.Windows.Forms.TextBox textBoxDissolvedOxygen;
        private System.Windows.Forms.TextBox textBoxLimpidity;
    }
}
