namespace Mayfly.Species.Systematics
{
    partial class StateEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateEditor));
            this.contextBond = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemTaxon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAllTaxa = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.newSpeciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAllSpecies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadNewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewFeatureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextGoto = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemNewFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelSpecies = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonBond = new System.Windows.Forms.Button();
            this.labelImage = new System.Windows.Forms.Label();
            this.pictureBoxPic = new System.Windows.Forms.PictureBox();
            this.contextBond.SuspendLayout();
            this.contextGoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPic)).BeginInit();
            this.SuspendLayout();
            // 
            // contextBond
            // 
            this.contextBond.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTaxon,
            this.menuItemSpecies,
            this.pictureToolStripMenuItem,
            this.nextStepToolStripMenuItem});
            this.contextBond.Name = "contextBond";
            this.contextBond.Size = new System.Drawing.Size(124, 92);
            // 
            // menuItemTaxon
            // 
            this.menuItemTaxon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAllTaxa,
            this.toolStripSeparator1});
            this.menuItemTaxon.Name = "menuItemTaxon";
            this.menuItemTaxon.Size = new System.Drawing.Size(123, 22);
            this.menuItemTaxon.Text = "Taxon";
            // 
            // menuItemAllTaxa
            // 
            this.menuItemAllTaxa.Name = "menuItemAllTaxa";
            this.menuItemAllTaxa.Size = new System.Drawing.Size(112, 22);
            this.menuItemAllTaxa.Text = "All taxa";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // menuItemSpecies
            // 
            this.menuItemSpecies.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSpeciesToolStripMenuItem,
            this.menuItemAllSpecies,
            this.toolStripSeparator2});
            this.menuItemSpecies.Name = "menuItemSpecies";
            this.menuItemSpecies.Size = new System.Drawing.Size(123, 22);
            this.menuItemSpecies.Text = "Species";
            // 
            // newSpeciesToolStripMenuItem
            // 
            this.newSpeciesToolStripMenuItem.Name = "newSpeciesToolStripMenuItem";
            this.newSpeciesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newSpeciesToolStripMenuItem.Text = "New species...";
            // 
            // menuItemAllSpecies
            // 
            this.menuItemAllSpecies.Name = "menuItemAllSpecies";
            this.menuItemAllSpecies.Size = new System.Drawing.Size(148, 22);
            this.menuItemAllSpecies.Text = "All species";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // pictureToolStripMenuItem
            // 
            this.pictureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNewToolStripMenuItem1,
            this.toolStripSeparator5,
            this.toolStripSeparator6,
            this.clearAllToolStripMenuItem});
            this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
            this.pictureToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.pictureToolStripMenuItem.Text = "Picture";
            // 
            // loadNewToolStripMenuItem1
            // 
            this.loadNewToolStripMenuItem1.Name = "loadNewToolStripMenuItem1";
            this.loadNewToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.loadNewToolStripMenuItem1.Text = "Load new...";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(131, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(131, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.clearAllToolStripMenuItem.Text = "Clear all";
            // 
            // nextStepToolStripMenuItem
            // 
            this.nextStepToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewFeatureToolStripMenuItem});
            this.nextStepToolStripMenuItem.Name = "nextStepToolStripMenuItem";
            this.nextStepToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.nextStepToolStripMenuItem.Text = "Next step";
            // 
            // addNewFeatureToolStripMenuItem
            // 
            this.addNewFeatureToolStripMenuItem.Name = "addNewFeatureToolStripMenuItem";
            this.addNewFeatureToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addNewFeatureToolStripMenuItem.Text = "New feature...";
            // 
            // contextGoto
            // 
            this.contextGoto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewFeature,
            this.toolStripSeparator3});
            this.contextGoto.Name = "contextGoto";
            this.contextGoto.Size = new System.Drawing.Size(148, 32);
            // 
            // menuItemNewFeature
            // 
            this.menuItemNewFeature.Name = "menuItemNewFeature";
            this.menuItemNewFeature.Size = new System.Drawing.Size(147, 22);
            this.menuItemNewFeature.Text = "New feature...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(144, 6);
            // 
            // labelSpecies
            // 
            this.labelSpecies.AutoSize = true;
            this.labelSpecies.BackColor = System.Drawing.Color.Transparent;
            this.labelSpecies.Location = new System.Drawing.Point(88, 259);
            this.labelSpecies.Name = "labelSpecies";
            this.labelSpecies.Size = new System.Drawing.Size(53, 13);
            this.labelSpecies.TabIndex = 10;
            this.labelSpecies.Text = "{Species}";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDescription.Location = new System.Drawing.Point(18, 18);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(223, 65);
            this.textBoxDescription.TabIndex = 4;
            this.textBoxDescription.SizeChanged += new System.EventHandler(this.textBoxDescription_SizeChanged);
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxState_TextChanged);
            this.textBoxDescription.Enter += new System.EventHandler(this.textBoxState_Enter);
            this.textBoxDescription.Leave += new System.EventHandler(this.textBoxState_Leave);
            // 
            // buttonBond
            // 
            this.buttonBond.Location = new System.Drawing.Point(18, 254);
            this.buttonBond.Name = "buttonBond";
            this.buttonBond.Size = new System.Drawing.Size(60, 23);
            this.buttonBond.TabIndex = 7;
            this.buttonBond.TabStop = false;
            this.buttonBond.Text = "Bond";
            this.buttonBond.UseVisualStyleBackColor = true;
            this.buttonBond.Visible = false;
            this.buttonBond.Click += new System.EventHandler(this.buttonBond_Click);
            // 
            // labelImage
            // 
            this.labelImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelImage.BackColor = System.Drawing.Color.Transparent;
            this.labelImage.Font = new System.Drawing.Font("Segoe UI Light", 15.75F);
            this.labelImage.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelImage.Location = new System.Drawing.Point(18, 138);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(223, 60);
            this.labelImage.TabIndex = 10;
            this.labelImage.Text = "NO IMAGES\r\nATTACHED";
            this.labelImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxPic
            // 
            this.pictureBoxPic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPic.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPic.Location = new System.Drawing.Point(18, 89);
            this.pictureBoxPic.Name = "pictureBoxPic";
            this.pictureBoxPic.Size = new System.Drawing.Size(223, 158);
            this.pictureBoxPic.TabIndex = 8;
            this.pictureBoxPic.TabStop = false;
            // 
            // StateEditor
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.labelImage);
            this.Controls.Add(this.buttonBond);
            this.Controls.Add(this.pictureBoxPic);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelSpecies);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "StateEditor";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(259, 295);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StateEditor_MouseClick);
            this.contextBond.ResumeLayout(false);
            this.contextGoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextBond;
        private System.Windows.Forms.ToolStripMenuItem menuItemTaxon;
        private System.Windows.Forms.ToolStripMenuItem menuItemAllTaxa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemSpecies;
        private System.Windows.Forms.ToolStripMenuItem menuItemAllSpecies;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ContextMenuStrip contextGoto;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewFeature;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem newSpeciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadNewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewFeatureToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelSpecies;
        private System.Windows.Forms.PictureBox pictureBoxPic;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonBond;
        private System.Windows.Forms.Label labelImage;
    }
}
