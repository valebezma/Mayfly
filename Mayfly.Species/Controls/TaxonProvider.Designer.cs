namespace Mayfly.Species
{
    partial class TaxonProvider
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxonProvider));
            this.listSpc = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listLoader = new System.ComponentModel.BackgroundWorker();
            // 
            // listSpc
            // 
            this.listSpc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listSpc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listSpc.FullRowSelect = true;
            this.listSpc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listSpc.HideSelection = false;
            resources.ApplyResources(this.listSpc, "listSpc");
            this.listSpc.MultiSelect = false;
            this.listSpc.Name = "listSpc";
            this.listSpc.ShowItemToolTips = true;
            this.listSpc.UseCompatibleStateImageBehavior = false;
            this.listSpc.View = System.Windows.Forms.View.Details;
            this.listSpc.ItemActivate += new System.EventHandler(this.listSpc_ItemActivate);
            this.listSpc.SelectedIndexChanged += new System.EventHandler(this.listSpc_SelectedIndexChanged);
            this.listSpc.VisibleChanged += new System.EventHandler(this.listSpc_VisibleChanged);
            this.listSpc.Enter += new System.EventHandler(this.listSpc_Enter);
            this.listSpc.Leave += new System.EventHandler(this.listSpc_Leave);
            this.listSpc.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.listSpc_PreviewKeyDown);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // listLoader
            // 
            this.listLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.listLoader_DoWork);
            this.listLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.listLoader_RunWorkerCompleted);

        }

        #endregion

        private System.Windows.Forms.ListView listSpc;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.ComponentModel.BackgroundWorker listLoader;
    }
}
