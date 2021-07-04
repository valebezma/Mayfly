namespace Mayfly.Species.Controls
{
    partial class State
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(State));
            this.labelState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelState
            // 
            this.labelState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelState.BackColor = System.Drawing.Color.Transparent;
            this.labelState.Font = new System.Drawing.Font("Segoe UI Semilight", 8F);
            this.labelState.Location = new System.Drawing.Point(18, 212);
            this.labelState.Margin = new System.Windows.Forms.Padding(3);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(223, 65);
            this.labelState.TabIndex = 0;
            this.labelState.Text = "State description";
            this.labelState.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.labelState.Click += new System.EventHandler(this.labelState_Click);
            this.labelState.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelState_MouseDown);
            this.labelState.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelState_MouseUp);
            // 
            // State
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.labelState);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "State";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(259, 295);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelState;
    }
}
