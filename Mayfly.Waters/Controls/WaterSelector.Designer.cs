namespace Mayfly.Waters.Controls
{
    partial class WaterSelector
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
            this.listWaters = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listLoader = new System.ComponentModel.BackgroundWorker();
            // 
            // listWaters
            // 
            this.listWaters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listWaters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listWaters.FullRowSelect = true;
            this.listWaters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listWaters.HideSelection = false;
            this.listWaters.LabelWrap = false;
            this.listWaters.Location = new System.Drawing.Point(0, 0);
            this.listWaters.MultiSelect = false;
            this.listWaters.Name = "listWaters";
            this.listWaters.ShowItemToolTips = true;
            this.listWaters.Size = new System.Drawing.Size(121, 250);
            this.listWaters.TabIndex = 0;
            this.listWaters.TileSize = new System.Drawing.Size(235, 25);
            this.listWaters.UseCompatibleStateImageBehavior = false;
            this.listWaters.View = System.Windows.Forms.View.Details;
            this.listWaters.Visible = false;
            this.listWaters.ItemActivate += new System.EventHandler(this.listViewWaters_ItemActivate);
            this.listWaters.SelectedIndexChanged += new System.EventHandler(this.listViewWaters_SelectedIndexChanged);
            this.listWaters.VisibleChanged += new System.EventHandler(this.listViewWaters_VisibleChanged);
            this.listWaters.Leave += new System.EventHandler(this.listViewWaters_Leave);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 260;
            // 
            // listLoader
            // 
            this.listLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.listLoader_DoWork);
            this.listLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.listLoader_RunWorkerCompleted);

        }

        #endregion

        private System.Windows.Forms.ListView listWaters;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.ComponentModel.BackgroundWorker listLoader;
    }
}
