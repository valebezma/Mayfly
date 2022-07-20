namespace Mayfly.Wild.Controls
{
    partial class SettingsControlExplorer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxConnectance = new System.Windows.Forms.ComboBox();
            this.comboBoxSimilarity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSimilarity = new System.Windows.Forms.Label();
            this.comboBoxDominance = new System.Windows.Forms.ComboBox();
            this.labelDominance = new System.Windows.Forms.Label();
            this.comboBoxDiversity = new System.Windows.Forms.ComboBox();
            this.labelDiversity = new System.Windows.Forms.Label();
            this.labelIndices = new System.Windows.Forms.Label();
            this.comboBoxReportCriticality = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxConsistency = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepWizards = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxConnectance
            // 
            this.comboBoxConnectance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxConnectance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnectance.FormattingEnabled = true;
            this.comboBoxConnectance.Items.AddRange(new object[] {
            "Czekanowski, 1911",
            "Shorygin, 1939",
            "Weinstein, 1976"});
            this.comboBoxConnectance.Location = new System.Drawing.Point(183, 146);
            this.comboBoxConnectance.Name = "comboBoxConnectance";
            this.comboBoxConnectance.Size = new System.Drawing.Size(175, 21);
            this.comboBoxConnectance.TabIndex = 8;
            // 
            // comboBoxSimilarity
            // 
            this.comboBoxSimilarity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSimilarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSimilarity.FormattingEnabled = true;
            this.comboBoxSimilarity.Items.AddRange(new object[] {
            "Czekanowski, 1900; Sørensen, 1948",
            "Jaccard, 1901",
            "Szymkiewicz, 1926; Simpson1943",
            "Kulczynski, 1927A",
            "Kulczynski, 1927B",
            "Braun-Blanquet, 1932",
            "Ochiai, 1957; Barkman, 1958",
            "Sokal, Sneath, 1963"});
            this.comboBoxSimilarity.Location = new System.Drawing.Point(183, 119);
            this.comboBoxSimilarity.Name = "comboBoxSimilarity";
            this.comboBoxSimilarity.Size = new System.Drawing.Size(175, 21);
            this.comboBoxSimilarity.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(48, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Connectance index";
            // 
            // labelSimilarity
            // 
            this.labelSimilarity.AutoSize = true;
            this.labelSimilarity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelSimilarity.Location = new System.Drawing.Point(48, 122);
            this.labelSimilarity.Name = "labelSimilarity";
            this.labelSimilarity.Size = new System.Drawing.Size(75, 13);
            this.labelSimilarity.TabIndex = 5;
            this.labelSimilarity.Text = "Similarity index";
            // 
            // comboBoxDominance
            // 
            this.comboBoxDominance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDominance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDominance.FormattingEnabled = true;
            this.comboBoxDominance.Items.AddRange(new object[] {
            "De Vries M., 1937",
            "Зенкевич Л. А., Броцкая В. А., 1937",
            "Броцкая В. А., Зенкевич Л. А., 1939",
            "Мордухай-Болтовской Ф. Д., 1940 (modification of Броцкая, Зенкевич, 1939)",
            "Арнольди Л. В., 1941",
            "Мордухай-Болтовской Ф. Д., 1948 (modification of Броцкая, Зенкевич, 1939)",
            "Balogh J., 1958",
            "Sanders H. L., 1960",
            "Sanders H. L., 1960 (in scale of 1)",
            "Петров К. М., 1961",
            "Kownacki A., 1971",
            "Kownacki A., 1971 by biomass",
            "Мордухай-Болтовской Ф. Д., 1975",
            "Иоганзен Б. Г., Файзова Л. В., 1978",
            "Дедю И. И., 1990",
            "Щербина Г. Х., 1993",
            "Шитиков В. К. и др., 2003"});
            this.comboBoxDominance.Location = new System.Drawing.Point(183, 92);
            this.comboBoxDominance.Name = "comboBoxDominance";
            this.comboBoxDominance.Size = new System.Drawing.Size(175, 21);
            this.comboBoxDominance.TabIndex = 4;
            // 
            // labelDominance
            // 
            this.labelDominance.AutoSize = true;
            this.labelDominance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDominance.Location = new System.Drawing.Point(48, 95);
            this.labelDominance.Name = "labelDominance";
            this.labelDominance.Size = new System.Drawing.Size(89, 13);
            this.labelDominance.TabIndex = 3;
            this.labelDominance.Text = "Dominance index";
            // 
            // comboBoxDiversity
            // 
            this.comboBoxDiversity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDiversity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiversity.FormattingEnabled = true;
            this.comboBoxDiversity.Location = new System.Drawing.Point(183, 65);
            this.comboBoxDiversity.Name = "comboBoxDiversity";
            this.comboBoxDiversity.Size = new System.Drawing.Size(175, 21);
            this.comboBoxDiversity.TabIndex = 2;
            // 
            // labelDiversity
            // 
            this.labelDiversity.AutoSize = true;
            this.labelDiversity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDiversity.Location = new System.Drawing.Point(48, 68);
            this.labelDiversity.Name = "labelDiversity";
            this.labelDiversity.Size = new System.Drawing.Size(75, 13);
            this.labelDiversity.TabIndex = 1;
            this.labelDiversity.Text = "Diversity index";
            // 
            // labelIndices
            // 
            this.labelIndices.AutoSize = true;
            this.labelIndices.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelIndices.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelIndices.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelIndices.Location = new System.Drawing.Point(28, 28);
            this.labelIndices.Margin = new System.Windows.Forms.Padding(3, 3, 3, 25);
            this.labelIndices.Name = "labelIndices";
            this.labelIndices.Size = new System.Drawing.Size(44, 15);
            this.labelIndices.TabIndex = 0;
            this.labelIndices.Text = "Indices";
            // 
            // comboBoxReportCriticality
            // 
            this.comboBoxReportCriticality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxReportCriticality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReportCriticality.FormattingEnabled = true;
            this.comboBoxReportCriticality.Items.AddRange(new object[] {
            "All",
            "Allowed or worse",
            "Bad or worse",
            "Critical only"});
            this.comboBoxReportCriticality.Location = new System.Drawing.Point(183, 275);
            this.comboBoxReportCriticality.Name = "comboBoxReportCriticality";
            this.comboBoxReportCriticality.Size = new System.Drawing.Size(175, 21);
            this.comboBoxReportCriticality.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(48, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Report of data artifacts";
            // 
            // checkBoxConsistency
            // 
            this.checkBoxConsistency.AutoSize = true;
            this.checkBoxConsistency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxConsistency.Location = new System.Drawing.Point(48, 250);
            this.checkBoxConsistency.Name = "checkBoxConsistency";
            this.checkBoxConsistency.Size = new System.Drawing.Size(140, 17);
            this.checkBoxConsistency.TabIndex = 15;
            this.checkBoxConsistency.Text = "Check data consistency";
            this.checkBoxConsistency.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeepWizards
            // 
            this.checkBoxKeepWizards.AutoSize = true;
            this.checkBoxKeepWizards.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxKeepWizards.Location = new System.Drawing.Point(48, 227);
            this.checkBoxKeepWizards.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.checkBoxKeepWizards.Name = "checkBoxKeepWizards";
            this.checkBoxKeepWizards.Size = new System.Drawing.Size(214, 17);
            this.checkBoxKeepWizards.TabIndex = 16;
            this.checkBoxKeepWizards.Text = "Keep wizard opened after report is done";
            this.checkBoxKeepWizards.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(28, 187);
            this.label5.Margin = new System.Windows.Forms.Padding(25, 25, 3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Advanced options";
            // 
            // SettingsControlExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxReportCriticality);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxConsistency);
            this.Controls.Add(this.checkBoxKeepWizards);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxConnectance);
            this.Controls.Add(this.comboBoxSimilarity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSimilarity);
            this.Controls.Add(this.comboBoxDominance);
            this.Controls.Add(this.labelDominance);
            this.Controls.Add(this.comboBoxDiversity);
            this.Controls.Add(this.labelDiversity);
            this.Controls.Add(this.labelIndices);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "SettingsControlExplorer";
            this.Padding = new System.Windows.Forms.Padding(25, 25, 45, 45);
            this.Size = new System.Drawing.Size(400, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxConnectance;
        private System.Windows.Forms.ComboBox comboBoxSimilarity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSimilarity;
        private System.Windows.Forms.ComboBox comboBoxDominance;
        private System.Windows.Forms.Label labelDominance;
        private System.Windows.Forms.ComboBox comboBoxDiversity;
        private System.Windows.Forms.Label labelDiversity;
        private System.Windows.Forms.Label labelIndices;
        private System.Windows.Forms.ComboBox comboBoxReportCriticality;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxConsistency;
        private System.Windows.Forms.CheckBox checkBoxKeepWizards;
        private System.Windows.Forms.Label label5;
    }
}
