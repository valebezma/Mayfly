using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Mayfly.TaskDialogs;
using Mayfly.Controls;
using Mayfly.Extensions;

namespace Mayfly.Species.Controls
{
    public partial class TaxaTreeView : TreeView
    {
        SpeciesKey data;
        SpeciesKey.TaxonRow rootTaxon;
        TaxonomicRank deepestRank;
        bool allowEdit;

        SpeciesKey.TaxonRow pickedTaxon;
        string message = string.Empty;

        TaxonEventHandler beforeTaxonSelected;
        internal bool SelectionAllowed = true;
        TaxonEventHandler taxonSelected;
        EventHandler treeLoaded;

        EventHandler changed;
        TaxonEventHandler taxonChanged;
        //EventHandler positionChanged;

        //[Browsable(false)]
        //public new TreeNodeCollection Nodes { get; set; }

        [Category("Mayfly Events")]
        public event TaxonEventHandler TaxonSelected
        {
            add
            {
                taxonSelected += value;
            }

            remove
            {
                taxonSelected -= value;
            }
        }

        [Category("Mayfly Events")]
        public event TaxonEventHandler BeforeTaxonSelected
        {
            add
            {
                beforeTaxonSelected += value;
            }

            remove
            {
                beforeTaxonSelected -= value;
            }
        }

        [Category("Mayfly Events")]
        public event EventHandler Changed
        {
            add
            {
                changed += value;
            }

            remove
            {
                changed -= value;
            }
        }

        [Category("Mayfly Events")]
        public event TaxonEventHandler TaxonChanged
        {
            add
            {
                taxonChanged += value;
            }

            remove
            {
                taxonChanged -= value;
            }
        }

        [Category("Mayfly Events")]
        public event EventHandler OnTreeLoaded
        {
            add
            {
                treeLoaded += value;
            }

            remove
            {
                treeLoaded -= value;
            }
        }

        [Category("Behavior"), DefaultValue(null)]
        public ProcessDisplay Display
        {
            get;
            set;
        }

        [Category("Behavior"), Browsable(false)]
        public SpeciesKey.TaxonRow PickedTaxon
        {
            get { return pickedTaxon; }

            set
            {
                pickedTaxon = value;

                if (pickedTaxon == null)
                {
                    SelectedNode = null;
                }
                else
                {
                    TreeNode[] tnn = Nodes.Find(value.ID.ToString(), true);
                    if (tnn.Length > 0)
                    {
                        SelectedNode = tnn[0];
                    }
                }
            }
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool AllowEdit
        {
            get { return allowEdit; }

            set
            {
                allowEdit = 
                contextTreeNewSpc.Visible =
                    contextTreeNewTaxon.Visible =
                    toolStripSeparator4.Visible =
                    value;
            }
        }

        [Category("Appearance")]
        public string HigherTaxonFormat
        {
            get; set;
        }

        [Category("Appearance")]
        public string LowerTaxonFormat
        {
            get; set;
        }

        [Category("Appearance")]
        public Color LowerTaxonColor
        {
            get; set;
        }

        [Category("Behavior"), Browsable(false)]
        public SpeciesKey.TaxonRow RootTaxon
        {
            get { return rootTaxon; }
            set { rootTaxon = value;
                contextTreeZoomOut.Visible = value != null;
            }
        }

        [Category("Behavior"), Browsable(false)]
        public TaxonomicRank DeepestRank
        {
            get { return deepestRank; }
            set { deepestRank = value; 
                //LoadTree();
            }
        }



        public TaxaTreeView()
        {
            InitializeComponent();
            TreeViewNodeSorter = new TreeNodeSorter();
        }

        public TaxaTreeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            TreeViewNodeSorter = new TreeNodeSorter();
            ContextMenuStrip = contextTree;
            
            this.contextTreeNewTaxon.Click += new System.EventHandler(this.contextTreeNewTaxon_Click);
            this.contextTreeNewSpc.Click += new System.EventHandler(this.contextTreeNewTaxon_Click);
            this.contextTreeExpand.Click += new System.EventHandler(this.contextTreeExpand_Click);
            this.contextTreeCollapse.Click += new System.EventHandler(this.contextTreeCollapse_Click);
            this.contextTreeZoomOut.Click += new System.EventHandler(this.contextTreeZoomOut_Click);

            this.contextTaxonEdit.Click += new System.EventHandler(this.contextTaxonEdit_Click);
            this.contextTaxonDelete.Click += new System.EventHandler(this.contextTaxonDelete_Click);
            this.contextTaxonDepart.Click += new System.EventHandler(this.contextTaxonDepart_Click);
            this.contextTaxonAddTaxon.Click += new System.EventHandler(this.contextTreeNewTaxon_Click);
            this.contextTaxonAddSpecies.Click += new System.EventHandler(this.contextTreeNewTaxon_Click);
            this.contextTaxonExpandAll.Click += new System.EventHandler(this.contextTaxonExpandAll_Click);
            this.contextTaxonZoomIn.Click += new System.EventHandler(this.contextTaxonZoomIn_Click);

            this.contextSpeciesEdit.Click += new System.EventHandler(this.contextTaxonEdit_Click);
            this.contextSpeciesDelete.Click += new System.EventHandler(this.contextTaxonDelete_Click);
            this.contextSpeciesDepart.Click += new System.EventHandler(this.contextTaxonDepart_Click);
        }



