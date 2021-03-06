using Mayfly.Wild;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using static Mayfly.Wild.ReaderSettings;

namespace Mayfly.Fish
{
    public static partial class DataExtensions
    {
        /// <summary>
        /// Adds Individual profile into Report
        /// </summary>
        /// <param name="indRow"></param>
        /// <param name="report"></param>
        public static void AddReport(this Wild.Survey.IndividualRow indRow, Report report)
        {
            Wild.Survey.CardRow cardRow = indRow.LogRow.CardRow;

            ResourceManager resources = new ResourceManager(typeof(Individual));
            ResourceManager cardResources = new ResourceManager(typeof(Card));

            Report.Table table1 = new Report.Table(resources.GetString("tabPageGeneral.Text"));

            table1.StartRow();
            table1.AddCellPrompt(Wild.Resources.Reports.Caption.Species, indRow.LogRow.DefinitionRow.KeyRecord.FullNameReport);

            if (indRow.IsTallyNull())
            {
                table1.AddCellPrompt(Resources.Reports.Card.IndNo);
            }
            else
            {
                table1.AddCellPrompt(Resources.Reports.Card.IndNo, indRow.Tally);
            }
            table1.EndRow();

            report.AddTable(table1);

            Report.Table table2 = new Report.Table(cardResources.GetString("tabPageSampling.Text"));

            table2.StartRow();
            if (cardRow.IsWaterIDNull())
            {
                table2.AddCellPrompt(cardResources.GetString("labelWater.Text"));
            }
            else
            {
                table2.AddCellPrompt(cardResources.GetString("labelWater.Text"), cardRow.WaterRow.Presentation);
            }

            if (cardRow.IsWhenNull())
            {
                table2.AddCellPrompt(Wild.Resources.Reports.Card.When, "_____._____._____ ___:___");
            }
            else
            {
                table2.AddCellPrompt(Wild.Resources.Reports.Card.When, cardRow.When, "g");
            }
            table2.EndRow();

            table2.StartRow();
            if (cardRow.IsWhereNull())
            {
                table2.AddCellPrompt(Wild.Resources.Reports.Card.Where, string.Empty, 2);
            }
            else
            {
                table2.AddCellPrompt(Wild.Resources.Reports.Card.Where, cardRow.Position.GetHTMLReference(UI.FormatCoordinate), 2);
            }
            table2.EndRow();

            table2.StartRow();
            table2.AddCellPrompt(Wild.Resources.Reports.Card.Where_1, string.Empty, 2);
            table2.EndRow();

            table2.StartRow();
            table2.AddCellPrompt(cardResources.GetString("labelSampler.Text"), cardRow.IsEqpIDNull() ? string.Empty : cardRow.EquipmentRow.ToString("f"), 2);
            table2.EndRow();

            report.AddTable(table2);

            #region General

            Report.Table table3 = new Report.Table(resources.GetString("labelFBA.Text"));

            table3.StartRow();

            if (indRow.IsLengthNull())
            {
                table3.AddCellPrompt(resources.GetString("labelLength.Text"),
                    string.Empty);
            }
            else
            {
                table3.AddCellPrompt(resources.GetString("labelLength.Text"),
                    indRow.Length);
            }

            if (indRow.IsAgeNull())
            {
                table3.AddCellPrompt(resources.GetString("labelAge.Text"),
                    string.Empty);
            }
            else
            {
                table3.AddCellPrompt(resources.GetString("labelAge.Text"),
                    new Age(indRow.Age));
            }

            table3.EndRow();

            table3.StartRow();

            if (indRow.IsMassNull())
            {
                table3.AddCellPrompt(resources.GetString("labelMass.Text"),
                    string.Empty);
            }
            else
            {
                table3.AddCellPrompt(resources.GetString("labelMass.Text"),
                    indRow.Mass);
            }

            if (indRow.IsSomaticMassNull())
            {
                table3.AddCellPrompt(resources.GetString("labelSomaticMass.Text"),
                    string.Empty);
            }
            else
            {
                table3.AddCellPrompt(resources.GetString("labelSomaticMass.Text"),
                    indRow.SomaticMass);
            }

            table3.EndRow();

            table3.StartRow();

            if (indRow.IsSexNull())
            {
                table3.AddCellPrompt(resources.GetString("labelSex.Text"),
                    string.Empty);
            }
            else
            {
                table3.AddCellPrompt(resources.GetString("labelSex.Text"),
                    new Sex(indRow.Sex));
            }

            if (indRow.IsMaturityNull())
            {
                table3.AddCellPrompt(resources.GetString("labelMaturity.Text"),
                    string.Empty);
            }
            else
            {
                table3.AddCellPrompt(resources.GetString("labelMaturity.Text"),
                    indRow.Maturity);
            }
            table3.EndRow();

            report.AddTable(table3);

            #endregion

            #region Sex

            //if (IndividualDataTable.IsSexExtended(indRow))
            //{
            Report.Table table4 = new Report.Table(resources.GetString("tabPageFecundity.Text"));
            table4.StartRow();

            if (indRow.IsGonadMassNull())
            {
                table4.AddCellPrompt(resources.GetString("labelGonadMass.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelGonadMass.Text"),
                    indRow.GonadMass);
            }

            if (indRow.IsGonadMassNull() || indRow.IsMassNull())
            {
                table4.AddCellPrompt(resources.GetString("labelGonadosomatic.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelGonadosomatic.Text"),
                    (indRow.GonadMass / indRow.Mass).ToString("0.0%"));
            }

            table4.EndRow();

            table4.StartRow();

            if (indRow.IsGonadSampleMassNull())
            {
                table4.AddCellPrompt(resources.GetString("labelGonadSampleMass.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelGonadSampleMass.Text"),
                    indRow.GonadSampleMass);
            }

            if (indRow.IsGonadSampleNull())
            {
                table4.AddCellPrompt(resources.GetString("labelGonadSample.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelGonadSample.Text"),
                    indRow.GonadSample);
            }

            table4.EndRow();

            table4.StartRow();

            if (indRow.IsGonadSampleMassNull())
            {
                table4.AddCellPrompt(resources.GetString("labelFecundityRel.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelFecundityRel.Text"),
                    (indRow.GetRelativeFecundity()).ToString("0.0"));
            }

            if (indRow.IsGonadSampleNull() || indRow.IsGonadMassNull())
            {
                table4.AddCellPrompt(resources.GetString("labelFecundityAbs.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelFecundityAbs.Text"),
                    (indRow.GetAbsoluteFecundity()).ToString("0"));
            }
            table4.EndRow();

            table4.StartRow();

            if (indRow.IsEggSizeNull())
            {
                table4.AddCellPrompt(resources.GetString("labelEggSize.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelEggSize.Text"),
                    indRow.EggSize.ToString("0.00"));
            }

            if (indRow.IsGonadSampleMassNull() || indRow.IsGonadSampleNull())
            {
                table4.AddCellPrompt(resources.GetString("labelEggMass.Text"),
                    string.Empty);
            }
            else
            {
                table4.AddCellPrompt(resources.GetString("labelEggMass.Text"),
                    (indRow.GonadSampleMass / indRow.GonadSample * 1000).ToString("0.0"));
            }
            table4.EndRow();
            report.AddTable(table4);
            //}

            #endregion

            #region Trophics

            Report.Table table5 = new Report.Table(resources.GetString("tabPageTrophics.Text"));

            table5.StartRow();
            if (indRow.IsFatnessNull())
            {
                table5.AddCellPrompt(resources.GetString("labelFatness.Text"),
                    string.Empty, 2);
            }
            else
            {
                if (indRow.Fatness == 0)
                {
                    table5.AddCellPrompt(resources.GetString("labelFatness.Text"),
                        resources.GetString("comboBoxFatness.Items"), 2);
                }
                else
                {
                    table5.AddCellPrompt(resources.GetString("labelFatness.Text"),
                        resources.GetString("comboBoxFatness.Items" + indRow.Fatness), 2);
                }
            }
            table5.EndRow();

            table5.StartRow();
            if (indRow.IsConsumedMassNull())
            {
                table5.AddCellPrompt(resources.GetString("labelConsumedMass.Text"));
            }
            else
            {
                table5.AddCellPrompt(resources.GetString("labelConsumedMass.Text"), indRow.ConsumedMass);
            }

            if (indRow.IsConsumedMassNull() || indRow.IsMassNull())
            {
                table5.AddCellPrompt(resources.GetString("labelConsumptionIndex.Text"));
            }
            else
            {
                table5.AddCellPrompt(resources.GetString("labelConsumptionIndex.Text"),
                    indRow.GetConsumptionIndex().ToString("0.0"));
            }
            table5.EndRow();

            report.AddTable(table5);

            Report.Table table6 = new Report.Table();

            table6.AddHeader(
                resources.GetString("labelTractContent.Text"),
                resources.GetString("labelFullness.Text"),
                resources.GetString("labelFermentation.Text"));


            for (int section = -1; section < 6; section++)
            {
                Wild.Survey.IntestineRow intestineRow = ((Wild.Survey)indRow.Table.DataSet).Intestine.FindByIndIDSection(indRow.ID, section);

                table6.StartRow();

                table6.AddCell(Fish.Service.Section(section));

                if (intestineRow == null || intestineRow.IsFullnessNull())
                {
                    table6.AddCell(string.Empty);
                }
                else
                {
                    if (intestineRow.Fullness == 0)
                    {
                        table6.AddCell(resources.GetString("comboBoxFullness.Items"));
                    }
                    else
                    {
                        table6.AddCell(resources.GetString("comboBoxFullness.Items" + intestineRow.Fullness));
                    }
                }

                if (intestineRow == null || intestineRow.IsFermentationNull())
                {
                    table6.AddCell(string.Empty);
                }
                else
                {
                    if (intestineRow.Fermentation == 0)
                    {
                        table6.AddCell(resources.GetString("comboBoxFermentation.Items"));
                    }
                    else
                    {
                        table6.AddCell(resources.GetString("comboBoxFermentation.Items" + intestineRow.Fermentation));
                    }
                }

                table6.EndRow();
            }

            report.AddTable(table6);

            //report.BreakPage();

            foreach (Wild.Survey.IntestineRow intestineRow in indRow.GetIntestineRows())
            {
                if (!intestineRow.IsConsumedNull())
                {
                    Wild.Survey consumedData = intestineRow.GetConsumed();
                    // Add log
                    report.AddTable(consumedData.Solitary.GetLogReport(resources.GetString("ColumnTrpMass.HeaderText"), Service.Section(intestineRow.Section)));

                    // Add consumed individuals log for each food component
                    foreach (Wild.Survey.LogRow logRow in consumedData.Log.Rows)
                    {
                        Benthos.DataExtensions.AddReport(logRow, report);
                        // It adds log like fish
                        //logRow.AddReport(report);
                    }
                }
            }

            #endregion

            #region Parasites

            if (indRow.ContainsParasites)
            {
                Report.Table table7 = new Report.Table(resources.GetString("tabPageParasites.Text"));

                table7.AddHeader(new string[] {
                        resources.GetString("ColumnParasite.HeaderText"),
                        resources.GetString("ColumnParasiteCount.HeaderText"),
                        resources.GetString("ColumnParasiteComments.HeaderText") },
                    new double[] { .40, .20, .40 });

                foreach (Wild.Survey.OrganRow infectedOrganRow in indRow.GetOrganRows())
                {
                    report.WriteLine("<tbody>");

                    table7.StartRow();
                    if (infectedOrganRow.Organ == 0)
                    {
                        table7.AddCellPrompt(resources.GetString("labelOrgan.Text"),
                            resources.GetString("comboBoxOrgan.Items"), 3);
                    }
                    else
                    {
                        table7.AddCellPrompt(resources.GetString("labelOrgan.Text"),
                            resources.GetString("comboBoxOrgan.Items" + infectedOrganRow.Organ), 3);
                    }

                    table7.EndRow();

                    foreach (Wild.Survey.LogRow logRow in infectedOrganRow.GetInfection().Log)
                    {
                        table7.StartRow();

                        table7.AddCell(logRow.DefinitionRow.Taxon);

                        if (logRow.IsQuantityNull())
                        {
                            table7.AddCell();
                        }
                        else
                        {
                            table7.AddCellRight(logRow.Quantity);
                        }

                        if (logRow.IsCommentsNull())
                        {
                            table7.AddCell(string.Empty);
                        }
                        else
                        {
                            table7.AddCell(logRow.Comments);
                        }

                        table7.EndRow();
                    }

                    report.WriteLine("</tbody>");
                }
                report.AddTable(table7);
            }

            #endregion

            if (indRow.GetValueRows().Length > 0)
            {
                Report.Table table8 = new Report.Table(resources.GetString("tabPageAddt.Text"));
                foreach (Wild.Survey.ValueRow valueRow in indRow.GetValueRows())
                {
                    table8.StartRow();
                    table8.AddCellPrompt(valueRow.VariableRow.Variable,
                        valueRow.Value);
                    table8.EndRow();
                }
                report.AddTable(table8);
            }
        }

