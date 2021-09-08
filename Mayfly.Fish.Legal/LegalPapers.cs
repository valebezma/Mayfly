using Mayfly;
using Mayfly.Extensions;
using System;

namespace Mayfly.Fish.Legal
{
    public partial class LegalPapers
    {
        partial class LegalNoteDataTable
        {
            internal int GetNextNoteNumber(LegalNoteType type)
            {
                int result = 0;

                foreach (LegalNoteRow noteRow in this)
                {
                    if (noteRow.IsNoNull()) continue;
                    if (noteRow.Content != (int)type) continue;

                    result = Math.Max(result, noteRow.No);
                }

                return result + 1;
            }
        }

        partial class SpeciesDataTable
        {
            public SpeciesRow FindBySpecies(string value)
            {
                foreach (SpeciesRow speciesRow in Rows)
                {
                    if (speciesRow.IsNull(columnSpecies))
                    {
                        continue;
                    }

                    if (speciesRow.Species == value)
                    {
                        return speciesRow;
                    }
                }
                return null;
            }
        }

        partial class LegalNoteRow
        {
            public string Requisites
            {
                get
                {
                    return string.Format("N{0} от {1:D}", this.No, this.Date);
                }
            }

            public int SpeciesInCatch
            {
                get
                {
                    return this.Content == 0 ?
                        GetCatchRows().Length :
                        CorrespondingCatchNote.GetCatchRows().Length;
                }
            }

            public decimal Mass
            {
                get
                {
                    if (this.Content == 0)
                    {
                        decimal result = 0;

                        foreach (CatchRow catchRow in this.GetCatchRows())
                        {
                            if (catchRow.IsMassNull()) continue;

                            result += catchRow.Mass;
                        }

                        return result;
                    }
                    else
                    {
                        if (this.IsSecondNull())
                        {
                            return 0;
                        }
                        else
                        {
                            return tableLegalNote.FindByID(this.Second).Mass;
                        }
                    }
                }
            }

            public LegalNoteRow CorrespondingCatchNote
            {
                get
                {
                    if (this.Content == 0) return null;
                    if (this.IsSecondNull()) return null;

                    return tableLegalNote.FindByID(this.Second);
                }
            }

