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
        public static void AddCommon(this CardStack stack, Report report, TaxonomicIndex.TaxonRow speciesRow)
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



        public static Report.Table GetSampleSizeTable(
            CardStack[] stacks,
            Func<CardStack, SampleSizeDescriptor> f1,
            Func<CardStack, SampleSizeDescriptor[]> f2,
            string ageNotice)
        {
            Report.Table table = new Report.Table() { ZeroToLongDash = true };

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
            table.AddHeaderCell("Age read" + (string.IsNullOrWhiteSpace(ageNotice) ? "" : table.AddNotice(ageNotice).Holder));
            table.AddHeaderCell("Sex defined");
            table.AddHeaderCell("Maturity defined");

            table.AddHeaderCell("Total mass");
            table.AddHeaderCell("Stratified samples");
            table.AddHeaderCell("Individual samples");

            table.EndRow();
            table.EndHeader();

            //foreach (SampleSizeDescriptor descriptor1 in f1.Invoke())
            foreach (CardStack stack in stacks)
            {
                SampleSizeDescriptor descriptor1 = f1.Invoke(stack);

                if (descriptor1.Quantity == 0) continue;

                // Set totals row
                table.StartRow();
                table.AddCell(descriptor1.Name, ReportCellClass.Bold, 2, CellSpan.Columns);
                table.AddCellRight(descriptor1.Quantity, true);
                table.AddCellRight(descriptor1.QuantityStratified, true);
                table.AddCellRight(descriptor1.QuantityIndividual, true);
                table.AddCellRight(descriptor1.Measured, true);
                table.AddCellRight(descriptor1.Weighted, true);
                table.AddCellRight(descriptor1.Tallied, true);
                table.AddCellRight(descriptor1.Aged, true);
                table.AddCellRight(descriptor1.Sexed, true);
                table.AddCellRight(descriptor1.Matured, true);

                table.AddCellRight(descriptor1.Mass, true);
                table.AddCellRight(descriptor1.MassStratified, true);
                table.AddCellRight(descriptor1.MassIndividual, true);
                table.EndRow();

                foreach (SampleSizeDescriptor descriptor2 in f2.Invoke(stack))
                {
                    if (descriptor2.Quantity == 0) continue;

                    // Set class row
                    table.StartRow();
                    table.AddCell(string.Empty);
                    table.AddCell(descriptor2.Name);
                    table.AddCellRight(descriptor2.Quantity);
                    table.AddCellRight(descriptor2.QuantityStratified);
                    table.AddCellRight(descriptor2.QuantityIndividual);
                    table.AddCellRight(descriptor2.Measured);
                    table.AddCellRight(descriptor2.Weighted);
                    table.AddCellRight(descriptor2.Tallied);
                    table.AddCellRight(descriptor2.Aged);
                    table.AddCellRight(descriptor2.Sexed);
                    table.AddCellRight(descriptor2.Matured);

                    table.AddCellRight(descriptor2.Mass);
                    table.AddCellRight(descriptor2.MassStratified);
                    table.AddCellRight(descriptor2.MassIndividual);
                    table.EndRow();
                }
            }

            return table;
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

        //    foreach (Data.DefinitionRow speciesRow in stack.GetSpeciesCaught())
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

        public static Report.Table GetSampleSizeTable(this CardStack stack, FishSamplerType samplerType)
        {
            return GetSampleSizeTable(stack.GetClassedStacks(samplerType),
                (s) =>
                {
                    return s.GetDescriptor();
                },
                (s) =>
                {
                    List<SampleSizeDescriptor> result = new List<SampleSizeDescriptor>();
                    foreach (TaxonomicIndex.TaxonRow speciesRow in s.GetSpecies())
                    {
                        result.Add(s.GetDescriptor(speciesRow));
                    }
                    return result.ToArray();
                },
                string.Empty);
        }

        public static void AddGearStatsReport(this CardStack stack, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            stack.AddEffortsSection(report, samplerType, ue);

            #region Sample Size

            report.AddSectionTitle(Resources.Reports.Sections.SampleSize.Subtitle);
            report.AddTable(GetSampleSizeTable(stack, samplerType), "airy");

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
                        logRow.DefinitionRow.KeyRecord.FullNameReport,
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
            return stack.GetStratifiedCribnote(stack.GetSpecies(15));
        }

        public static Report GetStratifiedCribnote(this CardStack stack, TaxonomicIndex.TaxonRow[] speciesRows)
        {
            Report report = new Report(Resources.Reports.Header.StratifiedCribnote, "strates.css");

            foreach (TaxonomicIndex.TaxonRow speciesRow in speciesRows)
            {
                // TODO: Skip if no samples?
                Report.Table cribnote = Wild.Service.GetStratifiedNote(string.Format(Wild.Resources.Reports.Header.StratifiedSample, speciesRow.FullNameReport),
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

        //public static Report GetTreatmentNote(this CardStack stack, Data.DefinitionRow[] speciesRows, System.Data.DataColumn column)
        //{
        //    Report report = new Report(string.Format(Resources.Reports.Sections.TreatmentSuggestion.Title, Service.Localize(column.Caption)));

        //    report.AddParagraph(Resources.Reports.Sections.TreatmentSuggestion.Par1, Service.Localize(column.Caption));

        //    foreach (Data.DefinitionRow speciesRow in speciesRows)
        //    {
        //        stack.GetTreatmentNote(report, speciesRow, column);
        //    }

        //    report.End();

        //    return report;
        //}

        //public static void GetTreatmentNote(this CardStack stack, Report report, Data.DefinitionRow speciesRow, System.Data.DataColumn column)
        //{
        //    TreatmentSuggestion suggestion = stack.GetTreatmentSuggestion(speciesRow, column);
        //    if (suggestion == null) return;
        //    report.AddSubtitle(speciesRow.GetReportFullPresentation(),
        //    report.AddParagraph(suggestion.GetNote(),
        //}

        //#endregion

        public static Report.Table GetSampleSizeTable(this CardStack stack, TaxonomicIndex.TaxonRow speciesRow, string ageNotice)
        {
            List<CardStack> gearStacks = new List<CardStack>();

            foreach (FishSamplerType samplerType in stack.GetSamplerTypes())
            {
                gearStacks.Add(stack.GetStack(samplerType));
            }

            return GetSampleSizeTable(gearStacks.ToArray(),
                (s) =>
                {
                    var d = s.GetDescriptor(speciesRow);
                    d.Name = s.Name;
                    return d;
                },
                (s) =>
                {
                    List<SampleSizeDescriptor> result = new List<SampleSizeDescriptor>();                    
                    foreach (CardStack cs in s.GetClassedStacks())
                    {
                        var d = cs.GetDescriptor(speciesRow);
                        d.Name = cs.Name;
                        result.Add(d);
                    }                    
                    return result.ToArray();
                },
                ageNotice);   
        }

        public static void AddSpeciesStatsReport(this CardStack stack, Report report, TaxonomicIndex.TaxonRow speciesRow, SpeciesStatsLevel level, ExpressionVariant variant)
        {
            if (level.HasFlag(SpeciesStatsLevel.Totals))
            {
                report.AddSectionTitle(Resources.Reports.Sections.SpeciesStats.Header1);
                TreatmentSuggestion suggestion = stack.GetTreatmentSuggestion(speciesRow, stack.Parent.Individual.AgeColumn);
                report.AddTable(GetSampleSizeTable(
                    stack,
                    speciesRow,
                    level.HasFlag(SpeciesStatsLevel.TreatmentSuggestion) ? (suggestion == null ? string.Empty : suggestion.GetNote()) : string.Empty),
                    "airy");
                
                int a = stack.Aged(speciesRow);
                if (a > 0)
                {
                    report.AddParagraph(Resources.Reports.Sections.SpeciesStats.Paragraph1_1, a);
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.Detailed))
            {
                report.AddSectionTitle(Resources.Reports.Sections.SpeciesStats.Header2);
                int no = 1;
                foreach (Data.CardRow cardRow in stack)
                {
                    Data.LogRow logRow = stack.Parent.Log.FindByCardIDDefID(cardRow.ID, speciesRow.ID);

                    if (logRow == null) continue;

                    report.AddParagraph(no + ") " + Resources.Reports.Sections.SpeciesStats.Paragraph2,
                        cardRow,
                        cardRow.GetEffort(),
                        cardRow.GetGearType().GetDefaultUnitEffort().Unit,
                        speciesRow.FullNameReport,
                        logRow.IsQuantityNull() ? Constants.Null : logRow.Quantity.ToString(),
                        logRow.IsMassNull() ? Constants.Null : logRow.Mass.ToString());

                    logRow.AddReport(report, CardReportLevel.Individuals | CardReportLevel.Stratified, string.Empty, string.Empty);

                    no++;
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.SurveySuggestion))
            {
                Report.Table crib = stack.GetSpeciesSurveySuggestionReport(report, speciesRow);

                if (crib != null)
                {
                    crib.MarkNotices = false;
                    crib.AddNotice(Resources.Reports.Sections.SpeciesStats.SurveySuggestionLegend1);
                    crib.AddNotice(Resources.Reports.Sections.SpeciesStats.SurveySuggestionLegend2);
                    crib.AddNotice(Resources.Reports.Sections.SpeciesStats.SurveySuggestionLegend3);
                    report.AddSectionTitle(Resources.Reports.Sections.SpeciesStats.Header4, Service.Localize(stack.Parent.Individual.AgeColumn.Caption));
                    report.AddParagraph(Resources.Reports.Sections.SpeciesStats.Paragraph3, UserSettings.RequiredClassSize);
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

            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                Report.Table crib = stack.GetSpeciesSurveySuggestionReport(report, speciesRow);

                if (crib == null) continue;

                //report.BreakPage(PageBreakOption.Odd);
                report.AddSectionTitle(speciesRow.FullNameReport);
                report.AddCribnote(crib);
            }

            report.UseTableNumeration = num;
        }

        public static Report.Table GetSpeciesSurveySuggestionReport(this CardStack stack, Report report, TaxonomicIndex.TaxonRow speciesRow)
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
                    var bpm = stack.Parent.FindGrowthModel(speciesRow.Name);
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
            foreach (TaxonomicIndex.TaxonRow speciesRow in stack.GetSpecies())
            {
                report.AddChapterTitle(speciesRow.FullNameReport);
                stack.AddSpeciesStatsReport(report, speciesRow, level, variant);
            }
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