        ///// <summary>
        ///// Adds Card (with specified detalization level) into Report
        ///// </summary>
        ///// <param name="cardRow"></param>
        ///// <param name="report"></param>
        ///// <param name="level"></param>
        //public static void AddReport(this Wild.Survey.CardRow cardRow, Report report, CardReportLevel level)
        //{
        //    ResourceManager resources = new ResourceManager(typeof(Card));

        //    if (level.HasFlag(CardReportLevel.Note))
        //    {
        //        report.AddSectionTitle(Wild.Resources.Reports.Header.SampleNote);

        //        #region Common

        //        Report.Table table1 = new Report.Table(Wild.Resources.Reports.Header.Common);

        //        table1.StartRow();
        //        table1.AddCellPrompt(Wild.Resources.Reports.Card.Investigator, cardRow.Investigator, 2);
        //        table1.EndRow();

        //        table1.StartRow();
        //        table1.AddCellPrompt(resources.GetString("labelWater.Text"),
        //            cardRow.IsWaterIDNull() ? Constants.Null : cardRow.WaterRow.Presentation, 2);
        //        table1.EndRow();

        //        table1.StartRow();
        //        table1.AddCellPrompt(resources.GetString("labelLabel.Text"),
        //                cardRow.IsLabelNull() ? Constants.Null : cardRow.Label);
        //        table1.AddCellPrompt(Wild.Resources.Reports.Card.When, cardRow.IsWhenNull() ? Constants.Null :
        //                (cardRow.When.ToShortDateString() + " " + cardRow.When.ToString("HH:mm")));
        //        table1.EndRow();

