namespace Mayfly.Fish.Legal
{
    partial class WizardNotes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardNotes));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.pageData = new AeroWizard.WizardPage();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.listViewCards = new System.Windows.Forms.ListView();
            this.columnFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnGear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnWhen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelWaterInstruction = new System.Windows.Forms.Label();
            this.pageCircumstances = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlace = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxWater = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGear = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBoxUseWater = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCompiler = new System.Windows.Forms.TextBox();
            this.textBoxBystander1 = new System.Windows.Forms.TextBox();
            this.textBoxBystander2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelBystander2 = new System.Windows.Forms.Label();
            this.pageCatch = new AeroWizard.WizardPage();
            this.spreadSheetCatches = new Mayfly.Controls.SpreadSheet();
            this.ColumnSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnExploration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMass = new Mayfly.Controls.SpreadSheetIconTextBoxColumn();
            this.ColumnFraction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextCatches = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextCatchDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.label8 = new System.Windows.Forms.Label();
            this.pageNote = new AeroWizard.WizardPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxDispose = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.radioButtonDispose = new System.Windows.Forms.RadioButton();
            this.radioButtonFood = new System.Windows.Forms.RadioButton();
            this.radioButtonExpedition = new System.Windows.Forms.RadioButton();
            this.radioButtonRelease = new System.Windows.Forms.RadioButton();
            this.radioButtonTransport = new System.Windows.Forms.RadioButton();
            this.radioButtonUtilization = new System.Windows.Forms.RadioButton();
            this.pageExpedition = new AeroWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxUseCompilerAsExpeditor = new System.Windows.Forms.CheckBox();
            this.checkBoxUseOrgAsDestination = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxDestination = new System.Windows.Forms.TextBox();
            this.textBoxAddressee = new System.Windows.Forms.TextBox();
            this.textBoxRoute = new System.Windows.Forms.TextBox();
            this.textBoxDish = new System.Windows.Forms.TextBox();
            this.textBoxConservant = new System.Windows.Forms.TextBox();
            this.textBoxExpeditorRequisites = new System.Windows.Forms.TextBox();
            this.textBoxExpeditor = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.loaderData = new System.ComponentModel.BackgroundWorker();
            this.speciesLogger = new Mayfly.Species.SpeciesSelector(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.pageData.SuspendLayout();
            this.pageCircumstances.SuspendLayout();
            this.pageCatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).BeginInit();
            this.contextCatches.SuspendLayout();
            this.pageNote.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pageExpedition.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonToolTipText = "Вернуться на предыдущий этап";
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.CancelButtonText = "Отмена";
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishButtonText = "Готово";
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonText = "Далее";
            this.wizardControl1.Pages.Add(this.pageData);
            this.wizardControl1.Pages.Add(this.pageCircumstances);
            this.wizardControl1.Pages.Add(this.pageCatch);
            this.wizardControl1.Pages.Add(this.pageNote);
            this.wizardControl1.Pages.Add(this.pageExpedition);
            this.wizardControl1.Size = new System.Drawing.Size(614, 512);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "Составление отчетности по вылову";
            this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
            this.wizardControl1.Finished += new System.EventHandler(this.wizardControl1_Finished);
            // 
            // pageData
            // 
            this.pageData.AllowNext = false;
            this.pageData.Controls.Add(this.buttonLoad);
            this.pageData.Controls.Add(this.listViewCards);
            this.pageData.Controls.Add(this.labelWaterInstruction);
            this.pageData.Name = "pageData";
            this.pageData.Size = new System.Drawing.Size(567, 358);
            this.pageData.TabIndex = 4;
            this.pageData.Text = "Добавление карточек улова рыбы";
            this.pageData.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.pageData_Commit);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoad.Location = new System.Drawing.Point(33, 320);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(150, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Загрузить карточки";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // listViewCards
            // 
            this.listViewCards.AllowDrop = true;
            this.listViewCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCards.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewCards.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFile,
            this.columnGear,
            this.columnWhen});
            this.listViewCards.FullRowSelect = true;
            this.listViewCards.Location = new System.Drawing.Point(33, 65);
            this.listViewCards.Name = "listViewCards";
            this.listViewCards.ShowGroups = false;
            this.listViewCards.Size = new System.Drawing.Size(475, 249);
            this.listViewCards.TabIndex = 1;
            this.listViewCards.TileSize = new System.Drawing.Size(150, 25);
            this.listViewCards.UseCompatibleStateImageBehavior = false;
            this.listViewCards.View = System.Windows.Forms.View.Details;
            this.listViewCards.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewCards_DragDrop);
            this.listViewCards.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewCards_DragEnter);
            // 
            // columnFile
            // 
            this.columnFile.Text = "Имя карточки";
            this.columnFile.Width = 150;
            // 
            // columnGear
            // 
            this.columnGear.Text = "Орудие лова";
            this.columnGear.Width = 150;
            // 
            // columnWhen
            // 
            this.columnWhen.Text = "Время выемки улова";
            this.columnWhen.Width = 120;
            // 
            // labelWaterInstruction
            // 
            this.labelWaterInstruction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWaterInstruction.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelWaterInstruction.Location = new System.Drawing.Point(0, 0);
            this.labelWaterInstruction.Name = "labelWaterInstruction";
            this.labelWaterInstruction.Size = new System.Drawing.Size(540, 30);
            this.labelWaterInstruction.TabIndex = 0;
            this.labelWaterInstruction.Text = "Для составления актов проведения контрольного лова и последующей нормативной доку" +
    "ментации загрузите карточки улова рыбы.";
            // 
            // pageCircumstances
            // 
            this.pageCircumstances.Controls.Add(this.label1);
            this.pageCircumstances.Controls.Add(this.textBoxPlace);
            this.pageCircumstances.Controls.Add(this.label15);
            this.pageCircumstances.Controls.Add(this.textBoxWater);
            this.pageCircumstances.Controls.Add(this.label3);
            this.pageCircumstances.Controls.Add(this.textBoxGear);
            this.pageCircumstances.Controls.Add(this.label14);
            this.pageCircumstances.Controls.Add(this.checkBoxUseWater);
            this.pageCircumstances.Controls.Add(this.label5);
            this.pageCircumstances.Controls.Add(this.textBoxCompiler);
            this.pageCircumstances.Controls.Add(this.textBoxBystander1);
            this.pageCircumstances.Controls.Add(this.textBoxBystander2);
            this.pageCircumstances.Controls.Add(this.label2);
            this.pageCircumstances.Controls.Add(this.labelBystander2);
            this.pageCircumstances.Name = "pageCircumstances";
            this.pageCircumstances.Size = new System.Drawing.Size(567, 358);
            this.pageCircumstances.TabIndex = 0;
            this.pageCircumstances.Text = "Определение обстоятельств";
            this.pageCircumstances.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPagePersons_Commit);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(540, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите обстоятельства составления актов и, если необходимо, откорректируйте знач" +
    "ения полей:";
            // 
            // textBoxPlace
            // 
            this.textBoxPlace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPlace.Location = new System.Drawing.Point(215, 135);
            this.textBoxPlace.Name = "textBoxPlace";
            this.textBoxPlace.Size = new System.Drawing.Size(300, 23);
            this.textBoxPlace.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 138);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(147, 15);
            this.label15.TabIndex = 7;
            this.label15.Text = "Место составления актов";
            // 
            // textBoxWater
            // 
            this.textBoxWater.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWater.Location = new System.Drawing.Point(215, 218);
            this.textBoxWater.Name = "textBoxWater";
            this.textBoxWater.Size = new System.Drawing.Size(300, 23);
            this.textBoxWater.TabIndex = 13;
            this.textBoxWater.TextChanged += new System.EventHandler(this.textBoxWater_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Водный объект";
            // 
            // textBoxGear
            // 
            this.textBoxGear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGear.Location = new System.Drawing.Point(215, 189);
            this.textBoxGear.Name = "textBoxGear";
            this.textBoxGear.Size = new System.Drawing.Size(300, 23);
            this.textBoxGear.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(25, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 15);
            this.label14.TabIndex = 10;
            this.label14.Text = "Орудия лова";
            // 
            // checkBoxUseWater
            // 
            this.checkBoxUseWater.AutoSize = true;
            this.checkBoxUseWater.Location = new System.Drawing.Point(215, 164);
            this.checkBoxUseWater.Name = "checkBoxUseWater";
            this.checkBoxUseWater.Size = new System.Drawing.Size(249, 19);
            this.checkBoxUseWater.TabIndex = 9;
            this.checkBoxUseWater.Text = "использовать название водного объекта";
            this.checkBoxUseWater.UseVisualStyleBackColor = true;
            this.checkBoxUseWater.CheckedChanged += new System.EventHandler(this.checkBoxUseWater_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(25, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Составитель";
            // 
            // textBoxCompiler
            // 
            this.textBoxCompiler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCompiler.Location = new System.Drawing.Point(215, 48);
            this.textBoxCompiler.Name = "textBoxCompiler";
            this.textBoxCompiler.ReadOnly = true;
            this.textBoxCompiler.Size = new System.Drawing.Size(300, 23);
            this.textBoxCompiler.TabIndex = 2;
            // 
            // textBoxBystander1
            // 
            this.textBoxBystander1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBystander1.Location = new System.Drawing.Point(215, 77);
            this.textBoxBystander1.Name = "textBoxBystander1";
            this.textBoxBystander1.Size = new System.Drawing.Size(300, 23);
            this.textBoxBystander1.TabIndex = 4;
            this.textBoxBystander1.TextChanged += new System.EventHandler(this.textBoxBystander1_TextChanged);
            // 
            // textBoxBystander2
            // 
            this.textBoxBystander2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBystander2.Location = new System.Drawing.Point(215, 106);
            this.textBoxBystander2.Name = "textBoxBystander2";
            this.textBoxBystander2.ReadOnly = true;
            this.textBoxBystander2.Size = new System.Drawing.Size(300, 23);
            this.textBoxBystander2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Наблюдатель 1";
            // 
            // labelBystander2
            // 
            this.labelBystander2.AutoSize = true;
            this.labelBystander2.Location = new System.Drawing.Point(25, 109);
            this.labelBystander2.Name = "labelBystander2";
            this.labelBystander2.Size = new System.Drawing.Size(91, 15);
            this.labelBystander2.TabIndex = 5;
            this.labelBystander2.Text = "Наблюдатель 2";
            // 
            // pageCatch
            // 
            this.pageCatch.AllowNext = false;
            this.pageCatch.Controls.Add(this.spreadSheetCatches);
            this.pageCatch.Controls.Add(this.label8);
            this.pageCatch.Name = "pageCatch";
            this.pageCatch.Size = new System.Drawing.Size(567, 358);
            this.pageCatch.TabIndex = 5;
            this.pageCatch.Text = "Описание улова";
            // 
            // spreadSheetCatches
            // 
            this.spreadSheetCatches.AllowUserToAddRows = true;
            this.spreadSheetCatches.AllowUserToDeleteRows = true;
            this.spreadSheetCatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadSheetCatches.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.spreadSheetCatches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpecies,
            this.ColumnExploration,
            this.ColumnQuantity,
            this.ColumnMass,
            this.ColumnFraction});
            this.spreadSheetCatches.Location = new System.Drawing.Point(0, 48);
            this.spreadSheetCatches.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.spreadSheetCatches.Name = "spreadSheetCatches";
            this.spreadSheetCatches.RowMenu = this.contextCatches;
            this.spreadSheetCatches.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.spreadSheetCatches.Size = new System.Drawing.Size(540, 295);
            this.spreadSheetCatches.TabIndex = 1;
            this.spreadSheetCatches.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCatches_CellValueChanged);
            this.spreadSheetCatches.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.spreadSheetCatches_RowEnter);
            this.spreadSheetCatches.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.spreadSheetCatches_RowsRemoved);
            this.spreadSheetCatches.SelectionChanged += new System.EventHandler(this.spreadSheetCatches_SelectionChanged);
            // 
            // ColumnSpecies
            // 
            this.ColumnSpecies.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ColumnSpecies.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnSpecies.HeaderText = "Вид";
            this.ColumnSpecies.Name = "ColumnSpecies";
            // 
            // ColumnExploration
            // 
            dataGridViewCellStyle2.Format = "P1";
            this.ColumnExploration.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnExploration.HeaderText = "Текущее освоение, %";
            this.ColumnExploration.Name = "ColumnExploration";
            this.ColumnExploration.ReadOnly = true;
            this.ColumnExploration.Width = 90;
            // 
            // ColumnQuantity
            // 
            dataGridViewCellStyle3.Format = "N0";
            this.ColumnQuantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnQuantity.HeaderText = "Количество, экз.";
            this.ColumnQuantity.Name = "ColumnQuantity";
            this.ColumnQuantity.Width = 90;
            // 
            // ColumnMass
            // 
            dataGridViewCellStyle4.Format = "N1";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ColumnMass.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnMass.HeaderText = "Масса, кг";
            this.ColumnMass.Image = null;
            this.ColumnMass.Name = "ColumnMass";
            this.ColumnMass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnMass.Width = 90;
            // 
            // ColumnFraction
            // 
            dataGridViewCellStyle5.Format = "P1";
            this.ColumnFraction.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnFraction.HeaderText = "Добавочная доля квоты, %";
            this.ColumnFraction.Name = "ColumnFraction";
            this.ColumnFraction.Width = 90;
            // 
            // contextCatches
            // 
            this.contextCatches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCatchDelete});
            this.contextCatches.Name = "contextCatches";
            this.contextCatches.Size = new System.Drawing.Size(119, 26);
            // 
            // contextCatchDelete
            // 
            this.contextCatchDelete.Name = "contextCatchDelete";
            this.contextCatchDelete.Size = new System.Drawing.Size(118, 22);
            this.contextCatchDelete.Text = "Удалить";
            this.contextCatchDelete.Click += new System.EventHandler(this.contextCatchDelete_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(540, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "На основе загруженных карточек составлен улов, который будет отражен в актах. Вне" +
    "сите изменения, если это требуется и переходите к следующему шагу.";
            // 
            // pageNote
            // 
            this.pageNote.AllowNext = false;
            this.pageNote.Controls.Add(this.dateTimePicker1);
            this.pageNote.Controls.Add(this.label20);
            this.pageNote.Controls.Add(this.label7);
            this.pageNote.Controls.Add(this.label6);
            this.pageNote.Controls.Add(this.panel1);
            this.pageNote.Controls.Add(this.radioButtonRelease);
            this.pageNote.Controls.Add(this.radioButtonTransport);
            this.pageNote.Controls.Add(this.radioButtonUtilization);
            this.pageNote.Name = "pageNote";
            this.pageNote.NextPage = this.pageExpedition;
            this.pageNote.Size = new System.Drawing.Size(567, 358);
            this.pageNote.TabIndex = 2;
            this.pageNote.Text = "Форма отчетности";
            this.pageNote.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageNote_Commit);
            this.pageNote.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageNote_Initialize);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dateTimePicker1.Location = new System.Drawing.Point(283, 56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(232, 23);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.Location = new System.Drawing.Point(0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(540, 30);
            this.label20.TabIndex = 0;
            this.label20.Text = "Укажите какую документацию следует сформировать для каждого акта проведения контр" +
    "ольного лова.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-3, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(411, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "Сопроводительная к актам проведения котрольного лова документация:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Укажите дату составления актов:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBoxDispose);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.radioButtonDispose);
            this.panel1.Controls.Add(this.radioButtonFood);
            this.panel1.Controls.Add(this.radioButtonExpedition);
            this.panel1.Location = new System.Drawing.Point(0, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(537, 120);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // textBoxDispose
            // 
            this.textBoxDispose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxDispose.Location = new System.Drawing.Point(283, 41);
            this.textBoxDispose.Name = "textBoxDispose";
            this.textBoxDispose.Size = new System.Drawing.Size(232, 23);
            this.textBoxDispose.TabIndex = 2;
            this.textBoxDispose.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 10);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(215, 15);
            this.label21.TabIndex = 0;
            this.label21.Text = "Укажите вариант уничтожения улова:";
            // 
            // radioButtonDispose
            // 
            this.radioButtonDispose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonDispose.AutoSize = true;
            this.radioButtonDispose.Location = new System.Drawing.Point(65, 42);
            this.radioButtonDispose.Name = "radioButtonDispose";
            this.radioButtonDispose.Size = new System.Drawing.Size(212, 19);
            this.radioButtonDispose.TabIndex = 1;
            this.radioButtonDispose.TabStop = true;
            this.radioButtonDispose.Text = "уничтожен указанным способом:";
            this.radioButtonDispose.UseVisualStyleBackColor = true;
            this.radioButtonDispose.CheckedChanged += new System.EventHandler(this.radioButtonUtilization_CheckedChanged);
            // 
            // radioButtonFood
            // 
            this.radioButtonFood.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonFood.AutoSize = true;
            this.radioButtonFood.Location = new System.Drawing.Point(65, 67);
            this.radioButtonFood.Name = "radioButtonFood";
            this.radioButtonFood.Size = new System.Drawing.Size(140, 19);
            this.radioButtonFood.TabIndex = 3;
            this.radioButtonFood.TabStop = true;
            this.radioButtonFood.Text = "передан для питания";
            this.radioButtonFood.UseVisualStyleBackColor = true;
            this.radioButtonFood.CheckedChanged += new System.EventHandler(this.radioButtonUtilization_CheckedChanged);
            // 
            // radioButtonExpedition
            // 
            this.radioButtonExpedition.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonExpedition.AutoSize = true;
            this.radioButtonExpedition.Location = new System.Drawing.Point(65, 92);
            this.radioButtonExpedition.Name = "radioButtonExpedition";
            this.radioButtonExpedition.Size = new System.Drawing.Size(432, 19);
            this.radioButtonExpedition.TabIndex = 4;
            this.radioButtonExpedition.TabStop = true;
            this.radioButtonExpedition.Text = "передан уполномоченному лицу для уничтожения или транспортировки";
            this.radioButtonExpedition.UseVisualStyleBackColor = true;
            this.radioButtonExpedition.CheckedChanged += new System.EventHandler(this.radioButtonUtilization_CheckedChanged);
            // 
            // radioButtonRelease
            // 
            this.radioButtonRelease.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonRelease.AutoSize = true;
            this.radioButtonRelease.Location = new System.Drawing.Point(65, 128);
            this.radioButtonRelease.Name = "radioButtonRelease";
            this.radioButtonRelease.Size = new System.Drawing.Size(158, 19);
            this.radioButtonRelease.TabIndex = 4;
            this.radioButtonRelease.TabStop = true;
            this.radioButtonRelease.Text = "улов возвращен в среду";
            this.radioButtonRelease.UseVisualStyleBackColor = true;
            this.radioButtonRelease.CheckedChanged += new System.EventHandler(this.radioButtonNote_CheckedChanged);
            // 
            // radioButtonTransport
            // 
            this.radioButtonTransport.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonTransport.AutoSize = true;
            this.radioButtonTransport.Location = new System.Drawing.Point(65, 178);
            this.radioButtonTransport.Name = "radioButtonTransport";
            this.radioButtonTransport.Size = new System.Drawing.Size(289, 19);
            this.radioButtonTransport.TabIndex = 6;
            this.radioButtonTransport.TabStop = true;
            this.radioButtonTransport.Text = "улов транспортирован в научную организацию";
            this.radioButtonTransport.UseVisualStyleBackColor = true;
            this.radioButtonTransport.CheckedChanged += new System.EventHandler(this.radioButtonNote_CheckedChanged);
            // 
            // radioButtonUtilization
            // 
            this.radioButtonUtilization.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioButtonUtilization.AutoSize = true;
            this.radioButtonUtilization.Location = new System.Drawing.Point(65, 153);
            this.radioButtonUtilization.Name = "radioButtonUtilization";
            this.radioButtonUtilization.Size = new System.Drawing.Size(131, 19);
            this.radioButtonUtilization.TabIndex = 5;
            this.radioButtonUtilization.TabStop = true;
            this.radioButtonUtilization.Text = "улов утилизирован";
            this.radioButtonUtilization.UseVisualStyleBackColor = true;
            this.radioButtonUtilization.CheckedChanged += new System.EventHandler(this.radioButtonNote_CheckedChanged);
            // 
            // pageExpedition
            // 
            this.pageExpedition.Controls.Add(this.label4);
            this.pageExpedition.Controls.Add(this.checkBoxUseCompilerAsExpeditor);
            this.pageExpedition.Controls.Add(this.checkBoxUseOrgAsDestination);
            this.pageExpedition.Controls.Add(this.label19);
            this.pageExpedition.Controls.Add(this.label18);
            this.pageExpedition.Controls.Add(this.label17);
            this.pageExpedition.Controls.Add(this.label23);
            this.pageExpedition.Controls.Add(this.label22);
            this.pageExpedition.Controls.Add(this.label26);
            this.pageExpedition.Controls.Add(this.label16);
            this.pageExpedition.Controls.Add(this.textBoxDestination);
            this.pageExpedition.Controls.Add(this.textBoxAddressee);
            this.pageExpedition.Controls.Add(this.textBoxRoute);
            this.pageExpedition.Controls.Add(this.textBoxDish);
            this.pageExpedition.Controls.Add(this.textBoxConservant);
            this.pageExpedition.Controls.Add(this.textBoxExpeditorRequisites);
            this.pageExpedition.Controls.Add(this.textBoxExpeditor);
            this.pageExpedition.IsFinishPage = true;
            this.pageExpedition.Name = "pageExpedition";
            this.pageExpedition.Size = new System.Drawing.Size(567, 358);
            this.pageExpedition.TabIndex = 3;
            this.pageExpedition.Text = "Транспортировка биологических образцов";
            this.pageExpedition.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPageExpedition_Commit);
            this.pageExpedition.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPageExpedition_Initialize);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(540, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "Укажите реквизиты принимающей организации и, если необходимо, откорректируйте зна" +
    "чения полей:";
            // 
            // checkBoxUseCompilerAsExpeditor
            // 
            this.checkBoxUseCompilerAsExpeditor.AutoSize = true;
            this.checkBoxUseCompilerAsExpeditor.Location = new System.Drawing.Point(215, 78);
            this.checkBoxUseCompilerAsExpeditor.Name = "checkBoxUseCompilerAsExpeditor";
            this.checkBoxUseCompilerAsExpeditor.Size = new System.Drawing.Size(234, 19);
            this.checkBoxUseCompilerAsExpeditor.TabIndex = 3;
            this.checkBoxUseCompilerAsExpeditor.Text = "использовать сведения о составителе";
            this.checkBoxUseCompilerAsExpeditor.UseVisualStyleBackColor = true;
            this.checkBoxUseCompilerAsExpeditor.CheckedChanged += new System.EventHandler(this.checkBoxUseCompiler_CheckedChanged);
            // 
            // checkBoxUseOrgAsDestination
            // 
            this.checkBoxUseOrgAsDestination.AutoSize = true;
            this.checkBoxUseOrgAsDestination.Location = new System.Drawing.Point(215, 242);
            this.checkBoxUseOrgAsDestination.Name = "checkBoxUseOrgAsDestination";
            this.checkBoxUseOrgAsDestination.Size = new System.Drawing.Size(267, 19);
            this.checkBoxUseOrgAsDestination.TabIndex = 12;
            this.checkBoxUseOrgAsDestination.Text = "использовать название вашей организации";
            this.checkBoxUseOrgAsDestination.UseVisualStyleBackColor = true;
            this.checkBoxUseOrgAsDestination.CheckedChanged += new System.EventHandler(this.checkBoxUseOrg_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(25, 164);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(137, 15);
            this.label19.TabIndex = 8;
            this.label19.Text = "Адрес грузополучателя";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(25, 193);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(148, 15);
            this.label18.TabIndex = 10;
            this.label18.Text = "Организация-получатель";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(25, 135);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 15);
            this.label17.TabIndex = 6;
            this.label17.Text = "Маршрут перевозки";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(25, 299);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(134, 15);
            this.label23.TabIndex = 15;
            this.label23.Text = "Вид и количество тары";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label22.Location = new System.Drawing.Point(25, 270);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(107, 15);
            this.label22.TabIndex = 13;
            this.label22.Text = "Способ фиксации";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(25, 106);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(148, 15);
            this.label26.TabIndex = 4;
            this.label26.Text = "Сведения о полномочиях";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(25, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(136, 15);
            this.label16.TabIndex = 1;
            this.label16.Text = "Уполномоченное лицо";
            // 
            // textBoxDestination
            // 
            this.textBoxDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDestination.Location = new System.Drawing.Point(215, 161);
            this.textBoxDestination.Name = "textBoxDestination";
            this.textBoxDestination.Size = new System.Drawing.Size(300, 23);
            this.textBoxDestination.TabIndex = 9;
            // 
            // textBoxAddressee
            // 
            this.textBoxAddressee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddressee.Location = new System.Drawing.Point(215, 190);
            this.textBoxAddressee.Multiline = true;
            this.textBoxAddressee.Name = "textBoxAddressee";
            this.textBoxAddressee.Size = new System.Drawing.Size(300, 46);
            this.textBoxAddressee.TabIndex = 11;
            // 
            // textBoxRoute
            // 
            this.textBoxRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRoute.Location = new System.Drawing.Point(215, 132);
            this.textBoxRoute.Name = "textBoxRoute";
            this.textBoxRoute.Size = new System.Drawing.Size(300, 23);
            this.textBoxRoute.TabIndex = 7;
            // 
            // textBoxDish
            // 
            this.textBoxDish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDish.Location = new System.Drawing.Point(215, 296);
            this.textBoxDish.Name = "textBoxDish";
            this.textBoxDish.Size = new System.Drawing.Size(300, 23);
            this.textBoxDish.TabIndex = 16;
            // 
            // textBoxConservant
            // 
            this.textBoxConservant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConservant.Location = new System.Drawing.Point(215, 267);
            this.textBoxConservant.Name = "textBoxConservant";
            this.textBoxConservant.Size = new System.Drawing.Size(300, 23);
            this.textBoxConservant.TabIndex = 14;
            // 
            // textBoxExpeditorRequisites
            // 
            this.textBoxExpeditorRequisites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExpeditorRequisites.Location = new System.Drawing.Point(215, 103);
            this.textBoxExpeditorRequisites.Name = "textBoxExpeditorRequisites";
            this.textBoxExpeditorRequisites.Size = new System.Drawing.Size(300, 23);
            this.textBoxExpeditorRequisites.TabIndex = 5;
            // 
            // textBoxExpeditor
            // 
            this.textBoxExpeditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExpeditor.Location = new System.Drawing.Point(215, 48);
            this.textBoxExpeditor.Name = "textBoxExpeditor";
            this.textBoxExpeditor.Size = new System.Drawing.Size(300, 23);
            this.textBoxExpeditor.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "2.ico");
            // 
            // loaderData
            // 
            this.loaderData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataLoader_DoWork);
            this.loaderData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dataLoader_RunWorkerCompleted);
            // 
            // speciesLogger
            // 
            this.speciesLogger.CheckDuplicates = false;
            this.speciesLogger.ColumnName = "ColumnSpecies";
            this.speciesLogger.Grid = this.spreadSheetCatches;
            this.speciesLogger.RecentListCount = 0;
            this.speciesLogger.SpeciesSelected += new Mayfly.Species.SpeciesSelectEventHandler(this.speciesLogger_SpeciesSelected);
            this.speciesLogger.DuplicateFound += new Mayfly.Species.DuplicateFoundEventHandler(this.speciesLogger_DuplicateDetected);
            // 
            // WizardNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 512);
            this.Controls.Add(this.wizardControl1);
            this.MinimumSize = new System.Drawing.Size(630, 550);
            this.Name = "WizardNotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.pageData.ResumeLayout(false);
            this.pageCircumstances.ResumeLayout(false);
            this.pageCircumstances.PerformLayout();
            this.pageCatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spreadSheetCatches)).EndInit();
            this.contextCatches.ResumeLayout(false);
            this.pageNote.ResumeLayout(false);
            this.pageNote.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pageExpedition.ResumeLayout(false);
            this.pageExpedition.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage pageCircumstances;
        private AeroWizard.WizardPage pageNote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCompiler;
        private System.Windows.Forms.TextBox textBoxBystander1;
        private System.Windows.Forms.TextBox textBoxBystander2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBystander2;
        private System.Windows.Forms.RadioButton radioButtonRelease;
        private System.Windows.Forms.RadioButton radioButtonUtilization;
        private System.Windows.Forms.RadioButton radioButtonExpedition;
        private System.Windows.Forms.RadioButton radioButtonFood;
        private System.Windows.Forms.RadioButton radioButtonDispose;
        private System.Windows.Forms.Panel panel1;
        private AeroWizard.WizardPage pageExpedition;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxDestination;
        private System.Windows.Forms.TextBox textBoxAddressee;
        private System.Windows.Forms.TextBox textBoxRoute;
        private System.Windows.Forms.TextBox textBoxExpeditor;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxDish;
        private System.Windows.Forms.TextBox textBoxConservant;
        private System.Windows.Forms.CheckBox checkBoxUseOrgAsDestination;
        private System.Windows.Forms.TextBox textBoxDispose;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBoxExpeditorRequisites;
        private System.Windows.Forms.CheckBox checkBoxUseCompilerAsExpeditor;
        private System.Windows.Forms.TextBox textBoxPlace;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxGear;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxUseWater;
        private System.Windows.Forms.RadioButton radioButtonTransport;
        private AeroWizard.WizardPage pageData;
        private System.Windows.Forms.Label labelWaterInstruction;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ListView listViewCards;
        private System.ComponentModel.BackgroundWorker loaderData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWater;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnFile;
        private System.Windows.Forms.ColumnHeader columnGear;
        private System.Windows.Forms.ColumnHeader columnWhen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private AeroWizard.WizardPage pageCatch;
        private System.Windows.Forms.Label label8;
        private Controls.SpreadSheet spreadSheetCatches;
        private System.Windows.Forms.ContextMenuStrip contextCatches;
        private System.Windows.Forms.ToolStripMenuItem contextCatchDelete;
        private Species.SpeciesSelector speciesLogger;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnExploration;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private Controls.SpreadSheetIconTextBoxColumn ColumnMass;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFraction;
    }
}