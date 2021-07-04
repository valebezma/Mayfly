using Mayfly.Benthos;
using Mayfly.Extensions;
using Mayfly.Waters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Mayfly.Wild;

namespace Mayfly.Benthos
{
    public static partial class DataExtensions
    {
        /// <summary>
        /// Adds Individuals log into Report
        /// </summary>
        /// <param name="indRows"></param>
        /// <param name="report"></param>
        public static void AddReport(this Data.IndividualRow[] indRows, Report report, string logtitle)
        {
            Report.Table logtable = indRows.GetIndividualsLogReportTable(new ResourceManager(typeof(Individuals)), logtitle);
            if (logtable != null) report.AddTable(logtable);
        }

        /// <summary>
        /// Adds Individuals log with given titles into Report
        /// </summary>
        /// <param name="logRow"></param>
        /// <param name="report"></param>
        /// <param name="logtitle"></param>
        public static void AddReport(this Data.LogRow logRow, Report report, string logtitle)
        {
            logRow.GetIndividualRows().AddReport(report, logtitle);
        }

        /// <summary>
        /// Adds Individuals log and Stratified sample into Report
        /// </summary>
        /// <param name="logRow"></param>
        /// <param name="report"></param>
        public static void AddReport(this Data.LogRow logRow, Report report)
        {
            string speciesPresentation = logRow.SpeciesRow.GetKeyRecord(UserSettings.SpeciesIndex).ScientificNameReport;
            logRow.AddReport(report, speciesPresentation);
        }

