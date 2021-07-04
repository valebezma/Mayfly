using Mayfly.Bacterioplankton;
using Mayfly.Bacterioplankton.Explorer;
using System;
using System.Collections.Generic;
using System.Data;
using Mayfly.Wild;

namespace Mayfly.Extensions
{
    public static class DataExtensionsCopy
    {
        //public static Data.CardRow[] ImportTo(this Data data, Data extData)
        //{
        //    List<Data.CardRow> result = new List<Data.CardRow>();

        //    foreach (Data.FactorRow factorRow in data.Factor)
        //    {
        //        if (extData.Factor.FindByFactor(factorRow.Factor) == null)
        //        {
        //            Data.FactorRow newFactorRow = extData.Factor.NewFactorRow();
        //            newFactorRow.Factor = factorRow.Factor;
        //            extData.Factor.AddFactorRow(newFactorRow);
        //        }
        //    }

        //    foreach (Data.CardRow cardRow in data.Card)
        //    {
        //        Data.CardRow newCardRow = data.ImportTo(extData, cardRow);
        //        result.Add(newCardRow);
        //    }

        //    return result.ToArray();
        //}

        //public static Data.CardRow ImportTo(this Data data, Data extData, Data.CardRow cardRow)
        //{
        //    Data.CardRow newCardRow = extData.Card.NewCardRow();

        //    if (!cardRow.IsWaterIDNull())
        //    {
        //        Data.WaterRow waterRow = extData.Water.Find(cardRow.WaterRow);

        //        if (waterRow == null)
        //        {
        //            waterRow = extData.Water.NewWaterRow();
        //            waterRow.Type = cardRow.WaterRow.Type;
        //            waterRow.Water = cardRow.WaterRow.Water;
        //            extData.Water.AddWaterRow(waterRow);

        //            try
        //            {
        //                waterRow.ID = cardRow.WaterID;
        //            }
        //            catch { }
        //        }

        //        newCardRow.WaterRow = waterRow;
        //    }

        //    foreach (DataColumn dataColumn in data.Card.Columns)
        //    {
        //        if (cardRow.IsNull(dataColumn)) continue;
        //        if (dataColumn == data.Card.IDColumn) continue;
        //        if (dataColumn == data.Card.WaterIDColumn) continue;
        //        newCardRow[dataColumn.ColumnName] = cardRow[dataColumn.ColumnName];
        //    }

        //    extData.Card.Rows.Add(newCardRow);

        //    cardRow.ImportLogTo(newCardRow);

        //    newCardRow.Path = cardRow.Path;
        //    newCardRow.Investigator = cardRow.Investigator;

        //    foreach (Data.FactorValueRow factorValueRow in cardRow.GetFactorValueRows())
        //    {
        //        Data.FactorValueRow newFactorValueRow = extData.FactorValue.NewFactorValueRow();
        //        newFactorValueRow.CardRow = newCardRow;
        //        newFactorValueRow.FactorRow = extData.Factor.FindByFactor(factorValueRow.FactorRow.Factor);
        //        newFactorValueRow.Value = factorValueRow.Value;
        //        extData.FactorValue.AddFactorValueRow(newFactorValueRow);
        //    }

        //    return newCardRow;
        //}

        //public static bool IsLoaded(this Data data, string fileName)
        //{
        //    foreach (Data.CardRow cardRow in data.Card)
        //    {
        //        if (cardRow.Path == null) continue;
        //        if (cardRow.Path == fileName) return true;
        //    }

        //    return false;
        //}
        


        //public static Data Copy(this Data data, DateTime startDay, DateTime endDay)
        //{
        //    Data result = data.Copy();

        //    for (int i = 0; i < result.Card.Rows.Count; i++)
        //    {
        //        if (result.Card[i].When.Date < startDay || result.Card[i].When.Date > endDay)
        //        {
        //            result.Card.Rows.RemoveAt(i);
        //            i--;
        //        }
        //    }

        //    result.ClearUseless();

        //    return result;
        //}
    }
}
