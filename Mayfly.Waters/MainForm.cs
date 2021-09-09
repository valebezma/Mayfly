using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.TaskDialogs;
using Mayfly;
using System.Reflection;
using Mayfly.Extensions;
using Mayfly.Software;

namespace Mayfly.Waters
{
    public partial class MainForm : Form
    {
        private string filename;

        public string FileName
        {
            set
            {
                this.ResetText(value == null ? IO.GetNewFileCaption(UserSettings.Interface.Extension) : value, EntryAssemblyInfo.Title);
                filename = value;
            }
            get
            {
                return filename;
            }
        }

        public WatersKey Data
        {
            set;
            get;
        }

        public bool IsChanged
        {
            set;
            get;
        }

        private WatersKey.WaterRow SelectedWater
        {
            set;
            get;
        }


        
        public MainForm()
        {
            InitializeComponent();
            listViewWaters.Shine();

            Data = new WatersKey();
            FileName = null;

            searchBox1.SetEmpty();
            labelTreeCount.UpdateStatus(Data.Water.Count);
            labelListCount.UpdateStatus(listViewWaters.Items.Count);
        }

        public MainForm(string filename) : this()
        {
            Open(filename);
        }


        
        private DialogResult CheckAndSave()
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSaveChanges.ShowDialog(this);

                if (b == tdbSave)
                {
                    menuItemSave_Click(menuItemSave, new EventArgs());
                    return DialogResult.OK;
                }
                else if (b == tdbDiscard)
                {
                    return DialogResult.No;
                }
                else if (b == tdbCancelClose)
                {
                    return DialogResult.Cancel;
                }
            }

