using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly;
using Mayfly.Wild;
using System.Resources;
using Mayfly.Extensions;
using Mayfly.Species;
using Mayfly.Fish;
using Meta.Numerics;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        /// <summary>
        /// Adds general information on cards in Stack into Report
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="report"></param>
        public static void AddCommon(this CardStack stack, Report report)
        {
            AddCommon(stack, report, null);
        }

        /// <summary>
        /// Adds general information on cards in Stack into Report. Notices not related to SpeciesRow will not be included
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="report"></param>
        /// <param name="speciesRow"></param>
        public static void AddCommon(this CardStack stack, Report report, Data.SpeciesRow speciesRow)
        {
            List<string> warnings = new List<string>();

            if (UserSettings.CheckConsistency)
            {
                if (speciesRow == null)
                {
                    var diagnostics = stack.CheckConsistency();

                    if (diagnostics.Length > 0)
                    {
                        foreach (var diagnostic in diagnostics)
                        {
                            if (diagnostic is CardConsistencyChecker ccc)
                            {
                                ccc.Filterate(UserSettings.ReportCriticality);

                                if (ccc.ArtifactsCount > 0 && ccc.WorstCriticality >= UserSettings.ReportCriticality)
                                {
                                    warnings.Add(diagnostic.ToString());
                                }
                            }

                            if (diagnostic is SpeciesConsistencyChecker scc)
                            {
                                scc.Filterate(UserSettings.ReportCriticality);

                                if (scc.ArtifactsCount > 0 && scc.WorstCriticality >= UserSettings.ReportCriticality)
                                {
                                    warnings.Add(diagnostic.ToString());
                                }
                            }
                        }
                    }
                }
                else
                {
                    var scc = speciesRow.CheckConsistency(stack);

                    scc.Filterate(UserSettings.ReportCriticality);

                    if (scc.ArtifactsCount > 0 && scc.WorstCriticality >= UserSettings.ReportCriticality)
                    {
                        warnings.AddRange(scc.GetNotices(true));
                        //warnings.AddRange(LogConsistencyChecker.GetAllNotices(scc.LogArtifacts));
                        //warnings.AddRange(IndividualConsistencyChecker.GetAllNotices(scc.IndividualArtifacts));
                    }
                }
            }
                        
            stack.AddCommon(report, warnings.ToArray());
        }


        #region Sourcesheets

        public static Report GetCardReport(this CardStack stack, CardReportLevel level)
        {
            Report report = new Report(string.Empty);

            bool first = true;
            foreach (Data.CardRow cardRow in stack)
            {
                if (first) { first = false; } else { report.BreakPage(Fish.UserSettings.OddCardStart ? PageBreakOption.Odd : PageBreakOption.None); }
                report.AddHeader(cardRow.FriendlyPath);
                cardRow.AddReport(report, level);
            }

            report.End();

            return report;
        }

        #endregion

        #region Gear stats

        //public static void AddSampleSizeReport(this CardStack stack, Report report)
        //{
        //    report.AddParagraph(Resources.Reports.GearStats.Paragraph0, report.NextTableNumber);

        //    Report.Table table1 = new Report.Table(Resources.Reports.GearStats.Table2_1);

        //    table1.StartHeader();
        //    table1.StartRow();
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25, 2);
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity, 4);
        //    table1.AddHeaderCell(Resources.Reports.Common.MassUnits, 2, CellSpan.Rows);
        //    table1.EndRow();
        //    table1.StartRow();
        //    table1.AddHeaderCell(Mayfly.Resources.Interface.Total);
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Length);
        //    table1.AddHeaderCell(Fish.Resources.Reports.Caption.Mass);
        //    table1.AddHeaderCell(Wild.Resources.Reports.Caption.Age);
        //    table1.EndRow();
        //    table1.EndHeader();

        //    double Q = 0;
        //    double L = 0;
        //    double W = 0;
        //    double A = 0;
        //    double M = 0;

        //    foreach (Data.SpeciesRow speciesRow in stack.GetSpeciesCaught())
        //    {
        //        table1.StartRow();
        //        double q = stack.Quantity(speciesRow);
        //        double l = stack.Measured(speciesRow);
        //        double w = stack.Weighted(speciesRow);
        //        double a = stack.Aged(speciesRow);
        //        double m = stack.Mass(speciesRow);
        //        Q += q;
        //        L += l;
        //        W += w;
        //        A += a;
        //        M += m;

        //        table1.AddCell(speciesRow.KeyRecord.ShortName);
        //        table1.AddCellRight(q > 0 ? q.ToString() : Constants.Null);
        //        table1.AddCellRight(l > 0 ? l.ToString() : Constants.Null);
        //        table1.AddCellRight(w > 0 ? w.ToString() : Constants.Null);
        //        table1.AddCellRight(a > 0 ? a.ToString() : Constants.Null);
        //        table1.AddCellRight(m, Textual.Mask(3));

        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total, ReportCellClass.Bold);
        //    table1.AddCellRight(Q > 0 ? Q.ToString() : Constants.Null, true);
        //    table1.AddCellRight(L > 0 ? L.ToString() : Constants.Null, true);
        //    table1.AddCellRight(W > 0 ? W.ToString() : Constants.Null, true);
        //    table1.AddCellRight(A > 0 ? A.ToString() : Constants.Null, true);
        //    table1.AddCellRight(M, Textual.Mask(3), true);
        //    table1.EndRow();

        //    report.AddTable(table1);
        //}

        public static Report.Table GetSampleSizeTable(this CardStack stack, string caption, FishSamplerType samplerType)
        {
            Report.Table table = new Report.Table(caption) { ZeroToLongDash = true };

            table.StartHeader();

            table.StartRow();

            table.AddHeaderCell(string.Empty, .05, 2, CellSpan.Rows);
            table.AddHeaderCell(string.Empty, .2, 2, CellSpan.Rows);

            table.AddHeaderCell(Wild.Resources.Reports.Caption.QuantityUnit, 9);

            table.AddHeaderCell(Resources.Reports.Common.MassUnits, 3);
            table.EndRow();

            table.StartRow();

            table.AddHeaderCell("Total sample quantity (catch counted)");
            table.AddHeaderCell("Stratified samples");
            table.AddHeaderCell("Log records");
            table.AddHeaderCell("Length measured");
            table.AddHeaderCell("Mass measured");
            table.AddHeaderCell("Samples taken (tally given)");
            table.AddHeaderCell("Age read");
            table.AddHeaderCell("Sex defined");
            table.AddHeaderCell("Maturity defined");

            table.AddHeaderCell("Total mass");
            table.AddHeaderCell("Stratified samples");
            table.AddHeaderCell("Individual samples");

            table.EndRow();
            table.EndHeader();
            
            string[] gearClasses = stack.Classes(samplerType);

            foreach (string gearClass in gearClasses)
            {
                CardStack gearClassData = stack.GetStack(samplerType, gearClass);

                // Set totals row
                table.StartRow();
                table.AddCell(gearClass);
                table.AddCell(string.Empty);
                table.AddCellRight(gearClassData.Quantity());
                table.AddCellRight(gearClassData.QuantityStratified());
                table.AddCellRight(gearClassData.QuantityIndividual());
                table.AddCellRight(gearClassData.Measured());
                table.AddCellRight(gearClassData.Weighted());
                table.AddCellRight(gearClassData.Tallied());
                table.AddCellRight(gearClassData.Aged());
                table.AddCellRight(gearClassData.Sexed());
                table.AddCellRight(gearClassData.Matured());

                table.AddCellRight(gearClassData.Mass());
                table.AddCellRight(gearClassData.MassStratified());
                table.AddCellRight(gearClassData.MassIndividual());
                table.EndRow();


                foreach (Data.SpeciesRow speciesRow in gearClassData.GetSpecies())
                {
                    // Set class row
                    table.StartRow();
                    table.AddCell(string.Empty);
                    table.AddCell(speciesRow);
                    table.AddCellRight(gearClassData.Quantity(speciesRow));
                    table.AddCellRight(gearClassData.QuantityStratified(speciesRow));
                    table.AddCellRight(gearClassData.QuantityIndividual(speciesRow));
                    table.AddCellRight(gearClassData.Measured(speciesRow));
                    table.AddCellRight(gearClassData.Weighted(speciesRow));
                    table.AddCellRight(gearClassData.Tallied(speciesRow));
                    table.AddCellRight(gearClassData.Aged(speciesRow));
                    table.AddCellRight(gearClassData.Sexed(speciesRow));
                    table.AddCellRight(gearClassData.Matured(speciesRow));

                    table.AddCellRight(gearClassData.Mass(speciesRow));
                    table.AddCellRight(gearClassData.MassStratified(speciesRow));
                    table.AddCellRight(gearClassData.MassIndividual(speciesRow));
                    table.EndRow();
                }
            }

            return table;
        }

        public static void AddGearStatsReport(this CardStack stack, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            stack.AddEffortsSection(report, samplerType, ue);
            CardStack samplerData = stack.GetStack(samplerType);

            #region Sample Size

            report.AddSectionTitle(Resources.Reports.Sections.SampleSize.Subtitle);
            report.AddTable(GetSampleSizeTable(samplerData, "Sample sizes", samplerType), "airy");

            #endregion

            #region Sample Detailed

            report.AddSectionTitle(Resources.Reports.Sections.SampleDetailed.Subtitle);
            int no = 1;
            foreach (Data.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;

                report.AddParagraph(no + ") " + Resources.Reports.Sections.SampleDetailed.Paragraph_1 + (cardRow.Quantity == 0 ? " " + Resources.Reports.Sections.SampleDetailed.Paragraph_1_1 : string.Empty),
                    cardRow, 
                    cardRow.GetEffort(), 
                    cardRow.GetGearType().GetDefaultUnitEffort().Unit);

                // TODO: Add gillnet parameters, time of setting etc.


                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    bool justregistered = logRow.DetailedQuantity== 0;

                    report.AddParagraph(Resources.Reports.Sections.SampleDetailed.Paragraph_2 + (justregistered ? " " + Resources.Reports.Sections.SampleDetailed.Paragraph_2_1 : string.Empty),
                        logRow.SpeciesRow.KeyRecord.FullNameReport,
                        logRow.IsQuantityNull() ? Constants.Null : logRow.Quantity.ToString(),
                        logRow.IsMassNull() ? Constants.Null : logRow.Mass.ToString());

                    if (!justregistered)
                    {
                        logRow.AddReport(report, CardReportLevel.Individuals | CardReportLevel.Stratified, string.Empty, string.Empty);
                    }
                }

                no++;
            }

            #endregion
        }

        public static Report.Table GetEffortsTable(this IEnumerable<CardStack> stacks, FishSamplerType samplerType, UnitEffort ue)
        {
            Report.Table table = new Report.Table(Resources.Reports.Sections.Efforts.Table, samplerType.ToDisplay());

            table.StartRow();
            table.AddHeaderCell(Resources.Reports.Caption.GearClass, .25);
            table.AddHeaderCell(Resources.Reports.Caption.Ops);
            table.AddHeaderCell(string.Format(Resources.Reports.Caption.Efforts, ue.Unit));
            table.AddHeaderCell(Resources.Reports.Caption.Fraction);
            table.EndRow();

            List<double> efforts = new List<double>();

            foreach (CardStack meshData in stacks)
            {
                efforts.Add(meshData.GetEffort(samplerType, ue.Variant));
            }

            int i = 0;

            foreach (CardStack meshData in stacks)
            {
                table.StartRow();
                table.AddCellValue(meshData.Name);
                table.AddCellRight(meshData.Count);
                table.AddCellRight(efforts[i], "N3");
                table.AddCellRight(efforts[i] / efforts.Sum(), "P1");
                table.EndRow();
                i++;
            }

            table.StartRow();
            table.AddCell(Mayfly.Resources.Interface.Total);
            table.AddCellRight(stacks.Count());
            table.AddCellRight(efforts.Sum(), "N3");
            table.AddCellRight(1, "P1");
            table.EndRow();

            return table;
        }

        public static void AddEffortsSection(this CardStack stack, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            string[] classes = stack.Classes(samplerType);

            report.AddSectionTitle(Resources.Reports.Sections.Efforts.Header);

            if (classes.Length > 1)
            {
                report.AddParagraph(Resources.Reports.Sections.Efforts.Paragraph1,
                    samplerType.ToDisplay(), classes.Length,
                    classes[0], classes.Last(),
                    ue.UnitDescription, report.NextTableNumber);

                report.AddTable(stack.GetClassedStacks(samplerType).GetEffortsTable(samplerType, ue));
            }
            else
            {
                report.AddParagraph(Resources.Reports.Sections.Efforts.Paragraph2,
                    samplerType.ToDisplay(), ue.UnitDescription, stack.GetEffort(samplerType, ue.Variant), ue.Unit);
            }
        }

        public static void AddGearStatsReport(this CardStack stack, Report report)
        {
            foreach (FishSamplerType samplerType in stack.GetSamplerTypes())
            {
                report.AddChapterTitle(samplerType.ToDisplay());
                stack.AddGearStatsReport(report, samplerType, samplerType.GetDefaultUnitEffort());
            }
        }
        
        #endregion

        #region Species stats

        #region Stratified tables

        public static Report GetStratifiedCribnote(this CardStack stack)
        {
            return stack.GetStratifiedCribnote(stack.GetSpeciesCaught(15));
        }

        public static Report GetStratifiedCribnote(this CardStack stack, Data.SpeciesRow[] speciesRows)
        {
            Report report = new Report(Resources.Reports.Header.StratifiedCribnote, "strates.css");

            foreach (Data.SpeciesRow speciesRow in speciesRows)
            {
                // TODO: Skip if no samples?
                Report.Table cribnote = Wild.Service.GetStratifiedNote(string.Format(Wild.Resources.Reports.Header.StratifiedSample, speciesRow.KeyRecord.FullNameReport),
                    stack.LengthMin(speciesRow), stack.LengthMax(speciesRow), UserSettings.SizeInterval, (l) =>
                    {
                        Interval strate = Interval.FromEndpointAndWidth(l, UserSettings.SizeInterval);
                        int aged = stack.Aged(speciesRow, strate);
                        int reged = stack.QuantityRegisteredNonAged(speciesRow, strate);
                        return aged.ToStratifiedDots() + ((aged > 0 && reged > 0) ? " <br /> " : string.Empty) + (reged > 0 ? ("(" + reged.ToStratifiedDots() + ")") : string.Empty);
                    });
                    
                if (cribnote == null) continue;
                report.AddCribnote(cribnote);
                report.BreakPage();
            }

            report.End();

            return report;
        }

        #endregion

        //#region Treatment suggestions

        //public static Report GetTreatmentNote(this CardStack stack, System.Data.DataColumn column)
        //{
        //    return stack.GetTreatmentNote(stack.GetSpecies(), column);
        //}

        //public static Report GetTreatmentNote(this CardStack stack, Data.SpeciesRow[] speciesRows, System.Data.DataColumn column)
        //{
        //    Report report = new Report(string.Format(Resources.Reports.Sections.TreatmentSuggestion.Title, Service.Localize(column.Caption)));

        //    report.AddParagraph(Resources.Reports.Sections.TreatmentSuggestion.Par1, Service.Localize(column.Caption));

        //    foreach (Data.SpeciesRow speciesRow in speciesRows)
        //    {
        //        stack.GetTreatmentNote(report, speciesRow, column);
        //    }

        //    report.End();

        //    return report;
        //}

        //public static void GetTreatmentNote(this CardStack stack, Report report, Data.SpeciesRow speciesRow, System.Data.DataColumn column)
        //{
        //    TreatmentSuggestion suggestion = stack.GetTreatmentSuggestion(speciesRow, column);
        //    if (suggestion == null) return;
        //    report.AddSubtitle(speciesRow.GetReportFullPresentation());
        //    report.AddParagraph(suggestion.GetNote());
        //}

        //#endregion

        public static Report.Table GetSampleSizeTable(this CardStack stack, Data.SpeciesRow speciesRow, string caption)
        {
            Report.Table table = new Report.Table(caption) { ZeroToLongDash = true };

            table.StartHeader();

            table.StartRow();

            table.AddHeaderCell(string.Empty, .05, 2, CellSpan.Rows);
            table.AddHeaderCell(string.Empty, .2, 2, CellSpan.Rows);

            table.AddHeaderCell(Wild.Resources.Reports.Caption.QuantityUnit, 9);

            table.AddHeaderCell(Resources.Reports.Common.MassUnits, 3);
            table.EndRow();

            table.StartRow();

            table.AddHeaderCell("Total sample quantity (catch counted)");
            table.AddHeaderCell("Stratified samples");
            table.AddHeaderCell("Log records");
            table.AddHeaderCell("Length measured");
            table.AddHeaderCell("Mass measured");
            table.AddHeaderCell("Samples taken (tally given)");
            table.AddHeaderCell("Age read");
            table.AddHeaderCell("Sex defined");
            table.AddHeaderCell("Maturity defined");

            table.AddHeaderCell("Total mass");
            table.AddHeaderCell("Stratified samples");
            table.AddHeaderCell("Individual samples");

            table.EndRow();
            table.EndHeader();

            foreach (FishSamplerType samplerType in stack.GetSamplerTypes())
            {
                CardStack samplerData = stack.GetStack(samplerType);

                // Set totals row
                table.StartRow();
                table.AddCell(samplerType.ToDisplay(), 2, CellSpan.Columns);
                table.AddCellRight(samplerData.Quantity(speciesRow), true);
                table.AddCellRight(samplerData.QuantityStratified(speciesRow), true);
                table.AddCellRight(samplerData.QuantityIndividual(speciesRow), true);
                table.AddCellRight(samplerData.Measured(speciesRow), true);
                table.AddCellRight(samplerData.Weighted(speciesRow), true);
                table.AddCellRight(samplerData.Tallied(speciesRow), true);
                table.AddCellRight(samplerData.Aged(speciesRow), true);
                table.AddCellRight(samplerData.Sexed(speciesRow), true);
                table.AddCellRight(samplerData.Matured(speciesRow), true);

                table.AddCellRight(samplerData.Mass(speciesRow), true);
                table.AddCellRight(samplerData.MassStratified(speciesRow), true);
                table.AddCellRight(samplerData.MassIndividual(speciesRow), true);
                table.EndRow();

                string[] gearClasses = stack.Classes(samplerType);

                foreach (string gearClass in gearClasses)
                {
                    CardStack gearClassData = stack.GetStack(samplerType, gearClass);

                    // Set class row
                    table.StartRow();
                    table.AddCell(string.Empty);
                    table.AddCell(gearClass);
                    table.AddCellRight(gearClassData.Quantity(speciesRow));
                    table.AddCellRight(gearClassData.QuantityStratified(speciesRow));
                    table.AddCellRight(gearClassData.QuantityIndividual(speciesRow));
                    table.AddCellRight(gearClassData.Measured(speciesRow));
                    table.AddCellRight(gearClassData.Weighted(speciesRow));
                    table.AddCellRight(gearClassData.Tallied(speciesRow));
                    table.AddCellRight(gearClassData.Aged(speciesRow));
                    table.AddCellRight(gearClassData.Sexed(speciesRow));
                    table.AddCellRight(gearClassData.Matured(speciesRow));

                    table.AddCellRight(gearClassData.Mass(speciesRow));
                    table.AddCellRight(gearClassData.MassStratified(speciesRow));
                    table.AddCellRight(gearClassData.MassIndividual(speciesRow));
                    table.EndRow();
                }
            }

            return table;
        }

        public static void AddSpeciesStatsReport(this CardStack stack, Report report, Data.SpeciesRow speciesRow, SpeciesStatsLevel level, ExpressionVariant variant)
        {
            if (level.HasFlag(SpeciesStatsLevel.Totals))
            {
                report.AddSectionTitle(Resources.Reports.Sections.SpeciesStats.Header1);
                report.AddTable(GetSampleSizeTable(stack, speciesRow, "Sample sizes"), "airy");
                
                int a = stack.Aged(speciesRow);
                if (a > 0)
                {
                    report.AddParagraph(Resources.Reports.Sections.SpeciesStats.Paragraph1_1, a);
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.TreatmentSuggestion))
            {
                TreatmentSuggestion suggestion = stack.GetTreatmentSuggestion(speciesRow, stack.Parent.Individual.AgeColumn);
                if (suggestion != null)
                {
                    report.AddSectionTitle(string.Format(Resources.Reports.Sections.SpeciesStats.Header3, Service.Localize(stack.Parent.Individual.AgeColumn.Caption)));
                    report.AddParagraph(suggestion.GetNote());
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.Detailed))
            {
                report.AddSectionTitle(Resources.Reports.Sections.SpeciesStats.Header2);
                int no = 1;
                foreach (Data.CardRow cardRow in stack)
                {
                    Data.LogRow logRow = stack.Parent.Log.FindByCardIDSpcID(cardRow.ID, speciesRow.ID);

                    if (logRow == null) continue;

                    report.AddParagraph(no + ") " + Resources.Reports.Sections.SpeciesStats.Paragraph2,
                        cardRow,
                        cardRow.GetEffort(),
                        cardRow.GetGearType().GetDefaultUnitEffort().Unit,
                        speciesRow.KeyRecord.FullNameReport,
                        logRow.IsQuantityNull() ? Constants.Null : logRow.Quantity.ToString(),
                        logRow.IsMassNull() ? Constants.Null : logRow.Mass.ToString());

                    logRow.AddReport(report, CardReportLevel.Individuals | CardReportLevel.Stratified, string.Empty, string.Empty);
                    //string.Format(Wild.Resources.Reports.Individuals.LogCaption, string.Empty), 
                    //string.Format(Wild.Resources.Interface.Interface.StratifiedSample, string.Empty));

                    no++;
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.SurveySuggestion))
            {
                Report.Table crib = stack.GetSpeciesSurveySuggestionReport(report, speciesRow);

                if (crib != null)
                {
                    report.AddSectionTitle(Resources.Reports.Sections.SpeciesStats.Header4, Service.Localize(stack.Parent.Individual.AgeColumn.Caption));
                    report.AddParagraph(Resources.Reports.Sections.SpeciesStats.Paragraph3, UserSettings.RequiredClassSize, 10, 10, 10);
                    report.AddCribnote(crib);
                }
            }

            // TODO: Include models in the report;
        }

        public static void AddSpeciesSurveySuggestionReport(this CardStack stack, Report report)
        {
            bool num = report.UseTableNumeration;
            report.UseTableNumeration = true;

            report.AddParagraph(Resources.Reports.Sections.SpeciesStats.Paragraph3, UserSettings.RequiredClassSize, 10, 10, 10);

            foreach (Data.SpeciesRow speciesRow in stack.GetSpeciesCaught())
            {
                Report.Table crib = stack.GetSpeciesSurveySuggestionReport(report, speciesRow);

                if (crib == null) continue;

                //report.BreakPage(PageBreakOption.Odd);
                report.AddSectionTitle(speciesRow.KeyRecord.FullNameReport);
                report.AddCribnote(crib);
            }

            report.UseTableNumeration = num;
        }

        public static Report.Table GetSpeciesSurveySuggestionReport(this CardStack stack, Report report, Data.SpeciesRow speciesRow)
        {
            Report.Table crib = Wild.Service.GetStratifiedNote(string.Empty,
                stack.LengthMin(speciesRow), stack.LengthMax(speciesRow), UserSettings.SizeInterval,
                (l) =>
                {
                    int req = UserSettings.RequiredClassSize;
                    Interval strate = Interval.FromEndpointAndWidth(l, UserSettings.SizeInterval);
                    int aged = stack.Aged(speciesRow, strate);
                    int reged = stack.QuantityRegisteredNonAged(speciesRow, strate);
                    int mod = 0;
                    var bpm = stack.Parent.FindGrowthModel(speciesRow.Species);
                    if (bpm != null)
                    {
                        var sca = bpm.ExternalData;
                        if (sca != null) mod = sca.Data.Count((XY xy) => strate.LeftClosedContains(xy.Y));
                    }
                    int has = aged + reged + mod;

                    return (aged > 0 ? (aged.ToString()) : string.Empty) +
                        (reged > 0 ? ((aged > 0 ? "+" : string.Empty) + "(" + reged + ")") : string.Empty) +
                        (mod > 0 ? (((reged + aged) > 0 ? "+" : string.Empty) + "[" + mod + "]") : string.Empty) +
                        "<br><br>" +
                        (has >= req ?
                        (reged > 0 && mod < req ? string.Format(Resources.Reports.Sections.SpeciesStats.Tip1, reged) : Resources.Reports.Sections.SpeciesStats.Tip2) :
                        has > 0 ? string.Format(Resources.Reports.Sections.SpeciesStats.Tip3, req - has) : Resources.Reports.Sections.SpeciesStats.Tip4);
                });

            return crib;
        }

        public static void AddSpeciesStatsReport(this CardStack stack, Report report, SpeciesStatsLevel level, ExpressionVariant variant)
        {
            bool num = report.UseTableNumeration;
            report.UseTableNumeration = true;

            foreach (Data.SpeciesRow speciesRow in stack.GetSpeciesCaught())
            {
                report.AddChapterTitle(speciesRow.KeyRecord.FullNameReport);
                stack.AddSpeciesStatsReport(report, speciesRow, level, variant);
            }

            report.UseTableNumeration = num;
        }

        #endregion
    }

    public enum SpeciesStatsLevel
    {
        Totals = 1,
        Detailed = 2,
        TreatmentSuggestion = 4,
        SurveySuggestion = 8
    }
}
