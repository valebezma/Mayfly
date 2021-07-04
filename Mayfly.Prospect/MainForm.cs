using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Waters;
using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.Waters.Controls;
using Mayfly.Geographics;

namespace Mayfly.Prospect
{
    public partial class MainForm : Form
    {
        public ComplexSurveyRoot Surveys;


        public MainForm()
        {
            InitializeComponent();

            listViewWaters.Shine();
            listViewInvestigators.Shine();
            listViewStudiedWaters.Shine();
            searchBox1.SetEmpty();           
            tabPageWaters.Parent = null;

            listViewLocations.Shine();
            tabPageLocations.Parent = null;

            tabPageCalendar.Parent = null;

            menuItemWaters.Enabled = Wild.UserSettings.WatersIndex != null;

            Surveys = new ComplexSurveyRoot();
            UpdateSummary();
        }



        public void UpdateSummary()
        {
            labelCardsNumber.Text = Surveys.CardsCount.ToString();

            if (Surveys.Dates.Count > 0)
            {
                labelDateStart.Text = Surveys.Dates[0].ToLongDateString();
                labelDateEnd.Text = Surveys.Dates.Last().ToLongDateString();
            }
            else
            {
                labelDateStart.Text = string.Empty;
                labelDateEnd.Text = string.Empty;
            }

            listViewStudiedWaters.Items.Clear();
            foreach (Waters.WatersKey.WaterRow waterRow in Surveys.Waters) {
                if (waterRow.IsWaterNull()) {
                    listViewWaters.CreateItem(waterRow.ID.ToString(), Waters.Resources.Interface.Unnamed, waterRow.Type - 1);
                } else {
                    listViewWaters.CreateItem(waterRow.ID.ToString(), waterRow.Water, waterRow.Type - 1);
                }
            }

            listViewInvestigators.Items.Clear();
            foreach (string investigator in Surveys.Investigators) {
                listViewInvestigators.CreateItem(investigator, investigator);
            }

            buttonPlankton.Enabled = Surveys.PlanktonCards.Count > 0;
            buttonBenthos.Enabled = Surveys.BenthosCards.Count > 0;
            buttonFish.Enabled = Surveys.FishCards.Count > 0;
        }



        public void LoadWaters()
        {
            //waterTree1.FillTree(Wild.UserSettings.WatersIndex);
            //labelTreeCount.UpdateStatus(Wild.UserSettings.WatersIndex.Water.Count);
            //labelListCount.UpdateStatus(0);
        }

        private void FillWatersList(WatersKey.WaterRow[] waterRows)
        {
            listViewStudiedWaters.Items.Clear();

            int added = 0;

            foreach (WatersKey.WaterRow waterRow in waterRows)
            {
                ListViewItem item = UpdateWaterItem(waterRow);
                item.Name = waterRow.ID.ToString();
                item.ForeColor = Surveys.IsSurveyed(waterRow) ? SystemColors.ControlText : SystemColors.ControlDark;

                listViewStudiedWaters.Items.Add(item);
                added++;

                if (added > 200) break;
            }

            //labelListCount.UpdateStatus(listViewStudiedWaters.Items.Count);
        }

        private ListViewItem UpdateWaterItem(WatersKey.WaterRow waterRow)
        {
            ListViewItem result = listViewStudiedWaters.GetItem(waterRow.ID.ToString());

            if (result == null)
            {
                result = new ListViewItem();

                switch ((WaterType)waterRow.Type)
                {
                    case WaterType.Stream:
                        result.Group = listViewStudiedWaters.Groups["listViewGroupStreams"];
                        break;
                    case WaterType.Lake:
                        result.Group = listViewStudiedWaters.Groups["listViewGroupLakes"];
                        break;
                    case WaterType.Tank:
                        result.Group = listViewStudiedWaters.Groups["listViewGroupTanks"];
                        break;
                }

                result.Tag = waterRow;
            }

            result.SubItems.Clear();

            if (waterRow.IsWaterNull())
            {
                result.Text = Waters.Resources.Interface.Unnamed;
            }
            else
            {
                result.Text = waterRow.Water;
            }

            if (waterRow.IsOutflowNull())
            {
                result.SubItems.Add(string.Empty);
                result.SubItems.Add(string.Empty);
            }
            else
            {
                WatersKey.WaterRow outflow = Wild.UserSettings.WatersIndex.Water.FindByID(waterRow.Outflow);
                if (outflow.IsWaterNull())
                {
                    result.Text = Waters.Resources.Interface.Unnamed;
                }
                else
                {
                    result.SubItems.Add(outflow.Water);
                }

                if (waterRow.IsMouthToMouthNull())
                {
                    result.SubItems.Add(string.Empty);
                }
                else
                {
                    switch ((WaterType)outflow.Type)
                    {
                        case WaterType.Stream:
                            result.SubItems.Add(waterRow.MouthToMouth.ToString("# ##0.0"));
                            break;
                        case WaterType.Tank:
                        case WaterType.Lake:
                            if (outflow.IsMouthToMouthNull())
                                result.SubItems.Add(string.Empty);
                            else
                            {
                                result.SubItems.Add((outflow.MouthToMouth + waterRow.MouthToMouth).ToString("# ##0.0"));
                            }
                            break;
                    }
                }
            }

            if (waterRow.IsLengthNull())
            {
                result.SubItems.Add(string.Empty);
            }
            else
            {
                result.SubItems.Add(waterRow.Length.ToString("# ##0.0"));
            }

            //if (plk == 0)
            //{
            //    result.SubItems.Add(string.Empty);
            //}
            //else
            //{
            //    result.SubItems.Add(plk.ToString("# ##0"));
            //}

            //if (ben == 0)
            //{
            //    result.SubItems.Add(string.Empty);
            //}
            //else
            //{
            //    result.SubItems.Add(ben.ToString("# ##0"));
            //}

            //if (fsh == 0)
            //{
            //    result.SubItems.Add(string.Empty);
            //}
            //else
            //{
            //    result.SubItems.Add(fsh.ToString("# ##0"));
            //}

            result.ToolTipText = waterRow.GetPath();

            return result;
        }

