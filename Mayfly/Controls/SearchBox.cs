using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Controls
{
    public partial class SearchBox : TextBox
    {
        private string invitation;

        private bool instantSearch;

        /// <summary>
        /// Current search term
        /// </summary>
        [Category("Behaviour"), Localizable(true)]
        public string Invitation
        {
            get { return invitation; }
            set
            {
                invitation = value;
                SetEntering();
            }
        }

        [Category("Behaviour"), Browsable(true)]
        public bool InstantSearch
        {
            get { return instantSearch; }
            set { instantSearch = value; }
        }

        public event EventHandler SearchTermChanged;



        public SearchBox() : base()
        {
            //this.Invitation = "Enter search term here";
            SetEmpty();
        }



        public void SetEmpty()
        {
            this.TextAlign = HorizontalAlignment.Center;
            this.Font = new Font("Segoe UI", 7.25F, FontStyle.Italic);
            this.ForeColor = SystemColors.InactiveCaptionText;

            this.Text = Invitation;
        }

        private void SetEntering()
        {
            this.TextAlign = HorizontalAlignment.Left;
            this.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);
            this.ForeColor = SystemColors.ActiveCaptionText;
        }



        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.Text == Invitation)
            {
                return;
            }

            if (!this.Text.IsAcceptable())
            {
                return;
            }

            if (this.InstantSearch)
            {
                SearchTermChanged.Invoke(this, e);
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (this.Text == Invitation)
            {
                this.Text = string.Empty;
                this.SetEntering();
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (!this.Text.IsAcceptable())
            {
                SetEmpty();
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (!this.InstantSearch && e.KeyCode == Keys.Enter)
            {
                SearchTermChanged.Invoke(this, e);
            }
        }
    }
}
