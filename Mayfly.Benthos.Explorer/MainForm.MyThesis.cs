using Mayfly.Extensions;
using Mayfly.Mathematics.Charts;
using Mayfly.Mathematics.Statistics.Regression;
using Meta.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Mayfly.Benthos.Explorer
{
    partial class MainForm
    {
        Data.SpeciesRow[] speciesThesa;
        CultureInfo ci;
        CardStack vsStack;
        CardStack vlStack;



        private void CompileThesis()
        {
            ci = Application.CurrentCulture;

            Application.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            speciesThesa = data.Species.GetPhylogeneticallySorted();

            IsBusy = true;
            processDisplay.StartProcessing(speciesThesa.Length, "Compiling thesa");

            BackgroundWorker rep = new BackgroundWorker();
            rep.WorkerReportsProgress = true;
            rep.DoWork += rep_DoWork;
            rep.RunWorkerCompleted += rep_RunWorkerCompleted;
            rep.ProgressChanged += rep_ProgressChanged;

            rep.RunWorkerAsync(); 
        }



        void rep_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            processDisplay.SetStatus(statusProcess, speciesThesa[e.ProgressPercentage].Species);
            processDisplay.SetProgress(e.ProgressPercentage);
        }

        void rep_DoWork(object sender, DoWorkEventArgs e)
        {
            vsStack= FullStack.GetStack(data.Water.FindByID(-1));
            vlStack = data.GetNotIncluded(vsStack);
            List<HeadAssess> result = new List<HeadAssess>();

            for (int i = 0; i < speciesThesa.Length; i++)
            {
                Data.SpeciesRow speciesRow = speciesThesa[i];
                ((BackgroundWorker)sender).ReportProgress(i);

                //if (speciesRow.Species == "Polypedilum nubeculosum") continue;

                if (speciesRow.Species.Contains(" gr.")) continue;
                if (speciesRow.Species.Contains(" sp.")) continue;

                List<Data.IndividualRow> wRows = speciesRow.GetWeightedIndividualRows();

                if (wRows.Count < HeadAssess.limit) continue;

                var spc = new HeadAssess(speciesRow, vsStack, vlStack);
                spc.Count = wRows.Count;
                result.Add(spc);

            }

            e.Result = result;
        }

        void rep_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
            processDisplay.StopProcessing();
            GetThesis((List<HeadAssess>)e.Result).Run();
            Application.CurrentCulture = ci;
        }



        public Report GetThesis(List<HeadAssess> result)
        {
            //double regret = .05;
            double coin = .5;

            Report report = new Report("Bios", "thesis.css");

            report.UseTableNumeration = true;

            //int id = 0;

            #region Models

            //foreach (HeadAssess spc in result)
            //{
            //    report.AddSubtitle(spc.Species.ReportFullPresentation);

            //    report.AddParagraph("Выборка вида составила {0} экз. ", spc.Count));

            //    if (spc.VystitysSample != null && spc.VolgaSample != null)
            //    {
            //        if (spc.VystitysSample.lw != null)
            //        {
            //            report.AddParagraph(
            //                "Уравнение зависимости массы от длины для выборки озера Виштынецкого ({0}):",
            //                spc.VystitysSample.lw.GetShortDescription()));

            //            report.AddEquation(spc.VystitysSample.lw.GetFullEquation("W", "L", "N3"), ",");
            //        }

            //        if (spc.VolgaSample.lw != null)
            //        {
            //            report.AddParagraph(
            //                "для выборки бассейна Верхней Волги ({0}):",
            //                spc.VolgaSample.lw.GetShortDescription()));

            //            report.AddEquation(spc.VolgaSample.lw.GetFullEquation("W", "L", "N3"));
            //        }

            //        if (spc.VystitysSample.lw != null && spc.VolgaSample.lw != null)
            //        {
            //            RegressionPool pool = new RegressionPool(
            //                new List<Regression>() { spc.VystitysSample.lw, spc.VolgaSample.lw });

            //            if (pool.Coincidence.Probability <= regret)
            //            {
            //                report.AddParagraph("Модели двух выборок значимо отличаются ({0}).",
            //                    pool.GetShortDescription()));

            //            }
            //            else if (pool.Coincidence.Probability >= coin)
            //            {
            //                report.AddParagraph(
            //                    "Модели двух выборок совпадают ({0}). Обобщенная модель <i>W</i>(<i>L</i>) имеет вид",
            //                    pool.GetShortDescription()));

            //                report.AddEquation(spc.TotalSample.lw.GetFullEquation("W", "L", "N3"), ",");

            //                report.AddParagraph(
            //                    "и обладает {0} ({1}).",
            //                    spc.TotalSample.lw.DeterminationStrength.ToLower().Replace("ая", "ой").Replace("язь", "язью"),
            //                    spc.TotalSample.lw.GetShortDescription()));
            //            }
            //            else
            //            {
            //                report.AddParagraph(
            //                    "Заключение о значимом совпадении или несовпадении моделей двух выборок невозможно ({0}).",
            //                    pool.GetShortDescription()));
            //            }
            //        }
            //    }
            //    else if (spc.TotalSample.lw != null)
            //    {
            //        report.AddParagraph("Уравнение зависимости массы от длины <i>W</i>(<i>L</i>) имеет вид");

            //        report.AddEquation(spc.TotalSample.lw.GetFullEquation("W", "L", "N3"), ",");

            //        report.AddParagraph(
            //            "и обладает {0} ({1}).",
            //            spc.TotalSample.lw.DeterminationStrength.ToLower().Replace("ая", "ой").Replace("язь", "язью"),
            //            spc.TotalSample.lw.GetShortDescription()));
            //    }


            //    if (spc.VystitysSample != null && spc.VolgaSample != null)
            //    {
            //        if (spc.VystitysSample.dw != null)
            //        {
            //            report.AddParagraph(
            //            "Уравнение связи массы и ширины головной капсулы для выборки озера Виштынецкого ({0}):",
            //            spc.VystitysSample.dw.GetShortDescription()));

            //            report.AddEquation(spc.VystitysSample.dw.GetFullEquation("W", "d", "N3"), ",");
            //        }

            //        if (spc.VolgaSample.dw != null)
            //        {
            //            report.AddParagraph(
            //            "для выборки  бассейна Верхней Волги ({0}):",
            //            spc.VolgaSample.dw.GetShortDescription()));

            //            report.AddEquation(spc.VolgaSample.dw.GetFullEquation("W", "d", "N3"));
            //        }

            //        if (spc.VystitysSample.dw != null && spc.VolgaSample.dw != null)
            //        {
            //            RegressionPool pool = new RegressionPool(
            //                new List<Regression>() { spc.VystitysSample.dw, spc.VolgaSample.dw });

            //            if (pool.Coincidence.Probability <= regret)
            //            {
            //                report.AddParagraph("Модели двух выборок значимо отличаются ({0}).",
            //                    pool.GetShortDescription()));

            //            }
            //            else if (pool.Coincidence.Probability >= coin)
            //            {
            //                report.AddParagraph(
            //                    "Модели двух выборок совпадают ({0}). Обобщенная модель <i>W</i>(<i>d</i>) имеет вид",
            //                    pool.GetShortDescription()));

            //                report.AddEquation(spc.TotalSample.dw.GetFullEquation("W", "d", "N3"), ",");

            //                report.AddParagraph(
            //                    "и обладает {0} ({1}).",
            //                    spc.TotalSample.dw.DeterminationStrength.ToLower().Replace("ая", "ой").Replace("язь", "язью"),
            //                    spc.TotalSample.dw.GetShortDescription()));
            //            }
            //            else
            //            {
            //                report.AddParagraph(
            //                    "Заключение о значимом совпадении или несовпадении моделей двух выборок невозможно ({0}).",
            //                    pool.GetShortDescription()));
            //            }
            //        }
            //    }
            //    else if (spc.TotalSample.dw != null)
            //    {
            //        report.AddParagraph("Уравнение связи массы и ширины головной капсулы <i>W</i>(<i>d</i>) имеет вид");

            //        report.AddEquation(spc.TotalSample.dw.GetFullEquation("W", "d", "N3"), ",");

            //        report.AddParagraph(
            //            "и обладает {0} ({1}).",
            //            spc.TotalSample.dw.DeterminationStrength.ToLower().Replace("ая", "ой").Replace("язь", "язью"),
            //            spc.TotalSample.dw.GetShortDescription()));
            //    }


            //    //if (spc.TotalSample.dw != null)
            //    //{
            //    //    ChartForm chartForm = new ChartForm();

            //    //    Scatterplot generalized = new Scatterplot(spc.TotalSample.dw.Data, spc.TotalSample.dw.Name);
            //    //    generalized.Properties.ShowTrend = true;
            //    //    generalized.Properties.SelectedApproximationType = Mathematics.Statistics.Regression.RegressionType.Power;
            //    //    generalized.BuildSeries();
            //    //    chartForm.StatChart.AddSeries(generalized);

            //    //    chartForm.StatChart.AxisYTitle = "Масса, мг";
            //    //    chartForm.StatChart.AxisXTitle = "Длина, мм";

            //    //    chartForm.StatChart.Remaster();
            //    //    chartForm.StatChart.Update(sender, e);

            //    //    Stream stream = new MemoryStream();
            //    //    chartForm.StatChart.SaveImage(stream, ChartImageFormat.Jpeg);
            //    //    Image image = Image.FromStream(stream);


            //    //    report.AddImage(image, id);

            //    //    id++;
            //    //}



            //    if (spc.TotalSample.Instars.Count > 0)
            //    {
            //        report.AddParagraph(
            //            "Эмпирические значения ширины капсулы, длины и массы тела личинок, а также теоретические значения массы приведены в таблице {0}.",
            //            report.NextTableNumber));

            //        Report.Table table1 = new Report.Table(
            //            "Размерно-возрастная характеристика и оценка моделей зависимости массы от длины тела и ширины головной капсулы личинок {0}",
            //            spc.Species.ReportFullPresentation));

            //        table1.StartTableHeader();

            //        table1.StartRow();
            //        report.AddHeaderCell("Возраст", CellSpan.Rows, 2);
            //        report.AddHeaderCell("Выборка, экз.", CellSpan.Rows, 2);
            //        report.AddHeaderCell("Эмпирические значения", CellSpan.Columns, 3);
            //        report.AddHeaderCell("Теоретические значения", CellSpan.Columns, 2);
            //        table1.EndRow();

            //        table1.StartRow();
            //        report.AddHeaderCell("<i>d</i>", .2);
            //        report.AddHeaderCell("<i>L</i>");
            //        report.AddHeaderCell("<i>W</i>");
            //        report.AddHeaderCell("<i>W</i>(<i>L</i>)");
            //        report.AddHeaderCell("<i>W</i>(<i>d</i>)");
            //        table1.EndRow();

            //        table1.EndTableHeader();

            //        bool bias = false;

            //        foreach (HeadSample pop in
            //            (spc.VystitysSample != null &&
            //            spc.VolgaSample != null &&
            //            spc.VystitysSample.Instars.Count > 0 &&
            //            spc.VolgaSample.Instars.Count > 0
            //            )
            //            ?
            //            new HeadSample[] { spc.VystitysSample, spc.VolgaSample, spc.TotalSample }
            //            : new HeadSample[] { spc.TotalSample })
            //        {
            //            if (pop == null) continue;
            //            if (pop.Count == 0) continue;
            //            if (pop.Instars.Count == 0) continue;

            //            table1.StartRow();
            //            table1.AddCellValue(pop.Name, CellSpan.Columns, 7);
            //            table1.EndRow();

            //            foreach (HeadInstarSample instar in pop.Instars)
            //            {
            //                table1.StartRow();

            //                table1.AddCellValue(instar.Instar);
            //                table1.AddCellValue(instar.Count);

            //                if (instar.CapsuleSample != null) table1.AddCellValue(instar.CapsuleSample, "G3");
            //                else table1.AddCell();

            //                if (instar.LengthSample != null) table1.AddCellValue(instar.LengthSample, "G1");
            //                else table1.AddCell();

            //                if (instar.MassSample != null)
            //                {
            //                    table1.AddCellValue(instar.MassSample, "G2");

            //                    double w1 = 0;

            //                    Meta.Numerics.Interval massInterval = Meta.Numerics.Interval.FromEndpoints(0, 1);

            //                    if (instar.MassSample.Count > 1) massInterval = instar.MassSample.PopulationMean.ConfidenceInterval(.95);

            //                    if (instar.LengthSample != null && pop.lw != null)
            //                    {
            //                        w1 = pop.lw.Predict(instar.LengthSample.Mean);
            //                        table1.AddCellValue(w1.ToString("N2") +
            //                            ((instar.MassSample.Count > 1 && massInterval.OpenContains(w1)) ? "" : "*")
            //                            );
            //                    }
            //                    else table1.AddCell();

            //                    double w2 = 0;
            //                    if (instar.CapsuleSample != null && pop.dw != null)
            //                    {
            //                        w2 = pop.dw.Predict(instar.CapsuleSample.Mean);
            //                        table1.AddCellValue(w2.ToString("N2") +
            //                            ((instar.MassSample.Count > 1 && massInterval.OpenContains(w2)) ? "" : "*")
            //                            );
            //                    }
            //                    else table1.AddCell();

            //                    bias |= !massInterval.OpenContains(w1) || !massInterval.OpenContains(w2);
            //                }
            //                else
            //                {
            //                    table1.AddCell();
            //                    table1.AddCell();
            //                    table1.AddCell();
            //                }

            //                table1.EndRow();
            //            }
            //        }

            //        report.AddTable(table1);

            //        if (bias)
            //            report.AddComment("* - оценка массы, полученная при помощи модели, смещена относительно 95% доверительного интервала эмпирических значений.");
            //    }
            //}

            #endregion

            //report.AddHeader("Обобщение и сведение информации");
            report.AddParagraph("Эмпирические характеристики старших возрастов и коэффициенты уравнений по всем описанным выше выборкам сведены в таблице {0}", report.NextTableNumber);

            #region Generalized table (Empiric heads)

            Report.Table table1 = new Report.Table("Размерно-возрастная характеристика старших возрастов");

            table1.StartHeader(0, .05, .125, .125, .05, .125, .125);

            table1.StartRow();
            table1.AddHeaderCell("Вид", 2, CellSpan.Rows);
            table1.AddHeaderCell("III", 3);
            table1.AddHeaderCell("IV", 3);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell("<i>n</i>");
            table1.AddHeaderCell("<i>d</i>");
            table1.AddHeaderCell("<i>W</i>");
            table1.AddHeaderCell("<i>n</i>");
            table1.AddHeaderCell("<i>d</i>");
            table1.AddHeaderCell("<i>W</i>");
            table1.EndRow();

            table1.EndHeader();


            foreach (HeadAssess spc in result)
            {
                if (spc.TotalSample.Instars.Count == 0) continue;

                HeadSample[] samples =
                    (spc.VystitysSample != null &&
                    spc.VolgaSample != null &&
                    spc.VystitysSample.Instars.Count > 0 &&
                    spc.VolgaSample.Instars.Count > 0)
                    ?
                    new HeadSample[] { spc.VystitysSample, spc.VolgaSample, spc.TotalSample } :
                    new HeadSample[] { spc.TotalSample };

                //table1.AddCell(spc.Species.Species, CellSpan.Rows, samples.Length);

                foreach (HeadSample pop in samples)
                {
                    table1.StartRow();
                    table1.AddCell(spc.Species.Species + (samples.Length > 1 ? (" (" + pop.Legend + ")") : string.Empty));
                    //table1.AddCell(pop.Legend);

                    bool had3 = false;

                    foreach (HeadInstarSample instar in pop.Instars)
                    {
                        if (instar.Instar == 3)
                        {
                            table1.AddCellValue(instar.Count);
                            table1.AddCellValue(instar.CapsuleSample, "E2");
                            //table1.AddCellValue(instar.LengthSample, "G2");
                            table1.AddCellValue(instar.MassSample, "S2");
                            had3 = true;
                        }
                    }

                    if (!had3)
                    {
                        table1.AddCellValue(Constants.Null);
                        table1.AddCellValue(Constants.Null);
                        //table1.AddCellValue(Constants.Null);
                        table1.AddCellValue(Constants.Null);
                    }

                    bool had4 = false;

                    foreach (HeadInstarSample instar in pop.Instars)
                    {
                        if (instar.Instar == 4)
                        {
                            table1.AddCellValue(instar.Count);
                            table1.AddCellValue(instar.CapsuleSample, "E2");
                            //table1.AddCellValue(instar.LengthSample, "G2");
                            table1.AddCellValue(instar.MassSample, "S2");
                            had4 = true;
                        }
                    }

                    if (!had4)
                    {
                        table1.AddCellValue(Constants.Null);
                        table1.AddCellValue(Constants.Null);
                        //table1.AddCellValue(Constants.Null);
                        table1.AddCellValue(Constants.Null);
                    }


                    //if (pop != spc.TotalSample)
                    //{
                    //    table1.EndRow();
                    //    table1.StartRow();
                    //}

                    table1.EndRow();
                }

            }

            report.AddTable(table1);

            #endregion

            #region Generalized table (Length-mass models only)

            Report.Table table2 = new Report.Table("Сводная таблица моделей 'Длина-масса'");

            table2.StartHeader(0, .1, .15, .15, .1, .1);

            table2.StartRow();
            table2.AddHeaderCell("Вид", 2, CellSpan.Rows);
            //report.AddHeaderCell("Регион", CellSpan.Rows, 2);
            table2.AddHeaderCell("Параметры уравнения W(L)", 5);
            table2.EndRow();

            table2.StartRow();
            table2.AddHeaderCell("n");
            table2.AddHeaderCell("<i>q</i>");
            table2.AddHeaderCell("<i>b</i>");
            table2.AddHeaderCell("r²");
            table2.AddHeaderCell("<i>p</i>");
            table2.EndRow();

            table2.EndHeader();


            foreach (HeadAssess spc in result)
            {
                HeadSample[] samples =
                    //(spc.VystitysSample != null &&
                    //spc.VolgaSample != null &&
                    //spc.VystitysSample.Instars.Count > 0 &&
                    //spc.VolgaSample.Instars.Count > 0)
                    //?
                    //new HeadSample[] { spc.VystitysSample, spc.VolgaSample, spc.TotalSample } :
                    new HeadSample[] { spc.TotalSample };

                //table2.StartRow();
                //table2.AddCell(spc.Species.Species, CellSpan.Rows, samples.Length);

                foreach (HeadSample pop in samples)
                {
                    if (pop.lw == null) continue;
                    if (pop.Count == 0) continue;

                    table2.StartRow();

                    // Get p if pop.Legend is Общ
                    if (/*samples.Length > 1 && pop == spc.TotalSample*/
                        spc.VystitysSample != null && spc.VolgaSample != null
                        )
                    {
                        RegressionPool rp = new RegressionPool(new List<Regression>() { spc.VystitysSample.lw, spc.VolgaSample.lw }, pop.Name);
                        //table2.AddCell(string.Format("{0} (<i>p</i> = {1:N3})", pop.Legend, rp.Coincidence.Probability));
                        table2.AddCell(string.Format("{0} (<i>p</i> = {1:N3})", spc.Species.Species, rp.Coincidence.Probability));
                    }
                    else
                    {
                        table2.AddCell(spc.Species.Species);
                    }

                    table2.AddCellValue(pop.lw.Data.Count);
                    table2.AddCellValue(pop.lw.GetParameter(0, "N3"));
                    table2.AddCellValue(pop.lw.GetParameter(1, "N2"));
                    table2.AddCellValue(pop.lw.Determination.ToString("N3"));
                    table2.AddCellValue(pop.lw.Fit.GoodnessOfFit.Probability.ToString("N3"));

                    //if (pop != spc.TotalSample)
                    //{
                    table2.EndRow();
                    //    table2.StartRow();
                    //}
                }

                table2.EndRow();
            }

            report.AddTable(table2);

            //report.ResetPortrait();

            //report.CloseDiv();

            #endregion

            #region Generalized table (Head-mass models only)

            Report.Table table3 = new Report.Table("Сводная таблица моделей 'Ширина головной капсулы-масса'");

            table3.StartHeader(0, .1, .15, .1, .1);

            table3.StartRow();
            table3.AddHeaderCell("Вид", 2, CellSpan.Rows);
            //report.AddHeaderCell("Регион", 2, CellSpan.Rows);
            table3.AddHeaderCell("Параметры уравнения W(d)", 5);
            table3.EndRow();

            table3.StartRow();
            table3.AddHeaderCell("n");
            table3.AddHeaderCell("<i>g</i>");
            table3.AddHeaderCell("<i>a</i>");
            table3.AddHeaderCell("r²");
            table3.AddHeaderCell("<i>p</i>");
            table3.EndRow();

            table3.EndHeader();


            foreach (HeadAssess spc in result)
            {
                HeadSample[] samples = (spc.VystitysSample != null &&
                    spc.VolgaSample != null &&
                    spc.VystitysSample.Instars.Count > 0 &&
                    spc.VolgaSample.Instars.Count > 0)
                    ?
                    new HeadSample[] { spc.VystitysSample, spc.VolgaSample, spc.TotalSample }
                    : new HeadSample[] { spc.TotalSample };

                //table3.StartRow();
                //table3.AddCell(spc.Species.Species, CellSpan.Rows, samples.Length);

                foreach (HeadSample pop in samples)
                {
                    if (pop.dw == null) continue;
                    if (pop.Count == 0) continue;


                    table3.StartRow();

                    // Get p if pop.Legend is Общ
                    if (spc.VystitysSample != null && spc.VolgaSample != null &&
                        spc.VystitysSample.dw != null && spc.VolgaSample.dw != null &&
                        pop == spc.TotalSample)
                    {
                        RegressionPool rp = new RegressionPool(new List<Regression>() { spc.VystitysSample.dw, spc.VolgaSample.dw }, pop.Name);
                        //table3.AddCell(spc.Species.Species + (samples.Length > 1 ? (" (" + pop.Legend + ")") : string.Empty));
                        table3.AddCell(string.Format("{0} (<i>p</i> = {1:N3})", spc.Species.Species, rp.Coincidence.Probability));
                    }
                    else
                    {
                        table3.AddCell(spc.Species.Species + (samples.Length > 1 ? (" (" + pop.Legend + ")") : string.Empty));
                        //table3.AddCell(pop.Legend);
                    }

                    table3.AddCellValue(pop.dw.Data.Count);
                    table3.AddCellValue(pop.dw.GetParameter(0, "N1"));
                    table3.AddCellValue(pop.dw.GetParameter(1, "N2"));
                    table3.AddCellValue(pop.dw.Determination.ToString("N3"));
                    table3.AddCellValue(pop.dw.Fit.GoodnessOfFit.Probability.ToString("N3"));

                    //if (pop != spc.TotalSample)
                    //{
                    //    table3.EndRow();
                    //    table3.StartRow();
                    //}

                    table3.EndRow();
                }

                //table3.EndRow();
            }

            report.AddTable(table3);

            //report.ResetPortrait();

            //report.CloseDiv();

            #endregion

            #region Generalized table (Models only)

            //Report.Table table1 = new Report.Table("Сводная таблица моделей");

            //table1.StartTableHeader();

            //report.AddCol();
            //report.AddCol(.1);

            //report.AddCol(.1);
            //report.AddCol(.1);
            //report.AddCol(.1);

            //report.AddCol(.1);
            //report.AddCol(.1);
            //report.AddCol(.1);

            //table1.StartRow();
            //report.AddHeaderCell("Вид", CellSpan.Rows, 2);
            //report.AddHeaderCell("Регион", CellSpan.Rows, 2);
            //report.AddHeaderCell("Параметры уравнения W(L)", CellSpan.Columns, 3);
            //report.AddHeaderCell("Параметры уравнения W(d)", CellSpan.Columns, 3);
            //table1.EndRow();

            //table1.StartRow();
            //report.AddHeaderCell("q");
            //report.AddHeaderCell("b");
            //report.AddHeaderCell("r²");

            //report.AddHeaderCell("g");
            //report.AddHeaderCell("a");
            //report.AddHeaderCell("r²");
            //table1.EndRow();

            //table1.EndTableHeader();


            //foreach (HeadAssess spc in result)
            //{
            //    HeadSample[] samples = (spc.VystitysSample != null &&
            //        spc.VolgaSample != null &&
            //        spc.VystitysSample.Instars.Count > 0 &&
            //        spc.VolgaSample.Instars.Count > 0)
            //        ?
            //        new HeadSample[] { spc.VystitysSample, spc.VolgaSample, spc.TotalSample }
            //        : new HeadSample[] { spc.TotalSample };

            //    table1.StartRow();
            //    table1.AddCell(spc.Species.ReportFullPresentation, CellSpan.Rows, samples.Length);

            //    foreach (HeadSample pop in samples)
            //    {
            //        if (pop == null) continue;
            //        if (pop.Count == 0) continue;

            //        table1.AddCell(pop.Legend);

            //        if (pop.lw != null)
            //        {
            //            table1.AddCellRight(pop.lw.GetParameter(0, "N3"));
            //            table1.AddCellRight(pop.lw.GetParameter(1, "N2"));
            //            table1.AddCellRight(pop.lw.Determination.ToString("N2"));
            //        }
            //        else
            //        {
            //            table1.AddCellValue(Constants.Null);
            //            table1.AddCellValue(Constants.Null);
            //            table1.AddCellValue(Constants.Null);
            //        }


            //        if (pop.dw != null)
            //        {
            //            table1.AddCellRight(pop.dw.GetParameter(0, "N1"));
            //            table1.AddCellRight(pop.dw.GetParameter(1, "N2"));
            //            table1.AddCellRight(pop.dw.Determination.ToString("N2"));
            //        }
            //        else
            //        {
            //            table1.AddCellValue(Constants.Null);
            //            table1.AddCellValue(Constants.Null);
            //            table1.AddCellValue(Constants.Null);
            //        }


            //        if (pop.Name != "Обобщенная выборка")
            //        {
            //            table1.EndRow();
            //            table1.StartRow();
            //        }
            //    }

            //    table1.EndRow();
            //}

            //report.AddTable(table1);

            ////report.ResetPortrait();

            ////report.CloseDiv();

            #endregion

            #region Coincidence

            report.AddHeader("Тестирование совпадений");

            #region Subgenus level

            //report.AddSubtitle("Тестирование совпадений на уровне подродов");

            //List<string[]> subgenera = new List<string[]>();

            //string[] Holotanypus = new string[] {
            //    "Procladius choreus", "Procladius culiciformis", "Procladius johnsoni",
            //    "Procladius ferrugineus", "Procladius signatus", "Procladius simplicistilus",
            //    "Procladius rufovittatus" };

            //subgenera.Add(Holotanypus);

            ////string[] Psilotanypus = new string[] {
            ////    "Procladius rufovittatus"
            ////};

            ////subgenera.Add(Psilotanypus);

            //foreach (string[] list in subgenera)
            //{
            //    DescribeCoincidence(report, list, result, coin);
            //}

            #endregion

            #region Genus level

            report.AddSubtitle("Тестирование совпадений на уровне рода");

            List<string[]> genera = new List<string[]>();

            string[] Ablabesmyia = new string[] {
                "Ablabesmyia phatta", "Ablabesmyia monilis" };

            genera.Add(Ablabesmyia);

            string[] Procladius = new string[] {
                "Procladius choreus", "Procladius culiciformis", "Procladius johnsoni",
                "Procladius ferrugineus", "Procladius signatus", "Procladius simplicistilus",
                "Procladius rufovittatus" };

            //string[] Procladius = new string[] {
            //    "Procladius rufovittatus" };

            //Holotanypus.CopyTo(Procladius, 0);

            genera.Add(Procladius);

            string[] Cricotopus = new string[] {
                "Cricotopus ornatus", "Cricotopus sylvestris" };

            genera.Add(Cricotopus);

            string[] Chironomus = new string[] {
                "Chironomus agilis", "Chironomus anthracinus",
                "Chironomus melanescens", "Chironomus muratensis",
                "Chironomus nudiventris", "Chironomus plumosus", "Chironomus tentans" };

            genera.Add(Chironomus);

            string[] Cryptochironomus = new string[] {
                "Cryptochironomus obreptans", "Cryptochironomus redekei" };

            genera.Add(Cryptochironomus);

            string[] Dicrotendipes = new string[] {
                "Dicrotendipes nervosus", "Dicrotendipes pulsus" };

            genera.Add(Dicrotendipes);

            string[] Glyptotendipes = new string[] {
                "Glyptotendipes paripes", "Glyptotendipes cauliginellus" };

            genera.Add(Glyptotendipes);

            //string[] Polypedilum = new string[] {
            //    "Polypedilum tetracrenatum", "Polypedilum convictum", "Polypedilum nubeculosum",
            //    "Polypedilum scalaenum", "Polypedilum bicrenatum" };

            string[] Polypedilum = new string[] {
                "Polypedilum scalaenum", "Polypedilum bicrenatum" };

            genera.Add(Polypedilum);

            string[] Sergentia = new string[] {
                "Sergentia baueri", "Sergentia coracina" };

            genera.Add(Sergentia);

            string[] Tanytarsus = new string[] {
                "Tanytarsus lugens", "Tanytarsus miriforceps" };

            genera.Add(Tanytarsus);

            foreach (string[] genus in genera)
            {
                DescribeCoincidence(report, nameof(genus), genus, result, coin);
            }

            #endregion

            #region Tribe level

            report.AddSubtitle("Тестирование совпадений на уровне трибы");

            List<string[]> tribes = new List<string[]>();

            ////string[] Pentaneurini = Ablabesmyia;

            ////string[] Procladiini = Procladius;

            string[] Chironomini = new string[] { "Benthalia carbonaria",

                "Chironomus agilis", "Chironomus anthracinus",
                "Chironomus melanescens", "Chironomus muratensis",
                "Chironomus nudiventris", "Chironomus plumosus", "Chironomus tentans",

                "Cladopelma viridulum",

                "Cryptochironomus obreptans", "Cryptochironomus redekei",

                "Cryptotendipes nigronitens",

                "Demicryptochirnomus vulneratus",

                "Dicrotendipes nervosus", "Dicrotendipes pulsus",

                "Einfeldia pagana",

                "Endochironomus albipennis",

                "Fleuria lacustris",

                "Glyptotendipes paripes", "Glyptotendipes cauliginellus",

                "Harnischia curtilamellata", "Lipiniella arenicola", "Microchironomus tener", "Microtendipes pedellus",
                "Parachironomus biannulatus", "Paralauterborniella nigrohalteralis", "Paratendipes albimanus",

                "Polypedilum tetracrenatum", "Polypedilum convictum", "Polypedilum nubeculosum",
                "Polypedilum scalaenum", "Polypedilum bicrenatum",

                "Sergentia baueri", "Sergentia coracina",

                "Stictochironomus crassiforceps" };

            tribes.Add(Chironomini);

            string[] Tanytarsini = new string[] {
                "Corynocera ambiqua", "Micropsectra radialis",
                "Tanytarsus lugens", "Tanytarsus miriforceps"
            };

            tribes.Add(Tanytarsini);

            foreach (string[] tribe in tribes)
            {
                DescribeCoincidence(report, nameof(tribe), tribe, result, coin);
            }

            #endregion

            #region Subfamily level

            report.AddSubtitle("Тестирование совпадений на уровне подсемейств");

            List<string[]> subfamilies = new List<string[]>();

            string[] Tanypodinae = new string[] {
                "Ablabesmyia phatta", "Ablabesmyia monilis",
                "Procladius choreus", "Procladius culiciformis", "Procladius johnsoni",
                "Procladius ferrugineus", "Procladius signatus", "Procladius simplicistilus",
                "Procladius rufovittatus", "Tanypus punctpennis"
            };

            subfamilies.Add(Tanypodinae);

            string[] Orthocladiinae = new string[] {
                "Acricotopus lucens", "Corynoneura edwardsi",
                "Cricotopus ornatus", "Cricotopus sylvestris",
                "Propsilocerus lacustris", "Psectrocladius fabricus"
            };

            subfamilies.Add(Orthocladiinae);

            string[] Chironominae = new string[Chironomini.Length + Tanytarsini.Length];

            Chironomini.CopyTo(Chironominae, 0);
            Tanytarsini.CopyTo(Chironominae, Chironomini.Length);

            subfamilies.Add(Chironominae);

            foreach (string[] subfamily in subfamilies)
            {
                DescribeCoincidence(report, nameof(subfamily), subfamily, result, coin);
            }

            #endregion

            #region Family level

            report.AddSubtitle("Тестирование совпадений на уровне семейства");

            string[] Chironomidae = new string[Tanypodinae.Length + 2 + Orthocladiinae.Length + Chironominae.Length];

            Tanypodinae.CopyTo(Chironomidae, 0);
            Chironomidae[Tanypodinae.Length] = "Potthastia longimanus";
            Chironomidae[Tanypodinae.Length + 1] = "Monodiamesa bathyphila";
            Orthocladiinae.CopyTo(Chironomidae, Tanypodinae.Length + 2);
            Chironominae.CopyTo(Chironomidae, Tanypodinae.Length + 2 + Orthocladiinae.Length);

            DescribeCoincidence(report, nameof(Chironomidae), Chironomidae, result, coin);

            foreach (string sp in Chironomidae)
            {
                Service.SaveAssociates(sp, Chironomidae);
            }

            #endregion

            #endregion

            #region Generalized table (Full)

            //report.ResetLandscape();

            //Report.Table table1 = new Report.Table("Сводная таблица");

            //table1.StartTableHeader();

            //report.AddCol();
            //report.AddCol(.03);

            //report.AddCol(.03);
            //report.AddCol(.09);
            //report.AddCol(.09);
            //report.AddCol(.09);

            //report.AddCol(.03);
            //report.AddCol(.09);
            //report.AddCol(.09);
            //report.AddCol(.09);

            //report.AddCol(.04);
            //report.AddCol(.04);
            //report.AddCol(.04);

            //report.AddCol(.04);
            //report.AddCol(.04);
            //report.AddCol(.04);

            //table1.StartRow();
            //report.AddHeaderCell("Вид", CellSpan.Rows, 3);
            //report.AddHeaderCell("Регион", CellSpan.Rows, 3);
            //report.AddHeaderCell("Размерная характеристика старших возрастов", CellSpan.Columns, 8);
            //report.AddHeaderCell("Параметры уравнения W(L)", CellSpan.Columns, 3);
            //report.AddHeaderCell("Параметры уравнения W(d)", CellSpan.Columns, 3);
            //table1.EndRow();

            //table1.StartRow();
            //report.AddHeaderCell("III", CellSpan.Columns, 4);
            //report.AddHeaderCell("IV", CellSpan.Columns, 4);
            //report.AddHeaderCell("q", CellSpan.Rows, 2);
            //report.AddHeaderCell("b", CellSpan.Rows, 2);
            //report.AddHeaderCell("r²", CellSpan.Rows, 2);
            //report.AddHeaderCell("g", CellSpan.Rows, 2);
            //report.AddHeaderCell("a", CellSpan.Rows, 2);
            //report.AddHeaderCell("r²", CellSpan.Rows, 2);
            //table1.EndRow();

            //table1.StartRow();
            //report.AddHeaderCell("<i>n</i>");
            //report.AddHeaderCell("<i>d</i>");
            //report.AddHeaderCell("<i>L</i>");
            //report.AddHeaderCell("<i>W</i>");
            //report.AddHeaderCell("<i>n</i>");
            //report.AddHeaderCell("<i>d</i>");
            //report.AddHeaderCell("<i>L</i>");
            //report.AddHeaderCell("<i>W</i>");
            //table1.EndRow();

            //table1.EndTableHeader();


            //foreach (HeadAssess spc in result)
            //{
            //    HeadSample[] samples = (spc.VystitysSample != null &&
            //        spc.VolgaSample != null &&
            //        spc.VystitysSample.Instars.Count > 0 &&
            //        spc.VolgaSample.Instars.Count > 0)
            //        ?
            //        new HeadSample[] { spc.VystitysSample, spc.VolgaSample, spc.TotalSample }
            //        : new HeadSample[] { spc.TotalSample };

            //    if (samples.Length > 0)
            //    {
            //        table1.StartRow();
            //        table1.AddCell(spc.Species.ReportFullPresentation, CellSpan.Rows, samples.Length);

            //        foreach (HeadSample pop in samples)
            //        {
            //            if (pop == null) continue;
            //            if (pop.Count == 0) continue;

            //            table1.AddCell(pop.Legend);

            //            bool had3 = false;

            //            foreach (HeadInstarSample instar in pop.Instars)
            //            {
            //                if (instar.Instar == 3)
            //                {
            //                    table1.AddCellValue(instar.Count);
            //                    table1.AddCellValue(instar.CapsuleSample, "G2");
            //                    table1.AddCellValue(instar.LengthSample, "G2");
            //                    table1.AddCellValue(instar.MassSample, "G2");
            //                    had3 = true;
            //                }
            //            }

            //            if (!had3)
            //            {
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //            }

            //            bool had4 = false;

            //            foreach (HeadInstarSample instar in pop.Instars)
            //            {
            //                if (instar.Instar == 4)
            //                {
            //                    table1.AddCellValue(instar.Count);
            //                    table1.AddCellValue(instar.CapsuleSample, "G2");
            //                    table1.AddCellValue(instar.LengthSample, "G2");
            //                    table1.AddCellValue(instar.MassSample, "G2");
            //                    had4 = true;
            //                }
            //            }

            //            if (!had4)
            //            {
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //            }


            //            if (pop.lw != null)
            //            {
            //                table1.AddCellRight(pop.lw.a.ToString("N3"));
            //                table1.AddCellRight(pop.lw.b.ToString("N2"));
            //                table1.AddCellRight(pop.lw.Determination.ToString("N2"));
            //            }
            //            else
            //            {
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //            }


            //            if (pop.dw != null)
            //            {
            //                table1.AddCellRight(pop.dw.a.ToString("N1"));
            //                table1.AddCellRight(pop.dw.b.ToString("N2"));
            //                table1.AddCellRight(pop.dw.Determination.ToString("N2"));
            //            }
            //            else
            //            {
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //                table1.AddCellValue(Constants.Null);
            //            }


            //            if (pop.Name != "Обобщенная выборка")
            //            {
            //                table1.EndRow();
            //                table1.StartRow();
            //            }
            //        }

            //        table1.EndRow();
            //    }
            //}

            //report.AddTable(table1);

            //report.ResetPortrait();

            #endregion

            #region Approach analysis

            Report.Table table4 = new Report.Table("Сводная таблица по анализу различных способов реконструкции массы личинок хирономид");

            table4.StartHeader(0, .15, .1, .1, .15, .1, .1);

            table4.StartRow();
            table4.AddHeaderCell("Вид", 2, CellSpan.Rows);
            table4.AddHeaderCell("III", 3);
            table4.AddHeaderCell("IV", 3);
            table4.EndRow();

            table4.StartRow();
            table4.AddHeaderCell("W");
            table4.AddHeaderCell("W(L)");
            table4.AddHeaderCell("W(d)");
            table4.AddHeaderCell("W");
            table4.AddHeaderCell("W(L)");
            table4.AddHeaderCell("W(d)");
            table4.EndRow();

            table4.EndHeader();

            foreach (HeadAssess spc in result)
            {
                table4.StartRow();
                table4.AddCell(spc.Species.Species);

                bool had3 = false;

                foreach (HeadInstarSample instar in spc.TotalSample.Instars)
                {
                    if (instar.Instar == 3)
                    {
                        had3 = true;

                        table4.AddCellValue(instar.MassSample, "C2");

                        if (instar.MassSample.Count < 3)
                        {
                            table4.AddCellValue(Constants.Null);
                            table4.AddCellValue(Constants.Null);
                        }
                        else
                        {
                            if (spc.TotalSample.lw != null && instar.LengthSample != null)
                            {
                                double wl = spc.TotalSample.lw.Predict(instar.LengthSample.Mean);

                                if (instar.MassSample.MarginsOfError().OpenContains(wl))
                                {
                                    table4.AddCellRight(wl, "N2");
                                }
                                else
                                {
                                    table4.AddCellRight(wl.ToString("N2") + "*", true);
                                }
                            }
                            else
                            {
                                table4.AddCellValue(Constants.Null);
                            }

                            if (spc.TotalSample.dw != null && instar.CapsuleSample != null)
                            {
                                double wd = spc.TotalSample.dw.Predict(instar.CapsuleSample.Mean);

                                if (instar.MassSample.MarginsOfError().OpenContains(wd))
                                {
                                    table4.AddCellRight(wd, "N2");
                                }
                                else
                                {
                                    table4.AddCellRight(wd.ToString("N2") + "*", true);
                                }
                            }
                            else
                            {
                                table4.AddCellValue(Constants.Null);
                            }
                        }
                    }
                }

                if (!had3)
                {
                    table4.AddCellValue(Constants.Null);
                    table4.AddCellValue(Constants.Null);
                    table4.AddCellValue(Constants.Null);
                }

                bool had4 = false;

                foreach (HeadInstarSample instar in spc.TotalSample.Instars)
                {
                    if (instar.Instar == 4)
                    {
                        had4 = true;

                        table4.AddCellValue(instar.MassSample, "C2");

                        if (instar.MassSample.Count < 3)
                        {
                            table4.AddCellValue(Constants.Null);
                            table4.AddCellValue(Constants.Null);
                        }
                        else
                        {
                            if (spc.TotalSample.lw != null && instar.LengthSample != null)
                            {
                                double wl = spc.TotalSample.lw.Predict(instar.LengthSample.Mean);

                                if (instar.MassSample.MarginsOfError().OpenContains(wl))
                                {
                                    table4.AddCellRight(wl, "N2");
                                }
                                else
                                {
                                    table4.AddCellRight(wl.ToString("N2") + "*", true);
                                }
                            }
                            else
                            {
                                table4.AddCellValue(Constants.Null);
                            }

                            if (spc.TotalSample.dw != null && instar.CapsuleSample != null)
                            {
                                double wd = spc.TotalSample.dw.Predict(instar.CapsuleSample.Mean);

                                if (instar.MassSample.MarginsOfError().OpenContains(wd))
                                {
                                    table4.AddCellRight(wd, "N2");
                                }
                                else
                                {
                                    table4.AddCellRight(wd.ToString("N2") + "*", true);
                                }
                            }
                            else
                            {
                                table4.AddCellValue(Constants.Null);
                            }
                        }
                    }
                }

                if (!had4)
                {
                    table4.AddCellValue(Constants.Null);
                    table4.AddCellValue(Constants.Null);
                    table4.AddCellValue(Constants.Null);
                }

                table4.EndRow();
            }

            report.AddTable(table4);

            #endregion

            report.EndSign();

            return report;
        }

        private void DescribeCoincidence(Report report, string name, string[] list, List<HeadAssess> result, double coin)
        {
            report.AddSubtitle3(string.Format("Проверка совпадения моделей {0}", name));

            RegressionPool lw_pool = headsPool_lw(result, list);

            if (lw_pool == null) goto DW;

            if (lw_pool.Coincidence == null) goto DW;

            if (lw_pool.Coincidence.Probability >= coin)
            {
                report.AddParagraph(
                    "Регрессионные модели связи длины с массой тела личинок хирономид {0} совпадают ({1}), что дает основание для построения единой для этих видов модели:",
                    list.Merge(), lw_pool.GetShortDescription());

                report.AddEquation(lw_pool.TotalRegression.GetFullEquation("W", "L", "N3"));

                report.AddParagraph(
                    "({0}).", lw_pool.TotalRegression.GetShortDescription());

                //ChartForm chartForm = new ChartForm();
                ////chartProperties.CopyTo(chartForm.StatChart);

                //foreach (Regression regr in lw_pool.SeparateRegressions)
                //{
                //    chartForm.StatChart.AddSeries(new Mayfly.Mathematics.Charts.Scatterplot(regr.Data, regr.Name));
                //}

                //Scatterplot generalized = new Scatterplot(lw_pool.TotalRegression.Data, "LW");
                //generalized.Properties.ShowTrend = true;
                //generalized.Properties.SelectedApproximationType = Mathematics.Statistics.Regression.RegressionType.Power;
                //generalized.BuildSeries();
                //chartForm.StatChart.AddSeries(generalized);

                //chartForm.StatChart.ShowLegend = true;
                //chartForm.StatChart.AxisYTitle = "Масса, мг";
                //chartForm.StatChart.AxisXTitle = "Длина, мм";
                //if (lw_pool.SeparateRegressions.Count < 13) chartForm.StatChart.SetSize(127.5F, 170);
                //else chartForm.StatChart.SetSize(255, 170);
                //chartForm.StatChart.Remaster();

                //generalized.Series.IsVisibleInLegend = false;
                //generalized.Trend.Series.IsVisibleInLegend = false;
                //generalized.Properties.DataPointColor = Color.Transparent;
                //generalized.Properties.TrendColor = Color.Red;

                //chartForm.StatChart.Update(sender, e);
                //Stream stream = new MemoryStream();
                //chartForm.StatChart.SaveImage(stream, ChartImageFormat.Jpeg);
                //Image image = Image.FromStream(stream);


                //report.AddImage(image, id);

                //id++;

                //chartForm.Close();

                //Mayfly.Mathematics.Charts.Plot plot = new Mathematics.Charts.Plot();

                //foreach (Regression regr in lw_pool.SeparateRegressions)
                //{                        
                //    plot.AddSeries(new Mayfly.Mathematics.Charts.Scatterplot(regr.Data, regr.Name));
                //}

                //plot.ShowOnChart(false, lw_pool.Name, 0, 0.1);
                //plot.Rebuild(sender, e);

            }
            else
            {
                report.AddParagraph(
                    "Регрессионные модели связи длины с массой тела личинок хирономид {0} не совпадают ({1}), что обосновывает использование специфичных моделей этих видов.",
                    list.Merge(), lw_pool.GetShortDescription());
            }

            DW:

            RegressionPool dw_pool = headsPool_dw(result, list);

            if (dw_pool == null) return;

            if (dw_pool.Coincidence == null) return;

            if (dw_pool.Coincidence.Probability >= coin)
            {
                report.AddParagraph(
                    "Регрессионные модели «ширина головной капсулы – масса» личинок хирономид {0} совпадают ({1}), что дает основание для построения единой для этих видов модели:",
                    list.Merge(), dw_pool.GetShortDescription());

                report.AddEquation(dw_pool.TotalRegression.GetFullEquation("W", "d", "N3"));

                report.AddParagraph(
                    "({0}).", dw_pool.TotalRegression.GetShortDescription());

                //ChartForm chartForm = new ChartForm();

                //foreach (Regression regr in dw_pool.SeparateRegressions)
                //{
                //    chartForm.StatChart.AddSeries(new Mayfly.Mathematics.Charts.Scatterplot(regr.Data, regr.Name));
                //}

                //Scatterplot generalized = new Scatterplot(dw_pool.TotalRegression.Data, "LW");
                //generalized.Properties.ShowTrend = true;
                //generalized.Properties.SelectedApproximationType = Mathematics.Statistics.Regression.RegressionType.Power;
                //generalized.BuildSeries();
                //chartForm.StatChart.AddSeries(generalized);

                //chartForm.StatChart.ShowLegend = true;
                //chartForm.StatChart.AxisYTitle = "Масса, мг";
                //chartForm.StatChart.AxisXTitle = "Ширина головной капсулы, мм";
                //if (dw_pool.SeparateRegressions.Count < 13) chartForm.StatChart.SetSize(127.5F, 170);
                //else chartForm.StatChart.SetSize(255, 170);
                //chartForm.StatChart.Remaster();
                //chartForm.StatChart.SetPrinterFriendly();

                //generalized.Series.IsVisibleInLegend = false;
                //generalized.Trend.Series.IsVisibleInLegend = false;
                //generalized.Properties.DataPointColor = Color.Transparent;
                //generalized.Properties.TrendColor = Color.Red;

                //chartForm.StatChart.Update(sender, e);

                //Stream stream = new MemoryStream();
                //chartForm.StatChart.SaveImage(stream, ChartImageFormat.Jpeg);
                //Image image = Image.FromStream(stream);


                //report.AddImage(image, id);

                //id++;

                //chartForm.Close();
            }
            else
            {
                report.AddParagraph(
                    "Регрессионные модели «ширина головной капсулы – масса» личинок хирономид {0} не совпадают ({1}), что обосновывает использование специфичных моделей этих видов.",
                    list.Merge(), dw_pool.GetShortDescription());
            }
        }

        private RegressionPool headsPool_lw(List<HeadAssess> result, string[] list)
        {
            List<Regression> lws = new List<Regression>();
            for (int i = 0; i < list.Length; i++)
            {
                HeadAssess hs = result.Find((h) => { return h.Species.Species.ToLower() == list[i].ToLower(); });
                if (hs != null) {
                    hs.TotalSample.lw.Name = list[i];
                    lws.Add(hs.TotalSample.lw); }
            }
            return new RegressionPool(lws, nameof(list));
        }

        private RegressionPool headsPool_dw(List<HeadAssess> result, string[] list)
        {
            List<Regression> dws = new List<Regression>();
            for (int i = 0; i < list.Length; i++)
            {
                HeadAssess hs = result.Find((h) => { return h.Species.Species.ToLower() == list[i].ToLower(); });
                if (hs != null && hs.TotalSample.dw != null) {
                    hs.TotalSample.dw.Name = list[i];
                    dws.Add(hs.TotalSample.dw); }
            }
            if (dws.Count < 2) return null;
            return new RegressionPool(dws, nameof(list));
        }

        private void ff(Report report, HeadAssess spc, string eqname)
        {
            if (spc.VystitysSample != null && spc.VolgaSample != null)
            {
                report.AddParagraph(
                    eqname + " для выборки озера Виштынецкого ({0}):",
                    spc.VystitysSample.lw.GetShortDescription());

                report.AddEquation(spc.VystitysSample.lw.GetFullEquation("W", "L", "N3"), ",");

                report.AddParagraph(
                    "для выборки бассейна Верхней Волги ({0}):",
                    spc.VolgaSample.lw.GetShortDescription());

                report.AddEquation(spc.VolgaSample.lw.GetFullEquation("W", "L", "N3"));

                report.AddParagraph("Обобщенная модель W(L) имеет вид");

                report.AddEquation(spc.TotalSample.lw.GetFullEquation("W", "L", "N3"), ",");

                report.AddParagraph(
                    "и обладает {0} ({1}).",
                    spc.TotalSample.lw.DeterminationStrength,
                    spc.TotalSample.lw.GetShortDescription());
            }
        }

        private void gg(Report report, HeadSample sample)
        {
            File.AppendAllText("out.txt", Environment.NewLine + "\t" + sample.Name + ":\t");

            if (sample.lw != null)
            {
                report.AddParagraph(
                    "Уравнение зависимости массы тела от длины для {0} (N = {1}) имеет вид:",
                    sample.Name, sample.lw.Data.Count);

                report.AddEquation(sample.lw.GetFullEquation("W", "L", "N3"));

                double p = sample.lw.fit.GoodnessOfFit.Probability;

                report.AddParagraph(
                    "и обладает {0} (R² = {1:N3}, " +
                    (p.ToString("N3") == (0).ToString("N3") ? "p << 0.001" : "p = " + p.ToString("N3")) + ").",
                    sample.lw.DeterminationStrength, sample.lw.Determination);

                File.AppendAllText("out.txt", "\tLW+");
            }

            if (sample.dw != null)
            {
                report.AddParagraph(
                    "Уравнение зависимости массы тела от ширины головной капсулы для выборки {0} (N = {1}) имеет вид:",
                    sample.Name, sample.dw.Data.Count);

                report.AddEquation(sample.dw.GetFullEquation("W", "d", "N3"));

                double p2 = sample.dw.fit.GoodnessOfFit.Probability;

                report.AddParagraph(
                    "и обладает {0} (R² = {1:N3}, " +
                    (p2.ToString("N3") == (0).ToString("N3") ? "p << 0.001" : "p = " + p2.ToString("N3")) + ").",
                    sample.dw.DeterminationStrength, sample.dw.Determination);

                File.AppendAllText("out.txt", "\tdW+");
            }
        }

        private void gg(Report report, string obj, List<Data.IndividualRow> wRows)
        {
            File.AppendAllText("out.txt", Environment.NewLine + "\t" + obj + ":\t");

            string l = "Длина, мм";
            string d = "Ширина головной капсулы, мм";
            string w = "Масса, мг";


            List<Data.IndividualRow> lRows = wRows.GetMeasuredRows(data.Individual.LengthColumn);
            BivariateSample lSample = new BivariateSample(l, w);
            foreach (Data.IndividualRow lRow in lRows)
            {
                lSample.Add(lRow.Length, lRow.Mass);
            }

            PowerRegression lw = new PowerRegression(lSample);

            if (lw != null)
            {
                report.AddParagraph(
                    "Уравнение зависимости массы тела от длины для {0} (N = {1}) имеет вид:",
                    obj, lSample.Count);

                report.AddEquation(lw.GetFullEquation("W", "L", "N3"));

                double p = lw.fit.GoodnessOfFit.Probability;

                report.AddParagraph(
                    "и обладает {0} (R² = {1:N3}, " +
                    (p.ToString("N3") == (0).ToString("N3") ? "p << 0.001" : "p = " + p.ToString("N3")) + ").",
                    lw.DeterminationStrength, lw.Determination);

                File.AppendAllText("out.txt", "\tLW+");
            }

            List<Data.IndividualRow> dRows = wRows.GetMeasuredRows(d);
            BivariateSample dSample = new BivariateSample(d, w);
            foreach (Data.IndividualRow dRow in dRows)
            {
                dSample.Add(data.GetIndividualValue(dRow, d), dRow.Mass);
            }

            PowerRegression dw = new PowerRegression(dSample);

            if (dw != null)
            {
                report.AddParagraph(
                    "Уравнение зависимости массы тела от ширины головной капсулы для выборки {0} (N = {1}) имеет вид:",
                    obj, dSample.Count);

                report.AddEquation(dw.GetFullEquation("W", "d", "N3"));

                double p2 = dw.fit.GoodnessOfFit.Probability;

                report.AddParagraph(
                    "и обладает {0} (R² = {1:N3}, " +
                    (p2.ToString("N3") == (0).ToString("N3") ? "p << 0.001" : "p = " + p2.ToString("N3")) + ").",
                    dw.DeterminationStrength, dw.Determination);

                File.AppendAllText("out.txt", "\tdW+");
            }
        }
    }
}
