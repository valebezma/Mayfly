namespace Mayfly.Controls
{
    partial class ColorBox
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
            this.dialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // dialog
            // 
            this.dialog.AnyColor = true;
            this.dialog.FullOpen = true;
            // 
            // ColorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "ColorBox";
            this.Size = new System.Drawing.Size(50, 20);
            this.EnabledChanged += new System.EventHandler(this.ColorBox_EnabledChanged);
            this.Click += new System.EventHandler(this.ColorPicker_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog dialog;
    }
}