        //        table1.StartRow();
        //        table1.AddCellPrompt(Wild.Resources.Reports.Card.Where, cardRow.Position.GetHTMLReference(UI.FormatCoordinate), 2);
        //        table1.EndRow();
        //        report.AddTable(table1);

        //        //if (cardRow.IsDepthNull())
        //        //{
        //        //    table1.AddCellPrompt(resources.GetString("labelDepth.Text"));
        //        //}
        //        //else
        //        //{
        //        //    table1.AddCellPrompt(resources.GetString("labelDepth.Text"), cardRow.Depth);
        //        //}

        //        #endregion

        //        #region Sampler

        //        Report.Table table2 = new Report.Table(resources.GetString("labelMethod.Text"));

        //        if (!cardRow.IsEqpIDNull())
        //        {
        //            table2.StartRow();
        //            table2.AddCellPrompt(resources.GetString("labelSampler.Text"), cardRow.SamplerRow, 2);
        //            table2.EndRow();

        //            table2.StartRow();
        //            table2.AddCellPrompt(resources.GetString("labelMesh.Text"), cardRow.IsMeshNull() ? Constants.Null : cardRow.Mesh.ToString());
        //            table2.AddCellPrompt(resources.GetString("labelHook.Text"), cardRow.IsHookNull() ? Constants.Null : cardRow.Hook.ToString());
        //            table2.EndRow();