        /// <summary>
        /// Adds Card (with specified detalization level) into Report
        /// </summary>
        /// <param name="cardRow"></param>
        /// <param name="report"></param>
        /// <param name="level"></param>
        public static void AddReport(this Data.CardRow cardRow, Report report, CardReportLevel level)
        {
            ResourceManager resources = new ResourceManager(typeof(Card));

            if (level.HasFlag(CardReportLevel.Note))
            {
                report.AddSubtitle(Wild.Resources.Reports.Header.SampleNote);

                #region Common

                Report.Table table1 = new Report.Table(Wild.Resources.Reports.Header.Common);

                table1.StartRow();
                table1.AddCellPrompt(Wild.Resources.Reports.Card.Investigator,
                    cardRow.Investigator, 2);
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(resources.GetString("labelWater.Text"),
                    cardRow.IsWaterIDNull() ? Constants.Null : cardRow.WaterRow.Presentation, 2);
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(resources.GetString("labelLabel.Text"),
                        cardRow.IsLabelNull() ? Constants.Null : cardRow.Label);
                table1.AddCellPrompt(Wild.Resources.Reports.Card.When, cardRow.IsWhenNull() ? Constants.Null :
                        (cardRow.When.ToShortDateString() + " " + cardRow.When.ToString("HH:mm")));
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(Wild.Resources.Reports.Card.Where,
                    cardRow.IsWhereNull() ? Constants.Null : cardRow.Position.GetPlaceableLink(Mayfly.UserSettings.FormatCoordinate), 2);
                table1.EndRow();
                report.AddTable(table1);

                #endregion

                #region Sampler

                Report.Table table2 = new Report.Table(resources.GetString("labelMethod.Text"));

                if (cardRow.IsSamplerNull())
                {
                    table2.StartRow();
                    table2.AddCellPrompt(resources.GetString("labelSampler.Text"), Constants.Null, 2);
                    table2.EndRow();

                    table2.StartRow();
                    table2.AddCellPromptEmpty(resources.GetString("labelSquare.Text"));
                    table2.AddCellPromptEmpty(Benthos.Resources.Interface.Interface.Repeats);
                    table2.EndRow();

                    table2.StartRow();
                    table2.AddCellPromptEmpty(resources.GetString("labelMesh.Text"));
                    table2.AddCellPromptEmpty(resources.GetString("labelDepth.Text"));
                    table2.EndRow();
                }
                else
                {
                    Samplers.SamplerRow SelectedSampler = Benthos.Service.Sampler(cardRow.Sampler);

                    table2.StartRow();
                    table2.AddCellPrompt(resources.GetString("labelSampler.Text"), SelectedSampler.Sampler, 2);
                    table2.EndRow();

                    table2.StartRow();
                    if (cardRow.IsSquareNull())
                    {
                        table2.AddCellPromptEmpty(resources.GetString("labelSquare.Text"));
                        table2.AddCellPromptEmpty(Benthos.Resources.Interface.Interface.Repeats);
                    }
                    else
                    {
                        table2.AddCellPrompt(resources.GetString("labelSquare.Text"), cardRow.Square, Mayfly.Service.Mask(4));

                        switch (SelectedSampler.GetSamplerType())
                        {
                            case BenthosSamplerType.Grabber:
                                // Replies count equals to 
                                // square divided by [standard effort] of grabber
                                table2.AddCellPrompt(Benthos.Resources.Interface.Interface.Repeats, (int)Math.Round(cardRow.Square / SelectedSampler.EffortValue, 0));
                                break;
                            case BenthosSamplerType.Scraper:
                                // Exposure length in centimeters equals to 
                                // square divided by knife length (standard effort) of creeper multiplied by 100
                                table2.AddCellPrompt(Benthos.Resources.Interface.Interface.Repeats, 100 * (int)Math.Round(cardRow.Square / SelectedSampler.EffortValue, 0));
                                break;
                            default:
                                table2.AddCellPromptEmpty(Benthos.Resources.Interface.Interface.Repeats);
                                break;
                        }
                    }
                    table2.EndRow();

                    table2.StartRow();

                    if (cardRow.IsMeshNull()) { table2.AddCellPromptEmpty(resources.GetString("labelMesh.Text")); }
                    else { table2.AddCellPrompt(resources.GetString("labelMesh.Text"), cardRow.Mesh); }

                    if (cardRow.IsDepthNull()) { table2.AddCellPromptEmpty(resources.GetString("labelDepth.Text")); }
                    else { table2.AddCellPrompt(resources.GetString("labelDepth.Text"), cardRow.Depth); }

                    table2.EndRow();
                }
                report.AddTable(table2);

                #endregion

                #region Site

                Report.Table table3 = new Report.Table(resources.GetString("labelSite.Text"));

                table3.StartRow();
                if (cardRow.IsCrossSectionNull())
                {
                    table3.AddCellPromptEmpty(resources.GetString("labelCrossSection.Text"));
                }
                else
                {
                    WaterType waterType = cardRow.IsWaterIDNull() ? WaterType.None : (WaterType)cardRow.WaterRow.Type;
                    table3.AddCellPrompt(resources.GetString("labelCrossSection.Text"), Wild.Service.CrossSection(waterType, cardRow.CrossSection));
                }
                if (cardRow.IsBankNull())
                {
                    table3.AddCellPromptEmpty(resources.GetString("labelBank.Text"));
                }
                else
                {
                    table3.AddCellPrompt(resources.GetString("labelBank.Text"), Wild.Service.Bank(cardRow.Bank));
                }
                table3.EndRow();

                report.AddTable(table3);

                #endregion

                if (!cardRow.IsSubstrateNull())
                {
                    report.AddTable(cardRow.GetSubstrate().GetReport());
                }

                if (cardRow.IsEnvironmentDescribed)
                {
                    //Report.Table table1 = new Report.Table(resources.GetString("tabPageEnvironment.Text"));

                    report.AddTable(cardRow.StateOfWater.GetReport());
                    report.AddTable(cardRow.WeatherConditions.GetReport());
                }

                #region Additional Factors

                if (cardRow.GetFactorValueRows().Length > 0)
                {
                    Report.Table table4 = new Report.Table(resources.GetString("labelFactors.Text"));
                    table4.AddHeader(new string[]{ Wild.Resources.Reports.Caption.Factor,
                            Wild.Resources.Reports.Caption.FactorValue }, new double[] { .80 });

                    foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
                    {
                        table4.StartRow();
                        table4.AddCell(factorValueRow.FactorRow.Factor);
                        table4.AddCellRight(factorValueRow.Value);
                        table4.EndRow();
                    }

                    report.AddTable(table4);
                }

                #endregion

                #region Comments

                Report.Table table5 = new Report.Table(Wild.Resources.Reports.Card.Comments);

                table5.StartRow();
                if (cardRow.IsCommentsNull())
                {
                    table5.AddCell(Constants.Null);
                }
                else
                {
                    table5.AddCell(cardRow.Comments);
                }
                table5.EndRow();
                report.AddTable(table5);

                #endregion
            }

            if (level.HasFlag(CardReportLevel.Species))
            {
                Data.LogRow[] logRows = cardRow.GetLogRows(Wild.UserSettings.LogOrder);

                report.AddSubtitle(resources.GetString("tabPageLog.Text"));

                if (logRows.Length == 0)
                {
                    report.AddParagraph(Resources.Common.EmptySample);
                }
                else
                {
                    //report.BreakPage();
                    report.AddTable(cardRow.GetLogReport(UserSettings.SpeciesIndex, resources.GetString("ColumnMass.HeaderText"), string.Empty));
                }
            }

            List<Data.IndividualRow> individualRows = new List<Data.IndividualRow>();
            foreach (Data.LogRow logRow in cardRow.GetLogRows())
            {
                individualRows.AddRange(logRow.GetIndividualRows());
            }

            if (individualRows.Count > 0)
            {
                if (level.HasFlag(CardReportLevel.Individuals))
                {
                    if (UserSettings.BreakBeforeIndividuals) { report.BreakPage(); }
                    report.AddSubtitle(Wild.Resources.Reports.Header.IndividualsLog);
                    foreach (Data.LogRow logRow in cardRow.GetLogRows())
                    {
                        logRow.AddReport(report);
                        if (UserSettings.BreakBetweenSpecies && cardRow.GetLogRows().Last() != logRow) { report.BreakPage(); }
                    }
                }


                if (level.HasFlag(CardReportLevel.Profile))
                {
                    // TODO: Implement individual reports when 
                    // individual profiles will be implemented
                }

            }
        }





