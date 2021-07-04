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
            ClearTaxa();


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

            if (IsBaseNodeSelected)
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
                        ListViewGroup taxonGroup = new ListViewGroup(taxaRow.Taxon);
                        taxonGroup.Name = taxaRow.Taxon;
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

            if (IsTaxaNodeSelected)
            {
                if (SelectedTaxon == null) // Varia or new taxon
                {
                    if (treeViewTaxa.SelectedNode.Name.Contains("Other_"))
                    {
                        UpdateRepresence(SelectedBase.Varia);
                    }
                    else
                    {
                        listViewRepresence.Items.Clear();
                        labelRepCount.UpdateStatus(0);
                    }
                }
                else
                {
                    UpdateRepresence(SelectedTaxon.GetSpecies());
                }
            }

            labelTaxCount.UpdateStatus(IsAllSpeciesShown ? Data.Taxa.Count :
                SelectedBase == null ? 0 : SelectedBase.GetTaxaRows().Length);

            menuItemAddSpecies.Enabled = IsAllSpeciesShown || IsTaxaNodeSelected;
        }

        private void UpdateRepresence(SpeciesKey.SpeciesRow[] speciesRows)
        {
            treeViewTaxa.Enabled = false;
            backSpcLoader.RunWorkerAsync(speciesRows);
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

        private void ClearTaxa()
        {
            // Clear tree
            for (int i = 0; i < treeViewTaxa.Nodes.Count; i++)
            {
                if (treeViewTaxa.Nodes[i].Name == "AllSpecies")
                {
                    continue;
                }

                treeViewTaxa.Nodes[i].Remove();
                i--;
            }

            // Clear species list
            listViewRepresence.Items.Clear();
        }
    }
}
