using System;
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
            treeViewDerivates.Shine();
            listViewRepresence.Shine();
            listViewMinor.Shine();
            treeViewStep.Shine();
            listViewEngagement.Shine();

            treeViewTaxa.TreeViewNodeSorter = 
                treeViewDerivates.TreeViewNodeSorter = 
                new TreeNodeSorter();

            listViewRepresence_SelectedIndexChanged(listViewRepresence, new EventArgs());

            //treeViewTaxa.BackColor =
            //    treeViewDerivates.BackColor =
            //    listViewEngagement.BackColor =
            //    listViewRepresence.BackColor =
            //    tabPageTaxa.BackColor;

            Data = new SpeciesKey();
            FileName = null;

            statusSpecies.ResetFormatted(Constants.Null);
            statusTaxa.ResetFormatted(Constants.Null);

            foreach (Label label in new Label[] {
                /*labelRepCount, labelTaxCount,*/ labelStpCount,
                labelEngagedCount, labelPicCount/*, labelMinCount*/ })
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

        private void menuTaxa_DropDownOpening(object sender, EventArgs e)
        {
            menuItemAddTaxon.Enabled = clickedTreeview == treeViewTaxa;
        }

        private void menuItemAddBase_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(Resources.Interface.NewBase);
            treeViewTaxa.Nodes.Add(node);
            treeViewTaxa.SelectedNode = node;
            node.ContextMenuStrip = contextBase;
            node.BeginEdit();
        }

        private void menuItemAddTaxon_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode(String.Format(Resources.Interface.NewTaxon, PickedBase.BaseName));
            node.ContextMenuStrip = contextTaxon;

            switch (treeViewTaxa.SelectedNode.Level)
            {
                case 0:
                    treeViewTaxa.SelectedNode.Nodes.Add(node);
                    break;

                case 1:
                    treeViewTaxa.SelectedNode.Parent.Nodes.Add(node);
                    break;
            }

            treeViewTaxa.SelectedNode = node;
            treeViewTaxa.Focus();
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

                if (IsTaxaNodePicked)
                {
                    Data.Rep.AddRepRow(PickedTaxon, speciesRow);
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


        #region Taxonomic tree tab

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            IsBaseNodePicked = e.Node?.Tag is SpeciesKey.BaseRow;
            PickedBase = IsBaseNodePicked ? (SpeciesKey.BaseRow)e.Node?.Tag : null;

            IsTaxaNodePicked = e.Node?.Tag is SpeciesKey.TaxaRow;
            PickedTaxon = IsTaxaNodePicked ? (SpeciesKey.TaxaRow)e.Node?.Tag : null;

            IsSpeciesNodePicked = e.Node?.Tag is SpeciesKey.SpeciesRow;
            PickedSpecies = IsSpeciesNodePicked ? (SpeciesKey.SpeciesRow)e.Node?.Tag : null;

            updateRepresence();
        }

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (IsBaseNodePicked)
            {
                clickedTreeview.DoDragDrop(PickedBase, DragDropEffects.Move);
            }
            else if (IsTaxaNodePicked)
            {
                clickedTreeview.DoDragDrop(PickedTaxon, DragDropEffects.Move | DragDropEffects.Link);
            }
            else if (IsSpeciesNodePicked)
            {
                clickedTreeview.DoDragDrop(new SpeciesKey.SpeciesRow[] { PickedSpecies }, DragDropEffects.Link | DragDropEffects.Copy);
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            ((TreeView)sender).Focus();
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            status.StatusLog.Visible = true;

            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode dropNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (dropNode == null) return;

            if (dropNode.Tag is SpeciesKey.BaseRow destBase) // target is BaseRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.BaseRow))) // Sort
                {
                    SpeciesKey.BaseRow carryBase = (SpeciesKey.BaseRow)e.Data.GetData(typeof(SpeciesKey.BaseRow));

                    if (carryBase == destBase) return;

                    bool forbidden = carryBase.HasDerivations;
                    e.Effect = forbidden ? DragDropEffects.None : DragDropEffects.Move;
                    status.StatusLog.Text = string.Format(forbidden ? 
                        "Sort unable because {0} has connected members already" : "Set {0} higher rank over {1}", carryBase.BaseName, destBase.BaseName);

                }
            }

            if (dropNode.Tag is SpeciesKey.TaxaRow destTaxon) // target is TaxaRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) // Associate or reassociate species to taxa
                {
                    SpeciesKey.SpeciesRow[] carrySpecies = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));

                    if (carrySpecies.Length > 0)
                    {
                        SpeciesKey.SpeciesRow speciesRow = carrySpecies[0];

                        if (dropNode.Tag != null)
                        {
                            SpeciesKey.TaxaRow taxaRow = speciesRow.GetTaxon(destTaxon.BaseRow);

                            (sender as TreeView).SelectedNode = dropNode;

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
                else if (e.Data.GetDataPresent(typeof(SpeciesKey.TaxaRow))) // Taxa-to-taxa action needed
                {
                    SpeciesKey.TaxaRow carryTaxa = (SpeciesKey.TaxaRow)e.Data.GetData(typeof(SpeciesKey.TaxaRow));

                    if (carryTaxa == destTaxon)
                    {
                        e.Effect = DragDropEffects.None;
                        status.StatusLog.Text = string.Empty;
                    }
                    else
                    {
                        if (carryTaxa.BaseRow == destTaxon.BaseRow) // If drag-and-drop inside one base - then sort
                        {
                            e.Effect = DragDropEffects.Move;
                            status.StatusLog.Text = string.Format("Set {0} higher position over {1}", carryTaxa.FullName, destTaxon.FullName);
                        }
                        else // If drag-and-drop from one base to another - create derivation
                        {
                            if (destTaxon.Includes(carryTaxa))
                            {
                                e.Effect = DragDropEffects.None;
                                status.StatusLog.Text = string.Format("{0} is already derivated from {1}", carryTaxa.FullName, destTaxon.FullName);
                            }
                            else if (destTaxon.BaseRow == carryTaxa.BaseRow)
                            {
                                e.Effect = DragDropEffects.None;
                                status.StatusLog.Text = string.Format("{0} and {1} are taxa of same rank", carryTaxa.FullName, destTaxon.FullName);
                            }
                            else if (carryTaxa.BaseRow.Index <= destTaxon.BaseRow.Index)
                            {
                                e.Effect = DragDropEffects.None;
                                status.StatusLog.Text = string.Format("{0} has higher rank than {1}", carryTaxa.FullName, destTaxon.FullName);
                            }
                            else
                            {
                                e.Effect = DragDropEffects.Link;
                                status.StatusLog.Text = string.Format("You are going to derivate {0} from {1}", carryTaxa.FullName, destTaxon.FullName);
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
            TreeNode dropNode = (sender as TreeView).GetNodeAt(dropLocation);
            if (dropNode == null) return;

            if (dropNode.Tag is SpeciesKey.BaseRow destBase) // target is BaseRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.BaseRow))) // Sort
                {
                    SpeciesKey.BaseRow carryBase = (SpeciesKey.BaseRow)e.Data.GetData(typeof(SpeciesKey.BaseRow));

                    carryBase.Index = (dropNode.Index + 1) * 10 - 1;
                    treeViewTaxa.Sort();

                    foreach (TreeNode tn in treeViewTaxa.Nodes){
                        if (tn.Tag is SpeciesKey.BaseRow br){
                            br.Index = (tn.Index + 1) * 10;
                        }
                    }

                    treeViewDerivates.Sort();
                    status.Message("Rank of {0} is now higher than that of {1}", carryBase.BaseName, destBase.BaseName);
                    IsChanged = true;
                }
            }

            if (dropNode.Tag is SpeciesKey.TaxaRow destTaxon) // target is TaxaRow
            {
                if (e.Data.GetDataPresent(typeof(SpeciesKey.SpeciesRow[]))) // Associate or reassociate species to taxa
                {
                    SpeciesKey.SpeciesRow[] carrySpecies = (SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[]));

                    TaskDialogButton b = null;
                    foreach (SpeciesKey.SpeciesRow speciesRow in carrySpecies)
                    {
                        SpeciesKey.RepRow[] currentTaxation = speciesRow.GetRepRows();

                        tdSpeciesRepresence.Content = string.Format(
                            Resources.Messages.SpeciesRepContent,
                            speciesRow.Species, destTaxon.TaxonName);

                        tdSpeciesRepresence.VerificationText = carrySpecies.Length > 1 ? "Remember for all" : String.Empty;

                        if (currentTaxation.Length > 0 && (b == null || !tdSpeciesRepresence.IsVerificationChecked)) b = tdSpeciesRepresence.ShowDialog(this);

                        if (currentTaxation.Length == 0 || b == tdbRepSpecies)
                        {
                            while (speciesRow.GetRepRows().Length > 0)
                            {
                                Data.Rep.RemoveRepRow(speciesRow.GetRepRows()[0]);
                            }

                            Data.Rep.AddRepRow(destTaxon, speciesRow);

                            IsChanged = true;
                            status.Message(string.Format(Resources.Messages.SpeciesRep, speciesRow.Species, destTaxon.TaxonName));
                        }
                    }
                    tdSpeciesRepresence.IsVerificationChecked = false;
                    updateVaria();
                    IsChanged = true;
                }
                else if (e.Data.GetDataPresent(typeof(SpeciesKey.TaxaRow))) // Taxa-to-taxa action needed
                {
                    SpeciesKey.TaxaRow carryTaxa = (SpeciesKey.TaxaRow)e.Data.GetData(typeof(SpeciesKey.TaxaRow));

                    if (carryTaxa.BaseRow == destTaxon.BaseRow) // If drag-and-drop inside one base - then sort
                    {
                        // Sort
                        carryTaxa.Index = (dropNode.Index + 1) * 10 - 1;

                        // Further work properly in taxa tree ONLY!
                        clickedTreeview.Sort();
                        foreach (TreeNode tn in dropNode.Parent.Nodes){
                            if (tn.Tag is SpeciesKey.TaxaRow tr){
                                tr.Index = (tn.Index + 1) * 10;
                            }
                        }

                        (treeViewDerivates == clickedTreeview ? treeViewTaxa : treeViewDerivates).Sort();
                        status.Message("{0} is now phylogenetically higher than {1}", carryTaxa.FullName, destTaxon.FullName);
                        IsChanged = true;
                    }
                    else // If drag-and-drop from one base to another - create derivation
                    {
                        // Create taxon derivation
                        Data.Derivation.AddDerivationRow(destTaxon, carryTaxa);
                        foreach (SpeciesKey.RepRow repRow in carryTaxa.GetRepRows()) {
                            // if destination taxon directly contains such representative
                            if (destTaxon.Includes(repRow.SpeciesRow, false)) {
                                // remove it, because such representation is now provided with newly created derivation
                                repRow.SpeciesRow.RemoveFrom(destTaxon);
                            }
                        }

                        updateVaria();

                        //if (clickedTreeview == treeViewTaxa)
                        //{
                        updateTree();
                        //}
                        //else
                        //{
                        //    dropNode.Nodes.Add(carryNode);
                        //}

                        status.Message("{0} is now derivated from {1}", carryTaxa.FullName, destTaxon.FullName);
                        IsChanged = true;
                    }
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
            if (IsBaseNodePicked)
            {
                contextRename_Click(sender, e);
            }
            else if (IsTaxaNodePicked)
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

        #region Taxonomic tree

        private void treeViewDerivates_MouseClick(object sender, MouseEventArgs e)
        {
            clickedTreeview = treeViewDerivates;
        }


        private void backTreeLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (SpeciesKey.TaxaRow taxaRow in Data.Taxa.GetRootTaxa()) {
                result.Add(getTaxonTreeNode(taxaRow, true, true));
            }
            foreach (SpeciesKey.SpeciesRow speciesRow in Data.Species.GetOrphans()) {
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
            updateRepresence();
        }

        #endregion

        #region Taxa list

        private void treeViewTaxa_MouseClick(object sender, MouseEventArgs e)
        {
            clickedTreeview = treeViewTaxa;
        }

        private void treeViewTaxa_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
            }
            else
            {
                switch (e.Node.Level)
                {
                    case 0:
                        if (IsBaseNodePicked) // renaming existing base
                        {
                            PickedBase.Base = PickedBase.Name = e.Label;
                            foreach (SpeciesKey.TaxaRow taxaRow in PickedBase.GetTaxaRows()) {
                                applyRename(taxaRow, treeViewDerivates.Nodes);
                                applyRename(taxaRow, listViewRepresence.Groups);
                            }
                        }
                        else // Node is just created
                        {
                            e.Node.Tag = Data.Base.AddBaseRow((e.Node.Index + 1) * 10, e.Label, e.Label);
                            updateVaria();
                        }

                        IsChanged = true;
                        break;

                    case 1: // Taxon level
                        if (IsTaxaNodePicked) // renaming existing taxon
                        {
                            PickedTaxon.Taxon = PickedTaxon.Name = e.Label;
                            applyRename(PickedTaxon, treeViewDerivates.Nodes);
                            applyRename(PickedTaxon, listViewRepresence.Groups);
                        }
                        else  // Node is just created
                        {
                            SpeciesKey.TaxaRow newTaxa = Data.Taxa.AddTaxaRow(PickedBase, (e.Node.Index + 1) * 10, e.Label, e.Label, null);
                            newTaxa.BaseRow = e.Node.Parent.Tag as SpeciesKey.BaseRow;
                            e.Node.Tag = newTaxa;
                            updateTree();
                        }

                        IsChanged = true;
                        break;
                }

                treeView_NodeMouseClick(treeViewTaxa, new TreeNodeMouseClickEventArgs(e.Node, MouseButtons.Left, 1, 0, 0));
                treeView_AfterSelect(treeViewTaxa, new TreeViewEventArgs(e.Node));
            }
        }

        private void backListLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TreeNode> result = new List<TreeNode>();

            foreach (SpeciesKey.BaseRow baseRow in ((SpeciesKey.BaseRow[])Data.Base.Select(null, "Index ASC")))
            {
                TreeNode baseNode = getBaseTreeNode(baseRow);
                foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows()) {
                    baseNode.Nodes.Add(getTaxonTreeNode(taxaRow, false, false));
                }
                result.Add(baseNode);
            }
            e.Result = result.ToArray();
        }

        private void backListLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TreeNode[] results = (TreeNode[])e.Result;
            treeViewTaxa.Nodes.AddRange(results);
            updateVaria();
            treeViewTaxa.ExpandAll();
            treeViewTaxa.Enabled = true;

            //labelTaxCount.UpdateStatus(Data.Taxa.Count);

            // List filled. Now update tree
            updateTree();
            processDisplay.StopProcessing();
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

                if (hoverItem != null)
                {
                    SpeciesKey.SpeciesRow speciesRow =
                        ((SpeciesKey.SpeciesRow[])e.Data.GetData(typeof(SpeciesKey.SpeciesRow[])))[0];

                    if (hoverItem.Tag != null)
                    {
                        SpeciesKey.SpeciesRow destSpeciesRow = (SpeciesKey.SpeciesRow)hoverItem.Tag;

                        listViewRepresence.SelectedItems.Clear();
                        hoverItem.Selected = true;

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

            //(sender as ListView).AutoScroll();
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

            //labelMinCount.UpdateStatus(listViewMinor.Items.Count);

            listViewRepresence.Height = treeViewTaxa.Height - ((listViewMinor.Items.Count == 0) ? 0 : 172);
            //labelMinCount.Visible = 
            listViewMinor.Visible =
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
                    if (IsBaseNodePicked)
                    {
                        item.Group = listViewRepresence.Groups[speciesRow.GetTaxon(PickedBase) == null ? "Varia" : speciesRow.GetTaxon(PickedBase).Taxon];
                    }
                    else if (IsTaxaNodePicked)
                    {
                        item.Group = listViewRepresence.Groups[speciesRow.GetParents().Last().Taxon];
                    }
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
                //labelRepCount.UpdateStatus(items.Length);
            }

            processDisplay.StopProcessing();
            treeViewTaxa.Enabled = true;
        }

        #endregion

        #region Base context menu

        private void contextRename_Click(object sender, EventArgs e)
        {
            treeViewTaxa.SelectedNode?.BeginEdit();
        }

        private void contextBaseDelete_Click(object sender, EventArgs e)
        {
            if (IsBaseNodePicked)
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
            contextTaxonEdit.Enabled =
                contextTaxonAddSpecies.Enabled =
                PickedTaxon != null;

            contextTaxonRename.Visible = clickedTreeview.LabelEdit;
            contextTaxonDelete.Visible = IsTaxaNodePicked && clickedTreeview.SelectedNode.Parent.Tag != null;
        }

        private void contextTaxonEdit_Click(object sender, EventArgs e)
        {
            EditTaxon taxonEdit = new EditTaxon(PickedTaxon);
            if (taxonEdit.ShowDialog(this) == DialogResult.OK)
            {
                IsChanged |= taxonEdit.IsChanged;
                treeViewTaxa.SelectedNode.Text = taxonEdit.TaxonRow.TaxonName;
            }
        }

        private void contextTaxonDelete_Click(object sender, EventArgs e)
        {
            if (IsTaxaNodePicked)
            {
                if (clickedTreeview.SelectedNode.Parent.Tag is SpeciesKey.TaxaRow tr) // Remove taxon from tree
                {
                    // Ask for reassigning representatives to higher taxon?

                    // Remove derivation
                    Data.Derivation.FindByMajTaxIDMinTaxID(tr.ID, PickedTaxon.ID).Delete();
                    clickedTreeview.Nodes.Remove(clickedTreeview.SelectedNode);
                    clickedTreeview.Nodes.Add(getTaxonTreeNode(PickedTaxon, true, true));
                }
                else if (clickedTreeview.SelectedNode.Parent.Tag is SpeciesKey.BaseRow) // Remove taxon from list
                {
                    // Remove taxon itself
                    ((SpeciesKey.TaxaRow)clickedTreeview.SelectedNode.Tag).Delete();
                    clickedTreeview.Nodes.Remove(clickedTreeview.SelectedNode);
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
            if (sender == listViewRepresence)
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    RunSpeciesEditing(Data.Species.FindByID(item.GetID()));
                }
            }

            if (sender == treeViewDerivates)
            {
                RunSpeciesEditing(treeViewDerivates.SelectedNode?.Tag as SpeciesKey.SpeciesRow);
            }
        }

        private void contextSpeciesDelete_Click(object sender, EventArgs e)
        {
            if (IsTaxaNodePicked || IsBaseNodePicked)
            {
                TaskDialogButton b = null;

                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    SpeciesKey.SpeciesRow speciesRow = (SpeciesKey.SpeciesRow)item.Tag;

                    SpeciesKey.TaxaRow taxaRow = clickedTreeview.SelectedNode.Tag is SpeciesKey.TaxaRow row
                        ? row
                        : (SpeciesKey.TaxaRow)item.Group.Tag;

                    if (taxaRow == null)
                    {
                        ((SpeciesKey.SpeciesRow)item.Tag).Delete();
                        item.Remove();
                    }
                    else
                    {
                        taskDialogDeleteSpecies.Content = string.Format(Resources.Messages.DeleteSpeciesContent,
                            speciesRow.Species, taxaRow.TaxonName);

                        if (b == null || !taskDialogDeleteSpecies.IsVerificationChecked)
                        {
                            b = taskDialogDeleteSpecies.ShowDialog(this);
                        }

                        if (b == tdbDeleteSpc)
                        {
                            ((SpeciesKey.SpeciesRow)item.Tag).Delete();
                            item.Remove();
                        }
                        else if (b == tdbDeleteRep)
                        {
                            speciesRow.RemoveFrom(taxaRow);

                            if (IsTaxaNodePicked)
                            {
                                item.Remove();
                            }
                            else if (IsBaseNodePicked)
                            {
                                item.Group = listViewRepresence.GetGroup("Varia");
                            }
                        }
                        else if (b == tdbDeleteCancel)
                        {
                            continue;
                        }
                    }
                }

                taskDialogDeleteSpecies.IsVerificationChecked = false;
            }
            else
            {
                foreach (ListViewItem item in listViewRepresence.SelectedItems)
                {
                    ((SpeciesKey.SpeciesRow)item.Tag).Delete();
                    item.Remove();
                }
            }

            IsChanged = true;
            updateVaria();
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