        private void RunWater(WatersKey.WaterRow waterRow)
        {
            if (waterRow == null) return;
            WaterComplexSurvey survey = Surveys.GetSurvey(waterRow);
            if (survey == null) return;
            WizardSurveyBrief explorer = new WizardSurveyBrief(survey);
            explorer.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            explorer.Show(this);
        }

        //private void UpdateWaterInfo()
        //{
        //    if (SelectedWater == null)
        //    {
        //        ClearWaterInfo();
        //    }
        //    else
        //    {
        //        labelWaterName.Text = SelectedWater.FullName;
        //        flowData.Controls.Clear();

        //        Fish.Explorer.CardStack allStack = UserSettings.FishData.GetCards(SelectedWater);

        //        if (allStack.Count > 0)
        //        {
        //            flowData.Controls.Add(allStack.GetButton(Resources.Interface.AllDataFish));
        //        }

        //        foreach (int year in UserSettings.FishData.GetStack().GetYears())
        //        {
        //            Fish.Explorer.CardStack annualStack = UserSettings.FishData.GetCards(SelectedWater, year);
        //            if (annualStack.Count == 0) continue;
        //            flowData.Controls.Add(annualStack.GetButton(
        //                string.Format(Resources.Interface.AnnualDataFish, year)));
        //        }
        //    }
        //}

        //private void ClearWaterInfo()
        //{
        //    labelWaterName.Text = string.Empty;
        //    flowData.Controls.Clear();
        //}



        public void LoadCalendar()
        {
            monthCalendar1.BoldedDates = Surveys.Dates.ToArray();
            monthCalendar1.SetDate(Surveys.Dates.Last());
        }



        public void LoadLocations()
        {
            listViewLocations.Items.Clear();

            foreach (Waypoint wpt in Surveys.PlanktonCards.GetLocations())
            {
                ListViewItem item = UpdateLocationItem(wpt);
                item.Group = listViewLocations.Groups[0];
            }

            foreach (Waypoint wpt in Surveys.BenthosCards.GetLocations())
            {
                ListViewItem item = UpdateLocationItem(wpt);
                item.Group = listViewLocations.Groups[1];
            }

            foreach (Waypoint wpt in Surveys.FishCards.GetLocations())
            {
                ListViewItem item = UpdateLocationItem(wpt);
                item.Group = listViewLocations.Groups[2];
            }

            labelLocations.UpdateStatus(listViewLocations.Items.Count);
        }

        private ListViewItem UpdateLocationItem(Waypoint wpt)
        {
            ListViewItem result = listViewLocations.GetItem(wpt.ToString());

            if (result == null)
            {
                result = new ListViewItem();
                result.Name = wpt.ToString();
                result.Text = wpt.ToString();
                result.Tag = wpt;

                listViewLocations.Items.Add(result);
            }

            //result.SubItems.Add(wpt.ToString());
            result.SubItems.Add(wpt.TimeMark.ToLongDateString());
            return result;
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Wild.UserSettings.WatersIndex != null) {
                waterTree1.FillTree(Wild.UserSettings.WatersIndex);
            }
        }



        private void menuItemWaters_Click(object sender, EventArgs e)
        {
            tabPageWaters.Parent = tabControl1;
            tabControl1.SelectedTab = tabPageWaters;

            LoadWaters();
        }

        private void menuItemCalendar_Click(object sender, EventArgs e)
        {
            tabPageCalendar.Parent = tabControl1;
            tabControl1.SelectedTab = tabPageCalendar;

            LoadCalendar();
        }

