using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mayfly.Species.Controls
{
    public partial class TaxonSelectorPopup : Form
    {
        EventHandler taxonSelected;
        EventHandler treeLoaded;

        [Category("Mayfly Events")]
        public event EventHandler OnTaxonSelected
        {
            add
            {
                taxonSelected += value;
            }

            remove
            {
                taxonSelected -= value;
            }
        }

        [Category("Mayfly Events")]
        public event EventHandler OnTreeLoaded
        {
            add
            {
                treeLoaded += value;
            }

            remove
            {
                treeLoaded -= value;
            }
        }

        public TaxonomicRank DeepestRank { get { return taxaTreeView.DeepestRank; } set { taxaTreeView.DeepestRank = value; } }

        public SpeciesKey.TaxonRow Taxon
        {
            set { taxaTreeView.PickedTaxon = value; }
            get { return taxaTreeView.PickedTaxon; }
        }


        public TaxonSelectorPopup(SpeciesKey data)
        {
            InitializeComponent();
            taxaTreeView.Shine();
            taxaTreeView.Bind(data);
        }



        private void setSelection()
        {
            if (taxonSelected != null) taxonSelected.Invoke(this, new EventArgs());
            Hide();
            this.Owner.BringToFront();
        }

        public void LoadTree()
        {
            taxaTreeView.LoadTree();
            taxaTreeView.ExpandAll();
        }



        private void taxaTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            setSelection();
        }

        private void SelectTaxon_Deactivate(object sender, EventArgs e)
        {
            Hide();
            this.Owner.BringToFront();
        }

        private void taxaTreeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Hide();
                this.Owner.BringToFront();
            }
            else if (e.KeyChar == (char)Keys.Return)
            {
                setSelection();
            }
        }

        private void taxaTreeView_OnTreeLoaded(object sender, EventArgs e)
        {
            if (treeLoaded != null) treeLoaded.Invoke(this, new EventArgs());
        }
    }
}
