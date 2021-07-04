using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Library
{
    public partial class ResearchCard : Form
    {
        public ResearchArchive.WorkRow WorkRow { get; private set; }

        public ResearchCard(ResearchArchive.WorkRow workRow)
        {
            InitializeComponent();

            WorkRow = workRow;

            UpdateResearch();
        }

        public void UpdateResearch()
        {
            if (!WorkRow.IsTitleNull()) textBoxTitle.Text = WorkRow.Title;
            if (!WorkRow.IsYearNull()) textBoxYear.Text = WorkRow.Year.ToString();
            if (!WorkRow.IsExeIDNull()) textBoxExecutive.Text = WorkRow.ExecutiveRow.Name;
            if (!WorkRow.IsApprovedIDNull()) textBoxConfirmed.Text = WorkRow.ExecutiveRowByExecutive_Work1.Name;
            if (!WorkRow.IsPagesNull()) textBoxPages.Text = WorkRow.Pages.ToString();
            textBoxOrg.Text = WorkRow.GetKeywords().Merge("; ");
            if (!WorkRow.IsSummaryNull()) textBoxSummary.Text = WorkRow.Summary;
            textBoxReference.Text = WorkRow.GetReference();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {

        }

        private void buttonRun_Click(object sender, EventArgs e)
        {

        }
    }
}
