namespace Mayfly.Waters.Controls
{
    partial class WaterTree
    {
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaterTree));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.taskDialogReattach = new Mayfly.TaskDialogs.TaskDialog(this.components);
            this.tdbReattach = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.tdbCancelReattach = new Mayfly.TaskDialogs.TaskDialogButton(this.components);
            this.backLoader = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empty.bmp");
            this.imageList1.Images.SetKeyName(1, "terminal_river.bmp");
            this.imageList1.Images.SetKeyName(2, "river.bmp");
            this.imageList1.Images.SetKeyName(3, "inflow_left.bmp");
            this.imageList1.Images.SetKeyName(4, "inflow_right.bmp");
            this.imageList1.Images.SetKeyName(5, "tank.bmp");
            this.imageList1.Images.SetKeyName(6, "lake_flood_left.bmp");
            this.imageList1.Images.SetKeyName(7, "lake_flood_right.bmp");
            this.imageList1.Images.SetKeyName(8, "lake.bmp");
            // 
            // taskDialogReattach
            // 
            this.taskDialogReattach.AllowDialogCancellation = true;
            this.taskDialogReattach.Buttons.Add(this.tdbReattach);
            this.taskDialogReattach.Buttons.Add(this.tdbCancelReattach);
            this.taskDialogReattach.CenterParent = true;
            resources.ApplyResources(this.taskDialogReattach, "taskDialogReattach");
            // 
            // tdbReattach
            // 
            resources.ApplyResources(this.tdbReattach, "tdbReattach");
            // 
            // tdbCancelReattach
            // 
            this.tdbCancelReattach.ButtonType = Mayfly.TaskDialogs.ButtonType.Cancel;
            // 
            // backLoader
            // 
            this.backLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backLoader_DoWork);
            this.backLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backLoader_RunWorkerCompleted);
            // 
            // WaterTree
            // 
            this.AllowDrop = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FullRowSelect = true;
            this.HotTracking = true;
            resources.ApplyResources(this, "$this");
            this.ImageList = this.imageList1;
            this.ItemHeight = 23;
            this.LineColor = System.Drawing.Color.Black;
            this.Name = "waterTree";
            this.ShowLines = false;
            this.ShowNodeToolTips = true;
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ImageList imageList1;
        private TaskDialogs.TaskDialog taskDialogReattach;
        private TaskDialogs.TaskDialogButton tdbReattach;
        private TaskDialogs.TaskDialogButton tdbCancelReattach;
        private System.ComponentModel.BackgroundWorker backLoader;
    }
}