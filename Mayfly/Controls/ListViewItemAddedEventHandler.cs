using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Controls
{
    public class ListViewItemAddedEventArgs : EventArgs
    {
        public ListViewItemAddedEventArgs(ListViewItem item)
        {
            this.Item = item;
        }

        public ListViewItem Item { get; private set; }
    }

    public delegate void ListViewItemAddedEventHandler(object sender, ListViewItemAddedEventArgs e);
}