        /// <summary>
        /// Creates report containing Individuals log and/or Individual profiles
        /// </summary>
        /// <param name="indRows"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static Report GetReport(this Data.IndividualRow[] indRows)
        {
            Report report = new Report(Wild.Resources.Reports.Header.IndividualsLog);
            indRows.AddReport(report, string.Empty);
            report.EndBranded();
            return report;
        }

        /// <summary>
        /// Creates report containing Individuals log and Stratified sample
        /// </summary>
        /// <param name="logRow"></param>
        /// <returns></returns>
        public static Report GetReport(this Data.LogRow logRow)
        {
            Report report = new Report(string.Format(Wild.Resources.Interface.Interface.IndLog, logRow.SpeciesRow.Species));
            logRow.AddReport(report);
            report.End(logRow.CardRow.When.Year, logRow.CardRow.Investigator);
            return report;
        }

        /// <summary>
        /// Creates report containing Card with specified detalization level
        /// </summary>
        /// <param name="cardRow"></param>
        /// <returns></returns>
        public static Report GetReport(this Data.CardRow cardRow, CardReportLevel level)
        {
            Report report = new Report(cardRow.FriendlyPath);
            cardRow.AddReport(report, level);
            report.End(cardRow.When.Year, cardRow.Investigator);
            return report;
        }

        /// <summary>
        /// Creates report containing Card
        /// </summary>
        /// <param name="cardRow"></param>
        /// <returns></returns>
        public static Report GetReport(this Data.CardRow cardRow)
        {
            return cardRow.GetReport(CardReportLevel.Note | CardReportLevel.Species | CardReportLevel.Individuals);
        }

        /// <summary>
        /// Creates report containing Card
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Report GetReport(this Data data)
        {
            return data.Solitary.GetReport();
        }
    }

