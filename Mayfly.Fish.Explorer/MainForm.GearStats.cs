using Mayfly.Extensions;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace Mayfly.Fish.Explorer
{
    partial class MainForm
    {
        private CardStack EffortsData;

        private CardStack CatchesData;

        private FishSamplerType SelectedSamplerType 
        {
            get; set;
            //{
            //    return Fish.Service.Sampler(listViewGears.GetID()).GetSamplerType();
            //}
        }

        private UnitEffort SelectedEffortUE { get; set; }



        private Series GetSinkingSchedule(FishSamplerType samplerType)
        {
            Series series = new Series();
            series.LegendText = samplerType.ToDisplay();
            series.BorderColor = Color.Black;

            series.ChartType = SeriesChartType.RangeBar;
            series.YValuesPerPoint = 2;
            series.SetCustomProperty("DrawSideBySide", "False");
            //series.SetCustomProperty("PointWidth", "2");

            foreach (Data.CardRow cardRow in FullStack.GetStack(samplerType))
            {
                if (cardRow.IsMeshNull())
                    continue;

                if (cardRow.IsWhenNull())
                    continue;

                int pointIndex = series.Points.AddXY(cardRow.Mesh, cardRow.When - (cardRow.IsSpanNull() ? TimeSpan.FromHours(2) : cardRow.Duration), cardRow.When);
                series.Points[pointIndex].Tag = cardRow;
                series.Points[pointIndex].ToolTip = cardRow.ToString();
            }

            return series;
        }

        //private void UpdateCatchTotals()
        //{
        //    double q = 0.0;
        //    double w = 0.0;

        //    for (int i = 0; i < spreadSheetCatches.Rows.Count; i++)
        //    {
        //        if (spreadSheetCatches[columnCatchSpc.Index, i].Value.ToString() == Mayfly.Resources.Interface.Total)
        //        {
        //            continue;
        //        }

        //        if (!spreadSheetCatches.IsHidden(i))
        //        {
        //            if (spreadSheetCatches[columnCatchN.Index, i].Value != null)
        //            {
        //                q += (double)spreadSheetCatches[columnCatchN.Index, i].Value;
        //            }

        //            if (spreadSheetCatches[columnCatchW.Index, i].Value != null)
        //            {
        //                w += (double)spreadSheetCatches[columnCatchW.Index, i].Value;
        //            }
        //        }
        //    }

        //    for (int i = 0; i < spreadSheetCatches.Rows.Count; i++)
        //    {
        //        if (spreadSheetCatches[columnCatchSpc.Index, i].Value.ToString() == Mayfly.Resources.Interface.Total)
        //        {
        //            spreadSheetCatches[columnCatchN.Index, i].Value = q;
        //            spreadSheetCatches[columnCatchW.Index, i].Value = w;
        //        }
        //    }
        //}
    }
}
