namespace Mayfly.Fish.Explorer
{
    partial class WizardTac
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.wizardControlTac = new AeroWizard.WizardControl();
            this.wizardPageEffort = new AeroWizard.WizardPage();
            this.labelGearInstruction = new System.Windows.Forms.Label();
            this.labelEUInstruction = new System.Windows.Forms.Label();
            this.labelGear = new System.Windows.Forms.Label();
            this.labelEU = new System.Windows.Forms.Label();
            this.comboBoxGear = new System.Windows.Forms.ComboBox();
            this.comboBoxUE = new System.Windows.Forms.ComboBox();
            this.wizardPageTitle = new AeroWizard.WizardPage();
            this.comboBoxSpecies = new System.Windows.Forms.ComboBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.textBoxSources = new System.Windows.Forms.TextBox();
            this.labelSources = new System.Windows.Forms.Label();
            this.textBoxFishery = new System.Windows.Forms.TextBox();
            this.labelFishery = new System.Windows.Forms.Label();
            this.labelSpecies = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPageData = new AeroWizard.WizardPage();
            this.spreadSheetData = new Mayfly.Controls.SpreadSheet();
            this.ColumnYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEffort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCpue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.openFileDialogData = new System.Windows.Forms.OpenFileDialog();
            this.userGetter = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlTac)).BeginInit();
            this.wizardPageEffort.SuspendLayout();
            this.wizardPageTitle.SuspendLayout();
            this.wizardPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetData)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControlTac
            // 
            this.wizardControlTac.Location = new System.Drawing.Point(0, 0);
            this.wizardControlTac.Name = "wizardControlTac";
            this.wizardControlTac.Pages.Add(this.wizardPageEffort);
            this.wizardControlTac.Pages.Add(this.wizardPageTitle);
            this.wizardControlTac.Pages.Add(this.wizardPageData);
            this.wizardControlTac.Pages.Add(this.wizardPage1);
            this.wizardControlTac.Pages.Add(this.wizardPage2);
            this.wizardControlTac.Size = new System.Drawing.Size(614, 511);
            this.wizardControlTac.TabIndex = 0;
            this.wizardControlTac.Title = "TAC wizard";
            this.wizardControlTac.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControlTac_Cancelling);
            // 
            // wizardPageEffort
            // 
            this.wizardPageEffort.Controls.Add(this.labelGearInstruction);
            this.wizardPageEffort.Controls.Add(this.labelEUInstruction);
            this.wizardPageEffort.Controls.Add(this.labelGear);
            this.wizardPageEffort.Controls.Add(this.labelEU);
            this.wizardPageEffort.Controls.Add(this.comboBoxGear);
            this.wizardPageEffort.Controls.Add(this.comboBoxUE);
            this.wizardPageEffort.Name = "wizardPageEffort";
            this.wizardPageEffort.Size = new System.Drawing.Size(567, 357);
            this.wizardPageEffort.TabIndex = 2;
            this.wizardPageEffort.Text = "Effort type selection";
            // 
            // labelGearInstruction
            // 
            this.labelGearInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGearInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelGearInstruction.Location = new System.Drawing.Point(0, 0);
            this.labelGearInstruction.Name = "labelGearInstruction";
            this.labelGearInstruction.Size = new System.Drawing.Size(540, 30);
            this.labelGearInstruction.TabIndex = 9;
            this.labelGearInstruction.Text = "To continue with analysis and report select gear type, data on which will be proc" +
    "essed.";
            // 
            // labelEUInstruction
            // 
            this.labelEUInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEUInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEUInstruction.Location = new System.Drawing.Point(0, 100);
            this.labelEUInstruction.Name = "labelEUInstruction";
            this.labelEUInstruction.Size = new System.Drawing.Size(540, 60);
            this.labelEUInstruction.TabIndex = 10;
            this.labelEUInstruction.Text = "Select effort unit - parameter to evaluate fishing effort. For active gears it is" +
    " real value of fished area or volume. For passive ones conventional (standard) e" +
    "ffort units are used.";
            // 
            // labelGear
            // 
            this.labelGear.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelGear.AutoSize = true;
            this.labelGear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelGear.Location = new System.Drawing.Point(25, 54);
            this.labelGear.Name = "labelGear";
            this.labelGear.Size = new System.Drawing.Size(60, 15);
            this.labelGear.TabIndex = 5;
            this.labelGear.Text = "Gear type:";
            // 
            // labelEU
            // 
            this.labelEU.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEU.AutoSize = true;
            this.labelEU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelEU.Location = new System.Drawing.Point(25, 186);
            this.labelEU.Name = "labelEU";
            this.labelEU.Size = new System.Drawing.Size(63, 15);
            this.labelEU.TabIndex = 7;
            this.labelEU.Text = "Effort unit:";
            // 
            // comboBoxGear
            // 
            this.comboBoxGear.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxGear.DisplayMember = "Name";
            this.comboBoxGear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGear.FormattingEnabled = true;
            this.comboBoxGear.Location = new System.Drawing.Point(162, 51);
            this.comboBoxGear.Name = "comboBoxGear";
            this.comboBoxGear.Size = new System.Drawing.Size(300, 23);
            this.comboBoxGear.Sorted = true;
            this.comboBoxGear.TabIndex = 6;
            this.comboBoxGear.ValueMember = "Type";
            this.comboBoxGear.SelectedIndexChanged += new System.EventHandler(this.comboBoxGear_SelectedIndexChanged);
            // 
            // comboBoxUE
            // 
            this.comboBoxUE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxUE.DisplayMember = "Value";
            this.comboBoxUE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUE.FormattingEnabled = true;
            this.comboBoxUE.Location = new System.Drawing.Point(162, 183);
            this.comboBoxUE.Name = "comboBoxUE";
            this.comboBoxUE.Size = new System.Drawing.Size(300, 23);
            this.comboBoxUE.TabIndex = 8;
            this.comboBoxUE.ValueMember = "Variant";
            this.comboBoxUE.SelectedIndexChanged += new System.EventHandler(this.comboBoxUE_SelectedIndexChanged);
            // 
            // wizardPageTitle
            // 
            this.wizardPageTitle.Controls.Add(this.comboBoxSpecies);
            this.wizardPageTitle.Controls.Add(this.buttonLoad);
            this.wizardPageTitle.Controls.Add(this.textBoxSources);
            this.wizardPageTitle.Controls.Add(this.labelSources);
            this.wizardPageTitle.Controls.Add(this.textBoxFishery);
            this.wizardPageTitle.Controls.Add(this.labelFishery);
            this.wizardPageTitle.Controls.Add(this.labelSpecies);
            this.wizardPageTitle.Controls.Add(this.label1);
            this.wizardPageTitle.Name = "wizardPageTitle";
            this.wizardPageTitle.Size = new System.Drawing.Size(567, 357);
            this.wizardPageTitle.TabIndex = 1;
            this.wizardPageTitle.Text = "Title data";
            this.wizardPageTitle.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageTitle_Commit);
            // 
            // comboBoxSpecies
            // 
            this.comboBoxSpecies.DisplayMember = "Species";
            this.comboBoxSpecies.FormattingEnabled = true;
            this.comboBoxSpecies.Location = new System.Drawing.Point(162, 51);
            this.comboBoxSpecies.Name = "comboBoxSpecies";
            this.comboBoxSpecies.Size = new System.Drawing.Size(321, 23);
            this.comboBoxSpecies.TabIndex = 15;
            this.comboBoxSpecies.ValueMember = "Species";
            this.comboBoxSpecies.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpecies_SelectedIndexChanged);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoad.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLoad.Location = new System.Drawing.Point(358, 138);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(125, 23);
            this.buttonLoad.TabIndex = 14;
            this.buttonLoad.Text = "Load from file...";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // textBoxSources
            // 
            this.textBoxSources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSources.Location = new System.Drawing.Point(162, 109);
            this.textBoxSources.Name = "textBoxSources";
            this.textBoxSources.Size = new System.Drawing.Size(321, 23);
            this.textBoxSources.TabIndex = 11;
            // 
            // labelSources
            // 
            this.labelSources.AutoSize = true;
            this.labelSources.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSources.Location = new System.Drawing.Point(24, 112);
            this.labelSources.Name = "labelSources";
            this.labelSources.Size = new System.Drawing.Size(48, 15);
            this.labelSources.TabIndex = 10;
            this.labelSources.Text = "Sources";
            // 
            // textBoxFishery
            // 
            this.textBoxFishery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFishery.Location = new System.Drawing.Point(162, 80);
            this.textBoxFishery.Name = "textBoxFishery";
            this.textBoxFishery.Size = new System.Drawing.Size(321, 23);
            this.textBoxFishery.TabIndex = 11;
            // 
            // labelFishery
            // 
            this.labelFishery.AutoSize = true;
            this.labelFishery.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFishery.Location = new System.Drawing.Point(24, 83);
            this.labelFishery.Name = "labelFishery";
            this.labelFishery.Size = new System.Drawing.Size(44, 15);
            this.labelFishery.TabIndex = 10;
            this.labelFishery.Text = "Fishery";
            // 
            // labelSpecies
            // 
            this.labelSpecies.AutoSize = true;
            this.labelSpecies.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSpecies.Location = new System.Drawing.Point(24, 54);
            this.labelSpecies.Name = "labelSpecies";
            this.labelSpecies.Size = new System.Drawing.Size(46, 15);
            this.labelSpecies.TabIndex = 10;
            this.labelSpecies.Text = "Species";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(540, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input some title data to distinguish final report.  Click \'Next\' to proceed to ca" +
    "tch series input.";
            // 
            // wizardPageData
            // 
            this.wizardPageData.Controls.Add(this.spreadSheetData);
            this.wizardPageData.Controls.Add(this.label4);
            this.wizardPageData.Name = "wizardPageData";
            this.wizardPageData.Size = new System.Drawing.Size(567, 357);
            this.wizardPageData.TabIndex = 0;
            this.wizardPageData.Text = "Catch data";
            // 
            // spreadSheetData
            // 
            this.spreadSheetData.AllowUserToAddRows = true;
            this.spreadSheetData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnYear,
            this.ColumnCatch,
            this.ColumnEffort,
            this.ColumnCpue});
            this.spreadSheetData.Location = new System.Drawing.Point(41, 51);
            this.spreadSheetData.Name = "spreadSheetData";
            this.spreadSheetData.Size = new System.Drawing.Size(458, 275);
            this.spreadSheetData.TabIndex = 1;
            this.spreadSheetData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetData_CellValueChanged);
            // 
            // ColumnYear
            // 
            dataGridViewCellStyle1.Format = "0000";
            this.ColumnYear.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnYear.HeaderText = "Year";
            this.ColumnYear.Name = "ColumnYear";
            // 
            // ColumnCatch
            // 
            this.ColumnCatch.HeaderText = "Catch, t";
            this.ColumnCatch.Name = "ColumnCatch";
            // 
            // ColumnEffort
            // 
            this.ColumnEffort.HeaderText = "Effort";
            this.ColumnEffort.Name = "ColumnEffort";
            // 
            // ColumnCpue
            // 
            this.ColumnCpue.HeaderText = "CPUE";
            this.ColumnCpue.Name = "ColumnCpue";
            this.ColumnCpue.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(540, 30);
            this.label4.TabIndex = 2;
            this.label4.Text = "Unfinished observations are detected. Activating one of them will forward you to " +
    "schedule page. Click \'Next\' to get through all steps. Clicking \'Next\' with no fi" +
    "le selected will start new observation.";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(567, 357);
            this.wizardPage1.TabIndex = 3;
            this.wizardPage1.Text = "wizardPage1";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(567, 357);
            this.wizardPage2.TabIndex = 4;
            this.wizardPage2.Text = "wizardPage2";
            // 
            // openFileDialogData
            // 
            this.openFileDialogData.Title = "Open Combi data file";
            // 
            // userGetter
            // 
            this.userGetter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.userGetter_DoWork);
            this.userGetter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.userGetter_RunWorkerCompleted);
            // 
            // WizardTac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 511);
            this.Controls.Add(this.wizardControlTac);
            this.MinimumSize = new System.Drawing.Size(630, 550);
            this.Name = "WizardTac";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlTac)).EndInit();
            this.wizardPageEffort.ResumeLayout(false);
            this.wizardPageEffort.PerformLayout();
            this.wizardPageTitle.ResumeLayout(false);
            this.wizardPageTitle.PerformLayout();
            this.wizardPageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControlTac;
        private AeroWizard.WizardPage wizardPageData;
        private AeroWizard.WizardPage wizardPageTitle;
        private AeroWizard.WizardPage wizardPageEffort;
        private System.Windows.Forms.Label label4;
        private Controls.SpreadSheet spreadSheetData;
        private System.Windows.Forms.OpenFileDialog openFileDialogData;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEffort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCpue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSpecies;
        private System.Windows.Forms.TextBox textBoxSources;
        private System.Windows.Forms.Label labelSources;
        private System.Windows.Forms.TextBox textBoxFishery;
        private System.Windows.Forms.Label labelFishery;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ComboBox comboBoxSpecies;
        private System.Windows.Forms.Label labelGearInstruction;
        private System.Windows.Forms.Label labelEUInstruction;
        private System.Windows.Forms.Label labelGear;
        private System.Windows.Forms.Label labelEU;
        public System.Windows.Forms.ComboBox comboBoxGear;
        public System.Windows.Forms.ComboBox comboBoxUE;
        private System.ComponentModel.BackgroundWorker userGetter;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage2;
    }
}