        public void Bind(SpeciesKey index)
        {
            data = index;
        }

        public void LoadTree()
        {
            Enabled = false;
            Nodes.Clear();

            if (Display != null) Display.StartProcessing(Resources.Interface.StatusLoadingTree);
            loader.RunWorkerAsync();
        }

        public TreeNode AddNode(SpeciesKey.TaxonRow taxonRow)
        {
            if (DeepestRank != null && taxonRow.Rank > DeepestRank.Value) return null;

            if (!InvokeRequired)
            {
                TreeNode tn = getTaxonTreeNode(taxonRow);
                if (taxonRow.TaxonRowParent == null || taxonRow.TaxonRowParent == RootTaxon)
                {
                    Nodes.Add(tn);
                }
                else
                {
                    TreeNode[] tnn = Nodes.Find(taxonRow.TaxonRowParent.ID.ToString(), true);
                    if (tnn.Length == 0)
                    {
                        if (Display != null) Display.Message("Taxon replaced outside focused field");
                    }
                    else
                    {
                        tnn[0].Nodes.Add(tn);
                    }
                }
                return tn;
            }
            else
            {
                AddNodeEventHandler nodeAdder = new AddNodeEventHandler(AddNode);
                return (TreeNode)Invoke(nodeAdder, new object[] { taxonRow });
            }
        }

        public void ApplyRename(TreeNode treeNode, string text)
        {
            if (treeNode == null || !treeNode.TreeView.InvokeRequired) { treeNode.Text = text; }
            else
            {
                TextSetEventHandler textSetter = new TextSetEventHandler(ApplyRename);
                treeNode.TreeView.Invoke(textSetter, new object[] { treeNode, text });
            }
        }

        private delegate void TextSetEventHandler(TreeNode treeNode, string text);

        public void ApplyAppearance(TreeNode treeNode, Color color)
        {
            if (treeNode == null || !treeNode.TreeView.InvokeRequired) { treeNode.ForeColor = color; }
            else
            {
                ColorSetEventHandler textSetter = new ColorSetEventHandler(ApplyAppearance);
                treeNode.TreeView.Invoke(textSetter, new object[] { treeNode, color });
            }
        }

        private delegate void ColorSetEventHandler(TreeNode treeNode, Color color);