            public LegalNote GetLegalNoteReport()
            {
                string header = "Акт ";

                switch ((LegalNoteType)this.Content)
                {
                    case LegalNoteType.Catch:
                        header += "о проведении контрольного лова водных<br/>биологических ресурсов (ВБР)";
                        break;

                    case LegalNoteType.Transport:
                        header += "отбора биологических образцов и живых особей водных биологических ресурсов, добытых (выловленных) при осуществлении рыболовства  в научно-исследовательских и контрольных целях, для транспортировки в научные организации для продолжения работ";
                        break;

                    case LegalNoteType.Release:
                        header += "возвращения водных биологических ресурсов, добытых (выловленных) при осуществлении рыболовства в научно-исследовательских и контрольных целях, в среду их обитания";
                        break;

                    case LegalNoteType.Utilization:
                        header += "уничтожения водных биологических ресурсов, добытых (выловленных) при осуществлении рыболовства в научно-исследовательских и контрольных целях";
                        break;
                }

                LegalNote result = new LegalNote(header);

                Report.Table table = new Report.Table();

                table.StartRow();
                table.AddCell(this.Date.ToString("d"), Legal.LegalNote.InputClass | ReportCellClass.Centered);
                table.AddCell(string.Empty);
                table.AddCell("№ " + this.No, Legal.LegalNote.InputClass | ReportCellClass.Centered);
                table.EndRow();

                table.StartRow();
                table.AddCellComment("дата составления");
                table.AddCell(string.Empty);
                table.AddCell(string.Empty);
                table.EndRow();

                result.AddTable(table);

                result.AddTable(
                    ReportExtensions.GetAskBlock("Настоящий акт составлен",
                        this.LicenseRow.ExecutiveFormal,
                        "Ф.И.О. и должность ответственного лица научной организации"));

                result.AddTable(
                    ReportExtensions.GetAskBlock("в присутствии*",
                        new string[] {
                            this.Bystander1,
                            this.Bystander2
                        }.Merge(),
                        "Ф.И.О., должность присутствующих лиц"));

                result.AddTable(ReportExtensions.GetAskBlock("на",
                    this.Place,
                    "место составления акта: название и регистрационный номер судна, рыбохозяйственная зона (подзона), географические координаты или водный объект рыбохозяйственного значения"));

                result.AddTable(ReportExtensions.GetAskBlock("В соответствии с программой выполнения работ при осуществлении рыболовства в научно-исследовательских и контрольных целях (далее – Программа):",
                    this.LicenseRow.Program,
                    "полное название программы, в соответствии с которой проводятся научные исследования"));

                result.AddTable(ReportExtensions.GetAskBlock("при осуществлении",
                    this.Gear,
                    "орудия и способ добычи (вылова) водных биологических ресурсов"));

                result.AddTable(ReportExtensions.GetAskBlock("в",
                    this.Water,
                    "рыбохозяйственная зона (подзона), географические координаты или водный объект рыбохозяйственного значения"));

                result.AddTable(ReportExtensions.GetAskBlock("по разрешению",
                    this.LicenseRow.Requisites,
                    "номер разрешения, число, месяц, год выдачи"));

                result.AddTable(ReportExtensions.GetAskBlock("сотрудниками",
                    this.LicenseRow.Holder,
                    "полное наименование научной организации"));
                double width_no = .05;
                double width_mass = .15;


                int index;

                decimal w;
                switch ((LegalNoteType)this.Content)
                {
                    case LegalNoteType.Catch:

                        #region Catch note

                        result.AddParagraph("были добыты (выловлены):");

                        Report.Table table1 = new Report.Table();

                        table1.AddHeader(new string[] {
                            Resources.NoteContent.CaptionNo,
                            Resources.NoteContent.CaptionSpecies,
                            Resources.NoteContent.CaptionMass },
                            new double[] { width_no, 0, width_mass });

                        int q = 0;
                        w = 0;
                        index = 0;

                        foreach (CatchRow catchRow in this.GetCatchRows())
                        {
                            q += catchRow.Quantity;
                            w += catchRow.Mass;

                            table1.StartRow();
                            table1.AddCell(index + 1, Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table1.AddCell(catchRow.SpeciesRow, Legal.LegalNote.InputClass);
                            table1.AddCell(catchRow.Mass.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table1.EndRow();

                            index++;
                        }

                        table1.StartRow();
                        table1.AddCell(Resources.NoteContent.Total, 2, CellSpan.Columns);
                        table1.AddCell(w.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                        table1.EndRow();

                        result.AddTable(table1);

                        LegalNoteRow secondNote = this.tableLegalNote.FindByID(this.Second);

                        switch ((LegalNoteType)secondNote.Content)
                        {
                            case LegalNoteType.Release:
                                result.AddReferenceNotes("ВБР выпущены в водоемы в количестве", q.ToString(), "шт. (или", w.ToString("n1"), "кг)");
                                result.AddReferenceNotes("(акт возвращения ВБР в среду обитания №", secondNote.No.ToString(), "от", secondNote.Date.ToShortDateString(), ")");

                                result.AddReferenceNotes("ВБР уничтожены в количестве", string.Empty, "шт. (или", string.Empty, "кг)");
                                result.AddReferenceNotes("(акт уничтожения №", string.Empty, "от", string.Empty, ")");

                                result.AddReferenceNotes("ВБР отобраны для транспортировки в количестве", string.Empty, "шт. (или", string.Empty, "кг)");
                                result.AddReferenceNotes("(акт отбора биологических образцов №", string.Empty, "от", string.Empty, ")");

                                break;

                            case LegalNoteType.Utilization:
                                result.AddReferenceNotes("ВБР выпущены в водоемы в количестве", string.Empty, "шт. (или", string.Empty, "кг)");
                                result.AddReferenceNotes("(акт возвращения ВБР в среду обитания №", string.Empty, "от", string.Empty, ")");

                                result.AddReferenceNotes("ВБР уничтожены в количестве", q.ToString(), "шт. (или", w.ToString("n1"), "кг)");
                                result.AddReferenceNotes("(акт уничтожения №", secondNote.No.ToString(), "от", secondNote.Date.ToShortDateString(), ")");

                                result.AddReferenceNotes("ВБР отобраны для транспортировки в количестве", string.Empty, "шт. (или", string.Empty, "кг)");
                                result.AddReferenceNotes("(акт отбора биологических образцов №", string.Empty, "от", string.Empty, ")");
                                break;

                            case LegalNoteType.Transport:
                                result.AddReferenceNotes("ВБР выпущены в водоемы в количестве", string.Empty, "шт. (или", string.Empty, "кг)");
                                result.AddReferenceNotes("(акт возвращения ВБР в среду обитания №", string.Empty, "от", string.Empty, ")");

                                result.AddReferenceNotes("ВБР уничтожены в количестве", string.Empty, "шт. (или", string.Empty, "кг)");
                                result.AddReferenceNotes("(акт уничтожения №", string.Empty, "от", string.Empty, ")");

                                result.AddReferenceNotes("ВБР отобраны для транспортировки в количестве", q.ToString(), "шт. (или", w.ToString("n1"), "кг)");
                                result.AddReferenceNotes("(акт отбора биологических образцов №", secondNote.No.ToString(), "от", secondNote.Date.ToShortDateString(), ")");
                                break;
                        }

                        #endregion

                        break;

                    case LegalNoteType.Transport:

                        #region Transport note

                        result.AddParagraph("(далее - научная организация) согласно рейдовому заданию для проведения исследований " +
                            "в научной организации были отобраны следующие биологические образцы и (или) виды водных " +
                            "биологических ресурсов для транспортировки с целью продолжения работ в соответствии с Программой:");

                        Report.Table table7 = new Report.Table();

                        table7.AddHeader(new string[]
                        {
                            Resources.NoteContent.CaptionNo,
                            "Биологические образцы, полученные от водных биологических ресурсов, виды водных биологических ресурсов",
                            "Способ фиксации (консервации)",
                            Resources.NoteContent.CaptionMass,
                            "Условия транспортировки, вид тары, её количество"
                        }, new double[] { width_no, 0, 0, width_mass, 0 });

                        w = 0;
                        index = 0;

                        foreach (CatchRow catchRow in this.CorrespondingCatchNote.GetCatchRows())
                        {
                            w += catchRow.Mass;

                            table7.StartRow();
                            table7.AddCell(index + 1, Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table7.AddCell(catchRow.SpeciesRow.Local, Legal.LegalNote.InputClass);
                            if (index == 0) { table7.AddCell(this.Conservant, Legal.LegalNote.InputClass | ReportCellClass.Centered, this.SpeciesInCatch, CellSpan.Rows); }
                            table7.AddCell(catchRow.Mass.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                            if (index == 0) { table7.AddCell(this.Dish, Legal.LegalNote.InputClass | ReportCellClass.Centered, this.SpeciesInCatch, CellSpan.Rows); }
                            table7.EndRow();

                            index++;
                        }

                        table7.StartRow();
                        table7.AddCell(Resources.NoteContent.Total, 3, CellSpan.Columns);
                        table7.AddCell(w.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                        table7.AddCell();
                        table7.EndRow();

                        result.AddTable(table7);

                        result.AddTable(ReportExtensions.GetAskBlock("Указанные биологические образцы и (или) виды водных биологических ресурсов будут транспортироваться в:",
                            this.Addressee + ", " + this.Destination, "конечный грузополучатель: полное название научной организации, адрес доставки"));

                        result.AddTable(ReportExtensions.GetAskBlock("Ответственный за транспортировку биологических образцов (или живых особей):",
                            this.Expeditor + ", " + this.ExpeditorRequisites, "ФИО, должность, наименование и сведения документа, удостоверяющего личность (серия, номер, кем и когда выдан)"));

                        #endregion

                        break;

                    case LegalNoteType.Release:

                        #region Release note

                        result.AddParagraph("были добыты  (выловлены) при осуществлении рыболовства в научно-исследовательских и контрольных целях и вовращены в среду их обитания следующие виды водных биологических ресурсов:");

                        Report.Table table2 = new Report.Table();

                        table2.AddHeader(new string[] {
                            Resources.NoteContent.CaptionNo,
                            Resources.NoteContent.CaptionSpecies,
                            "Дата и время возвращения водных биологических ресурсов в среду их обитания",
                            "Место возвращения водных биологических ресурсов<small>(рыбохозяйственная зона (подзона), географические координаты или водный объект рыбохозяйственного значения)</small>",
                            Resources.NoteContent.CaptionMass
                        }, new double[] { width_no, 0, 0, 0, width_mass });

                        w = 0;
                        index = 0;

                        foreach (CatchRow catchRow in this.CorrespondingCatchNote.GetCatchRows())
                        {
                            w += catchRow.Mass;

                            table2.StartRow();
                            table2.AddCell(index + 1, Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table2.AddCell(catchRow.SpeciesRow.Local, Legal.LegalNote.InputClass);
                            if (index == 0)
                            {
                                table2.AddCell(this.Date.ToString("d"), Legal.LegalNote.InputClass | ReportCellClass.Centered, this.SpeciesInCatch, CellSpan.Rows);
                                table2.AddCell(this.Water, Legal.LegalNote.InputClass | ReportCellClass.Centered, this.SpeciesInCatch, CellSpan.Rows);
                            }
                            table2.AddCell(catchRow.Mass.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table2.EndRow();

                            index++;
                        }

                        table2.StartRow();
                        table2.AddCell(Resources.NoteContent.Total, 4, CellSpan.Columns);
                        table2.AddCell(w, Legal.LegalNote.InputClass | ReportCellClass.Right);
                        table2.EndRow();

                        result.AddTable(table2);

                        #endregion

                        break;

                    case LegalNoteType.Utilization:

                        #region Utilization note

                        result.AddParagraph("были  добыты  (выловлены)  и  уничтожены   с   соблюдением   обязательных " +
                            "требований нормативных и  технических  документов  по  охране  окружающей " +
                            "среды,  следующие  виды  водных  биологических  ресурсов  (если  иное  не " +
                            "предусмотрено международными договорами Российской Федерации):");

                        Report.Table table3 = new Report.Table();

                        table3.AddHeader(new string[]
                        {
                            Resources.NoteContent.CaptionNo,
                            Resources.NoteContent.CaptionSpecies,
                            "Технический способ уничтожения с указанием применяемого оборудования",
                            "Место уничтожения водных биологических ресурсов<br /><small>(рыбохозяйственная зона (подзона), географические координаты или водный объект рыбохозяйственного значения)</small>",
                            "Дата и время уничтожения водных биоресурсов",
                            Resources.NoteContent.CaptionMass
                        }, new double[] { width_no, 0, 0, 0, 0, width_mass });

                        w = 0;
                        index = 0;

                        foreach (CatchRow catchRow in this.CorrespondingCatchNote.GetCatchRows())
                        {
                            w += catchRow.Mass;

                            table3.StartRow();
                            table3.AddCell(index + 1, Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table3.AddCell(catchRow.SpeciesRow.Local, Legal.LegalNote.InputClass);

                            if (index == 0)
                            {
                                table3.AddCell(this.Utilization == 0 ? this.Technic : string.Empty, Legal.LegalNote.InputClass, this.SpeciesInCatch, CellSpan.Rows);
                                table3.AddCell(this.Utilization == 0 ? this.Water : string.Empty, Legal.LegalNote.InputClass, this.SpeciesInCatch, CellSpan.Rows);
                                table3.AddCell(this.Utilization == 0 ? this.Date.ToString("d") : string.Empty, Legal.LegalNote.InputClass, this.SpeciesInCatch, CellSpan.Rows);
                            }

                            table3.AddCell(catchRow.Mass.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                            table3.EndRow();

                            index++;
                        }

                        table3.StartRow();
                        table3.AddCell(Resources.NoteContent.Total, 5, CellSpan.Columns);
                        table3.AddCell(w, Legal.LegalNote.InputClass | ReportCellClass.Right);
                        table3.EndRow();

                        result.AddTable(table3);

                        result.AddParagraph("из них:");
                        result.AddParagraph("а) переданы в целях использования для питания экипажей судов, " +
                            "членов бригад, научных экспедиционных групп непосредственно осуществляющих добычу (вылов) " +
                            "водных биологических ресурсов при осуществлении " +
                            "рыболовства в научно-исследовательских и контрольных целях***:");

                        Report.Table table4 = new Report.Table();

                        table4.AddHeader(new string[] {
                            Resources.NoteContent.CaptionNo,
                            "Вид рыбы",
                            Resources.NoteContent.CaptionMass
                        },
                            new double[] { width_no, 0, width_mass });

                        w = 0;
                        index = 0;

                        if (this.Utilization == 1)
                        {
                            foreach (CatchRow catchRow in this.CorrespondingCatchNote.GetCatchRows())
                            {
                                w += catchRow.Mass;

                                table4.StartRow();
                                table4.AddCell(index + 1, Legal.LegalNote.InputClass | ReportCellClass.Right);
                                table4.AddCell(catchRow.SpeciesRow.Local, Legal.LegalNote.InputClass);
                                table4.AddCell(catchRow.Mass.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                                table4.EndRow();

                                index++;
                            }
                        }

                        table4.StartRow();
                        table4.AddCell(Resources.NoteContent.Total, 2, CellSpan.Columns);
                        table4.AddCell(w, Legal.LegalNote.InputClass | ReportCellClass.Right);
                        table4.EndRow();

                        result.AddTable(table4);

                        result.AddTable(ReportExtensions.GetAskBlock("Приемку водных биологических ресурсов, указанных в настоящем акте, осуществил:",
                            string.Empty, "Ф.И.О., должность, наименование и сведения документа, удостоверяющего личность (серия, номер, кем и когда выдан)"));
                        result.AddTable(ReportExtensions.GetSignatureBlock(this.Date, Resources.NoteContent.HintSign, "дата приемки"));

                        result.AddParagraph("б) передано лицу, уполномоченному научной организацией, осуществляющей " +
                            "рыболовство в научно-исследовательских и контрольных целях, для последующего уничтожения " +
                            "или транспортировки (далее - уполномоченное лицо)***:");

                        Report.Table table5 = new Report.Table();

                        table5.AddHeader(new string[]
                        {
                            Resources.NoteContent.CaptionNo,
                            Resources.NoteContent.CaptionSpecies,
                            "Ф.И.О., должность, реквизиты доверенности (договора) научной организации, наименование и сведения документа, удостоверяющего личность уполномоченного лица",
                            Resources.NoteContent.CaptionMass
                        }, new double[] { width_no, 0, 0, width_mass });

                        w = 0;
                        index = 0;

                        if (this.Utilization == 2)
                        {
                            foreach (CatchRow catchRow in this.CorrespondingCatchNote.GetCatchRows())
                            {
                                w += catchRow.Mass;

                                table5.AddCell(index + 1, Legal.LegalNote.InputClass | ReportCellClass.Right);
                                table5.AddCell(catchRow.SpeciesRow.Local, Legal.LegalNote.InputClass);

                                if (index == 0)
                                {
                                    table5.AddCell(
                                        (this.IsExpeditorNull() ? string.Empty : this.Expeditor) + "<br/>" +
                                        (this.IsExpeditorRequisitesNull() ? string.Empty : this.ExpeditorRequisites),
                                        Legal.LegalNote.InputClass, this.SpeciesInCatch, CellSpan.Rows);
                                }

                                table5.AddCell(catchRow.Mass.ToString("N1"), Legal.LegalNote.InputClass | ReportCellClass.Right);
                                table5.EndRow();

                                index++;
                            }
                        }

                        table5.StartRow();
                        table5.AddCell(Resources.NoteContent.Total, 3, CellSpan.Columns);
                        table5.AddCell(w, Legal.LegalNote.InputClass | ReportCellClass.Right);
                        table5.EndRow();

                        result.AddTable(table5);

                        result.AddTable(ReportExtensions.GetAskBlock("Приемку водных биологических ресурсов, указанных в настоящем акте, осуществил:",
                            this.Utilization == 2 ? this.Expeditor : string.Empty, "Ф.И.О. уполномоченного лица"));
                        result.AddTable(ReportExtensions.GetSignatureBlock(this.Date, Resources.NoteContent.HintSign, "дата приемки"));

                        result.AddTable(ReportExtensions.GetAskBlock("Водные биологические ресурсы будут транспортироваться по маршруту",
                            this.IsRouteNull() ? string.Empty : this.Route, "пункты маршрута, виды транспорта"));

                        result.AddTable(ReportExtensions.GetAskBlock("в",
                            (this.IsAddresseeNull() ? string.Empty : this.Addressee) + ", " +
                            (this.IsDestinationNull() ? string.Empty : this.Destination),
                            "конечный грузополучатель: полное название научной организации, фактический адрес доставки"));

                        #endregion

                        break;
                }

                if (this.IsStampAngleNull())
                {
                    this.StampAngle = new Random().Next(-40, 40);
                }

                //result.End(this.LicenseRow.Executive, this.Bystander1, this.Bystander2, this.StampAngle);


                result.AddParagraph("Настоящий акт составлен в 2 экземплярах.");
                result.AddParagraph("Подписи присутствующих при составлении акта*:");

                Report.Table table9 = new Report.Table();

                table9.AddHeader(.6, 0, .3);

                table9.StartRow();
                table9.AddCell(this.Bystander1, Legal.LegalNote.InputClass);
                table9.AddCell();
                table9.AddCell(string.Empty, Legal.LegalNote.InputClass);
                table9.EndRow();

                table9.StartRow();
                table9.AddCellComment(Resources.NoteContent.HintName);
                table9.AddCell();
                table9.AddCellComment(Resources.NoteContent.HintSign);
                table9.EndRow();

                table9.StartRow();
                table9.AddCell(this.Bystander2, Legal.LegalNote.InputClass);
                table9.AddCell();
                table9.AddCell(string.Empty, Legal.LegalNote.InputClass);
                table9.EndRow();

                table9.StartRow();
                table9.AddCellComment(Resources.NoteContent.HintName);
                table9.AddCell();
                table9.AddCellComment(Resources.NoteContent.HintSign);
                table9.EndRow();



                result.AddTable(table9);

                result.AddParagraph("Подпись ответственного лица научной организации:");

                Report.Table table10 = new Report.Table();

                table10.AddHeader(.6, 0, .3);

                table10.StartRow();

                table10.AddCell(this.LicenseRow.Executive, Legal.LegalNote.InputClass);
                table10.AddCell();
                if (UserSettings.UseFaximile) table10.AddSignature(UserSettings.Faximile);
                else table10.AddCell(string.Empty, Legal.LegalNote.InputClass);
                table10.EndRow();

                table10.StartRow();
                table10.AddCellComment(Resources.NoteContent.HintName);
                table10.AddCell();
                table10.AddCellComment(Resources.NoteContent.HintSign);
                table10.EndRow();

                table10.StartRow();
                table10.AddCell();
                if (UserSettings.UseStamp)
                {
                    result.AddScript("rotate.js");
                    table10.AddStamp(UserSettings.Stamp, this.StampAngle);
                }
                else { table10.AddCell(); }
                table10.AddCell("М. П.**");
                table10.EndRow();

                result.AddTable(table10);

                result.AddComment("* – заполняется в случае присутствия на момент отбора образцов и живых особой водных " +
                    "биологических ресурсов других лиц, кроме ответственного лица научной организации;", false);

                result.AddComment("** – акт заверяется судовой печатью при осуществлении рыболовства в научно-исследовательских " +
                    "целях с использованием судов, или печатью научной организации (её филиала) при осуществлении " +
                    "рыболовства в научно-исследовательских целях без использования судов.", false);

                if ((LegalNoteType)this.Content == LegalNoteType.Transport)
                    result.AddComment("*** – заполняется только при осуществлении указанных в графе действий;", false);

                return result;
            }
        }

        partial class QuoteRow
        {
            public int CaughtQuantity()
            {
                int result = 0;

                foreach (CatchRow catchRow in ((LegalPapers)tableQuote.DataSet).Catch)
                {
                    if (catchRow.SpeciesRow != this.SpeciesRow) continue;
                    if (catchRow.LegalNoteRow.LicenseRow != this.LicenseRow) continue;
                    if (catchRow.LegalNoteRow.Content != (int)LegalNoteType.Catch) continue;

                    result += catchRow.Quantity;
                }

                return result;
            }

            public Decimal CaughtMass()
            {
                Decimal result = 0;

                foreach (CatchRow catchRow in ((LegalPapers)tableQuote.DataSet).Catch)
                {
                    if (catchRow.SpeciesRow != this.SpeciesRow) continue;
                    if (catchRow.LegalNoteRow.LicenseRow != this.LicenseRow) continue;
                    if (catchRow.LegalNoteRow.Content != (int)LegalNoteType.Catch) continue;

                    result += catchRow.Mass;
                }

                return result;
            }

            public Decimal RemainMass()
            {
                return this.Mass - this.CaughtMass();
            }

            public int CaughtQuantity(DateTime start, DateTime end)
            {
                int result = 0;

                foreach (CatchRow catchRow in ((LegalPapers)tableQuote.DataSet).Catch)
                {
                    if (catchRow.SpeciesRow != this.SpeciesRow) continue;
                    if (catchRow.LegalNoteRow.LicenseRow != this.LicenseRow) continue;
                    if (catchRow.LegalNoteRow.Content != (int)LegalNoteType.Catch) continue;
                    if (catchRow.LegalNoteRow.Date < start) continue;
                    if (catchRow.LegalNoteRow.Date > end) continue;

                    result += catchRow.Quantity;
                }

                return result;

            }

            public Decimal CaughtMass(DateTime start, DateTime end)
            {
                Decimal result = 0;

                foreach (CatchRow catchRow in ((LegalPapers)tableQuote.DataSet).Catch)
                {
                    if (catchRow.SpeciesRow != this.SpeciesRow) continue;
                    if (catchRow.LegalNoteRow.LicenseRow != this.LicenseRow) continue;
                    if (catchRow.LegalNoteRow.Content != (int)LegalNoteType.Catch) continue;
                    if (catchRow.LegalNoteRow.Date < start) continue;
                    if (catchRow.LegalNoteRow.Date > end) continue;

                    result += catchRow.Mass;
                }

                return result;
            }

        }


        partial class LicenseRow
        {
            public string Requisites
            {
                get
                {
                    if (this.No.IsAcceptable() && !this.IsIssuedNull())
                    {
                        return string.Format("{0:№ 00 0000  00 0000 AA} от {1:D}", this.No,
                            //string.Format(@"№\ 00\ 0000\ \ 00\ 0000\ AA", this.No), 
                            this.Issued);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

            public QuoteRow GetQuoteRow(string species)
            {
                foreach (LegalPapers.QuoteRow quoteRow in this.GetQuoteRows())
                {
                    if (quoteRow.SpeciesRow.Species == species)
                    {
                        return quoteRow;
                    }
                }

                return null;
            }

            public string ExecutiveFormal
            {
                get
                {
                    return this.IsExecutivePostNull() ? this.Executive :
                        string.Format("{0} {1} {2}",
                        this.ExecutivePost, this.Holder, this.Executive);
                }
            }
        }

        public void ResetNumbers(LegalNoteType type)
        {
            int i = 1;

            foreach (LegalNoteRow noteRow in this.LegalNote.Select("Content = " + (int)type, "Date asc"))
            {
                //if (noteRow.IsNoNull()) continue;
                //if (noteRow.Content != (int)type) continue;
                noteRow.No = i;
                i++;
            }

        }
    }
}
