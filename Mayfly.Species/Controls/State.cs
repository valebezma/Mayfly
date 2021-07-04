using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Species.Controls
{
    public partial class State : UserControl
    {
        SpeciesKey.StateRow stateRow;

        public SpeciesKey.StateRow StateRow
        {
            get { return stateRow; }

            set
            {
                stateRow = value;
                labelState.Text = value.Description;

                //if (value.IsGotoNull())
                //{
                //    if (value.IsSpcIDNull())
                //    {
                //        hoveredBack = Properties.Resources.addnew;
                //    }
                //    else
                //    {
                //        hoveredBack = Properties.Resources.attached;
                //    }
                //}
                //else
                //{
                //    hoveredBack = Properties.Resources.curled;
                //}
            }
        }

        public event StateClickedEventHandler UserClicked;

        //private Bitmap hoveredBack; 

        public State()
        {
            InitializeComponent();
            //hoveredBack = Properties.Resources.hovered;
        }

        public State(SpeciesKey.StateRow stateRow)
            : this()
        {
            StateRow = stateRow;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackgroundImage = Properties.Resources.hovered;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackgroundImage = Properties.Resources.blank;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                this.BackgroundImage = Species.Properties.Resources.pressed;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
                this.BackgroundImage = Species.Properties.Resources.hovered;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (StateRow.IsGotoNull())
            {
                if (StateRow.IsSpcIDNull())
                {
                    Mayfly.Service.PlaySound(Species.Properties.Resources.wrongthesis);
                }
            }
            else
            {
                Mayfly.Service.PlaySound(Species.Properties.Resources.newthesissound);
            }

            if (UserClicked != null) {

                UserClicked.Invoke(this, new StateClickedEventArgs(this));
            }
        }

        private void labelState_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void labelState_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void labelState_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
    }
}
