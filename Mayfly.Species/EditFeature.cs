using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Species
{
    public partial class EditFeature : Form
    {
        SpeciesKey.FeatureRow FeatureRow;

        private EditFeature()
        {
            InitializeComponent();
        }

        public EditFeature(SpeciesKey.StepRow stepRow) : this()
        {
            FeatureRow = ((SpeciesKey)stepRow.Table.DataSet).Feature.NewFeatureRow();
            FeatureRow.StepRow = stepRow;
        }

        public EditFeature(SpeciesKey.FeatureRow featureRow)
            : this()
        {
            FeatureRow = featureRow;

            if (!FeatureRow.IsTitleNull()) textBoxTitle.Text = FeatureRow.Title;
            if (!FeatureRow.IsDescriptionNull()) textBoxDescription.Text = FeatureRow.Description;

            //flowStates.Controls.Remove(buttonAddState);
            foreach (SpeciesKey.StateRow stateRow in FeatureRow.GetStateRows())
            {
                //StateEditor state = new StateEditor(stateRow);
                //flowStates.Controls.Add(state);
            }
            //flowStates.Controls.Add(buttonAddState);
        }

        private void buttonAddState_Click(object sender, EventArgs e)
        {
            // Create new stateRow?

            //flowStates.Controls.Remove(buttonAddState);
            //StateEditor state = new StateEditor();
            //flowStates.Controls.Add(state);
            //state.Focus();
            //flowStates.Controls.Add(buttonAddState);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            FeatureRow.Title = textBoxTitle.Text;

            if (textBoxDescription.Text.IsAcceptable()) FeatureRow.Description = textBoxDescription.Text;
            else FeatureRow.SetDescriptionNull();
        }

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            buttonOK.Enabled = textBoxTitle.Text.IsAcceptable();
        }
    }
}
