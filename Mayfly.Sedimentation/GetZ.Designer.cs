namespace Mayfly.Sedimentation
{
    partial class GetZ
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
            this.numericUpDownFi = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBoxObstruction = new System.Windows.Forms.TextBox();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelVelocity = new System.Windows.Forms.Label();
            this.textBoxVelocity = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxTurbulentWidth = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFi)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownFi
            // 
            this.numericUpDownFi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFi.Location = new System.Drawing.Point(242, 132);
            this.numericUpDownFi.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDownFi.Name = "numericUpDownFi";
            this.numericUpDownFi.Size = new System.Drawing.Size(75, 20);
            this.numericUpDownFi.TabIndex = 9;
            this.numericUpDownFi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFi.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownFi.ValueChanged += new System.EventHandler(this.value_Changed);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(28, 134);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "Угол отвала, °";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label36.Location = new System.Drawing.Point(28, 82);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(166, 13);
            this.label36.TabIndex = 4;
            this.label36.Text = "Ширина взмученного потока, м";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(28, 108);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(137, 13);
            this.label33.TabIndex = 6;
            this.label33.Text = "Степень стеснения русла";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label34.Location = new System.Drawing.Point(28, 177);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(159, 13);
            this.label34.TabIndex = 10;
            this.label34.Text = "Коэффициент уноса грунта, %";
            // 
            // textBoxObstruction
            // 
            this.textBoxObstruction.AllowDrop = true;
            this.textBoxObstruction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxObstruction.Location = new System.Drawing.Point(242, 106);
            this.textBoxObstruction.Name = "textBoxObstruction";
            this.textBoxObstruction.ReadOnly = true;
            this.textBoxObstruction.Size = new System.Drawing.Size(75, 20);
            this.textBoxObstruction.TabIndex = 7;
            this.textBoxObstruction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxZ
            // 
            this.textBoxZ.AllowDrop = true;
            this.textBoxZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxZ.Location = new System.Drawing.Point(242, 175);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.ReadOnly = true;
            this.textBoxZ.Size = new System.Drawing.Size(75, 20);
            this.textBoxZ.TabIndex = 11;
            this.textBoxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(194, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Использовать";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // labelVelocity
            // 
            this.labelVelocity.AutoSize = true;
            this.labelVelocity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelVelocity.Location = new System.Drawing.Point(28, 31);
            this.labelVelocity.Name = "labelVelocity";
            this.labelVelocity.Size = new System.Drawing.Size(168, 13);
            this.labelVelocity.TabIndex = 0;
            this.labelVelocity.Text = "Средняя скорость течения, м/с";
            // 
            // textBoxVelocity
            // 
            this.textBoxVelocity.AllowDrop = true;
            this.textBoxVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxVelocity.Location = new System.Drawing.Point(242, 28);
            this.textBoxVelocity.Name = "textBoxVelocity";
            this.textBoxVelocity.ReadOnly = true;
            this.textBoxVelocity.Size = new System.Drawing.Size(75, 20);
            this.textBoxVelocity.TabIndex = 1;
            this.textBoxVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxVelocity.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelWidth.Location = new System.Drawing.Point(28, 57);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(92, 13);
            this.labelWidth.TabIndex = 2;
            this.labelWidth.Text = "Ширина русла, м";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.AllowDrop = true;
            this.textBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWidth.Location = new System.Drawing.Point(242, 54);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.ReadOnly = true;
            this.textBoxWidth.Size = new System.Drawing.Size(75, 20);
            this.textBoxWidth.TabIndex = 3;
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // textBoxTurbulentWidth
            // 
            this.textBoxTurbulentWidth.AllowDrop = true;
            this.textBoxTurbulentWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTurbulentWidth.Location = new System.Drawing.Point(242, 79);
            this.textBoxTurbulentWidth.Name = "textBoxTurbulentWidth";
            this.textBoxTurbulentWidth.Size = new System.Drawing.Size(75, 20);
            this.textBoxTurbulentWidth.TabIndex = 5;
            this.textBoxTurbulentWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTurbulentWidth.TextChanged += new System.EventHandler(this.value_Changed);
            // 
            // GetZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(345, 270);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.textBoxTurbulentWidth);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.labelVelocity);
            this.Controls.Add(this.textBoxVelocity);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numericUpDownFi);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.textBoxObstruction);
            this.Controls.Add(this.textBoxZ);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetZ";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Коэффициент уноса грунта";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownFi;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBoxObstruction;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelVelocity;
        private System.Windows.Forms.TextBox textBoxVelocity;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxTurbulentWidth;
    }
}