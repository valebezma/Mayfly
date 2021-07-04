namespace Mayfly.Library
{
    partial class ResearchCard
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
            this.buttonRun = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.textBoxExecutive = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxConfirmed = new System.Windows.Forms.TextBox();
            this.textBoxOrg = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSummary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPages = new System.Windows.Forms.TextBox();
            this.textBoxReference = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.textBoxKeywords = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(189, 18);
            this.textBoxTitle.Multiline = true;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(377, 40);
            this.textBoxTitle.TabIndex = 3;
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Location = new System.Drawing.Point(446, 420);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(120, 23);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Открыть отчет";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Файлы PDF|*.pdf";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Тема";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Год выполнения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ответственный исполнитель";
            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(189, 64);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.ReadOnly = true;
            this.textBoxYear.Size = new System.Drawing.Size(75, 20);
            this.textBoxYear.TabIndex = 5;
            this.textBoxYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxExecutive
            // 
            this.textBoxExecutive.Location = new System.Drawing.Point(189, 116);
            this.textBoxExecutive.Name = "textBoxExecutive";
            this.textBoxExecutive.ReadOnly = true;
            this.textBoxExecutive.Size = new System.Drawing.Size(200, 20);
            this.textBoxExecutive.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Утвержден";
            // 
            // textBoxConfirmed
            // 
            this.textBoxConfirmed.Location = new System.Drawing.Point(189, 142);
            this.textBoxConfirmed.Name = "textBoxConfirmed";
            this.textBoxConfirmed.ReadOnly = true;
            this.textBoxConfirmed.Size = new System.Drawing.Size(200, 20);
            this.textBoxConfirmed.TabIndex = 9;
            // 
            // textBoxOrg
            // 
            this.textBoxOrg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOrg.Location = new System.Drawing.Point(189, 90);
            this.textBoxOrg.Name = "textBoxOrg";
            this.textBoxOrg.ReadOnly = true;
            this.textBoxOrg.Size = new System.Drawing.Size(377, 20);
            this.textBoxOrg.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Титульная организация";
            // 
            // textBoxSummary
            // 
            this.textBoxSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSummary.Location = new System.Drawing.Point(189, 244);
            this.textBoxSummary.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.textBoxSummary.Multiline = true;
            this.textBoxSummary.Name = "textBoxSummary";
            this.textBoxSummary.ReadOnly = true;
            this.textBoxSummary.Size = new System.Drawing.Size(377, 92);
            this.textBoxSummary.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Реферат";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Страниц";
            // 
            // textBoxPages
            // 
            this.textBoxPages.Location = new System.Drawing.Point(189, 180);
            this.textBoxPages.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.textBoxPages.Name = "textBoxPages";
            this.textBoxPages.ReadOnly = true;
            this.textBoxPages.Size = new System.Drawing.Size(75, 20);
            this.textBoxPages.TabIndex = 11;
            this.textBoxPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxReference
            // 
            this.textBoxReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReference.Location = new System.Drawing.Point(189, 354);
            this.textBoxReference.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.textBoxReference.Multiline = true;
            this.textBoxReference.Name = "textBoxReference";
            this.textBoxReference.ReadOnly = true;
            this.textBoxReference.Size = new System.Drawing.Size(377, 48);
            this.textBoxReference.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 357);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Ссылка на отчет";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(320, 420);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(120, 23);
            this.buttonCopy.TabIndex = 0;
            this.buttonCopy.Text = "Копировать ссылку";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // textBoxKeywords
            // 
            this.textBoxKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeywords.Location = new System.Drawing.Point(189, 206);
            this.textBoxKeywords.Name = "textBoxKeywords";
            this.textBoxKeywords.ReadOnly = true;
            this.textBoxKeywords.Size = new System.Drawing.Size(377, 20);
            this.textBoxKeywords.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Ключевые слова";
            // 
            // Card
            // 
            this.AcceptButton = this.buttonRun;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.textBoxConfirmed);
            this.Controls.Add(this.textBoxExecutive);
            this.Controls.Add(this.textBoxPages);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxReference);
            this.Controls.Add(this.textBoxKeywords);
            this.Controls.Add(this.textBoxOrg);
            this.Controls.Add(this.textBoxSummary);
            this.Controls.Add(this.textBoxTitle);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "Card";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карточка НИР";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.TextBox textBoxExecutive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxConfirmed;
        private System.Windows.Forms.TextBox textBoxOrg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSummary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPages;
        private System.Windows.Forms.TextBox textBoxReference;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.TextBox textBoxKeywords;
        private System.Windows.Forms.Label label9;
    }
}