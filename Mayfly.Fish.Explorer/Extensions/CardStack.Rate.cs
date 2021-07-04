using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Wild;
using Mayfly.Mathematics.Statistics;
using Mayfly.Fish;
using Mayfly.Extensions;
using Meta.Numerics;

namespace Mayfly.Fish.Explorer
{
    public static partial class CardStackExtensions
    {
        public static double GetRate(this CardStack stack)
        {
            return stack.GetGatheringRate() * stack.GetTreatmentRate();
        }



        public static double GetGatheringRate(this CardStack stack)
        {
            double G = 0;
            int n = 0;

            foreach (FishSamplerType type in stack.GetSamplerTypes())
            {
                double g = stack.GetGatheringRate(type);
                G += g;
                n++;
            }

            return (G / (double)n);
        }

        public static double GetGatheringRate(this CardStack stack, FishSamplerType samplerType)
        {
            double w = stack.GetWidth(samplerType);
            double f = stack.GetFrequency(samplerType);

            return ((w + f) / 2.0);
        }

        public static double GetWidth(this CardStack stack, FishSamplerType samplerType)
        {
            if (!samplerType.IsMesh()) return 1;
            if (!samplerType.IsPassive()) return 1;

            int[] gearClasses = stack.Classes(samplerType);
            return gearClasses.Length > 0 ?
            (1d - (double)gearClasses.Min() / (double)gearClasses.Max()) / .9 :
            1d;
        }

        public static double GetFrequency(this CardStack stack, FishSamplerType samplerType)
        {
            if (!samplerType.IsMesh()) return 1;
            if (!samplerType.IsPassive()) return 1;

            double pace = stack.GetPace(samplerType);

            return double.IsNaN(pace) ? 1 : 1 - 1 / (1 + Math.Pow(10, 4 - 0.5 * pace));
        }

        public static double GetPace(this CardStack stack, FishSamplerType samplerType)
        {
            int[] gearClasses = stack.Classes(samplerType);

            List<int> steps = new System.Collections.Generic.List<int>();
            for (int i = 1; i < gearClasses.Length; i++)
            {
                steps.Add(gearClasses[i] - gearClasses[i - 1]);
            }

            return (double)steps.Sum() / (double)steps.Count;
        }

        public static double GetTreatmentRate(this CardStack stack)
        {
            double T = 0;
            int n = 0;

            foreach (Data.SpeciesRow spcRow in stack.GetSpecies())
            {
                double t = stack.GetTreatmentRate(spcRow);
                T += t;
                n++; 
            }

            return (T / (double)n);
        }

        public static double GetTreatmentRate(this CardStack stack, Data.SpeciesRow spcRow)
        {
            double i = stack.GetIndiscriminance(spcRow);
            double w = stack.GetMassModelQuality(spcRow);
            double g = stack.GetGrowthModelQuality(spcRow);

            return (i * ((w > 0) ? w : 1) * ((g > 0) ? g : 1));
            //return (i + w + g) / 3.0;
        }

        public static double GetIndiscriminance(this CardStack stack, Data.SpeciesRow spcRow)
        {
            return ((double)stack.MeasuredAnyhow(spcRow) / (double)stack.QuantitySampled(spcRow));
        }

        public static double GetMassModelQuality(this CardStack stack, Data.SpeciesRow spcRow)
        {
            //Mathematics.Statistics.Regression regression = stack.Parent.WeightModels.GetInternalRegression(spcRow.Species);

            //if (regression == null) {
            //    return 0;
            //} else {
            //    double r = 1 - ((double)regression.GetRunouts().Count / (double)regression.Data.Count);
            //    double r2 = regression.Determination;
            //    return Math.Sqrt(r * r2);
            //}

            return 1;
        }

        public static double GetGrowthModelQuality(this CardStack stack, Data.SpeciesRow spcRow)
        {
            //Mathematics.Statistics.Regression regression = stack.Parent.GrowthModels.GetInternalRegression(spcRow.Species);

            //if (regression == null) {
            //    return 0;
            //} else {
            //    double r = 1 - ((double)regression.GetRunouts().Count / (double)regression.Data.Count);
            //    double r2 = regression.Determination;
            //    return Math.Sqrt(r * r2);
            //}

            return 1;
        }



        public static Report GetRateReport(this CardStack stack)
        {
            Report report = new Report(Resources.Reports.Rate.Title);
            stack.AddCommon(report);
            report.UseTableNumeration = true;

            report.AddParagraph(Resources.Reports.Rate.Paragraph1_1);
            report.AddEquation(@"{R} = \bar{G} \times \bar{T}");

            report.AddSubtitle(Resources.Reports.Rate.TitleG);
            stack.AddGathering(report);

            List<string> perfectlySampled = new List<string>();
            report.AddSubtitle(Resources.Reports.Rate.TitleT);
            stack.AddTreatment(report);

            report.AddParagraph(Resources.Reports.Rate.Paragraph1_2);

            report.AddEquation("{R} = {" + stack.GetGatheringRate().ToString(Mayfly.Service.Mask(3)) +
                "} \\times {" + stack.GetTreatmentRate().ToString(Mayfly.Service.Mask(3)) + "} = " +
                stack.GetRate().ToString(Mayfly.Service.Mask(3)));
            report.EndBranded();

            return report;
        }

