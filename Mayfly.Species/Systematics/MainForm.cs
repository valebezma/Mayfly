﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Software;
using Mayfly.Extensions;
using Mayfly;
using Mayfly.TaskDialogs;
using System.IO;
using Mayfly.Controls;

namespace Mayfly.Species.Systematics
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            treeViewTaxa.Shine();
            treeViewTaxa.TreeViewNodeSorter = new TreeNodeSorter();
            listViewRepresence.Shine();

            listViewMinor.Shine();
            listViewRepresence_SelectedIndexChanged(listViewRepresence, new EventArgs());

            treeViewStep.Shine();
            listViewEngagement.Shine();

            Data = new SpeciesKey();
            FileName = null;

            foreach (Label label in new Label[] {
                labelRepCount, labelTaxCount, labelStpCount,
                labelEngagedCount, labelPicCount, labelMinCount })
            {
                label.UpdateStatus(0);
            }


            tabPageKey.Parent = null;
            tabPagePictures.Parent = null;
        }

        public MainForm(string filename)
            : this()
        {
            LoadData(filename);
        }
   

        private void ClearPictures()
        {

        }




        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSaveChanges.ShowDialog(this);

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
            menuTaxa.Visible = tabControl.SelectedTab == tabPageTaxa;
            menuKey.Visible = tabControl.SelectedTab == tabPageKey;
            menuPictures.Visible = tabControl.SelectedTab == tabPagePictures;
        }



        #region Menus

        #region File menu

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (CheckAndSave() != DialogResult.Cancel)
            {
                Clear();
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
            {
                if (CheckAndSave() == DialogResult.Cancel) return;

                Clear();
                LoadData(UserSettings.Interface.OpenDialog.FileName);
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
            if (UserSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(UserSettings.Interface.ExportDialog.FileName))
                {
                    case ".sps":
                        Save(UserSettings.Interface.ExportDialog.FileName);
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

        private void menuItemAddBase_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(Resources.Interface.NewBase);
            treeViewTaxa.Nodes["RootList"].Nodes.Add(node);
            treeViewTaxa.SelectedNode = node;
            node.ContextMenuStrip = contextBase;
            treeViewTaxa.LabelEdit = true;
            node.BeginEdit();
        }

        private void menuItemAddTaxon_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(Resources.Interface.NewTaxon);
            node.ContextMenuStrip = contextTaxon;

            switch (treeViewTaxa.SelectedNode.Index)
            {
                case 1:
                    treeViewTaxa.SelectedNode.Nodes.Add(node);
                    break;

                case 2:
                    treeViewTaxa.SelectedNode.Parent.Nodes.Add(node);
                    break;
            }

            treeViewTaxa.SelectedNode = node;
            node.BeginEdit();
        }

        private void menuItemAddSpecies_Click(object sender, EventArgs e)
        {
            SpeciesKey.SpeciesRow speciesRow = Data.Species.NewSpeciesRow();
            speciesRow.Species = "Species novus";

            //EditSpecies editSpecies = new EditSpecies(speciesRow);
            AddSpecies editSpecies = new AddSpecies(speciesRow);

            if (editSpecies.ShowDialog(this) == DialogResult.OK)
            {
                Data.Species.AddSpeciesRow(speciesRow);

                listViewRepresence.CreateItem(speciesRow);

                ListViewItem item = listViewEngagement.CreateItem(speciesRow);
                item.Group = speciesRow.GetStateRows().Length == 0 ?
                    listViewEngagement.Groups[1] : listViewEngagement.Groups[0];

                if (IsTaxaNodeSelected)
                {
                    Data.Rep.AddRepRow(SelectedTaxon, speciesRow);
                }

                updateVaria();
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


        #region List tab

        private void contextRename_Click(object sender, EventArgs e)
        {
            treeViewTaxa.SelectedNode.BeginEdit();
        }

        #region Base context menu

        private void contextBaseDelete_Click(object sender, EventArgs e)
        {
            if (IsBaseNodeSelected)
            {
                if (treeViewTaxa.SelectedNode.Tag != null)
                {
                    ((SpeciesKey.BaseRow)treeViewTaxa.SelectedNode.Tag).Delete();
                }
                treeViewTaxa.Nodes.Remove(treeViewTaxa.SelectedNode);
                IsChanged = true;
            }
        }

        #endregion

        #region Taxon context menu

        private void contextTaxon_Opening(object sender, CancelEventArgs e)
        {
            contextTaxonDelete.Enabled =
                contextTaxonEdit.Enabled =
                contextTaxonAddSpecies.Enabled =
                SelectedTaxon != null;

            contextTaxonRename.Visible = treeViewTaxa.LabelEdit;
            contextTaxonDelete.Visible = IsTaxaNodeSelected && 
                treeViewTaxa.SelectedNode.Parent.Tag != null;
        }

        private void contextTaxonEdit_Click(object sender, EventArgs e)
        {
            EditTaxon taxonEdit = new EditTaxon(SelectedTaxon);
            if (taxonEdit.ShowDialog(this) == DialogResult.OK)
            {
                IsChanged |= taxonEdit.IsChanged;
                treeViewTaxa.SelectedNode.Text = taxonEdit.TaxonRow.TaxonName;
            }
        }

        private void contextTaxonDelete_Click(object sender, EventArgs e)
        {
            if (IsTaxaNodeSelected)
            {
                if (treeViewTaxa.SelectedNode.Parent.Tag is SpeciesKey.TaxaRow tr)
                {
                    // ASk for reassigning representatives to higher taxon?

                    // Remove derivation
                    Data.Derivation.FindByMajTaxIDMinTaxID(tr.ID, SelectedTaxon.ID).Delete();
                    treeViewTaxa.Nodes.Remove(treeViewTaxa.SelectedNode);
                }
                else if (treeViewTaxa.SelectedNode.Parent.Tag is SpeciesKey.BaseRow)
                {
                    // Remove taxon itself
                    ((SpeciesKey.TaxaRow)treeViewTaxa.SelectedNode.Tag).Delete();
                    treeViewTaxa.Nodes.Remove(treeViewTaxa.SelectedNode);
                    updateTree();
                }

                updateVaria();
                IsChanged = true;
            }
        }

        #endregion

        #region Species context menu

        private void contextSpeciesEdit_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewRepresence.SelectedItems)
            {
                RunSpeciesEditing(Data.Species.FindByID(item.GetID()));
            }
        }

        private void contextSpeciesDelete_Click(object sender, EventArgs e)
        {
            if (IsAllSpeciesShown || treeViewTaxa.SelectedNode == null)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    ((SpeciesKey.SpeciesRow)item.Tag).Delete();
                    item.Remove();
                }
            }

            if (IsTaxaNodeSelected || IsBaseNodeSelected)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    SpeciesKey.SpeciesRow speciesRow = (SpeciesKey.SpeciesRow)item.Tag;

                    SpeciesKey.TaxaRow taxaRow = treeViewTaxa.SelectedNode.Tag is SpeciesKey.TaxaRow row
                        ? row
                        : (SpeciesKey.TaxaRow)item.Group.Tag;

                    taskDialogDeleteSpecies.Content = string.Format(Resources.Messages.DeleteSpeciesContent,
                        speciesRow.Species, taxaRow.TaxonName);

                    TaskDialogButton b = taskDialogDeleteSpecies.ShowDialog(this);

                    if (b == tdbDeleteSpc)
                    {
                        ((SpeciesKey.SpeciesRow)item.Tag).Delete();
                        item.Remove();
                    }
                    else if (b == tdbDeleteRep)
                    {
                        speciesRow.RemoveFrom(taxaRow);
                        item.Remove();
                    }
                    else if (b == tdbDeleteCancel)
                    {
                        continue;
                    }
                }
            }

            IsChanged = true;
            updateVaria();
        }

        #endregion

        #region Taxa tree

        private void treeViewTaxa_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IsAllSpeciesShown = treeViewTaxa.SelectedNode?.Name == "RootAll";
            IsBaseNodeSelected = treeViewTaxa.SelectedNode?.Tag is SpeciesKey.BaseRow;
            IsTaxaNodeSelected = treeViewTaxa.SelectedNode?.Tag is SpeciesKey.TaxaRow;

            treeViewTaxa.LabelEdit = 
            menuItemAddTaxon.Enabled = IsBaseNodeSelected || (IsTaxaNodeSelected && treeViewTaxa.SelectedNode?.Parent.Tag is SpeciesKey.BaseRow);

            SelectedTaxon = (IsTaxaNodeSelected ? (SpeciesKey.TaxaRow)treeViewTaxa.SelectedNode.Tag : null);
            SelectedBase = (IsBaseNodeSelected ? (SpeciesKey.BaseRow)treeViewTaxa.SelectedNode.Tag : 
                (IsTaxaNodeSelected ? SelectedTaxon.BaseRow :
                IsAllSpeciesShown ? null : (SpeciesKey.BaseRow)treeViewTaxa.SelectedNode.Parent?.Tag));

            UpdateRepresence();
        }

        private void treeViewTaxa_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                switch (e.Node.Level)
                {
                    case 0: // Node is AllSpecies
                        e.CancelEdit = true;
                        break;

                    case 1: // Base level
                        if (e.Node.Tag == null) // Node is just created
                        {
                            e.Node.Tag = Data.Base.AddBaseRow(e.Node.Index * 10, e.Label, e.Label);
                            updateVaria();
                        }
                        else // renaming existing base
                        {
                            ((SpeciesKey.BaseRow)e.Node.Tag).Base = e.Label;
                            updateTree();
                        }
                        
                        IsChanged = true;
                        break;

                    case 2: // Taxon level
                        if (e.Node.Tag == null) // Node is just created
                        {
                            e.Node.Tag = Data.Taxa.AddTaxaRow(SelectedBase, e.Node.Index * 10, e.Label, e.Label, null);
                        }
                        else // renaming existing taxon
                        {
                            ((SpeciesKey.TaxaRow)e.Node.Tag).Taxon = e.Label;
                            updateTree();
                        }

                        IsChanged = true;
                        break;
                }
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private void treeViewTaxa_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (IsBaseNodeSelected)
            {
                treeViewTaxa.DoDragDrop(e.Item, DragDropEffects.Move);
            }

            if (IsTaxaNodeSelected)
            {
                treeViewTaxa.DoDragDrop(e.Item, DragDropEffects.Move | DragDropEffects.Link);
            }

            //if (IsTaxaNodeSelected)
            //{
            //    List<SpeciesKey.SpeciesRow> speciesRows = new List<SpeciesKey.SpeciesRow>();
            //    speciesRows.AddRange(SelectedTaxon == null ? SelectedBase.Varia : SelectedTaxon.GetSpecies());
            //    treeViewTaxa.DoDragDrop(speciesRows.ToArray(), DragDropEffects.Link | DragDropEffects.Copy);
            //}
        }

        private void treeViewTaxa_DragEnter(object sender, DragEventArgs e)
        {
            treeViewTaxa.Focus();
        }

        private void treeViewTaxa_DragOver(object sender, DragEventArgs e)
        {
            status.StatusLog.Text = string.Empty;
            TreeNode hoverNode = treeViewTaxa.GetHoveringNode(e.X, e.Y);

            if (hoverNode == null) return;

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode carryNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                if (hoverNode.Tag is SpeciesKey.BaseRow hoverBase && carryNode.Tag is SpeciesKey.BaseRow carryBase)
                {
                    bool forbidden = Data.Derivation.HasDerivations(carryBase);
                    e.Effect = forbidden ? DragDropEffects.None : DragDropEffects.Move;
                    status.StatusLog.Text = string.Format(forbidden ? "Sort unable because {0} has connected members already" : "Set {0} higher rank over {1}", carryNode.Text, hoverNode.Text);
                }

                if (hoverNode.Tag is SpeciesKey.TaxaRow hoverTaxa && carryNode.Tag is SpeciesKey.TaxaRow carryTaxa)
                {
                    if (carryNode.Parent.Tag is SpeciesKey.BaseRow && carryTaxa.BaseRow == hoverTaxa.BaseRow)
                    {
                        e.Effect = DragDropEffects.Move;
                        status.StatusLog.Text = string.Format("Set {0} higher position over {1}", carryTaxa.FullName, hoverTaxa.FullName);
                    }
                    else
                    {
                        if (hoverTaxa.Includes(carryTaxa))
                        {
                            e.Effect = DragDropEffects.None;
                            status.StatusLog.Text = string.Format("{0} is already derivated from {1}", carryTaxa.FullName, hoverTaxa.FullName);
                        }
                        else if (hoverTaxa.BaseRow == carryTaxa.BaseRow)
                        {
                            e.Effect = DragDropEffects.None;
                            status.StatusLog.Text = string.Format("{0} and {1} are taxa of same rank", carryTaxa.FullName, hoverTaxa.FullName);
                        }
                        else if (carryTaxa.BaseRow.Index <= hoverTaxa.BaseRow.Index)
                        {
                            e.Effect = DragDropEffects.None;
                            status.StatusLog.Text = string.Format("{0} has higher rank than {1}", carryTaxa.FullName, hoverTaxa.FullName);
                        }
                        else
                        {
                            e.Effect = DragDropEffects.Link;
                            status.StatusLog.Text = string.Format("You are going to derivate {0} from {1}", carryTaxa.FullName, hoverTaxa.FullName);
                        }
                    }
                }
            }

            if (hoverNode.Tag is SpeciesKey.TaxaRow destTaxon && e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[])))
            {
                SpeciesKey.SpeciesRow[] speciesRows = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));

                if (speciesRows.Length > 0)
                {
                    SpeciesKey.SpeciesRow speciesRow = speciesRows[0];

                    if (hoverNode.Tag != null)
                    {
                        SpeciesKey.TaxaRow taxaRow = speciesRow.GetTaxon(destTaxon.BaseRow);

                        treeViewTaxa.SelectedNode = hoverNode;

                        if (taxaRow == null)
                        {
                            e.Effect = DragDropEffects.Link;
                            status.StatusLog.Text = string.Format(Resources.Messages.AssociateTip,
                                speciesRow.Species, destTaxon.FullName);
                        }
                        else
                        {
                            e.Effect = DragDropEffects.Copy;
                            status.StatusLog.Text = string.Format(Resources.Messages.ReassociateTip,
                                speciesRow.Species, taxaRow.FullName, destTaxon.FullName);
                        }
                    }
                }
            }

            treeViewTaxa.AutoScroll();
        }

        private void treeViewTaxa_DragDrop(object sender, DragEventArgs e)
        {
            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode dropNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (dropNode == null) return;

            if (dropNode.Tag is SpeciesKey.TaxaRow destinationTaxon && e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[])))
            {
                foreach (SpeciesKey.SpeciesRow speciesRow in
                    (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[])))
                {
                    SpeciesKey.TaxaRow associatedTaxon = speciesRow.GetTaxon(destinationTaxon.BaseRow);

                    if (associatedTaxon == null)
                    {
                        Data.Rep.AddRepRow(destinationTaxon, speciesRow);

                        foreach (ListViewItem item in dragItems)
                        {
                            listViewRepresence.Items.Remove(item);
                        }
                        dragItems.Clear();

                        IsChanged = true;
                    }
                    else
                    {
                        if (associatedTaxon != destinationTaxon)
                        {
                            taskDialogReassociateSpecies.Content = string.Format(
                                Resources.Messages.SpeciesReassociateContent,
                                speciesRow.Species, associatedTaxon.TaxonName, destinationTaxon.TaxonName);

                            TaskDialogButton b = taskDialogReassociateSpecies.ShowDialog(this);

                            if (b == tdbReassSpecies)
                            {
                                foreach (SpeciesKey.RepRow RepRow in speciesRow.GetRepRows())
                                {
                                    if (RepRow.TaxaRow == associatedTaxon)
                                    {
                                        RepRow.TaxaRow = destinationTaxon;
                                    }
                                }

                                foreach (ListViewItem item in dragItems)
                                {
                                    listViewRepresence.Items.Remove(item);
                                }
                                dragItems.Clear();

                                IsChanged = true;

                                //UpdateSpeciesItems(speciesRow);

                                status.Message(string.Format(Resources.Messages.SpeciesReassociated,
                                    speciesRow.Species, associatedTaxon.TaxonName, destinationTaxon.TaxonName));
                            }
                        }
                    }
                }

                updateVaria();

                IsChanged = true;
            }

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                if (dropNode.Tag is SpeciesKey.BaseRow && ((TreeNode)e.Data.GetData(typeof(TreeNode))).Tag is SpeciesKey.BaseRow baseRow)
                {
                    baseRow.Index = dropNode.Index * 10 - 1;
                    IsChanged = true;
                    treeViewTaxa.Sort();

                    foreach (TreeNode tn in treeViewTaxa.Nodes)
                    {
                        if (tn.Tag is SpeciesKey.BaseRow br)
                        {
                            br.Index = tn.Index * 10;
                        }
                    }
                }

                if (dropNode.Tag is SpeciesKey.TaxaRow destTaxon && ((TreeNode)e.Data.GetData(typeof(TreeNode))).Tag is SpeciesKey.TaxaRow taxaRow)
                {
                    if (taxaRow.BaseRow == destTaxon.BaseRow) // If drag-and-drop inside one base - then sort
                    {
                        // Sort
                        taxaRow.Index = dropNode.Index * 10 - 1;
                        IsChanged = true;
                        treeViewTaxa.Sort();

                        foreach (TreeNode tn in dropNode.Parent.Nodes)
                        {
                            if (tn.Tag is SpeciesKey.TaxaRow tr)
                            {
                                tr.Index = tn.Index * 10;
                            }
                        }
                    }
                    else // If drag-and-drop from one base to another - create derivation
                    {
                        // Create taxon derivation
                        Data.Derivation.AddDerivationRow(destTaxon, taxaRow);

                        foreach (SpeciesKey.RepRow repRow in taxaRow.GetRepRows())
                        {
                            // if destination taxon directly contains such representative
                            if (destTaxon.Includes(repRow.SpeciesRow, false))
                            {
                                // remove it, because such representation is now provided with newly created derivation
                                repRow.SpeciesRow.RemoveFrom(destTaxon);
                            }
                        }

                        updateVaria();
                        updateTree();
                        IsChanged = true;

                        status.Message(string.Format("{0} is now derivated from {1}", taxaRow.FullName, destTaxon.FullName));
                    }
                }
            }
        }

        private void treeViewTaxa_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeViewTaxa.SelectedNode = e.Node;
            }
        }

        private void treeViewTaxa_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (IsBaseNodeSelected)
            {
                contextRename_Click(sender, e);
            }

            if (IsTaxaNodeSelected)
            {
                contextTaxonEdit_Click(sender, e);
            }
        }

        #endregion

        private void backTreeLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TreeNode> result = new List<TreeNode>();

            foreach (SpeciesKey.BaseRow baseRow in ((SpeciesKey.BaseRow[])Data.Base.Select(null, "Index ASC")))
            {
                TreeNode baseNode = getBaseTreeNode(baseRow);
                result.Add(baseNode);

                foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows())
                {
                    baseNode.Nodes.Add(getTaxonTreeNode(taxaRow, false));
                }
            }

            e.Result = result.ToArray();
        }

        private void backTreeLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TreeNode[] results = (TreeNode[])e.Result;
            treeViewTaxa.Nodes["rootList"].Nodes.AddRange(results);
            labelTaxCount.UpdateStatus(Data.Taxa.Count);
            updateVaria();
            updateTree();
            treeViewTaxa.Enabled = true;
        }

        #region Represence list view

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

                if (hoverItem != null)
                {
                    SpeciesKey.SpeciesRow speciesRow = 
                        ((SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[])))[0];

                    if (hoverItem.Tag != null)
                    {
                        SpeciesKey.SpeciesRow destSpeciesRow = (SpeciesKey.SpeciesRow)hoverItem.Tag;

                        listViewRepresence.SelectedItems.Clear();
                        //hoverItem.Selected = true;

                        if (destSpeciesRow == speciesRow)
                        {
                            e.Effect = DragDropEffects.None;
                        }

                        if (destSpeciesRow.MinorSynonyms.Contains(speciesRow))
                        {
                            e.Effect = DragDropEffects.None;
                        }

                        if (destSpeciesRow != null)
                        {
                            e.Effect = DragDropEffects.Link;
                            status.StatusLog.Text = string.Format(Resources.Messages.AssociateTip,
                                speciesRow.Species, destSpeciesRow.Species);
                        }
                    }
                }
            }
        }

        private void listViewRepresence_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) return;

            ListViewItem dropItem = listViewRepresence.GetHoveringItem(e.X, e.Y);

            if (dropItem == null) return;

            SpeciesKey.SpeciesRow destSpeciesRow = (SpeciesKey.SpeciesRow)dropItem.Tag;

            foreach (SpeciesKey.SpeciesRow speciesRow in (SpeciesKey.SpeciesRow[])
                e.Data.GetData(typeof(SpeciesKey.SpeciesRow[])))
            {
                if (destSpeciesRow != speciesRow)
                {
                    if (!destSpeciesRow.MinorSynonyms.Contains(speciesRow))
                    {
                        if (Data.Synonymy.FindByMajIDMinID(speciesRow.ID, destSpeciesRow.ID) == null)
                        {
                            if (Data.Synonymy.FindByMajIDMinID(destSpeciesRow.ID, speciesRow.ID) == null)
                            {
                                Data.Synonymy.AddSynonymyRow(destSpeciesRow, speciesRow);
                                IsChanged = true;
                            }
                        }
                    }
                }
            }

            dropItem.Selected = true;
            listViewRepresence_SelectedIndexChanged(sender, e);
            //UpdateSynonyms();
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

            labelMinCount.UpdateStatus(listViewMinor.Items.Count);
            
            listViewRepresence.Height = tabPageTaxa.Height - ((listViewMinor.Items.Count == 0) ? 156 : 328);
            labelMinCount.Visible = listViewMinor.Visible = 
                (listViewMinor.Items.Count > 0);
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
            foreach (ListViewItem item in listViewMinor.SelectedItems)
            {
                SpeciesKey.SynonymyRow synRow = Data.Synonymy.FindByMajIDMinID(
                    listViewRepresence.GetID(), item.GetID());
                Data.Synonymy.RemoveSynonymyRow(synRow);
            }

            listViewRepresence_SelectedIndexChanged(sender, e);
        }

        #endregion

        private void backSpcLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            SpeciesKey.SpeciesRow[] speciesRows = (SpeciesKey.SpeciesRow[])e.Argument;
            List<ListViewItem> result = new List<ListViewItem>();

            if (SelectedBase != null)
            {
                List<SpeciesKey.SpeciesRow> eliminationList = new List<SpeciesKey.SpeciesRow>();
                eliminationList.AddRange(speciesRows);

                foreach (SpeciesKey.TaxaRow taxaRow in SelectedBase.GetTaxaRows())
                {
                    foreach (SpeciesKey.SpeciesRow speciesRow in taxaRow.GetSpecies())
                    {
                        if (!eliminationList.Contains(speciesRow)) continue;

                        ListViewItem item = new ListViewItem();
                        item.UpdateItem(speciesRow);
                        item.Tag = speciesRow;
                        item.Group = listViewRepresence.Groups[taxaRow.TaxonName];

                        result.Add(item);
                        eliminationList.Remove(speciesRow);
                    }
                }

                foreach (SpeciesKey.SpeciesRow speciesRow in eliminationList)
                {
                    ListViewItem item = new ListViewItem();
                    item.UpdateItem(speciesRow);
                    item.Tag = speciesRow;
                    result.Add(item);
                    item.Group = listViewRepresence.Groups["Varia"];
                }
            }
            else
            {
                foreach (SpeciesKey.SpeciesRow speciesRow in speciesRows)
                {
                    ListViewItem item = new ListViewItem();
                    item.UpdateItem(speciesRow);
                    item.Tag = speciesRow;
                    result.Add(item);
                }
            }

            e.Result = result.ToArray();
        }

        private void backSpcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                listViewRepresence.Items.Clear();

                ListViewItem[] items = (ListViewItem[])e.Result;
                listViewRepresence.Items.AddRange(items);

                labelRepCount.UpdateStatus(items.Length);
            }

            treeViewTaxa.Enabled = true;
        }

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
            RearrangeEnagagedItems(SelectedStep);
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
                    RunSpeciesEditing(SelectedState.SpeciesRow);
                }
            }
        }

        private void listViewEngagement_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEngagement.SelectedItems)
            {
                RunSpeciesEditing(Data.Species.FindByID(item.GetID()));
            }
        }

        private void buttonTry_Click(object sender, EventArgs e)
        {
            Species.MainForm mainForm = new Species.MainForm(Data, SelectedStep);
            mainForm.ShowDialog(this);
        }

        #endregion
    }
}