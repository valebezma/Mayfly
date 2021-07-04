using System.Windows.Forms;

namespace Mayfly.Waters
{
    public class MouthToMouthSorterAsc : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode)
            {
                TreeNode nodeX = x as TreeNode;
                TreeNode nodeY = y as TreeNode;
                if (nodeX.Tag != null && nodeY.Tag != null)
                { 
                    return new WaterMouthToMouthSorter().Compare(nodeX.Tag, nodeY.Tag); 
                }
            }

            if (x is ListViewItem)
            {
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;
                if (itemX.Tag != null && itemY.Tag != null)
                {
                    return new WaterMouthToMouthSorter().Compare(itemX.Tag, itemY.Tag);
                }
            }

            return string.Compare((x as TreeNode).Name, (y as TreeNode).Name);
        }
    }

    public class SizeSorterAsc : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode)
            {
                TreeNode nodeX = x as TreeNode; 
                TreeNode nodeY = y as TreeNode;
                if (nodeX.Tag != null && nodeY.Tag != null)
                { 
                    return new WaterSizeSorter().Compare(nodeX.Tag, nodeY.Tag);
                }
            }

            if (x is ListViewItem)
            {
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;
                if (itemX.Tag != null && itemY.Tag != null)
                {
                    return new WaterSizeSorter().Compare(itemX.Tag, itemY.Tag);
                }
            }

            return string.Compare((x as TreeNode).Name, (y as TreeNode).Name);
        }
    }

    public class NameSorterAsc : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode)
            {
                TreeNode nodeX = x as TreeNode; 
                TreeNode nodeY = y as TreeNode;
                if (nodeX.Tag != null && nodeY.Tag != null)
                { 
                    return new WaterNameSorter().Compare(nodeX.Tag, nodeY.Tag); 
                }
            }

            if (x is ListViewItem)
            {
                ListViewItem itemX = x as ListViewItem; 
                ListViewItem itemY = y as ListViewItem;
                if (itemX.Tag != null && itemY.Tag != null)
                {
                    return new WaterNameSorter().Compare(itemX.Tag, itemY.Tag);
                }
            }

            return 0;
        }
    }


    public class MouthToMouthSorterDesc : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode)
            {
                TreeNode nodeX = x as TreeNode; 
                TreeNode nodeY = y as TreeNode;
                if (nodeX.Tag != null && nodeY.Tag != null)
                {
                    return -1 * (new WaterMouthToMouthSorter().Compare(nodeX.Tag, nodeY.Tag));
                }
            }

            if (x is ListViewItem)
            {
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;
                if (itemX.Tag != null && itemY.Tag != null)
                { 
                    return -1 * (new WaterMouthToMouthSorter().Compare(itemX.Tag, itemY.Tag));
                }
            }

            return -string.Compare((x as TreeNode).Name, (y as TreeNode).Name);
        }
    }

    public class SizeSorterDesc : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode)
            {
                TreeNode nodeX = x as TreeNode; 
                TreeNode nodeY = y as TreeNode;
                if (nodeX.Tag != null && nodeY.Tag != null)
                { 
                    return -1 * (new WaterSizeSorter().Compare(nodeX.Tag, nodeY.Tag));
                }
            }

            if (x is ListViewItem)
            {
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;
                if (itemX.Tag != null && itemY.Tag != null)
                {
                    return -1 * (new WaterSizeSorter().Compare(itemX.Tag, itemY.Tag));
                }
            }

            return string.Compare((x as TreeNode).Name, (y as TreeNode).Name);
        }
    }

    public class NameSorterDesc : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is TreeNode)
            {
                TreeNode nodeX = x as TreeNode; 
                TreeNode nodeY = y as TreeNode;
                if (nodeX.Tag != null && nodeY.Tag != null)
                {
                    return -1 * (new WaterNameSorter().Compare(nodeX.Tag, nodeY.Tag)); 
                }
            }

            if (x is ListViewItem)
            {
                ListViewItem itemX = x as ListViewItem;
                ListViewItem itemY = y as ListViewItem;
                if (itemX.Tag != null && itemY.Tag != null)
                { 
                    return -1 * (new WaterNameSorter().Compare(itemX.Tag, itemY.Tag));
                }
            }

            return 0;
        }
    }


    public class WaterNameSorter : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            WatersKey.WaterRow waterRowX = x as WatersKey.WaterRow;
            WatersKey.WaterRow waterRowY = y as WatersKey.WaterRow;

            string X = string.Empty;
            string Y = string.Empty;

            if (!waterRowX.IsWaterNull())
            {
                X = waterRowX.Water;
            }

            if (!waterRowY.IsWaterNull())
            {
                Y = waterRowY.Water;
            }

            return string.Compare(X, Y);
        }
    }

    public class WaterSizeSorter : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            WatersKey.WaterRow waterRowX = x as WatersKey.WaterRow;
            WatersKey.WaterRow waterRowY = y as WatersKey.WaterRow;

            double X = -1;
            double Y = -1;

            switch ((WaterType)waterRowX.Type)
            {
                case WaterType.Stream: 
                    if (!waterRowX.IsLengthNull()) 
                    {
                        X = waterRowX.Length; 
                    }
                    break;
                case WaterType.Lake:
                    if (waterRowX.IsAreaNull())
                    {
                        if (!waterRowX.IsWatershedNull())
                        { 
                            X = (int)waterRowX.Watershed; 
                        }
                    }
                    else
                    {
                        X = (int)waterRowX.Area; 
                    }
                    break;
                case WaterType.Tank:
                    if (waterRowX.IsAreaNull())
                    {
                        if (!waterRowX.IsLengthNull())
                        {
                            X = (int)waterRowX.Length;
                        }
                    }
                    else
                    { 
                        X = (int)waterRowX.Area; 
                    }
                    break;
            }

            switch ((WaterType)waterRowY.Type)
            {
                case WaterType.Stream:
                    if (!waterRowY.IsLengthNull())
                    {
                        Y = waterRowY.Length;
                    }
                    break;
                case WaterType.Lake:
                    if (waterRowY.IsAreaNull())
                    {
                        if (!waterRowY.IsWatershedNull())
                        {
                            Y = (int)waterRowY.Watershed;
                        }
                    }
                    else
                    {
                        Y = (int)waterRowY.Area;
                    } 
                    break;
                case WaterType.Tank:
                    if (waterRowY.IsAreaNull())
                    {
                        if (!waterRowY.IsLengthNull())
                        {
                            Y = (int)waterRowY.Length;
                        }
                    }
                    else
                    {
                        Y = (int)waterRowY.Area;
                    } 
                    break;
            }

            return (int)(X - Y);
        }
    }

    public class WaterMouthToMouthSorter : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            WatersKey.WaterRow waterRowX = x as WatersKey.WaterRow;
            WatersKey.WaterRow waterRowY = y as WatersKey.WaterRow;

            if (waterRowX == null) return 0;
            if (waterRowY == null) return 0;

            double X = -1;
            double Y = -1;

            if (waterRowX != null && !waterRowX.IsMouthToMouthNull())
            {
                X = waterRowX.MouthToMouth;
            }

            if (waterRowY != null && !waterRowY.IsMouthToMouthNull())
            {
                Y = waterRowY.MouthToMouth;
            }

            if (X == -1 || Y == -1 || X == Y)
            {
                return string.Compare(waterRowX.FullName, waterRowY.FullName);
            }
            else return (int)(X - Y);
        }
    }
}
