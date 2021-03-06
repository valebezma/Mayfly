using Mayfly.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mayfly.Mathematics.Charts
{
    public partial class HistogramProperties : Form
    {
        public event HistogramEventHandler ValueChanged;

        public Histogramma Histogram { get; set; }

        public string HistogramName {
            get { return textBoxSeriesName.Text; }
            set {
                textBoxSeriesName.Text = value;
                ResetTitle();
            }
        }

        public string FitName {
            get;
            private set;
        }

        public bool Borders {
            get {
                return checkBoxBorder.Checked;
            }

            set {
                checkBoxBorder.Checked = value;
            }
        }

        public double PointWidth {
            set { trackBarWidth.Value = (int)(value * 10); }
            get { return (double)trackBarWidth.Value / 10; }
        }

        public Color DataPointColor {
            get { return colorBoxColumn.Color; }
            set { colorBoxColumn.Color = value; }
        }

        public Color FitColor {
            get { return colorBoxFit.Color; }
            set { colorBoxFit.Color = value; }
        }

        public int FitWidth {
            get { return trackBarFitWidth.Value; }
            set { trackBarFitWidth.Value = value; }
        }

        public bool AllowBorder {
            get { return radioButtonLower.Enabled; }
            set { radioButtonLower.Enabled = radioButtonUpper.Enabled = label14.Enabled = value; }
        }

        public bool ShowFit {
            get { return checkBoxShowFit.Checked && comboBoxFit.SelectedIndex > -1; }
            set { checkBoxShowFit.Checked = value; }
        }

        public bool ShowAnnotation {
            get {
                return ShowFit && (ShowCount || ShowFitResult);
            }
        }

        public bool ShowCount {
            get { return ShowFit && checkBoxShowCount.Checked; }
            set { checkBoxShowCount.Checked = value; }
        }

        public bool ShowFitResult {
            get { return ShowFit && checkBoxShowFitResult.Checked; }
            set { checkBoxShowFitResult.Checked = value; }
        }

        public BalanceSide SelectedClassBorderType {
            get {
                if (radioButtonLower.Checked) return BalanceSide.Left;
                return BalanceSide.Right;
            }

            set {
                switch (value) {
                    case BalanceSide.Right:
                        radioButtonUpper.Checked = true;
                        break;
                    case BalanceSide.Left:
                        radioButtonLower.Checked = true;
                        break;
                }
            }
        }

        public DistributionType SelectedApproximationType {
            get { return (DistributionType)comboBoxFit.SelectedIndex; }
            set { comboBoxFit.SelectedIndex = (int)value; }
        }



        public HistogramProperties(Histogramma histogram) {
            InitializeComponent();
            Histogram = histogram;
            SelectedApproximationType = DistributionType.Auto;
        }



        public void ResetTitle() {
            Text = string.Format(Resources.Interface.SeriesProperties, HistogramName);
        }

        public void ShowFitTab() {
            tabControl.SelectedTab = tabPageFitDistribution;
        }



        private void valueChanged(object sender, EventArgs e) {
            if (ValueChanged != null && ActiveControl == (Control)sender) {
                ValueChanged.Invoke(sender, new HistogramEventArgs(Histogram));
            }

            if (Histogram != null) Histogram.Update();
        }

        private void textBoxSeriesName_TextChanged(object sender, EventArgs e) {
            ResetTitle();
            FitName = string.Format(Resources.Interface.FitTitle, HistogramName);
            valueChanged(sender, e);
        }

        private void pictureBoxLower_Click(object sender, EventArgs e) {
            SelectedClassBorderType = BalanceSide.Left;
            valueChanged(ActiveControl, e);
        }

        private void pictureBoxUpper_Click(object sender, EventArgs e) {
            SelectedClassBorderType = BalanceSide.Right;
            valueChanged(ActiveControl, e);
        }

        private void form_Closing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        private void structureChanged(object sender, EventArgs e) {
            if (ValueChanged != null && ActiveControl == (Control)sender) {
                ValueChanged.Invoke(sender, new HistogramEventArgs(Histogram));
            }
        }

        private void checkBoxShowFit_CheckedChanged(object sender, EventArgs e) {
            comboBoxFit.Enabled =
                checkBoxShowFit.Checked;

            trackBarFitWidth.Enabled =
            labelFitColor.Enabled =
            colorBoxFit.Enabled =
            labelFitWidth.Enabled =
            trackBarFitWidth.Enabled =
            checkBoxShowCount.Enabled =
            checkBoxShowFitResult.Enabled =
                checkBoxShowFit.Checked && comboBoxFit.SelectedIndex > -1;

            valueChanged(sender, e);
        }

        private void checkBox_EnabledChanged(object sender, EventArgs e) {
            if (!((CheckBox)sender).Enabled) ((CheckBox)sender).Checked = false;
        }
    }
}
