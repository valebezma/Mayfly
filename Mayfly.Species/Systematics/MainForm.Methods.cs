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



        public bool IsAllSpeciesShown
        {
            get;
            private set;
        }

        public bool IsBaseNodeSelected
        {
            get;
            private set;
        }

        public bool IsTaxaNodeSelected
        {
            get;
            private set;
        }

        public SpeciesKey.BaseRow SelectedBase
        {
            get;
            private set;
        }

        public SpeciesKey.TaxaRow SelectedTaxon
        {
            get;
            private set;
        }

        List<ListViewItem> dragItems = new List<ListViewItem>();



        private void LoadData(string filename)
        {
            //Clear();

            FileName = filename;
            Data.Read(FileName);

            LoadList();

            if (Data.IsKeyAvailable)
            {
                LoadKey();
            }
            else
            {
                LoadEngagedList();
            }
            //status.Message(Resources.Messages.Loaded);
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

        private void LoadList()
        {
            clearTaxaTree();
            treeViewTaxa.Enabled = false;
            backTreeLoader.RunWorkerAsync();
            UpdateRepresence();
        }

        private void UpdateRepresence()
        {
            listViewRepresence.Groups.Clear();
            listViewRepresence.ShowGroups = IsBaseNodeSelected;

            if (IsAllSpeciesShown)
            {
                UpdateRepresence(Data.Species.GetSorted());
            }
            else if (IsBaseNodeSelected)
            {
                if (SelectedBase == null)
                {
                    listViewRepresence.Items.Clear();
                    labelRepCount.UpdateStatus(0);
                }
                else
                {
                    listViewRepresence.Groups.Clear();

                    SpeciesKey.TaxaRow[] taxaRows = SelectedBase.GetTaxaRows();

                    foreach (SpeciesKey.TaxaRow taxaRow in taxaRows)
                    {
                        ListViewGroup taxonGroup = new ListViewGroup(taxaRow.TaxonName);
                        taxonGroup.Name = taxaRow.TaxonName;
                        taxonGroup.HeaderAlignment = HorizontalAlignment.Center;
                        listViewRepresence.Groups.Add(taxonGroup);
                    }

                    //if (SelectedBase.GetIrrelevant().Length > 0)
                    //{
                    ListViewGroup variaGroup = new ListViewGroup(Species.Resources.Interface.Varia);
                    variaGroup.Name = "Varia";
                    variaGroup.HeaderAlignment = HorizontalAlignment.Center;
                    listViewRepresence.Groups.Add(variaGroup);
                    //}

                    UpdateRepresence(Data.Species.GetSorted());
                }
            }
            else if (IsTaxaNodeSelected)
            {
                UpdateRepresence(SelectedTaxon.GetSpecies());
            }
            else
            {
                if (SelectedTaxon == null) // Varia or new taxon
                {
                    if (treeViewTaxa.SelectedNode != null && treeViewTaxa.SelectedNode.Name.Contains("Other_"))
                    {
                        UpdateRepresence(SelectedBase.Varia);
                    }
                    else
                    {
                        listViewRepresence.Items.Clear();
                        labelRepCount.UpdateStatus(0);
                    }
                }
            }

            labelTaxCount.UpdateStatus(IsAllSpeciesShown ? Data.Taxa.Count :
                SelectedBase == null ? 0 : SelectedBase.GetTaxaRows().Length);

            menuItemAddSpecies.Enabled = IsAllSpeciesShown || IsTaxaNodeSelected;
        }

        private void UpdateRepresence(SpeciesKey.SpeciesRow[] speciesRows)
        {
            treeViewTaxa.Enabled = false;

            if (!backSpcLoader.IsBusy)
            {
                backSpcLoader.RunWorkerAsync(speciesRows);
            }
        }

        private void updateVaria()
        {
            foreach (TreeNode baseNode in treeViewTaxa.Nodes["RootList"].Nodes)
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
            // Clear tree
            for (int i = 0; i < treeViewTaxa.Nodes["RootList"].Nodes.Count; i++)
            {
                treeViewTaxa.Nodes["RootList"].Nodes[i].Remove();
                i--;
            }

            // Clear species list
            listViewRepresence.Items.Clear();
        }

        private void updateTree()
        {
            for (int i = 0; i < treeViewTaxa.Nodes["RootTree"].Nodes.Count; i++) {
                treeViewTaxa.Nodes["RootTree"].Nodes[i].Remove();
                i--;
            }

            List<TreeNode> resultTree = new List<TreeNode>();
            foreach (SpeciesKey.TaxaRow taxaRow in Data.Taxa.GetRootTaxa()) {
                resultTree.Add(getTaxonTreeNode(taxaRow, true));
            }
            treeViewTaxa.Nodes["rootTree"].Nodes.AddRange(resultTree.ToArray());
            treeViewTaxa.Nodes["rootTree"].ExpandAll();
        }

        private TreeNode getBaseTreeNode(SpeciesKey.BaseRow baseRow)
        {
            TreeNode baseNode = new TreeNode
            {
                Tag = baseRow,
                Text = baseRow.Base,
                ContextMenuStrip = contextBase
            };

            return baseNode;
        }

        private TreeNode getTaxonTreeNode(SpeciesKey.TaxaRow taxaRow, bool derivates)
        {
            
            TreeNode taxaNode = new TreeNode
            {
                Tag = taxaRow,
                Text = derivates ?  string.Format("{0} ({1})", taxaRow.FullName, taxaRow.GetSpecies().Length) : taxaRow.Taxon,
                ContextMenuStrip = contextTaxon
            };

            if (derivates)
            {
                foreach (SpeciesKey.DerivationRow derRow in taxaRow.GetDerivationRowsByFK_Taxa_Derivation())
                {
                    taxaNode.Nodes.Add(getTaxonTreeNode(derRow.TaxaRowByFK_Taxa_Derivation1, derivates));
                }
            }

            return taxaNode;
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

    }    
}
