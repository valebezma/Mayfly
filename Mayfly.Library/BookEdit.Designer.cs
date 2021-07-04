namespace Mayfly.Library
{
    partial class BookEdit
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
            this.openPDF = new System.Windows.Forms.OpenFileDialog();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelWhen = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelPages = new System.Windows.Forms.Label();
            this.textBoxAuthor = new Mayfly.Controls.MultiStringSelector();
            this.numericYear = new System.Windows.Forms.NumericUpDown();
            this.numericPages = new System.Windows.Forms.NumericUpDown();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelFile = new System.Windows.Forms.Label();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelPublisher = new System.Windows.Forms.Label();
            this.comboBoxPublisher = new System.Windows.Forms.ComboBox();
            this.labelUDK = new System.Windows.Forms.Label();
            this.comboBoxUDK = new System.Windows.Forms.ComboBox();
            this.labelBBK = new System.Windows.Forms.Label();
            this.comboBoxBBK = new System.Windows.Forms.ComboBox();
            this.labelISBN = new System.Windows.Forms.Label();
            this.labelWhere = new System.Windows.Forms.Label();
            this.comboBoxWhere = new System.Windows.Forms.ComboBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.labelRack = new System.Windows.Forms.Label();
            this.comboBoxRack = new System.Windows.Forms.ComboBox();
            this.labelShelf = new System.Windows.Forms.Label();
            this.labelChapter1 = new System.Windows.Forms.Label();
            this.labelChapter2 = new System.Windows.Forms.Label();
            this.labelChapter3 = new System.Windows.Forms.Label();
            this.numericShelf = new System.Windows.Forms.NumericUpDown();
            this.maskedISBN = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericShelf)).BeginInit();
            this.SuspendLayout();
            // 
            // openPDF
            // 
            this.openPDF.Filter = "Файлы PDF|*.pdf";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(45, 71);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(57, 13);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Название";
            // 
            // labelWhen
            // 
            this.labelWhen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWhen.AutoSize = true;
            this.labelWhen.Location = new System.Drawing.Point(45, 198);
            this.labelWhen.Name = "labelWhen";
            this.labelWhen.Size = new System.Drawing.Size(70, 13);
            this.labelWhen.TabIndex = 9;
            this.labelWhen.Text = "Год издания";
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(45, 117);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(57, 13);
            this.labelAuthor.TabIndex = 3;
            this.labelAuthor.Text = "Автор (-ы)";
            // 
            // labelPages
            // 
            this.labelPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPages.AutoSize = true;
            this.labelPages.Location = new System.Drawing.Point(45, 224);
            this.labelPages.Name = "labelPages";
            this.labelPages.Size = new System.Drawing.Size(49, 13);
            this.labelPages.TabIndex = 11;
            this.labelPages.Text = "Страниц";
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAuthor.Location = new System.Drawing.Point(156, 114);
            this.textBoxAuthor.Name = "textBoxAuthor";
            this.textBoxAuthor.Options = null;
            this.textBoxAuthor.Size = new System.Drawing.Size(380, 20);
            this.textBoxAuthor.Strings = new string[0];
            this.textBoxAuthor.TabIndex = 4;
            this.textBoxAuthor.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericYear
            // 
            this.numericYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericYear.Location = new System.Drawing.Point(156, 195);
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
            this.numericYear.TabIndex = 10;
            this.numericYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericYear.Value = new decimal(new int[] {
            1990,
            0,
            0,
            0});
            // 
            // numericPages
            // 
            this.numericPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericPages.Location = new System.Drawing.Point(156, 221);
            this.numericPages.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericPages.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericPages.Name = "numericPages";
            this.numericPages.Size = new System.Drawing.Size(75, 20);
            this.numericPages.TabIndex = 12;
            this.numericPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericPages.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.Location = new System.Drawing.Point(416, 471);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(120, 23);
            this.buttonBrowse.TabIndex = 29;
            this.buttonBrowse.Text = "Обзор...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelFile
            // 
            this.labelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(45, 476);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(74, 13);
            this.labelFile.TabIndex = 27;
            this.labelFile.Text = "Путь к файлу";
            // 
            // textBoxFile
            // 
            this.textBoxFile.AllowDrop = true;
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Location = new System.Drawing.Point(156, 473);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(254, 20);
            this.textBoxFile.TabIndex = 28;
            this.textBoxFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxFileName_DragDrop);
            this.textBoxFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBoxFileName_DragEnter);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(461, 525);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 30;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelPublisher
            // 
            this.labelPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPublisher.AutoSize = true;
            this.labelPublisher.Location = new System.Drawing.Point(45, 144);
            this.labelPublisher.Name = "labelPublisher";
            this.labelPublisher.Size = new System.Drawing.Size(56, 13);
            this.labelPublisher.TabIndex = 5;
            this.labelPublisher.Text = "Издатель";
            // 
            // comboBoxPublisher
            // 
            this.comboBoxPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPublisher.DisplayMember = "Name";
            this.comboBoxPublisher.FormattingEnabled = true;
            this.comboBoxPublisher.Location = new System.Drawing.Point(156, 141);
            this.comboBoxPublisher.Name = "comboBoxPublisher";
            this.comboBoxPublisher.Size = new System.Drawing.Size(380, 21);
            this.comboBoxPublisher.TabIndex = 6;
            this.comboBoxPublisher.SelectedIndexChanged += new System.EventHandler(this.comboBoxPublisher_SelectedIndexChanged);
            this.comboBoxPublisher.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelUDK
            // 
            this.labelUDK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUDK.AutoSize = true;
            this.labelUDK.Location = new System.Drawing.Point(45, 302);
            this.labelUDK.Name = "labelUDK";
            this.labelUDK.Size = new System.Drawing.Size(31, 13);
            this.labelUDK.TabIndex = 14;
            this.labelUDK.Text = "УДК";
            // 
            // comboBoxUDK
            // 
            this.comboBoxUDK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxUDK.DisplayMember = "Name";
            this.comboBoxUDK.FormattingEnabled = true;
            this.comboBoxUDK.Location = new System.Drawing.Point(156, 299);
            this.comboBoxUDK.Name = "comboBoxUDK";
            this.comboBoxUDK.Size = new System.Drawing.Size(380, 21);
            this.comboBoxUDK.TabIndex = 15;
            this.comboBoxUDK.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelBBK
            // 
            this.labelBBK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBBK.AutoSize = true;
            this.labelBBK.Location = new System.Drawing.Point(45, 329);
            this.labelBBK.Name = "labelBBK";
            this.labelBBK.Size = new System.Drawing.Size(28, 13);
            this.labelBBK.TabIndex = 16;
            this.labelBBK.Text = "ББК";
            // 
            // comboBoxBBK
            // 
            this.comboBoxBBK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBBK.DisplayMember = "Name";
            this.comboBoxBBK.FormattingEnabled = true;
            this.comboBoxBBK.Location = new System.Drawing.Point(156, 326);
            this.comboBoxBBK.Name = "comboBoxBBK";
            this.comboBoxBBK.Size = new System.Drawing.Size(130, 21);
            this.comboBoxBBK.TabIndex = 17;
            this.comboBoxBBK.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelISBN
            // 
            this.labelISBN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelISBN.AutoSize = true;
            this.labelISBN.Location = new System.Drawing.Point(310, 329);
            this.labelISBN.Name = "labelISBN";
            this.labelISBN.Size = new System.Drawing.Size(32, 13);
            this.labelISBN.TabIndex = 18;
            this.labelISBN.Text = "ISBN";
            // 
            // labelWhere
            // 
            this.labelWhere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelWhere.AutoSize = true;
            this.labelWhere.Location = new System.Drawing.Point(45, 171);
            this.labelWhere.Name = "labelWhere";
            this.labelWhere.Size = new System.Drawing.Size(84, 13);
            this.labelWhere.TabIndex = 7;
            this.labelWhere.Text = "Место издания";
            // 
            // comboBoxWhere
            // 
            this.comboBoxWhere.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWhere.DisplayMember = "Where";
            this.comboBoxWhere.FormattingEnabled = true;
            this.comboBoxWhere.Location = new System.Drawing.Point(156, 168);
            this.comboBoxWhere.Name = "comboBoxWhere";
            this.comboBoxWhere.Size = new System.Drawing.Size(380, 21);
            this.comboBoxWhere.TabIndex = 8;
            this.comboBoxWhere.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.textBoxTitle.Location = new System.Drawing.Point(156, 68);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxTitle.Size = new System.Drawing.Size(380, 40);
            this.textBoxTitle.TabIndex = 2;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // numericQuantity
            // 
            this.numericQuantity.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericQuantity.Location = new System.Drawing.Point(156, 418);
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
            this.numericQuantity.TabIndex = 22;
            this.numericQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelQuantity
            // 
            this.labelQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(45, 420);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(105, 13);
            this.labelQuantity.TabIndex = 21;
            this.labelQuantity.Text = "Количество единиц";
            // 
            // labelRack
            // 
            this.labelRack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelRack.AutoSize = true;
            this.labelRack.Location = new System.Drawing.Point(310, 420);
            this.labelRack.Name = "labelRack";
            this.labelRack.Size = new System.Drawing.Size(51, 13);
            this.labelRack.TabIndex = 23;
            this.labelRack.Text = "Стеллаж";
            // 
            // comboBoxRack
            // 
            this.comboBoxRack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRack.DisplayMember = "Name";
            this.comboBoxRack.FormattingEnabled = true;
            this.comboBoxRack.Location = new System.Drawing.Point(461, 417);
            this.comboBoxRack.Name = "comboBoxRack";
            this.comboBoxRack.Size = new System.Drawing.Size(75, 21);
            this.comboBoxRack.TabIndex = 25;
            this.comboBoxRack.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelShelf
            // 
            this.labelShelf.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelShelf.AutoSize = true;
            this.labelShelf.Location = new System.Drawing.Point(310, 447);
            this.labelShelf.Name = "labelShelf";
            this.labelShelf.Size = new System.Drawing.Size(39, 13);
            this.labelShelf.TabIndex = 24;
            this.labelShelf.Text = "Полка";
            // 
            // labelChapter1
            // 
            this.labelChapter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChapter1.AutoSize = true;
            this.labelChapter1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelChapter1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelChapter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChapter1.Location = new System.Drawing.Point(25, 262);
            this.labelChapter1.Margin = new System.Windows.Forms.Padding(0, 25, 0, 25);
            this.labelChapter1.Name = "labelChapter1";
            this.labelChapter1.Size = new System.Drawing.Size(82, 15);
            this.labelChapter1.TabIndex = 13;
            this.labelChapter1.Text = "Кодификация";
            // 
            // labelChapter2
            // 
            this.labelChapter2.AutoSize = true;
            this.labelChapter2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelChapter2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelChapter2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChapter2.Location = new System.Drawing.Point(25, 25);
            this.labelChapter2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 25);
            this.labelChapter2.Name = "labelChapter2";
            this.labelChapter2.Size = new System.Drawing.Size(138, 15);
            this.labelChapter2.TabIndex = 0;
            this.labelChapter2.Text = "Заглавная информация";
            // 
            // labelChapter3
            // 
            this.labelChapter3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChapter3.AutoSize = true;
            this.labelChapter3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelChapter3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelChapter3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelChapter3.Location = new System.Drawing.Point(25, 375);
            this.labelChapter3.Margin = new System.Windows.Forms.Padding(3, 25, 3, 25);
            this.labelChapter3.Name = "labelChapter3";
            this.labelChapter3.Size = new System.Drawing.Size(146, 15);
            this.labelChapter3.TabIndex = 20;
            this.labelChapter3.Text = "Информация о хранении";
            // 
            // numericShelf
            // 
            this.numericShelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericShelf.Location = new System.Drawing.Point(461, 444);
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
            this.numericShelf.TabIndex = 26;
            this.numericShelf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericShelf.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // maskedISBN
            // 
            this.maskedISBN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedISBN.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.maskedISBN.Location = new System.Drawing.Point(406, 326);
            this.maskedISBN.Mask = "000 0 00 000000 0";
            this.maskedISBN.Name = "maskedISBN";
            this.maskedISBN.Size = new System.Drawing.Size(130, 20);
            this.maskedISBN.TabIndex = 19;
            this.maskedISBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maskedISBN.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // BookEdit
            // 
            this.AcceptButton = this.buttonOK;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 596);
            this.Controls.Add(this.maskedISBN);
            this.Controls.Add(this.numericShelf);
            this.Controls.Add(this.labelChapter3);
            this.Controls.Add(this.labelChapter2);
            this.Controls.Add(this.labelChapter1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.comboBoxRack);
            this.Controls.Add(this.labelShelf);
            this.Controls.Add(this.labelRack);
            this.Controls.Add(this.comboBoxBBK);
            this.Controls.Add(this.labelISBN);
            this.Controls.Add(this.comboBoxUDK);
            this.Controls.Add(this.labelBBK);
            this.Controls.Add(this.comboBoxWhere);
            this.Controls.Add(this.comboBoxPublisher);
            this.Controls.Add(this.labelUDK);
            this.Controls.Add(this.labelWhere);
            this.Controls.Add(this.textBoxAuthor);
            this.Controls.Add(this.labelPublisher);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.labelPages);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelWhen);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.numericQuantity);
            this.Controls.Add(this.numericPages);
            this.Controls.Add(this.numericYear);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 635);
            this.Name = "BookEdit";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование записи книги";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BookEdit_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericShelf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openPDF;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelWhen;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelPages;
        private Mayfly.Controls.MultiStringSelector textBoxAuthor;
        private System.Windows.Forms.NumericUpDown numericYear;
        private System.Windows.Forms.NumericUpDown numericPages;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelPublisher;
        private System.Windows.Forms.ComboBox comboBoxPublisher;
        private System.Windows.Forms.Label labelUDK;
        private System.Windows.Forms.ComboBox comboBoxUDK;
        private System.Windows.Forms.Label labelBBK;
        private System.Windows.Forms.ComboBox comboBoxBBK;
        private System.Windows.Forms.Label labelISBN;
        private System.Windows.Forms.Label labelWhere;
        private System.Windows.Forms.ComboBox comboBoxWhere;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.Label labelRack;
        private System.Windows.Forms.ComboBox comboBoxRack;
        private System.Windows.Forms.Label labelShelf;
        private System.Windows.Forms.Label labelChapter1;
        private System.Windows.Forms.Label labelChapter2;
        private System.Windows.Forms.Label labelChapter3;
        private System.Windows.Forms.NumericUpDown numericShelf;
        private System.Windows.Forms.MaskedTextBox maskedISBN;
    }
}