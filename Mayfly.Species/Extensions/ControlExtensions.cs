using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Species;

namespace Mayfly.Extensions
{
    public static class ControlExtensions
    {
        public static ListViewItem CreateItem(this ListView listView, SpeciesKey.SpeciesRow speciesRow)
        {
            ListViewItem result = listView.CreateItem(speciesRow.ID.ToString(), speciesRow.Species);
            result.UpdateItem(speciesRow);
            result.Tag = speciesRow;
            return result;
        }

        public static void UpdateItem(this ListViewItem item, SpeciesKey.SpeciesRow speciesRow)
        {
            item.Name = speciesRow.ID.ToString();
            item.Text = speciesRow.Species;
            item.ToolTipText = speciesRow.ToolTip.Merge(Constants.Break);
            item.UpdateItem(new object[] { speciesRow.IsReferenceNull() ? string.Empty : speciesRow.Reference });
            item.Tag = speciesRow;
        }
    }
}
