using Mayfly.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Mayfly.Geographics;
using Mayfly.Wild;
using System.Globalization;

namespace Mayfly.Fish.Explorer
{
    public partial class Surveys
    {
        internal int EmptyFleetCount
        {
            get
            {
                int r = 0;

                foreach (FleetRow fleetRow in this.Fleet)
                {
                    if (fleetRow.GetGearRows().Length == 0)
                        r++;
                }

                return r;
            }
        }

        public Data Anamnesis { get; set; }

        public bool ContainsCatchData
        {
            get
            {
                bool contains = false;

                foreach (ActionRow actionRow in Action)
                {
                    if (actionRow.IsCatchXMLNull()) continue;
                    contains = true;
                    break;
                }

                return contains;
            }
        }

        partial class GearRow
        {
            public override string ToString()
            {
                if (this.IsSamplerIDNull()) return "Empty equipment row";

                Wild.Samplers.SamplerRow samplerRow = Fish.UserSettings.SamplersIndex.Sampler.FindByID(this.SamplerID);

                if (this.IsMeshNull()) return string.Format("{0}: {1}", this.Name, samplerRow.ShortName);
                else return string.Format("{0}: {1} ◊{2}", this.Name, samplerRow.ShortName, this.Mesh);
            }
        }

        partial class ActionRow
        {
            public ActionRow PreviousAction
            {
                get
                {
                    ActionRow result = this;

                    foreach (TimelineRow timeRow in ((Surveys)this.tableAction.DataSet)
                        .Timeline.Select(string.Empty, "Timepoint ASC"))
                    {
                        if (timeRow.Timepoint >= this.TimelineRow.Timepoint)
                            break;

                        foreach (ActionRow actionRow in timeRow.GetActionRows())
                        {
                            if (actionRow.GearRow == this.GearRow)
                            {
                                result = actionRow;
                            }
                        }
                    }

                    return result;
                }
            }

            public string GetShortDescription()
            {
                return string.Format(Resources.Interface.Interface.CatchDescription, this.GearRow.ToString(), this.TimelineRow.Timepoint);
            }

            public Data GetCatchData()
            {
                DateTime now = this.TimelineRow.Timepoint;
                DateTime since = this.PreviousAction.TimelineRow.Timepoint;

                Data data = new Data();
                if (!this.IsCatchXMLNull())
                {
                    data.ReadXml(new StringReader(this.CatchXML));
                }

                data.Solitary.When = now;
                data.Solitary.AttachSign();

                data.Solitary.Sampler = this.GearRow.SamplerID;
                if (!this.GearRow.IsLengthNull()) data.Solitary.Length = this.GearRow.Length;
                if (!this.GearRow.IsHeightNull()) data.Solitary.Height = this.GearRow.Height;
                if (!this.GearRow.IsMeshNull()) data.Solitary.Mesh = this.GearRow.Mesh;
                data.Solitary.Span = (int)(now - since).TotalMinutes;

                if (!this.TimelineRow.IsWeatherNull())
                    data.Solitary.Weather = this.TimelineRow.Weather;

                if (!this.TimelineRow.IsWaterTemperatureNull())
                    data.Solitary.Physicals = "BTM:-;SRF:" + this.TimelineRow.WaterTemperature.ToString(string.Empty, CultureInfo.InvariantCulture) + ";RAT:-;LMP:-;";//.StateOfWater.TemperatureSurface = this.TimelineRow.WaterTemperature;

                if (((Surveys)this.tableAction.DataSet).Survey.Rows.Count > 0)
                {
                    SurveyRow comRow = ((Surveys)this.tableAction.DataSet).Survey[0];

                    if (!comRow.IsWaterNameNull() || !comRow.IsWaterTypeNull())
                    {
                        Data.WaterRow wr = data.Water.NewWaterRow();
                        if (!comRow.IsWaterTypeNull()) wr.Type = comRow.WaterType;
                        if (!comRow.IsWaterNameNull()) wr.Water = comRow.WaterName;
                        data.Water.AddWaterRow(wr);
                        data.Solitary.WaterRow = wr;
                    }
                }

                return data;
            }
        }

        partial class TimelineRow
        {
            public string[] ExportCards(string path)
            {
                List<string> result = new List<string>();

                foreach (ActionRow actionRow in this.GetActionRows())
                {
                    switch ((EquipmentEvent)actionRow.Type)
                    {
                        case EquipmentEvent.Inspection:
                        case EquipmentEvent.Removing:
                            Data data = actionRow.GetCatchData();
                            string filename = FileSystem.SuggestName(path, data.GetSuggestedName());
                            data.WriteToFile(Path.Combine(path, filename));
                            result.Add(Path.Combine(path, filename));
                            break;
                    }
                }

                return result.ToArray();
            }

            public string GetShortDescription()
            {
                return string.Format(Resources.Interface.Interface.TimepointDescription, this.Timepoint);
            }

            public WeatherState WeatherConditions
            {
                get
                {
                    return this.IsWeatherNull() ? null : new WeatherState(this.Weather);
                }
            }
        }

        public Data[] GetCards()
        {
            List<Data> result = new List<Data>();

            foreach (ActionRow actionRow in this.Action)
            {
                switch ((EquipmentEvent)actionRow.Type)
                {
                    case EquipmentEvent.Inspection:
                    case EquipmentEvent.Removing:
                        Data data = actionRow.GetCatchData();
                        result.Add(data);
                        break;
                }
            }

            return result.ToArray();
        }

        public string[] ExportCards(string path)
        {
            List<string> result = new List<string>();

            foreach (Data data in GetCards())
            {
                string filename = FileSystem.SuggestName(path, data.GetSuggestedName());
                data.WriteToFile(Path.Combine(path, filename));
                result.Add(Path.Combine(path, filename));
            }

            return result.ToArray();
        }

        //public string[] ExportCards(string path)
        //{
        //    List<string> result = new List<string>();

        //    foreach (ActionRow actionRow in this.Action)
        //    {
        //        switch ((EquipmentEvent)actionRow.Type)
        //        {
        //            case EquipmentEvent.Inspection:
        //            case EquipmentEvent.Removing:
        //                Data data = actionRow.GetCatchData();
        //                string filename = FileSystem.SuggestName(path, data.SuggestedName);
        //                data.WriteToFile(Path.Combine(path, filename));
        //                result.Add(Path.Combine(path, filename));
        //                break;
        //        }
        //    }

        //    return result.ToArray();
        //}

        public Data GetCombinedData()
        {
            Data result = new Data();

            foreach (ActionRow actionRow in Action)
            {
                if (actionRow.IsCatchXMLNull()) continue;
                Data data = new Data();
                data.ReadXml(new StringReader(actionRow.CatchXML));
                data.CopyTo(result);
            }

            if (Anamnesis != null)
            {
                Anamnesis.CopyTo(result);
            }

            return result;
        }
    }
}
