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
            Artefact[] artefacts = stack.GetArtefacts(false);

            string warning = string.Empty;

            if (artefacts.Length > 0)
            {
                warning = Resources.Artefact.ArtefactsFound + Environment.NewLine;

                foreach (Artefact artefact in artefacts)
                {
                    // If report is about species - skip artefacts of other species
                    if (speciesRow != null && artefact is SpeciesArtefact && ((SpeciesArtefact)artefact).SpeciesRow != speciesRow)
                        continue;

                    warning += artefact.ToString() + Environment.NewLine;
                }
            }

            stack.AddCommon(report, stack.GetSamplersList().Merge(), warning);
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

        public static void AddGearStatsReport(this CardStack stack, Report report)
        {
            report.UseTableNumeration = true;
            //stack.AddSampleSizeReport(report);

            foreach (FishSamplerType samplerType in stack.GetSamplerTypes())
            {
                report.AddChapterTitle(samplerType.ToDisplay());
                stack.AddGearStatsReport(report, samplerType, samplerType.GetDefaultUnitEffort());
            }
        }

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

        //        table1.AddCell(speciesRow.ToShortHTML());
        //        table1.AddCellRight(q > 0 ? q.ToString() : Constants.Null);
        //        table1.AddCellRight(l > 0 ? l.ToString() : Constants.Null);
        //        table1.AddCellRight(w > 0 ? w.ToString() : Constants.Null);
        //        table1.AddCellRight(a > 0 ? a.ToString() : Constants.Null);
        //        table1.AddCellRight(m, Mayfly.Service.Mask(3));

        //        table1.EndRow();
        //    }

        //    table1.StartRow();
        //    table1.AddCell(Mayfly.Resources.Interface.Total, ReportCellClass.Bold);
        //    table1.AddCellRight(Q > 0 ? Q.ToString() : Constants.Null, true);
        //    table1.AddCellRight(L > 0 ? L.ToString() : Constants.Null, true);
        //    table1.AddCellRight(W > 0 ? W.ToString() : Constants.Null, true);
        //    table1.AddCellRight(A > 0 ? A.ToString() : Constants.Null, true);
        //    table1.AddCellRight(M, Mayfly.Service.Mask(3), true);
        //    table1.EndRow();

        //    report.AddTable(table1);
        //}

        public static void AddGearStatsReport(this CardStack stack, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            stack.AddEffortsSection(report, samplerType, ue);

            #region Sample Size

            report.AddSectionTitle(Resources.Reports.Section.SampleSize.Subtitle);

            CardStack samplerData = stack.GetStack(samplerType);

            string[] classes = stack.Classes(samplerType);

            //report.AddParagraph(
            //    string.Format(Resources.Reports.GearStats.Paragraph2,
            //    samplerType.ToDisplay(), classes.Length,
            //    samplerData.SamplerClasses(samplerType)[0],
            //    samplerData.SamplerClasses(samplerType).Last(), ue.UnitDescription)
            //    );

            Report.Table table1 = new Report.Table(Resources.Reports.Section.SampleSize.Table_1);

            table1.StartHeader();

            table1.StartRow();
            table1.AddHeaderCell(Fish.Resources.Reports.Card.Mesh, .05, 2, CellSpan.Rows);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .2, 2, CellSpan.Rows);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Quantity, 4);
            table1.AddHeaderCell(Resources.Reports.Common.MassUnits, 2, CellSpan.Rows);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell(Mayfly.Resources.Interface.Total);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Length);
            table1.AddHeaderCell(Fish.Resources.Reports.Caption.Mass);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Age);
            table1.EndRow();

            table1.EndHeader();

            foreach (string mesh in classes)
            {
                CardStack meshData = stack.GetStack(samplerType, mesh);
                bool totalMeshNeeded = true;
                double Q = 0;
                double L = 0;
                double W = 0;
                double A = 0;
                double M = 0;

                foreach (Data.SpeciesRow speciesRow in meshData.GetSpeciesCaught())
                {
                    table1.StartRow();

                    if (totalMeshNeeded)
                    {
                        table1.AddCellValue(meshData.Name, meshData.SpeciesWealth + 1);
                        totalMeshNeeded = false;
                    }

                    double q = meshData.Quantity(speciesRow);
                    double l = meshData.Measured(speciesRow);
                    double w = meshData.Weighted(speciesRow);
                    double a = meshData.Aged(speciesRow);
                    double m = meshData.Mass(speciesRow);
                    Q += q;
                    L += l;
                    W += w;
                    A += a;
                    M += m;

                    table1.AddCell(speciesRow.ToShortHTML());
                    table1.AddCellRight(q > 0 ? q.ToString() : Constants.Null);
                    table1.AddCellRight(l > 0 ? l.ToString() : Constants.Null);
                    table1.AddCellRight(w > 0 ? w.ToString() : Constants.Null);
                    table1.AddCellRight(a > 0 ? a.ToString() : Constants.Null);
                    table1.AddCellRight(m.ToString(Mayfly.Service.Mask(3)));

                    table1.EndRow();
                }

                if (Q > 0)
                {
                    table1.StartRow();
                    table1.AddCell(Mayfly.Resources.Interface.Total, ReportCellClass.Bold);
                    table1.AddCellRight(Q > 0 ? Q.ToString() : Constants.Null, true);
                    table1.AddCellRight(L > 0 ? L.ToString() : Constants.Null, true);
                    table1.AddCellRight(W > 0 ? W.ToString() : Constants.Null, true);
                    table1.AddCellRight(A > 0 ? A.ToString() : Constants.Null, true);
                    table1.AddCellRight(M.ToString(Mayfly.Service.Mask(3)), true);
                    table1.EndRow();
                }
            }

            if (classes.Length == 0 || classes.Length > 1)
            {
                bool totalSamplerNeeded = true;
                double Q = 0;
                double L = 0;
                double W = 0;
                double A = 0;
                double M = 0;

                foreach (Data.SpeciesRow speciesRow in samplerData.GetSpeciesCaught())
                {
                    table1.StartRow();

                    if (totalSamplerNeeded)
                    {
                        table1.AddCellValue(Mayfly.Resources.Interface.Total, samplerData.SpeciesWealth + 1);
                        totalSamplerNeeded = false;
                    }

                    double q = samplerData.Quantity(speciesRow);
                    double l = samplerData.Measured(speciesRow);
                    double w = samplerData.Weighted(speciesRow);
                    double a = samplerData.Aged(speciesRow);
                    double m = samplerData.Mass(speciesRow);
                    Q += q;
                    L += l;
                    W += w;
                    A += a;
                    M += m;

                    table1.AddCell(speciesRow.ToShortHTML());
                    table1.AddCellRight(q > 0 ? q.ToString() : Constants.Null);
                    table1.AddCellRight(l > 0 ? l.ToString() : Constants.Null);
                    table1.AddCellRight(w > 0 ? w.ToString() : Constants.Null);
                    table1.AddCellRight(a > 0 ? a.ToString() : Constants.Null);
                    table1.AddCellRight(m, Mayfly.Service.Mask(3));

                    table1.EndRow();
                }

                table1.StartRow();
                table1.AddCell(Mayfly.Resources.Interface.Total, ReportCellClass.Bold);
                table1.AddCellRight(Q > 0 ? Q.ToString() : Constants.Null, true);
                table1.AddCellRight(L > 0 ? L.ToString() : Constants.Null, true);
                table1.AddCellRight(W > 0 ? W.ToString() : Constants.Null, true);
                table1.AddCellRight(A > 0 ? A.ToString() : Constants.Null, true);
                table1.AddCellRight(M, Mayfly.Service.Mask(3), true);
                table1.EndRow();
            }

            report.AddTable(table1);

            #endregion

            #region Sample Detailed

            report.AddSectionTitle(Resources.Reports.Section.SampleDetailed.Subtitle);
            int no = 1;
            foreach (Data.CardRow cardRow in stack)
            {
                if (cardRow.GetGearType() != samplerType) continue;

                report.AddParagraph(no + ") " + Resources.Reports.Section.SampleDetailed.Paragraph_1 + (cardRow.Quantity == 0 ? " " + Resources.Reports.Section.SampleDetailed.Paragraph_1_1 : string.Empty),
                    cardRow, 
                    cardRow.GetEffort(), 
                    cardRow.GetGearType().GetDefaultUnitEffort().Unit);

                // TODO: Add gillnet parameters, time of setting etc.


                foreach (Data.LogRow logRow in cardRow.GetLogRows())
                {
                    bool justregistered = logRow.DetailedQuantity== 0;

                    report.AddParagraph(Resources.Reports.Section.SampleDetailed.Paragraph_2 + (justregistered ? " " + Resources.Reports.Section.SampleDetailed.Paragraph_2_1 : string.Empty),
                        logRow.SpeciesRow.ToHTML(),
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

        public static void AddEffortsSection(this CardStack stack, Report report, FishSamplerType samplerType, UnitEffort ue)
        {
            CardStack samplerData = stack.GetStack(samplerType, ue.Variant);
            double totalE = samplerData.GetEffort(samplerType, ue.Variant);
            string[] classes = stack.Classes(samplerType);

            report.AddSectionTitle(Resources.Reports.Section.Efforts.Subtitle);
            //report.AddSubtitle3(Resources.Reports.GearStats.HeaderEfforts);

            if (classes.Length > 1)
            {
                report.AddParagraph(
                    string.Format(Resources.Reports.Section.Efforts.Paragraph_1,
                    samplerType.ToDisplay(), classes.Length,
                    samplerData.SamplerClasses(samplerType)[0],
                    samplerData.SamplerClasses(samplerType).Last(), ue.UnitDescription)
                    );

                Report.Table table1 = new Report.Table(Resources.Reports.Section.Efforts.Table_1, samplerType.ToDisplay());

                table1.StartRow();
                table1.AddHeaderCell(Resources.Reports.Column.GearClass, .25);
                table1.AddHeaderCell(Resources.Reports.Column.Ops);
                table1.AddHeaderCell(string.Format(Resources.Reports.Column.Efforts, ue.Unit));
                table1.AddHeaderCell(Resources.Reports.Column.Fraction);
                table1.EndRow();

                foreach (string mesh in classes)
                {
                    CardStack meshData = samplerData.GetStack(samplerType, mesh);

                    table1.StartRow();
                    table1.AddCellValue(meshData.Name);
                    double e = meshData.GetEffort(samplerType, ue.Variant);
                    table1.AddCellRight(meshData.Count);
                    table1.AddCellRight(e, "N3");
                    table1.AddCellRight(e / totalE, "P1");
                    table1.EndRow();
                }

                table1.StartRow();
                table1.AddCell(Mayfly.Resources.Interface.Total);
                table1.AddCellRight(samplerData.Count);
                table1.AddCellRight(totalE, "N3");
                table1.AddCellRight(1, "P1");
                table1.EndRow();

                report.AddTable(table1);
            }
            else
            {
                report.AddParagraph(Resources.Reports.Section.Efforts.Paragraph_2, samplerType.ToDisplay(), ue.UnitDescription, totalE, ue.Unit);
            }
        }
        
        #endregion

        #region Species stats

        public static void AddSpcCpue(this CardStack stack, Report report, Data.SpeciesRow speciesRow,
            FishSamplerType samplerType, UnitEffort ue)
        {
            CardStack samplerData = stack.GetStack(samplerType, ue.Variant);

            if (samplerData.Count == 0) return;
            if (samplerData.Quantity(speciesRow) == 0) return;

            string[] classes = stack.Classes(samplerType);

            Report.Table table1 = new Report.Table(Resources.Reports.Title.CpueSpc, speciesRow.ToHTML(), samplerType.ToDisplay());

            table1.StartRow();
            table1.AddHeaderCell(Fish.Resources.Reports.Card.Mesh, .25, 2);
            table1.AddHeaderCell(string.Format(Resources.Reports.Column.Efforts, ue.Unit), 2, CellSpan.Rows);
            table1.AddHeaderCell(Resources.Reports.Column.Catch, 2);
            table1.AddHeaderCell(Resources.Reports.Column.CpueAccr, 2);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Common.Ind);
            table1.AddHeaderCell(Resources.Reports.Common.Kg);
            table1.AddHeaderCell(Resources.Reports.Common.Ind + " / " + ue.Unit);
            table1.AddHeaderCell(Resources.Reports.Common.Kg + " / " + ue.Unit);
            table1.EndRow();

            double E = 0.0;
            double Q = 0.0;
            double W = 0.0;
            int Index = 0;
            double QpE = 0.0;
            double WpE = 0.0;


            if (classes.Length > 1)
            {
                foreach (string mesh in classes)
                {
                    CardStack meshData = stack.GetStack(samplerType, mesh);

                    double q = meshData.Quantity(speciesRow);

                    if (q == 0) continue;

                    double e = meshData.GetEffort(samplerType);
                    double w = meshData.Mass(speciesRow);

                    E += e;
                    Q += q;
                    W += w;
                    QpE += (q / e);
                    WpE += (w / e);

                    table1.StartRow();
                    table1.AddCellValue(meshData.Name);
                    table1.AddCellRight(e, Mayfly.Service.Mask(3));
                    table1.AddCellRight(q);
                    table1.AddCellRight(w, Mayfly.Service.Mask(3));
                    table1.AddCellRight(q / e, Mayfly.Service.Mask(3));
                    table1.AddCellRight(w / e, Mayfly.Service.Mask(3));
                    table1.EndRow();

                    Index++;
                }

                table1.StartFooter();

                table1.StartRow();
                table1.AddCell(Mayfly.Resources.Interface.Total);
                table1.AddCellRight(E, Mayfly.Service.Mask(3));
                table1.AddCellRight(Q);
                table1.AddCellRight(W.ToString(Mayfly.Service.Mask(3)));
                table1.AddCellRight(Constants.Null);
                table1.AddCellRight(Constants.Null);
                table1.EndRow();

                table1.StartRow();
                table1.AddCell(Mayfly.Resources.Interface.Mean);
                //table1.AddCell(Resources.Reports.Reports.CpueMean);
                //table1.AddCellRight(Constants.Null);
                //table1.AddCellRight(Constants.Null);
                //table1.AddCellRight(Constants.Null);
                table1.AddCellRight(E / (double)classes.Length, Mayfly.Service.Mask(3));
                table1.AddCellRight(Q / (double)classes.Length, Mayfly.Service.Mask(1));
                table1.AddCellRight(W / (double)classes.Length, Mayfly.Service.Mask(3));
                table1.AddCellRight(QpE / (double)classes.Length, Mayfly.Service.Mask(3));
                table1.AddCellRight(WpE / (double)classes.Length, Mayfly.Service.Mask(3));
                table1.EndRow();

                table1.EndFooter();
            }
            else
            {
                double q = samplerData.Quantity(speciesRow);

                double e = samplerData.GetEffort(samplerType);
                double w = samplerData.Mass(speciesRow);

                table1.StartRow();
                table1.AddCell(Resources.Interface.Interface.AllDataCombined);
                table1.AddCellRight(e, Mayfly.Service.Mask(3));
                table1.AddCellRight(q);
                table1.AddCellRight(w, Mayfly.Service.Mask(3));
                table1.AddCellRight(q / e, Mayfly.Service.Mask(3));
                table1.AddCellRight(w / e, Mayfly.Service.Mask(3));
                table1.EndRow();
            }

            report.AddTable(table1);
        }

        #region Stratified tables

        public static Report GetStratifiedCribnote(this CardStack stack)
        {
            return stack.GetStratifiedCribnote(stack.GetSpeciesCaught(15));
        }

        public static Report GetStratifiedCribnote(this CardStack stack, Data.SpeciesRow[] speciesRows)
        {
            Report report = new Report(Resources.Reports.Common.StratifiedCribnote, "strates.css");

            foreach (Data.SpeciesRow speciesRow in speciesRows)
            {
                // TODO: Skip if no samples?
                Report.Table cribnote = Wild.Service.GetStratifiedNote(string.Format(Wild.Resources.Reports.Header.StratifiedSample, speciesRow.ToHTML()),
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
        //    Report report = new Report(string.Format(Resources.Reports.TreatmentSuggestion.Title, Service.Localize(column.Caption)));

        //    report.AddParagraph(Resources.Reports.TreatmentSuggestion.Par1, Service.Localize(column.Caption));

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

        public static void AddSpeciesStatsReport(this CardStack stack, Report report, SpeciesStatsLevel level)
        {
            bool num = report.UseTableNumeration;
            report.UseTableNumeration = true;

            foreach (Data.SpeciesRow speciesRow in stack.GetSpeciesCaught())
            {
                report.AddChapterTitle(speciesRow.ToHTML());
                stack.AddSpeciesStatsReport(report, speciesRow, level);
            }

            report.UseTableNumeration = num;
        }

        public static void AddSpeciesStatsReport(this CardStack stack, Report report, Data.SpeciesRow speciesRow, SpeciesStatsLevel level)
        {
            if (level.HasFlag(SpeciesStatsLevel.Totals))
            {
                report.AddSectionTitle(Resources.Reports.SpeciesStats.Header1);

                report.AddParagraph(Resources.Reports.SpeciesStats.Paragraph1,
                    speciesRow.ToHTML(),
                    stack.Quantity(speciesRow),
                    stack.QuantitySampled(speciesRow),
                    stack.Measured(speciesRow) + stack.QuantityStratified(speciesRow),
                    stack.Weighted(speciesRow),
                    stack.Registered(speciesRow)
                    );

                int a = stack.Aged(speciesRow);

                if (a > 0)
                {
                    report.AddParagraph(Resources.Reports.SpeciesStats.Paragraph1_1, a);
                }

                foreach (FishSamplerType samplerType in stack.GetSamplerTypes())
                {
                    stack.AddSpcCpue(report, speciesRow, samplerType, samplerType.GetDefaultUnitEffort());
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.Detailed))
            {
                report.AddSectionTitle(Resources.Reports.SpeciesStats.Header2);
                int no = 1;
                foreach (Data.CardRow cardRow in stack)
                {
                    Data.LogRow logRow = stack.Parent.Log.FindByCardIDSpcID(cardRow.ID, speciesRow.ID);

                    if (logRow == null) continue;

                    report.AddParagraph(no + ") " + Resources.Reports.SpeciesStats.Paragraph2,
                        cardRow,
                        cardRow.GetEffort(),
                        cardRow.GetGearType().GetDefaultUnitEffort().Unit,
                        speciesRow.ToHTML(),
                        logRow.IsQuantityNull() ? Constants.Null : logRow.Quantity.ToString(),
                        logRow.IsMassNull() ? Constants.Null : logRow.Mass.ToString());

                    logRow.AddReport(report, CardReportLevel.Individuals | CardReportLevel.Stratified, string.Empty, string.Empty);
                    //string.Format(Wild.Resources.Reports.Individuals.LogCaption, string.Empty), 
                    //string.Format(Wild.Resources.Interface.Interface.StratifiedSample, string.Empty));

                    no++;
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.TreatmentSuggestion))
            {
                TreatmentSuggestion suggestion = stack.GetTreatmentSuggestion(speciesRow, stack.Parent.Individual.AgeColumn);
                if (suggestion != null)
                {
                    report.AddSectionTitle(string.Format(Resources.Reports.SpeciesStats.Header3, Service.Localize(stack.Parent.Individual.AgeColumn.Caption)));
                    report.AddParagraph(suggestion.GetNote());
                }
            }

            if (level.HasFlag(SpeciesStatsLevel.SurveySuggestion))
            {
                Report.Table crib = stack.GetSpeciesSurveySuggestionReport(report, speciesRow);

                if (crib != null)
                {
                    report.AddSectionTitle(Resources.Reports.SpeciesStats.Header4, Service.Localize(stack.Parent.Individual.AgeColumn.Caption));
                    report.AddParagraph(Resources.Reports.SpeciesStats.Paragraph3, UserSettings.RequiredClassSize, 10, 10, 10);
                    report.AddCribnote(crib);
                }
            }

            // TODO: Include models in the report;
        }

        public static void AddSpeciesSurveySuggestionReport(this CardStack stack, Report report)
        {
            bool num = report.UseTableNumeration;
            report.UseTableNumeration = true;

            report.AddParagraph(Resources.Reports.SpeciesStats.Paragraph3, UserSettings.RequiredClassSize, 10, 10, 10);

            foreach (Data.SpeciesRow speciesRow in stack.GetSpeciesCaught())
            {
                Report.Table crib = stack.GetSpeciesSurveySuggestionReport(report, speciesRow);

                if (crib == null) continue;

                //report.BreakPage(PageBreakOption.Odd);
                report.AddSectionTitle(speciesRow.ToHTML());
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
                    Mathematics.Charts.Scatterplot sca = stack.Parent.GrowthModels.GetExternalScatterplot(speciesRow.Species);
                    if (sca != null) mod = sca.Data.Count((XY xy) => strate.LeftClosedContains(xy.Y));
                    int has = aged + reged + mod;

                    return (aged > 0 ? (aged.ToString()) : string.Empty) +
                        (reged > 0 ? ((aged > 0 ? "+" : string.Empty) + "(" + reged + ")") : string.Empty) +
                        (mod > 0 ? (((reged + aged) > 0 ? "+" : string.Empty) + "[" + mod + "]") : string.Empty) +
                        "<br><br>" +
                        (has >= req ?
                        (reged > 0 && mod < req ? string.Format(Resources.Reports.SpeciesStats.Tip1, reged) : Resources.Reports.SpeciesStats.Tip2) :
                        has > 0 ? string.Format(Resources.Reports.SpeciesStats.Tip3, req - has) : Resources.Reports.SpeciesStats.Tip4);
                });

            return crib;
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
