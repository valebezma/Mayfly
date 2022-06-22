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

        public bool IsTaxonNodePicked
        {
            get;
            private set;
        }

        public SpeciesKey.TaxonRow PickedTaxon
        {
            get;
            private set;
        }

        public bool IsSpeciesNodePicked
        {
            get;
            private set;
        }

        public SpeciesKey.SpeciesRow PickedSpecies
        {
            get;
            private set;
        }



        private void loadData(string filename)
        {
            FileName = filename;
            Data.Read(FileName);

            updateSummary();

            updateTree();

            if (Data.IsKeyAvailable) {
                loadKey();
            } else {
                loadEngagedList();
            }
            
            IsChanged = false;
        }

        private void updateSummary()
        {
            statusSpecies.ResetFormatted(Data.Species.Count);
            statusTaxon.ResetFormatted(Data.Taxon.Count);
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
                Name = taxonRow.Taxon,
                HeaderAlignment = HorizontalAlignment.Center,
                Tag = taxonRow
            };
        }




        private void updateTree()
        { 
            listViewRepresence.Groups.Clear();
            foreach (SpeciesKey.TaxonRow taxonRow in Data.Taxon) {
                listViewRepresence.Groups.Add(getGroup(taxonRow));
            }
            listViewRepresence.Groups.Add(new ListViewGroup(string.Format("Varia ({0})", Data.Species.GetOrphans().Length)) {
                Name = "Varia",
                HeaderAlignment = HorizontalAlignment.Center
            });

            treeViewDerivates.Nodes.Clear();
            treeViewDerivates.Enabled = false;
            processDisplay.StartProcessing(100, Resources.Interface.StatusLoadingTree);
            backTreeLoader.RunWorkerAsync();
        }

        private void editSpecies(SpeciesKey.SpeciesRow speciesRow)
        {
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

                TreeNode carryNode = treeViewDerivates.Nodes.Find("s" + editSpecies.SpeciesRow.ID.ToString(), true)?[0];
                carryNode.Remove();
                carryNode = getSpeciesTreeNode(editSpecies.SpeciesRow);
                if (editSpecies.SpeciesRow.TaxonRow == null)
                {
                    treeViewDerivates.Nodes.Add(carryNode);
                }
                else
                {
                    TreeNode dropNode = treeViewDerivates.Nodes.Find("t" + editSpecies.SpeciesRow.TaxonRow.ID.ToString(), true)?[0];
                    dropNode.Nodes.Add(carryNode);
                }
                treeViewDerivates.SelectedNode = carryNode;

                applyRename(listViewRepresence.Groups);

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

        ToolTip toolTip = new ToolTip();
        string message = string.Empty;

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

        private void updateRepresenceSpecies(SpeciesKey.SpeciesRow[] speciesRows)
        {
            treeViewDerivates.Enabled = false; 
            processDisplay.StartProcessing(100, Resources.Interface.StatusLoadingList);

            if (!backSpcLoader.IsBusy)
            {
                backSpcLoader.RunWorkerAsync(speciesRows);
            }
        }

        private TreeNode getSpeciesTreeNode(SpeciesKey.SpeciesRow spcRow)
        {
            return new TreeNode
            {
                Tag = spcRow,
                Name = "s" + spcRow.ID.ToString(),
                Text = spcRow.FullName,
                //ForeColor = spcRow.IsMajorSynonimIDNull() ? Color.Black : Color.DarkGray,
                ContextMenuStrip = contextSpecies
            };
        }

        private TreeNode getTaxonTreeNode(SpeciesKey.TaxonRow taxonRow, bool derivates, bool representatives)
        {
            TreeNode taxonNode = new TreeNode
            {
                Tag = taxonRow,
                Name = "t" + taxonRow.ID.ToString(),
                Text = taxonRow.InterfaceString,
                ContextMenuStrip = contextTaxon
            };

            if (derivates)
            {
                foreach (SpeciesKey.TaxonRow derRow in taxonRow.GetTaxonRows())
                {
                    taxonNode.Nodes.Add(getTaxonTreeNode(derRow, derivates, representatives));
                }
            }

            if (representatives)
            {
                foreach (SpeciesKey.SpeciesRow repRow in taxonRow.GetSpeciesRows())
                {
                    taxonNode.Nodes.Add(getSpeciesTreeNode(repRow));
                }
            }

            return taxonNode;
        }

        private void applyRename(TreeNode node)
        {
            if (node.Tag is SpeciesKey.TaxonRow)
            {
                SpeciesKey.TaxonRow taxonRow = (SpeciesKey.TaxonRow)node.Tag;
                node.Text = taxonRow.InterfaceString;
                if (node.Parent != null) applyRename(node.Parent);
            }
        }

        private void applyRename(ListViewGroupCollection groups)
        {
            foreach (ListViewGroup group in groups)
            {
                if (group.Tag is SpeciesKey.TaxonRow tr) {
                    group.Header = tr.InterfaceString;
                } else {
                    group.Header = string.Format("Varia ({0})", Data.Species.GetOrphans().Length);
                }
            }
        }

        private void applySort(TreeNodeCollection nodes)
        {
            foreach (TreeNode tn in nodes)
            {
                if (tn.Tag is SpeciesKey.TaxonRow tr)
                {
                    tr.Index = (tn.Index + 1) * 10;

                    applySort(tn.Nodes);
                }
            }
        }

        private DragDropEffects checkSynonimyAvailability(SpeciesKey.SpeciesRow[] minorSpecies, SpeciesKey.SpeciesRow majorSpecies)
        {
            if (Array.IndexOf(minorSpecies, majorSpecies) > -1) // Carry species to itself
            {
                notifyInstantly(Resources.Interface.TipSpcToSpcSynonimUnableItself);
                return DragDropEffects.None;
            }
            else if (!majorSpecies.IsSynonimyAvailable(minorSpecies))
            {
                notifyInstantly(Resources.Interface.TipSpcToSpcSynonimUnableSeparateBranch);
                return DragDropEffects.None;
            }
            else
            {
                notifyInstantly(Resources.Interface.TipSpcToSpcSynonim,
                    minorSpecies.Length == 1 ? minorSpecies[0].Species : string.Format(Resources.Interface.TipSpcMultiple, minorSpecies.Length),
                    majorSpecies);
                return DragDropEffects.Link;
            }
        }

        private void setSynonims(SpeciesKey.SpeciesRow[] minorSpecies, SpeciesKey.SpeciesRow majorSpecies)
        {
            foreach (SpeciesKey.SpeciesRow speciesRow in minorSpecies)
            {
                if (majorSpecies != speciesRow)
                {
                    speciesRow.MajorSynonimID = majorSpecies.ID;
                }
            }
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
            foreach (SpeciesKey.SpeciesRow speciesRow in Data.Species)
            {
                ListViewItem item = listViewEngagement.CreateItem(speciesRow);
                item.Group = speciesRow.GetStateRows().Length == 0 ?
                    listViewEngagement.Groups[1] : listViewEngagement.Groups[0];
            }

            labelEngagedCount.UpdateStatus(listViewEngagement.Groups[0].Items.Count);
        }

        private void rearrangeEnagagedItems(SpeciesKey.StepRow selectedStep)
        {
            foreach (SpeciesKey.SpeciesRow speciesRow in Data.Species)
            {

            }
        }

        private void clearKey()
        {

        }

        private void clearPictures()
        {

        }
    }    
}
