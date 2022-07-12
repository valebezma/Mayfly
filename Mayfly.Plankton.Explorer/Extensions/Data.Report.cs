using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly.Plankton;
using Mayfly.Plankton.Explorer;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class DataExtensionsReport
    {
        public static void AddCommon(this Wild.Survey data, Report report)
        {
            data.AddCommon(report, new string[] { }, new string[] { });
        }

        public static void AddCommon(this Wild.Survey data, Report report, string prompt, string value)
        {
            data.AddCommon(report, new string[] { prompt }, new string[] { value });
        }

        public static void AddCommon(this Wild.Survey data, Report report, string[] prompts, string[] values)
        {
            Report.Table table1 = new Report.Table();
            table1.StartRow();
            table1.AddCellPrompt(Wild.Resources.Reports.Card.Investigator,
                data.GetInvestigators().Merge());
            table1.EndRow();
            table1.StartRow();
            table1.AddCellPrompt("Waters",
                data.GetWaters().Merge());
            table1.EndRow();
            table1.StartRow();

            DateTime[] dates = data.GetDates();
            table1.AddCellPrompt("Collection dates", dates.GetDatesDescription());
            table1.EndRow();
            table1.StartRow();
            table1.AddCellPrompt("Samplers", data.GetSamplersList().Merge());
            table1.EndRow();

            for (int i = 0; i < prompts.Length; i++)
            {
                table1.StartRow();
                table1.AddCellPrompt(
                    prompts[i],
                    values[i]);
                table1.EndRow();
            }

            report.AddTable(table1);
        }
    }
}
