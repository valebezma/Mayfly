using Mayfly.Fish;
using Mayfly.Fish.Explorer;
using Mayfly.Waters;
using System.Windows.Forms;
using System;
using Mayfly.Extensions;
using System.Collections.Generic;
using System.Reflection;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class FishDataExtensions
    {
        public static CardStack GetCards(this Data data, WatersKey.WaterRow waterRow)
        {
            CardStack result = new CardStack();

            if (waterRow.IsWaterNull()) return result;

            foreach (Data.CardRow cardRow in data.Card)
            {
                if (cardRow.IsWaterIDNull()) continue;
                if (cardRow.WaterRow.Type  != waterRow.Type) continue;
                if (cardRow.WaterRow.IsWaterNull()) continue;
                if (cardRow.WaterRow.Water != waterRow.Water) continue;
                //if (cardRow.WaterID != waterRow.ID) continue;

                result.Add(cardRow);
            }

            return result;
        }

        public static CardStack GetCards(this Data data, WatersKey.WaterRow waterRow, int year)
        {
            CardStack result = new CardStack();

            if (waterRow.IsWaterNull()) return result;

            foreach (Data.CardRow cardRow in data.Card)
            {
                if (cardRow.When.Year != year) continue;

                if (cardRow.IsWaterIDNull()) continue;
                if (cardRow.WaterRow.Type  != waterRow.Type) continue;
                if (cardRow.WaterRow.IsWaterNull()) continue;
                if (cardRow.WaterRow.Water  != waterRow.Water) continue;

                result.Add(cardRow);
            }

            return result;
        }

        public static Button GetButton(this CardStack data, string text)
        {
            Button button = new Button();

            button.Tag = data;
            button.Text = text;
            button.AutoSize = true;
            button.Click += stackButton_Click;

            return button;
        }

        private static void stackButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            CardStack data = (CardStack)button.Tag;
            Mayfly.Fish.Explorer.MainForm explorer = new Fish.Explorer.MainForm(data);
            explorer.Show();
        }
    }
}
