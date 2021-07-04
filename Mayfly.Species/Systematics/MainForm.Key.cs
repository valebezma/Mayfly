using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species.Systematics
{
    partial class MainForm
    {
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
