using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Mayfly.TaskDialogs;
using System.IO;
using System.Reflection;
using Mayfly.Extensions;
using Mayfly.Software;

namespace Mayfly.Species
{
    public partial class MainForm : Form
    {
        #region Properties

        private string fileName;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? FileSystem.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
                fileName = value;
            }

            get
            {
                return fileName;
            }
        }

        public SpeciesKey Data { set; get; }

        public bool IsAllSpeciesShown
        {
            get
            {
                if (treeViewTaxa.SelectedNode == null)
                {
                    return false;
                }
                else
                {
                    return treeViewTaxa.SelectedNode.Name == "AllSpecies";
                }
            }
        }

        public bool IsBaseNodeSelected
        {
            get
            {
                if (IsAllSpeciesShown)
                {
                    return false;
                }
                else
                {
                    if (treeViewTaxa.SelectedNode == null)
                    {
                        return false;
                    }
                    else
                    {
                        return treeViewTaxa.SelectedNode.Level == 0 &&
                            treeViewTaxa.SelectedNode.Name != "AllSpecies";
                    }
                }
            }
        }

        public bool IsTaxaNodeSelected
        {
            get
            {
                if (treeViewTaxa.SelectedNode == null)
                {
                    return false;
                }
                else
                {
                    return treeViewTaxa.SelectedNode.Level == 1;
                }
            }
        }

        public SpeciesKey.BaseRow SelectedBase
        {
            get
            {
                if (IsBaseNodeSelected)
                {
                    return (SpeciesKey.BaseRow)treeViewTaxa.SelectedNode.Tag;
                }

                if (IsTaxaNodeSelected)
                {
                    return (SpeciesKey.BaseRow)treeViewTaxa.SelectedNode.Parent.Tag;
                }
                return null;
            }
        }

        public SpeciesKey.TaxaRow SelectedTaxa
        {
            get
            {
                if (IsTaxaNodeSelected)
                {
                    return (SpeciesKey.TaxaRow)treeViewTaxa.SelectedNode.Tag;
                }
                return null;
            }
        }

        #endregion

        public bool InWork { get; private set; }

        public SpeciesKey.SpeciesRow SelectedSpecies { get; private set; }

        public MainForm()
        { 
            InitializeComponent();

            InWork = false;

            treeViewTaxa.Shine();
            listViewRepresence.Shine();

            treeViewFeatures.Shine();
            listViewFits.Shine();

            Data = new SpeciesKey();
            FileName = null;
        }

        public MainForm(string fileName) : this()
        {
            LoadData(fileName);
        }

        public MainForm(string fileName, bool inWork) : this(fileName)
        {
            InWork = inWork;

            if (inWork)
            {
                tabControl.SelectedTab = tabPageKey;
            }
        }

        public MainForm(SpeciesKey data)
            : this()
        {
            Text = "Guide";

            if (data.IsKeyAvailable)
            {
                Data = data;
                LoadList();
                LoadKey();
                LoadFit();
                InWork = true;
                tabControl.SelectedTab = tabPageKey;
            }
            else
            {
                throw new ArgumentNullException(
                    "Definition key is empty.");
            }
        }

        public MainForm(SpeciesKey data, SpeciesKey.StepRow step)
            : this(data)
        {
            definitionPanel.GetTo(step);
        }

        #region Methods

        private void LoadData(string fileName)
        {
            FileName = fileName;
            Data.ReadXml(FileName);

            LoadList();

            if (Data.IsKeyAvailable)
            {
                LoadKey();
                LoadFit();
            }
            else
            {
                tabPageKey.Parent =
                    tabPageFit.Parent = null;
            }

            //status1.Message(Resources.Messages.Loaded);
        }

        public void ToSpecies(string species)
        {
            // Find species row
            SpeciesKey.SpeciesRow speciesRow = Data.Species.FindBySpecies(species);

            if (speciesRow == null) return;

            // Select it's item in list
            ListViewItem spcItem = listViewRepresence.GetItem(speciesRow.ID.ToString());
            if (spcItem != null)
            {
                listViewRepresence.SelectedItems.Clear();
                spcItem.Selected = true;
                spcItem.EnsureVisible();
            }

            if (speciesRow.GetStateRows().Length > 0)
            {
                // Select it's item in fits
                foreach (SpeciesKey.StateRow stateRow in speciesRow.GetStateRows())
                {
                    TreeNode stateNode = treeViewFeatures.GetNode(stateRow.ID.ToString());
                    if (stateNode != null)
                    {
                        stateNode.Parent.ExpandAll();
                        stateNode.Checked = true;
                    }
                }
                
                ListViewItem fitItem = listViewFits.GetItem(speciesRow.ID.ToString());
                if (fitItem != null) fitItem.Selected = true;

                // Follow to it in key
                definitionPanel.GetTo(speciesRow.GetStateRows()[0]);
            }
        }

        private void SelectSpecies(SpeciesKey.SpeciesRow speciesRow)
        {
            SpeciesCard card = new SpeciesCard(speciesRow, InWork);

            this.Hide();
            definitionPanel.HideFits();
            definitionPanel.HideHistory();

            card.FormClosed += card_FormClosed;
            card.ShowDialog();
        }

        void card_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.OK)
            {
                SelectedSpecies = ((SpeciesCard)sender).SpeciesRow;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                Show();

                if (checkBoxFits.Checked) definitionPanel.ShowFits();
                if (checkBoxDiagnosis.Checked) definitionPanel.ShowHistory();
            }
        }

        #region Taxa tab

        private void LoadList()
        {
            FillTaxaTree();
            FillRepresenceList();
        }

        private void FillTaxaTree()
        {
            foreach (SpeciesKey.BaseRow baseRow in Data.Base.Rows)
            {
                TreeNode baseNode = new TreeNode
                {
                    Tag = baseRow,
                    Text = baseRow.BaseName
                };
                treeViewTaxa.Nodes.Add(baseNode);

                foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows())
                {
                    TreeNode taxaNode = new TreeNode
                    {
                        Tag = taxaRow,
                        Text = taxaRow.TaxonName
                    };
                    baseNode.Nodes.Add(taxaNode);
                }                
            }

            labelTaxCount.UpdateStatus(Data.Taxa.Count);

            UpdateOthers();
            treeViewTaxa.ExpandAll();
            treeViewTaxa.Sort();
        }

        private void FillRepresenceList()
        {
            FillRepresenceList(Data.Species.GetSorted());
            try
            {
                listViewRepresence.Items[0].Selected = true;
            }
            catch { }
        }

        private void FillRepresenceList(SpeciesKey.SpeciesRow[] speciesRows)
        {
            foreach (SpeciesKey.SpeciesRow speciesRow in speciesRows)
            {
                ListViewItem item = listViewRepresence.CreateItem(speciesRow);

                if (IsBaseNodeSelected)
                {
                    SpeciesKey.TaxaRow taxaRow = speciesRow.GetTaxon(SelectedBase);

                    if (taxaRow == null)
                    {
                        item.Remove();
                    }
                    else
                    {
                        item.Group = listViewRepresence.CreateGroup(taxaRow.TaxonName);
                        item.Group.Tag = taxaRow;
                    }
                }

                if (IsTaxaNodeSelected)
                {
                    if (SelectedTaxa != speciesRow.GetTaxon(SelectedBase))
                    {
                        item.Remove();
                    }
                }
            }
            
            listViewRepresence.Shine();
            labelSpcCount.UpdateStatus(listViewRepresence.Items.Count);
        }

        private void UpdateOthers()
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
                        TreeNode newOthersNode = new TreeNode(String.Format(Resources.Interface.Others, count));
                        newOthersNode.Name = "Other_" + baseNode.Name;
                        baseNode.Nodes.Add(newOthersNode);
                    }
                    else
                    {
                        othersNode[0].Text = String.Format(Resources.Interface.Others, count);
                    }
                }
            }
        }

        #endregion

        #region Key tab

        private void LoadKey()
        {
            definitionPanel.SetFeatures(Data.InitialDefinitionStep);
        }

        private void UpdateTaxaDirections(SpeciesKey.StepRow currentStep)
        {
            menuItemGotoTaxon.DropDownItems.Clear();
            contextGotoTaxon.DropDownItems.Clear();

            menuItemToTaxon.DropDownItems.Clear();
            contextToTaxon.DropDownItems.Clear();

            foreach (SpeciesKey.StateRow stateRow in currentStep.ForestandingTaxaStates)
            {
                menuItemGotoTaxon.DropDownItems.Add(TaxonItem(stateRow, Goto_Click));
                contextGotoTaxon.DropDownItems.Add(TaxonItem(stateRow, Goto_Click));
                menuItemToTaxon.DropDownItems.Add(TaxonItem(stateRow, To_Click));
                contextToTaxon.DropDownItems.Add(TaxonItem(stateRow, To_Click));
            }

            menuItemGotoTaxon.SortItems();
            contextGotoTaxon.SortItems();
            menuItemToTaxon.SortItems();
            contextToTaxon.SortItems();

            menuItemGotoTaxon.Enabled = contextGotoTaxon.Enabled = menuItemGotoTaxon.DropDownItems.Count > 0;
            menuItemToTaxon.Enabled = contextToTaxon.Enabled = menuItemToTaxon.DropDownItems.Count > 0;
        }

        private ToolStripItem TaxonItem(SpeciesKey.StateRow stateRow, EventHandler eventHandler)
        {
            ToolStripItem result = new ToolStripMenuItem();
            result.Name = stateRow.ID.ToString();
            result.Text = stateRow.TaxaRow.GetStateRows().Length == 1 ?
                stateRow.TaxaRow.TaxonName :
                string.Format("{0} ({1}: {2}...)", stateRow.TaxaRow.TaxonName, stateRow.FeatureRow.Title,
                    stateRow.Description.Substring(0, Math.Min(15, stateRow.Description.Length)));
            result.Click += eventHandler;
            return result;
        }

        private void UpdateSpeciesDirections(SpeciesKey.StepRow currentStep)
        {
            menuItemGotoSpecies.DropDownItems.Clear();
            contextGotoSpecies.DropDownItems.Clear();

            menuItemToSpecies.DropDownItems.Clear();
            contextToSpecies.DropDownItems.Clear();

            foreach (SpeciesKey.StateRow stateRow in currentStep.ForestandingSpeciesStates)
            {
                menuItemGotoSpecies.DropDownItems.Add(SpeciesItem(stateRow, Goto_Click));
                contextGotoSpecies.DropDownItems.Add(SpeciesItem(stateRow, Goto_Click));
                menuItemToSpecies.DropDownItems.Add(SpeciesItem(stateRow, To_Click));
                contextToSpecies.DropDownItems.Add(SpeciesItem(stateRow, To_Click));
            }

            menuItemGotoSpecies.SortItems();
            contextGotoSpecies.SortItems();
            menuItemToSpecies.SortItems();
            contextToSpecies.SortItems();

            menuItemGotoSpecies.Enabled = contextGotoSpecies.Enabled = menuItemGotoSpecies.DropDownItems.Count > 0;
            menuItemToSpecies.Enabled = contextToSpecies.Enabled = menuItemToSpecies.DropDownItems.Count > 0;
        }

        private ToolStripItem SpeciesItem(SpeciesKey.StateRow stateRow, EventHandler eventHandler)
        {
            ToolStripItem result = new ToolStripMenuItem();
            result.Name = stateRow.ID.ToString();

            result.Text = stateRow.SpeciesRow.GetStateRows().Length == 1 ?
                stateRow.SpeciesRow.Name :
                string.Format("{0} ({1}: {2}...)", stateRow.SpeciesRow.Name, stateRow.FeatureRow.Title,
                stateRow.Description.Substring(0, Math.Min(15, stateRow.Description.Length)));

            result.Click += eventHandler;
            return result;
        }

        #endregion

        #region Fit tab

        private void LoadFit()
        {
            FillFeaturesTree();
        }

        private void FillFeaturesTree()
        {
            foreach (SpeciesKey.FeatureRow featureRow in Data.Feature)
            {
                TreeNode featureNode = new TreeNode();
                featureNode.Tag = featureRow;
                featureNode.Text = featureRow.Title;
                treeViewFeatures.Nodes.Add(featureNode);

                foreach (SpeciesKey.StateRow stateRow in featureRow.GetStateRows())
                {
                    TreeNode stateNode = new TreeNode();
                    stateNode.Name = stateRow.ID.ToString();
                    stateNode.Tag = stateRow;
                    stateNode.Text = stateRow.Description;
                    featureNode.Nodes.Add(stateNode);
                }
            }

            labelFtrCount.UpdateStatus(0);
            labelFitCount.UpdateStatus(listViewFits.Items.Count);

            //treeViewFeatures.ExpandAll();
            //treeViewFeatures.Sort();
        }

        private SpeciesKey.StateRow[] GetCheckedStates()
        {
            List<SpeciesKey.StateRow> result = new List<SpeciesKey.StateRow>();

            foreach (TreeNode featureNode in treeViewFeatures.Nodes)
            {
                foreach (TreeNode stateNode in featureNode.Nodes)
                {
                    if (stateNode.Checked)
                    {
                        result.Add(stateNode.Tag as SpeciesKey.StateRow);
                    }
                }
            }

            return result.ToArray();
        }

        private void FillFitList(SpeciesKey.SpeciesRow[] speciesRows)
        {
            foreach (SpeciesKey.SpeciesRow speciesRow in speciesRows)
            {
                listViewFits.CreateItem(speciesRow);
            }

            labelFitCount.UpdateStatus(listViewFits.Items.Count);
        }

        #endregion

        #endregion

        #region Interface

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            menuKeys.Visible = tabControl.SelectedTab == tabPageKey;
        
            if (tabControl.SelectedTab == tabPageKey)
            {
                if (checkBoxFits.Checked) definitionPanel.ShowFits();
                if (checkBoxDiagnosis.Checked) definitionPanel.ShowHistory();
            }
            else
            {
                definitionPanel.HideFits();
                definitionPanel.HideHistory();
            }
        }

        #region Menus

        #region File menu

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

        #region Keys menu

        private void menuItemRestart_Click(object sender, EventArgs e)
        {
            definitionPanel.Restart();
        }

        private void menuItemBack_Click(object sender, EventArgs e)
        {
            definitionPanel.GetBack();
        }

        private void Goto_Click(object sender, EventArgs e)
        {
            SpeciesKey.StateRow stateRow = Data.State.FindByID(Convert.ToInt32(((ToolStripMenuItem)sender).Name));
            definitionPanel.GetTo(stateRow);            
        }

        private void To_Click(object sender, EventArgs e)
        {
            SpeciesKey.StateRow stateRow = Data.State.FindByID(Convert.ToInt32(((ToolStripMenuItem)sender).Name));
            definitionPanel.GetForward(stateRow);
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
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }

        #endregion

        #region Taxa and species

        private void treeViewTaxa_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listViewRepresence.Items.Clear();
            listViewRepresence.ShowGroups = IsBaseNodeSelected;

            if (IsAllSpeciesShown)
            {
                FillRepresenceList();
            }

            if (IsBaseNodeSelected)
            {
                SpeciesKey.TaxaRow[] taxaRows = SelectedBase.GetTaxaRows();

                labelTaxCount.UpdateStatus(taxaRows.Length);

                foreach (SpeciesKey.TaxaRow taxaRow in taxaRows)
                {
                    FillRepresenceList(taxaRow.GetSpecies());
                }
            }

            if (IsTaxaNodeSelected)
            {
                labelTaxCount.UpdateStatus(SelectedBase.GetTaxaRows().Length);

                if (e.Node.Tag is SpeciesKey.TaxaRow)
                {
                    FillRepresenceList(SelectedTaxa.GetSpecies());
                }
                else
                {
                    FillRepresenceList(SelectedBase.Varia);
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

        private void listViewSpecies_ItemActivate(object sender, EventArgs e)
        {
            if (listViewRepresence.SelectedItems.Count > 0)
            {
                SelectSpecies(listViewRepresence.SelectedItems[0].Tag as SpeciesKey.SpeciesRow);
            }
        }

        #endregion

        #region Key

        private void definitionPanel_SpeciesDefined(object sender, Controls.StateClickedEventArgs e)
        {
            SelectSpecies(e.SpeciesRow);
        }

        private void definitionPanel_StepChanged(object sender, Controls.DefinitionEventArgs e)
        {
            buttonBack.Enabled = menuItemBack.Enabled = definitionPanel.Selections.Count > 0;
            UpdateTaxaDirections(e.CurrentStep);
            UpdateSpeciesDirections(e.CurrentStep);
        }

        private void definitionPanel_UserSelectedState(object sender, Controls.StateClickedEventArgs e)
        {
            if (e.IsTaxonAttached)
            {
                status1.Message(e.TaxaRow.FullName);
            }
        }

        private void checkBoxHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDiagnosis.Checked)
            {
                definitionPanel.ShowHistory();
            }
            else
            {
                definitionPanel.HideHistory();
            }
        }

        private void checkBoxFits_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFits.Checked)
            {
                definitionPanel.ShowFits();
            }
            else
            {
                definitionPanel.HideFits();
            }
        }
    
        #endregion

        #region Fits

        private void treeViewFeatures_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 1) return;

            if (e.Node.Checked)
            {
                foreach (TreeNode treeNode in e.Node.Parent.Nodes)
                {
                    if (treeNode == e.Node) continue;
                    treeNode.Checked = false;
                }

                labelFtrCount.UpdateStatus(GetCheckedStates().Length);
            }

            listViewFits.Items.Clear();
            FillFitList(Data.GetByFeatureStates(GetCheckedStates()));
        }

        private void treeViewFeatures_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(treeViewFeatures.BackColor),
                    e.Node.Bounds);

                //using (SolidBrush brush = new SolidBrush(backColor))
                //{
                //}

                //TextRenderer.DrawText(e.Graphics, e.Node.Text, this.TvOne.Font, e.Node.Bounds, foreColor, backColor);
            }

            //if ((e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused)
            //{
            //    ControlPaint.DrawFocusRectangle(e.Graphics, e.Node.Bounds, foreColor, backColor);
            //}
            e.DrawDefault = false;
        }

        private void listViewFits_ItemActivate(object sender, EventArgs e)
        {
            if (listViewFits.SelectedItems.Count > 0)
            {
                SelectSpecies(listViewFits.SelectedItems[0].Tag as SpeciesKey.SpeciesRow);
            }
        }

        #endregion

        #endregion

    }
}