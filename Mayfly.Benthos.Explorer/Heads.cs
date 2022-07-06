using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.Species;
using Mayfly.Wild;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;
using Mayfly.Mathematics.Statistics;
using Meta.Numerics.Statistics;

namespace Mayfly.Benthos.Explorer
{

    public class HeadAssess
    {
        public static int limit = 15;

        public TaxonomicIndex.TaxonRow Species;

        public HeadSample VystitysSample;

        public HeadSample VolgaSample;

        public HeadSample TotalSample;

        public int Count;

        public HeadAssess(TaxonomicIndex.TaxonRow speciesRow,
            CardStack vsStack, CardStack vlStack)
        {
            Species = speciesRow;

            List<Data.IndividualRow> wRows = speciesRow.GetWeightedIndividualRows();
            List<Data.IndividualRow> vsRows = speciesRow.GetWeightedIndividualRows(vsStack);
            List<Data.IndividualRow> vlRows = speciesRow.GetWeightedIndividualRows(vlStack);

            TotalSample = new HeadSample(wRows, "Обобщенная выборка", "Общ");
            if (vsRows.Count >= limit) VystitysSample = new HeadSample(vsRows, "Озеро Виштынецкое", "Вшт");
            if (vlRows.Count >= limit) VolgaSample = new HeadSample(vlRows, "Бассейн Верхней Волги", "Влг");
        }
    }

    public class HeadSample
    {
        public Power lw;

        public Power dw;

        public int Count;

        public string Name;

        public string Legend;

        public List<HeadInstarSample> Instars;



        public HeadSample(List<Data.IndividualRow> wRows, string name, string legend)
        {
            string l = "Длина, мм";
            string d = "Ширина головной капсулы, мм";
            string w = "Масса, мг";
            Data data = (Data)wRows[0].Table.DataSet;
            Name = name;
            Legend = legend;

            for (int i = 0; i < wRows.Count; i++)
            {
                if ((!wRows[i].IsGradeNull() && wRows[i].Grade > 1))
                {
                    wRows.RemoveAt(i);
                    i--;
                }
            }

            Count = wRows.Count;

            List<Data.IndividualRow> iRows = wRows.GetMeasuredRows(data.Individual.InstarColumn);

            Instars = new List<HeadInstarSample>();

            if (iRows.Count > 0)
            {
                List<object> instars = data.Individual.InstarColumn.GetValues(iRows, true);

                foreach (object instar in instars)
                {
                    //List<Data.IndividualRow> irows = (List<Data.IndividualRow>)
                    var irows =
                        iRows.GetRows(data.Individual.InstarColumn, instar.ToString());

                    List<double> iii = new List<double>();

                    foreach (Data.IndividualRow indRow in irows)
                    {
                        Data.ValueRow vr = data.Value.FindByIndIDVarID(indRow.ID,
                            data.Variable.FindByVarName(d).ID);
                        if (vr != null) iii.Add(vr.Value);
                    }

                    HeadInstarSample his = new HeadInstarSample()
                    {
                        Instar = (int)instar,
                        Count = irows.Count,
                        MassSample = new Sample(data.Individual.MassColumn.GetDoubles(irows)),
                        LengthSample = new Sample(data.Individual.LengthColumn.GetDoubles(irows)),
                        CapsuleSample = new Sample(iii)
                    };

                    Instars.Add(his);
                }

                Instars.Sort();
            }

            List<Data.IndividualRow> lRows = wRows.GetMeasuredRows(data.Individual.LengthColumn);

            BivariateSample lSample = new BivariateSample(l, w);
            foreach (Data.IndividualRow lRow in lRows)
            {
                lSample.Add(lRow.Length, lRow.Mass);
            }

            lw = new Power(lSample);

            List<Data.IndividualRow> dRows = wRows.GetMeasuredRows(d);
            BivariateSample dSample = new BivariateSample(d, w);
            foreach (Data.IndividualRow dRow in dRows)
            {
                dSample.Add(data.GetIndividualValue(dRow, d), dRow.Mass);
            }

            dw = new Power(dSample);

            if (dw.Slope < 2 || dw.Slope > 4)
            {
                dw = null;
            }
        }
    }

    public class HeadInstarSample : IComparable
    {
        public int Instar;
        public int Count;
        public Sample MassSample;
        public Sample LengthSample;
        public Sample CapsuleSample;


        int IComparable.CompareTo(object obj)
        {
            return Compare(this, (HeadInstarSample)obj);
        }

        public static int Compare(HeadInstarSample value1, HeadInstarSample value2)
        {
            return value1.Instar - value2.Instar;
        }

    }
}
