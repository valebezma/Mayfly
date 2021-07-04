namespace Mayfly.Software
{
    partial class ActionLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionLogs));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusEvents = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPC = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.comboBoxEventType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.backLoader = new System.ComponentModel.BackgroundWorker();
            this.spreadSheetEvents = new Mayfly.Controls.SpreadSheet();
            this.ColumnWhen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFeature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusEvents,
            this.statusUser,
            this.statusPC});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusEvents
            // 
            resources.ApplyResources(this.statusEvents, "statusEvents");
            this.statusEvents.BackColor = System.Drawing.SystemColors.Control;
            this.statusEvents.Name = "statusEvents";
            // 
            // statusUser
            // 
            resources.ApplyResources(this.statusUser, "statusUser");
            this.statusUser.BackColor = System.Drawing.SystemColors.Control;
            this.statusUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusUser.Name = "statusUser";
            // 
            // statusPC
            // 
            resources.ApplyResources(this.statusPC, "statusPC");
            this.statusPC.BackColor = System.Drawing.SystemColors.Control;
            this.statusPC.Name = "statusPC";
            this.statusPC.Spring = true;
            // 
            // dateTimePickerFrom
            // 
            resources.ApplyResources(this.dateTimePickerFrom, "dateTimePickerFrom");
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.term_ValueChanged);
            // 
            // dateTimePickerTo
            // 
            resources.ApplyResources(this.dateTimePickerTo, "dateTimePickerTo");
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.term_ValueChanged);
            // 
            // comboBoxEventType
            // 
            resources.ApplyResources(this.comboBoxEventType, "comboBoxEventType");
            this.comboBoxEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEventType.FormattingEnabled = true;
            this.comboBoxEventType.Items.AddRange(new object[] {
            resources.GetString("comboBoxEventType.Items"),
            resources.GetString("comboBoxEventType.Items1"),
            resources.GetString("comboBoxEventType.Items2"),
            resources.GetString("comboBoxEventType.Items3"),
            resources.GetString("comboBoxEventType.Items4"),
            resources.GetString("comboBoxEventType.Items5"),
            resources.GetString("comboBoxEventType.Items6"),
            resources.GetString("comboBoxEventType.Items7"),
            resources.GetString("comboBoxEventType.Items8"),
            resources.GetString("comboBoxEventType.Items9"),
            resources.GetString("comboBoxEventType.Items10")});
            this.comboBoxEventType.Name = "comboBoxEventType";
            this.comboBoxEventType.SelectedIndexChanged += new System.EventHandler(this.term_ValueChanged);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // backLoader
            // 
            this.backLoader.WorkerSupportsCancellation = true;
            this.backLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backLoader_DoWork);
            this.backLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backLoader_RunWorkerCompleted);
            // 
            // spreadSheetEvents
            // 
            resources.ApplyResources(this.spreadSheetEvents, "spreadSheetEvents");
            this.spreadSheetEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnWhen,
            this.ColumnFeature,
            this.ColumnEvent});
            this.spreadSheetEvents.Name = "spreadSheetEvents";
            this.spreadSheetEvents.ReadOnly = true;
            // 
            // ColumnWhen
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Format = "F";
            this.ColumnWhen.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.ColumnWhen, "ColumnWhen");
            this.ColumnWhen.Name = "ColumnWhen";
            this.ColumnWhen.ReadOnly = true;
            // 
            // ColumnFeature
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnFeature.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColumnFeature, "ColumnFeature");
            this.ColumnFeature.Name = "ColumnFeature";
            this.ColumnFeature.ReadOnly = true;
            // 
            // ColumnEvent
            // 
            this.ColumnEvent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnEvent.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColumnEvent, "ColumnEvent");
            this.ColumnEvent.Name = "ColumnEvent";
            this.ColumnEvent.ReadOnly = true;
            // 
            // ActionLogs
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxEventType);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.spreadSheetEvents);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ActionLogs";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.ActionLogs_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private Controls.SpreadSheet spreadSheetEvents;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.ComboBox comboBoxEventType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripStatusLabel statusEvents;
        private System.Windows.Forms.ToolStripStatusLabel statusUser;
        private System.Windows.Forms.ToolStripStatusLabel statusPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWhen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFeature;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEvent;
        private System.ComponentModel.BackgroundWorker backLoader;
    }
}