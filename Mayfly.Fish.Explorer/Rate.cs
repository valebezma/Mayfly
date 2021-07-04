using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Wild;
using Mayfly.Extensions;

namespace Mayfly.Fish.Explorer
{
    public partial class Rate : Form
    {
        CardStack Data;

        Data.SpeciesRow SpeciesRow { get; set; }

        FishSamplerType SamplerType { get; set; }



        public Rate(CardStack data)
        {
            InitializeComponent();
            Data = data;

            SamplerType = FishSamplerType.None;
            comboBoxSpecies.DataSource = Data.GetSpeciesCaught();
            comboBoxSampler.DataSource = Data.GetSamplerTypes();
        }




        public Report GetGatheringReport()
        {
            Report report = new Report(Resources.Reports.Rate.TitleG);
            Data.AddCommon(report);
            report.UseTableNumeration = true;
            Data.AddGathering(report);
            report.EndBranded();

            return report;
        }

        public Report GetTreatmentReport()
        {
            Report report = new Report(Resources.Reports.Rate.TitleT);
            Data.AddCommon(report);
            report.UseTableNumeration = true;            
            Data.AddTreatment(report);
            report.EndBranded();

            return report;
        }



        private void Rate_Load(object sender, EventArgs e)
        {
            chartR.Series[0].AnimateChart(Data.GetRate(), labelR, .65);
            chartT2.Series[0].AnimateChart(Data.GetTreatmentRate(), labelT2, .8);
            chartG2.Series[0].AnimateChart(Data.GetGatheringRate(), labelG2, .8);
        }

        private void Rate_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (BackgroundWorker rateCalc in new BackgroundWorker[] { 
                rateF, rateG, rateI, rateWe, rateT, rateW
            })
            {
                if (rateCalc.IsBusy)
                    rateCalc.CancelAsync();
            }
        }



        private void rateW_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Data.GetWidth(SamplerType);
        }

        private void rateW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
            chartW.Series[0].AnimateChart((double)e.Result, labelW, .8);
        }

        private void rateF_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Data.GetFrequency(SamplerType);
        }



        private void rateF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
            chartF.Series[0].AnimateChart((double)e.Result, labelF, .8);
        }

        private void rateG_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SamplerType == FishSamplerType.None) e.Result = Data.GetGatheringRate();
            else e.Result = Data.GetGatheringRate(SamplerType);
        }

        private void rateG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
            chartG.Series[0].AnimateChart((double)e.Result, labelG, .8);

            buttonPrintG.Enabled = comboBoxSampler.Enabled = true;
            tabPageGathering.UseWaitCursor = false;
            comboBoxSampler.Focus();
        }

        private void reportG_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetGatheringReport();
        }

        private void reportG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();
            buttonPrintG.Enabled = true;
            tabPageGathering.UseWaitCursor = false;
        }



        private void buttonPrintG_Click(object sender, EventArgs e)
        {
            buttonPrintG.Enabled = false;
            tabPageGathering.UseWaitCursor = true;
            reportG.RunWorkerAsync();
        }

        private void comboBoxSampler_SelectedIndexChanged(object sender, EventArgs e)
        {
            SamplerType = comboBoxSampler.SelectedIndex == -1 ? FishSamplerType.None :
                (FishSamplerType)comboBoxSampler.SelectedValue;

            buttonPrintG.Enabled = comboBoxSampler.Enabled = false;
            tabPageGathering.UseWaitCursor = true;

            rateW.RunWorkerAsync();
            rateF.RunWorkerAsync();

            rateG.RunWorkerAsync();
        }

        private void labelG_SizeChanged(object sender, EventArgs e)
        {
            labelG.CenterWith(chartG);
        }





        private void rateT_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SpeciesRow == null) e.Result = Data.GetTreatmentRate();
            else e.Result = Data.GetTreatmentRate(SpeciesRow);
        }

        private void rateT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
             chartT.Series[0].AnimateChart((double)e.Result, labelT, .8);

            buttonPrintT.Enabled = comboBoxSpecies.Enabled = true;
            tabPageTreatment.UseWaitCursor = false;
            comboBoxSpecies.Focus();
        }

        private void rateI_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SpeciesRow == null) e.Result = double.NaN;
            else e.Result = Data.GetIndiscriminance(SpeciesRow);
        }

        private void rateI_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
            chartI.Series[0].AnimateChart((double)e.Result, labelI, .95);
        }

        private void rateGr_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SpeciesRow == null) e.Result = double.NaN;
            else e.Result = Data.GetGrowthModelQuality(SpeciesRow);
        }

        private void rateGr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
            chartGr.Series[0].AnimateChart((double)e.Result, labelGr, .95);
        }

        private void rateWe_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SpeciesRow == null) e.Result = double.NaN;
            else e.Result = Data.GetMassModelQuality(SpeciesRow);
        }

        private void rateWe_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled || this.IsDisposed) return;
            chartWe.Series[0].AnimateChart((double)e.Result, labelWe, .95);
        }

        private void reportT_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetTreatmentReport();
        }

        private void reportT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();
            buttonPrintT.Enabled = true;
            tabPageTreatment.UseWaitCursor = false;
        }



        private void buttonPrintT_Click(object sender, EventArgs e)
        {
            buttonPrintT.Enabled = false;
            tabPageTreatment.UseWaitCursor = true;
            reportT.RunWorkerAsync();
        }

        private void comboBoxSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpeciesRow = (Data.SpeciesRow)comboBoxSpecies.SelectedItem;

            buttonPrintT.Enabled = comboBoxSpecies.Enabled = false;
            tabPageTreatment.UseWaitCursor = true;

            rateI.RunWorkerAsync();
            rateGr.RunWorkerAsync();
            rateWe.RunWorkerAsync();
            rateT.RunWorkerAsync();
        }

        private void labelT_SizeChanged(object sender, EventArgs e)
        {
            labelT.CenterWith(chartT);
        }
        


        private void report_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Data.GetRateReport();
        }

        private void report_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Report)e.Result).Run();
            buttonPrint.Enabled = true;
            tabPageRate.UseWaitCursor = false;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            buttonPrint.Enabled = false;
            tabPageRate.UseWaitCursor = true;
            report.RunWorkerAsync();
        }

        private void labelR_SizeChanged(object sender, EventArgs e)
        {
            labelR.CenterWith(chartR);
        }

        private void comboBoxSpecies_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((ComboBox)sender).HandleInput(e);
        }
    }
}
