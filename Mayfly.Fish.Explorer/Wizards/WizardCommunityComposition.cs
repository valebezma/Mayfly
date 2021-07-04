using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using AeroWizard;
using Mayfly.Wild;
using Mayfly.Waters;
using Mayfly.Mathematics;
using System.Data;
using Mayfly;
using Meta.Numerics.Statistics;
using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.Fish;

namespace Mayfly.Fish.Explorer.Survey
{
    public partial class WizardCommunityComposition : Form
    {
        public CardStack Data { get; set; }

        private WizardGearSet gearWizard;

        public CompositionEqualizer CatchesComposition;

        public SpeciesComposition NaturalComposition;

        public event EventHandler CatchesEstimated;

        public event EventHandler Finished;

        public event EventHandler Returned;



        private WizardCommunityComposition()
        {
            InitializeComponent();

            ColumnCatchesAbundance.ValueType =
                ColumnCatchesAbundanceP.ValueType =
                ColumnCatchesBiomass.ValueType =
                ColumnCatchesBiomassP.ValueType =
                ColumnCommAbundance.ValueType =
                ColumnCommAbundanceP.ValueType =
                ColumnCommBiomass.ValueType =
                ColumnCommBiomassP.ValueType =
                ColumnCommQ.ValueType =
                ColumnCommDominance.ValueType =
                typeof(double);

            ColumnCatchesSex.ValueType =
                ColumnCatchesSpecies.ValueType =
                ColumnCommSpecies.ValueType =
                typeof(string);
        }

        public WizardCommunityComposition(CardStack data) : this()
        {
            Data = data;

            Log.Write(EventType.WizardStarted, "Community composition wizard is started.");
        }



        public void Run(WizardGearSet _gearWizard, SpeciesComposition _example)
        {
            gearWizard = _gearWizard;
            //gearWizard.Replace(this);

            ColumnCatchesAbundance.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCatchesBiomass.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCommAbundance.ResetFormatted(gearWizard.SelectedUnit.Unit);
            ColumnCommBiomass.ResetFormatted(gearWizard.SelectedUnit.Unit);

            checkBoxPUE.ResetFormatted(gearWizard.SelectedUnit.Unit);

            comboBoxParameter.Enabled = checkBoxFractions.Enabled = checkBoxPUE.Enabled = false;
            pageComposition.SetNavigation(false);
            calculatorCommunity.RunWorkerAsync(_example);
        }


