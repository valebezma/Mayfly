using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Species;

namespace Mayfly.Species.Controls
{
    public partial class Feature : UserControl
    {
        #region Properties

        public static Size CollapsedSize { get { return new Size(300, 45); } }

        private static int sizeStep = 100;

        public Size ExpandedSize { get; set; }

        public List<State> States;

        public SpeciesKey.FeatureRow featureRow;

        public SpeciesKey.FeatureRow FeatureRow
        {
            get
            {
                return featureRow;
            }

            set
            {
                featureRow = value;

                if (value == null)
                {
                    labelTitle.Text =
                    labelDescription.Text = string.Empty;
                    States.Clear();
                    flowStates.Controls.Clear();
                }
                else
                {
                    labelTitle.Text = value.Title;
                    labelDescription.Text = value.Description;
                    totalWidth = 0;

                    foreach (SpeciesKey.StateRow stateRow in value.GetStateRows())
                    {
                        State state = new State(stateRow);

                        States.Add(state);
                        flowStates.Controls.Add(state);
                        state.UserClicked += state_UserClicked;

                        totalWidth += state.Width;
                    }
                }
            }
        }

        public event StateClickedEventHandler UserSelectedState;

        void state_UserClicked(object sender, StateClickedEventArgs e)
        {
            if (UserSelectedState != null) {

                UserSelectedState.Invoke(sender, e);
            }
        }

        public bool IsExpanded;

        public event EventHandler Expanded;

        public event EventHandler Collapsed;

        #endregion

        private int totalWidth;

        public Feature()
        {
            InitializeComponent();
            States = new List<State>();
            ExpandedSize = Size;

            //Collapse();
        }

        public Feature(SpeciesKey.FeatureRow featRow) : this()
        {
            FeatureRow = featRow;

            Size = CollapsedSize;
            IsExpanded = false;
        }

        #region Methods
        
        public void Collapse()
        {
            timerCollapse.Enabled = true;

            //Size = MaximumSize = MinimumSize = CollapsedSize;

            //IsExpanded = false;
            
            //if (Collapsed != null) {
            //    Collapsed.Invoke(this, new EventArgs());
            //}
        }

        private void timerCollapse_Tick(object sender, EventArgs e)
        {
            if (Size == CollapsedSize)
            {
                timerCollapse.Enabled = false;

                IsExpanded = false;

                if (Collapsed != null)
                {
                    Collapsed.Invoke(this, new EventArgs());
                }

                labelTitle.Width = labelDescription.Width = CollapsedSize.Width - Padding.Left - Padding.Right;
                labelTitle.Left = labelDescription.Left = Padding.Left;
            }
            else
            {
                Size = reducedSize();
            }
        }

        private Size reducedSize()
        {
            Size result = new Size();

            result.Height = Math.Max(CollapsedSize.Height, Size.Height - sizeStep);
            result.Width = Math.Max(CollapsedSize.Width, Size.Width - sizeStep);

            return result;
        }

        public void Expand()
        {
            timerExpand.Enabled = true;
        }

        private void timerExpand_Tick(object sender, EventArgs e)
        {
            if (this.Size == this.ExpandedSize)
            {
                timerExpand.Enabled = false;
                IsExpanded = true;

                if (Expanded != null)
                {
                    Expanded.Invoke(this, new EventArgs());
                }

                labelTitle.Width = labelDescription.Width = totalWidth;
                labelTitle.Left = labelDescription.Left = 
                    Math.Max(Padding.Left, (Width - totalWidth) / 2);
            }
            else
            {
                this.Size = increasedSize();
            }
        }

        private Size increasedSize()
        {
            Size result = new Size();

            result.Height = Math.Min(ExpandedSize.Height, Size.Height + sizeStep);
            result.Width = Math.Min(ExpandedSize.Width, Size.Width + sizeStep);

            return result;
        }

        #endregion

        protected override void OnClick(EventArgs e)
        {
             base.OnClick(e);

            if (IsExpanded) {
                Collapse();
            }
            else {
                Expand();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            //Collapse();
        }

        private void Feature_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
            Update();

            if (Parent !=null)
            {
                Parent.Refresh();
                Parent.Update();
            }
            
            //if (IsExpanded) ExpandedSize = Size;
        }

        private void labelTitle_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
    }
}
