namespace Mayfly.Library
{
    partial class ResearchEdit
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
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.openPDF = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSummary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxExecutive = new System.Windows.Forms.ComboBox();
            this.comboBoxApproved = new System.Windows.Forms.ComboBox();
            this.numericYear = new System.Windows.Forms.NumericUpDown();
            this.numericPages = new System.Windows.Forms.NumericUpDown();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelChapter1 = new System.Windows.Forms.Label();
            this.labelChapter3 = new System.Windows.Forms.Label();
            this.labelChapter2 = new System.Windows.Forms.Label();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.labelRack = new System.Windows.Forms.Label();
            this.labelShelf = new System.Windows.Forms.Label();
            this.comboBoxRack = new System.Windows.Forms.ComboBox();
            this.numericShelf = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxWhere = new System.Windows.Forms.ComboBox();
            this.labelWhere = new System.Windows.Forms.Label();
            this.comboBoxPublisher = new System.Windows.Forms.ComboBox();
            this.buttonRead = new System.Windows.Forms.Button();
            this.textBoxKeywords = new Mayfly.Controls.MultiStringSelector();
            this.textBoxAuthor = new Mayfly.Controls.MultiStringSelector();
            ((System.ComponentModel.ISupportInitialize)(this.numericYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericShelf)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(156, 115);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTitle.Size = new System.Drawing.Size(530, 40);
            this.textBoxTitle.TabIndex = 6;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // openPDF
            // 
            this.openPDF.Filter = "Файлы PDF|*.pdf";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Тема";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Год выполнения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Отв. исполнитель";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Утвержден";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Организация";
            // 
            // textBoxSummary
            // 
            this.textBoxSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSummary.Location = new System.Drawing.Point(156, 372);
            this.textBoxSummary.Multiline = true;
            this.textBoxSummary.Name = "textBoxSummary";
            this.textBoxSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSummary.Size = new System.Drawing.Size(530, 94);
            this.textBoxSummary.TabIndex = 21;
            this.textBoxSummary.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 375);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Реферат";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(385, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Страниц";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 349);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Ключевые слова";
            // 
            // comboBoxExecutive
            // 
            this.comboBoxExecutive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxExecutive.DisplayMember = "Name";
            this.comboBoxExecutive.FormattingEnabled = true;
            this.comboBoxExecutive.Location = new System.Drawing.Point(156, 161);
            this.comboBoxExecutive.Name = "comboBoxExecutive";
            this.comboBoxExecutive.Size = new System.Drawing.Size(530, 21);
            this.comboBoxExecutive.TabIndex = 8;
            this.comboBoxExecutive.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // comboBoxApproved
            // 
            this.comboBoxApproved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxApproved.DisplayMember = "Name";
            this.comboBoxApproved.FormattingEnabled = true;
            this.comboBoxApproved.Location = new System.Drawing.Point(156, 88);
            this.comboBoxApproved.Name = "comboBoxApproved";
            this.comboBoxApproved.Size = new System.Drawing.Size(530, 21);
            this.comboBoxApproved.TabIndex = 4;
            this.comboBoxApproved.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericYear
            // 
            this.numericYear.Location = new System.Drawing.Point(156, 215);
            this.numericYear.Maximum = new decimal(new int[] {
            2016,
            0,
            0,
            0});
            this.numericYear.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.numericYear.Name = "numericYear";
            this.numericYear.Size = new System.Drawing.Size(75, 20);
            this.numericYear.TabIndex = 12;
            this.numericYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericYear.Value = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            this.numericYear.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericPages
            // 
            this.numericPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericPages.Location = new System.Drawing.Point(611, 215);
            this.numericPages.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericPages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPages.Name = "numericPages";
            this.numericPages.Size = new System.Drawing.Size(75, 20);
            this.numericPages.TabIndex = 14;
            this.numericPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericPages.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericPages.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(566, 589);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(120, 23);
            this.buttonBrowse.TabIndex = 31;
            this.buttonBrowse.Text = "Обзор...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 594);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Путь к файлу";
            // 
            // textBoxFile
            // 
            this.textBoxFile.AllowDrop = true;
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(156, 591);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(404, 20);
            this.textBoxFile.TabIndex = 30;
            this.textBoxFile.TextChanged += new System.EventHandler(this.textBoxFile_TextChanged);
            this.textBoxFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFileName_DragDrop);
            this.textBoxFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxFileName_DragEnter);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(611, 640);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 28;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelChapter1
            // 
            this.labelChapter1.AutoSize = true;
            this.labelChapter1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelChapter1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelChapter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChapter1.Location = new System.Drawing.Point(25, 25);
            this.labelChapter1.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.labelChapter1.Name = "labelChapter1";
            this.labelChapter1.Size = new System.Drawing.Size(138, 15);
            this.labelChapter1.TabIndex = 0;
            this.labelChapter1.Text = "Заглавная информация";
            // 
            // labelChapter3
            // 
            this.labelChapter3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChapter3.AutoSize = true;
            this.labelChapter3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelChapter3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelChapter3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChapter3.Location = new System.Drawing.Point(25, 494);
            this.labelChapter3.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelChapter3.Name = "labelChapter3";
            this.labelChapter3.Size = new System.Drawing.Size(146, 15);
            this.labelChapter3.TabIndex = 22;
            this.labelChapter3.Text = "Информация о хранении";
            // 
            // labelChapter2
            // 
            this.labelChapter2.AutoSize = true;
            this.labelChapter2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelChapter2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelChapter2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChapter2.Location = new System.Drawing.Point(25, 303);
            this.labelChapter2.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.labelChapter2.Name = "labelChapter2";
            this.labelChapter2.Size = new System.Drawing.Size(120, 15);
            this.labelChapter2.TabIndex = 17;
            this.labelChapter2.Text = "Содержание работы";
            // 
            // numericQuantity
            // 
            this.numericQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericQuantity.Location = new System.Drawing.Point(156, 537);
            this.numericQuantity.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(75, 20);
            this.numericQuantity.TabIndex = 24;
            this.numericQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericQuantity.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelQuantity
            // 
            this.labelQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(45, 539);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(105, 13);
            this.labelQuantity.TabIndex = 23;
            this.labelQuantity.Text = "Количество единиц";
            // 
            // labelRack
            // 
            this.labelRack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelRack.AutoSize = true;
            this.labelRack.Location = new System.Drawing.Point(385, 539);
            this.labelRack.Name = "labelRack";
            this.labelRack.Size = new System.Drawing.Size(51, 13);
            this.labelRack.TabIndex = 25;
            this.labelRack.Text = "Стеллаж";
            // 
            // labelShelf
            // 
            this.labelShelf.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelShelf.AutoSize = true;
            this.labelShelf.Location = new System.Drawing.Point(385, 566);
            this.labelShelf.Name = "labelShelf";
            this.labelShelf.Size = new System.Drawing.Size(39, 13);
            this.labelShelf.TabIndex = 27;
            this.labelShelf.Text = "Полка";
            // 
            // comboBoxRack
            // 
            this.comboBoxRack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRack.DisplayMember = "Name";
            this.comboBoxRack.FormattingEnabled = true;
            this.comboBoxRack.Location = new System.Drawing.Point(611, 536);
            this.comboBoxRack.Name = "comboBoxRack";
            this.comboBoxRack.Size = new System.Drawing.Size(75, 21);
            this.comboBoxRack.TabIndex = 26;
            this.comboBoxRack.SelectedIndexChanged += new System.EventHandler(this.value_Changed);
            this.comboBoxRack.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericShelf
            // 
            this.numericShelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericShelf.Location = new System.Drawing.Point(611, 563);
            this.numericShelf.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericShelf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericShelf.Name = "numericShelf";
            this.numericShelf.Size = new System.Drawing.Size(75, 20);
            this.numericShelf.TabIndex = 28;
            this.numericShelf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericShelf.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericShelf.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 245);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Исполнители";
            // 
            // comboBoxWhere
            // 
            this.comboBoxWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWhere.DisplayMember = "Where";
            this.comboBoxWhere.FormattingEnabled = true;
            this.comboBoxWhere.Location = new System.Drawing.Point(156, 188);
            this.comboBoxWhere.Name = "comboBoxWhere";
            this.comboBoxWhere.Size = new System.Drawing.Size(530, 21);
            this.comboBoxWhere.TabIndex = 10;
            // 
            // labelWhere
            // 
            this.labelWhere.AutoSize = true;
            this.labelWhere.Location = new System.Drawing.Point(45, 191);
            this.labelWhere.Name = "labelWhere";
            this.labelWhere.Size = new System.Drawing.Size(84, 13);
            this.labelWhere.TabIndex = 9;
            this.labelWhere.Text = "Место издания";
            // 
            // comboBoxPublisher
            // 
            this.comboBoxPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPublisher.DisplayMember = "Name";
            this.comboBoxPublisher.FormattingEnabled = true;
            this.comboBoxPublisher.Location = new System.Drawing.Point(156, 62);
            this.comboBoxPublisher.Name = "comboBoxPublisher";
            this.comboBoxPublisher.Size = new System.Drawing.Size(530, 21);
            this.comboBoxPublisher.TabIndex = 4;
            this.comboBoxPublisher.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // buttonRead
            // 
            this.buttonRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRead.Location = new System.Drawing.Point(156, 640);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(120, 23);
            this.buttonRead.TabIndex = 31;
            this.buttonRead.Text = "Открыть отчет";
            this.buttonRead.UseVisualStyleBackColor = true;
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // textBoxKeywords
            // 
            this.textBoxKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeywords.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxKeywords.Location = new System.Drawing.Point(156, 346);
            this.textBoxKeywords.Name = "textBoxKeywords";
            this.textBoxKeywords.Options = null;
            this.textBoxKeywords.Separator = "; ";
            this.textBoxKeywords.Size = new System.Drawing.Size(530, 20);
            this.textBoxKeywords.Strings = new string[0];
            this.textBoxKeywords.TabIndex = 19;
            this.textBoxKeywords.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAuthor.Location = new System.Drawing.Point(156, 241);
            this.textBoxAuthor.Name = "textBoxAuthor";
            this.textBoxAuthor.Options = null;
            this.textBoxAuthor.Separator = "; ";
            this.textBoxAuthor.Size = new System.Drawing.Size(530, 20);
            this.textBoxAuthor.Strings = new string[0];
            this.textBoxAuthor.TabIndex = 16;
            this.textBoxAuthor.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // ResearchEdit
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(734, 711);
            this.Controls.Add(this.comboBoxWhere);
            this.Controls.Add(this.labelWhere);
            this.Controls.Add(this.labelChapter3);
            this.Controls.Add(this.numericShelf);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelChapter2);
            this.Controls.Add(this.labelChapter1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxRack);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelShelf);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelRack);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.comboBoxPublisher);
            this.Controls.Add(this.comboBoxApproved);
            this.Controls.Add(this.comboBoxExecutive);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxKeywords);
            this.Controls.Add(this.textBoxAuthor);
            this.Controls.Add(this.textBoxSummary);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.numericPages);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.numericYear);
            this.Controls.Add(this.numericQuantity);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 655);
            this.Name = "ResearchEdit";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование записи отчета о НИР";
            ((System.ComponentModel.ISupportInitialize)(this.numericYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericShelf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.OpenFileDialog openPDF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSummary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Mayfly.Controls.MultiStringSelector textBoxKeywords;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxExecutive;
        private System.Windows.Forms.ComboBox comboBoxApproved;
        private System.Windows.Forms.NumericUpDown numericYear;
        private System.Windows.Forms.NumericUpDown numericPages;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelChapter1;
        private System.Windows.Forms.Label labelChapter3;
        private System.Windows.Forms.Label labelChapter2;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label labelRack;
        private System.Windows.Forms.Label labelShelf;
        private System.Windows.Forms.ComboBox comboBoxRack;
        private System.Windows.Forms.NumericUpDown numericShelf;
        private System.Windows.Forms.Label label10;
        private Mayfly.Controls.MultiStringSelector textBoxAuthor;
        private System.Windows.Forms.ComboBox comboBoxWhere;
        private System.Windows.Forms.Label labelWhere;
        private System.Windows.Forms.ComboBox comboBoxPublisher;
        private System.Windows.Forms.Button buttonRead;
    }
}