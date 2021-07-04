using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mayfly
{
    public class SearchResultSorter : IComparer<string>, IComparer<object>, IComparer<ListViewItem>
    {
        private string searchTerm;

        public SearchResultSorter(string term)
        { 
            searchTerm = term.ToUpperInvariant();
        }

        public int Compare(string x, string y)
        {
            x = x.ToUpperInvariant();
            y = y.ToUpperInvariant();

            int u = x.IndexOf(searchTerm);
            int v = y.IndexOf(searchTerm);

            if (u == -1) u = x.Length;
            if (v == -1) v = y.Length;

            if (u == v)
            {
                int l = searchTerm.Length;
                try
                {
                    return string.Compare(x.Substring(u + l), y.Substring(v + l));
                }
                catch
                {
                    return string.Compare(x, y);
                }
            }
            else
            {
                return u - v;
            }
        }

        public int Compare(object x, object y)
        {
            return Compare(x.ToString(), y.ToString());            
        }

        public int Compare(ListViewItem item1, ListViewItem item2)
        {
            return Compare(item1.Text, item2.Text);            
        }
    }
}
