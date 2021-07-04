namespace Mayfly.Fish.Explorer.Observations
{
    partial class Environment
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Environment));
            Mayfly.Wild.Weather weather1 = new Mayfly.Wild.Weather();
            this.labelWaterConds = new System.Windows.Forms.Label();
            this.textBoxTempSurface = new System.Windows.Forms.TextBox();
            this.labelTemperatureSurface = new System.Windows.Forms.Label();
            this.labelActWeather = new System.Windows.Forms.Label();
            this.toolTipAttention = new System.Windows.Forms.ToolTip(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.weatherControl1 = new Mayfly.Wild.Controls.WeatherControl();
            this.SuspendLayout();
            // 
            // labelWaterConds
            // 
            resources.ApplyResources(this.labelWaterConds, "labelWaterConds");
            this.labelWaterConds.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelWaterConds.Name = "labelWaterConds";
            // 
            // textBoxTempSurface
            // 
            resources.ApplyResources(this.textBoxTempSurface, "textBoxTempSurface");
            this.textBoxTempSurface.Name = "textBoxTempSurface";
            this.textBoxTempSurface.TextChanged += new System.EventHandler(this.value_Changed);
            this.textBoxTempSurface.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelTemperatureSurface
            // 
            resources.ApplyResources(this.labelTemperatureSurface, "labelTemperatureSurface");
            this.labelTemperatureSurface.Name = "labelTemperatureSurface";
            // 
            // labelActWeather
            // 
            resources.ApplyResources(this.labelActWeather, "labelActWeather");
            this.labelActWeather.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelActWeather.Name = "labelActWeather";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // weatherControl1
            // 
            resources.ApplyResources(this.weatherControl1, "weatherControl1");
            this.weatherControl1.BackColor = System.Drawing.SystemColors.Window;
            this.weatherControl1.Name = "weatherControl1";
            weather1.AdditionalEvent = -1;
            weather1.Cloudage = double.NaN;
            weather1.Degree = -1;
            weather1.Discretion = -1;
            weather1.Event = 0;
            weather1.Humidity = double.NaN;
            weather1.Pressure = double.NaN;
            weather1.Temperature = double.NaN;
            weather1.WindDirection = double.NaN;
            weather1.WindRate = double.NaN;
            this.weatherControl1.Weather = weather1;
            this.weatherControl1.Changed += new System.EventHandler(this.value_Changed);
            // 
            // Environment
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.weatherControl1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelWaterConds);
            this.Controls.Add(this.textBoxTempSurface);
            this.Controls.Add(this.labelTemperatureSurface);
            this.Controls.Add(this.labelActWeather);
            this.Name = "Environment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWaterConds;
        public System.Windows.Forms.TextBox textBoxTempSurface;
        private System.Windows.Forms.Label labelTemperatureSurface;
        private System.Windows.Forms.Label labelActWeather;
        private System.Windows.Forms.ToolTip toolTipAttention;
        private System.Windows.Forms.Button buttonOK;
        private Wild.Controls.WeatherControl weatherControl1;

    }
}