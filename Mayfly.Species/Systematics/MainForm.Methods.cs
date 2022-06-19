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



        public bool IsBaseNodePicked
        {
            get;
            private set;
        }

        public SpeciesKey.BaseRow PickedBase
        {
            get;
            private set;
        }

        public bool IsTaxaNodePicked
        {
            get;
            private set;
        }

        public SpeciesKey.TaxaRow PickedTaxon
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


        TreeView clickedTreeview;



        private void LoadData(string filename)
        {
            //Clear();

            FileName = filename;
            Data.Read(FileName);

            updateList();

            if (Data.IsKeyAvailable) {
                LoadKey();
            } else {
                LoadEngagedList();
            }
            
            status.Message("Index is loaded");
            IsChanged = false;
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
            status.Message(Resources.Messages.Saved);
            IsChanged = false;
        }

        private void Clear()
        {
            FileName = null;
            Data = new SpeciesKey();

            clearTaxaTree();
            ClearKey();
            ClearPictures();
        }




        private void RunSpeciesEditing(SpeciesKey.SpeciesRow speciesRow)
        {
            //EditSpecies editSpecies = new EditSpecies(speciesRow);
            AddSpecies editSpecies = new AddSpecies(speciesRow);
            editSpecies.FormClosed += new FormClosedEventHandler(editSpecies_FormClosed);
            editSpecies.Show(this);
        }

        private void editSpecies_FormClosed(object sender, FormClosedEventArgs e)
        {
            //EditSpecies editSpecies = (EditSpecies)sender;
            AddSpecies editSpecies = (AddSpecies)sender;

            if (editSpecies.DialogResult == DialogResult.OK)
            {
                IsChanged = IsChanged || editSpecies.IsChanged;

                foreach (ListView listView in new ListView[] { listViewRepresence, listViewEngagement })
                {
                    ListViewItem item = listView.GetItem(editSpecies.SpeciesRow.ID.ToString());
                    if (item == null) continue;

                    item.UpdateItem(editSpecies.SpeciesRow);
                    listView.Shine();
                }

                // Update node in treeviewkeys
            }
        }



        private ListViewGroup getGroup(SpeciesKey.TaxaRow taxaRow)
        {
            return new ListViewGroup(string.Format("{0} ({1})", taxaRow.FullName, taxaRow.GetSpecies().Length))
            {
                Name = taxaRow.Taxon,
                HeaderAlignment = HorizontalAlignment.Center,
                Tag = taxaRow
            };
        }

        private void updateRepresenceSpecies(SpeciesKey.SpeciesRow[] speciesRows)
        {
            treeViewTaxa.Enabled = false; 
            processDisplay.StartProcessing(1000, "Species list updating");

            if (!backSpcLoader.IsBusy)
            {
                backSpcLoader.RunWorkerAsync(speciesRows);
            }
        }

        private void updateRepresence()
        {
            label3.Text = "Species" + String.Format(IsBaseNodePicked ? " by " + PickedBase.BaseName :
                (IsTaxaNodePicked ? " of " + PickedTaxon.FullName : String.Empty));

            listViewRepresence.Groups.Clear();

            if (IsBaseNodePicked)
            {
                listViewRepresence.Groups.Clear();
                foreach (SpeciesKey.TaxaRow taxaRow in PickedBase.GetTaxaRows()) {
                    listViewRepresence.Groups.Add(getGroup(taxaRow));
                }
                ListViewGroup variaGroup = new ListViewGroup(Species.Resources.Interface.Varia) {
                    Name = "Varia",
                    HeaderAlignment = HorizontalAlignment.Center
                };
                listViewRepresence.Groups.Add(variaGroup);

                updateRepresenceSpecies(Data.Species.GetSorted());
            }
            else if (IsTaxaNodePicked)
            {
                listViewRepresence.Groups.Clear();
                listViewRepresence.Groups.Add(getGroup(PickedTaxon));
                foreach (SpeciesKey.TaxaRow taxaRow in PickedTaxon.GetChildren(true)) {
                    listViewRepresence.Groups.Add(getGroup(taxaRow));
                }
                updateRepresenceSpecies(PickedTaxon.GetSpecies());
            }
            else if (PickedTaxon == null) // Varia or new taxon
            {
                if (treeViewTaxa.SelectedNode != null && treeViewTaxa.SelectedNode.Name.Contains("Other_"))
                {
                    updateRepresenceSpecies((treeViewTaxa.SelectedNode.Parent.Tag as SpeciesKey.BaseRow)?.Varia);
                }
                else
                {
                    updateRepresenceSpecies(Data.Species.GetSorted());
                }
            }

            menuItemAddSpecies.Enabled = IsTaxaNodePicked;
        }

        private void updateVaria()
        {
            foreach (TreeNode baseNode in treeViewTaxa.Nodes)
            {
                if (!(baseNode.Tag is SpeciesKey.BaseRow)) continue;

                TreeNode[] othersNode = baseNode.Nodes.Find("Other_" + baseNode.Name, false);

                int count = ((SpeciesKey.BaseRow)baseNode.Tag).Varia.Length;

                if (count == 0)
                {
                    if (othersNode.Length > 0)
                    {
                        othersNode[0].Remove();
                    }
                }
                else
                {
                    if (othersNode.Length == 0)
                    {
                        TreeNode newOthersNode = new TreeNode(String.Format(Species.Resources.Interface.Others, count));
                        newOthersNode.Name = "Other_" + baseNode.Name;
                        baseNode.Nodes.Add(newOthersNode);
                    }
                    else
                    {
                        othersNode[0].Text = String.Format(Species.Resources.Interface.Others, count);
                    }
                }
            }
        }

        private void clearTaxaTree()
        {
            listViewRepresence.Items.Clear();
        }

        private void updateList()
        {
            treeViewTaxa.Nodes.Clear();
            treeViewTaxa.Enabled = false;
            backListLoader.RunWorkerAsync();
        }

        private void updateTree()
        {
            treeViewDerivates.Nodes.Clear();
            treeViewDerivates.Enabled = false;
            processDisplay.StartProcessing(1000, "Tree updating");
            backTreeLoader.RunWorkerAsync();
        }

        private TreeNode getBaseTreeNode(SpeciesKey.BaseRow baseRow)
        {
            return new TreeNode
            {
                Tag = baseRow,
                Text = baseRow.Base,
                ContextMenuStrip = contextBase
            };
        }

        private TreeNode getSpeciesTreeNode(SpeciesKey.SpeciesRow spcRow)
        {
            return new TreeNode
            {
                Tag = spcRow,
                Text = spcRow.FullName,
                ForeColor = Color.DarkGray,
                ContextMenuStrip = contextSpecies
            };
        }

        private TreeNode getTaxonTreeNode(SpeciesKey.TaxaRow taxaRow, bool derivates, bool representatives)
        {
            TreeNode taxaNode = new TreeNode
            {
                Tag = taxaRow,
                Text = derivates ? string.Format("{0} ({1})", taxaRow.FullName, taxaRow.GetSpecies().Length) : taxaRow.Taxon,
                ContextMenuStrip = contextTaxon
            };

            if (derivates)
            {
                foreach (SpeciesKey.DerivationRow derRow in taxaRow.GetDerivationRowsByFK_Taxa_Derivation())
                {
                    taxaNode.Nodes.Add(getTaxonTreeNode(derRow.TaxaRowByFK_Taxa_Derivation1, derivates, representatives));
                }
            }

            if (representatives)
            {
                foreach (SpeciesKey.RepRow repRow in taxaRow.GetRepRows())
                {
                    taxaNode.Nodes.Add(getSpeciesTreeNode(repRow.SpeciesRow));
                }
            }

            return taxaNode;
        }

        private void applyRename(SpeciesKey.TaxaRow taxaRow, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is SpeciesKey.TaxaRow && node.Tag == taxaRow)
                {
                    node.Text = string.Format("{0} ({1})", taxaRow.FullName, taxaRow.GetSpecies().Length);
                }
                else
                {
                    applyRename(taxaRow, node.Nodes);
                }
            }
        }

        private void applyRename(SpeciesKey.TaxaRow taxaRow, ListViewGroupCollection groups)
        {
            foreach (ListViewGroup group in groups)
            {
                if (group.Tag is SpeciesKey.TaxaRow && group.Tag == taxaRow)
                {
                    group.Header = string.Format("{0} ({1})", taxaRow.FullName, taxaRow.GetSpecies().Length);
                    group.Name = taxaRow.Taxon;
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



        private void LoadKey()
        {
            LoadKeyTree();
            LoadEngagedList();
        }

        private void LoadKeyTree()
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

        private void LoadEngagedList()
        {
            foreach (SpeciesKey.SpeciesRow speciesRow in Data.Species)
            {
                ListViewItem item = listViewEngagement.CreateItem(speciesRow);
                item.Group = speciesRow.GetStateRows().Length == 0 ?
                    listViewEngagement.Groups[1] : listViewEngagement.Groups[0];
            }

            labelEngagedCount.UpdateStatus(listViewEngagement.Groups[0].Items.Count);
        }

        private void RearrangeEnagagedItems(SpeciesKey.StepRow selectedStep)
        {
            foreach (SpeciesKey.SpeciesRow speciesRow in Data.Species)
            {

            }
        }

        private void ClearKey()
        {

        }



        private void ClearPictures()
        {

        }
    }    
}
