namespace Mayfly.Wild.Controls
{
    partial class WeatherControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeatherControl));
            this.checkBoxCloudage = new System.Windows.Forms.CheckBox();
            this.trackBarCloudage = new System.Windows.Forms.TrackBar();
            this.textBoxHumidity = new System.Windows.Forms.TextBox();
            this.textBoxWindDirection = new System.Windows.Forms.TextBox();
            this.textBoxWindRate = new System.Windows.Forms.TextBox();
            this.textBoxBaro = new System.Windows.Forms.TextBox();
            this.textBoxTemperature = new System.Windows.Forms.TextBox();
            this.comboBoxDiscretion = new System.Windows.Forms.ComboBox();
            this.comboBoxDegree = new System.Windows.Forms.ComboBox();
            this.comboBoxAdditionalEvent = new System.Windows.Forms.ComboBox();
            this.comboBoxEvent = new System.Windows.Forms.ComboBox();
            this.labelWindDirection = new System.Windows.Forms.Label();
            this.labelEventDiscretion = new System.Windows.Forms.Label();
            this.labelHumidity = new System.Windows.Forms.Label();
            this.labelEventDegree = new System.Windows.Forms.Label();
            this.labelAdditionalEvent = new System.Windows.Forms.Label();
            this.labelWindRate = new System.Windows.Forms.Label();
            this.labelEvent = new System.Windows.Forms.Label();
            this.labelAirPressure = new System.Windows.Forms.Label();
            this.labelTemperatureAir = new System.Windows.Forms.Label();
            this.toolTipAttention = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCloudage)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxCloudage
            // 
            resources.ApplyResources(this.checkBoxCloudage, "checkBoxCloudage");
            this.checkBoxCloudage.Name = "checkBoxCloudage";
            this.checkBoxCloudage.UseVisualStyleBackColor = true;
            this.checkBoxCloudage.CheckedChanged += new System.EventHandler(this.checkBoxCloudage_CheckedChanged);
            // 
            // trackBarCloudage
            // 
            resources.ApplyResources(this.trackBarCloudage, "trackBarCloudage");
            this.trackBarCloudage.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarCloudage.LargeChange = 2;
            this.trackBarCloudage.Maximum = 8;
            this.trackBarCloudage.Name = "trackBarCloudage";
            this.trackBarCloudage.TickFrequency = 10;
            this.trackBarCloudage.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarCloudage.Value = 1;
            this.trackBarCloudage.Scroll += new System.EventHandler(this.trackBarClouds_Scroll);
            // 
            // textBoxHumidity
            // 
            resources.ApplyResources(this.textBoxHumidity, "textBoxHumidity");
            this.textBoxHumidity.Name = "textBoxHumidity";
            this.textBoxHumidity.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxHumidity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxWindDirection
            // 
            resources.ApplyResources(this.textBoxWindDirection, "textBoxWindDirection");
            this.textBoxWindDirection.Name = "textBoxWindDirection";
            this.textBoxWindDirection.EnabledChanged += new System.EventHandler(this.textBoxWindDirection_EnabledChanged);
            this.textBoxWindDirection.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxWindDirection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxWindRate
            // 
            resources.ApplyResources(this.textBoxWindRate, "textBoxWindRate");
            this.textBoxWindRate.Name = "textBoxWindRate";
            this.textBoxWindRate.TextChanged += new System.EventHandler(this.textBoxWindRate_TextChanged);
            this.textBoxWindRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxBaro
            // 
            resources.ApplyResources(this.textBoxBaro, "textBoxBaro");
            this.textBoxBaro.Name = "textBoxBaro";
            this.textBoxBaro.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxBaro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // textBoxTemperature
            // 
            resources.ApplyResources(this.textBoxTemperature, "textBoxTemperature");
            this.textBoxTemperature.Name = "textBoxTemperature";
            this.textBoxTemperature.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxTemperature.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // comboBoxDiscretion
            // 
            resources.ApplyResources(this.comboBoxDiscretion, "comboBoxDiscretion");
            this.comboBoxDiscretion.DisplayMember = "Display";
            this.comboBoxDiscretion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiscretion.FormattingEnabled = true;
            this.comboBoxDiscretion.Name = "comboBoxDiscretion";
            this.comboBoxDiscretion.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.comboBoxDiscretion.EnabledChanged += new System.EventHandler(this.comboBoxEventDiscretion_EnabledChanged);
            this.comboBoxDiscretion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBoxDegree
            // 
            resources.ApplyResources(this.comboBoxDegree, "comboBoxDegree");
            this.comboBoxDegree.DisplayMember = "Display";
            this.comboBoxDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDegree.FormattingEnabled = true;
            this.comboBoxDegree.Name = "comboBoxDegree";
            this.comboBoxDegree.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.comboBoxDegree.EnabledChanged += new System.EventHandler(this.comboBoxEventDegree_EnabledChanged);
            this.comboBoxDegree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBoxAdditionalEvent
            // 
            resources.ApplyResources(this.comboBoxAdditionalEvent, "comboBoxAdditionalEvent");
            this.comboBoxAdditionalEvent.DisplayMember = "Display";
            this.comboBoxAdditionalEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdditionalEvent.FormattingEnabled = true;
            this.comboBoxAdditionalEvent.Name = "comboBoxAdditionalEvent";
            this.comboBoxAdditionalEvent.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.comboBoxAdditionalEvent.EnabledChanged += new System.EventHandler(this.comboBoxAdditionalEvent_EnabledChanged);
            this.comboBoxAdditionalEvent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBoxEvent
            // 
            resources.ApplyResources(this.comboBoxEvent, "comboBoxEvent");
            this.comboBoxEvent.DisplayMember = "Display";
            this.comboBoxEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEvent.FormattingEnabled = true;
            this.comboBoxEvent.Name = "comboBoxEvent";
            this.comboBoxEvent.SelectedIndexChanged += new System.EventHandler(this.comboBoxEvent_SelectedIndexChanged);
            this.comboBoxEvent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // labelWindDirection
            // 
            resources.ApplyResources(this.labelWindDirection, "labelWindDirection");
            this.labelWindDirection.Name = "labelWindDirection";
            // 
            // labelEventDiscretion
            // 
            resources.ApplyResources(this.labelEventDiscretion, "labelEventDiscretion");
            this.labelEventDiscretion.Name = "labelEventDiscretion";
            // 
            // labelHumidity
            // 
            resources.ApplyResources(this.labelHumidity, "labelHumidity");
            this.labelHumidity.Name = "labelHumidity";
            // 
            // labelEventDegree
            // 
            resources.ApplyResources(this.labelEventDegree, "labelEventDegree");
            this.labelEventDegree.Name = "labelEventDegree";
            // 
            // labelAdditionalEvent
            // 
            resources.ApplyResources(this.labelAdditionalEvent, "labelAdditionalEvent");
            this.labelAdditionalEvent.Name = "labelAdditionalEvent";
            // 
            // labelWindRate
            // 
            resources.ApplyResources(this.labelWindRate, "labelWindRate");
            this.labelWindRate.Name = "labelWindRate";
            // 
            // labelEvent
            // 
            resources.ApplyResources(this.labelEvent, "labelEvent");
            this.labelEvent.Name = "labelEvent";
            // 
            // labelAirPressure
            // 
            resources.ApplyResources(this.labelAirPressure, "labelAirPressure");
            this.labelAirPressure.Name = "labelAirPressure";
            // 
            // labelTemperatureAir
            // 
            resources.ApplyResources(this.labelTemperatureAir, "labelTemperatureAir");
            this.labelTemperatureAir.Name = "labelTemperatureAir";
            // 
            // WeatherControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.trackBarCloudage);
            this.Controls.Add(this.checkBoxCloudage);
            this.Controls.Add(this.textBoxHumidity);
            this.Controls.Add(this.textBoxWindDirection);
            this.Controls.Add(this.textBoxWindRate);
            this.Controls.Add(this.textBoxBaro);
            this.Controls.Add(this.textBoxTemperature);
            this.Controls.Add(this.comboBoxDiscretion);
            this.Controls.Add(this.comboBoxDegree);
            this.Controls.Add(this.comboBoxAdditionalEvent);
            this.Controls.Add(this.comboBoxEvent);
            this.Controls.Add(this.labelWindDirection);
            this.Controls.Add(this.labelEventDiscretion);
            this.Controls.Add(this.labelHumidity);
            this.Controls.Add(this.labelEventDegree);
            this.Controls.Add(this.labelAdditionalEvent);
            this.Controls.Add(this.labelWindRate);
            this.Controls.Add(this.labelEvent);
            this.Controls.Add(this.labelAirPressure);
            this.Controls.Add(this.labelTemperatureAir);
            this.Name = "WeatherControl";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCloudage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxCloudage;
        private System.Windows.Forms.TrackBar trackBarCloudage;
        public System.Windows.Forms.TextBox textBoxHumidity;
        public System.Windows.Forms.TextBox textBoxWindDirection;
        public System.Windows.Forms.TextBox textBoxWindRate;
        public System.Windows.Forms.TextBox textBoxBaro;
        public System.Windows.Forms.TextBox textBoxTemperature;
        private System.Windows.Forms.ComboBox comboBoxDiscretion;
        private System.Windows.Forms.ComboBox comboBoxDegree;
        private System.Windows.Forms.ComboBox comboBoxAdditionalEvent;
        private System.Windows.Forms.ComboBox comboBoxEvent;
        private System.Windows.Forms.Label labelWindDirection;
        private System.Windows.Forms.Label labelEventDiscretion;
        private System.Windows.Forms.Label labelHumidity;
        private System.Windows.Forms.Label labelEventDegree;
        private System.Windows.Forms.Label labelAdditionalEvent;
        private System.Windows.Forms.Label labelWindRate;
        private System.Windows.Forms.Label labelEvent;
        private System.Windows.Forms.Label labelAirPressure;
        private System.Windows.Forms.Label labelTemperatureAir;
        private System.Windows.Forms.ToolTip toolTipAttention;
    }
}
