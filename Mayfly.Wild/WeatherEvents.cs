using System.Collections.Generic;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class WeatherEvents
    {
        partial class ValueDataTable
        {
            public ValueRow FindByCode(int code)
            {
                foreach (ValueRow valueRow in this)
                {
                    if (valueRow.IsCodeNull()) continue;
                    if (valueRow.Code != code) continue;
                    return valueRow;
                }

                return null;
            }

            public ValueRow GetByEvent(EventRow eventRow, DegreeRow degreeRow, DiscretionRow discretionRow, EventRow addtEventRow)
            {
                if (eventRow == null) return null;

                foreach (ValueRow valueRow in this)
                {
                    if (valueRow.EventsRowByFK_Events_Value != eventRow) continue;

                    if (addtEventRow == null)
                    {
                        if (!valueRow.IsAdditionalEventNull()) continue;
                    }
                    else
                    {
                        if (valueRow.EventsRowByFK_Events_Value1 != addtEventRow) continue;
                    }

                    if (degreeRow == null)
                    {
                        if (!valueRow.IsDegreeNull()) continue;
                    }
                    else
                    {
                        if (valueRow.DegreeRow != degreeRow) continue;
                    }

                    if (discretionRow == null)
                    {
                        if (!valueRow.IsDiscretionNull()) continue;
                    }
                    else
                    {
                        if (valueRow.DiscretionRow != discretionRow) continue;
                    }

                    return valueRow;
                }

                return null;
            }
        }

        partial class ValueRow
        {
            //public string Description
            //{
            //    get
            //    {
            //        string result = string.Empty;
            //        if (!this.IsDegreeNull()) result += this.DegreeRow.Name;
            //        result += " " + this.EventsRowByFK_Events_Value.Name.ToLower();
            //        if (!this.IsDiscretionNull()) result += " " + this.DiscretionRow.Name.ToLower();
            //        if (!this.IsAdditionalEventNull()) result += " + " + this.EventsRowByFK_Events_Value1.Name.ToLower();
            //        return result;
            //    }
            //}
        }
    
        partial class EventRow
        {
            public string Display { get { return this.Name.GetLocalizedValue(); } }

            private WeatherEvents.ValueDataTable Value
            {
                get
                {
                    return ((WeatherEvents)this.Table.DataSet).Value;
                }
            }

            private WeatherEvents.DegreeDataTable Degree
            {
                get
                {
                    return ((WeatherEvents)this.Table.DataSet).Degree;
                }
            }

            public bool IsDegreeAvailable
            {
                get
                {
                    foreach (ValueRow valueRow in Value.Rows)
                    {
                        if (valueRow.EventsRowByFK_Events_Value == this)
                        {
                            if (!valueRow.IsDegreeNull()) return true;
                        }
                    }
                    return false;
                }
            }

            public DegreeRow[] AvailableDegrees
            {
                get
                {
                    List<DegreeRow> result = new List<DegreeRow>();

                    foreach (ValueRow valueRow in Value.Rows)
                    {
                        if (valueRow.EventsRowByFK_Events_Value == this)
                        {
                            if (!valueRow.IsDegreeNull())
                            {
                                if (!result.Contains(Degree.FindByID(valueRow.Degree)))
                                {
                                    result.Add(Degree.FindByID(valueRow.Degree));
                                }
                            }
                        }
                    }

                    return result.ToArray();
                }
            }

            public bool IsDiscretionAvailable
            {
                get
                {
                    foreach (ValueRow valueRow in Value.Rows)
                    {
                        if (valueRow.EventsRowByFK_Events_Value == this)
                        {
                            if (!valueRow.IsDiscretionNull()) return true;
                        }
                    }
                    return false;
                }
            }

            public bool IsAdditionalEventAvailable
            {
                get
                {
                    foreach (ValueRow _Value in Value.Rows)
                    {
                        if (_Value.EventsRowByFK_Events_Value == this)
                        {
                            if (!_Value.IsAdditionalEventNull()) return true;
                        }
                    }
                    return false;
                }
            }

            public EventRow[] AvailableAdditionalEvents
            {
                get
                {
                    List<EventRow> result = new List<EventRow>();

                    foreach (ValueRow valueRow in Value.Rows)
                    {
                        if (valueRow.EventsRowByFK_Events_Value == this)
                        {
                            if (!valueRow.IsAdditionalEventNull())
                            {
                                if (!result.Contains(tableEvent.FindByID(valueRow.AdditionalEvent)))
                                {
                                    result.Add(tableEvent.FindByID(valueRow.AdditionalEvent));
                                }
                            }
                        }
                    }

                    return result.ToArray();
                }
            }
        }

        partial class DegreeRow
        {
            public string Display { get { return this.Name.GetLocalizedValue(); } }
        }

        partial class DiscretionRow
        {
            public string Display { get { return this.Name.GetLocalizedValue(); } }
        }
    }
}
