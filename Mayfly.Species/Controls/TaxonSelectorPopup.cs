using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species.Controls
{
    public partial class TaxonSelectorPopup : Form
    {
        SpeciesKey data;
        EventHandler taxonSelected;
        SpeciesKey.TaxonRow taxon;

        [Category("Mayfly Events")]
        public event EventHandler OnTaxonSelected
        {
            add
            {
                taxonSelected = value;
            }

            remove
            {
                taxonSelected = value;
            }
        }

        public SpeciesKey.TaxonRow Taxon
        {
            set
            {
                taxon = value;
                if (value != null) treeViewDerivates.SelectedNode = treeViewDerivates.Nodes.Find(value.ID.ToString(), true)?[0];
            }
            get { return taxon; }
        }


        public TaxonSelectorPopup(SpeciesKey _data)
        {
            InitializeComponent();
            treeViewDerivates.Shine();

            data = _data;
            treeViewDerivates.Enabled = false;
            Cursor = Cursors.WaitCursor;
            backTreeLoader.RunWorkerAsync();
        }


        private TreeNode getTaxonTreeNode(SpeciesKey.TaxonRow taxonRow)
        {
            TreeNode taxonNode = new TreeNode
            {
                Tag = taxonRow,
                Name = taxonRow.ID.ToString(),
                Text = taxonRow.FullName
            };

            foreach (SpeciesKey.TaxonRow derRow in taxonRow.GetTaxonRows())
            {
                taxonNode.Nodes.Add(getTaxonTreeNode(derRow));
            }

            return taxonNode;
        }

        private void setSelection()
        {
            Taxon = treeViewDerivates.SelectedNode?.Tag as SpeciesKey.TaxonRow;
            if (taxonSelected != null) taxonSelected.Invoke(this, new EventArgs());
            Hide();
        }


        private void backTreeLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (SpeciesKey.TaxonRow taxonRow in data.GetRootTaxon())
            {
                result.Add(getTaxonTreeNode(taxonRow));
            }
            e.Result = result.ToArray();
        }

        private void backTreeLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TreeNode[] results = (TreeNode[])e.Result;
            treeViewDerivates.Nodes.AddRange(results);
            treeViewDerivates.Enabled = true;

            // Tree filled. Now load list
            Cursor = Cursors.Arrow;
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            setSelection();
        }

        private void SelectTaxon_Deactivate(object sender, EventArgs e)
        {
            Hide();
        }

        private void treeViewDerivates_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Hide();
            }
            else if (e.KeyChar == (char)Keys.Return)
            {
                setSelection();
            }
        }
    }
}
