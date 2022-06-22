using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Species
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            treeViewDerivates.Shine();
            listViewRepresence.Shine();
            listViewMinor.Shine();
            treeViewStep.Shine();
            listViewEngagement.Shine();

            treeViewDerivates.TreeViewNodeSorter = 
                new TreeNodeSorter();

            listViewRepresence_SelectedIndexChanged(listViewRepresence, new EventArgs());

            Data = new SpeciesKey();
            FileName = null;

            statusSpecies.ResetFormatted(Constants.Null);
            statusTaxon.ResetFormatted(Constants.Null);

            foreach (Label label in new Label[] {
                labelStpCount, labelEngagedCount, labelPicCount })
            {
                label.UpdateStatus(0);
            }


            tabPageKey.Parent = null;
            tabPagePictures.Parent = null;
        }

        public MainForm(string filename)
            : this()
        {
            loadData(filename);
        }




        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChanged)
            {
                TaskDialogButton b = tdSave.ShowDialog(this);

                if (b == tdbCancelClose)
                {
                    e.Cancel = true;
                }
                else if (b == tdbSave)
                {
                    menuItemSave_Click(sender, e);
                }
            }
        }

        private void tab_Changed(object sender, EventArgs e)
        {
            menuTaxon.Visible = tabControl.SelectedTab == tabPageTaxon;
            menuKey.Visible = tabControl.SelectedTab == tabPageKey;
            menuPictures.Visible = tabControl.SelectedTab == tabPagePictures;
        }



        #region Menus

        #region File menu

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (checkAndSave() != DialogResult.Cancel)
            {
                clear();
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (checkAndSave() == DialogResult.Cancel) return;

                clear();
                loadData(UserSettings.Interface.OpenDialog.FileName);
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
                    save(FileName);
                }
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(UserSettings.Interface.ExportDialog.FileName))
                {
                    case ".sps":
                        save(UserSettings.Interface.ExportDialog.FileName);
                        break;
                    case ".html":
                        File.WriteAllText(UserSettings.Interface.ExportDialog.FileName,
                            Data.Report.ToString(), Encoding.UTF8);
                        break;
                }
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

        #endregion

        #region List menu

        private void menuItemAddTaxon_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow taxonRow = Data.Taxon.NewTaxonRow();
            taxonRow.Rank = 61;
            taxonRow.Taxon = Resources.Interface.NewTaxon;

            EditTaxon editTaxon = new EditTaxon(taxonRow);

            if (editTaxon.ShowDialog(this) == DialogResult.OK)
            {
                IsChanged = true;
                Data.Taxon.AddTaxonRow(taxonRow);
                TreeNode tn = getTaxonTreeNode(taxonRow, true, true);
                treeViewDerivates.Nodes.Add(tn);
                listViewRepresence.Groups.Add(getGroup(taxonRow));
                IsChanged = true;
            }
        }

        private void menuItemAddSpecies_Click(object sender, EventArgs e)
        {
            SpeciesKey.SpeciesRow speciesRow = Data.Species.NewSpeciesRow();
            speciesRow.Species = Resources.Interface.NewSpecies;
            if (IsTaxonNodePicked) {
                speciesRow.TaxonRow = PickedTaxon;
            }

            EditSpecies editSpecies = new EditSpecies(speciesRow);

            if (editSpecies.ShowDialog(this) == DialogResult.OK)
            {
                Data.Species.AddSpeciesRow(speciesRow);

                listViewRepresence.CreateItem(speciesRow);

                ListViewItem item = listViewEngagement.CreateItem(speciesRow);
                item.Group = listViewEngagement.Groups[1];

                IsChanged = true;
            }
        }

        #endregion

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            about.ShowDialog();
        }

        #endregion


        #region Taxonomic tree tab

        #region Taxonomic tree

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool speciesWasPicked = IsSpeciesNodePicked;

            IsTaxonNodePicked = e.Node?.Tag is SpeciesKey.TaxonRow;
            PickedTaxon = IsTaxonNodePicked ? (SpeciesKey.TaxonRow)e.Node?.Tag : null;

            IsSpeciesNodePicked = e.Node?.Tag is SpeciesKey.SpeciesRow;
            PickedSpecies = IsSpeciesNodePicked ? (SpeciesKey.SpeciesRow)e.Node?.Tag : null;

            if (IsTaxonNodePicked) {
                updateRepresenceSpecies(PickedTaxon.GetSpecies());
            } else if (!speciesWasPicked && IsSpeciesNodePicked) {
                updateRepresenceSpecies(Data.GetSorted());
            }
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (IsTaxonNodePicked)
            {
                treeViewDerivates.DoDragDrop(e.Item, DragDropEffects.Move | DragDropEffects.Link);
            }
            else if (IsSpeciesNodePicked)
            {
                treeViewDerivates.DoDragDrop(new SpeciesKey.SpeciesRow[] { PickedSpecies }, DragDropEffects.Link | DragDropEffects.Copy);
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            ((TreeView)sender).Focus();
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode dropNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (dropNode == null) return;

            if (dropNode.Tag is SpeciesKey.TaxonRow dropTaxon) // Target is TaxonRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) // Source is ListView
                {
                    SpeciesKey.SpeciesRow[] carrySpecies = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));
                    e.Effect = DragDropEffects.Link;
                    notifyInstantly(Resources.Interface.TipSpcToTaxInclude,
                        carrySpecies.Length == 1 ? carrySpecies[0].Species : string.Format("{0} species", carrySpecies.Length),
                        dropTaxon.FullName);
                }
                else if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    TreeNode carryNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                    if (carryNode.Tag is SpeciesKey.TaxonRow carryTaxon)
                    {
                        if (carryTaxon == dropTaxon)
                        {
                            e.Effect = DragDropEffects.None;
                        }
                        else
                        {
                            if (carryTaxon.Rank == dropTaxon.Rank) // If drag-and-drop inside one base - then sort
                            {
                                e.Effect = DragDropEffects.Move;
                                notifyInstantly(Resources.Interface.TipTaxToTaxSort, carryTaxon, dropTaxon);
                            }
                            else // If drag-and-drop from one base to another - create derivation
                            {
                                if (carryTaxon.TaxonRowParent == dropTaxon)
                                {
                                    e.Effect = DragDropEffects.None;
                                    notifyInstantly(Resources.Interface.TipTaxToTaxDeriveUnableAlready, carryTaxon, dropTaxon);
                                }
                                else if (carryTaxon.Rank <= dropTaxon.Rank)
                                {
                                    e.Effect = DragDropEffects.None;
                                    notifyInstantly(Resources.Interface.TipTaxToTaxDeriveUnableWrongRank, carryTaxon, dropTaxon);
                                }
                                else
                                {
                                    e.Effect = DragDropEffects.Link;
                                    notifyInstantly(Resources.Interface.TipTaxToTaxDerive, carryTaxon, dropTaxon);
                                }
                            }
                        }
                    }
                }
            }
            else if (dropNode.Tag is SpeciesKey.SpeciesRow dropSpecies) // Target is SpeciesRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) // Source is ListView
                {
                    e.Effect = checkSynonimyAvailability((SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[])), dropSpecies);
                }
                else if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    e.Effect = DragDropEffects.None;
                }
            }              

            (sender as TreeView).AutoScroll();
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode dropNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (dropNode == null) return;

            if (dropNode.Tag is SpeciesKey.TaxonRow dropTaxon) // Target is TaxonRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) // Species-to-taxon
                {
                    SpeciesKey.SpeciesRow[] carrySpecies = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));

                    foreach (SpeciesKey.SpeciesRow speciesRow in carrySpecies)
                    {
                        speciesRow.TaxonRow = dropTaxon;
                        TreeNode carryNode = treeViewDerivates.Nodes.Find("s" + speciesRow.ID.ToString(), true)?[0];
                        if (carryNode.Parent?.Tag is SpeciesKey.TaxonRow) applyRename(carryNode.Parent);
                        carryNode.Remove();
                        dropNode.Nodes.Add(getSpeciesTreeNode(speciesRow));
                        status.Message(Resources.Interface.ResultSpcToTaxIncluded, speciesRow, dropTaxon);
                    }

                    treeViewDerivates.Sort();
                    applyRename(dropNode);
                    applyRename(listViewRepresence.Groups);
                    treeView_AfterSelect(treeViewDerivates, new TreeViewEventArgs(dropNode));

                    IsChanged = true;
                }
                
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    TreeNode carryNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                    if (((TreeNode)e.Data.GetData(typeof(TreeNode))).Tag is SpeciesKey.TaxonRow carryTaxon) // Taxon-to-taxon
                    {
                        if (carryTaxon.Rank == dropTaxon.Rank) // Sort
                        {
                            carryTaxon.Index = (dropNode.Index + 1) * 10 - 1;
                            treeViewDerivates.Sort();
                            applySort(treeViewDerivates.Nodes);
                            status.Message(Resources.Interface.ResultTaxToTaxSorted, carryTaxon, dropTaxon);
                        }
                        else // Derive
                        {
                            carryTaxon.TaxonRowParent = dropTaxon;
                            dropNode.Nodes.Add(getTaxonTreeNode(carryTaxon, true, true));
                            carryNode.Remove();
                            applyRename(dropNode);
                            status.Message(Resources.Interface.ResultTaxToTaxDerived, carryTaxon, dropTaxon);
                        }

                        IsChanged = true;
                    }
                }
            }
            else if (dropNode.Tag is SpeciesKey.SpeciesRow dropSpecies) // target is SpeciesRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) // Species-to-species
                {
                    SpeciesKey.SpeciesRow[] carrySpecies = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));

                    setSynonims(carrySpecies, dropSpecies);
                    IsChanged = true;
                    status.Message(Resources.Interface.ResultSpcToSpcSynonimized, 
                        (carrySpecies.Length == 1 ? carrySpecies[0].Species : string.Format(Resources.Interface.TipSpcMultiple, carrySpecies.Length)), 
                        dropSpecies);
                }
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                e.Node.TreeView.SelectedNode = e.Node;
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (IsTaxonNodePicked)
            {
                contextTaxonEdit_Click(sender, e);
            }
            else if (IsSpeciesNodePicked)
            {
                contextSpeciesEdit_Click(sender, e);
            }
        }


        private void backListLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }


        private void backTreeLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (SpeciesKey.TaxonRow taxonRow in Data.GetRootTaxon()) {
                result.Add(getTaxonTreeNode(taxonRow, true, true));
            }
            foreach (SpeciesKey.SpeciesRow speciesRow in Data.GetOrphans()) {
                result.Add(getSpeciesTreeNode(speciesRow));
            }
            e.Result = result.ToArray();
        }

        private void backTreeLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TreeNode[] results = (TreeNode[])e.Result;
            treeViewDerivates.Nodes.AddRange(results);
            treeViewDerivates.Enabled = true;

            // Tree filled. Now load list
            processDisplay.StopProcessing();
            status.Message(Resources.Interface.StatusLoaded);
            treeView_AfterSelect(treeViewDerivates, new TreeViewEventArgs(treeViewDerivates.SelectedNode));
        }

        #endregion

        #region Represence list

        private void listViewRepresence_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewRepresence.HitTest(e.Location).Item != null)
                {
                    contextSpecies.Show(listViewRepresence, e.Location);
                }
            }
        }

        private void listViewRepresence_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<SpeciesKey.SpeciesRow> speciesRows = new List<SpeciesKey.SpeciesRow>();

            foreach (ListViewItem item in (sender as ListView).SelectedItems)
            {
                speciesRows.Add((SpeciesKey.SpeciesRow)item.Tag);
            }

            listViewRepresence.DoDragDrop(speciesRows.ToArray(), DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void listViewRepresence_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[])))
            {
                ListViewItem hoverItem = listViewRepresence.GetHoveringItem(e.X, e.Y);
                if (hoverItem == null) return;
                SpeciesKey.SpeciesRow hoverSpecies = (SpeciesKey.SpeciesRow)hoverItem.Tag;
                listViewRepresence.SelectedItems.Clear();
                hoverItem.Selected = true;
                if (hoverSpecies == null) return;
                e.Effect = checkSynonimyAvailability((SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[])), hoverSpecies);
            }
        }

        private void listViewRepresence_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) return;

            ListViewItem dropItem = listViewRepresence.GetHoveringItem(e.X, e.Y);

            if (dropItem == null) return;

            SpeciesKey.SpeciesRow dropSpecies = (SpeciesKey.SpeciesRow)dropItem.Tag;
            SpeciesKey.SpeciesRow[] carrySpecies = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));

            setSynonims(carrySpecies, dropSpecies);
            IsChanged = true;
            status.Message(Resources.Interface.ResultSpcToSpcSynonimized,
                (carrySpecies.Length == 1 ? carrySpecies[0].Species : string.Format(Resources.Interface.TipSpcMultiple, carrySpecies.Length)),
                dropSpecies);

            dropItem.Selected = true;
            listViewRepresence_SelectedIndexChanged(sender, e);
        }

        private void listViewRepresence_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewMinor.Items.Clear();

            if (listViewRepresence.SelectedItems.Count > 0)
            {
                SpeciesKey.SpeciesRow majorRow = listViewRepresence.SelectedItems[0].Tag as SpeciesKey.SpeciesRow;

                foreach (SpeciesKey.SpeciesRow speciesRow in majorRow.MinorSynonyms)
                {
                    listViewMinor.CreateItem(speciesRow);
                }
            }

            listViewRepresence.Height = treeViewDerivates.Height - ((listViewMinor.Items.Count == 0) ? 0 : 184);
            listViewMinor.Visible = (listViewMinor.Items.Count > 0);
        }

        private void listViewMinor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listViewMinor.HitTest(e.Location).Item != null)
                {
                    contextSynonym.Show(listViewMinor, e.Location);
                }
            }
        }

        private void contextRemoveSynonym_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewMinor.SelectedItems) {
                Data.Species.FindByID(item.GetID()).SetMajorSynonimIDNull();
            }

            listViewRepresence_SelectedIndexChanged(sender, e);
        }

        private void backSpcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null) { e.Cancel = true; }
            else
            {
                List<ListViewItem> result = new List<ListViewItem>();
                foreach (SpeciesKey.SpeciesRow speciesRow in (SpeciesKey.SpeciesRow[])e.Argument)
                {
                    ListViewItem item = new ListViewItem();
                    item.UpdateItem(speciesRow);
                    item.Group = listViewRepresence.Groups[speciesRow.TaxonRow == null ? "Varia" : speciesRow.TaxonRow.Taxon];
                    result.Add(item);
                }
                e.Result = result.ToArray();
            }
        }

        private void backSpcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                listViewRepresence.Items.Clear();
                ListViewItem[] items = (ListViewItem[])e.Result;
                listViewRepresence.Items.AddRange(items);
            }

            processDisplay.StopProcessing();
            treeViewDerivates.Enabled = true;
        }

        #endregion

        private void contextTreeExpand_Click(object sender, EventArgs e)
        {
            treeViewDerivates.ExpandAll();
        }

        private void contextTreeCollapse_Click(object sender, EventArgs e)
        {
            treeViewDerivates.CollapseAll();
        }

        #region Taxon context menu

        private void contextTaxon_Opening(object sender, CancelEventArgs e)
        {
            contextTaxonDepart.Visible  = !PickedTaxon.IsTaxIDNull();
            if (!PickedTaxon.IsTaxIDNull()) contextTaxonDepart.Text = string.Format(Resources.Interface.MenuDepart, PickedTaxon.TaxonRowParent);
        }

        private void contextTaxonEdit_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow currentParent = PickedTaxon.TaxonRowParent;
            EditTaxon taxonEdit = new EditTaxon(PickedTaxon);

            if (taxonEdit.ShowDialog(this) == DialogResult.OK)
            {
                IsChanged |= taxonEdit.IsChanged;

                TreeNode carryNode = treeViewDerivates.Nodes.Find("t" + taxonEdit.TaxonRow.ID.ToString(), true)?[0];
                if (currentParent != taxonEdit.TaxonRow.TaxonRowParent)
                {
                    carryNode.Remove();
                    if (taxonEdit.TaxonRow.TaxonRowParent == null)
                    {
                        treeViewDerivates.Nodes.Add(getTaxonTreeNode(taxonEdit.TaxonRow, true, true));
                    }
                    else
                    {
                        TreeNode dropNode = treeViewDerivates.Nodes.Find("t" + taxonEdit.TaxonRow.TaxonRowParent.ID.ToString(), true)?[0];
                        dropNode.Nodes.Add(getTaxonTreeNode(taxonEdit.TaxonRow, true, true));
                    }
                }
                else
                {
                    applyRename(carryNode);
                }

                applyRename(listViewRepresence.Groups);
            }
        }

        private void contextTaxonDelete_Click(object sender, EventArgs e)
        {
            SpeciesKey.SpeciesRow[] reps = PickedTaxon.GetSpeciesRows();
            SpeciesKey.TaxonRow[] descs = PickedTaxon.GetTaxonRows();
            if (reps.Length > 0)
            {
                tdDeleteTaxon.Content = string.Format(
                    Resources.Messages.TaxonDeleteInstruction,
                    PickedTaxon, reps.Length, descs.Length);

                tdbDeleteParentize.Enabled = PickedTaxon.TaxonRowParent != null;

                TaskDialogButton b = tdDeleteTaxon.ShowDialog(this);

                if (b == tdbDeleteConfirm)
                {
                    foreach (SpeciesKey.SpeciesRow rep in reps)
                    {
                        rep.Delete();
                    }
                }
                else if (b == tdbDeleteParentize)
                {
                    foreach (SpeciesKey.SpeciesRow rep in reps)
                    {
                        rep.TaxonRow = PickedTaxon.TaxonRowParent;
                        treeViewDerivates.SelectedNode.Parent.Nodes.Add(getSpeciesTreeNode(rep));
                    }

                    foreach (SpeciesKey.TaxonRow desc in descs)
                    {
                        desc.TaxonRowParent = PickedTaxon.TaxonRowParent;
                        treeViewDerivates.SelectedNode.Parent.Nodes.Add(getTaxonTreeNode(desc, true, true));
                    }
                }
                else if (b == tdbDeleteOrphanize)
                {
                    foreach (SpeciesKey.SpeciesRow rep in reps)
                    {
                        rep.SetTaxIDNull();
                        treeViewDerivates.Nodes.Add(getSpeciesTreeNode(rep));
                    }

                    foreach (SpeciesKey.TaxonRow desc in descs)
                    {
                        desc.TaxonRowParent = PickedTaxon.TaxonRowParent;
                        treeViewDerivates.Nodes.Add(getTaxonTreeNode(desc, true, true));
                    }
                }
                else if (b == tdbDeleteCancel)
                {
                    return;
                }
            }

            PickedTaxon.Delete();

            treeViewDerivates.Nodes.Remove(treeViewDerivates.SelectedNode);

            IsChanged = true;
        }

        private void contextTaxonDepart_Click(object sender, EventArgs e)
        {
            PickedTaxon.SetTaxIDNull();
            treeViewDerivates.Nodes.Add(getTaxonTreeNode(PickedTaxon, true, true));
            treeViewDerivates.Nodes.Remove(treeViewDerivates.SelectedNode);

            IsChanged = true;
        }

        private void contextTaxonExpandAll_Click(object sender, EventArgs e)
        {
            treeViewDerivates.SelectedNode.ExpandAll();
        }

        #endregion

        #region Species context menu

        private void contextSpecies_Opening(object sender, CancelEventArgs e)
        {
            contextSpeciesDepart.Visible = !PickedSpecies.IsTaxIDNull();
            if (!PickedSpecies.IsTaxIDNull()) contextSpeciesDepart.Text = string.Format(Resources.Interface.MenuDepart, PickedSpecies.TaxonRow);
        }

        private void contextSpeciesEdit_Click(object sender, EventArgs e)
        {
            if (listViewRepresence.ContainsFocus)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    editSpecies(Data.Species.FindByID(item.GetID()));
                }
            }

            if (treeViewDerivates.ContainsFocus)
            {
                editSpecies(PickedSpecies);
            }
        }

        private void contextSpeciesDelete_Click(object sender, EventArgs e)
        {
            if (listViewRepresence.ContainsFocus)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    ((SpeciesKey.SpeciesRow)item.Tag).Delete();
                    item.Remove();
                }
            }

            if (treeViewDerivates.ContainsFocus)
            {
                PickedSpecies.Delete();
                treeViewDerivates.SelectedNode.Remove();
            }

            IsChanged = true;
        }

        private void contextSpeciesDepart_Click(object sender, EventArgs e)
        {
            if (listViewRepresence.ContainsFocus)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    SpeciesKey.SpeciesRow speciesRow = (SpeciesKey.SpeciesRow)item.Tag;
                    SpeciesKey.TaxonRow taxonRow = treeViewDerivates.SelectedNode.Tag is SpeciesKey.TaxonRow row
                        ? row : (SpeciesKey.TaxonRow)item.Group.Tag;

                    if (taxonRow != null)
                    {
                        speciesRow.SetTaxIDNull();

                        if (IsTaxonNodePicked)
                        {
                            item.Remove();
                        }
                        else
                        {
                            item.Group = null;
                        }
                    }
                }
            }

            if (treeViewDerivates.ContainsFocus)
            {
                PickedSpecies.SetTaxIDNull();
                treeViewDerivates.SelectedNode.Remove();
                treeViewDerivates.Nodes.Add(getSpeciesTreeNode(PickedSpecies));
            }

            IsChanged = true;
        }

        #endregion

        #endregion


        #region Key tab

        #region Context menu

        private void contextStepDelete_Click(object sender, EventArgs e)
        {
            if (IsStepNodeSelected)
            {
                ((SpeciesKey.StepRow)treeViewStep.SelectedNode.Tag).Delete();
                treeViewStep.Nodes.Remove(treeViewStep.SelectedNode);
                IsChanged = true;
            }
        }

        private void contextStepNewFeature_Click(object sender, EventArgs e)
        {
            EditFeature editFeature = new EditFeature(SelectedStep);
            editFeature.Show(this);
        }

        private void contextFeatureEdit_Click(object sender, EventArgs e)
        {
            EditFeature editFeature = new EditFeature(SelectedFeature);
            editFeature.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            //treeViewStep, treeViewStep.SelectedNode.Bounds.Location);
            //editFeature.FormClosed += new FormClosedEventHandler(taxonEdit_FormClosed);
            editFeature.Show();
        }

        private void contextFeatureDelete_Click(object sender, EventArgs e)
        {
            if (IsFeatureNodeSelected)
            {
                ((SpeciesKey.FeatureRow)treeViewStep.SelectedNode.Tag).Delete();
                treeViewStep.Nodes.Remove(treeViewStep.SelectedNode);
                IsChanged = true;
            }
        }

        #endregion

        private void treeViewStep_AfterSelect(object sender, TreeViewEventArgs e)
        {
            rearrangeEnagagedItems(SelectedStep);
        }

        private void treeViewStep_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeViewStep.SelectedNode = e.Node;
            }
        }

        private void treeViewStep_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (IsFeatureNodeSelected)
            {
                contextFeatureEdit_Click(sender, e);
            }

            if (IsStateNodeSelected)
            {
                if (!SelectedState.IsGotoNull())
                {
                    treeViewStep.SelectedNode = treeViewStep.GetNode(SelectedState.Goto.ToString());
                }
                else if (!SelectedState.IsSpcIDNull())
                {
                    editSpecies(SelectedState.SpeciesRow);
                }
            }
        }

        private void listViewEngagement_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEngagement.SelectedItems)
            {
                editSpecies(Data.Species.FindByID(item.GetID()));
            }
        }

        private void buttonTry_Click(object sender, EventArgs e)
        {
            //Species.Display mainForm = new Species.Display(Data, SelectedStep);
            //mainForm.ShowDialog(this);
        }

        #endregion
    }
}