        //            table2.StartRow();
        //            table2.AddCellPrompt(resources.GetString("labelLength.Text"), cardRow.IsLengthNull() ? Constants.Null : cardRow.Length.ToString());
        //            table2.AddCellPrompt(resources.GetString("labelOpening.Text"), cardRow.IsOpeningNull() ? Constants.Null : cardRow.Opening.ToString());
        //            table2.EndRow();

        //            table2.StartRow();
        //            table2.AddCellPrompt(resources.GetString("labelHeight.Text"), cardRow.IsHeightNull() ? Constants.Null : cardRow.Height.ToString());
        //            table2.AddCellPrompt(resources.GetString("labelSquare.Text"), cardRow.IsSquareNull() ? Constants.Null : cardRow.Square.ToString());
        //            table2.EndRow();

        //            report.AddTable(table2);

        //            Report.Table table3 = new Report.Table(resources.GetString("labelEffort.Text"));

        //            table3.StartRow();
        //            if (cardRow.IsSpanNull())
        //            {
        //                table3.AddCellPrompt(resources.GetString("labelOperation.Text"));
        //                table3.AddCellPrompt(Resources.Reports.Card.Duration);
        //            }
        //            else
        //            {
        //                table3.AddCellPrompt(resources.GetString("labelOperation.Text"), (cardRow.WhenStarted.ToShortDateString() + " " + cardRow.WhenStarted.ToString("HH:mm")));
        //                table3.AddCellPrompt(Resources.Reports.Card.Duration, string.Format("{0:N0}:{1:mm}", cardRow.Duration.TotalHours, cardRow.Duration));
        //            }
        //            table3.EndRow();