        public DragDropEffects CheckTaxonCompatibility(SpeciesKey.TaxonRow carryTaxonRow, SpeciesKey.TaxonRow targetTaxonRow)
        {
            if (carryTaxonRow == targetTaxonRow) // Taxon to itself
            {
                return DragDropEffects.None;
            }
            else if (carryTaxonRow.TaxonRowParent == targetTaxonRow) // If already parent
            {
                return DragDropEffects.None;
            }
            else if (carryTaxonRow.Rank == targetTaxonRow.Rank) // Same rank
            {
                if (ModifierKeys.HasFlag(Keys.Shift)) // If Shift is pressed - synonimize
                {
                    if (targetTaxonRow.IsSynonymyAvailable(carryTaxonRow))
                    {
                        notifyInstantly(Resources.Interface.TipSynonym, carryTaxonRow, targetTaxonRow);
                        return DragDropEffects.Link;
                    }
                    else
                    {
                        notifyInstantly(Resources.Interface.TipSynonymUnableSeparateBranch);
                        return DragDropEffects.None;
                    }
                }
                else // If Shift is not pressed
                {
                    if (carryTaxonRow.IsHigher) // Higher taxon positioning
                    {
                        notifyInstantly(Resources.Interface.TipSort, carryTaxonRow, targetTaxonRow);
                        return DragDropEffects.Move;
                    }
                    else if (carryTaxonRow.Rank == 91) // Species to species subordering
                    {
                        if (SpeciesKey.Genus(targetTaxonRow.Name) != SpeciesKey.Genus(carryTaxonRow.Name))
                        {
                            notifyInstantly(Resources.Interface.TipSubspeciesUnableNameMismatch, carryTaxonRow, targetTaxonRow);
                            return DragDropEffects.None;
                        }
                        else if (targetTaxonRow.IsSynonymyAvailable(carryTaxonRow))
                        {
                            notifyInstantly(Resources.Interface.TipSubspecies, carryTaxonRow, targetTaxonRow);
                            return DragDropEffects.Link;
                        }
                        else
                        {
                            notifyInstantly(Resources.Interface.TipSubspeciesUnableSeparateBranch);
                            return DragDropEffects.None;
                        }
                    }
                }
            }
            else if (carryTaxonRow.Rank < targetTaxonRow.Rank) // If rank mismatch
            {
                notifyInstantly(Resources.Interface.TipIncludeUnableWrongRank, carryTaxonRow, targetTaxonRow);
                return DragDropEffects.None;
            }
            else // Rank are fine
            {
                notifyInstantly(Resources.Interface.TipInclude, carryTaxonRow, targetTaxonRow);
                return DragDropEffects.Link;
            }

            return DragDropEffects.None;
        }



        private void notifyInstantly(string format, params object[] values)
        {
            Point pt = Cursor.Position;
            pt.Offset(-this.FindForm().Location.X + 15, -this.FindForm().Location.Y);
            if (message != string.Format(format, values))
            {
                message = string.Format(format, values);
                toolTip.Show(message, this.FindForm(), pt, 5000);
            }
        }

        private TreeNode getTaxonTreeNode(SpeciesKey.TaxonRow taxonRow)
        {
            TreeNode taxonNode = new TreeNode
            {
                Tag = taxonRow,
                Name = taxonRow.ID.ToString(),
                Text = taxonRow.ToString(taxonRow.IsHigher ? HigherTaxonFormat : LowerTaxonFormat),
                ContextMenuStrip = taxonRow.IsHigher ? contextTaxon : contextSpecies
            };

            if (LowerTaxonColor != null && !taxonRow.IsHigher) taxonNode.ForeColor = LowerTaxonColor;

            foreach (SpeciesKey.TaxonRow derRow in taxonRow.GetTaxonRows())
            {
                if (DeepestRank != null && derRow.Rank > DeepestRank.Value) continue;

                taxonNode.Nodes.Add(getTaxonTreeNode(derRow));
            }

            return taxonNode;
        }

        private delegate TreeNode AddNodeEventHandler(SpeciesKey.TaxonRow taxonRow);


        private void applySort(TreeNodeCollection nodes)
        {
            foreach (TreeNode tn in nodes)
            {
                if (tn.Tag is SpeciesKey.TaxonRow tr)
                {
                    if (!tr.IsHigher) continue;

                    tr.Index = (tn.Index + 1) * 10;
                    applySort(tn.Nodes);
                }
            }
        }

        public void ApplyRenameAscending(TreeNode node)
        {
            if (HigherTaxonFormat.ToLowerInvariant() != "t") return;
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync(node);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            TreeNode node = (TreeNode)e.Argument;
            SpeciesKey.TaxonRow taxonRow = (SpeciesKey.TaxonRow)((TreeNode)e.Argument).Tag;
            ApplyRename(node, taxonRow.ToString(taxonRow.IsHigher ? HigherTaxonFormat : LowerTaxonFormat));
            if (node.Parent != null) ApplyRenameAscending(node.Parent);
        }

        public void ApplyAppearanceDescending(TreeNode node)
        {
            BackgroundWorker bwapp = new BackgroundWorker();
            bwapp.DoWork += bwapp_DoWork;
            bwapp.RunWorkerAsync(node);
        }

        private void bwapp_DoWork(object sender, DoWorkEventArgs e)
        {
            TreeNode node = (TreeNode)e.Argument;
            SpeciesKey.TaxonRow taxonRow = (SpeciesKey.TaxonRow)((TreeNode)e.Argument).Tag;

            ApplyRename(node, taxonRow.ToString(taxonRow.IsHigher ? HigherTaxonFormat : LowerTaxonFormat));
            ApplyAppearance(node, (taxonRow.IsHigher || LowerTaxonColor == null) ? ForeColor : LowerTaxonColor);

            foreach (TreeNode childNode in node.Nodes)
            {
                ApplyAppearanceDescending(childNode);
            }

        }

