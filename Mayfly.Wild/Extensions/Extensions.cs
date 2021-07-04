using Mayfly.Controls;
using Mayfly.Species;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Mayfly.Wild
{
    public static class Extensions
    {
        public static void UpdateValues(this Composition[] compositions, SpreadSheet sheet, DataGridViewColumn columnNames, ValueVariant vv)
        {
            foreach (Composition comp in compositions)
            {
                comp.UpdateValues(sheet, columnNames, vv);
            }
        }

        public static string ToStratifiedDots(this int value)
        {
            return value.ToStratifiedDots(0);
        }

        public static string ToStratifiedDots(this int value, int maxlength)
        {
            string result = string.Empty;

            // 1 - Find tens
            int tens = value / 10;

            // 2 - Find rest of tens
            int rest = value - tens * 10;

            int currlength = 0;

            for (int i = 0; i < tens; i++)
            {
                currlength++;
                result += string.Format("<img src='{0}\\interface\\reports\\img\\dots\\10.png'>", Application.StartupPath);

                if (maxlength > 0 && currlength == maxlength)
                {
                    result += "<br>";
                    currlength = 0;
                }
            }
            
            if (rest > 0) 
                result += string.Format("<img src='{0}\\interface\\reports\\img\\dots\\{1}.png'>", Application.StartupPath, rest);

            return result;
        }

        public static string[] GetOperableFilenames(this DragEventArgs e, string extension)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, false)) return new string[0];
            string[] entries = (string[])e.Data.GetData(DataFormats.FileDrop);
            return entries.GetOperableFilenames(extension);
        }

        public static string[] GetOperableFilenames(this string[] entries, string extension)
        {
            List<string> result = new List<string>();

            if (entries == null) return result.ToArray();

            if (Licensing.Verify("Bios"))
            {
                result.AddRange(FileSystem.MaskedNames(entries, Wild.UserSettings.InterfaceBio.Extension));
            }

            //result.AddRange(FileSystem.MaskedNames(entries, Wild.UserSettings.InterfacePermission.Extension));

            if (extension != Wild.UserSettings.InterfaceBio.Extension)// && 
                //extension != Wild.UserSettings.InterfacePermission.Extension)
            {
                result.AddRange(FileSystem.MaskedNames(entries, extension));
            }

            return result.ToArray();
        }

        public static void AddTaxaMenus(this ToolStripMenuItem item, SpeciesKey.BaseRow baseRow, EventHandler command)
        {
            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i].Tag != null)
                {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator))
            {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (SpeciesKey.TaxaRow taxaRow in baseRow.GetTaxaRows())
            {
                ToolStripItem _item = new ToolStripMenuItem();
                _item.Tag = taxaRow;
                _item.Text = taxaRow.Taxon;
                _item.Click += command;
                item.DropDownItems.Add(_item);
            }

        }

        public static void AddBaseMenus(this ToolStripMenuItem item, SpeciesKey key, EventHandler command)
        {
            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i].Tag != null)
                {
                    item.DropDownItems.RemoveAt(i);
                    i--;
                }
            }

            if (item.DropDownItems.Count > 0 && !(item.DropDownItems[item.DropDownItems.Count - 1] is ToolStripSeparator))
            {
                item.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (SpeciesKey.BaseRow baseRow in key.Base)
            {
                ToolStripItem _item = new ToolStripMenuItem();
                _item.Tag = baseRow;
                _item.Text = baseRow.Base;
                _item.Click += command;
                item.DropDownItems.Add(_item);
            }

            item.Enabled = item.DropDownItems.Count > 0;
        }

        public static void AddBaseList(this ComboBox comboBox, SpeciesKey key)
        {
            comboBox.DisplayMember = "Base";

            comboBox.Items.Clear();

            // Fill list

            foreach (SpeciesKey.BaseRow baseRow in key.Base)
            {
                comboBox.Items.Add(baseRow);
            }

            comboBox.Enabled = comboBox.Items.Count > 0;
        }
    }
}