        private void menuItemLocations_Click(object sender, EventArgs e)
        {
            tabPageLocations.Parent = tabControl1;
            tabControl1.SelectedTab = tabPageLocations;

            LoadLocations();
        }

        private void menuItemSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
            UpdateSummary();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            About about = new About(Properties.Resources.logo);
            //about.SetIcon(Mayfly.Service.GetIcon(Application.ExecutablePath, 0));
            about.ShowDialog();
        }
        


        private void buttonPlankton_Click(object sender, EventArgs e)
        {
            Plankton.Explorer.MainForm explorer = new Plankton.Explorer.MainForm(Surveys.PlanktonCards);
            explorer.Show();            
        }

        private void buttonBenthos_Click(object sender, EventArgs e)
        {
            Benthos.Explorer.MainForm explorer = new Benthos.Explorer.MainForm(Surveys.BenthosCards);
            explorer.Show();
        }

        private void buttonFish_Click(object sender, EventArgs e)
        {
            Fish.Explorer.MainForm explorer = new Fish.Explorer.MainForm(Surveys.FishCards);
            explorer.Show();
        }

        private void listViewWaters_ItemActivate(object sender, EventArgs e)
        {
            WatersKey.WaterRow waterRow = Wild.UserSettings.WatersIndex.Water
                .FindByID(listViewWaters.GetID());
            RunWater(waterRow);
        }



        private void waterTree1_WaterSelected(object sender, WaterEventArgs e)
        {
            searchBox1.Enabled = waterTree1.WaterObjects.Count > 1;
            FillWatersList(waterTree1.WaterObjects.ToArray());
        }

        private void waterTree1_WaterAdded(TreeNode parent, TreeNode child, int imageIndex)
        {
            WatersKey.WaterRow addedWater = child.Tag as WatersKey.WaterRow;
            if (addedWater != null) {
                child.ForeColor = Surveys.IsSurveyed(addedWater) ? SystemColors.ControlText : SystemColors.ControlDark;
            }
        }

        private void searchBox1_TextChanged(object sender, EventArgs e)
        {
            if (searchBox1.Text.Length > 1)
            {
                FillWatersList(waterTree1.GetWatersNameContaining(searchBox1.Text));
                status1.Message(listViewWaters.Items.Count.ToString(Waters.Resources.Messages.Filtered));
            }
        }

        private void listViewStudiedWaters_ItemActivate(object sender, EventArgs e)
        {
            WatersKey.WaterRow waterRow = listViewStudiedWaters.SelectedItems[0].Tag as WatersKey.WaterRow;
            RunWater(waterRow);
        }

        private void contextWater_Opening(object sender, CancelEventArgs e)
        {
            WaterComplexSurvey survey = Surveys.GetSurvey(
                Wild.UserSettings.WatersIndex.Water.FindByID(
                listViewStudiedWaters.GetID()));

            contextWaterBrief.Enabled = survey != null;
            contextWaterBrief.Tag = survey;

            while (contextMenuWater.Items.Count > 1) {
                contextMenuWater.Items.RemoveAt(1);
            }

            if (survey.AnnualSurveys.Count > 1) {
                contextMenuWater.Items.Add(new ToolStripSeparator());

                foreach (AnnualWaterComplexSurvey annual in survey.AnnualSurveys)
                {
                    ToolStripMenuItem annualItem = new ToolStripMenuItem(string.Format(Wild.Resources.Interface.Interface.AnnualSurveyItem, annual.Year));
                    annualItem.Tag = annual;
                    annualItem.Click += surveyItem_Click;
                    contextMenuWater.Items.Add(annualItem);
                }
            }
        }

        private void surveyItem_Click(object sender, EventArgs e)
        {
            ComplexSurvey survey = (ComplexSurvey)((ToolStripMenuItem)sender).Tag;
            if (survey == null) return;
            WizardSurveyBrief explorer = new WizardSurveyBrief(survey);
            explorer.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            explorer.Show(this);
        }



        private void listViewLocations_ItemActivate(object sender, EventArgs e)
        {
            List<Waypoint> result = new List<Waypoint>(); 

            foreach (ListViewItem item in listViewLocations.SelectedItems)
            {
                result.Add((Waypoint)item.Tag);
            }

            result.Sort();

            FileSystem.RunFile(Waypoint.GetLink(result).OriginalString);
        }

        private void buttonLocSave_Click(object sender, EventArgs e)
        {
            List<Waypoint> result = new List<Waypoint>(); 

            foreach (ListViewItem item in listViewLocations.Items)
            {
                result.Add((Waypoint)item.Tag);
            }

            Geographics.Service.SaveWaypoints(result.ToArray());
        }
    }
}
