namespace Mayfly.Controls
{
    partial class FilterUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterUnit));
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.comboBoxFilterable = new System.Windows.Forms.ComboBox();
            this.contextTerm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxNot = new System.Windows.Forms.CheckBox();
            this.labelIs = new System.Windows.Forms.Label();
            this.contextTerm.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTo
            // 
            resources.ApplyResources(this.labelTo, "labelTo");
            this.labelTo.Name = "labelTo";
            // 
            // labelFrom
            // 
            resources.ApplyResources(this.labelFrom, "labelFrom");
            this.labelFrom.Name = "labelFrom";
            // 
            // textBoxTo
            // 
            resources.ApplyResources(this.textBoxTo, "textBoxTo");
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.TextChanged += new System.EventHandler(this.textBoxTo_TextChanged);
            this.textBoxTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_KeyDown);
            this.textBoxTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            this.textBoxTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_KeyUp);
            this.textBoxTo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.input_PreviewKeyDown);
            // 
            // textBoxFrom
            // 
            resources.ApplyResources(this.textBoxFrom, "textBoxFrom");
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.TextChanged += new System.EventHandler(this.textBoxFrom_TextChanged);
            this.textBoxFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // comboBoxCategory
            // 
            resources.ApplyResources(this.comboBoxCategory, "comboBoxCategory");
            this.comboBoxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCategory.DisplayMember = "HeaderText";
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            this.comboBoxCategory.TextChanged += new System.EventHandler(this.comboBoxCategory_TextChanged);
            this.comboBoxCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.input_KeyDown);
            this.comboBoxCategory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            this.comboBoxCategory.KeyUp += new System.Windows.Forms.KeyEventHandler(this.input_KeyUp);
            this.comboBoxCategory.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.input_PreviewKeyDown);
            // 
            // comboBoxFilterable
            // 
            resources.ApplyResources(this.comboBoxFilterable, "comboBoxFilterable");
            this.comboBoxFilterable.ContextMenuStrip = this.contextTerm;
            this.comboBoxFilterable.DisplayMember = "HeaderText";
            this.comboBoxFilterable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterable.FormattingEnabled = true;
            this.comboBoxFilterable.Name = "comboBoxFilterable";
            this.comboBoxFilterable.TabStop = false;
            this.comboBoxFilterable.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterable_SelectedIndexChanged);
            this.comboBoxFilterable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // contextTerm
            // 
            this.contextTerm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextRemove,
            this.contextRepeat});
            this.contextTerm.Name = "contextMenuStripTerm";
            resources.ApplyResources(this.contextTerm, "contextTerm");
            // 
            // contextRemove
            // 
            this.contextRemove.Name = "contextRemove";
            resources.ApplyResources(this.contextRemove, "contextRemove");
            this.contextRemove.Click += new System.EventHandler(this.contextRemove_Click);
            // 
            // contextRepeat
            // 
            this.contextRepeat.Name = "contextRepeat";
            resources.ApplyResources(this.contextRepeat, "contextRepeat");
            this.contextRepeat.Click += new System.EventHandler(this.contextRepeat_Click);
            // 
            // checkBoxNot
            // 
            resources.ApplyResources(this.checkBoxNot, "checkBoxNot");
            this.checkBoxNot.Name = "checkBoxNot";
            this.checkBoxNot.TabStop = false;
            this.checkBoxNot.UseVisualStyleBackColor = true;
            this.checkBoxNot.CheckedChanged += new System.EventHandler(this.valueChanged);
            // 
            // labelIs
            // 
            resources.ApplyResources(this.labelIs, "labelIs");
            this.labelIs.Name = "labelIs";
            // 
            // FilterUnit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelIs);
            this.Controls.Add(this.checkBoxNot);
            this.Controls.Add(this.comboBoxFilterable);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.comboBoxCategory);
            this.Name = "FilterUnit";
            this.contextTerm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelTo;
        public System.Windows.Forms.Label labelFrom;
        public System.Windows.Forms.TextBox textBoxTo;
        public System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.ComboBox comboBoxFilterable;
        private System.Windows.Forms.CheckBox checkBoxNot;
        private System.Windows.Forms.Label labelIs;
        public System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.ContextMenuStrip contextTerm;
        private System.Windows.Forms.ToolStripMenuItem contextRemove;
        private System.Windows.Forms.ToolStripMenuItem contextRepeat;
    }
}