        //            table3.StartRow();
        //            table3.AddCellPrompt(resources.GetString("labelVelocity.Text"),
        //                cardRow.IsVelocityNull() ? Constants.Null : cardRow.Velocity.ToString());
        //            table3.AddCellPrompt(resources.GetString("labelExposure.Text"),
        //                cardRow.IsExposureNull() ? Constants.Null : cardRow.Exposure.ToString());
        //            table3.EndRow();

        //            table3.StartRow();
        //            table3.AddCellPrompt(resources.GetString("labelArea.Text"),
        //                cardRow.GetSquare().ToString("N3"));
        //            table3.AddCellPrompt(resources.GetString("labelVolume.Text"),
        //                cardRow.GetVolume().ToString("N3"));
        //            table3.EndRow();

        //            table3.StartRow();
        //            table3.AddCellPrompt(resources.GetString("labelEfforts.Text"),
        //                cardRow.GetEffort().ToString("N3"), 2);
        //            table3.EndRow();

        //            report.AddTable(table3);
        //        }

        //        #endregion

        //        #region Environment

        //        if (cardRow.IsEnvironmentDescribed)
        //        {
        //            report.AddTable(cardRow.StateOfWater.GetReport());
        //            report.AddTable(cardRow.WeatherConditions.GetReport());
        //        }

        //        #endregion

        //        #region Additional Factors

        //        if (cardRow.GetFactorValueRows().Length > 0)
        //        {
        //            Report.Table table5 = new Report.Table(resources.GetString("labelFactors.Text"));