            return DialogResult.No;
        }

        private void Save(string filename)
        {
            Data.SaveToFile(filename);
            FileName = filename;
            status1.Message(Resources.Messages.Saved);
            IsChanged = false;
        }

        private void Open(string filename)
        {
            Data = new WatersKey();
            Data.ReadXml(filename);

            FileName = filename;

            labelTreeCount.UpdateStatus(Data.Water.Count);
            waterTree1.FillTree(Data);

            IsChanged = false;
        }

        private void FillList(WatersKey.WaterRow[] WaterRows)
        {
            if (WaterRows.Length == 0) return;

            Cursor = Cursors.AppStarting;
            listViewWaters.Items.Clear();

            if (!backFiller.IsBusy)
            {
                backFiller.RunWorkerAsync(WaterRows);
            }
        }

        private void backFiller_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ListViewItem> result = new List<ListViewItem>();

            foreach (WatersKey.WaterRow waterRow in (WatersKey.WaterRow[])e.Argument)
            {
                ListViewItem item = UpdateWaterItem(waterRow);
                item.Name = waterRow.ID.ToString();
                result.Add(item);
            }

            e.Result = result.ToArray();
        }

        private void backFiller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listViewWaters.Items.AddRange((ListViewItem[])e.Result);
            labelListCount.UpdateStatus(listViewWaters.Items.Count);
            Cursor = Cursors.Default;
        }

        private ListViewItem UpdateWaterItem(WatersKey.WaterRow WaterRow)
        {
            ListViewItem result = listViewWaters.GetItem(WaterRow.ID.ToString());

            if (result == null)
            {
                result = new ListViewItem();

                switch ((WaterType)WaterRow.Type)
                {
                    case WaterType.Stream:
                        result.Group = listViewWaters.Groups["listViewGroupStreams"];
                        break;
                    case WaterType.Lake:
                        result.Group = listViewWaters.Groups["listViewGroupLakes"];
                        break;
                    case WaterType.Tank:
                        result.Group = listViewWaters.Groups["listViewGroupTanks"];
                        break;
                }

                result.Tag = WaterRow;
            }

            result.SubItems.Clear();

            if (WaterRow.IsWaterNull())
            {
                result.Text = Resources.Interface.Unnamed;
            }
            else
            {
                result.Text = WaterRow.Water;
            }

            if (WaterRow.IsOutflowNull())
            {
                result.SubItems.Add(string.Empty);
                result.SubItems.Add(string.Empty);
            }
            else
            {
                WatersKey.WaterRow Outflow = Data.Water.FindByID(WaterRow.Outflow);
                if (Outflow.IsWaterNull())
                {
                    result.SubItems.Add(Resources.Interface.Unnamed);
                }
                else
                {
                    result.SubItems.Add(Outflow.Water);
                }

                if (WaterRow.IsMouthToMouthNull())
                {
                    result.SubItems.Add(string.Empty);
                }
                else
                {
                    switch ((WaterType)Outflow.Type)
                    {
                        case WaterType.Stream:
                            result.SubItems.Add(WaterRow.MouthToMouth.ToString("# ##0.0"));
                            break;
                        case WaterType.Tank:
                        case WaterType.Lake:
                            if (Outflow.IsMouthToMouthNull())
                                result.SubItems.Add(string.Empty);
                            else
                            {
                                result.SubItems.Add((Outflow.MouthToMouth + WaterRow.MouthToMouth).ToString("# ##0.0"));
                            }
                            break;
                    }
                }
            }

            if (WaterRow.IsLengthNull())
            {
                result.SubItems.Add(string.Empty);
            }
            else
            {
                result.SubItems.Add(WaterRow.Length.ToString("# ##0.0"));
            }

            return result;
        }

        private void UpdateWater(WatersKey.WaterRow waterRow)
        {
            if (!waterRow.IsOutflowNull())
            {
                //WatersKey.WaterRow possibleOutflow = WatersKey.PossibleOutflow(waterRow, waterRow.WaterRowParent);

                //if (possibleOutflow != null)
                //{
                //    taskDialogReattach.Content = string.Format(Resources.Messages.ReattachContent_auto,
                //        WatersKey.WaterFullName(waterRow.WaterRowParent),
                //        WatersKey.WaterFullName(possibleOutflow),
                //        WatersKey.WaterFullName(waterRow));
                //    TaskDialogButton b = taskDialogReattach.ShowDialog(this);

                //    if (b == tdbReattach)
                //    {
                //        waterRow.Outflow = possibleOutflow.ID;
                //        waterRow.MouthToMouth = waterRow.MouthToMouth - possibleOutflow.MouthToMouth;
                //    }
                //}
            }

            //UpdateWaterNode(waterRow);
            UpdateWaterItem(waterRow);
            if (waterRow == SelectedWater) UpdateInfo();
        }

        private void Clear()
        {
            Data = new WatersKey();
            FileName = null;
        }

        private void ClearDescription()
        {
            labelWaterName.Text = string.Empty;
            textBoxDescription.Text = string.Empty; 
        }

        private void UpdateInfo()
        {
            if (SelectedWater == null)
            {
                ClearDescription();
            }
            else
            {
                labelWaterName.Text = SelectedWater.FullName;
                textBoxDescription.Text = SelectedWater.Description;
            }
        }

        private void Delete(WatersKey.WaterRow water)
        {
            status1.Message(Resources.Messages.Deleted, water.FullName);

            TreeNode node = waterTree1.GetNode(water.ID.ToString());
            if (node != null)
            {
                node.Remove();
            }

            ListViewItem item = listViewWaters.GetItem(water.ID.ToString());
            if (item != null)
            {
                item.Remove();
            }

            water.Delete();
        }



        #region Menu

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                //Clear();
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (CheckAndSave() != DialogResult.Cancel)
                {
                    Open(UserSettings.Interface.OpenDialog.FileName);
                }
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e)
        {
            if (IsChanged)
            {
                if (FileName == null)
                {
                    menuItemSaveAs_Click(menuItemSaveAs, new EventArgs());
                }
                else
                {
                    Save(FileName);
                }
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.SaveDialog.ShowDialog() == DialogResult.OK)
            {
                Save(UserSettings.Interface.SaveDialog.FileName);
            }
        }

        private void menuItemPreview_Click(object sender, EventArgs e)
        {
            Data.Report.Preview();
        }

        private void menuItemPrint_Click(object sender, EventArgs e)
        {
            Data.Report.Print();
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void menuItemAddStream_Click(object sender, EventArgs e)
        {
            CardStream WaterForm = new CardStream(Data);
            WaterForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
            WaterForm.Show();
        }

        private void menuItemAddLake_Click(object sender, EventArgs e)
        {
            CardLake WaterForm = new CardLake(Data);
            WaterForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
            WaterForm.Show();
        }

        private void menuItemAddTank_Click(object sender, EventArgs e)
        {
            CardTank WaterForm = new CardTank(Data);
            WaterForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
            WaterForm.Show();
        }

        private void menuItemEdit_Click(object sender, EventArgs e)
        {
            switch ((WaterType)SelectedWater.Type)
            {
                case WaterType.Stream:
                    CardStream StreamForm = new CardStream(Data);
                    StreamForm.WaterRow = SelectedWater;
                    StreamForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
                    StreamForm.Show();
                    break;
                case WaterType.Lake:
                    CardLake LakeForm = new CardLake(Data);
                    LakeForm.WaterRow = SelectedWater;
                    LakeForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
                    LakeForm.Show();
                    break;
                case WaterType.Tank:
                    CardTank TankForm = new CardTank(Data);
                    TankForm.WaterRow = SelectedWater;
                    TankForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
                    TankForm.Show();
                    break;
            }
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {                
            taskDialogDelete.Content = string.Format(Resources.Messages.DeleteContent, SelectedWater.FullName);

            WatersKey.WaterRow[] Inflows = SelectedWater.GetInflows();

            tdbDeleteInflows.Enabled = (Inflows.Length > 0);

            TaskDialogButton b = taskDialogDelete.ShowDialog(this);

            if (b == tdbDeleteOne)
            {
                foreach (WatersKey.WaterRow inflow in SelectedWater.GetInflows(false))
                {
                    inflow.SetOutflowNull();
                    UpdateWater(inflow);
                }

                Delete(SelectedWater);
            }
            else if (b == tdbDeleteInflows)
            {
                foreach (WatersKey.WaterRow inflow in Inflows)
                {
                    Delete(inflow);
                }

                Delete(SelectedWater);
            }
            else if (b == tdbDeleteCancel)
            {
                return;
            }

            IsChanged = true;
            //treeViewWaters_AfterSelect(treeViewWaters, new TreeViewEventArgs(treeViewWaters.SelectedNode));
        }
        
        private void menuItemFilter_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter(Data);
            filter.buttonApply.Click += new EventHandler(Filter_Click);
            filter.Show();
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            FillList(((Filter)((Button)sender).FindForm()).FilteredWaters);
        }



        private void menuItemSettings_Click(object sender, EventArgs e)
        {

        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }
        
        #endregion



        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = CheckAndSave() == DialogResult.Cancel;
        }
        


        private void treeViewWaters_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            menuItemEdit_Click(sender, e);
        }

        private void treeViewWaters_WaterSelected(object sender, Controls.WaterEventArgs e)
        {
            searchBox1.SetEmpty();
            SelectedWater = e.WaterRow;

            FillList(waterTree1.WaterObjects.ToArray());
            UpdateInfo();
        }

        private void treeViewWaters_WaterUpdated(object sender, Controls.WaterEventArgs e)
        {
            IsChanged = true;
            UpdateWater(e.WaterRow);
        }
    


        private void contextToolStripMenuItemAddStream_Click(object sender, EventArgs e)
        {
            CardStream WaterForm = new CardStream(Data);
            //WaterForm.waterSelectorOutflow.WaterObject = Data.Water.FindByID(SelectedWater.ID);
            WaterForm.SetOutflow();
            WaterForm.IsChanged = false;
            WaterForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
            WaterForm.Show();
        }

        private void contextToolStripMenuItemAddLake_Click(object sender, EventArgs e)
        {
            CardLake WaterForm = new CardLake(Data);
            WaterForm.comboBoxKind.SelectedIndex = 0;
            //WaterForm.waterSelectorOutflow.WaterObject = Data.Water.FindByID(SelectedWater.ID);
            WaterForm.IsChanged = false;
            WaterForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
            WaterForm.Show();
        }

        private void contextToolStripMenuItemAddTank_Click(object sender, EventArgs e)
        {
            CardTank WaterForm = new CardTank(Data);
            //WaterForm.waterSelectorOutflow.WaterObject = Data.Water.FindByID(SelectedWater.ID);
            WaterForm.IsChanged = false;
            WaterForm.FormClosed += new FormClosedEventHandler(WaterForm_Closed);
            WaterForm.Show();
        }

        private void loadInflowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillList(SelectedWater.GetInflows());
        }

        private void listViewWaters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewWaters.SelectedIndices.Count == 0)
            {
                ClearDescription();
            }
            else
            {
                SelectedWater = listViewWaters.Items[listViewWaters.SelectedIndices[0]].Tag as WatersKey.WaterRow;
                UpdateInfo();
            }
        }

        private void listViewWaters_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewWaters.HitTest(e.Location).Item != null)
                {
                    contextMenuStripWater.Show(listViewWaters, e.Location); 
                }
            }
        }

        private void searchBox1_TextChanged(object sender, EventArgs e)
        {
            FillList(Data.GetWatersNameContaining(searchBox1.Text));
            status1.Message(listViewWaters.Items.Count.ToString(Resources.Messages.Filtered));
        }

        private void WaterForm_Closed(object sender, EventArgs e)
        {
            if (sender is CardTank)
            {
                CardTank T = (CardTank)sender;
                IsChanged = IsChanged || T.IsChanged;
                if (T.WaterRow != null)
                {
                    UpdateWater(T.WaterRow);
                }
            }

            if (sender is CardLake)
            {
                CardLake L = (CardLake)sender;
                IsChanged = IsChanged || L.IsChanged;
                if (L.WaterRow != null)
                {
                    UpdateWater(L.WaterRow);
                }
            }

            if (sender is CardStream)
            {
                CardStream S = (CardStream)sender;
                IsChanged = IsChanged || S.IsChanged;
                if (S.WaterRow != null)
                {
                    UpdateWater(S.WaterRow);
                }
            }
        }
    }
}
