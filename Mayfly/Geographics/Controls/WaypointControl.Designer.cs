namespace Mayfly.Geographics
{
    partial class WaypointControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaypointControl));
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.maskedTextBoxLongitude = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxLatitude = new System.Windows.Forms.MaskedTextBox();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelLat = new System.Windows.Forms.Label();
            this.labelLng = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextItemGet = new System.Windows.Forms.ToolStripMenuItem();
            this.contextItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.degreesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.degreesAndMinutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.degreesMinutesAndSecondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericAltitude = new Mayfly.Controls.NumberBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.AllowDrop = true;
            resources.ApplyResources(this.dateTimePickerDate, "dateTimePickerDate");
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.ValueChanged += new System.EventHandler(this.dateTimePickerDate_ValueChanged);
            // 
            // labelDate
            // 
            resources.ApplyResources(this.labelDate, "labelDate");
            this.labelDate.Name = "labelDate";
            // 
            // maskedTextBoxLongitude
            // 
            this.maskedTextBoxLongitude.AllowDrop = true;
            resources.ApplyResources(this.maskedTextBoxLongitude, "maskedTextBoxLongitude");
            this.maskedTextBoxLongitude.Name = "maskedTextBoxLongitude";
            this.maskedTextBoxLongitude.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.maskedTextBoxLongitude.TextChanged += new System.EventHandler(this.value_Changed);
            this.maskedTextBoxLongitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // maskedTextBoxLatitude
            // 
            this.maskedTextBoxLatitude.AllowDrop = true;
            resources.ApplyResources(this.maskedTextBoxLatitude, "maskedTextBoxLatitude");
            this.maskedTextBoxLatitude.Name = "maskedTextBoxLatitude";
            this.maskedTextBoxLatitude.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.maskedTextBoxLatitude.TextChanged += new System.EventHandler(this.value_Changed);
            this.maskedTextBoxLatitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // labelAltitude
            // 
            resources.ApplyResources(this.labelAltitude, "labelAltitude");
            this.labelAltitude.Name = "labelAltitude";
            // 
            // labelLatitude
            // 
            resources.ApplyResources(this.labelLatitude, "labelLatitude");
            this.labelLatitude.Name = "labelLatitude";
            // 
            // labelLat
            // 
            resources.ApplyResources(this.labelLat, "labelLat");
            this.labelLat.Name = "labelLat";
            this.labelLat.Click += new System.EventHandler(this.labelDirection_Click);
            // 
            // labelLng
            // 
            resources.ApplyResources(this.labelLng, "labelLng");
            this.labelLng.Name = "labelLng";
            this.labelLng.Click += new System.EventHandler(this.labelDirection_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextItemGet,
            this.contextItemOpen,
            this.toolStripSeparator1,
            this.setFormatToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // contextItemGet
            // 
            this.contextItemGet.Name = "contextItemGet";
            resources.ApplyResources(this.contextItemGet, "contextItemGet");
            this.contextItemGet.Click += new System.EventHandler(this.contextItemGet_Click);
            // 
            // contextItemOpen
            // 
            this.contextItemOpen.Name = "contextItemOpen";
            resources.ApplyResources(this.contextItemOpen, "contextItemOpen");
            this.contextItemOpen.Click += new System.EventHandler(this.contextItemOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // setFormatToolStripMenuItem
            // 
            this.setFormatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.degreesToolStripMenuItem,
            this.degreesAndMinutesToolStripMenuItem,
            this.degreesMinutesAndSecondsToolStripMenuItem});
            this.setFormatToolStripMenuItem.Name = "setFormatToolStripMenuItem";
            resources.ApplyResources(this.setFormatToolStripMenuItem, "setFormatToolStripMenuItem");
            // 
            // degreesToolStripMenuItem
            // 
            this.degreesToolStripMenuItem.Name = "degreesToolStripMenuItem";
            resources.ApplyResources(this.degreesToolStripMenuItem, "degreesToolStripMenuItem");
            this.degreesToolStripMenuItem.Click += new System.EventHandler(this.degreesToolStripMenuItem_Click);
            // 
            // degreesAndMinutesToolStripMenuItem
            // 
            this.degreesAndMinutesToolStripMenuItem.Name = "degreesAndMinutesToolStripMenuItem";
            resources.ApplyResources(this.degreesAndMinutesToolStripMenuItem, "degreesAndMinutesToolStripMenuItem");
            this.degreesAndMinutesToolStripMenuItem.Click += new System.EventHandler(this.degreesAndMinutesToolStripMenuItem_Click);
            // 
            // degreesMinutesAndSecondsToolStripMenuItem
            // 
            this.degreesMinutesAndSecondsToolStripMenuItem.Name = "degreesMinutesAndSecondsToolStripMenuItem";
            resources.ApplyResources(this.degreesMinutesAndSecondsToolStripMenuItem, "degreesMinutesAndSecondsToolStripMenuItem");
            this.degreesMinutesAndSecondsToolStripMenuItem.Click += new System.EventHandler(this.degreesMinutesAndSecondsToolStripMenuItem_Click);
            // 
            // numericAltitude
            // 
            this.numericAltitude.AllowDrop = true;
            resources.ApplyResources(this.numericAltitude, "numericAltitude");
            this.numericAltitude.Maximum = 10000D;
            this.numericAltitude.Minimum = -1000D;
            this.numericAltitude.Name = "numericAltitude";
            this.numericAltitude.Value = -1001D;
            this.numericAltitude.TextChanged += new System.EventHandler(this.value_Changed);
            this.numericAltitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.value_KeyPress);
            // 
            // WaypointControl
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.maskedTextBoxLongitude);
            this.Controls.Add(this.maskedTextBoxLatitude);
            this.Controls.Add(this.labelAltitude);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.labelLat);
            this.Controls.Add(this.labelLng);
            this.Controls.Add(this.numericAltitude);
            this.Name = "WaypointControl";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.locationData_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.locationData_DragEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.Label labelLatitude;
        private Mayfly.Controls.NumberBox numericAltitude;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxLongitude;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxLatitude;
        private System.Windows.Forms.Label labelLat;
        private System.Windows.Forms.Label labelLng;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem contextItemGet;
        private System.Windows.Forms.ToolStripMenuItem contextItemOpen;
        private System.Windows.Forms.ToolStripMenuItem setFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem degreesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem degreesAndMinutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem degreesMinutesAndSecondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
