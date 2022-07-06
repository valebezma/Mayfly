using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;
using System.Drawing.Drawing2D;

namespace Mayfly.Species.Systematics
{
    public partial class StateEditor : UserControl
    {
        public bool IsImageAttached
        {
            get
            {
                return false;
            }
        }

        public TaxonomicIndex.StateRow StateRow;

        public StateEditor()
        {
            InitializeComponent();

            textBoxDescription.Focus();
        }

        public StateEditor(TaxonomicIndex.StateRow stateRow)
            : this()
        {
            StateRow = stateRow;

            textBoxDescription.Text = StateRow.Description;

            labelSpecies.Visible = !StateRow.IsTaxIDNull();
            if (!StateRow.IsTaxIDNull()) labelSpecies.Text = StateRow.TaxonRow.Name;

            SetAppear();
        }

        private void SetAppear()
        {
            if (StateRow.IsGotoNull())
            {
                if (StateRow.IsTaxIDNull())
                {
                    this.BackgroundImage = Species.Properties.Resources.addnew;
                }
                else
                {
                    this.BackgroundImage = Species.Properties.Resources.attached;
                }
            }
            else
            {
                this.BackgroundImage = Species.Properties.Resources.curled;
            }
        }

        private void buttonBond_Click(object sender, EventArgs e)
        {
            contextBond.Show(buttonBond, Point.Empty,  ToolStripDropDownDirection.AboveRight);
        }

        private void textBoxState_Enter(object sender, EventArgs e)
        {
            buttonBond.Visible = true;

            //textBoxDescription.Width -= buttonBond.Width + buttonBond.Margin.Left + textBoxDescription.Margin.Right;
        }

        private void textBoxState_Leave(object sender, EventArgs e)
        {
            if (!textBoxDescription.Text.IsAcceptable())
            {
                if (StateRow == null) this.Dispose();
                else textBoxDescription.Text = StateRow.Description;
            }


            //textBoxDescription.Width += buttonBond.Width + buttonBond.Margin.Left + textBoxDescription.Margin.Right;
            buttonBond.Visible = false;
        }

        private void textBoxState_TextChanged(object sender, EventArgs e)
        {
            buttonBond.Enabled = textBoxDescription.Text.IsAcceptable();
        }

        private void textBoxDescription_SizeChanged(object sender, EventArgs e)
        {
            //pictureBoxGoto.Left = textBoxDescription.Bounds.Right - pictureBoxGoto.Width;
        }

        private void StateEditor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && 
                e.Location.X > 200 && e.Location.Y > 250)
            {
                if (StateRow.IsGotoNull())
                {
                    // create new step, feature and open feature
                    if (StateRow.IsTaxIDNull())
                    {
                        EditFeature editFeature = new EditFeature(((TaxonomicIndex)StateRow.Table.DataSet).Step.NewStepRow());
                        editFeature.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.NextToHost);
                        editFeature.Show();
                    }
                }
                else
                {
                    foreach (TaxonomicIndex.FeatureRow featureRow in StateRow.Next.GetFeatureRows())
                    {
                        EditFeature editFeature = new EditFeature(featureRow);
                        editFeature.SetFriendlyDesktopLocation(this.FindForm(), FormLocation.NextToHost);
                        editFeature.Show();
                    }
                }
            }
        }

    }
}
