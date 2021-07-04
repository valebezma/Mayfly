using Mayfly.Wild;
using Mayfly.Waters;
using Mayfly.Controls;
using Microsoft.Win32;
using System.IO;
using System.Resources;
using System;
using System.Windows.Forms;
using System.Collections;
using Mayfly.Extensions;

namespace Mayfly.Reader
{
    public abstract class Service
    {
        public static double GetStrate(double length)
        {
            return Math.Floor(length);
        }

        public static void HandleIdenticCount(DataGridView grid, DataGridViewCellEventArgs e)
        {
            if (grid[e.ColumnIndex, e.RowIndex].Value is int)
            {
                int value = (int)grid[e.ColumnIndex, e.RowIndex].Value;
                if (value < 2)
                {
                    grid[e.ColumnIndex, e.RowIndex].Value = null;
                    System.Media.SystemSounds.Beep.Play();
                }
            }
        }
    }
}
