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
            if (tx.Tag is SpeciesKey.BaseRow brx && ty.Tag is SpeciesKey.BaseRow bry)
            {
                return brx.CompareTo(bry);
            }

            if (tx.Tag is SpeciesKey.TaxaRow trx && ty.Tag is SpeciesKey.TaxaRow trz)
            {
                return trx.CompareTo(trz);
            }

            if (tx.Name == null || ty.Name == null)
                return 0;

            return string.Compare(tx.Name.ToString(), ty.Name.ToString());
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
