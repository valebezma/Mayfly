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
                this.ResetText(value ?? UserSettings.Interface.NewFilename, EntryAssemblyInfo.Title);
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        public TaxonomicIndex Data { set; get; }

        public bool IsChanged { set; get; }

        bool wasPlain;



        private void loadData(string filename)
        {
            FileName = filename;
            Data.Read(FileName);

            listViewRepresence.Groups.Clear();
            taxaTreeView.LoadTree();

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
            Data = new TaxonomicIndex();
            taxaTreeView.Bind(Data);

            listViewRepresence.Items.Clear();
            taxaTreeView.Nodes.Clear();

            clearKey();
            clearPictures();
        }



        private ListViewGroup getGroup(TaxonomicIndex.TaxonRow taxonRow)
        {
            return new ListViewGroup()
            {
                Header = taxonRow.InterfaceString,
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

        private void applyRename()
        {
            foreach (ListViewGroup group in listViewRepresence.Groups)
            {
                group.Header = group.Tag is TaxonomicIndex.TaxonRow tr ?
                    tr.InterfaceString : 
                    string.Format(Resources.Interface.VariaCount, Data.GetRootSpeciesRows().Length);
            }

            listViewRepresence.Shine();
        }



        private void updateTaxonData(TaxonomicIndex.TaxonRow taxonRow)
        {
            if (taxonRow == null)
            {
                labelSpecies.Text = 
                labelClassification.Text = 
                string.Empty;
            }
            else
            {
                if (!taxonRow.IsHigher) taxonRow = taxonRow.ValidRecord;

                labelSpecies.Text = taxonRow.FullName;
                labelClassification.SetPathAsText(taxonRow, "s");
                updateRepresenceSpecies(taxonRow.GetSpeciesRows(true));
            }
        }

        private void updateRepresenceSpecies(TaxonomicIndex.TaxonRow[] speciesRows)
        {
            wasPlain = checkBoxPlain.Checked;
            taxaTreeView.Enabled = false; 
            processDisplay.StartProcessing(Resources.Interface.StatusLoadingList);

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(backSpcLoader_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backSpcLoader_RunWorkerCompleted);
            bw.RunWorkerAsync(speciesRows);
            //if (!backSpcLoader.IsBusy)
            //{
            //    backSpcLoader.RunWorkerAsync(speciesRows);
            //}
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void editSpecies(TaxonomicIndex.TaxonRow speciesRow)
        {
            speciesRow = speciesRow.ValidRecord; 

            foreach (Form form in Application.OpenForms)
            {
                if (form is EditTaxon editor && editor.TaxonRow == speciesRow)
                {
                    editor.BringToFront();
                    return;
                }
            }

            EditTaxon editSpecies = new EditTaxon(speciesRow);
            editSpecies.FormClosed += new FormClosedEventHandler(editSpecies_FormClosed);
            editSpecies.Show(this);
        }

        private void editSpecies_FormClosed(object sender, FormClosedEventArgs e)
        {
            EditTaxon editSpecies = (EditTaxon)sender;

            if (editSpecies.DialogResult == DialogResult.OK)
            {
                IsChanged = IsChanged || editSpecies.IsChanged;

                // Update node in treeviewkeys

                TreeNode carryNode = taxaTreeView.Nodes.Find(editSpecies.TaxonRow.ID.ToString(), true)?[0];
                carryNode.Remove();
                carryNode = taxaTreeView.AddNode(editSpecies.TaxonRow);
                taxaTreeView.SelectedNode = carryNode;

                applyRename();

                // Apply rename to species item in listView

                foreach (ListView listView in new ListView[] { listViewRepresence, listViewEngagement })
                {
                    ListViewItem item = listView.GetItem(editSpecies.TaxonRow.ID.ToString());
                    if (item == null) continue;

                    item.UpdateItem(editSpecies.TaxonRow);
                    listView.Shine();
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

        public TaxonomicIndex.StepRow SelectedStep
        {
            get
            {
                if (IsStepNodeSelected)
                {
                    return (TaxonomicIndex.StepRow)treeViewStep.SelectedNode.Tag;
                }

                if (IsFeatureNodeSelected)
                {
                    return (TaxonomicIndex.StepRow)treeViewStep.SelectedNode.Parent.Tag;
                }

                if (IsStateNodeSelected)
                {
                    return (TaxonomicIndex.StepRow)treeViewStep.SelectedNode.Parent.Parent.Tag;
                }

                return null;
            }
        }

        public TaxonomicIndex.FeatureRow SelectedFeature
        {
            get
            {
                if (IsFeatureNodeSelected)
                {
                    return (TaxonomicIndex.FeatureRow)treeViewStep.SelectedNode.Tag;
                }

                if (IsStateNodeSelected)
                {
                    return (TaxonomicIndex.FeatureRow)treeViewStep.SelectedNode.Parent.Tag;
                }

                return null;
            }
        }

        public TaxonomicIndex.StateRow SelectedState
        {
            get
            {
                if (IsStateNodeSelected)
                {
                    return (TaxonomicIndex.StateRow)treeViewStep.SelectedNode.Tag;
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

            foreach (TaxonomicIndex.StepRow stepRow in Data.Step)
            {
                TreeNode stepNode = new TreeNode();
                stepNode.Name = stepRow.ID.ToString();
                stepNode.Tag = stepRow;
                stepNode.Text = string.Format(Resources.Diagnosis.Step, stepRow.ID);
                stepNode.ContextMenuStrip = contextStep;
                treeViewStep.Nodes.Add(stepNode);

                foreach (TaxonomicIndex.FeatureRow featureRow in stepRow.GetFeatureRows())
                {
                    TreeNode featureNode = new TreeNode();
                    featureNode.Name = featureRow.ID.ToString();
                    featureNode.Tag = featureRow;
                    featureNode.Text = featureRow.Title;
                    featureNode.ContextMenuStrip = contextFeature;
                    stepNode.Nodes.Add(featureNode);

                    foreach (TaxonomicIndex.StateRow stateRow in featureRow.GetStateRows())
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
            foreach (TaxonomicIndex.TaxonRow speciesRow in Data.GetSpeciesRows())
            {
                ListViewItem item = listViewEngagement.CreateItem(speciesRow);
                item.Group = speciesRow.GetStateRows().Length == 0 ?
                    listViewEngagement.Groups[1] : listViewEngagement.Groups[0];
            }

            labelEngagedCount.UpdateStatus(listViewEngagement.Groups[0].Items.Count);
        }

        private void rearrangeEnagagedItems(TaxonomicIndex.StepRow selectedStep)
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
