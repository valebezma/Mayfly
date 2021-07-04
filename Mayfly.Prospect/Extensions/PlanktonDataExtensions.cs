using Mayfly.Plankton;
using Mayfly.Plankton.Explorer;
using Mayfly.Waters;
using Mayfly.Wild;

namespace Mayfly.Prospect
{
    public static class PlanktonDataExtensions
    {
        public static CardStack GetCards(this Data data, WatersKey.WaterRow waterRow)
        {
            CardStack result = new CardStack();

            if (waterRow.IsWaterNull()) return result;

            foreach (Data.CardRow cardRow in data.Card)
            {
                if (cardRow.IsWaterIDNull()) continue;
                if (cardRow.WaterRow.Type != waterRow.Type) continue;
                if (cardRow.WaterRow.IsWaterNull()) continue;
                if (cardRow.WaterRow.Water != waterRow.Water) continue;
                if (cardRow.WaterID != waterRow.ID) continue;

                result.Add(cardRow);
            }

            return result;
        }
    }
}