        //            table5.AddHeader(new string[]{ Wild.Resources.Reports.Caption.Factor,
        //                    Wild.Resources.Reports.Caption.FactorValue }, new double[] { .80 });

        //            foreach (Wild.Survey.FactorValueRow FVR in cardRow.GetFactorValueRows())
        //            {
        //                table5.StartRow();
        //                table5.AddCell(FVR.FactorRow.Factor);
        //                table5.AddCellRight(FVR.Value);
        //                table5.EndRow();
        //            }

        //            report.AddTable(table5);
        //        }

        //        #endregion

        //        #region Comments

        //        Report.Table table4 = new Report.Table(Wild.Resources.Reports.Card.Comments);
        //        table4.StartRow();
        //        table4.AddCell(cardRow.IsCommentsNull() ? Constants.Null : cardRow.Comments);
        //        table4.EndRow();
        //        report.AddTable(table4);

        //        #endregion
        //    }

        //    if (level.HasFlag(CardReportLevel.Log))
        //    {
        //        Wild.Survey.LogRow[] LogRows = cardRow.GetLogRows();

        //        report.AddSectionTitle(resources.GetString("tabPageLog.Text"));

        //        if (LogRows.Length == 0)
        //        {
        //            report.AddParagraph(Wild.Resources.Common.EmptySample);
        //        }
        //        else
        //        {
        //            //report.BreakPage();
        //            report.AddTable(cardRow.GetLogReport(resources.GetString("ColumnMass.HeaderText"), string.Empty));
        //        }
        //    }

        //    List<Wild.Survey.IndividualRow> individualRows = new List<Wild.Survey.IndividualRow>();
        //    foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
        //    {
        //        individualRows.AddRange(logRow.GetIndividualRows());
        //    }

        //    List<Wild.Survey.StratifiedRow> stratifiedRows = new List<Wild.Survey.StratifiedRow>();
        //    foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
        //    {
        //        stratifiedRows.AddRange(logRow.GetStratifiedRows());
        //    }

        //    if ((individualRows.Count + stratifiedRows.Count) > 0 && (level.HasFlag(CardReportLevel.Individuals) || level.HasFlag(CardReportLevel.Stratified)))
        //    {
        //        if (UserSettings.ReaderSettings.BreakBeforeIndividuals) { report.BreakPage(); }
        //        report.AddSectionTitle(Wild.Resources.Reports.Header.IndividualsLog);
        //        foreach (Wild.Survey.LogRow logRow in cardRow.GetLogRows())
        //        {
        //            string speciesPresentation = logRow.DefinitionRow.KeyRecord.FullNameReport;
        //            logRow.AddReport(report, level, speciesPresentation, string.Format(Wild.Resources.Reports.Header.StratifiedSample, speciesPresentation));
        //            if (UserSettings.ReaderSettings.BreakBetweenSpecies && cardRow.GetLogRows().Last() != logRow) { report.BreakPage(); }
        //        }
        //    }

        //    if (individualRows.Count > 0 && level.HasFlag(CardReportLevel.Profile))
        //    {
        //        if (UserSettings.ReaderSettings.BreakBeforeIndividuals) { report.BreakPage(); }
        //        individualRows.ToArray().AddReport(report, CardReportLevel.Profile, string.Empty);
        //    }
        //}
    }

