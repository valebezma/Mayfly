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

namespace Mayfly.Species
{
    partial class MainForm
    {
        private string filename;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        public SpeciesKey Data { set; get; }

        public bool IsChanged { set; get; }

        public SpeciesKey.TaxonRow pickedTaxon;
        ToolTip toolTip = new ToolTip();
        string message = string.Empty;



        private void loadData(string filename)
        {
            FileName = filename;

            listViewRepresence.Groups.Clear();
            treeViewDerivates.Nodes.Clear();
            treeViewDerivates.Enabled = false;
            processDisplay.StartProcessing(100, Resources.Interface.StatusLoadingTree);

            backTreeLoader.RunWorkerAsync();
        }

        private void updateSummary()
        {
            statusSpecies.ResetFormatted(Data.ValidSpeciesCount);
            statusTaxon.ResetFormatted(Data.HigherTaxonCount);
        }

        private DialogResult checkAndSave()
        {
            if (IsChanged)
            {
                TaskDialogButton b = tdSave.ShowDialog(this);

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

        private void save(string filename)
        {
            Data.SaveToFile(filename);
            FileName = filename;
            status.Message(Resources.Interface.StatusSaved);
            IsChanged = false;
        }

        private void clear()
        {
            FileName = null;
            Data = new SpeciesKey();

            listViewRepresence.Items.Clear();
            treeViewDerivates.Nodes.Clear();

            clearKey();
            clearPictures();
        }



        private ListViewGroup getGroup(SpeciesKey.TaxonRow taxonRow)
        {
            return new ListViewGroup(taxonRow.InterfaceString)
            {
                Name = taxonRow.Name,
                HeaderAlignment = HorizontalAlignment.Center,
                Tag = taxonRow
            };
        }

        private void addGroup(ListViewGroup group)
        {
            if (!listViewRepresence.InvokeRequired)
            {
                listViewRepresence.Groups.Add(group);
            }
            else
            {
                AddGroupEventHandler groupAdder = new AddGroupEventHandler(addGroup);
                listViewRepresence.Invoke(groupAdder, new object[] { group });
            }
        }

        private delegate void AddGroupEventHandler(ListViewGroup group);



        private TreeNode getTaxonTreeNode(SpeciesKey.TaxonRow taxonRow) //, bool representatives)
        {
            TreeNode taxonNode = new TreeNode
            {
                Tag = taxonRow,
                Name = taxonRow.ID.ToString(),
                Text = taxonRow.IsSpecies ? taxonRow.FullName : taxonRow.InterfaceString,
                ContextMenuStrip = taxonRow.IsSpecies ? contextSpecies : contextTaxon
            };

            foreach (SpeciesKey.TaxonRow derRow in taxonRow.GetTaxonRows())
            {
                if (derRow.IsSpecies) continue;
                taxonNode.Nodes.Add(getTaxonTreeNode(derRow));
            }
            
            return taxonNode;
        }

        private void addNode(TreeNode node)
        {
            if (!treeViewDerivates.InvokeRequired)
            {
                treeViewDerivates.Nodes.Add(node);
            }
            else
            {
                AddNodeEventHandler nodeAdder = new AddNodeEventHandler(addNode);
                treeViewDerivates.Invoke(nodeAdder, new object[] { node });
            }
        }

        private delegate void AddNodeEventHandler(TreeNode node);



        private void editSpecies(SpeciesKey.TaxonRow speciesRow)
        {
            speciesRow = speciesRow.ValidSpeciesRow;

            foreach (Form form in Application.OpenForms)
            {
                if (form is EditSpecies editor && editor.SpeciesRow == speciesRow)
                {
                    editor.BringToFront();
                    return;
                }
            }

            EditSpecies editSpecies = new EditSpecies(speciesRow);
            editSpecies.FormClosed += new FormClosedEventHandler(editSpecies_FormClosed);
            editSpecies.Show(this);
        }

        private void editSpecies_FormClosed(object sender, FormClosedEventArgs e)
        {
            EditSpecies editSpecies = (EditSpecies)sender;

            if (editSpecies.DialogResult == DialogResult.OK)
            {
                IsChanged = IsChanged || editSpecies.IsChanged;

                // Update node in treeviewkeys

                TreeNode carryNode = treeViewDerivates.Nodes.Find(editSpecies.SpeciesRow.ID.ToString(), true)?[0];
                carryNode.Remove();
                carryNode = getTaxonTreeNode(editSpecies.SpeciesRow);
                if (editSpecies.SpeciesRow.TaxonRowParent == null)
                {
                    treeViewDerivates.Nodes.Add(carryNode);
                }
                else
                {
                    TreeNode dropNode = treeViewDerivates.Nodes.Find(editSpecies.SpeciesRow.TaxonRowParent.ID.ToString(), true)?[0];
                    dropNode.Nodes.Add(carryNode);
                }
                treeViewDerivates.SelectedNode = carryNode;

                applyRename();

                // Apply rename to species item in listView

                foreach (ListView listView in new ListView[] { listViewRepresence, listViewEngagement })
                {
                    ListViewItem item = listView.GetItem(editSpecies.SpeciesRow.ID.ToString());
                    if (item == null) continue;

                    item.UpdateItem(editSpecies.SpeciesRow);
                    listView.Shine();
                }
            }
        }

        private void notifyInstantly(string format, params object[] values)
        {
            Point pt = Cursor.Position;
            pt.Offset(-this.Location.X + 15, -this.Location.Y);
            if (message != string.Format(format, values))
            {
                message = string.Format(format, values);
                toolTip.Show(message, this, pt, 5000);
            }
        }

        private void updateRepresenceSpecies(SpeciesKey.TaxonRow[] speciesRows)
        {
            treeViewDerivates.Enabled = false; 
            processDisplay.StartProcessing(100, Resources.Interface.StatusLoadingList);

            if (!backSpcLoader.IsBusy)
            {
                backSpcLoader.RunWorkerAsync(speciesRows);
            }
        }

        private void applyRename(TreeNode node)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync(node);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            TreeNode node = (TreeNode)e.Argument;
            SpeciesKey.TaxonRow taxonRow = (SpeciesKey.TaxonRow)((TreeNode)e.Argument).Tag;
            applyRename(node, taxonRow.IsSpecies ? taxonRow.FullName : taxonRow.InterfaceString);
            if (node.Parent != null) applyRename(node.Parent);
        }

        public void applyRename(TreeNode treeNode, string text)
        {
            if (treeNode == null || !treeNode.TreeView.InvokeRequired) { treeNode.Text = text; }
            else {
                TextSetEventHandler textSetter = new TextSetEventHandler(applyRename);
                treeNode.TreeView.Invoke(textSetter, new object[] { treeNode, text });
            }
        }

        private delegate void TextSetEventHandler(TreeNode treeNode, string text);

        //private void applyRename(SpeciesKey.TaxonRow taxonRow)
        //{
        //    listViewRepresence.Groups[taxonRow.Name].Header = taxonRow.InterfaceString;
        //    if (taxonRow.TaxonRowParent != null) applyRename(taxonRow.TaxonRowParent);
        //}

        private void applyRename()
        {
            foreach (ListViewGroup group in listViewRepresence.Groups)
            {
                if (group.Tag is SpeciesKey.TaxonRow tr)
                {
                    group.Header = tr.InterfaceString;
                }
                else
                {
                    group.Header = string.Format(Resources.Interface.VariaCount, Data.GetOrphans().Length);
                }
            }

            listViewRepresence.Shine();
        }

        private void applySort(TreeNodeCollection nodes)
        {
            foreach (TreeNode tn in nodes)
            {
                if (tn.Tag is SpeciesKey.TaxonRow tr)
                {
                    if (tr.IsSpecies) continue;

                    tr.Index = (tn.Index + 1) * 10;
                    applySort(tn.Nodes);
                }
            }
        }

        private DragDropEffects checkSynonymyAvailability(SpeciesKey.TaxonRow[] minorSpecies, SpeciesKey.TaxonRow majorSpecies)
        {
            if (Array.IndexOf(minorSpecies, majorSpecies) > -1) // Carry species to itself
            {
                notifyInstantly(Resources.Interface.TipSpcToSpcSynonymUnableItself);
                return DragDropEffects.None;
            }
            else if (!majorSpecies.IsSynonymyAvailable(minorSpecies))
            {
                notifyInstantly(Resources.Interface.TipSpcToSpcSynonymUnableSeparateBranch);
                return DragDropEffects.None;
            }
            else
            {
                notifyInstantly(Resources.Interface.TipSpcToSpcSynonym,
                    minorSpecies.Length == 1 ? minorSpecies[0].Name : string.Format(Resources.Interface.TipSpcMultiple, minorSpecies.Length),
                    majorSpecies);
                return DragDropEffects.Link;
            }
        }

        //private void setSynonyms(SpeciesKey.TaxonRow[] minorSpecies, SpeciesKey.TaxonRow majorSpecies)
        //{
        //    foreach (SpeciesKey.TaxonRow speciesRow in minorSpecies)
        //    {
        //        if (majorSpecies != speciesRow)
        //        {
        //            speciesRow.TaxID = majorSpecies.ID;
        //        }
        //    }
        //}

        private void setParent(SpeciesKey.TaxonRow[] carryTaxonRows, SpeciesKey.TaxonRow targetTaxonRow)
        {
            TreeNode[] tt = treeViewDerivates.Nodes.Find(targetTaxonRow.ID.ToString(), true);
            TreeNode targetNode = tt.Length > 0 ? tt[0] : null;

            foreach (SpeciesKey.TaxonRow speciesRow in carryTaxonRows)
            {
                if (targetTaxonRow == speciesRow) continue;

                tt = treeViewDerivates.Nodes.Find(speciesRow.ID.ToString(), true);
                TreeNode carryNode = tt.Length > 0 ? tt[0] : null;

                tt = treeViewDerivates.Nodes.Find(speciesRow.TaxonRowParent?.ID.ToString(), true);

                speciesRow.TaxonRowParent = targetTaxonRow;

                if (tt.Length > 0) applyRename(tt[0]);

                if (carryNode != null)
                {
                    carryNode.Remove();
                    if (targetNode != null) targetNode.Nodes.Add(getTaxonTreeNode(speciesRow));
                }
            }

            //treeViewDerivates.Sort();
            if (targetNode != null) applyRename(targetNode);
            applyRename();
        }









        public bool IsStepNodeSelected
        {
            get
            {
                if (treeViewStep.SelectedNode == null)
                {
                    return false;
                }
                else
                {
                    return treeViewStep.SelectedNode.Level == 0;
                }
            }
        }

        public bool IsFeatureNodeSelected
        {
            get
            {
                if (treeViewStep.SelectedNode == null)
                {
                    return false;
                }
                else
                {
                    return treeViewStep.SelectedNode.Level == 1;
                }
            }
        }

        public bool IsStateNodeSelected
        {
            get
            {
                if (treeViewStep.SelectedNode == null)
                {
                    return false;
                }
                else
                {
                    return treeViewStep.SelectedNode.Level == 2;
                }
            }
        }

        public SpeciesKey.StepRow SelectedStep
        {
            get
            {
                if (IsStepNodeSelected)
                {
                    return (SpeciesKey.StepRow)treeViewStep.SelectedNode.Tag;
                }

                if (IsFeatureNodeSelected)
                {
                    return (SpeciesKey.StepRow)treeViewStep.SelectedNode.Parent.Tag;
                }

                if (IsStateNodeSelected)
                {
                    return (SpeciesKey.StepRow)treeViewStep.SelectedNode.Parent.Parent.Tag;
                }

                return null;
            }
        }

        public SpeciesKey.FeatureRow SelectedFeature
        {
            get
            {
                if (IsFeatureNodeSelected)
                {
                    return (SpeciesKey.FeatureRow)treeViewStep.SelectedNode.Tag;
                }

                if (IsStateNodeSelected)
                {
                    return (SpeciesKey.FeatureRow)treeViewStep.SelectedNode.Parent.Tag;
                }

                return null;
            }
        }

        public SpeciesKey.StateRow SelectedState
        {
            get
            {
                if (IsStateNodeSelected)
                {
                    return (SpeciesKey.StateRow)treeViewStep.SelectedNode.Tag;
                }

                return null;
            }
        }



        private void loadKey()
        {
            loadKeyTree();
            loadEngagedList();
        }

        private void loadKeyTree()
        {
            treeViewStep.Nodes.Clear();

            foreach (SpeciesKey.StepRow stepRow in Data.Step)
            {
                TreeNode stepNode = new TreeNode();
                stepNode.Name = stepRow.ID.ToString();
                stepNode.Tag = stepRow;
                stepNode.Text = string.Format(Resources.Diagnosis.Step, stepRow.ID);
                stepNode.ContextMenuStrip = contextStep;
                treeViewStep.Nodes.Add(stepNode);

                foreach (SpeciesKey.FeatureRow featureRow in stepRow.GetFeatureRows())
                {
                    TreeNode featureNode = new TreeNode();
                    featureNode.Name = featureRow.ID.ToString();
                    featureNode.Tag = featureRow;
                    featureNode.Text = featureRow.Title;
                    featureNode.ContextMenuStrip = contextFeature;
                    stepNode.Nodes.Add(featureNode);

                    foreach (SpeciesKey.StateRow stateRow in featureRow.GetStateRows())
                    {
                        TreeNode stateNode = new TreeNode();
                        stateNode.Name = stateRow.ID.ToString();
                        stateNode.Tag = stateRow;
                        stateNode.Text = stateRow.ShortDescription;
                        stateNode.ContextMenuStrip = contextFeature;
                        stateNode.ToolTipText = stateRow.Description.InsertBreaks(50);
                        featureNode.Nodes.Add(stateNode);
                    }
                }

                stepNode.Expand();
            }

            labelStpCount.UpdateStatus(Data.Step.Count);

            //treeViewStep.ExpandAll();
        }

        private void loadEngagedList()
        {
            foreach (SpeciesKey.TaxonRow speciesRow in Data.GetSpeciesRows())
            {
                ListViewItem item = listViewEngagement.CreateItem(speciesRow);
                item.Group = speciesRow.GetStateRows().Length == 0 ?
                    listViewEngagement.Groups[1] : listViewEngagement.Groups[0];
            }

            labelEngagedCount.UpdateStatus(listViewEngagement.Groups[0].Items.Count);
        }

        private void rearrangeEnagagedItems(SpeciesKey.StepRow selectedStep)
        {
            //foreach (SpeciesKey.TaxonRow speciesRow in Data.Species)
            //{

            //}
        }

        private void clearKey()
        {

        }

        private void clearPictures()
        {

        }
    }    
}
