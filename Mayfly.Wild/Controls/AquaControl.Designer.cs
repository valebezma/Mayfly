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
            this.labelFlowRate = new System.Windows.Forms.Label();
            this.labelpH = new System.Windows.Forms.Label();
            this.labelConductivity = new System.Windows.Forms.Label();
            this.labelOxygenSaturation = new System.Windows.Forms.Label();
            this.labelDissolvedOxygen = new System.Windows.Forms.Label();
            this.labelLimpidity = new System.Windows.Forms.Label();
            this.colorPicker1 = new Mayfly.Wild.Controls.ColorPicker();
            this.numericTempBottom = new Mayfly.Controls.NumberBox();
            this.numericTempSurface = new Mayfly.Controls.NumberBox();
            this.numericFlowRate = new Mayfly.Controls.NumberBox();
            this.numericpH = new Mayfly.Controls.NumberBox();
            this.numericConductivity = new Mayfly.Controls.NumberBox();
            this.numericOxygenSaturation = new Mayfly.Controls.NumberBox();
            this.numericDissolvedOxygen = new Mayfly.Controls.NumberBox();
            this.numericLimpidity = new Mayfly.Controls.NumberBox();
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
            // colorPicker1
            // 
            resources.ApplyResources(this.colorPicker1, "colorPicker1");
            this.colorPicker1.BackColor = System.Drawing.Color.White;
            this.colorPicker1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorPicker1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorPicker1.FormattingEnabled = true;
            this.colorPicker1.Name = "colorPicker1";
            this.colorPicker1.SelectedItem = null;
            this.colorPicker1.SelectedValue = System.Drawing.Color.White;
            this.colorPicker1.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.colorPicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.colorPicker1_KeyPress);
            // 
            // numericTempBottom
            // 
            resources.ApplyResources(this.numericTempBottom, "numericTempBottom");
            this.numericTempBottom.Maximum = 100D;
            this.numericTempBottom.Minimum = 0D;
            this.numericTempBottom.Name = "numericTempBottom";
            this.numericTempBottom.Value = -1D;
            this.numericTempBottom.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericTempBottom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericTempSurface
            // 
            resources.ApplyResources(this.numericTempSurface, "numericTempSurface");
            this.numericTempSurface.Maximum = 100D;
            this.numericTempSurface.Minimum = 0D;
            this.numericTempSurface.Name = "numericTempSurface";
            this.numericTempSurface.Value = -1D;
            this.numericTempSurface.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericTempSurface.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericFlowRate
            // 
            resources.ApplyResources(this.numericFlowRate, "numericFlowRate");
            this.numericFlowRate.Maximum = 1000D;
            this.numericFlowRate.Minimum = 0D;
            this.numericFlowRate.Name = "numericFlowRate";
            this.numericFlowRate.Value = -1D;
            this.numericFlowRate.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericFlowRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericpH
            // 
            resources.ApplyResources(this.numericpH, "numericpH");
            this.numericpH.Maximum = 14D;
            this.numericpH.Minimum = 0D;
            this.numericpH.Name = "numericpH";
            this.numericpH.Value = -1D;
            this.numericpH.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericpH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericConductivity
            // 
            resources.ApplyResources(this.numericConductivity, "numericConductivity");
            this.numericConductivity.Maximum = 100D;
            this.numericConductivity.Minimum = 0D;
            this.numericConductivity.Name = "numericConductivity";
            this.numericConductivity.Value = -1D;
            this.numericConductivity.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericConductivity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericOxygenSaturation
            // 
            resources.ApplyResources(this.numericOxygenSaturation, "numericOxygenSaturation");
            this.numericOxygenSaturation.Maximum = 100D;
            this.numericOxygenSaturation.Minimum = 0D;
            this.numericOxygenSaturation.Name = "numericOxygenSaturation";
            this.numericOxygenSaturation.Value = -1D;
            this.numericOxygenSaturation.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericOxygenSaturation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericDissolvedOxygen
            // 
            resources.ApplyResources(this.numericDissolvedOxygen, "numericDissolvedOxygen");
            this.numericDissolvedOxygen.Maximum = 100D;
            this.numericDissolvedOxygen.Minimum = 0D;
            this.numericDissolvedOxygen.Name = "numericDissolvedOxygen";
            this.numericDissolvedOxygen.Value = -1D;
            this.numericDissolvedOxygen.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericDissolvedOxygen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // numericLimpidity
            // 
            resources.ApplyResources(this.numericLimpidity, "numericLimpidity");
            this.numericLimpidity.Maximum = 10000D;
            this.numericLimpidity.Minimum = 0D;
            this.numericLimpidity.Name = "numericLimpidity";
            this.numericLimpidity.Value = -1D;
            this.numericLimpidity.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericLimpidity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // AquaControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.numericTempBottom);
            this.Controls.Add(this.numericTempSurface);
            this.Controls.Add(this.numericFlowRate);
            this.Controls.Add(this.numericpH);
            this.Controls.Add(this.numericConductivity);
            this.Controls.Add(this.numericOxygenSaturation);
            this.Controls.Add(this.numericDissolvedOxygen);
            this.Controls.Add(this.numericLimpidity);
            this.Controls.Add(this.colorPicker1);
            this.Controls.Add(this.checkBoxSewage);
            this.Controls.Add(this.checkBoxFoam);
            this.Controls.Add(this.checkBoxTurbidity);
            this.Controls.Add(this.checkBoxOdor);
            this.Controls.Add(this.labelColour);
            this.Controls.Add(this.labelTemperatureBottom);
            this.Controls.Add(this.labelTemperatureSurface);
            this.Controls.Add(this.labelFlowRate);
            this.Controls.Add(this.labelpH);
            this.Controls.Add(this.labelConductivity);
            this.Controls.Add(this.labelOxygenSaturation);
            this.Controls.Add(this.labelDissolvedOxygen);
            this.Controls.Add(this.labelLimpidity);
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
        private Mayfly.Controls.NumberBox numericTempBottom;
        private Mayfly.Controls.NumberBox numericTempSurface;
        private Mayfly.Controls.NumberBox numericFlowRate;
        private Mayfly.Controls.NumberBox numericpH;
        private Mayfly.Controls.NumberBox numericConductivity;
        private Mayfly.Controls.NumberBox numericOxygenSaturation;
        private Mayfly.Controls.NumberBox numericDissolvedOxygen;
        private Mayfly.Controls.NumberBox numericLimpidity;
    }
}
