using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Waters
{
    public partial class Filter : Form
    {
        private TabControl HiddenPages = new TabControl();

        private WatersKey Data { get; set; }

        public WatersKey.WaterRow[] FilteredWaters
        {
            get
            {
                List<WatersKey.WaterRow> result = new List<WatersKey.WaterRow>();

                foreach (WatersKey.WaterRow waterRow in Data.Water.Rows)
                {
                    if (comboBoxType.SelectedIndex > 0)
                    {
                        if ((WaterType)waterRow.Type != (WaterType)comboBoxType.SelectedIndex) { continue; }

                        switch ((WaterType)comboBoxType.SelectedIndex)
                        {
                            case WaterType.Lake:

                                if (checkBoxLakeKind.Checked)
                                {
                                    if (waterRow.IsKindNull()) continue;
                                    else { if (comboBoxLakeKind.SelectedIndex != waterRow.Kind) continue; }
                                }

                                if (checkBoxLakeAltitude.Checked)
                                {
                                    if (waterRow.IsAltitudeNull()) continue;
                                    else
                                    {
                                        if (textBoxLakeAltitude1.Text.IsDoubleConvertible() && waterRow.Altitude < double.Parse(textBoxLakeAltitude1.Text)) continue;
                                        if (textBoxLakeAltitude2.Text.IsDoubleConvertible() && waterRow.Altitude > double.Parse(textBoxLakeAltitude2.Text)) continue;
                                    }

                                }

                                if (checkBoxLakeWatershed.Checked)
                                {
                                    if (waterRow.IsWatershedNull()) continue;
                                    else
                                    {
                                        if (textBoxLakeWatershed1.Text.IsDoubleConvertible() && waterRow.Watershed < double.Parse(textBoxLakeWatershed1.Text)) continue;
                                        if (textBoxLakeWatershed2.Text.IsDoubleConvertible() && waterRow.Watershed > double.Parse(textBoxLakeWatershed2.Text)) continue;
                                    }
                                }

                                if (checkBoxLakeArea.Checked)
                                {
                                    if (waterRow.IsAreaNull()) continue;
                                    else
                                    {
                                        if (textBoxLakeArea1.Text.IsDoubleConvertible() && waterRow.Area < double.Parse(textBoxLakeArea1.Text)) continue;
                                        if (textBoxLakeArea2.Text.IsDoubleConvertible() && waterRow.Area > double.Parse(textBoxLakeArea2.Text)) continue;
                                    }
                                }

                                if (checkBoxLakeVolume.Checked)
                                {
                                    if (waterRow.IsVolumeNull()) continue;
                                    else
                                    {
                                        if (textBoxLakeVolume1.Text.IsDoubleConvertible() && waterRow.Volume < double.Parse(textBoxLakeVolume1.Text)) continue;
                                        if (textBoxLakeVolume2.Text.IsDoubleConvertible() && waterRow.Volume > double.Parse(textBoxLakeVolume2.Text)) continue;
                                    }
                                }

                                if (checkBoxLakeDepthMax.Checked)
                                {
                                    if (waterRow.IsDepthMaxNull()) continue;
                                    else
                                    {
                                        if (textBoxLakeDepthMax1.Text.IsDoubleConvertible() && waterRow.DepthMax < double.Parse(textBoxLakeDepthMax1.Text)) continue;
                                        if (textBoxLakeDepthMax2.Text.IsDoubleConvertible() && waterRow.DepthMax > double.Parse(textBoxLakeDepthMax2.Text)) continue;
                                    }
                                }

                                if (checkBoxLakeDepthMid.Checked)
                                {
                                    if (waterRow.IsDepthMidNull()) continue;
                                    else
                                    {
                                        if (textBoxLakeDepthMid1.Text.IsDoubleConvertible() && waterRow.DepthMid < double.Parse(textBoxLakeDepthMid1.Text)) continue;
                                        if (textBoxLakeDepthMid2.Text.IsDoubleConvertible() && waterRow.DepthMid > double.Parse(textBoxLakeDepthMid2.Text)) continue;
                                    }
                                }

                                break;

                            case WaterType.Stream:

                                if (checkBoxStreamLength.Checked)
                                {
                                    if (waterRow.IsLengthNull()) continue;
                                    else
                                    {
                                        if (textBoxStreamLength1.Text.IsDoubleConvertible() && waterRow.Length < double.Parse(textBoxStreamLength1.Text)) continue;
                                        if (textBoxStreamLength2.Text.IsDoubleConvertible() && waterRow.Length > double.Parse(textBoxStreamLength2.Text)) continue;
                                    }
                                }

                                if (checkBoxStreamWatershed.Checked)
                                {
                                    if (waterRow.IsWatershedNull()) continue;
                                    else
                                    {
                                        if (textBoxStreamWatershed1.Text.IsDoubleConvertible() && waterRow.Watershed < double.Parse(textBoxStreamWatershed1.Text)) continue;
                                        if (textBoxStreamWatershed2.Text.IsDoubleConvertible() && waterRow.Watershed > double.Parse(textBoxStreamWatershed2.Text)) continue;
                                    }
                                }

                                if (checkBoxStreamConsumption.Checked)
                                {
                                    if (waterRow.IsConsumptionNull()) continue;
                                    else
                                    {
                                        if (textBoxStreamConsumption1.Text.IsDoubleConvertible() && waterRow.Consumption < double.Parse(textBoxStreamConsumption1.Text)) continue;
                                        if (textBoxStreamConsumption2.Text.IsDoubleConvertible() && waterRow.Consumption > double.Parse(textBoxStreamConsumption2.Text)) continue;
                                    }
                                }

                                if (checkBoxStreamVolume.Checked)
                                {
                                    if (waterRow.IsVolumeNull()) continue;
                                    else
                                    {
                                        if (textBoxStreamVolume1.Text.IsDoubleConvertible() && waterRow.Volume < double.Parse(textBoxStreamVolume1.Text)) continue;
                                        if (textBoxStreamVolume2.Text.IsDoubleConvertible() && waterRow.Volume > double.Parse(textBoxStreamVolume2.Text)) continue;
                                    }
                                }

                                if (checkBoxStreamSlope.Checked)
                                {
                                    if (waterRow.IsSlopeNull()) continue;
                                    else
                                    {
                                        if (textBoxStreamSlope1.Text.IsDoubleConvertible() && waterRow.Slope < double.Parse(textBoxStreamSlope1.Text)) continue;
                                        if (textBoxStreamSlope2.Text.IsDoubleConvertible() && waterRow.Slope > double.Parse(textBoxStreamSlope2.Text)) continue;
                                    }
                                }

                                break;

                            case WaterType.Tank:

                                if (checkBoxTankKind.Checked)
                                {
                                    if (waterRow.IsKindNull()) continue;
                                    else { if (comboBoxTankKind.SelectedIndex != waterRow.Kind) continue; }
                                }

                                if (checkBoxTankAltitude.Checked)
                                {
                                    if (waterRow.IsAltitudeNull()) continue;
                                    else
                                    {
                                        if (textBoxTankAltitude1.Text.IsDoubleConvertible() && waterRow.Altitude < double.Parse(textBoxTankAltitude1.Text)) continue;
                                        if (textBoxTankAltitude2.Text.IsDoubleConvertible() && waterRow.Altitude > double.Parse(textBoxTankAltitude2.Text)) continue;
                                    }

                                }

                                if (checkBoxTankArea.Checked)
                                {
                                    if (waterRow.IsAreaNull()) continue;
                                    else
                                    {
                                        if (textBoxTankArea1.Text.IsDoubleConvertible() && waterRow.Area < double.Parse(textBoxTankArea1.Text)) continue;
                                        if (textBoxTankArea2.Text.IsDoubleConvertible() && waterRow.Area > double.Parse(textBoxTankArea2.Text)) continue;
                                    }
                                }

                                if (checkBoxTankVolume.Checked)
                                {
                                    if (waterRow.IsVolumeNull()) continue;
                                    else
                                    {
                                        if (textBoxTankVolume1.Text.IsDoubleConvertible() && waterRow.Volume < double.Parse(textBoxTankVolume1.Text)) continue;
                                        if (textBoxTankVolume2.Text.IsDoubleConvertible() && waterRow.Volume > double.Parse(textBoxTankVolume2.Text)) continue;
                                    }
                                }

                                if (checkBoxTankDepthMax.Checked)
                                {
                                    if (waterRow.IsDepthMaxNull()) continue;
                                    else
                                    {
                                        if (textBoxTankDepthMax1.Text.IsDoubleConvertible() && waterRow.DepthMax < double.Parse(textBoxTankDepthMax1.Text)) continue;
                                        if (textBoxTankDepthMax2.Text.IsDoubleConvertible() && waterRow.DepthMax > double.Parse(textBoxTankDepthMax2.Text)) continue;
                                    }
                                }

                                if (checkBoxTankDepthMid.Checked)
                                {
                                    if (waterRow.IsDepthMidNull()) continue;
                                    else
                                    {
                                        if (textBoxTankDepthMid1.Text.IsDoubleConvertible() && waterRow.DepthMid < double.Parse(textBoxTankDepthMid1.Text)) continue;
                                        if (textBoxTankDepthMid2.Text.IsDoubleConvertible() && waterRow.DepthMid > double.Parse(textBoxTankDepthMid2.Text)) continue;
                                    }
                                }

                                if (checkBoxTankFlowage.Checked)
                                {
                                    if (waterRow.IsFlowageNull()) continue;
                                    else
                                    {
                                        if (textBoxTankFlowage1.Text.IsDoubleConvertible() && waterRow.Flowage < double.Parse(textBoxTankFlowage1.Text)) continue;
                                        if (textBoxTankFlowage2.Text.IsDoubleConvertible() && waterRow.Flowage > double.Parse(textBoxTankFlowage2.Text)) continue;
                                    }
                                }

                                if (checkBoxTankCycling.Checked)
                                {
                                    if (waterRow.IsCyclingNull()) continue;
                                    else
                                    {
                                        if (textBoxTankCycling1.Text.IsDoubleConvertible() && waterRow.Cycling < double.Parse(textBoxTankCycling1.Text)) continue;
                                        if (textBoxTankCycling2.Text.IsDoubleConvertible() && waterRow.Cycling > double.Parse(textBoxTankCycling2.Text)) continue;
                                    }
                                }

                                break;
                        }
                    }

                    if (checkBoxOutflow.Checked && outflowSelector.WaterObject != null)
                    {
                        if (waterRow.IsOutflowNull())
                        {
                            continue;
                        }
                        else
                        {
                            WatersKey.WaterRow terminalInflow = waterRow.GetTerminalInflow(outflowSelector.WaterObject);

                            // Check terminal position on the selected root outflow
                            if (!CheckMouthPosition(terminalInflow)) continue;

                            if (domainUpDownInflowOrder.SelectedIndex == 0)
                            {
                                // If selected 'All Orders Inflows' -
                                // check terminal inflow of current water.
                                if (terminalInflow == null)
                                {
                                    // If it is null - then continue to next water.
                                    continue;
                                }
                            }
                            else
                            {
                                // If order is selected - 
                                // calculate water's order.
                                int Order = OrderDistance(waterRow,
                                    outflowSelector.WaterObject,
                                    domainUpDownInflowOrder.SelectedIndex);

                                if (Order == -1)
                                {
                                    // If it is equal to -1
                                    // then continue
                                    continue;
                                }
                                else
                                {
                                    // If it is unequal to selected order
                                    // then continue

                                    if (!checkBoxCumulative.Checked &&
                                        Order != domainUpDownInflowOrder.SelectedIndex)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    
                    result.Add(waterRow);
                }

                return result.ToArray();
            }
        }

        public Filter(WatersKey watersKey)
        {
            InitializeComponent();
            Data = watersKey;
            outflowSelector.CreateList();
            outflowSelector.Index = watersKey;

            comboBoxType.SelectedIndex = 0;
            comboBoxLakeKind.SelectedIndex = 0;
            domainUpDownInflowOrder.SelectedIndex = 0;
        }

        private int OrderDistance(WatersKey.WaterRow waterRow, WatersKey.WaterRow distantOutflow, int Limit)
        {
            for (int i = 1; i <= Limit; i++)
            {
                WatersKey.WaterRow outflow = waterRow.GetOutflow(i);

                if ((WaterType)outflow.Type == WaterType.Tank)
                {
                    if (!outflow.IsOutflowNull() &&
                        outflow.GetOutflow() == distantOutflow)
                    {
                        return i;
                    }
                }
                else
                {
                    if (outflow == distantOutflow)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks whether or not specified water fits values selected on form
        /// in relation to terminal water
        /// </summary>
        /// <param name="waterRow"></param>
        /// <param name="TerminalWater"></param>
        /// <returns></returns>
        private bool CheckMouthPosition(WatersKey.WaterRow waterRow)
        {
            if (waterRow != null)
            {
                if (checkBoxBank.Checked)
                {
                    // If bank is specified
                    if (waterRow.IsMouthCoastNull())
                    {
                        // and water's bank isn't - return false;
                        return false;
                    }
                    else
                    {
                        // Is bank doesn't match - return false; 
                        if (waterRow.MouthCoast != comboBoxBank.SelectedIndex)
                        {
                            return false;
                        }
                    }
                }

                if (checkBoxMouthToMouth.Checked)
                {
                    if (waterRow.IsMouthToMouthNull())
                    {
                        return false;
                    }
                    else
                    {
                        WatersKey.WaterRow outflow = waterRow.GetOutflow();

                        if ((WaterType)outflow.Type == WaterType.Tank)
                        {
                            if (outflow.IsMouthToMouthNull())
                            {
                                return false; 
                            }
                            else
                            {
                                if (textBoxFromMouth1.Text.IsDoubleConvertible())
                                {
                                    double Start = double.Parse(textBoxFromMouth1.Text);

                                    if (waterRow.MouthToMouth + outflow.MouthToMouth < Start)
                                    {
                                        return false;
                                    }
                                }

                                if (textBoxFromMouth2.Text.IsDoubleConvertible())
                                {
                                    double End = double.Parse(textBoxFromMouth2.Text);

                                    if (waterRow.MouthToMouth + outflow.MouthToMouth > End)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (textBoxFromMouth1.Text.IsDoubleConvertible())
                            {
                                double Start = double.Parse(textBoxFromMouth1.Text);

                                if (waterRow.MouthToMouth < Start)
                                {
                                    return false;
                                }
                            }

                            if (textBoxFromMouth2.Text.IsDoubleConvertible())
                            {
                                double End = double.Parse(textBoxFromMouth2.Text);

                                if (waterRow.MouthToMouth > End)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((WaterType)comboBoxType.SelectedIndex)
            {
                case WaterType.Lake:
                    SwitchTabPage(tabPageStream, false);
                    SwitchTabPage(tabPageLake, true);
                    SwitchTabPage(tabPageTank, false);
                    break;
                case WaterType.Stream:
                    SwitchTabPage(tabPageStream, true);
                    SwitchTabPage(tabPageLake, false);
                    SwitchTabPage(tabPageTank, false);
                    break;
                case WaterType.Tank:
                    SwitchTabPage(tabPageStream, false);
                    SwitchTabPage(tabPageLake, false);
                    SwitchTabPage(tabPageTank, true);
                    break;
                default:
                    SwitchTabPage(tabPageStream, false);
                    SwitchTabPage(tabPageLake, false);
                    SwitchTabPage(tabPageTank, false);
                    break;
            }
        }

        private void SwitchTabPage(TabPage tabPage, bool on)
        {
            if (on)
            {
                tabPage.Parent = tabControlFilter;
            }
            else
            {
                tabPage.Parent = HiddenPages;
            }
        }

        private void checkBoxOutflow_CheckedChanged(object sender, EventArgs e)
        {
            outflowSelector.Enabled = 
            domainUpDownInflowOrder.Enabled = label4.Enabled =
            checkBoxOutflow.Checked;
            checkBoxCumulative.Visible = (checkBoxOutflow.Checked && domainUpDownInflowOrder.SelectedIndex > 1);
            UpdateOutflow();
            valueChanged(sender, e);
        }

        private void UpdateOutflow()
        {
            checkBoxBank.Enabled = (outflowSelector.WaterObject != null);
            checkBoxMouthToMouth.Enabled = (outflowSelector.WaterObject != null);

            if (checkBoxOutflow.Checked)
            {
                if (outflowSelector.WaterObject != null)
                {
                    WaterType selectedWaterType = (WaterType)outflowSelector.WaterObject.Type;
                    checkBoxBank.Enabled = domainUpDownInflowOrder.Enabled = (selectedWaterType != WaterType.Lake);
                }
            }
            else
            {
                checkBoxBank.Enabled = false;
                checkBoxBank.Checked = false;

                checkBoxMouthToMouth.Enabled = false;
                checkBoxMouthToMouth.Checked = false;
            }
        }

        private void valueChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoApply.Checked)
            {
                buttonApply.PerformClick();
            }
        }

        private void checkBoxBank_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBank.Enabled = checkBoxBank.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxMouthToMouth_CheckedChanged(object sender, EventArgs e)
        {
            label2.Enabled = label3.Enabled =
            textBoxFromMouth1.Enabled = textBoxFromMouth2.Enabled =
            R61.Enabled = checkBoxMouthToMouth.Checked;
            valueChanged(sender, e);
        }

        #region Streams

        private void checkBoxStreamLength_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStreamLength1.Enabled = textBoxStreamLength2.Enabled = R11.Enabled = R12.Enabled = checkBoxStreamLength.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxStreamArea_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStreamWatershed1.Enabled = textBoxStreamWatershed2.Enabled = R21.Enabled = R22.Enabled = checkBoxStreamWatershed.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxStreamSpend_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStreamConsumption1.Enabled = textBoxStreamConsumption2.Enabled = R31.Enabled = R32.Enabled = checkBoxStreamConsumption.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxStreamYearVolume_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStreamVolume1.Enabled = textBoxStreamVolume2.Enabled = R41.Enabled = R42.Enabled = checkBoxStreamVolume.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxStreamSlope_CheckedChanged(object sender, EventArgs e)
        {
            textBoxStreamSlope1.Enabled = textBoxStreamSlope2.Enabled = R51.Enabled = R52.Enabled = checkBoxStreamSlope.Checked;
            valueChanged(sender, e);
        }

        #endregion

        #region Lakes

        private void checkBoxLakeKind_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxLakeKind.Enabled = checkBoxLakeKind.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxLakeAltitude_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLakeAltitude1.Enabled = textBoxLakeAltitude2.Enabled = label7.Enabled = label8.Enabled = checkBoxLakeAltitude.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxLakeWatershed_CheckedChanged(object sender, EventArgs e)
        {
            label11.Enabled = label12.Enabled = textBoxLakeWatershed1.Enabled = textBoxLakeWatershed2.Enabled =
              checkBoxLakeWatershed.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxLakeArea_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLakeArea1.Enabled = textBoxLakeArea2.Enabled = L11.Enabled = L12.Enabled =
              checkBoxLakeArea.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxLakeVolume_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLakeVolume1.Enabled = textBoxLakeVolume2.Enabled = L21.Enabled =
              L22.Enabled = checkBoxLakeVolume.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxLakeDepthMax_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLakeDepthMax1.Enabled = textBoxLakeDepthMax2.Enabled = L31.Enabled =
              L32.Enabled = checkBoxLakeDepthMax.Checked;
            valueChanged(sender, e);
        }

        private void checkBoxLakeDepthMid_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLakeDepthMid1.Enabled = textBoxLakeDepthMid2.Enabled = L41.Enabled =
              L42.Enabled = checkBoxLakeDepthMid.Checked;
            valueChanged(sender, e);
        }

        #endregion

        #region Tanks

        private void checkBoxTankLength_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankLength1.Enabled = textBoxTankLength2.Enabled = T11.Enabled = T12.Enabled = checkBoxTankLength.Checked;
        }

        private void checkBoxTankLevel_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankAltitude1.Enabled = textBoxTankAltitude2.Enabled = T21.Enabled = T22.Enabled = checkBoxTankAltitude.Checked;
        }

        private void checkBoxTankArea_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankArea1.Enabled = textBoxTankArea2.Enabled = T31.Enabled = T32.Enabled = checkBoxTankArea.Checked;
        }

        private void checkBoxTankVolume_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankVolume1.Enabled = textBoxTankVolume2.Enabled = T41.Enabled = T42.Enabled = checkBoxTankVolume.Checked;
        }

        private void checkBoxTankDepthMax_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankDepthMax1.Enabled = textBoxTankDepthMax2.Enabled = T51.Enabled = T52.Enabled = checkBoxTankDepthMax.Checked;
        }

        private void checkBoxTankDepthMid_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankDepthMid1.Enabled = textBoxTankDepthMid2.Enabled = T61.Enabled = T62.Enabled = checkBoxTankDepthMid.Checked;
        }

        private void checkBoxTankFlowing_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankFlowage1.Enabled = textBoxTankFlowage2.Enabled = T71.Enabled = T72.Enabled = checkBoxTankFlowage.Checked;
        }

        private void checkBoxTankExchange_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTankCycling1.Enabled = textBoxTankCycling2.Enabled = T81.Enabled = T82.Enabled = checkBoxTankCycling.Checked;
        }

        #endregion

        private void f_Filter_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void domainUpDownInflowOrder_SelectedItemChanged(object sender, EventArgs e)
        {
            checkBoxCumulative.Visible = (domainUpDownInflowOrder.SelectedIndex > 1);

            if (domainUpDownInflowOrder.SelectedIndex == domainUpDownInflowOrder.Items.Count - 1)
            {
                domainUpDownInflowOrder.Items.Add(String.Format(Resources.Interface.Order,
                    domainUpDownInflowOrder.Items.Count));
            }
            valueChanged(sender, e);

        }

        private void SelectorOutflow_WaterChanged(object sender, EventArgs e)
        {
            UpdateOutflow();
            valueChanged(sender, e);
        }

        private void checkBoxAutoApply_CheckedChanged(object sender, EventArgs e)
        {
            //buttonApply.Enabled = !checkBoxAutoApply.Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply.PerformClick();
            Close();
        }
    }
}