    public static class BenthosReport
    {
        public static Report BlankCard
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Card));
                Report report = new Report(string.Format(
                    "<span class='pretitle'>{0}.</span> [<span style='float:right'>]</span>",
                    FileSystem.GetFriendlyFiletypeName(Benthos.UserSettings.Interface.Extension)));

                #region Common upper part

                Report.Table table1 = new Report.Table(Wild.Resources.Reports.Header.Common);

                table1.StartRow();
                table1.AddCellPrompt(Wild.Resources.Reports.Card.Investigator);
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(Wild.Resources.Reports.Card.When);
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(resources.GetString("labelWater.Text"));
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(Wild.Resources.Reports.Card.Where);
                table1.EndRow();

                report.AddTable(table1);

                #endregion

                #region Sampler

                Report.Table table2 = new Report.Table(resources.GetString("labelMethod.Text"));

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelSampler.Text"), string.Empty, 2);
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelSquare.Text"));
                table2.AddCellPrompt(Benthos.Resources.Interface.Interface.Repeats);
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelMesh.Text"));
                table2.AddCellPrompt(resources.GetString("labelDepth.Text"));
                table2.EndRow();

                report.AddTable(table2);

                #endregion

                #region Site

                Report.Table table3 = new Report.Table(resources.GetString("labelSite.Text"));

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelCrossSection.Text"));
                table3.AddCellPrompt(resources.GetString("labelBank.Text"));
                table3.EndRow();

                report.AddTable(table3);

                #endregion

                #region Substrate

                Report.Table table4 = new Report.Table(resources.GetString("tabPageSubstrate.Text"));

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("checkBoxBoulder.Text"));
                table4.AddCellPrompt(resources.GetString("checkBoxCobble.Text"));
                table4.AddCellPrompt(resources.GetString("checkBoxGravel.Text"));
                table4.AddCellPrompt(resources.GetString("checkBoxSand.Text"));
                table4.AddCellPrompt(resources.GetString("checkBoxSilt.Text"));
                table4.AddCellPrompt(resources.GetString("checkBoxClay.Text"));
                table4.EndRow();

                table4.StartRow();
                table4.AddCellPrompt(Benthos.Resources.Reports.Card.Substrate_1, string.Empty, 6);
                table4.EndRow();

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("checkBoxPhytal.Text"), string.Empty, 3);
                table4.AddCellPrompt(resources.GetString("checkBoxLiving.Text"), string.Empty, 3);
                table4.EndRow();

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("checkBoxWood.Text"), string.Empty, 2);
                table4.AddCellPrompt(resources.GetString("checkBoxCPOM.Text"), string.Empty, 2);
                table4.AddCellPrompt(resources.GetString("checkBoxFPOM.Text"), string.Empty, 2);
                table4.EndRow();

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("checkBoxSapropel.Text"), string.Empty, 3);
                table4.AddCellPrompt(resources.GetString("checkBoxDebris.Text"), string.Empty, 3);
                table4.EndRow();

                report.AddTable(table4);

                #endregion

                #region Environment part

                Report.Table table5 = new Report.Table(resources.GetString("tabPageEnvironment.Text"));

                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelTemperatureBottom.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("labelTemperatureSurface.Text"), string.Empty, 2);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelFlowRate.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("labelLimpidity.Text"), string.Empty, 2);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("checkBoxOdor.Text"));
                table5.AddCellPrompt(resources.GetString("checkBoxSewage.Text"));
                table5.AddCellPrompt(resources.GetString("checkBoxFoam.Text"));
                table5.AddCellPrompt(resources.GetString("checkBoxTurbidity.Text"));
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelColour.Text"), string.Empty, 4);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelConductivity.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("labelDissolvedOxygen.Text"), string.Empty, 2);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelpH.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("labelOxygenSaturation.Text"), string.Empty, 2);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelHumidity.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("labelTemperatureAir.Text"), string.Empty, 2);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("labelAirPressure.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("checkBoxWindRate.Text"), string.Empty, 2);
                table5.EndRow();
                table5.StartRow();
                table5.AddCellPrompt(resources.GetString("checkBoxCloudage.Text"), string.Empty, 2);
                table5.AddCellPrompt(resources.GetString("checkBoxPrecipitation.Text"), string.Empty, 2);
                table5.EndRow();
                report.AddTable(table5);

                #endregion

                #region Comments

                Report.Table table6 = new Report.Table(Wild.Resources.Reports.Card.Comments);
                table6.StartRow();
                table6.AddCell(string.Empty);
                table6.EndRow();
                report.AddTable(table6);

                #endregion

                report.AddTable(Mayfly.Wild.Service.GetBlankTable(resources.GetString("tabPageLog.Text"), Resources.Reports.Caption.Mass, 50));

                return report;
            }
        }

        public static Report BlankIndividualsLog
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Individuals));
                Report report = new Report(Wild.Resources.Reports.Header.IndividualsLog);
                Report.Table table = new Report.Table();
                table.AddHeader(new string[] { "№",
                    resources.GetString("ColumnFrequency.HeaderText"),
                    resources.GetString("ColumnMass.HeaderText"),
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    Wild.Resources.Reports.Card.Comments },
                    new double[] { .05, .1, .1, .1, .1, .1, .45 });
                for (int i = 1; i <= 40; i++)
                {
                    table.StartRow();
                    table.AddCellValue(i);
                    table.AddCell();
                    table.AddCell();
                    table.AddCell();
                    table.AddCell();
                    table.AddCell();
                    table.AddCell();
                    table.EndRow();
                }

                report.AddTable(table);
                return report;
            }
        }
    }
}