        public void ApplyAppearanceDescending()
        {
            foreach (TreeNode node in Nodes)
            {
                ApplyAppearanceDescending(node);
            }
        }



        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectedNode = e.Node;
            }

            base.OnNodeMouseClick(e);
        }

        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseDoubleClick(e);

            if (!AllowEdit) return;

            if (pickedTaxon == null) return;

            contextTaxonEdit_Click(this, e);
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            if (beforeTaxonSelected != null)
            {
                beforeTaxonSelected.Invoke(this, new TaxonEventArgs(e.Node == null ? null : e.Node.Tag as SpeciesKey.TaxonRow));
            }

            if (SelectionAllowed)
            {
                pickedTaxon = e.Node == null ? null : e.Node.Tag as SpeciesKey.TaxonRow;
                if (taxonSelected != null) taxonSelected.Invoke(this, new TaxonEventArgs(pickedTaxon));
            }

            SelectionAllowed = true;
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (SelectedNode != e.Item)
            {
                OnAfterSelect(new TreeViewEventArgs((TreeNode)e.Item));
            }

            if (!AllowEdit) return;

            DoDragDrop(new SpeciesKey.TaxonRow[] { pickedTaxon }, DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Copy);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            if (!AllowEdit) return;

            Focus();
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            if (!AllowEdit) return;

            Point dropLocation = PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = GetNodeAt(dropLocation);
            if (targetNode == null) return;
            SpeciesKey.TaxonRow targetTaxonRow = targetNode.Tag as SpeciesKey.TaxonRow;
            if (targetTaxonRow == null) return;
            SpeciesKey.TaxonRow[] carryTaxonRows = (SpeciesKey.TaxonRow[])e.Data.GetData(typeof(SpeciesKey.TaxonRow[]));

            if (carryTaxonRows.Length > 1)
            {
                if (targetTaxonRow.IsHigher) // Multiple species to higher taxon
                {
                    e.Effect = DragDropEffects.Link;
                    notifyInstantly(Resources.Interface.TipInclude,
                        carryTaxonRows.Length == 1 ? carryTaxonRows[0].Name :
                        string.Format(Resources.Interface.TipMultipleSpecies, carryTaxonRows.Length),
                        targetTaxonRow.FullName);
                }
                //else // Multiple species to species
                //{
                //    e.Effect = checkSynonymyAvailability(carryTaxonRows, targetTaxonRow);
                //}
            }
            else
            {
                e.Effect = CheckTaxonCompatibility(carryTaxonRows[0], targetTaxonRow);
            }

            //AutoScroll();
        }

        public void SetParent(SpeciesKey.TaxonRow[] carryTaxonRows, SpeciesKey.TaxonRow targetTaxonRow)
        {
            TreeNode[] tt = Nodes.Find(targetTaxonRow.ID.ToString(), true);
            TreeNode targetNode = tt.Length > 0 ? tt[0] : null;

            foreach (SpeciesKey.TaxonRow carryTaxonRow in carryTaxonRows)
            {
                if (targetTaxonRow == carryTaxonRow) continue; // prevented

                tt = Nodes.Find(carryTaxonRow.ID.ToString(), true);
                TreeNode carryNode = tt.Length > 0 ? tt[0] : null;

                tt = Nodes.Find(carryTaxonRow.TaxonRowParent?.ID.ToString(), true);

                carryTaxonRow.TaxonRowParent = targetTaxonRow;
                if (!carryTaxonRow.IsHigher && !ModifierKeys.HasFlag(Keys.Shift)) // If (sub-)species dropped
                {
                    if (targetTaxonRow.Rank == 91) // If dropped to species
                    {
                        carryTaxonRow.Rank = 92;
                        carryTaxonRow.Name = targetTaxonRow.Name + " " + SpeciesKey.SpecificName(carryTaxonRow.Name);
                    }
                    else if (targetTaxonRow.IsHigher) // If dropped to higher taxon
                    {
                        if (carryTaxonRow.Rank == 92) // If subspecies dropped to higher taxon
                        {
                            carryTaxonRow.Rank = 91; // Reset to species
                            carryTaxonRow.Name = SpeciesKey.Genus(carryTaxonRow.Name) + " " + SpeciesKey.SpecificName(carryTaxonRow.Name);
                        }
                    }
                }

                if (tt.Length > 0) ApplyRenameAscending(tt[0]);

                if (carryNode != null)
                {
                    carryNode.Remove();
                    if (targetNode != null) targetNode.Nodes.Add(getTaxonTreeNode(carryTaxonRow));
                }

                if (taxonChanged != null) taxonChanged.Invoke(this, new TaxonEventArgs(carryTaxonRow));
            }

            if (targetNode != null) ApplyRenameAscending(targetNode);

            SelectedNode = targetNode;

            //if (positionChanged != null) positionChanged.Invoke(this, new EventArgs());
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            if (!AllowEdit) return;

            Point dropLocation = PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = GetNodeAt(dropLocation);
            if (targetNode == null) return;
            SpeciesKey.TaxonRow targetTaxonRow = targetNode.Tag as SpeciesKey.TaxonRow;
            if (targetTaxonRow == null) return;
            SpeciesKey.TaxonRow[] carryTaxonRows = (SpeciesKey.TaxonRow[])e.Data.GetData(typeof(SpeciesKey.TaxonRow[]));

            if (carryTaxonRows.Length > 1)
            {
                SetParent(carryTaxonRows, targetTaxonRow);
                if (Display != null) Display.Message(Resources.Interface.StatusTreeIncluded,
                    string.Format(Resources.Interface.TipMultipleSpecies, carryTaxonRows.Length),
                    targetTaxonRow);
            }
            else
            {
                SpeciesKey.TaxonRow carryTaxonRow = carryTaxonRows[0];

                if (carryTaxonRow.Rank == targetTaxonRow.Rank) // Same rank
                {
                    if (ModifierKeys.HasFlag(Keys.Shift)) // If Shift is pressed - synonimize
                    {
                        SetParent(carryTaxonRows, targetTaxonRow);
                        if (Display != null) Display.Message(Resources.Interface.StatusTreeSynonymized, carryTaxonRow, targetTaxonRow);
                    }
                    else // If Shift is not pressed
                    {
                        if (carryTaxonRow.IsHigher) // Higher taxon positioning
                        {
                            carryTaxonRow.Index = (targetNode.Index + 1) * 10 - 1;
                            Sort();
                            applySort(Nodes);
                            if (Display != null) Display.Message(Resources.Interface.StatusTreeSorted, carryTaxonRow, targetTaxonRow);
                        }
                        else if (carryTaxonRow.Rank == 91) // Species to species subordering
                        {
                            SetParent(carryTaxonRows, targetTaxonRow);
                            if (Display != null) Display.Message(Resources.Interface.StatusTreeSubspecies, carryTaxonRow, targetTaxonRow);
                        }
                    }
                }
                else // Rank are fine
                {
                    SetParent(carryTaxonRows, targetTaxonRow);
                    if (Display != null) Display.Message(Resources.Interface.StatusTreeIncluded, carryTaxonRow, targetTaxonRow);
                }
            }

            if (changed != null) changed.Invoke(this, new EventArgs());
        }



        private void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            if (data == null) return;

            SpeciesKey.TaxonRow[] taxonRows = RootTaxon == null ? data.GetRootTaxonRows() : RootTaxon.GetTaxonRows();

            if (Display != null) Display.SetProgressMaximum(taxonRows.Length);

            int i = 1;
            foreach (SpeciesKey.TaxonRow taxonRow in taxonRows)
            {
                AddNode(taxonRow);
                loader.ReportProgress(i);
                i++;
            }
        }

        private void loader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (Display != null) Display.SetProgress(e.ProgressPercentage);
        }

        private void loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Enabled = true;
            if (Display != null) Display.StopProcessing();
            OnAfterSelect(new TreeViewEventArgs(SelectedNode));
            if (treeLoaded != null) treeLoaded.Invoke(this, new EventArgs());
        }



        private void contextTreeNewTaxon_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow taxonRow = data.Taxon.NewTaxonRow(new TaxonomicRank(61), Resources.Interface.NewTaxon);
            if (pickedTaxon != null) { taxonRow.TaxonRowParent = pickedTaxon; }

            if (sender == contextTaxonAddSpecies) { taxonRow.Rank = 91; }

            EditTaxon editTaxon = new EditTaxon(taxonRow);

            if (editTaxon.ShowDialog(this) == DialogResult.OK)
            {
                data.Taxon.AddTaxonRow(taxonRow);
                TreeNode node = AddNode(taxonRow);
                if (node == null) {
                    OnAfterSelect(new TreeViewEventArgs(SelectedNode));
                    ApplyRenameAscending(SelectedNode);
                } else {
                    SelectedNode = node;
                }

                //listViewRepresence.Groups.Add(getGroup(taxonRow));

                if (changed != null) changed.Invoke(this, new EventArgs());
            }
        }

        private void contextTreeZoomOut_Click(object sender, EventArgs e)
        {
            RootTaxon = null;
            LoadTree();
        }

        private void contextTreeExpand_Click(object sender, EventArgs e)
        {
            ExpandAll();
        }

        private void contextTreeCollapse_Click(object sender, EventArgs e)
        {
            CollapseAll();
        }

        #region Taxon context menu

        private void contextTaxon_Opening(object sender, CancelEventArgs e)
        {
            contextTaxonDepart.Visible = !pickedTaxon.IsTaxIDNull();
            if (!pickedTaxon.IsTaxIDNull()) contextTaxonDepart.Text = string.Format(Resources.Interface.MenuDepart, pickedTaxon.TaxonRowParent);
        }

        private void contextTaxonEdit_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow currentParent = pickedTaxon.TaxonRowParent;
            EditTaxon taxonEdit = new EditTaxon(pickedTaxon);

            if (taxonEdit.ShowDialog(this) == DialogResult.OK)
            {
                if (taxonEdit.IsChanged)
                {
                    TreeNode carryNode = Nodes.Find(taxonEdit.TaxonRow.ID.ToString(), true)?[0];
                    if (currentParent != taxonEdit.TaxonRow.TaxonRowParent)
                    {
                        carryNode.Remove();
                        carryNode = AddNode(taxonEdit.TaxonRow);
                    }
                    else
                    {
                        ApplyRenameAscending(carryNode);
                    }
                    SelectedNode = carryNode;

                    if (changed != null) changed.Invoke(this, new EventArgs());
                    if (taxonChanged != null) taxonChanged.Invoke(this, new TaxonEventArgs(taxonEdit.TaxonRow));
                }
            }
        }

        private void contextTaxonDelete_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow[] descs = pickedTaxon.GetTaxonRows();

            if (descs.Length > 0)
            {
                tdDeleteTaxon.Content = string.Format(
                    Resources.Messages.TaxonDeleteInstruction,
                    pickedTaxon, descs.Length);

                tdbTaxonDeleteParentize.Enabled = pickedTaxon.TaxonRowParent != null;

                TaskDialogButton b = tdDeleteTaxon.ShowDialog(this);

                if (b == tdbTaxonDeleteConfirm)
                {
                    foreach (SpeciesKey.TaxonRow rep in descs)
                    {
                        rep.Delete();
                    }
                }
                else if (b == tdbTaxonDeleteParentize)
                {
                    foreach (SpeciesKey.TaxonRow desc in descs)
                    {
                        desc.TaxonRowParent = pickedTaxon.TaxonRowParent;
                        SelectedNode.Parent.Nodes.Add(getTaxonTreeNode(desc));
                    }
                }
                else if (b == tdbTaxonDeleteOrphanize)
                {
                    foreach (SpeciesKey.TaxonRow desc in descs)
                    {
                        desc.SetTaxIDNull();
                        AddNode(desc);
                    }
                }
                else if (b == tdbTaxonDeleteCancel)
                {
                    return;
                }
            }

            pickedTaxon.Delete();
            pickedTaxon = null;
            SelectedNode.Remove();

            if (changed != null) changed.Invoke(this, new EventArgs());
        }

        private void contextTaxonDepart_Click(object sender, EventArgs e)
        {
            pickedTaxon.SetTaxIDNull();
            SpeciesKey.TaxonRow tr = pickedTaxon;
            SelectedNode.Remove();
            AddNode(tr);

            if (changed != null) changed.Invoke(this, new EventArgs());
        }

        private void contextTaxonExpandAll_Click(object sender, EventArgs e)
        {
            SelectedNode.ExpandAll();
        }

        private void contextTaxonZoomIn_Click(object sender, EventArgs e)
        {
            RootTaxon = PickedTaxon;
            LoadTree();
        }

        #endregion

        #region Species context menu

        private void contextSpecies_Opening(object sender, CancelEventArgs e)
        {
            contextSpeciesDepart.Visible = !pickedTaxon.IsTaxIDNull();
            if (!pickedTaxon.IsTaxIDNull()) contextSpeciesDepart.Text = string.Format(Resources.Interface.MenuDepart, pickedTaxon.TaxonRowParent);
        }

        #endregion
    }
}
