using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species.Controls
{
    public partial class TaxonSelector : System.Windows.Forms.TextBox
    {
        SpeciesKey data;
        SpeciesKey.TaxonRow taxon;
        EventHandler taxonSelected;
        TaxonEventHandler beforeTaxonSelected;

        TaxonSelectorPopup selectTaxon;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpeciesKey Data
        {
            set
            {
                data = value;
                selectTaxon = new TaxonSelectorPopup(data);
                selectTaxon.OnTaxonSelected += selectTaxon_OnTaxonSelected;
            }

            get
            {
                return data;
            }
        }

        private void selectTaxon_OnTaxonSelected(object sender, EventArgs e)
        {
            if (beforeTaxonSelected != null)
            {
                beforeTaxonSelected.Invoke(this, new TaxonEventArgs(((TaxonSelectorPopup)sender).Taxon));
            }

            if (SelectionAllowed)
            {
                Taxon = ((TaxonSelectorPopup)sender).Taxon;
                if (taxonSelected != null) taxonSelected.Invoke(this, new TaxonEventArgs(Taxon));
            }

            SelectionAllowed = true;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpeciesKey.TaxonRow Taxon
        {
            set
            {
                taxon = value;
                base.Text = taxon == null ? this.UnselectedLabel : taxon.FullName;
            }

            get
            {
                return taxon;
            }
        }

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

        [Browsable(true), DefaultValue("Not selected")]
        public string UnselectedLabel { get; set; }

        internal bool SelectionAllowed = true;

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

        [Category("Mayfly Events")]
        public event TaxonEventHandler BeforeTaxonSelected
        {
            add
            {
                beforeTaxonSelected = value;
            }

            remove
            {
                beforeTaxonSelected = value;
            }
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

        public TaxonSelector()
        {
            InitializeComponent();
            base.ReadOnly = true;
            base.WordWrap = false;
        }

        public TaxonSelector(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            base.ReadOnly = true;
            base.WordWrap = false;
        }

        private void runSelectionForm()
        {
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
                if (Taxon != null) selectTaxon.Taxon = Taxon;
                selectTaxon.Show(FindForm());
            }
        }
    }
}
