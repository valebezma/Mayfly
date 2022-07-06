using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Species
{
    internal class TreeNodeSorter : IComparer, IComparer<TreeNode>
    {
        public int Compare(TreeNode tx, TreeNode ty)
        {
            if (tx.Tag is TaxonomicIndex.TaxonRow trx && ty.Tag is TaxonomicIndex.TaxonRow trz)
            {
                return trx.CompareTo(trz);
            }

            if (tx.Text == null || ty.Text == null)
                return 0;

            return string.Compare(tx.Text, ty.Text);
        }

        public int Compare(object tx, object ty)
        {
            if (tx is TreeNode tnx && ty is TreeNode tny)
            {
                return Compare(tnx, tny);
            }

            return 0;
        }
    }
}
