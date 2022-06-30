using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.Extensions;
using Mayfly.Controls;
using Meta.Numerics;
using Mayfly.Wild;
using Mayfly.Species;

namespace Mayfly.Fish.Explorer
{
    public partial class StratifiedControl : Form
    {
        Data Data { get; set; }

        private SpeciesKey.TaxonRow SelectedSpecies;



        public StratifiedControl(Data data)
        {
            InitializeComponent();
            listViewSpecies.Shine();
            UpdateSample(data);
        }



        public void UpdateSample(Data data)
        {
            Data = data;

            foreach (Data.SpeciesRow speciesRow in Data.Species)
            {
                listViewSpecies.CreateItem(speciesRow.Species, speciesRow.KeyRecord.CommonName);
            }

            UpdateSample(SelectedSpecies);
        }

        private void UpdateSample(SpeciesKey.TaxonRow speciesRow)
        {
            spreadSheetSample.Rows.Clear();

            if (speciesRow == null) return;


            double min = Data.GetStack().LengthMin(speciesRow);
            double max = Data.GetStack().LengthMax(speciesRow);

            Interval strate = Service.GetStrate(min);

            //for (double l = min; l <= max; l += 10)
            for (double l = Service.GetStrate(min).LeftEndpoint; l < max; l += UserSettings.SizeInterval)
            {
                strate = Interval.FromEndpointAndWidth(l, UserSettings.SizeInterval);

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetSample);
                gridRow.Cells[ColumnClass.Index].Value = strate;
                spreadSheetSample.Rows.Add(gridRow);

                double q = Data.GetStack().Quantity(speciesRow, strate);
                if (q > 0)
                {
                    gridRow.Cells[ColumnCount.Index].Value = q;

                    int weighted = Data.GetStack().Treated(speciesRow, Data.Individual.MassColumn, strate);
                    if (weighted > 0)
                    {
                        gridRow.Cells[ColumnMass.Index].Value = weighted;

                        if (weighted >= UserSettings.RequiredClassSize)
                            ((TextAndImageCell)gridRow.Cells[ColumnMass.Index]).Image = 
                                Mathematics.Properties.Resources.Check;
                    }
                    else
                    {
                        gridRow.Cells[ColumnMass.Index].Value = null;
                        //((TextAndImageCell)gridRow.Cells[ColumnMass.Index]).Image = null;
                    }

                    int regids = Data.GetStack().Treated(speciesRow, Data.Individual.RegIDColumn, strate);
                    if (regids > 0)
                    {
                        gridRow.Cells[ColumnRegID.Index].Value = regids;
                        if (regids >= UserSettings.RequiredClassSize)
                            ((TextAndImageCell)gridRow.Cells[ColumnRegID.Index]).Image =
                                Mathematics.Properties.Resources.Check;
                    }
                    else
                    {
                        gridRow.Cells[ColumnRegID.Index].Value = null;
                        //((TextAndImageCell)gridRow.Cells[ColumnRegID.Index]).Image = null;
                    }
                }
                else
                {
                    gridRow.Cells[ColumnCount.Index].Value = null;
                }

            }
        }

        private void listViewSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSpecies.SelectedItems.Count == 0)
            {
                SelectedSpecies = null;
                spreadSheetSample.Rows.Clear();
            }
            if (listViewSpecies.SelectedItems.Count > 0)
            {
                SelectedSpecies = Data.Species.FindBySpecies(listViewSpecies.SelectedItems[0].Name).KeyRecord;
                UpdateSample(SelectedSpecies);
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Data.GetStack().GetStratifiedCribnote().Run();
        }
    }
}
