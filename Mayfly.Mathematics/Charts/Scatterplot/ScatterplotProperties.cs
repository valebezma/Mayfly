using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics.Statistics;
using Mayfly.Extensions;

namespace Mayfly.Mathematics.Charts
{
    public partial class ScatterplotProperties : Form
    {
        #region Properties

        public event ScatterplotEventHandler ValueChanged;

        public Scatterplot Scatterplot { get; set; }

        public string ScatterplotName
        {
            get 
            {
                return textBoxRegressionName.Text;
            }

            set
            {
                textBoxRegressionName.Text = value;
                ResetTitle();
            }
        }

        public string TrendName
        {
            get;
            private set;
        }

        public bool ShowTrend
        {
            get { return checkBoxShowTrend.Checked && comboBoxTrend.SelectedIndex > -1; }

            set { checkBoxShowTrend.Checked = value; }
        }

        public TrendType SelectedApproximationType
        {
            get { return (TrendType)comboBoxTrend.SelectedIndex; }
            set { comboBoxTrend.SelectedIndex = (int)value; }
        } 

        public bool AllowCursors
        {
            get { return ShowTrend && checkBoxAllowCursors.Checked; }
            set { checkBoxAllowCursors.Checked = value; }
        }

        public bool ShowCount
        {
            get { return ShowTrend && checkBoxShowCount.Checked; }
            set { checkBoxShowCount.Checked = value; }
        }

        public bool ShowExplained
        {
            get { return ShowTrend && checkBoxShowExplained.Checked; }
            set { checkBoxShowExplained.Checked = value; }
        }

        public bool ShowAnnotation
        {
            get
            {
                return ShowTrend && (ShowCount || ShowExplained);
            }
        }

        public bool ShowConfidenceBands
        {
            get { return ShowTrend && checkBoxCI.Checked; }
            set { checkBoxCI.Checked = value; }
        }

        public bool ShowPredictionBands
        {
            get { return ShowTrend && checkBoxPI.Checked; }
            set { checkBoxPI.Checked = value; }
        }

        public bool HighlightOutliers
        {
            get { return ShowTrend && checkBoxOutliers.Checked; }
            set { checkBoxOutliers.Checked = value; }
        }

        public Color DataPointColor
        {
            get { return colorDialogMarker.Color; }
            set { 

                colorDialogMarker.Color = panelMarkerColor.BackColor = value;
            }
        }

        public Color TrendColor
        {
            get { return colorDialogTrend.Color; }
            set
            {
                colorDialogTrend.Color = value;
                if (ShowTrend) panelTrendColor.BackColor = value;
            }
        }

        public int DataPointSize
        {
            get { return trackBarMarkerSize.Value; }
            set { trackBarMarkerSize.Value = value; }
        }

        public int DataPointBorderWidth
        {
            get { return trackBarMarkerWidth.Value; }
            set { trackBarMarkerWidth.Value = value; }
        }

        public int TrendWidth
        {
            get { return trackBarTrendWidth.Value; }
            set { trackBarTrendWidth.Value = value; }
        }

        public double ConfidenceLevel
        {
            get { return (double)numericUpDownConfidenceLevel.Value / 100; }
            set { numericUpDownConfidenceLevel.Value = 100 * (decimal)value; }
        }

        #endregion

        bool TrendColored = false;



        public ScatterplotProperties()
        {
            InitializeComponent();

            comboBoxTrend.Items.AddRange(Service.GetRegressionTypes());
            SelectedApproximationType = TrendType.Linear;
            ConfidenceLevel = 1 - UserSettings.DefaultAlpha;
        }

        public ScatterplotProperties(Scatterplot scatterplot) : this()
        {
            Scatterplot = scatterplot;
            TrendColor = DataPointColor.Darker();
            checkBoxShowTrend.Enabled = scatterplot.Data.Count > UserSettings.StrongSampleSize;
        }



        #region Methods

        public void ResetTitle()
        {
            Text = string.Format(Resources.Interface.SeriesProperties, ScatterplotName);
        }

        public void ShowTrendTab()
        {
            tabControl1.SelectedTab = tabPageTrendline;
        }

        #endregion



        private void valueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && ActiveControl == (Control)sender)
            {
                ValueChanged.Invoke(sender, new ScatterplotEventArgs(Scatterplot));
            }

            if (Scatterplot != null) Scatterplot.Update(sender, e);
        }

        private void checkBoxTrend_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxTrend.Enabled =
            checkBoxShowCount.Enabled =
            checkBoxShowExplained.Enabled =
            checkBoxCI.Enabled =
            checkBoxPI.Enabled =
            
            trackBarTrendWidth.Enabled =
            label_trend_color.Enabled =
            panelTrendColor.Enabled =
            trackBarTrendWidth.Enabled =
            label23.Enabled =
            checkBoxAllowCursors.Enabled =

                checkBoxShowTrend.Checked && comboBoxTrend.SelectedIndex > -1;

            if (checkBoxShowTrend.Checked && !TrendName.IsAcceptable())
            {
                TrendName = string.Format(Resources.Interface.TrendLineTitle, ScatterplotName);
            }

            if (checkBoxShowTrend.Checked)
            {
                panelTrendColor.BackColor = colorDialogTrend.Color;

                if (!TrendColored)
                {
                    TrendColor = DataPointColor.Darker();
                }
            }
            else
            {
                panelTrendColor.BackColor = Color.Transparent;
            }

            valueChanged(sender, e);
        }

        private void panelTrendColor_Click(object sender, EventArgs e)
        {
            colorDialogTrend.ShowDialog(this);
            TrendColored = true;
            panelTrendColor.BackColor = colorDialogTrend.Color;
            valueChanged(ActiveControl, e);
        }

        private void panelMarkerColor_Click(object sender, EventArgs e)
        {
            colorDialogMarker.ShowDialog(this);
            panelMarkerColor.BackColor = colorDialogMarker.Color;
            valueChanged(ActiveControl, e);

            if (!TrendColored)
            {
                TrendColor = DataPointColor.Darker();
            }
        }

        private void panelMarkerColor_BackColorChanged(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                colorDialogMarker.Color = panelMarkerColor.BackColor;
            }
        }

        private void comboBoxTrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueChanged(sender, e);
        }

        private void comboBoxTrend_Click(object sender, EventArgs e)
        {
            if (!comboBoxTrend.Enabled) ShowTrend = true;
        }

        private void textBoxRegressionName_TextChanged(object sender, EventArgs e)
        {
            ResetTitle();
            TrendName = string.Format(Resources.Interface.TrendLineTitle, ScatterplotName);
            valueChanged(sender, e);
        }

        private void checkBoxBands_CheckedChanged(object sender, EventArgs e)
        {
            label5.Enabled =
                numericUpDownConfidenceLevel.Enabled = 
                (ShowPredictionBands || ShowConfidenceBands);

            checkBoxOutliers.Enabled = ShowPredictionBands;

            valueChanged(sender, e);
        }

        private void Properties_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
