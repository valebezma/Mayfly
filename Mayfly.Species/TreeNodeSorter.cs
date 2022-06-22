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
            if (tx.Tag is SpeciesKey.TaxonRow trx && ty.Tag is SpeciesKey.TaxonRow trz)
            {
                return trx.CompareTo(trz);
            }

            if (tx.Tag is SpeciesKey.TaxonRow && ty.Tag is SpeciesKey.SpeciesRow)
            {
                return -1;
            }

            if (ty.Tag is SpeciesKey.TaxonRow && tx.Tag is SpeciesKey.SpeciesRow)
            {
                return 1;
            }

            //if (tx.Tag is SpeciesKey.SpeciesRow srx && ty.Tag is SpeciesKey.SpeciesRow sry)
            //{
            //    return srx.CompareTo(sry);
            //}

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
