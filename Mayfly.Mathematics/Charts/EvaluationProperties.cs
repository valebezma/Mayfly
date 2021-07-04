using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;

namespace Mayfly.Mathematics.Charts
{
    public partial class EvaluationProperties : Form
    {
        #region Properties

        public event EvaluationEventHandler ValueChanged;

        public event EvaluationEventHandler StructureChanged;

        public Evaluation Evaluation { get; set; }

        public string EvaluationName
        {
            get { return textBoxSeriesName.Text; }
            set
            {
                textBoxSeriesName.Text = value;
                ResetTitle();
            }
        }

        public bool Borders
        {
            get
            {
                return checkBoxBorder.Checked; 
            }
            
            set
            {
                checkBoxBorder.Checked = value; 
            }
        }

        public double PointWidth
        {
            set { trackBarWidth.Value = (int) (value * 10); }
            get { return (double)trackBarWidth.Value / 10; }
        }

        public Color DataPointColor
        {
            get { return colorDialogColumn.Color; }
            set { colorDialogColumn.Color = panelMarkerColor.BackColor = value; }
        }

        public ErrorBarType Appearance
        {
            get { return (ErrorBarType)comboBoxValueVariant.SelectedIndex; }
            set { comboBoxValueVariant.SelectedIndex = (int)value; }
        }

        public double ConfidenceLevel
        {
            get { return (double)numericUpDownConfidenceLevel.Value / 100; }
            set { numericUpDownConfidenceLevel.Value = 100 * (decimal)value; }
        }

        #endregion

        #region Constructors

        public EvaluationProperties(Evaluation evaluation)
        {
            InitializeComponent();

            Evaluation = evaluation;
            ConfidenceLevel = UserSettings.DefaultConfidenceLevel;
            Appearance = ErrorBarType.StandardError;
        }

        #endregion

        #region Methods

        public void ResetTitle()
        {
            Text = string.Format(Resources.Interface.SeriesProperties, EvaluationName);
        }

        #endregion

        private void valueChanged(object sender, EventArgs e)
        {
            Evaluation.Update(sender, e);

            if (ValueChanged != null && ActiveControl == (Control)sender)
            {
                ValueChanged.Invoke(sender, new EvaluationEventArgs(Evaluation));
            }
        }

        private void panelMarkerColor_Click(object sender, EventArgs e)
        {
            colorDialogColumn.ShowDialog(this);
            panelMarkerColor.BackColor = colorDialogColumn.Color;
            valueChanged(ActiveControl, e);
        }

        private void textBoxSeriesName_TextChanged(object sender, EventArgs e)
        {
            ResetTitle();
            valueChanged(sender, e);
        }

        private void HistogramProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void structureChanged(object sender, EventArgs e)
        {
            if (StructureChanged != null && ActiveControl == (Control)sender)
            {
                StructureChanged.Invoke(sender, new EvaluationEventArgs(Evaluation));
            }

            Evaluation.BuildSeries();
        }

        private void comboBoxValueVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDownConfidenceLevel.Enabled = labelCL.Enabled = 
                (Appearance == ErrorBarType.ConfidenceInterval);
            valueChanged(sender, e);
        }
    }
}
