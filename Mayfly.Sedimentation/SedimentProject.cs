using System.Resources;

namespace Mayfly.Sedimentation
{
    public partial class SedimentProject
    {
        partial class ZoneRow
        {
            public double Longitude { get { return this.To - this.From; } }

            //public double Square
            //{
            //    get
            //    {
            //        return this.ProjectRow.IsWidthNull() ? double.NaN :
            //            Longitude * this.ProjectRow.Width;
            //    }
            //}
        }

        partial class SectionRow
        {
            public double ConcentrationRatio(SectionRow sectionRow)
            {
                return ModelNgavt.GetDilution(
                    this.IsVelocityNull() ? ProjectRow.Velocity : this.Velocity,
                    this.IsDepthNull() ? ProjectRow.Depth : this.Depth,
                    ProjectRow.ControlHydraulicSize, this.Distance - sectionRow.Distance);
            }
        }

        partial class CompositionRow
        {
            public MineralFraction Fraction
            {
                get
                {
                    MineralFraction f = new MineralFraction(this.FractionFrom, this.FractionTo);
                    if (!this.IsSeparateNull()) f.Value = (this.ProjectRow.Weight * this.ProjectRow.Z / 100.0) * this.Separate;
                    return f;
                }
            }

            public double HydraulicSize
            {
                get
                {
                    return this.ProjectRow.IsTemperatureNull() ?
                        this.Fraction.GetHydraulicSize(15) :
                        this.Fraction.GetHydraulicSize(this.ProjectRow.Temperature);
                }
            }

            public double SedimentationLongitude
            {
                get
                {
                    return this.Fraction.GetSedimentationLongitude(this.ProjectRow.Depth,
                        this.ProjectRow.Velocity,
                        this.ProjectRow.IsTemperatureNull() ? 15 : this.ProjectRow.Temperature);
                }
            }

