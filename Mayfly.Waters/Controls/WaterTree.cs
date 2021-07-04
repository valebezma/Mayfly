using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Mayfly.Controls;
using Mayfly.TaskDialogs;
using Mayfly.Extensions;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Mayfly.Waters.Controls
{
    public partial class WaterTree : TreeView
    {
        private WatersKey watersIndex;

        private WatersKey.WaterRow waterRow;

        public WatersKey.WaterRow WaterObject
        {
            get
            {
                return waterRow;
            }

            set
            {
                waterRow = value;

                if (WaterSelected != null)
                {
                    WaterSelected.Invoke(this, new WaterEventArgs(value));
                }
            }
        }

        public bool IsWaterSelected
        {
            get
            {
                return WaterObject != null;
            }
        }

        public bool AllowReplacement
        {
            get;
            set;
        }

        public event WaterEventHandler WaterSelected;

        public event WaterEventHandler WaterUpdated;

        public event TreeNodeEventHandler WaterAdded;

        public List<WatersKey.WaterRow> WaterObjects 
        {
            get;
            set;
        }

        public ContextMenuStrip WaterMenuStrip 
        {
            get;
            set;
        }



        public WaterTree()
        {
            InitializeComponent();

            this.Shine();
            this.TreeViewNodeSorter = new WaterMouthToMouthSorter();
        }



        public WatersKey.WaterRow[] GetWatersNameContaining(string query)
        {
            List<WatersKey.WaterRow> result = new List<WatersKey.WaterRow>();

            if (WaterObjects != null)
            {
                foreach (WatersKey.WaterRow waterRow in WaterObjects)
                {
                    if (waterRow.FullName.ToUpperInvariant().Contains(query.ToUpperInvariant()))
                    {
                        result.Add(waterRow);
                    }
                }

                result.Sort(new SearchResultSorter(query));
            }

            return result.ToArray();
        }

        public void FillTree(WatersKey index)
        {
            watersIndex = index;
            this.Nodes.Clear();

            TreeNode nodeType = this.Nodes.Add("Type", Resources.WaterTree.Type);
            nodeType.Nodes.Add("Streams", Resources.WaterTree.Streams);
            nodeType.Nodes.Add("Lakes", Resources.WaterTree.Lakes);
            nodeType.Nodes.Add("Tanks", Resources.WaterTree.Tanks);

            TreeNode nodeBasin = this.Nodes.Add("Basin", Resources.WaterTree.Basin);
            nodeBasin.Nodes.Add("Inlands", Resources.WaterTree.Inlands);

            this.Lock();

            backLoader.RunWorkerAsync(watersIndex.GetRoots());
        }

        private void backLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            AddNodes((WatersKey.WaterRow[])e.Argument);
        }

        private void backLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Sort();
            this.GetNode("Type").Expand();
            this.GetNode("Basin").Expand();
            this.Unlock();
        }

        private void AddNodes(WatersKey.WaterRow[] waterRows)
        {
            foreach (WatersKey.WaterRow waterRow in waterRows)
            {
                TreeNode result = this.GetNode(waterRow.ID.ToString());

                if (result == null)
                {
                    result = new TreeNode();
                    result.Name = waterRow.ID.ToString();
                    result.Tag = waterRow;
                    result.ContextMenuStrip = WaterMenuStrip;
                }

                result.Text = waterRow.FullName;
                int imageIndex = waterRow.GetImageIndex();

                if (waterRow.IsOutflowNull())
                {
                    result.Remove();

                    if (waterRow.Type == (int)WaterType.Lake)
                    {
                        this.AddNode("Inlands", result, imageIndex);
                    }
                    else
                    {
                        this.AddNode("Basin", result, imageIndex);
                    }
                }
                else
                {
                    if (result.Parent != this.GetNode(waterRow.Outflow.ToString()))
                    {
                        result.Remove();
                        this.AddNode(waterRow.Outflow.ToString(), result, imageIndex);
                    }
                }

                AddNodes(waterRow.GetWaterRows());

                //BackgroundWorker inflowLoader = new BackgroundWorker();
                //inflowLoader.DoWork += backLoader_DoWork;
                //inflowLoader.RunWorkerAsync(waterRow.GetWaterRows());
            }
        }

        private void AddNode(string parentName, TreeNode child, int imageIndex)
        {
            AddNode(this.GetNode(parentName), child, imageIndex);
        }

        private void AddNode(TreeNode parent, TreeNode child, int imageIndex)
        {
            if (parent.TreeView.InvokeRequired)
            {
                TreeNodeEventHandler treeNodeAdder = new TreeNodeEventHandler(AddNode);
                parent.TreeView.Invoke(treeNodeAdder, new object[] { parent, child, imageIndex });
            }
            else
            {
                parent.Nodes.Add(child);
                child.ImageIndex = child.SelectedImageIndex = imageIndex;

                if (WaterAdded != null)
                {
                    WaterAdded.Invoke(parent, child, imageIndex);
                }
            }
        }

        public delegate void TreeNodeEventHandler(TreeNode parent, TreeNode child, int imageIndex);

        //private TreeNode UpdateWaterNode(WatersKey.WaterRow waterRow)
        //{
        //    TreeNode result = this.GetNode(waterRow.ID.ToString());

        //    if (result == null)
        //    {
        //        result = new TreeNode();
        //        result.Name = waterRow.ID.ToString();
        //        result.Tag = waterRow;
        //        result.ContextMenuStrip = WaterMenuStrip;
        //    }

        //    result.Text = waterRow.FullName;

        //    if (waterRow.IsOutflowNull())
        //    {
        //        result.Remove();

        //        if (waterRow.Type == (int)WaterType.Lake)
        //        {
        //            this.GetNode("Inlands").Nodes.Add(result);
        //        }
        //        else
        //        {
        //            this.GetNode("Basin").Nodes.Add(result);
        //        }
        //    }
        //    else
        //    {
        //        if (result.Parent != this.GetNode(waterRow.Outflow.ToString()))
        //        {
        //            result.Remove();
        //            this.GetNode(waterRow.Outflow.ToString()).Nodes.Add(result);
        //        }
        //    }

        //    // Set image

        //    switch ((WaterType)waterRow.Type)
        //    {
        //        case WaterType.Stream:
        //            if (waterRow.IsOutflowNull())
        //            {
        //                result.ImageIndex = 1;
        //            }
        //            else
        //            {
        //                if (waterRow.IsMouthCoastNull())
        //                {
        //                    result.ImageIndex = 2;
        //                }
        //                else
        //                {
        //                    result.ImageIndex = waterRow.MouthCoast == 0 ? 3 : 4;
        //                }
        //            }
        //            break;

        //        case WaterType.Tank:
        //            result.ImageIndex = 5;
        //            break;

        //        case WaterType.Lake:
        //            if (waterRow.IsOutflowNull())
        //            {
        //                result.ImageIndex = 8;
        //            }
        //            else
        //            {
        //                if (waterRow.IsMouthCoastNull())
        //                {
        //                    result.ImageIndex = 2;
        //                }
        //                else
        //                {
        //                    result.ImageIndex = waterRow.MouthCoast == 1 ? 6 : 7;
        //                }
        //            }

        //            break;
        //    }

        //    result.SelectedImageIndex = result.ImageIndex;

        //    foreach (WatersKey.WaterRow inflow in waterRow.GetWaterRows())
        //    {
        //        UpdateWaterNode(inflow);
        //    }

        //    return result;
        //}

        private void Lock()
        {
            this.Enabled = false;
        }

        private void Unlock()
        {
            this.Enabled = true;
        }



        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            WaterObjects = new List<WatersKey.WaterRow>();

            if (e.Node.Name == "Basin")
            {
                WaterObject = null;
                return;
            }

            if (e.Node.Name == "Type")
            {
                WaterObjects.AddRange((WatersKey.WaterRow[])watersIndex.Water.Select());
                WaterObject = null;
                return;
            }

            if (e.Node.Name == "Streams")
            {
                WaterObjects.AddRange(watersIndex.GetStreams());
                WaterObject = null;
                return;
            }

            if (e.Node.Name == "Lakes")
            {
                WaterObjects.AddRange(watersIndex.GetLakes());
                WaterObject = null;
                return;
            }

            if (e.Node.Name == "Tanks")
            {
                WaterObjects.AddRange(watersIndex.GetTanks());
                WaterObject = null;
                return;
            }

            if (e.Node.Name == "Inlands")
            {
                WaterObjects.AddRange(watersIndex.GetInlandLakes());
                WaterObject = null;
                return;
            }

            WaterObjects.Add(e.Node.Tag as WatersKey.WaterRow);
            WaterObjects.AddRange((
                e.Node.Tag as WatersKey.WaterRow).GetInflows());
            WaterObject = e.Node.Tag as WatersKey.WaterRow;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            if (e.Effect == DragDropEffects.Move)
            {
                TreeNode hoveringNode = this.GetHoveringNode(e.X, e.Y);

                if (hoveringNode != null)
                {
                    TreeNode draggingNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;

                    if (draggingNode != null)
                    {
                        WatersKey.WaterRow dragWater = draggingNode.Tag as WatersKey.WaterRow;
                        WatersKey.WaterRow destinationOutflow = hoveringNode.Tag as WatersKey.WaterRow;

                        if (dragWater.IsOutflowNull())
                        {
                            dragWater.Outflow = destinationOutflow.ID;

                            if (WaterUpdated != null)
                            {
                                WaterUpdated.Invoke(this, new WaterEventArgs(dragWater));
                            }
                        }
                        else
                        {
                            if (dragWater.GetOutflow() != destinationOutflow)
                            {
                                taskDialogReattach.Content = string.Format(Resources.Messages.ReattachContent,
                                    dragWater.FullName,
                                    dragWater.WaterRowParent.FullName,
                                    destinationOutflow.FullName);

                                TaskDialogButton b = taskDialogReattach.ShowDialog(this);

                                if (b == tdbReattach)
                                {
                                    WatersKey.WaterRow possibleOutflow = dragWater.GetPossibleOutflow(destinationOutflow);

                                    if (possibleOutflow != null)
                                    {
                                        taskDialogReattach.Content = string.Format(Resources.Messages.ReattachContent,
                                            destinationOutflow.FullName, possibleOutflow.FullName, dragWater.FullName);

                                        TaskDialogButton b2 = taskDialogReattach.ShowDialog(this);

                                        if (b2 == tdbReattach)
                                        {
                                            destinationOutflow = possibleOutflow;
                                            dragWater.MouthToMouth = dragWater.MouthToMouth - possibleOutflow.MouthToMouth;
                                        }
                                    }

                                    dragWater.Outflow = destinationOutflow.ID;

                                    if (WaterUpdated != null)
                                    {
                                        WaterUpdated.Invoke(this, new WaterEventArgs(dragWater));
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetData(typeof(TreeNode)) is TreeNode)
            { e.Effect = DragDropEffects.Copy; }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            TreeNode hoveringNode = this.GetHoveringNode(e.X, e.Y);
            TreeNode draggingNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;

            if (hoveringNode != null && hoveringNode != draggingNode)
            {
                e.Effect = DragDropEffects.Move;
                hoveringNode.TreeView.SelectedNode = hoveringNode;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (AllowReplacement)
            {
                TreeNode node = e.Item as TreeNode;

                if (node.Name != "Inlands")
                {
                    this.DoDragDrop(node, DragDropEffects.All);
                }
            }
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (e.Button == MouseButtons.Right)
            {
                this.SelectedNode = e.Node;

                if (WaterMenuStrip != null)
                {
                    WaterMenuStrip.Show(e.Location);
                }
            }
        }

        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseDoubleClick(e);

            //menuItemEdit_Click(sender, e);
        }

        private enum SortMode
        {
            MouthToMouth,
            Name
        };
    }
}
