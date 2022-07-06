using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Drawing;

namespace Mayfly.Species.Controls
{
    public partial class TaxonBox : System.Windows.Forms.TextBox
    {
        TaxonomicIndex data;
        TaxonomicIndex.TaxonRow taxon;
        EventHandler taxonSelected;
        EventHandler treeLoaded;
        TaxonEventHandler beforeTaxonSelected;
        TaxonBoxPopup selectTaxon;
        TaxonomicRank deepestRank;
        internal bool SelectionAllowed = true;


        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TaxonomicIndex Data
        {
            set
            {
                data = value;

                if (AllowSelect)
                {
                    selectTaxon = new TaxonBoxPopup(data);
                    selectTaxon.OnTaxonSelected += selectTaxon_OnTaxonSelected;
                    selectTaxon.OnTreeLoaded += selectTaxon_OnTreeLoaded;
                }
            }

            get
            {
                return data;
            }
        }

        private void selectTaxon_OnTreeLoaded(object sender, EventArgs e)
        {
            if (treeLoaded != null) treeLoaded.Invoke(this, new EventArgs());
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TaxonomicIndex.TaxonRow Taxon
        {
            set
            {
                taxon = value;

                if (AllowSelect)
                {
                    selectTaxon.Taxon = taxon; 
                }
                resetText();
            }

            get
            {
                return taxon;
            }
        }

        [Browsable(true), DefaultValue("Not selected")]
        public string UnselectedLabel { get; set; }

        [Browsable(true)]
        public bool AllowSelect { get; set; }

        [Browsable(true)]
        public string Format { get; set; }

        //[Browsable(true)]
        //public bool Enabled { get; set; }

        internal TaxonomicRank DeepestRank
        {
            set
            {
                deepestRank = value;
                if (AllowSelect)
                {
                    selectTaxon.DeepestRank = deepestRank;
                    selectTaxon.LoadTree();
                }
            }

            get { return deepestRank; }
        }

        #region Hide some unnessesary properties

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ReadOnly { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string[] Lines { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Multiline { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool WordWrap { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AcceptsReturn { get; set; }

        [Browsable(false), Obsolete, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AcceptsTab { get; set; }

        #endregion

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
        public event TaxonEventHandler BeforeTaxonSelected
        {
            add
            {
                beforeTaxonSelected += value;
            }

            remove
            {
                beforeTaxonSelected -= value;
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


        public TaxonBox()
        {
            InitializeComponent();
            base.ReadOnly = true;
            base.WordWrap = false;
        }

        public TaxonBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            base.ReadOnly = true;
            base.WordWrap = false;
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button == MouseButtons.Left)
            {
                runSelectionForm();
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == (char)Keys.Back)
            {
                Taxon = null;
            }
            else if (e.KeyChar == (char)Keys.Return)
            {
                runSelectionForm();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            resetText();
        }



        private void runSelectionForm()
        {
            if (!AllowSelect) return;
            if (Data == null) return;
            if (Data.Taxon.Count == 0) return;
            selectTaxon.Width = this.Width;
            selectTaxon.SnapToTop(this);


            if (selectTaxon.Visible)
            {
                selectTaxon.BringToFront();
            }
            else
            {
                selectTaxon.Show(FindForm());
            }
        }

        private void resetText()
        {
            if (taxon == null) {
                base.Text = this.UnselectedLabel;
                return;
            }

            this.SetPathAsText(taxon, Format);
        }

        private void selectTaxon_OnTaxonSelected(object sender, EventArgs e)
        {
            if (beforeTaxonSelected != null)
            {
                beforeTaxonSelected.Invoke(this, new TaxonEventArgs(((TaxonBoxPopup)sender).Taxon));
            }

            if (SelectionAllowed)
            {
                taxon = ((TaxonBoxPopup)sender).Taxon;
                resetText();
                if (taxonSelected != null) taxonSelected.Invoke(this, new TaxonEventArgs(taxon));
            }

            SelectionAllowed = true;
        }
    }
}
