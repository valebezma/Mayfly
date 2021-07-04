namespace QuizManager
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainerContent = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelRounds = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddRound = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxGameTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelGroupGeneral = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGameTitle = new System.Windows.Forms.Label();
            this.numericUpDownRoundTime = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanelTasks = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddTask = new System.Windows.Forms.Button();
            this.textBoxRoundTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.splitContainerTeam = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.tabPageTeam = new System.Windows.Forms.TabPage();
            this.tabPagePlay = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTasks = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTeams = new System.Windows.Forms.ToolStripStatusLabel();
            this.taskDialogSaveChanges = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbSave = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbDiscard = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelClose = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerContent)).BeginInit();
            this.splitContainerContent.Panel1.SuspendLayout();
            this.splitContainerContent.Panel2.SuspendLayout();
            this.splitContainerContent.SuspendLayout();
            this.flowLayoutPanelRounds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoundTime)).BeginInit();
            this.flowLayoutPanelTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTeam)).BeginInit();
            this.splitContainerTeam.Panel1.SuspendLayout();
            this.splitContainerTeam.Panel2.SuspendLayout();
            this.splitContainerTeam.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.tabPageTeam.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerContent
            // 
            resources.ApplyResources(this.splitContainerContent, "splitContainerContent");
            this.splitContainerContent.Name = "splitContainerContent";
            // 
            // splitContainerContent.Panel1
            // 
            this.splitContainerContent.Panel1.Controls.Add(this.flowLayoutPanelRounds);
            this.splitContainerContent.Panel1.Controls.Add(this.comboBox1);
            this.splitContainerContent.Panel1.Controls.Add(this.textBoxGameTitle);
            this.splitContainerContent.Panel1.Controls.Add(this.label2);
            this.splitContainerContent.Panel1.Controls.Add(this.labelGroupGeneral);
            this.splitContainerContent.Panel1.Controls.Add(this.label1);
            this.splitContainerContent.Panel1.Controls.Add(this.labelGameTitle);
            // 
            // splitContainerContent.Panel2
            // 
            this.splitContainerContent.Panel2.Controls.Add(this.numericUpDownRoundTime);
            this.splitContainerContent.Panel2.Controls.Add(this.flowLayoutPanelTasks);
            this.splitContainerContent.Panel2.Controls.Add(this.textBoxRoundTitle);
            this.splitContainerContent.Panel2.Controls.Add(this.label3);
            this.splitContainerContent.Panel2.Controls.Add(this.label5);
            this.splitContainerContent.Panel2.Controls.Add(this.label4);
            resources.ApplyResources(this.splitContainerContent.Panel2, "splitContainerContent.Panel2");
            // 
            // flowLayoutPanelRounds
            // 
            resources.ApplyResources(this.flowLayoutPanelRounds, "flowLayoutPanelRounds");
            this.flowLayoutPanelRounds.Controls.Add(this.buttonAddRound);
            this.flowLayoutPanelRounds.Name = "flowLayoutPanelRounds";
            // 
            // buttonAddRound
            // 
            resources.ApplyResources(this.buttonAddRound, "buttonAddRound");
            this.buttonAddRound.Name = "buttonAddRound";
            this.buttonAddRound.UseVisualStyleBackColor = true;
            this.buttonAddRound.Click += new System.EventHandler(this.buttonAddRound_Click);
            // 
            // comboBox1
            // 
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
            // 
            // textBoxGameTitle
            // 
            resources.ApplyResources(this.textBoxGameTitle, "textBoxGameTitle");
            this.textBoxGameTitle.Name = "textBoxGameTitle";
            this.textBoxGameTitle.TextChanged += new System.EventHandler(this.textBoxGameTitle_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Name = "label2";
            // 
            // labelGroupGeneral
            // 
            resources.ApplyResources(this.labelGroupGeneral, "labelGroupGeneral");
            this.labelGroupGeneral.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelGroupGeneral.Name = "labelGroupGeneral";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelGameTitle
            // 
            resources.ApplyResources(this.labelGameTitle, "labelGameTitle");
            this.labelGameTitle.Name = "labelGameTitle";
            // 
            // numericUpDownRoundTime
            // 
            resources.ApplyResources(this.numericUpDownRoundTime, "numericUpDownRoundTime");
            this.numericUpDownRoundTime.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownRoundTime.Name = "numericUpDownRoundTime";
            this.numericUpDownRoundTime.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownRoundTime.ValueChanged += new System.EventHandler(this.round_Changed);
            // 
            // flowLayoutPanelTasks
            // 
            resources.ApplyResources(this.flowLayoutPanelTasks, "flowLayoutPanelTasks");
            this.flowLayoutPanelTasks.Controls.Add(this.buttonAddTask);
            this.flowLayoutPanelTasks.Name = "flowLayoutPanelTasks";
            // 
            // buttonAddTask
            // 
            resources.ApplyResources(this.buttonAddTask, "buttonAddTask");
            this.buttonAddTask.Name = "buttonAddTask";
            this.buttonAddTask.UseVisualStyleBackColor = true;
            this.buttonAddTask.Click += new System.EventHandler(this.buttonAddTask_Click);
            // 
            // textBoxRoundTitle
            // 
            resources.ApplyResources(this.textBoxRoundTitle, "textBoxRoundTitle");
            this.textBoxRoundTitle.Name = "textBoxRoundTitle";
            this.textBoxRoundTitle.TextChanged += new System.EventHandler(this.round_Changed);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // splitContainerTeam
            // 
            resources.ApplyResources(this.splitContainerTeam, "splitContainerTeam");
            this.splitContainerTeam.Name = "splitContainerTeam";
            // 
            // splitContainerTeam.Panel1
            // 
            this.splitContainerTeam.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainerTeam.Panel1.Controls.Add(this.label6);
            // 
            // splitContainerTeam.Panel2
            // 
            this.splitContainerTeam.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainerTeam.Panel2.Controls.Add(this.textBox3);
            this.splitContainerTeam.Panel2.Controls.Add(this.label10);
            this.splitContainerTeam.Panel2.Controls.Add(this.label12);
            resources.ApplyResources(this.splitContainerTeam.Panel2, "splitContainerTeam.Panel2");
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Name = "label6";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.button2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.SteelBlue;
            this.label10.Name = "label10";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemNew,
            this.menuItemSave,
            this.menuItemSaveAs,
            this.toolStripSeparator1,
            this.menuItemClose});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            resources.ApplyResources(this.menuItemOpen, "menuItemOpen");
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            resources.ApplyResources(this.menuItemNew, "menuItemNew");
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            resources.ApplyResources(this.menuItemSave, "menuItemSave");
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            resources.ApplyResources(this.menuItemSaveAs, "menuItemSaveAs");
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            resources.ApplyResources(this.menuItemClose, "menuItemClose");
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageGame);
            this.tabControl1.Controls.Add(this.tabPageTeam);
            this.tabControl1.Controls.Add(this.tabPagePlay);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.splitContainerContent);
            resources.ApplyResources(this.tabPageGame, "tabPageGame");
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // tabPageTeam
            // 
            this.tabPageTeam.Controls.Add(this.splitContainerTeam);
            resources.ApplyResources(this.tabPageTeam, "tabPageTeam");
            this.tabPageTeam.Name = "tabPageTeam";
            this.tabPageTeam.UseVisualStyleBackColor = true;
            // 
            // tabPagePlay
            // 
            resources.ApplyResources(this.tabPagePlay, "tabPagePlay");
            this.tabPagePlay.Name = "tabPagePlay";
            this.tabPagePlay.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTasks,
            this.toolStripStatusLabelTeams});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabelTasks
            // 
            resources.ApplyResources(this.toolStripStatusLabelTasks, "toolStripStatusLabelTasks");
            this.toolStripStatusLabelTasks.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelTasks.Name = "toolStripStatusLabelTasks";
            // 
            // toolStripStatusLabelTeams
            // 
            resources.ApplyResources(this.toolStripStatusLabelTeams, "toolStripStatusLabelTeams");
            this.toolStripStatusLabelTeams.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelTeams.Name = "toolStripStatusLabelTeams";
            // 
            // taskDialogSaveChanges
            // 
            this.taskDialogSaveChanges.Buttons.Add(this.tdbSave);
            this.taskDialogSaveChanges.Buttons.Add(this.tdbDiscard);
            this.taskDialogSaveChanges.Buttons.Add(this.tdbCancelClose);
            this.taskDialogSaveChanges.CenterParent = true;
            resources.ApplyResources(this.taskDialogSaveChanges, "taskDialogSaveChanges");
            // 
            // tdbSave
            // 
            resources.ApplyResources(this.tdbSave, "tdbSave");
            // 
            // tdbDiscard
            // 
            resources.ApplyResources(this.tdbDiscard, "tdbDiscard");
            // 
            // tdbCancelClose
            // 
            this.tdbCancelClose.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.splitContainerContent.Panel1.ResumeLayout(false);
            this.splitContainerContent.Panel1.PerformLayout();
            this.splitContainerContent.Panel2.ResumeLayout(false);
            this.splitContainerContent.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerContent)).EndInit();
            this.splitContainerContent.ResumeLayout(false);
            this.flowLayoutPanelRounds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoundTime)).EndInit();
            this.flowLayoutPanelTasks.ResumeLayout(false);
            this.splitContainerTeam.Panel1.ResumeLayout(false);
            this.splitContainerTeam.Panel1.PerformLayout();
            this.splitContainerTeam.Panel2.ResumeLayout(false);
            this.splitContainerTeam.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTeam)).EndInit();
            this.splitContainerTeam.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.tabPageTeam.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGame;
        private System.Windows.Forms.TabPage tabPageTeam;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage tabPagePlay;
        private System.Windows.Forms.SplitContainer splitContainerContent;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRounds;
        private System.Windows.Forms.Button buttonAddRound;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBoxGameTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelGroupGeneral;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGameTitle;
        private System.Windows.Forms.NumericUpDown numericUpDownRoundTime;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTasks;
        private System.Windows.Forms.Button buttonAddTask;
        private System.Windows.Forms.TextBox textBoxRoundTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTasks;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTeams;
        private System.Windows.Forms.SplitContainer splitContainerTeam;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private Mayfly.TaskDialogs.TaskDialog taskDialogSaveChanges;
        private Mayfly.TaskDialogs.TaskDialogButton tdbSave;
        private Mayfly.TaskDialogs.TaskDialogButton tdbDiscard;
        private Mayfly.TaskDialogs.TaskDialogButton tdbCancelClose;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
    }
}