        public static void AddGathering(this CardStack stack, Report report)
        {
            string fracFormat = Mayfly.Service.Mask(3);

            report.AddParagraph(Resources.Reports.Rate.Paragraph2_1);
            report.AddEquation(@"{G} = \frac{{W} + {F}}{2}");

            report.AddParagraph(Resources.Reports.Rate.Paragraph2_2);
            report.AddEquation(@"{W} = \frac{1 - \frac{\text{" + Resources.Reports.Rate.EquationMesh +
                @"}_{min}}{\text{" + Resources.Reports.Rate.EquationMesh + "}_{max}}}{" + 0.9 + "}");

            report.AddParagraph(Resources.Reports.Rate.Paragraph2_3);
            report.AddEquation(@"{F} = 1 - \frac{1}{1 + {10}^{{4} - " + 0.5 + @"\times{P}}}");

            report.AddParagraph(Resources.Reports.Rate.Paragraph2_4);

            Report.Table table1 = new Report.Table(Resources.Reports.Rate.Table1);

            table1.StartHeader();
            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_1, .2);
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_2);
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_3);
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_4);
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_5);
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_6);
            table1.AddHeaderCell(Resources.Reports.Rate.Column1_7);
            table1.EndRow();
            table1.EndHeader();

            foreach (FishSamplerType samplerType in stack.GetSamplerTypes())
            {

                table1.StartRow();
                table1.AddCell(samplerType.ToDisplay());

                if (samplerType.IsPassive())
                {
                    int[] classes = stack.Classes(samplerType);
                    double widthRatio = (double)classes.Min() / (double)classes.Max();
                    double pace = stack.GetPace(samplerType);
                    table1.AddCellValue(classes.Merge());
                    table1.AddCellRight(widthRatio, fracFormat);
                    table1.AddCellRight(stack.GetWidth(samplerType), fracFormat);
                    table1.AddCellRight(pace, fracFormat);
                    table1.AddCellRight(stack.GetFrequency(samplerType), fracFormat);
                }
                else
                {
                    table1.AddCellRight(Mayfly.Constants.Null);
                    table1.AddCellRight(Mayfly.Constants.Null);
                    table1.AddCellRight(Mayfly.Constants.Null);
                    table1.AddCellRight(Mayfly.Constants.Null);
                    table1.AddCellRight(Mayfly.Constants.Null);
                }

                table1.AddCellRight(stack.GetGatheringRate(samplerType), fracFormat);
                table1.EndRow();
            }

            table1.StartFooter();
            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Mean);
            table1.AddCell();
            table1.AddCell();
            table1.AddCell();
            table1.AddCell();
            table1.AddCell();
            table1.AddCellRight(stack.GetGatheringRate(), fracFormat);
            table1.EndRow();
            table1.EndFooter();

            report.AddTable(table1);
        }

        public static void AddTreatment(this CardStack stack, Report report)
        {
            string fracFormat = Mayfly.Service.Mask(3);

            report.AddParagraph(Resources.Reports.Rate.Paragraph3_1);
            report.AddEquation(@"{T} = {I} \times {w} \times {g}");

            report.AddParagraph(Resources.Reports.Rate.Paragraph3_2);
            report.AddEquation(@"{Q} = \sqrt{{r^2} \times {(1 - b)}} \text {" +
                Resources.Reports.Rate.EquationWhere + @" } {b} \text { " +
                Resources.Reports.Rate.EquationB + "}");

            Report.Table table1 = new Report.Table(Resources.Reports.Rate.Table2);

            table1.StartHeader();
            table1.StartRow();
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .2, 2);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_1, 2, CellSpan.Rows);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_2, 2);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_3, 2);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_4, 2, CellSpan.Rows);
            table1.EndRow();
            table1.StartRow();
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_5);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_6);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_5);
            table1.AddHeaderCell(Resources.Reports.Rate.Column2_6);
            table1.EndRow();
            table1.EndHeader();

            foreach (Data.SpeciesRow spcRow in stack.GetSpecies())
            {
                table1.StartRow();
                table1.AddCell(spcRow.GetReportShortPresentation());
                table1.AddCellRight(stack.GetIndiscriminance(spcRow), fracFormat);

                Mathematics.Statistics.Regression gr = stack.Parent.GrowthModels.GetInternalRegression(spcRow.Species);
                if (gr == null)
                {
                    table1.AddCellRight(Mayfly.Constants.Null);
                    table1.AddCellRight(Mayfly.Constants.Null);
                }
                else
                {
                    //double g1 = ((double)gr.GetRunouts().Count / (double)gr.Data.Count);
                    //double g2 = gr.Determination;

                    //table1.AddCellRight(g1, fracFormat);
                    //table1.AddCellRight(g2, fracFormat);
                }

                Mathematics.Statistics.Regression wr = stack.Parent.MassModels.GetInternalRegression(spcRow.Species);
                if (wr == null)
                {
                    table1.AddCellRight(Mayfly.Constants.Null);
                    table1.AddCellRight(Mayfly.Constants.Null);
                }
                else
                {
                    //double w1 = ((double)wr.GetRunouts().Count / (double)wr.Data.Count);
                    //double w2 = wr.Determination;

                    //table1.AddCellRight(w1, fracFormat);
                    //table1.AddCellRight(w2, fracFormat);
                }

                table1.AddCellRight(stack.GetTreatmentRate(spcRow), fracFormat);
                table1.EndRow();
            }

            table1.StartFooter();
            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Mean);
            table1.AddCell();
            table1.AddCell();
            table1.AddCell();
            table1.AddCell();
            table1.AddCell();
            table1.AddCellRight(stack.GetTreatmentRate(), fracFormat);
            table1.EndRow();
            table1.EndFooter();

            report.AddTable(table1);
        }
    }
}
