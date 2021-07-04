namespace Mayfly.Species
{
    partial class SpeciesCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeciesCard));
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelSpecies = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelLocal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelTaxa = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelReference = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSynonyms = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelSpecies
            // 
            resources.ApplyResources(this.labelSpecies, "labelSpecies");
            this.labelSpecies.Name = "labelSpecies";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labelLocal
            // 
            resources.ApplyResources(this.labelLocal, "labelLocal");
            this.labelLocal.Name = "labelLocal";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // labelTaxa
            // 
            resources.ApplyResources(this.labelTaxa, "labelTaxa");
            this.labelTaxa.Name = "labelTaxa";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // labelReference
            // 
            resources.ApplyResources(this.labelReference, "labelReference");
            this.labelReference.Name = "labelReference";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.Name = "labelDescription";
            // 
            // buttonSelect
            // 
            resources.ApplyResources(this.buttonSelect, "buttonSelect");
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // labelSynonyms
            // 
            resources.ApplyResources(this.labelSynonyms, "labelSynonyms");
            this.labelSynonyms.Name = "labelSynonyms";
            // 
            // SpeciesCard
            // 
            this.AcceptButton = this.buttonSelect;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelSynonyms);
            this.Controls.Add(this.labelTaxa);
            this.Controls.Add(this.labelLocal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelReference);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelSpecies);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.buttonClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpeciesCard";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelSpecies;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLocal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelTaxa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelReference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSynonyms;
    }
}