    public static class FishReport
    {
        public static Report BlankCard
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Card));
                Report report = new Report(string.Format(
                    "<span class='pretitle'>{0}.</span> [<span style='float:right'>]</span>",
                    IO.GetFriendlyFiletypeName(Interface.Extension)));

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
                table2.AddCellPrompt(resources.GetString("labelSampler.Text"));
                table2.AddCellPrompt(resources.GetString("labelMesh.Text"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(Fish.Resources.Reports.Card.Length);
                table2.AddCellPrompt(Fish.Resources.Reports.Card.Opening);
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(Fish.Resources.Reports.Card.Height);
                table2.AddCellPrompt(resources.GetString("labelDepth.Text"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(Fish.Resources.Reports.Card.Exposure, string.Empty, 2);
                table2.EndRow();

                report.AddTable(table2);

                #endregion

                #region Environment part

                Report.Table table3 = new Report.Table(resources.GetString("tabPageEnvironment.Text"));

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelTemperatureBottom.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("labelTemperatureSurface.Text"), string.Empty, 2);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelFlowRate.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("labelLimpidity.Text"), string.Empty, 2);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("checkBoxOdor.Text"));
                table3.AddCellPrompt(resources.GetString("checkBoxSewage.Text"));
                table3.AddCellPrompt(resources.GetString("checkBoxFoam.Text"));
                table3.AddCellPrompt(resources.GetString("checkBoxTurbidity.Text"));
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelColour.Text"), string.Empty, 4);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelConductivity.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("labelDissolvedOxygen.Text"), string.Empty, 2);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelpH.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("labelOxygenSaturation.Text"), string.Empty, 2);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelHumidity.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("labelTemperatureAir.Text"), string.Empty, 2);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelAirPressure.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("checkBoxWindRate.Text"), string.Empty, 2);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("checkBoxCloudage.Text"), string.Empty, 2);
                table3.AddCellPrompt(resources.GetString("checkBoxPrecipitation.Text"), string.Empty, 2);
                table3.EndRow();

                report.AddTable(table3);

                #endregion

                #region Comments

                Report.Table table4 = new Report.Table(Wild.Resources.Reports.Card.Comments);
                table4.StartRow();
                table4.AddCell(string.Empty);
                table4.EndRow();
                report.AddTable(table4);

                #endregion

                report.AddTable(Mayfly.Wild.Service.GetBlankTable(resources.GetString("tabPageLog.Text"), 
                    Wild.Resources.Reports.Caption.MassUnit, 15));

                return report;
            }
        }

        public static Report BlankIndividualsLog
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Individuals));
                Report report = new Report(Wild.Resources.Reports.Header.IndividualsLog);
                Report.Table table = new Report.Table(Resources.Reports.Header.FBA);
                table.AddHeader(new string[] { "№",
                            resources.GetString("ColumnLength.HeaderText"),
                            resources.GetString("ColumnMass.HeaderText"),
                            resources.GetString("ColumnAge.HeaderText"),
                            resources.GetString("ColumnSex.HeaderText"),
                            resources.GetString("ColumnMaturity.HeaderText"),
                            Wild.Resources.Reports.Card.Comments },
                    new double[] { .05, .1, .1, .1, .1, .1, .45 });
                for (int i = 1; i <= 30; i++)
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
                report.AddCribnote(Wild.Service.GetStratifiedNote(50, 300, Fish.UserSettings.DefaultStratifiedInterval, 15));
                return report;
            }
        }

        public static Report BlankIndividualProfile
        {
            get
            {
                ResourceManager resources = new ResourceManager(typeof(Individual));

                string app1 = string.Format("<span class='leftcomment'>{0}</span>",
                    resources.GetString("comboBoxMaturity.Items"));
                for (int i = 1; i < 6; i++)
                {
                    app1 += string.Format("<br><span class='leftcomment'>{0}</span>",
                        resources.GetString("comboBoxMaturity.Items" + i));
                }

                string app2 = string.Format("<span class='leftcomment'>{0}</span>",
                    resources.GetString("comboBoxFatness.Items"));
                for (int i = 1; i < 6; i++)
                {
                    app2 += string.Format("<br><span class='leftcomment'>{0}</span>",
                        resources.GetString("comboBoxFatness.Items" + i));
                }

                string app3 = string.Format("<span class='leftcomment'>{0}</span>",
                    resources.GetString("comboBoxFullness.Items"));
                for (int i = 1; i < 6; i++)
                {
                    app3 += string.Format("<br><span class='leftcomment'>{0}</span>",
                        resources.GetString("comboBoxFullness.Items" + i));
                }

                string app4 = string.Format("<span class='leftcomment'>{0}</span>",
                    resources.GetString("comboBoxFermentation.Items"));
                for (int i = 1; i < 6; i++)
                {
                    app4 += string.Format("<br><span class='leftcomment'>{0}</span>",
                        resources.GetString("comboBoxFermentation.Items" + i));
                }


                Report report = new Report(Wild.Resources.Reports.Header.IndividualProfile);

                Report.Table table1 = new Report.Table(resources.GetString("tabPageGeneral.Text"));
                table1.StartRow();
                table1.AddCell(Wild.Resources.Reports.Caption.Species);
                table1.AddCell(Resources.Reports.Card.IndNo);
                table1.EndRow();
                report.AddTable(table1);

                #region General

                Report.Table table2 = new Report.Table(resources.GetString("labelFBA.Text"));

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelLength.Text"));
                table2.AddCellPrompt(resources.GetString("labelMaturity.Text"), app1, 5, CellSpan.Rows);
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelSex.Text"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelAge.Text"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelMass.Text"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelSomaticMass.Text"));
                table2.EndRow();

                report.AddTable(table2);

                #endregion

                #region Sex

                Report.Table table3 = new Report.Table(resources.GetString("tabPageMaturity.Text"));
                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelGonadMass.Text"),
                        string.Empty);
                table3.AddCellPrompt(resources.GetString("labelGonadosomatic.Text"),
                        string.Empty);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelGonadSampleMass.Text"),
                        string.Empty);
                table3.AddCellPrompt(resources.GetString("labelGonadSample.Text"),
                        string.Empty);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelFecundityRel.Text"),
                        string.Empty);
                table3.AddCellPrompt(resources.GetString("labelFecundityAbs.Text"),
                        string.Empty);
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelEggSize.Text"),
                        string.Empty);
                table3.AddCellPrompt(resources.GetString("labelEggMass.Text"),
                        string.Empty);
                table3.EndRow();
                report.AddTable(table3);

                #endregion

                #region Trophics

                Report.Table table4 = new Report.Table(resources.GetString("tabPageTrophics.Text"));

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("labelConsumedMass.Text"));
                //table4.EndRow();

                //table4.StartRow();
                table4.AddCellPrompt(resources.GetString("labelFatness.Text"), app2, 2, CellSpan.Rows);
                table4.EndRow();

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("labelConsumptionIndex.Text"));
                table4.EndRow();

                report.AddTable(table4);

                //Report.Table table1 = new Report.Table(new string[] { "margin-top" }, new string[] { "5px" });   
                Report.Table table5 = new Report.Table();
                table5.StartHeader();
                table5.AddHeaderCell(resources.GetString("labelTractContent.Text"), .25);
                table5.AddCellPrompt(resources.GetString("labelFullness.Text"), app3);
                table5.AddCellPrompt(resources.GetString("labelFermentation.Text"), app4);
                table5.EndHeader();

                for (int sect = 0; sect < 6; sect++)
                {
                    table5.StartRow();
                    if (sect == 0)
                    {
                        table5.AddCell(resources.GetString("comboBoxSection.Items"));
                    }
                    else
                    {
                        table5.AddCell(resources.GetString("comboBoxSection.Items" + sect));
                    }

                    table5.AddCell(string.Empty);
                    table5.AddCell(string.Empty);
                    table5.EndRow();
                }

                report.AddTable(table5);

                #endregion

                //if (true)
                //{
                //report.BreakPage();
                report.AddTable(Wild.Service.GetBlankTable(string.Empty, 
                    Wild.Resources.Reports.Caption.MassUnit, 20));
                //}

                report.End();

                return report;
            }
        }
    }
}