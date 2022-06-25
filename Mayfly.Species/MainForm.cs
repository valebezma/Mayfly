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

            treeViewDerivates.TreeViewNodeSorter = new TreeNodeSorter();

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
            taxonRow.Name = Resources.Interface.NewTaxon;

            EditTaxon editTaxon = new EditTaxon(taxonRow);

            if (editTaxon.ShowDialog(this) == DialogResult.OK)
            {
                IsChanged = true;
                Data.Taxon.AddTaxonRow(taxonRow);
                TreeNode tn = getTaxonTreeNode(taxonRow);
                treeViewDerivates.Nodes.Add(tn);
                listViewRepresence.Groups.Add(getGroup(taxonRow));
                IsChanged = true;
            }
        }

        private void menuItemAddSpecies_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow speciesRow = Data.Taxon.NewTaxonRow();
            speciesRow.Name = Resources.Interface.NewSpecies;
            if (pickedTaxon != null) {
                speciesRow.TaxonRowParent = pickedTaxon;
            }

            EditSpecies editSpecies = new EditSpecies(speciesRow);

            if (editSpecies.ShowDialog(this) == DialogResult.OK)
            {
                Data.Taxon.AddTaxonRow(speciesRow);

                listViewRepresence.CreateItem(speciesRow as SpeciesKey.TaxonRow);

                ListViewItem item = listViewEngagement.CreateItem(speciesRow as SpeciesKey.TaxonRow);
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
            if (e.Node == null) return;

            bool speciesWasPicked = pickedTaxon != null && pickedTaxon.IsSpecies;
            pickedTaxon = e.Node.Tag as SpeciesKey.TaxonRow;

            if (!pickedTaxon.IsSpecies) {
                updateRepresenceSpecies(pickedTaxon.GetSpeciesRows(true));
            } else if (!speciesWasPicked) {
                updateRepresenceSpecies(Data.GetSpeciesRows());
            }
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (treeViewDerivates.SelectedNode != e.Item)
            {
                treeView_AfterSelect(treeViewDerivates, new TreeViewEventArgs((TreeNode)e.Item));
            }
            treeViewDerivates.DoDragDrop(new SpeciesKey.TaxonRow[] { pickedTaxon }, DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            ((TreeView)sender).Focus();
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (targetNode == null) return;
            SpeciesKey.TaxonRow targetTaxon = targetNode.Tag as SpeciesKey.TaxonRow;
            if (targetTaxon == null) return;
            SpeciesKey.TaxonRow[] carryTaxonRows = (SpeciesKey.TaxonRow[])e.Data.GetData(typeof(SpeciesKey.TaxonRow[]));

            if (carryTaxonRows.Length > 1)
            {
                if (targetTaxon.IsSpecies) // Multiple species to species
                {
                    e.Effect = checkSynonymyAvailability(carryTaxonRows, targetTaxon);
                }
                else // Multiple species to higher taxon
                {
                    e.Effect = DragDropEffects.Link;
                    notifyInstantly(Resources.Interface.TipSpcToTaxInclude,
                        carryTaxonRows.Length == 1 ? carryTaxonRows[0].Name : 
                        string.Format(Resources.Interface.TipSpcMultiple, carryTaxonRows.Length),
                        targetTaxon.FullName);
                }
            }
            else
            {
                SpeciesKey.TaxonRow carryTaxonRow = carryTaxonRows[0];

                if (carryTaxonRow == targetTaxon) // Taxon to itself
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    if (carryTaxonRow.IsSpecies)
                    {
                        if (targetTaxon.IsSpecies) // Single species to species
                        {
                            e.Effect = checkSynonymyAvailability(carryTaxonRows, targetTaxon);
                        }
                        else // Single species to higher taxon
                        {
                            if (carryTaxonRow.TaxonRowParent == null || carryTaxonRow.TaxonRowParent != targetTaxon)
                            {
                                e.Effect = DragDropEffects.Link;
                                notifyInstantly(Resources.Interface.TipSpcToTaxInclude, carryTaxonRow, targetTaxon);
                            }
                        }
                    }
                    else
                    {
                        if (carryTaxonRow.Rank == targetTaxon.Rank) // Same rank
                        {
                            e.Effect = DragDropEffects.Move;
                            notifyInstantly(Resources.Interface.TipTaxToTaxSort, carryTaxonRow, targetTaxon);
                        }
                        else
                        {
                            if (carryTaxonRow.TaxonRowParent == targetTaxon) // If already parent
                            {
                                e.Effect = DragDropEffects.None;
                                notifyInstantly(Resources.Interface.TipTaxToTaxDeriveUnableAlready, carryTaxonRow, targetTaxon);
                            }
                            else if (carryTaxonRow.Rank <= targetTaxon.Rank) // If rank mismatch
                            {
                                e.Effect = DragDropEffects.None;
                                notifyInstantly(Resources.Interface.TipTaxToTaxDeriveUnableWrongRank, carryTaxonRow, targetTaxon);
                            }
                            else // Fine conditions
                            {
                                e.Effect = DragDropEffects.Link;
                                notifyInstantly(Resources.Interface.TipTaxToTaxDerive, carryTaxonRow, targetTaxon);
                            }
                        }
                    }
                }
            }

            (sender as TreeView).AutoScroll();
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (targetNode == null) return;
            SpeciesKey.TaxonRow targetTaxonRow = targetNode.Tag as SpeciesKey.TaxonRow;
            if (targetTaxonRow == null) return;
            SpeciesKey.TaxonRow[] carryTaxonRows = (SpeciesKey.TaxonRow[])e.Data.GetData(typeof(SpeciesKey.TaxonRow[]));

            if (carryTaxonRows.Length > 1)
            {
                setParent(carryTaxonRows, targetTaxonRow);
                treeView_AfterSelect(treeViewDerivates, new TreeViewEventArgs(targetNode));
                status.Message((targetTaxonRow.IsSpecies ?
                    Resources.Interface.ResultSpcToSpcSynonymized : Resources.Interface.ResultSpcToTaxIncluded),
                    string.Format(Resources.Interface.TipSpcMultiple, carryTaxonRows.Length),
                    targetTaxonRow);
            }
            else
            {
                SpeciesKey.TaxonRow carryTaxonRow = carryTaxonRows[0];

                if (carryTaxonRow.IsSpecies)
                {
                    setParent(carryTaxonRows, targetTaxonRow);
                    treeViewDerivates.SelectedNode = targetNode;
                    treeView_AfterSelect(treeViewDerivates, new TreeViewEventArgs(targetNode));
                    status.Message((targetTaxonRow.IsSpecies ?
                        Resources.Interface.ResultSpcToSpcSynonymized : Resources.Interface.ResultSpcToTaxIncluded),
                        carryTaxonRow, targetTaxonRow);
                }
                else
                {
                    if (carryTaxonRow.Rank == targetTaxonRow.Rank) // Same rank
                    {
                        carryTaxonRow.Index = (targetNode.Index + 1) * 10 - 1;
                        treeViewDerivates.Sort();
                        applySort(treeViewDerivates.Nodes);
                        status.Message(Resources.Interface.ResultTaxToTaxSorted, carryTaxonRow, targetTaxonRow);
                    }
                    else
                    {
                        carryTaxonRow.TaxonRowParent = targetTaxonRow;
                        treeViewDerivates.Nodes.Find(carryTaxonRow.ID.ToString(), true)?[0].Remove();
                        targetNode.Nodes.Add(getTaxonTreeNode(carryTaxonRow));
                        applyRename(targetNode);
                        status.Message(Resources.Interface.ResultTaxToTaxDerived, carryTaxonRow, targetTaxonRow);
                    }

                }
            }

            IsChanged = true;
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
            if ((e.Node.Tag as SpeciesKey.TaxonRow).IsSpecies) {
                contextSpeciesEdit_Click(sender, e);
            } else {
                contextTaxonEdit_Click(sender, e);
            }
        }


        private void backListLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }


        private void backTreeLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            Data.Read(FileName);

            foreach (SpeciesKey.TaxonRow taxonRow in Data.GetHigherTaxonRows(false))
            {
                addGroup(getGroup(taxonRow));
            }

            addGroup(new ListViewGroup(string.Format(Resources.Interface.VariaCount, Data.GetOrphans().Length))
            {
                Name = "Varia",
                HeaderAlignment = HorizontalAlignment.Center
            });

            foreach (SpeciesKey.TaxonRow taxonRow in Data.GetRootTaxonRows()) 
            {
                addNode(getTaxonTreeNode(taxonRow));
            }

            //foreach (SpeciesKey.TaxonRow speciesRow in Data.GetOrphans())
            //{
            //    result.Add(getTaxonTreeNode(speciesRow, true));
            //}
        }

        private void backTreeLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            treeViewDerivates.Enabled = true;

            processDisplay.StopProcessing();
            status.Message(Resources.Interface.StatusLoaded);

            updateSummary();

            // Tree filled. Now load list
            treeView_AfterSelect(treeViewDerivates, new TreeViewEventArgs(treeViewDerivates.SelectedNode));

            //if (Data.IsKeyAvailable) {
            //    loadKey();
            //} else {
            //    loadEngagedList();
            //}

            IsChanged = false;
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
            List<SpeciesKey.TaxonRow> speciesRows = new List<SpeciesKey.TaxonRow>();

            foreach (ListViewItem item in (sender as ListView).SelectedItems)
            {
                speciesRows.Add((SpeciesKey.TaxonRow)item.Tag);
            }

            listViewRepresence.DoDragDrop(speciesRows.ToArray(), DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void listViewRepresence_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(SpeciesKey.TaxonRow[])))
            {
                ListViewItem hoverItem = listViewRepresence.GetHoveringItem(e.X, e.Y);
                if (hoverItem == null) return;
                SpeciesKey.TaxonRow hoverSpecies = (SpeciesKey.TaxonRow)hoverItem.Tag;
                listViewRepresence.SelectedItems.Clear();
                hoverItem.Selected = true;
                if (hoverSpecies == null) return;
                e.Effect = checkSynonymyAvailability((SpeciesKey.TaxonRow[])e.Data.GetData(typeof(SpeciesKey.TaxonRow[])), hoverSpecies);
            }
        }

        private void listViewRepresence_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(SpeciesKey.TaxonRow[]))) return;

            ListViewItem dropItem = listViewRepresence.GetHoveringItem(e.X, e.Y);

            if (dropItem == null) return;

            SpeciesKey.TaxonRow dropSpecies = (SpeciesKey.TaxonRow)dropItem.Tag;
            SpeciesKey.TaxonRow[] carrySpecies = (SpeciesKey.TaxonRow[])e.Data.GetData(typeof(SpeciesKey.TaxonRow[]));

            setParent(carrySpecies, dropSpecies);
            IsChanged = true;
            status.Message(Resources.Interface.ResultSpcToSpcSynonymized,
                (carrySpecies.Length == 1 ? carrySpecies[0].Name : string.Format(Resources.Interface.TipSpcMultiple, carrySpecies.Length)),
                dropSpecies);

            dropItem.Selected = true;
            listViewRepresence_SelectedIndexChanged(sender, e);
        }

        private void listViewRepresence_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewMinor.Items.Clear();

            if (listViewRepresence.SelectedItems.Count > 0)
            {
                SpeciesKey.TaxonRow majorRow = listViewRepresence.SelectedItems[0].Tag as SpeciesKey.TaxonRow;

                foreach (SpeciesKey.TaxonRow speciesRow in majorRow.MinorSynonyms)
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
                Data.Taxon.FindByID(item.GetID()).SetTaxIDNull();
            }

            listViewRepresence_SelectedIndexChanged(sender, e);
        }

        private void backSpcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null) { e.Cancel = true; }
            else
            {
                List<ListViewItem> result = new List<ListViewItem>();
                foreach (SpeciesKey.TaxonRow speciesRow in (SpeciesKey.TaxonRow[])e.Argument)
                {
                    ListViewItem item = new ListViewItem();
                    item.UpdateItem(speciesRow);
                    item.Group = listViewRepresence.Groups[speciesRow.TaxonRowParent == null ? "Varia" : speciesRow.ValidSpeciesRow.TaxonRowParent.Name];
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
            contextTaxonDepart.Visible  = !pickedTaxon.IsTaxIDNull();
            if (!pickedTaxon.IsTaxIDNull()) contextTaxonDepart.Text = string.Format(Resources.Interface.MenuDepart, pickedTaxon.TaxonRowParent);
        }

        private void contextTaxonEdit_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow currentParent = pickedTaxon.TaxonRowParent;
            EditTaxon taxonEdit = new EditTaxon(pickedTaxon);

            if (taxonEdit.ShowDialog(this) == DialogResult.OK)
            {
                IsChanged |= taxonEdit.IsChanged;

                TreeNode carryNode = treeViewDerivates.Nodes.Find(taxonEdit.TaxonRow.ID.ToString(), true)?[0];
                if (currentParent != taxonEdit.TaxonRow.TaxonRowParent)
                {
                    carryNode.Remove();
                    if (taxonEdit.TaxonRow.TaxonRowParent == null)
                    {
                        treeViewDerivates.Nodes.Add(getTaxonTreeNode(taxonEdit.TaxonRow));
                    }
                    else
                    {
                        TreeNode dropNode = treeViewDerivates.Nodes.Find(taxonEdit.TaxonRow.TaxonRowParent.ID.ToString(), true)?[0];
                        dropNode.Nodes.Add(getTaxonTreeNode(taxonEdit.TaxonRow));
                    }
                }
                else
                {
                    applyRename(carryNode);
                }

                applyRename();
            }
        }

        private void contextTaxonDelete_Click(object sender, EventArgs e)
        {
            SpeciesKey.TaxonRow[] reps = pickedTaxon.GetSpeciesRows(false);
            SpeciesKey.TaxonRow[] descs = pickedTaxon.GetTaxonRows();

            if (reps.Length > 0)
            {
                tdDeleteTaxon.Content = string.Format(
                    Resources.Messages.TaxonDeleteInstruction,
                    pickedTaxon, descs.Length, reps.Length);

                tdbDeleteParentize.Enabled = pickedTaxon.TaxonRowParent != null;

                TaskDialogButton b = tdDeleteTaxon.ShowDialog(this);

                if (b == tdbDeleteConfirm)
                {
                    foreach (SpeciesKey.TaxonRow rep in descs)
                    {
                        rep.Delete();
                    }
                }
                else if (b == tdbDeleteParentize)
                {
                    foreach (SpeciesKey.TaxonRow desc in descs)
                    {
                        desc.TaxonRowParent = pickedTaxon.TaxonRowParent;
                        treeViewDerivates.SelectedNode.Parent.Nodes.Add(getTaxonTreeNode(desc));
                    }
                }
                else if (b == tdbDeleteOrphanize)
                {
                    foreach (SpeciesKey.TaxonRow desc in descs)
                    {
                        desc.TaxonRowParent = pickedTaxon.TaxonRowParent;
                        treeViewDerivates.Nodes.Add(getTaxonTreeNode(desc));
                    }
                }
                else if (b == tdbDeleteCancel)
                {
                    return;
                }
            }

            pickedTaxon.Delete();
            pickedTaxon = null;
            treeViewDerivates.Nodes.Remove(treeViewDerivates.SelectedNode);

            IsChanged = true;
        }

        private void contextTaxonDepart_Click(object sender, EventArgs e)
        {
            pickedTaxon.SetTaxIDNull();
            treeViewDerivates.Nodes.Add(getTaxonTreeNode(pickedTaxon));
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
            contextSpeciesDepart.Visible = pickedTaxon.IsSpecies && !pickedTaxon.IsTaxIDNull();
            if (pickedTaxon.IsSpecies && !pickedTaxon.IsTaxIDNull()) 
                contextSpeciesDepart.Text = string.Format(Resources.Interface.MenuDepart, pickedTaxon.TaxonRowParent);
        }

        private void contextSpeciesEdit_Click(object sender, EventArgs e)
        {
            if (listViewRepresence.ContainsFocus)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    editSpecies(Data.Taxon.FindByID(item.GetID()));
                }
            }

            if (treeViewDerivates.ContainsFocus)
            {
                editSpecies(pickedTaxon);
            }
        }

        private void contextSpeciesDelete_Click(object sender, EventArgs e)
        {
            if (listViewRepresence.ContainsFocus)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    ((SpeciesKey.TaxonRow)item.Tag).Delete();
                    item.Remove();
                }
            }

            if (treeViewDerivates.ContainsFocus)
            {
                pickedTaxon.Delete();
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
                    SpeciesKey.TaxonRow speciesRow = (SpeciesKey.TaxonRow)item.Tag;
                    if (item.Group.Tag != null) {
                        speciesRow.SetTaxIDNull();
                        item.Group = null;
                    }
                }
            }

            if (treeViewDerivates.ContainsFocus)
            {
                pickedTaxon.SetTaxIDNull();
                treeViewDerivates.SelectedNode.Remove();
                treeViewDerivates.Nodes.Add(getTaxonTreeNode(pickedTaxon));
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
                else if (!SelectedState.IsTaxIDNull())
                {
                    if (SelectedState.TaxonRow.IsSpecies)
                    {
                        editSpecies((SpeciesKey.TaxonRow)SelectedState.TaxonRow);
                    }
                }
            }
        }

        private void listViewEngagement_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEngagement.SelectedItems)
            {
                editSpecies(Data.Taxon.FindByID(item.GetID()) as SpeciesKey.TaxonRow);
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