namespace Mayfly.Wild.Exchange
{
    partial class PermissionGrant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PermissionGrant));
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxReference = new System.Windows.Forms.TextBox();
            this.textBoxRecipient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeExpiration = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.labelExchangeInstruction = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.labelPass = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // textBoxReference
            // 
            resources.ApplyResources(this.textBoxReference, "textBoxReference");
            this.textBoxReference.Name = "textBoxReference";
            this.textBoxReference.ReadOnly = true;
            // 
            // textBoxRecipient
            // 
            resources.ApplyResources(this.textBoxRecipient, "textBoxRecipient");
            this.textBoxRecipient.Name = "textBoxRecipient";
            this.textBoxRecipient.TextChanged += new System.EventHandler(this.textBoxRecipient_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dateTimeExpiration
            // 
            resources.ApplyResources(this.dateTimeExpiration, "dateTimeExpiration");
            this.dateTimeExpiration.Name = "dateTimeExpiration";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelExchangeInstruction
            // 
            resources.ApplyResources(this.labelExchangeInstruction, "labelExchangeInstruction");
            this.labelExchangeInstruction.Name = "labelExchangeInstruction";
            // 
            // buttonSave
            // 
            resources.ApplyResources(this.buttonSave, "buttonSave");
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPass
            // 
            resources.ApplyResources(this.textBoxPass, "textBoxPass");
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.TextChanged += new System.EventHandler(this.textBoxRecipient_TextChanged);
            // 
            // labelPass
            // 
            resources.ApplyResources(this.labelPass, "labelPass");
            this.labelPass.Name = "labelPass";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // PermissionGrant
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelExchangeInstruction);
            this.Controls.Add(this.dateTimeExpiration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxRecipient);
            this.Controls.Add(this.textBoxReference);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PermissionGrant";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label label7;
        protected System.Windows.Forms.TextBox textBoxReference;
        protected System.Windows.Forms.TextBox textBoxRecipient;
        protected System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeExpiration;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label labelExchangeInstruction;
        private System.Windows.Forms.Button buttonSave;
        protected System.Windows.Forms.TextBox textBoxPass;
        protected System.Windows.Forms.Label labelPass;
        protected System.Windows.Forms.Label label3;
    }
}