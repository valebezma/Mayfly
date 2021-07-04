namespace Mayfly.Wild.Controls
{
    partial class StratifiedSample
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
            this.Terminal = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.Terminal.SuspendLayout();
            this.SuspendLayout();
            // 
            // Terminal
            // 
            this.Terminal.Controls.Add(this.label1);
            this.Terminal.Location = new System.Drawing.Point(15, 0);
            this.Terminal.Name = "Terminal";
            this.Terminal.Size = new System.Drawing.Size(50, 35);
            this.Terminal.TabIndex = 2;
            this.Terminal.Click += new System.EventHandler(this.Terminal_Click);
            this.Terminal.Paint += new System.Windows.Forms.PaintEventHandler(this.Terminal_Paint);
            this.Terminal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Terminal_MouseDown);
            this.Terminal.MouseEnter += new System.EventHandler(this.Terminal_MouseEnter);
            this.Terminal.MouseLeave += new System.EventHandler(this.Terminal_MouseLeave);
            this.Terminal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Terminal_MouseUp);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLeft
            // 
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(15, 35);
            this.panelLeft.TabIndex = 3;
            this.panelLeft.Click += new System.EventHandler(this.panelLeft_Click);
            this.panelLeft.MouseEnter += new System.EventHandler(this.panelLeft_MouseEnter);
            this.panelLeft.MouseLeave += new System.EventHandler(this.panelLeft_MouseLeave);
            // 
            // StratifiedSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.Terminal);
            this.DoubleBuffered = true;
            this.Name = "StratifiedSample";
            this.Size = new System.Drawing.Size(189, 52);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.controlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.controlRemoved);
            this.Terminal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Terminal;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label label1;








    }
}
