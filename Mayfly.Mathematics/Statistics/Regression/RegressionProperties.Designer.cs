namespace Mayfly.Mathematics.Statistics
{
    partial class RegressionProperties
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegressionProperties));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.webBrowserEquation = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.numXp = new Mayfly.Controls.NumberBox();
            this.numYp = new Mayfly.Controls.NumberBox();
            this.numY = new Mayfly.Controls.NumberBox();
            this.numX = new Mayfly.Controls.NumberBox();
            this.labelR2 = new System.Windows.Forms.Label();
            this.domainParameters = new System.Windows.Forms.DomainUpDown();
            this.labelValues = new System.Windows.Forms.Label();
            this.labelDC = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.labelF = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSSY = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSSR = new System.Windows.Forms.Label();
            this.labelSE = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.textBoxDC = new System.Windows.Forms.TextBox();
            this.textBoxF = new System.Windows.Forms.TextBox();
            this.textBoxSSY = new System.Windows.Forms.TextBox();
            this.textBoxSSR = new System.Windows.Forms.TextBox();
            this.textBoxSE = new System.Windows.Forms.TextBox();
            this.buttonY = new System.Windows.Forms.Button();
            this.pictureBoxParameter = new System.Windows.Forms.PictureBox();
            this.pictureBoxF = new System.Windows.Forms.PictureBox();
            this.buttonX = new System.Windows.Forms.Button();
            this.labelN = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.labelEquation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDescriptive = new System.Windows.Forms.Label();
            this.labelStats = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxN = new System.Windows.Forms.TextBox();
            this.labelParameters = new System.Windows.Forms.Label();
            this.tabPageDiagnostic = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxParameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxF)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowserEquation
            // 
            this.webBrowserEquation.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.webBrowserEquation, "webBrowserEquation");
            this.webBrowserEquation.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserEquation.Name = "webBrowserEquation";
            this.webBrowserEquation.ScriptErrorsSuppressed = true;
            this.toolTip1.SetToolTip(this.webBrowserEquation, resources.GetString("webBrowserEquation.ToolTip"));
            this.webBrowserEquation.WebBrowserShortcutsEnabled = false;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageDiagnostic);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.numXp);
            this.tabPageGeneral.Controls.Add(this.numYp);
            this.tabPageGeneral.Controls.Add(this.numY);
            this.tabPageGeneral.Controls.Add(this.numX);
            this.tabPageGeneral.Controls.Add(this.labelR2);
            this.tabPageGeneral.Controls.Add(this.domainParameters);
            this.tabPageGeneral.Controls.Add(this.labelValues);
            this.tabPageGeneral.Controls.Add(this.labelDC);
            this.tabPageGeneral.Controls.Add(this.buttonReport);
            this.tabPageGeneral.Controls.Add(this.webBrowserEquation);
            this.tabPageGeneral.Controls.Add(this.labelF);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.labelSSY);
            this.tabPageGeneral.Controls.Add(this.label2);
            this.tabPageGeneral.Controls.Add(this.labelSSR);
            this.tabPageGeneral.Controls.Add(this.labelSE);
            this.tabPageGeneral.Controls.Add(this.comboBoxType);
            this.tabPageGeneral.Controls.Add(this.textBoxDC);
            this.tabPageGeneral.Controls.Add(this.textBoxF);
            this.tabPageGeneral.Controls.Add(this.textBoxSSY);
            this.tabPageGeneral.Controls.Add(this.textBoxSSR);
            this.tabPageGeneral.Controls.Add(this.textBoxSE);
            this.tabPageGeneral.Controls.Add(this.buttonY);
            this.tabPageGeneral.Controls.Add(this.pictureBoxParameter);
            this.tabPageGeneral.Controls.Add(this.pictureBoxF);
            this.tabPageGeneral.Controls.Add(this.buttonX);
            this.tabPageGeneral.Controls.Add(this.labelN);
            this.tabPageGeneral.Controls.Add(this.buttonCopy);
            this.tabPageGeneral.Controls.Add(this.labelEquation);
            this.tabPageGeneral.Controls.Add(this.label1);
            this.tabPageGeneral.Controls.Add(this.labelDescriptive);
            this.tabPageGeneral.Controls.Add(this.labelStats);
            this.tabPageGeneral.Controls.Add(this.labelType);
            this.tabPageGeneral.Controls.Add(this.textBoxN);
            this.tabPageGeneral.Controls.Add(this.labelParameters);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // numXp
            // 
            resources.ApplyResources(this.numXp, "numXp");
            this.numXp.Name = "numXp";
            this.numXp.ReadOnly = true;
            this.numXp.Value = double.NaN;
            // 
            // numYp
            // 
            resources.ApplyResources(this.numYp, "numYp");
            this.numYp.Name = "numYp";
            this.numYp.ReadOnly = true;
            this.numYp.Value = double.NaN;
            // 
            // numY
            // 
            resources.ApplyResources(this.numY, "numY");
            this.numY.Name = "numY";
            this.numY.Value = double.NaN;
            this.numY.TextChanged += new System.EventHandler(this.numY_TextChanged);
            // 
            // numX
            // 
            resources.ApplyResources(this.numX, "numX");
            this.numX.Name = "numX";
            this.numX.Value = double.NaN;
            this.numX.TextChanged += new System.EventHandler(this.numX_TextChanged);
            // 
            // labelR2
            // 
            resources.ApplyResources(this.labelR2, "labelR2");
            this.labelR2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelR2.Name = "labelR2";
            // 
            // domainParameters
            // 
            resources.ApplyResources(this.domainParameters, "domainParameters");
            this.domainParameters.Name = "domainParameters";
            this.domainParameters.SelectedItemChanged += new System.EventHandler(this.domainParameters_SelectedItemChanged);
            // 
            // labelValues
            // 
            resources.ApplyResources(this.labelValues, "labelValues");
            this.labelValues.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelValues.Name = "labelValues";
            // 
            // labelDC
            // 
            resources.ApplyResources(this.labelDC, "labelDC");
            this.labelDC.Name = "labelDC";
            // 
            // buttonReport
            // 
            resources.ApplyResources(this.buttonReport, "buttonReport");
            this.buttonReport.FlatAppearance.BorderSize = 0;
            this.buttonReport.Image = global::Mayfly.Pictogram.Report;
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // labelF
            // 
            resources.ApplyResources(this.labelF, "labelF");
            this.labelF.Name = "labelF";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labelSSY
            // 
            resources.ApplyResources(this.labelSSY, "labelSSY");
            this.labelSSY.Name = "labelSSY";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelSSR
            // 
            resources.ApplyResources(this.labelSSR, "labelSSR");
            this.labelSSR.Name = "labelSSR";
            // 
            // labelSE
            // 
            resources.ApplyResources(this.labelSE, "labelSE");
            this.labelSE.Name = "labelSE";
            // 
            // comboBoxType
            // 
            resources.ApplyResources(this.comboBoxType, "comboBoxType");
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxTrend_SelectedIndexChanged);
            // 
            // textBoxDC
            // 
            resources.ApplyResources(this.textBoxDC, "textBoxDC");
            this.textBoxDC.Name = "textBoxDC";
            this.textBoxDC.ReadOnly = true;
            // 
            // textBoxF
            // 
            resources.ApplyResources(this.textBoxF, "textBoxF");
            this.textBoxF.Name = "textBoxF";
            this.textBoxF.ReadOnly = true;
            // 
            // textBoxSSY
            // 
            resources.ApplyResources(this.textBoxSSY, "textBoxSSY");
            this.textBoxSSY.Name = "textBoxSSY";
            this.textBoxSSY.ReadOnly = true;
            // 
            // textBoxSSR
            // 
            resources.ApplyResources(this.textBoxSSR, "textBoxSSR");
            this.textBoxSSR.Name = "textBoxSSR";
            this.textBoxSSR.ReadOnly = true;
            // 
            // textBoxSE
            // 
            resources.ApplyResources(this.textBoxSE, "textBoxSE");
            this.textBoxSE.Name = "textBoxSE";
            this.textBoxSE.ReadOnly = true;
            // 
            // buttonY
            // 
            resources.ApplyResources(this.buttonY, "buttonY");
            this.buttonY.Name = "buttonY";
            this.buttonY.UseVisualStyleBackColor = true;
            this.buttonY.Click += new System.EventHandler(this.buttonY_Click);
            // 
            // pictureBoxParameter
            // 
            resources.ApplyResources(this.pictureBoxParameter, "pictureBoxParameter");
            this.pictureBoxParameter.Image = global::Mayfly.Mathematics.Properties.Resources.Check;
            this.pictureBoxParameter.Name = "pictureBoxParameter";
            this.pictureBoxParameter.TabStop = false;
            this.pictureBoxParameter.DoubleClick += new System.EventHandler(this.pictureBoxF_DoubleClick);
            // 
            // pictureBoxF
            // 
            resources.ApplyResources(this.pictureBoxF, "pictureBoxF");
            this.pictureBoxF.Image = global::Mayfly.Mathematics.Properties.Resources.Check;
            this.pictureBoxF.Name = "pictureBoxF";
            this.pictureBoxF.TabStop = false;
            this.pictureBoxF.DoubleClick += new System.EventHandler(this.pictureBoxF_DoubleClick);
            // 
            // buttonX
            // 
            resources.ApplyResources(this.buttonX, "buttonX");
            this.buttonX.Name = "buttonX";
            this.buttonX.UseVisualStyleBackColor = true;
            this.buttonX.Click += new System.EventHandler(this.buttonX_Click);
            // 
            // labelN
            // 
            resources.ApplyResources(this.labelN, "labelN");
            this.labelN.Name = "labelN";
            // 
            // buttonCopy
            // 
            resources.ApplyResources(this.buttonCopy, "buttonCopy");
            this.buttonCopy.FlatAppearance.BorderSize = 0;
            this.buttonCopy.Image = global::Mayfly.Pictogram.Copy;
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // labelEquation
            // 
            resources.ApplyResources(this.labelEquation, "labelEquation");
            this.labelEquation.Name = "labelEquation";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Name = "label1";
            // 
            // labelDescriptive
            // 
            resources.ApplyResources(this.labelDescriptive, "labelDescriptive");
            this.labelDescriptive.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelDescriptive.Name = "labelDescriptive";
            // 
            // labelStats
            // 
            resources.ApplyResources(this.labelStats, "labelStats");
            this.labelStats.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelStats.Name = "labelStats";
            // 
            // labelType
            // 
            resources.ApplyResources(this.labelType, "labelType");
            this.labelType.Name = "labelType";
            // 
            // textBoxN
            // 
            resources.ApplyResources(this.textBoxN, "textBoxN");
            this.textBoxN.Name = "textBoxN";
            this.textBoxN.ReadOnly = true;
            // 
            // labelParameters
            // 
            resources.ApplyResources(this.labelParameters, "labelParameters");
            this.labelParameters.Name = "labelParameters";
            // 
            // tabPageDiagnostic
            // 
            resources.ApplyResources(this.tabPageDiagnostic, "tabPageDiagnostic");
            this.tabPageDiagnostic.Name = "tabPageDiagnostic";
            this.tabPageDiagnostic.UseVisualStyleBackColor = true;
            // 
            // RegressionProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegressionProperties";
            this.ShowIcon = false;
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxParameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDiagnostic;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.DomainUpDown domainParameters;
        private System.Windows.Forms.Label labelValues;
        private System.Windows.Forms.Label labelDC;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.WebBrowser webBrowserEquation;
        private System.Windows.Forms.Label labelSE;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.TextBox textBoxSE;
        private System.Windows.Forms.Button buttonY;
        private System.Windows.Forms.Button buttonX;
        private System.Windows.Forms.Label labelN;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label labelEquation;
        private System.Windows.Forms.Label labelStats;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TextBox textBoxN;
        private System.Windows.Forms.Label labelParameters;
        private System.Windows.Forms.Label labelR2;
        private System.Windows.Forms.Label labelDescriptive;
        private System.Windows.Forms.Label labelF;
        private System.Windows.Forms.Label labelSSR;
        private System.Windows.Forms.TextBox textBoxF;
        private System.Windows.Forms.TextBox textBoxSSR;
        private System.Windows.Forms.PictureBox pictureBoxF;
        private System.Windows.Forms.Label labelSSY;
        private System.Windows.Forms.TextBox textBoxSSY;
        private System.Windows.Forms.TextBox textBoxDC;
        private System.Windows.Forms.PictureBox pictureBoxParameter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Controls.NumberBox numXp;
        private Controls.NumberBox numYp;
        private Controls.NumberBox numY;
        private Controls.NumberBox numX;
    }
}