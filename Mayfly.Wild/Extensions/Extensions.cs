using Mayfly.Controls;
using Mayfly.Species;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Mayfly.Wild
{
    public static class Extensions
    {
        public static void UpdateValues(this Composition[] compositions, SpreadSheet sheet, DataGridViewColumn columnNames, CompositionColumn vv)
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

        public static string ToStratifiedDots(this int value, int maxlength) // Should return some divs with classes 'dot10', 'dot9' etc.
        {
            string result = string.Empty;

            // 1 - Find tens
            int tens = value / 10;

            int currlength = 0;

            result += "<div class='counter-container'>";

            for (int i = 0; i < tens; i++)
            {
                currlength++;

                result += "<div class='counter'>";

                for (int c = 1; c <= 4; c++) {
                    result += "<div class='point count-" + c + "'></div>";
                }
                for (int c = 5; c <= 10; c++) {
                    result += "<div class='bar count-" + c + "'></div>";
                }

                result += "</div>";

                if (maxlength > 0 && currlength == maxlength)
                {
                    result += "<br>";
                    currlength = 0;
                }
            }

            // 2 - Find rest of tens
            int rest = value % 10;

            if (rest > 0)
            {
                result += "<div class='counter'>";

                for (int c = 1; c <= rest; c++) {
                    result += "<div class='point count-" + c + "'></div>";
                }
                for (int c = 5; c <= rest; c++) {
                    result += "<div class='bar count-" + c + "'></div>";
                }

                result += "</div>";
            }

            result += "</div>";

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

            result.AddRange(FileSystem.MaskedNames(entries, Wild.UserSettings.InterfaceBio.Extension));

            if (extension != Wild.UserSettings.InterfaceBio.Extension)
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