        public void AddEstimatedComposition(Report report)
        {
            report.AddParagraph(Resources.Reports.Community.ParagraphCommunity1,
                gearWizard.SelectedSamplerType.ToDisplay());

            report.AddEquation("\\text{Abundance} = \\frac{\\overline{CPUE}}{q}", ",");

            report.AddParagraph(
                string.Format(Resources.Reports.Community.ParagraphCommunity2, report.NextTableNumber)
                );

            Report.Table table1 = new Report.Table(Resources.Reports.Community.TableCommunity);

            table1.StartRow();
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .25, 2);
            table1.AddHeaderCell(Resources.Reports.Community.Catchability, 2, CellSpan.Rows);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Abundance, 2);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Biomass, 2);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Occurrence, 2, CellSpan.Rows);
            table1.AddHeaderCell(Wild.Resources.Reports.Caption.Dominance + "*", 2, CellSpan.Rows);
            table1.EndRow();

            table1.StartRow();
            table1.AddHeaderCell(gearWizard.AbundanceUnits +
                (gearWizard.SelectedUnit.Variant == ExpressionVariant.Efforts ? "**" : string.Empty));
            table1.AddHeaderCell("%");
            table1.AddHeaderCell(gearWizard.BiomassUnits +
                (gearWizard.SelectedUnit.Variant == ExpressionVariant.Efforts ? "**" : string.Empty));
            table1.AddHeaderCell("%");
            table1.EndRow();

            foreach (Category species in NaturalComposition)
            {
                Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(species.Name);

                table1.StartRow();
                table1.AddCell(speciesRow.ToShortHTML());
                table1.AddCellRight(Service.GetCatchability(gearWizard.SelectedSamplerType, species.Name), "N2");
                table1.AddCellRight(species.Abundance, ColumnCommAbundance.DefaultCellStyle.Format);
                table1.AddCellRight(species.AbundanceFraction, ColumnCommAbundanceP.DefaultCellStyle.Format);
                table1.AddCellRight(species.Biomass, ColumnCommBiomass.DefaultCellStyle.Format);
                table1.AddCellRight(species.BiomassFraction, ColumnCommBiomassP.DefaultCellStyle.Format);
                table1.AddCellRight(species.Occurrence, ColumnCommOccurrence.DefaultCellStyle.Format);
                table1.AddCellRight(species.Dominance, ColumnCommDominance.DefaultCellStyle.Format);
                table1.EndRow();
            }

            table1.StartRow();
            table1.AddCell(Mayfly.Resources.Interface.Total);
            table1.AddCell();
            table1.AddCellRight(NaturalComposition.TotalAbundance, ColumnCommAbundance.DefaultCellStyle.Format);
            table1.AddCellRight(1, ColumnCommAbundanceP.DefaultCellStyle.Format);
            table1.AddCellRight(NaturalComposition.TotalBiomass, ColumnCommBiomass.DefaultCellStyle.Format);
            table1.AddCellRight(1, ColumnCommBiomassP.DefaultCellStyle.Format);
            table1.AddCell();
            table1.AddCell();
            table1.EndRow();
            report.AddTable(table1);

            report.AddComment(string.Format(Resources.Reports.Community.CommunityNoticeDominance,
                UserSettings.DominanceIndexName) +
                (gearWizard.SelectedUnit.Variant == ExpressionVariant.Efforts ?
                string.Format(Resources.Reports.Community.CommunityNotice,
                    gearWizard.SelectedUnit.Unit, gearWizard.SelectedUnit.UnitCost,
                    Resources.Reports.Common.m3) : string.Empty));

            report.AddParagraph(Resources.Reports.Community.ParagraphCommunity3,
                Wild.Resources.Interface.Diversity.ResourceManager.GetString(Wild.UserSettings.Diversity.ToString()),
                NaturalComposition.Diversity);
        }

        public Report GetCpueAppendices()
        {
            Report report = new Report(Resources.Reports.Community.AppendixCpue, PageBreakOption.Landscape);
            
            
            #region A

            Report.Appendix a = new Report.Appendix(Resources.Reports.Community.AppA);

            a.StartRow();
            a.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 3);
            a.AddHeaderCell(Resources.Reports.GearStats.ColumnGearClass, CatchesComposition.Dimension * 2);
            a.EndRow();

            a.StartRow();
            foreach (Composition gearClasscomposition in CatchesComposition.SeparateCompositions)
            {
                a.AddHeaderCell(gearClasscomposition.Name, 2);
            }
            a.EndRow();

            a.StartRow();
            for (int i = 0; i < CatchesComposition.Dimension; i++)
            {
                a.AddHeaderCell(string.Format("N, {0}", Resources.Reports.Common.Ind));
                a.AddHeaderCell(string.Format("B, {0}", Resources.Reports.Common.Kg));
            }
            a.EndRow();

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);

                a.StartRow();
                a.AddCell(speciesRow.ToShortHTML());
                for (int j = 0; j < CatchesComposition.Dimension; j++)
                {
                    if (CatchesComposition[j, i].Quantity > 0)
                    {
                        a.AddCellRight(CatchesComposition[j, i].Quantity, "N0");
                        a.AddCellRight(CatchesComposition[j, i].Mass, "N3");
                    }
                    else
                    {
                        a.AddCell();
                        a.AddCell();
                    }
                }
                a.EndRow();

            }

            a.StartRow();
            a.AddCell(Mayfly.Resources.Interface.Total);
            for (int j = 0; j < CatchesComposition.Dimension; j++)
            {
                a.AddCellRight(CatchesComposition.GetComposition(j).TotalQuantity);
                a.AddCellRight(CatchesComposition.GetComposition(j).TotalMass);
            }
            a.EndRow();

            #endregion

            report.AddAppendix(a);

            #region B

            Report.Appendix b = new Report.Appendix(Resources.Reports.Community.AppB);

            b.StartRow();
            b.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 3);
            b.AddHeaderCell(Resources.Reports.GearStats.ColumnGearClass, CatchesComposition.Dimension * 2);
            b.EndRow();

            b.StartRow();
            foreach (Composition gearClasscomposition in CatchesComposition.SeparateCompositions)
            {
                b.AddHeaderCell(gearClasscomposition.Name, 2);
            }
            b.EndRow();

            b.StartRow();
            for (int i = 0; i < CatchesComposition.Dimension; i++)
            {
                b.AddHeaderCell(string.Format("N, {0} / {1}", Resources.Reports.Common.Ind, gearWizard.SelectedUnit.Unit));
                b.AddHeaderCell(string.Format("B, {0} / {1}", Resources.Reports.Common.Kg, gearWizard.SelectedUnit.Unit));
            }
            b.EndRow();

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);

                b.StartRow();
                b.AddCell(speciesRow.ToShortHTML());
                for (int j = 0; j < CatchesComposition.Dimension; j++)
                {
                    if (CatchesComposition[j, i].Quantity > 0)
                    {
                        b.AddCellRight(CatchesComposition[j, i].Abundance, "N1");
                        b.AddCellRight(CatchesComposition[j, i].Biomass, "N2");
                    }
                    else
                    {
                        b.AddCell();
                        b.AddCell();
                    }
                }
                b.EndRow();

            }

            b.StartRow();
            b.AddCell(Mayfly.Resources.Interface.Total);
            for (int j = 0; j < CatchesComposition.Dimension; j++)
            {
                b.AddCellRight(CatchesComposition.GetComposition(j).TotalAbundance, "N0");
                b.AddCellRight(CatchesComposition.GetComposition(j).TotalBiomass, "N2");
            }
            b.EndRow();

            #endregion

            report.AddAppendix(b);

            #region E

            Report.Appendix e = new Report.Appendix(Resources.Reports.Community.AppC);

            e.StartRow();
            e.AddHeaderCell(Wild.Resources.Reports.Caption.Species, .15, 3);
            e.AddHeaderCell(Resources.Reports.GearStats.ColumnGearClass, CatchesComposition.Dimension * 2);
            e.EndRow();

            e.StartRow();
            foreach (Composition gearClasscomposition in CatchesComposition.SeparateCompositions)
            {
                e.AddHeaderCell(gearClasscomposition.Name, 2);
            }
            e.EndRow();

            e.StartRow();
            for (int i = 0 ; i < CatchesComposition.Dimension; i++)
            {
                //e.AddHeaderCell(string.Format("{0}, %", Wild.Resources.Reports.Caption.Abundance));
                //e.AddHeaderCell(string.Format("{0}, %", Wild.Resources.Reports.Caption.Biomass));
                e.AddHeaderCell("N, %");
                e.AddHeaderCell("B, %");
            }
            e.EndRow();

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);

                e.StartRow();
                e.AddCell(speciesRow.ToShortHTML());
                for (int j = 0; j < CatchesComposition.Dimension; j++)
                {
                    if (CatchesComposition[j, i].Quantity > 0) 
                    { 
                        e.AddCellRight(CatchesComposition[j, i].AbundanceFraction * 100, "N1");
                        e.AddCellRight(CatchesComposition[j, i].BiomassFraction * 100, "N1");
                    }
                    else
                    { 
                        e.AddCell(); 
                        e.AddCell(); 
                    }
                }
                e.EndRow();

            }

            e.StartRow();
            e.AddCell(Mayfly.Resources.Interface.Total);
            for (int j = 0; j < CatchesComposition.Dimension; j++)
            {
                e.AddCellRight(100, "N1");
                e.AddCellRight(100, "N1");
            }
            e.EndRow();

            #endregion

            report.AddAppendix(e);

            report.EndBranded();

            return report;
        }

        public Report GetAbundanceAppendices()
        {
            Report report = new Report(Resources.Reports.Community.AppendixAbundance);

            #region Abundance calculations

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                Data.SpeciesRow speciesRow = Data.Parent.Species.FindBySpecies(CatchesComposition[i].Name);
                report.AddSubtitle3(speciesRow.ToHTML());

                double catchability = Service.GetCatchability(gearWizard.SelectedSamplerType, CatchesComposition[i].Name);

                string latexA = string.Empty;
                string latexAA = string.Empty;
                string latexB = string.Empty;
                string latexBB = string.Empty;

                double totalNpue = 0;
                double totalBpue = 0;

                for (gearWizard.DatasetIndex = 0; gearWizard.DatasetIndex < gearWizard.SelectedCount; gearWizard.DatasetIndex++)
                {
                    Composition classComposition = CatchesComposition.GetComposition(gearWizard.DatasetIndex);

                    if (classComposition[i].Quantity == 0) continue;

                    totalNpue += classComposition[i].Abundance;// *gearWizard.CurrentSpatialWeight;
                    totalBpue += classComposition[i].Biomass;// *gearWizard.CurrentSpatialWeight;

                    if (latexA != string.Empty)
                    {
                        latexAA += " + ";
                        latexBB += " + ";
                        latexA += " + ";
                        latexB += " + ";
                    }

                    //latexAA += "{{" + classComposition[i].Abundance.ToString("N3") + "} \\times " +
                    //    "{" + gearWizard.CurrentSpatialWeight.ToString("N2") + "}}\\\\";

                    //latexBB += "{{" + classComposition[i].Biomass.ToString("N3") + "} \\times " +
                    //    "{" + gearWizard.CurrentSpatialWeight.ToString("N2") + "}}\\\\";

                    latexAA += "{{" + classComposition[i].Quantity.ToString("N0") + " / " + classComposition[i].Index.ToString("G3") + "}}\\\\";
                    latexA += "{{" + classComposition[i].Abundance.ToString("N3") + "}}\\\\";

                    latexBB += "{{" + classComposition[i].Mass.ToString("G3") + " / " + classComposition[i].Index.ToString("G3") + "}}\\\\";
                    latexB += "{{" + classComposition[i].Biomass.ToString("N3") + "}}\\\\";
                }

                latexA = latexA.TrimEnd("\\".ToCharArray());
                latexB = latexB.TrimEnd("\\".ToCharArray());

                report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Abundance + "} = " +
                    "\\frac{ \\left( \\begin{split} " + latexAA + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
                    "\\frac{ \\left( \\begin{split} " + latexA + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
                    //"\\frac{{" + totalNpue.ToString("N3") + "}/{" + gearWizard.SelectedCount + "}}{" + Catchability.ToString("N3") + "} = " +
                    "\\frac{" + (totalNpue / (double)gearWizard.SelectedCount).ToString("N3") + "}{" + catchability.ToString("N3") + "} = " +
                    "{" + NaturalComposition[i].Abundance.ToString("N3") + "}\\text{ " + gearWizard.AbundanceUnits + "}");

                report.AddEquation("\\text{" + Wild.Resources.Reports.Caption.Biomass + "} = " +
                    "\\frac{ \\left( \\begin{split} " + latexBB + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
                    "\\frac{ \\left( \\begin{split} " + latexB + " \\end{split} \\right) / {" + gearWizard.SelectedCount + "}}{" + catchability.ToString("N3") + "} = " +
                    //"\\frac{{" + totalBpue.ToString("N3") + "}/{" + gearWizard.SelectedCount + "}}{" + Catchability.ToString("N3") + "} = " +
                    "\\frac{" + (totalBpue / (double)gearWizard.SelectedCount).ToString("N3") + "}{" + catchability.ToString("N3") + "} = " +
                    "{" + NaturalComposition[i].Biomass.ToString("N3") + "}\\text{ " + gearWizard.BiomassUnits + "}");

            }

            #endregion

            report.EndBranded();

            return report;
        }


        private void RecalculateCommunity()
        {
            for (int i = 0; i < NaturalComposition.Count; i++)
            {
                double catchability = Service.GetCatchability(gearWizard.SelectedSamplerType, CatchesComposition[i].Name);
                NaturalComposition[i].Abundance = CatchesComposition[i].Abundance / catchability;
                //NaturalComposition[i].Abundance = Math.Round(CatchesComposition[i].Abundance / catchability, 0);
                NaturalComposition[i].Biomass = CatchesComposition[i].Biomass / catchability;
            }
        }



        private void wizardPage1_Initialize(object sender, WizardPageInitEventArgs e)
        {
            wizardExplorer.NextPage();
        }

        private void pageComposition_Rollback(object sender, WizardPageConfirmEventArgs e)
        {
            if (Returned != null)
            {
                e.Cancel = true;
                Returned.Invoke(sender, e);
            }
        }

        private void communityCalculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Mayfly.Controls.Taskbar.SetProgressValue(this.Handle, (ulong)e.ProgressPercentage, (ulong)100);
        }

        private void communityCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            NaturalComposition = (e.Argument == null ? gearWizard.SelectedData.GetCommunityCompositionFrame() : (SpeciesComposition)e.Argument);

            CatchesComposition = gearWizard.SelectedStacks.ToArray().GetWeightedComposition(
                gearWizard.WeightType, gearWizard.SelectedUnit.Variant, NaturalComposition);
            CatchesComposition.Name = gearWizard.SelectedSamplerType.ToDisplay();

            RecalculateCommunity();
        }

        private void communityCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CatchesComposition.SetLines(columnComposition);
            CatchesComposition.SetLines(ColumnCatchesSpecies);
            CatchesComposition.SetLines(ColumnCommSpecies);

            spreadSheetComposition.ClearInsertedColumns();

            foreach (Composition gearClassComposition in CatchesComposition.SeparateCompositions)
            {
                spreadSheetComposition.InsertColumn(gearClassComposition.Name, gearClassComposition.Name,
                    typeof(double), spreadSheetComposition.ColumnCount, 75).ReadOnly = true;
            }

            for (int i = 0; i < CatchesComposition.Count; i++)
            {
                spreadSheetCatches[ColumnCatchesAbundance.Index, i].Value = CatchesComposition[i].Abundance;
                spreadSheetCatches[ColumnCatchesAbundanceP.Index, i].Value = CatchesComposition[i].AbundanceFraction;
                spreadSheetCatches[ColumnCatchesBiomass.Index, i].Value = CatchesComposition[i].Biomass;
                spreadSheetCatches[ColumnCatchesBiomassP.Index, i].Value = CatchesComposition[i].BiomassFraction;
                spreadSheetCatches[ColumnCatchesSex.Index, i].Value = CatchesComposition[i].GetSexualComposition();
            }

            for (int i = 0; i < NaturalComposition.Count; i++)
            {
                spreadSheetCommunity[ColumnCommAbundance.Index, i].Value = NaturalComposition[i].Abundance;
                spreadSheetCommunity[ColumnCommAbundanceP.Index, i].Value = NaturalComposition[i].AbundanceFraction;
                spreadSheetCommunity[ColumnCommBiomass.Index, i].Value = NaturalComposition[i].Biomass;
                spreadSheetCommunity[ColumnCommBiomassP.Index, i].Value = NaturalComposition[i].BiomassFraction;
                spreadSheetCommunity[ColumnCommQ.Index, i].Value = 
                    Service.GetCatchability(gearWizard.SelectedSamplerType, NaturalComposition[i].Name);
                spreadSheetCommunity[ColumnCommOccurrence.Index, i].Value = NaturalComposition[i].Occurrence;
                spreadSheetCommunity[ColumnCommDominance.Index, i].Value = NaturalComposition[i].Dominance;
            }

            textBoxDiversity.Text = NaturalComposition.Diversity.ToString("N3");

            comboBoxParameter.Enabled = checkBoxFractions.Enabled = checkBoxPUE.Enabled = true;
            comboBoxParameter.SelectedIndex = 1;
            pageComposition.SetNavigation(true);
        }

        private void displayParameter_Changed(object sender, EventArgs e)
        {
            if (comboBoxParameter.SelectedIndex == -1) return;

            if (checkBoxFractions.Checked)
            {
                spreadSheetComposition.DefaultCellStyle.Format = "P1";
            }
            //else if (checkBoxPUE.Checked)
            //{
            //    spreadSheetComposition.DefaultCellStyle.Format = Mayfly.Service.Mask(3);
            //}
            else
            {
                switch (comboBoxParameter.SelectedIndex)
                {
                    case 0:
                        spreadSheetComposition.DefaultCellStyle.Format = string.Empty;
                        break;

                    case 1:
                        spreadSheetComposition.DefaultCellStyle.Format = Mayfly.Service.Mask(3);
                        break;
                }
            }

            CatchesComposition.SeparateCompositions.ToArray().UpdateValues(spreadSheetComposition, columnComposition,
                Category.GetValueVariant(comboBoxParameter.SelectedIndex == 0,
                checkBoxPUE.Checked, checkBoxFractions.Checked));
        }

        private void checkBoxFractions_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxPUE.Enabled = !checkBoxFractions.Checked;

            displayParameter_Changed(sender, e);
        }

        private void pageCatches_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (CatchesEstimated != null)
            {
                CatchesEstimated.Invoke(this, e);
                e.Cancel = true;
            }
        }

        private void spreadSheetCommunity_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColumnCommQ.Index && spreadSheetCommunity.ContainsFocus)
            {
                if (spreadSheetCommunity[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    spreadSheetCommunity[e.ColumnIndex, e.RowIndex].Value =
                        Service.GetCatchability(gearWizard.SelectedSamplerType, NaturalComposition[e.RowIndex].Name);
                }
                else
                {
                    double value = (double)spreadSheetCommunity[e.ColumnIndex, e.RowIndex].Value;
                    Service.SaveCatchability(gearWizard.SelectedSamplerType, NaturalComposition[e.RowIndex].Name, value);

                    RecalculateCommunity();
                }

                for (int i = 0; i < NaturalComposition.Count; i++)
                {
                    spreadSheetCommunity[ColumnCommAbundance.Index, i].Value = NaturalComposition[i].Abundance;
                    spreadSheetCommunity[ColumnCommAbundanceP.Index, i].Value = NaturalComposition[i].AbundanceFraction;
                    spreadSheetCommunity[ColumnCommBiomass.Index, i].Value = NaturalComposition[i].Biomass;
                    spreadSheetCommunity[ColumnCommBiomassP.Index, i].Value = NaturalComposition[i].BiomassFraction;
                }
            }
        }

        private void pageCommunity_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            Log.Write(EventType.WizardEnded, "Community composition wizard is finished.");

            if (Finished != null)
            {
                e.Cancel = true;
                Finished.Invoke(sender, e);
            }
        }

        private void wizardExplorer_Cancelling(object sender, EventArgs e)
        {
            Close();
        }
    }
}