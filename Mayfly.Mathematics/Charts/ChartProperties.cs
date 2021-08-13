using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mayfly.Mathematics.Charts
{
    public partial class ChartProperties : Form
    {
        Plot plot;



        public ChartProperties(Plot _plot)
        {
            InitializeComponent();

            plot = _plot;
            plot.CollectionChanged += plot_CollectionChanged;
        }

        void plot_CollectionChanged(object sender, EventArgs e)
        {
            bool axis2 = false;

            foreach (Series series in plot.Series)
            {
                axis2 |= series.YAxisType == AxisType.Secondary;
            }

            tabPageY2.Parent = axis2 ? tabControl : null;
        }



        public void ResetTitle()
        {
        }

        public void SetAxisXTitleEntering()
        {
            tabControl.SelectedTab = tabPageAxisX;
            textBoxXTitle.Focus();
        }

        public void SetAxisYTitleEntering()
        {
            tabControl.SelectedTab = tabPageAxisY;
            textBoxYTitle.Focus();
        }

        public void SetAxisY2TitleEntering()
        {
            tabControl.SelectedTab = tabPageY2;
            textBoxY2Title.Focus();
        }
        


        private void Appearance_ValueChanged(object sender, EventArgs e)
        {
            if (((Control)sender).ContainsFocus)
            {
                plot.Update(sender, e);
            }
        }

        private void Structure_ValueChanged(object sender, EventArgs e)
        {
            if (((Control)sender).ContainsFocus)
            {
                plot.Rebuild(sender, e);
            }

            checkBoxXLog.Enabled = numericUpDownXMin.Value > 0;
            checkBoxYLog.Enabled = numericUpDownYMin.Value > 0;
        }

        private void Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void checkBoxAllowBreak_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownScaleBreak.Enabled = label1.Enabled = checkBoxAllowBreak.Checked;
            Appearance_ValueChanged(sender, e);
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            if (fontDialogUniversalFont.ShowDialog(this) == DialogResult.OK)
            {
                textBoxFont.Text = string.Format("{0}, {1}", fontDialogUniversalFont.Font.Name, fontDialogUniversalFont.Font.Size);
                Appearance_ValueChanged(buttonFont, e);
            }
        }


        private void xExtreme_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AxisXInterval_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownXMin.Increment = numericUpDownXMax.Increment = numericUpDownXInterval.Value;

            if (plot.AxisXAutoMinimum)
            {
                plot.AxisXMin = Math.Floor(plot.AxisXMin / plot.AxisXInterval) * plot.AxisXInterval;
            }

            if (plot.AxisXAutoMaximum)
            {
                plot.AxisXMax = Math.Round(plot.AxisXMax / plot.AxisXInterval) * plot.AxisXInterval;
            }

            Structure_ValueChanged(sender, e);
        }


        private void yExtreme_ValueChanged(object sender, EventArgs e)
        {

        }

        private void AxisYInterval_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYMin.Increment = numericUpDownYMax.Increment = numericUpDownYInterval.Value;
            plot.AxisYMin = Math.Floor(plot.AxisYMin / plot.AxisYInterval) * plot.AxisYInterval;
            plot.AxisYMax = Math.Ceiling(plot.AxisYMax / plot.AxisYInterval) * plot.AxisYInterval;
            Structure_ValueChanged(sender, e);
        }

        private void AxisY2Interval_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownY2Min.Increment = numericUpDownY2Max.Increment = numericUpDownY2Interval.Value;
            plot.AxisY2Min = Math.Floor(plot.AxisY2Min / plot.AxisY2Interval) * plot.AxisY2Interval;
            plot.AxisY2Max = Math.Ceiling(plot.AxisY2Max / plot.AxisY2Interval) * plot.AxisY2Interval;
            Structure_ValueChanged(sender, e);
        }


        private void radioButtonXMinAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownXMin.Enabled = dateTimePickerXMin.Enabled = !checkBoxXMinAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void radioButtonXMaxAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownXMax.Enabled = dateTimePickerXMax.Enabled = !checkBoxXMaxAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void radioButtonXIntervalAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownXInterval.Enabled = comboBoxTimeInterval.Enabled =
                !checkBoxXIntervalAuto.Checked;

            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }


        private void fontDialogUniversalFont_Apply(object sender, EventArgs e)
        {
        }


        private void dateTimePickerXMin_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerXMin.ContainsFocus) dateTimePickerXMax.MinDate = dateTimePickerXMin.Value.Date;
            Structure_ValueChanged(sender, e);
        }

        private void dateTimePickerXMax_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerXMax.ContainsFocus) dateTimePickerXMin.MaxDate = dateTimePickerXMax.Value.Date.AddDays(1);
            Structure_ValueChanged(sender, e);
        }

        private void numericUpDownXMin_ValueChanged(object sender, EventArgs e)
        {

        }


        private void checkBoxYMinAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownYMin.Enabled = !checkBoxYMinAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void checkBoxYMaxAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownYMax.Enabled = !checkBoxYMaxAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void checkBoxYIntervalAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownYInterval.Enabled = !checkBoxYIntervalAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void numericUpDownYMin_ValueChanged(object sender, EventArgs e)
        {
            checkBoxYLog.Enabled = plot.AxisYMin < 0;
            if (!checkBoxYLog.Enabled) checkBoxYLog.Checked = false;
            Structure_ValueChanged(sender, e);
        }


        private void checkBoxY2MinAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownY2Min.Enabled = !checkBoxY2MinAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void checkBoxY2MaxAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownY2Max.Enabled = !checkBoxY2MaxAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void checkBoxY2IntervalAuto_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownY2Interval.Enabled = !checkBoxY2IntervalAuto.Checked;
            if (((CheckBox)sender).Checked) Structure_ValueChanged(sender, e);
        }

        private void checkBoxYLog_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBoxYLog.Enabled) checkBoxYLog.Checked = false;
        }

        private void checkBoxXLog_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkBoxXLog.Enabled) checkBoxXLog.Checked = false;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            plot.Print();
        }
    }
}