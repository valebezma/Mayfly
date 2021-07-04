namespace Mayfly.Species.Controls
{
    partial class Feature
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.flowStates = new System.Windows.Forms.FlowLayoutPanel();
            this.timerCollapse = new System.Windows.Forms.Timer(this.components);
            this.timerExpand = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTitle.Location = new System.Drawing.Point(13, 10);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(573, 25);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Feature title";
            this.labelTitle.Click += new System.EventHandler(this.labelTitle_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDescription.Font = new System.Drawing.Font("Segoe UI Light", 8F);
            this.labelDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelDescription.Location = new System.Drawing.Point(13, 45);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(573, 26);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Feature description";
            // 
            // flowStates
            // 
            this.flowStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowStates.AutoScroll = true;
            this.flowStates.Location = new System.Drawing.Point(13, 84);
            this.flowStates.Name = "flowStates";
            this.flowStates.Size = new System.Drawing.Size(574, 316);
            this.flowStates.TabIndex = 7;
            this.flowStates.WrapContents = false;
            // 
            // timerCollapse
            // 
            this.timerCollapse.Interval = 1;
            this.timerCollapse.Tick += new System.EventHandler(this.timerCollapse_Tick);
            // 
            // timerExpand
            // 
            this.timerExpand.Interval = 1;
            this.timerExpand.Tick += new System.EventHandler(this.timerExpand_Tick);
            // 
            // Feature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowStates);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelTitle);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.Name = "Feature";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(602, 415);
            this.SizeChanged += new System.EventHandler(this.Feature_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.FlowLayoutPanel flowStates;
        private System.Windows.Forms.Timer timerCollapse;
        private System.Windows.Forms.Timer timerExpand;
    }
}