            public double Weight
            {
                get
                {
                    try
                    {
                        return this.Separate * this.ProjectRow.WeightFlushed;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }

            //public MineralComposition Sediments
            //{
            //    get
            //    {
            //        MineralComposition compos = new MineralComposition();

            //        double generalLength = 0;

            //        foreach (SedimentProject.CompositionRow row in this.ProjectRow.GetCompositionRows())
            //        {
            //            if (row == this) break;
            //            generalLength = row.SedimentationLongitude;
            //        }

            //        foreach (SedimentProject.CompositionRow row in this.ProjectRow.GetCompositionRows())
            //        {
            //            Fraction frac = row.Fraction.Copy();
            //            compos.Add(frac);

            //            if (this.SedimentationLongitude <= row.SedimentationLongitude)
            //            {
            //                frac.Value = row.Weight * ((this.SedimentationLongitude - generalLength) / row.SedimentationLongitude);
            //            }
            //            else
            //            {
            //                frac.Value = 0;
            //            }
            //        }

            //        return compos;
            //    }
            //}
        }


        partial class ProjectRow
        {
            public double ControlHydraulicSize
            {
                get
                {
                    return this.IsdNull() ? double.NaN : Service.HydraulicSize(this.d,
                        this.IsTemperatureNull() ? 15 : this.Temperature);
                }
            }

            public double ControlLoad
            {
                get
                {
                    return this.Load * (this.IsControlPartNull() ? double.NaN : this.ControlPart);
                }
            }




            public double WorkSquare
            {
                get
                {
                    return !this.IsWorkWidthNull() && !this.IsWorkLongitudeNull() ? this.WorkWidth * this.WorkLongitude : double.NaN;
                }
            }

            public double RightGap
            {
                get
                {
                    try { return this.Width - this.WorkWidth - this.LeftGap; } catch { return double.NaN; }
                }
            }


            public double Duration
            {
                get
                {
                    return !this.IsVolumeNull() && !this.IsPerformanceNull() ? this.Volume / this.Performance : double.NaN;
                }
            }

            public double DurationSeconds
            {
                get
                {
                    return this.Duration * 3600;
                }
            }

            public double PerformanceSecondly
            {
                get
                {
                    return this.IsPerformanceNull() ? double.NaN : this.Performance / 3600.0;
                }
            }

            public double WaterSpend
            {
                get
                {
                    return this.IsWorkWidthNull() || this.IsDepthNull() || this.IsVelocityNull() ?
                        double.NaN : (this.WorkWidth * this.Depth * this.Velocity);
                }
            }

            //public double WorkWaterSpend
            //{
            //    get
            //    {
            //        return this.IsWorkWidthNull() || this.IsDepthNull() || this.IsVelocityNull() ?
            //            double.NaN : (this.WorkWidth * this.Depth * this.Velocity);
            //    }
            //}

            public double Weight
            {
                get
                {
                    return this.IsVolumeNull() || this.IsDensityNull() ? double.NaN : this.Volume * this.Density;
                }
            }

            public double WeightFlushed
            {
                get
                {
                    return this.IsZNull() ? double.NaN : this.Weight * this.Z / 100.0;
                }
            }

            public double Load
            {
                get
                {
                    return this.IsDensityNull() || this.IsZNull() ? double.NaN :
                        ((this.PerformanceSecondly * this.Density * (this.Z / 100.0)) / this.WaterSpend) // tons per cubic meter
                        * 1000000; // convert to grams per cubic meter
                }
            }

            public double Obstruction
            {
                get
                {
                    return this.IsTurbulentWidthNull() || this.IsWidthNull() ?
                        double.NaN : this.TurbulentWidth / this.Width;
                }
            }

            public MineralComposition GetSedimentsComposition(double start, double end)
            {
                MineralComposition result = new MineralComposition();

                foreach (SedimentProject.CompositionRow fractionRow in this.GetCompositionRows())
                {
                    MineralFraction frac = fractionRow.Fraction.Copy();
                    result.Add(frac);

                    if (fractionRow.SedimentationLongitude > start)
                    {
                        double crossed = (end - start) / fractionRow.SedimentationLongitude;

                        if (fractionRow.SedimentationLongitude < end)
                        {
                            crossed = (fractionRow.SedimentationLongitude - start) / fractionRow.SedimentationLongitude;
                        }

                        frac.Value = fractionRow.Weight * crossed;
                    }
                    else
                    {
                        frac.Value = 0;
                    }
                }

                return result;
            }

            public double GetMaximalDistance()
            {
                double max = 0;

                foreach (CompositionRow row in this.GetCompositionRows())
                {
                    max = System.Math.Max(max, row.SedimentationLongitude);
                }

                return max;

                //double result = 0;

                //foreach (CompositionRow row in this.GetCompositionRows())
                //{
                //    if (row.SedimentationLongitude == max) continue;
                //    result = System.Math.Max(result, row.SedimentationLongitude);
                //}

                //return result;
            }

            public Report GetReport()
            {
                return this.GetReport(new ModelGgi(this), true, true, true);
            }

            public Report GetReport(ModelLoad model)
            {
                return this.GetReport(model, true, true, true);
            }

            public Report GetReport(ModelLoad model, bool methods, bool impact, bool criticals)
            {
                ResourceManager resources = new ResourceManager(typeof(WizardSed));

                Report report = new Report("Моделирование переноса и осаждения примесей в водной среде " +
                    "и распределения донных отложений для оценки воздействий на водные биоресурсы");

                Report.Table table1 = new Report.Table();
                table1.StartRow();
                table1.AddCellPrompt(resources.GetString("labelTitle.Text"), this.IsTitleNull() ? string.Empty : this.Title, 2);
                table1.EndRow();
                table1.StartRow();
                table1.AddCellPrompt("Начало реализации", this.IsStartNull() ? string.Empty : this.Start.ToString("D"));
                table1.AddCellPrompt("Окончание реализации", this.IsEndNull() ? string.Empty : this.Start.ToString("D"));
                table1.EndRow();
                table1.StartRow();
                table1.AddCellPrompt("Водный объект", this.IsWaterNull() ? string.Empty : this.Water, 2);
                table1.EndRow();
                table1.StartRow();
                table1.AddCellPrompt("Участок работ", this.IsStretchNull() ? string.Empty : this.Stretch);
                table1.AddCellPrompt("Вид грунтовых русловых работ", this.IsWorkTypeNull() ? string.Empty : this.WorkType);
                table1.EndRow();
                report.AddTable(table1);

                report.AddSubtitle("Материал и методы");

                report.AddSubtitle3("Исходные данные и их производные, принятые для моделирования");

                #region Initials

                report.AddParagraph("На основе предоставленных сведений о производстве " +
                    "(объем перемещаемого грунта, производительсноть механизма и объемной массы грунта) " +
                    "рассчитана продолжительность работ и общая масса перемещаемого грунта.");

                Report.Table table2 = new Report.Table(resources.GetString("pageWork.Text"));

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelVolume.Text"),
                    this.IsVolumeNull() ? string.Empty : this.Volume.ToString("N3"));
                table2.AddCellPrompt(resources.GetString("labelPerformance.Text"),
                    this.IsPerformanceNull() ? string.Empty : this.Performance.ToString("N3"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelPerformanceSecondly.Text"),
                    this.IsPerformanceNull() ? string.Empty : this.PerformanceSecondly.ToString("N3"));
                table2.AddCellPrompt(resources.GetString("labelDuration.Text"),
                    this.Duration.ToString("N3"));
                table2.EndRow();

                table2.StartRow();
                table2.AddCellPrompt(resources.GetString("labelDensity.Text"),
                    this.IsDensityNull() ? string.Empty : this.Density.ToString("N3"));
                table2.AddCellPrompt(resources.GetString("labelWeight.Text"),
                    this.IsVolumeNull() || this.IsDensityNull() ? string.Empty : this.Weight.ToString("N3"));
                table2.EndRow();

                report.AddTable(table2);

                report.AddParagraph("На основе предоставленных сведений о параметрах русла и условий среды " +
                    "рассчитан расход воды в русле.");

                Report.Table table3 = new Report.Table(resources.GetString("pageChannel.Text"));

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelWidth.Text"),
                    this.IsWidthNull() ? string.Empty : this.Width.ToString("N3"));
                table3.AddCellPrompt(resources.GetString("labelDepth.Text"),
                    this.IsDepthNull() ? string.Empty : this.Depth.ToString("N3"));
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelVelocity.Text"), this.IsVelocityNull() ? string.Empty : this.Velocity.ToString("N3"));
                table3.AddCellPrompt(resources.GetString("labelTemperature.Text"), this.IsTemperatureNull() ? string.Empty : this.Temperature.ToString("N3"));
                //table3.AddCellPrompt(resources.GetString("labelWaterSpend.Text"), this.IsWidthNull() || this.IsDepthNull() || this.IsVelocityNull() ? string.Empty : this.WaterSpend.ToString("N3"));
                table3.EndRow();

                table3.StartRow();
                table3.AddCellPrompt(resources.GetString("labelLoadNatural.Text"), this.IsLoadNaturalNull() ? string.Empty : this.LoadNatural.ToString("N3"), 2);
                table3.EndRow();

                report.AddTable(table3);

                Report.Table table31 = new Report.Table(resources.GetString("wizardPageWorkZone.Text"));

                table31.StartRow();
                table31.AddCellPrompt("Радиальное растекание", this.IsLateralFlowNull() ? "Не предусмотрено" : string.Format("Предусмотрено под углом {0}° ({1:N3} рад)", this.LateralFlow, this.LateralFlow / 360.0), 2);
                table31.EndRow();

                table31.StartRow();
                table31.AddCellPrompt(resources.GetString("labelWorkWidth.Text"), this.IsWorkWidthNull() ? string.Empty : this.WorkWidth.ToString("N3"));
                table31.AddCellPrompt(resources.GetString("labelWorkLongitude.Text"), this.IsWorkLongitudeNull() ? string.Empty : this.WorkLongitude.ToString("N3"));
                table31.EndRow();

                table31.StartRow();
                table31.AddCellPrompt(resources.GetString("labelWorkSquare.Text"), this.WorkSquare.ToString("N3"));
                table31.AddCellPrompt(resources.GetString("labelWaterSpend.Text"), this.WaterSpend.ToString("N3"));
                table31.EndRow();

                table31.StartRow();
                table31.AddCellPrompt(resources.GetString("labelLeftGap.Text"), this.IsLeftGapNull() ? string.Empty : this.LeftGap.ToString("N3"));
                table31.AddCellPrompt(resources.GetString("labelRightGap.Text"), this.RightGap.ToString("N3"));
                table31.EndRow();

                report.AddTable(table31);

                report.AddParagraph("С использованием коэффициента взмучивания " +
                    "рассчитана масса грунта, поступающая в поток и дополнительная мутность в створе работ.");

                Report.Table table4 = new Report.Table(resources.GetString("pageEntrainment.Text"));

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("labelZ.Text"), this.Z.ToString("N3"));
                table4.AddCellPrompt(resources.GetString("labelGgiFlushed.Text"), this.WeightFlushed.ToString("N3"));
                table4.EndRow();

                report.AddReference(References.Nomogram_Z);

                table4.StartRow();
                table4.AddCellPrompt(resources.GetString("labelGgiLoad.Text"), this.Load.ToString("N3"), 2);
                table4.EndRow();

                report.AddTable(table4);

                if (model is ModelNgavt)
                {
                    report.AddParagraph("В применяемой модели рассчитывается мутность частиц контрольного диаметра. " +
                        "Приняты следующие контрольный диаметр и доля частиц контрольного диаметра в изначальной смеси:");

                    Report.Table table5 = new Report.Table();

                    table5.StartRow();
                    table5.AddCellPrompt(resources.GetString("labelControlD.Text"), this.d);
                    table5.EndRow();

                    table5.StartRow();
                    table5.AddCellPrompt(resources.GetString("labelControlPart.Text"), this.ControlPart.ToString("P1"));
                    table5.EndRow();

                    report.AddTable(table5);
                }

                if (model is ModelGgi)
                {
                    if (this.GetCompositionRows().Length > 0) // Метод ГГИ
                    {
                        report.AddParagraph("На основе предоставленных сведений о гранулометрическом составе " +
                            "рассчитана масса отдельных фракций, поступающая в поток.");

                        Report.Table table6 = new Report.Table(resources.GetString("pageComposition.Text"));

                        table6.StartRow();
                        table6.AddHeaderCell("Фракция, мм");
                        table6.AddHeaderCell(resources.GetString("ColumnGrainSeparate.HeaderText"));
                        table6.AddHeaderCell(resources.GetString("ColumnGrainWeight.HeaderText"));
                        table6.EndRow();

                        foreach (SedimentProject.CompositionRow fractionRow in this.GetCompositionRows())
                        {
                            if (fractionRow.IsSeparateNull()) continue;

                            MineralFraction fraction = fractionRow.Fraction;
                            fraction.Value = fractionRow.IsSeparateNull() ? 0 : this.WeightFlushed * fractionRow.Separate;

                            table6.StartRow();
                            table6.AddCellValue(fraction);
                            table6.AddCellRight(fractionRow.Separate * 100, "N1");
                            table6.AddCellRight(fraction.Value, "N2");

                            table6.EndRow();
                        }

                        report.AddTable(table6);

                    }
                }

                #endregion

                if (methods)
                {
                    report.AddSubtitle3("Методы");

                    #region Methods

                    // Начальная масса и мутность

                    report.AddParagraph("Для применения модели переноса и осаждения " +
                        "была вычислена масса грунта, поступающего в поток (т) по формуле:");

                    report.AddEquation("{W_{ \\text {fl}}} = {V} \\times {\\rho} \\times {z} = {W} \\times {z}", ",");

                    report.AddParagraph("где V - {0};  ρ - {1};  z - {2}; W - {3}.",
                        resources.GetString("labelVolume.Text").ToLower(),
                        resources.GetString("labelDensity.Text").ToLower(),
                        resources.GetString("labelZ.Text").ToLower(),
                        resources.GetString("labelWeight.Text").ToLower());

                    report.AddParagraph("Коэффициент взмучивания z ");

                    report.AddParagraph("Дополнительная мутность в створе работ (г/м³) была рассчитана по формуле:");

                    report.AddEquation("{\\Delta P} = \\frac{{G} \\times {\\rho} \\times {z}}{q}", ",");

                    report.AddParagraph("где G - {0};  q - {1}.",
                        resources.GetString("labelPerformanceSecondly.Text").ToLower(),
                        resources.GetString("labelWaterSpend.Text").ToLower());

                    if (model is ModelNgavt)
                    {
                        #region Метод НГАВТ

                        report.AddParagraph("Дополнительную мутность (г/м³) на разном удалении от створа работ находили по формуле (Баула, 1994):");
                        report.AddEquation("\\frac{{\\mu}_{i}}{{\\mu}_{i - 1}} = {e}^{{-0.0023} \\times {\\Delta L_i} \\times {\\ln( \\frac{1.7}{v_i} )^{1.7}} \\times {\\ln( \\frac{12.5}{h_i} )^{0.5}} \\times {\\ln( \\frac{\\omega}{0.00018} )}}", ",");
                        report.AddParagraph("где ω - гидравлическая крупность частиц (м/с), вычисленная " +
                            "(исходя из контрольного диаметра, равного {0} мм) по следующей формуле (Караушев, 1977):",
                            this.d);

                        if (this.d < 0.1)
                        {
                            report.AddEquation("{\\omega} = {0.22} \\times { \\frac{g d^2}{4 {\\nu}}  } \\times \\frac{{\\rho}_S - {\\rho}}{\\rho}", ",");
                            report.AddParagraph("где g - ускорение свободного падения (м/с²); " +
                                    "d - размер частиц (м); " +
                                    "ρ - плотность воды (г/см³); " +
                                    "ρ<sub>S</sub> - плотность частиц (г/см³); " +
                                    "ν - вязкость воды, вычисленная по следующей формуле:");
                            report.AddEquation("{\\nu} = {0.00002414} \\times {10}^{ \\frac{247.8}{{t} + 273.15 - 140}}", ",");
                            report.AddParagraph("где t - температура воды, °C.");
                        }
                        else
                        {
                            if (this.d < (2 / UserSettings.SolidShape))
                            {
                                report.AddEquation("{\\omega} =  { ({1.6}{\\theta} - 0.16) } { (68 d - 0.003) } \\frac{{\\rho}_S - {\\rho}}{\\rho} k_T", ",");
                                report.AddParagraph("где Θ - коэффициент формы частиц; " +
                                    "ρ<sub>S</sub> - плотность частиц (г/см³); " +
                                    "ρ - плотность воды (г/см³); " +
                                    "g - ускорение свободного падения (м/с²); " +
                                    "d - размер частиц (м);" +
                                    "k<sub>T</sub> - поправочный температурный коэффициент (по: Караушев, 1977).");

                            }
                            else
                            {
                                report.AddEquation("{\\omega} = { ({2.4}{\\theta} - 0.7) } \\sqrt{ \\frac{{\\rho}_S - {\\rho}}{\\rho} g d }", ",");
                                report.AddParagraph("где Θ - коэффициент формы частиц; " +
                                    "ρ<sub>S</sub> - плотность частиц (г/см³); " +
                                    "ρ - плотность воды (г/см³); " +
                                    "g - ускорение свободного падения (м/с²); " +
                                    "d - размер частиц (м).");
                            }
                        }


                        #endregion
                    }

                    if (model is ModelGgi)
                    {
                        #region Метод ГГИ

                        #region Мутность

                        report.AddParagraph("Дополнительную мутность (г/м³) на разном удалении от створа работ находили по формуле:");

                        report.AddEquation("{\\mu} = \\frac{W_{ \\text {tr}} \\times 1000000}{{q} \\times {T \\times 3600}}", ",");

                        report.AddParagraph("где W<sub>tr</sub> - {0};  T - {1}.",
                            resources.GetString("ColumnSedWeightTransit.HeaderText").ToLower(),
                            resources.GetString("labelDuration.Text").ToLower());

                        report.AddParagraph("Масса транзитного грунта рассчитана как" +
                            " разница между массой грунта, поступившей в поток, и массой, осевшей выше расчетного створа:");

                        report.AddEquation("{W_{ \\text {tr}}} = {W_{ \\text {fl}}} - {W_{ \\text {zone (0-l)}}}", ",");

                        report.AddParagraph("Масса осевшего на рассчетном участке грунта определяли как суммарную массу осевших здесь фракций:");

                        report.AddEquation("{W_{ \\text {zone}}} = \\sum {W_{ \\text {fr, zone}}}", ",");

                        report.AddParagraph("Масса фракции, осевшей на участке, определяли по формуле (Добыча нерудных строительных ..., 2012):");

                        report.AddEquation("{W_{ \\text {fr, zone}}} = {W_{ \\text {fr}}} \\times \\frac{L_{ \\text {zone}}}{L_{ \\text {fr}}}", ",");

                        report.AddParagraph("где W<sub>fr</sub> - масса фракции, т;" +
                            " L<sub>zone</sub> - протяженность участка (либо той его части, которая находится в пределах зоны полного осаждения фракции), м;" +
                            " L<sub>fr</sub> - расстояние полного осаждений фракции, м.");

                        #endregion

                        #region Расстояние полного осаждения

                        report.AddParagraph("Расстояние полного осаждений фракции находили по формуле:");

                        report.AddEquation("{L_{ \\text {fr}}} = \\frac{{h} \\times v}{\\omega}", ",");

                        report.AddParagraph("где h - средняя глубина потока (м); " +
                            "v - скорость потока (м/с); " +
                            "ω - гидравлическая крупность частиц (м/с), вычисленная по следующим формулам (Караушев, 1977):");

                        report.AddParagraph("для турбулетного режима осаждения:");
                        report.AddEquation("{\\omega} = { ({2.4}{\\theta} - 0.7) } \\sqrt{ \\frac{{\\rho}_S - {\\rho}}{\\rho} g d }", ",");
                        report.AddParagraph("где Θ - коэффициент формы частиц; " +
                            "ρ<sub>S</sub> - плотность частиц (г/см³); " +
                            "ρ - плотность воды (г/см³); " +
                            "g - ускорение свободного падения (м/с²); " +
                            "d - размер частиц (м).");

                        report.AddParagraph("для ламинарного режима осаждения:");
                        report.AddEquation("{\\omega} = {0.22} \\times { \\frac{g d^2}{4 {\\nu}}  } \\times \\frac{{\\rho}_S - {\\rho}}{\\rho}", ",");
                        report.AddParagraph("где ν - вязкость воды, вычисленная по следующей формуле:");
                        report.AddEquation("{\\nu} = {0.00002414} \\times {10}^{ \\frac{247.8}{{t} + 273.15 - 140}}", ",");
                        report.AddParagraph("где t - температура воды, °C.");

                        report.AddParagraph("для переходного режима осаждения:");
                        report.AddEquation("{\\omega} =  { ({1.6}{\\theta} - 0.16) } { (68 d - 0.003) } \\frac{{\\rho}_S - {\\rho}}{\\rho} k_T", ",");
                        report.AddParagraph("где k<sub>T</sub> - поправочный температурный коэффициент (по: Караушев, 1977).");

                        report.AddParagraph("Режим осаждения применяли исходя из " +
                            (UserSettings.SizeSource == GrainSizeType.Lower ? "нижней границы" :
                            (UserSettings.SizeSource == GrainSizeType.Moda ? "моды" : "верхней границы")) +
                            " фракции и коэффициента формы частиц: для частиц менее 0,1 мм применяли ламинарную модель осаждения, " +
                            "для частиц крупнее 0,1 мм при размере частиц менее, чем отношение 2 к коэффициенту формы, применяли переходную модель осаждения, " +
                            " для более крупных частиц применяли турбулентную модель осаждения.");

                        #endregion

                        #region Площадь

                        report.AddParagraph("Площадь зон для расчета осаждения расчитывали следующим образом:");

                        if (this.IsLateralFlowNull())
                        {
                            report.AddEquation("{s} = {l} \\times {b}", ",");
                            report.AddParagraph("где l - протяженность участка, м; b - ширина участка, м.");
                        }
                        else
                        {
                            report.AddEquation("{s} = s_{\\text{пр}} + 2 \\times s_{\\text{лат}}", ",");
                            report.AddParagraph("где s<sub>пр</sub> - площадь зоны непосредственно ниже по течению от участка работ; s<sub>лат</sub> - площадь зоны, покрываемой радиальным растеканием взвеси.");

                            report.AddEquation("{s}_{\\text{пр}} = {l} \\times {b}", ",");
                            report.AddParagraph("где l - протяженность зоны, м; b - ширина участка, м.");

                            report.AddEquation("{s}_{\\text{лат}} = {l_{ \\text{ниж} }}^{2} \\times {\\alpha} - {l_{\\text {вер} }}^{2} \\times {\\alpha}", ",");
                            report.AddParagraph("где l<sub>вер</sub> - протяженность от нижней границы участка работ до верхней границы зоны, м; l<sub>ниж</sub> - протяженность от нижней границы участка работ до нижней границы зоны, м; w - ширина участка работ, м; α - угол латерального растекания, рад.");

                            report.AddParagraph("При достижении зоны латериального растекания взвеси береговой линии площадь зоны такого растекания уменьшается на значение:");
                            report.AddEquation("{s}_{\\text{искл}} = \\bigg( \\frac{ l_{\\text{ниж}} \\times {sin{\\alpha}} - x}{sin{\\alpha}} \\bigg)^2 \\times {\\alpha}", ",");
                            report.AddParagraph("где x - расстояние от участка работ до берега, м.");

                            report.AddParagraph("При достижении зоны латериального растекания взвеси береговой линии выше зоны площадь зоны уменьшается дополнительно на значение:");
                            report.AddEquation("{s}_{\\text{искл}} = \\bigg( \\frac{ l_{\\text{вер}} \\times {sin{\\alpha}} - x}{sin{\\alpha}}\\bigg)^2 \\times {\\alpha}", ".");
                        }


                        #endregion

                        #region Наилок

                        report.AddParagraph("Толщину наилка (мм) на участке водного объекта находили следующим образом:");

                        report.AddEquation("{\\delta} = \\frac{V \\times 1000}{s}", ",");

                        report.AddParagraph("где V - {0}; s - {1}.",
                            resources.GetString("ColumnSedVolume.HeaderText").ToLower(),
                            resources.GetString("ColumnSedSquare.HeaderText").ToLower() +
                            ". Объем заиления (м³) находили по формуле:");

                        report.AddEquation("{V} = \\frac {W_{ \\text {zone}}}{{\\rho}_{ \\text {zone}}}", ",");

                        report.AddParagraph("где W<sub>zone</sub> - {0}; ρ - {1} (по: Петухова, 1966).",
                            resources.GetString("ColumnSedWeight.HeaderText").ToLower(),
                            resources.GetString("ColumnSedDensity.HeaderText").ToLower());

                        #endregion

                        #endregion
                    }

                    #endregion
                }

                report.AddSubtitle("Результаты");

                if (impact)
                {
                    report.AddSubtitle3(resources.GetString("checkBoxImpact.Text"));

                    if (model is ModelNgavt)
                    {
                        #region НГАВТ

                        report.AddParagraph("Начальная концентрация контрольной примеси определяется как" +
                            " доля частиц контролирующего диаметра от суммарной дополнительной мутности в своре работ.");

                        Report.Table table7 = new Report.Table();

                        table7.StartRow();
                        table7.AddCellPrompt(resources.GetString("labelControlOmega.Text"), this.ControlHydraulicSize.ToString("N3"));
                        table7.EndRow();

                        table7.StartRow();
                        table7.AddCellPrompt(resources.GetString("labelControlLoad.Text"), this.ControlLoad.ToString("N3"));
                        table7.EndRow();

                        report.AddTable(table7);

                        #region Sections

                        if (this.GetSectionRows().Length > 0)
                        {
                            Report.Table table8 = new Report.Table("Створы и мутность");

                            table8.StartRow();
                            table8.AddHeaderCell(resources.GetString("ColumnSectionDistance.HeaderText"));
                            table8.AddHeaderCell(resources.GetString("ColumnSectionVelocity.HeaderText"));
                            table8.AddHeaderCell(resources.GetString("ColumnSectionDepth.HeaderText"));
                            table8.AddHeaderCell(resources.GetString("ColumnSectionRatio.HeaderText"));
                            table8.AddHeaderCell(resources.GetString("ColumnSectionLoad.HeaderText"));
                            table8.EndRow();

                            double load = this.ControlLoad;
                            double l = 0;

                            foreach (SedimentProject.SectionRow row in this.GetSectionRows())
                            {
                                table8.StartRow();

                                table8.AddCellRight(row.Distance, "N1");

                                if (row.IsVelocityNull()) table8.AddCellRight(this.Velocity, "N3");
                                else table8.AddCellRight(row.Velocity, "N3");

                                if (row.IsDepthNull()) table8.AddCellRight(this.Depth, "N3");
                                else table8.AddCellRight(row.Depth, "N3");

                                double ratio = ModelNgavt.GetDilution(this.Velocity, this.Depth,
                                    this.ControlHydraulicSize, row.Distance - l);

                                table8.AddCellRight(-(1 - ratio), "N3");
                                table8.AddCellRight(ratio * load, "N3");

                                table8.EndRow();

                                l = row.Distance;
                                load *= ratio;
                            }

                            report.AddTable(table8);
                        }

                        #endregion

                        #endregion
                    }

                    if (model is ModelGgi && (this.GetCompositionRows().Length > 0))
                    {
                        #region ГГИ

                        #region Zones

                        Report.Table table9 = new Report.Table(resources.GetString("pageZone.Text"));

                        table9.StartRow();
                        table9.AddHeaderCell("Фракция, мм");
                        table9.AddHeaderCell(resources.GetString("ColumnZoneSize.HeaderText"));
                        table9.AddHeaderCell(resources.GetString("ColumnZoneKT.HeaderText"));
                        table9.AddHeaderCell(resources.GetString("ColumnZoneHydraulicSize.HeaderText"));
                        table9.AddHeaderCell(resources.GetString("ColumnZoneLongitude.HeaderText"));
                        table9.EndRow();

                        foreach (SedimentProject.CompositionRow fractionRow in this.GetCompositionRows())
                        {
                            MineralFraction frac = fractionRow.Fraction;

                            table9.StartRow();
                            table9.AddCellValue(frac);
                            table9.AddCellRight(frac.GrainSize);
                            table9.AddCellRight(this.IsTemperatureNull() ? 1 :
                                Service.TemperatureCorrectionFactor(frac.GrainSize, this.Temperature),
                                "N2");
                            table9.AddCellRight(fractionRow.HydraulicSize, "N3");
                            table9.AddCellRight(fractionRow.SedimentationLongitude, "N1");

                            table9.EndRow();
                        }

                        report.AddTable(table9);

                        #endregion

                        #region Sedimentation

                        if (model.Stretches.Count > 0)
                        {

                            report.AddParagraph("На основе сведений о массе отдельных фракций и температуре, при которой выполняются работы, " +
                                "рассчитана протяженность зон полного осаждения фракций.");

                            Report.Table table10 = new Report.Table("Расчет массы и состава осаждений");

                            table10.StartRow();
                            table10.AddHeaderCell("Участок осаждения", .2);
                            table10.AddHeaderCell(resources.GetString("ColumnSedLength.HeaderText"));

                            foreach (SedimentProject.CompositionRow fractionRow in this.GetCompositionRows())
                            {
                                table10.AddHeaderCell(fractionRow.Fraction.ToString());
                            }

                            table10.AddHeaderCell(resources.GetString("ColumnSedWeight.HeaderText"));

                            table10.EndRow();

                            foreach (ModelStretch stretch in model.Stretches)
                            {
                                if (stretch.Weight == 0) continue;

                                table10.StartRow();
                                table10.AddCellValue(stretch.Name);
                                table10.AddCellRight(stretch.Longitude, "N1");

                                foreach (MineralFraction frac in stretch.Sediments)
                                {
                                    if (frac.Value > 0) table10.AddCellRight(frac.Value, "N3");
                                    else table10.AddCell();
                                }

                                if (stretch.Weight > 0) table10.AddCellRight(stretch.Weight, "N3");
                                else table10.AddCell();

                                table10.EndRow();
                            }

                            report.AddTable(table10);




                            Report.Table table11 = new Report.Table("Расчет дополнительной мутности");

                            table11.StartRow();
                            table11.AddHeaderCell("Участок осаждения", .2);
                            table11.AddHeaderCell(resources.GetString("ColumnSedSquare.HeaderText"));
                            table11.AddHeaderCell(resources.GetString("ColumnLoadVolume.HeaderText"));
                            table11.AddHeaderCell(resources.GetString("ColumnSedWeight.HeaderText"));
                            //report.AddHeaderCell(resources.GetString("ColumnSedWeightIntegral.HeaderText"));
                            table11.AddHeaderCell(resources.GetString("ColumnSedWeightTransit.HeaderText"));
                            table11.AddHeaderCell(resources.GetString("ColumnSedLoad.HeaderText"));

                            table11.EndRow();

                            foreach (ModelStretch stretch in model.Stretches)
                            {
                                if (stretch.Weight == 0) continue;

                                table11.StartRow();
                                table11.AddCellValue(stretch.Name);

                                table11.AddCellRight(stretch.Square, "N3");
                                table11.AddCellRight(stretch.WaterVolume, "N3");

                                if (stretch.Weight > 0) table11.AddCellRight(
                                    stretch.Weight, "N3");
                                else table11.AddCell();

                                //if (stretch.WeightCumulate > 0) table6.AddCellRight(
                                //    stretch.WeightCumulate, ColumnSedWeightIntegral.DefaultCellStyle.Format);
                                //else table6.AddCell();

                                if (stretch.TransitWeight > 0) table11.AddCellRight(
                                    stretch.TransitWeight, "N3");
                                else table11.AddCell();

                                if (stretch.FinalAdditionalLoad > 0) table11.AddCellRight(
                                    stretch.FinalAdditionalLoad, "N1");
                                else table11.AddCell();

                                table11.EndRow();
                            }

                            report.AddTable(table11);


                            Report.Table table12 = new Report.Table("Расчет плотности и объема осаждений");

                            table12.StartRow();
                            table12.AddHeaderCell("Участок осаждения", .2);
                            table12.AddHeaderCell(resources.GetString("ColumnSedSquare.HeaderText"));
                            table12.AddHeaderCell(resources.GetString("ColumnSedWeight.HeaderText"));
                            table12.AddHeaderCell(resources.GetString("ColumnSedDensity.HeaderText"));
                            table12.AddHeaderCell(resources.GetString("ColumnSedVolume.HeaderText"));
                            table12.AddHeaderCell(resources.GetString("ColumnSedWidth.HeaderText"));

                            table12.EndRow();

                            foreach (ModelStretch stretch in model.Stretches)
                            {
                                if (stretch.Weight == 0) continue;

                                table12.StartRow();
                                table12.AddCellValue(stretch.Name);

                                table12.AddCellRight(stretch.Square, "N3");

                                if (stretch.Weight > 0) table12.AddCellRight(
                                    stretch.Weight, "N3");
                                else table12.AddCell();

                                if (stretch.SedimentsDensity > 0) table12.AddCellRight(
                                    stretch.SedimentsDensity, "N3");
                                else table12.AddCell();

                                if (stretch.SedimentsVolume > 0) table12.AddCellRight(
                                    stretch.SedimentsVolume, "N3");
                                else table12.AddCell();

                                if (stretch.SedimentsMeanWidth > 0) table12.AddCellRight(
                                    stretch.SedimentsMeanWidth, "N1");
                                else table12.AddCell();

                                table12.EndRow();
                            }

                            report.AddTable(table12);
                        }

                        #endregion

                        report.AddReference(References.Petukhova_1966);

                        #endregion
                    }

                    report.AddReference(model.Reference);
                    report.AddReference(References.Karaushev_1977);
                }

                if (criticals && this.GetCriticalLoadRows().Length > 0)
                {
                    report.AddSubtitle3(resources.GetString("checkBoxCriticals.Text"));

                    #region Criticals

                    Report.Table table13 = new Report.Table(resources.GetString("pageLoad.Text"));

                    table13.StartRow();
                    table13.AddHeaderCell("Зона мутности, г/м³", .2);
                    table13.AddHeaderCell(resources.GetString("ColumnLoadLongitude.HeaderText"));
                    table13.AddHeaderCell(resources.GetString("ColumnLoadSquare.HeaderText"));
                    table13.AddHeaderCell(resources.GetString("ColumnLoadVolume.HeaderText"));
                    table13.AddHeaderCell(resources.GetString("ColumnLoadWaterVolume.HeaderText"));
                    table13.EndRow();

                    foreach (SedimentProject.CriticalLoadRow loadRow in this.GetCriticalLoadRows())
                    {
                        table13.StartRow();
                        table13.AddCellValue(string.Format("{0:N1} — {1:N1}", loadRow.From, loadRow.To));

                        ModelStretch stretch = new ModelStretch(0, 1, this);

                        if (model != null)
                        {
                            stretch = model.GetByLoad(loadRow.From, loadRow.To);
                        }

                        if (model != null)
                        {
                            stretch = model.GetByLoad(loadRow.From, loadRow.To);
                        }

                        table13.AddCellRight(stretch.Longitude, "N1");
                        table13.AddCellRight(stretch.Square, "N1");
                        table13.AddCellRight(stretch.WaterVolume, "N1");
                        table13.AddCellRight(stretch.TransitWaterVolume, "N1");

                        table13.EndRow();
                    }

                    report.AddTable(table13);

                    if (model is ModelGgi && this.GetCriticalSiltRows().Length > 0)
                    {
                        Report.Table table14 = new Report.Table(resources.GetString("pageSilt.Text"));

                        table14.StartRow();
                        table14.AddHeaderCell("Толщина наиления, мм", .2);
                        table14.AddHeaderCell(resources.GetString("ColumnSiltLongitude.HeaderText"));
                        table14.AddHeaderCell(resources.GetString("ColumnSiltSquare.HeaderText"));
                        table14.EndRow();

                        foreach (SedimentProject.CriticalSiltRow siltRow in this.GetCriticalSiltRows())
                        {
                            table14.StartRow();
                            table14.AddCellValue(string.Format("{0:N1} — {1:N1}", siltRow.From, siltRow.To));
                            ModelStretch stretch = ((ModelGgi)model).GetBySilt(siltRow.From, siltRow.To);
                            table14.AddCellRight(stretch.Longitude, "N1");
                            table14.AddCellRight(stretch.Square, "N1");
                            table14.EndRow();
                        }

                        report.AddTable(table14);
                    }

                    #endregion
                }

                report.End("Расчет выполнен в программе для ЭВМ \"Моделирование переноса и осаждения минеральной взвеси в водной среде для оценки воздействия на водные биологические ресурсы\".");

                return report;
            }
        }
    }
}