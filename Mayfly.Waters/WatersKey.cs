using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Resources;
using Mayfly.Extensions;


namespace Mayfly.Waters
{
    public partial class WatersKey
    {
        partial class WaterDataTable
        {
            public bool Check(WaterRow dataRow)
            {
                foreach (WaterRow waterRow in this.Rows)
                {
                    if (waterRow.ID == (int)dataRow["ID"])
                    {
                        if (waterRow.FullName == dataRow.FullName)
                        { return true; }
                    }
                    else
                    {
                        if (waterRow.FullName == dataRow.FullName)
                        { dataRow.SetID(waterRow); return true; }
                    }
                }
                return false;
            }

            public void Import(DataRow dataRow)
            {
                WaterRow waterRow = NewWaterRow();
                waterRow.Water = dataRow["Water"].ToString();
                waterRow.Type = int.Parse(dataRow["Type"].ToString());
                Rows.Add(waterRow);
                dataRow.SetID(waterRow);
            }

            //public bool UnknownWaterAction(DataRow ComparingWater,
            //    _Water.WaterDataTable Waters)
            //{
            //    // TODO: IMPLEMENT OWN DESIGNED DIALOG

            //    bool result = true;
            //    //string NewWater = WaterFullName(ComparingWater);

            //    //List<TaskDialogButton> Options = new List<TaskDialogButton>();

            //    //// OPTION 1: IMPORT TO REFERENCE
            //    //TaskDialogButton ButtonImport = new TaskDialogButton(TaskDialogResult.Import, Resources.Interface.WaterImport);
            //    //Options.Add(ButtonImport);

            //    //// OPTION 2: CHOOSE ANOTHER ONE FROM 'SAME NAMED' LIST
            //    ////foreach (t_WaterRow SameNamedWater in Waters.WatersNamed(NewWater))
            //    ////{
            //    ////    TaskDialogButton ButtonSelectOther = new TaskDialogButton(((int)SameNamedWater.ID).ToString(),
            //    ////        String.Format("Использовать {0} из справочника", WaterFullName(SameNamedWater)) + Mayfly.Interface.Break +
            //    ////        String.Format("{0} - это требуемый водоем", WaterDescription(SameNamedWater)));
            //    ////    Options.Add(ButtonSelectOther);
            //    ////}

            //    //TaskDialogResult Result = Mayfly.Service.DialogResult(LockingForm, Mayfly.Wild.Resources.Interface.Interface.ImportNewReferenceRecord,
            //    //    Resources.Interface.WaterMissingInstruction, string.Format(Resources.Interface.WaterMissing, NewWater),
            //    //    TaskDialogIcon.Information, true, Options.ToArray());

            //    //switch (Result)
            //    //{
            //    //    case TaskDialogResult.Import: 
            //              Waters.Import(ComparingWater);
            //    //        Waters.DataSet.WriteXml(Mayfly.Waters.UserSettings.Waters);
            //    //        break;
            //    //    case TaskDialogResult.Cancel:
            //    //        Waters.Import(ComparingWater);
            //    //        result = false;
            //    //        break;
            //    //    default:
            //    //        throw new NotImplementedException("REIMPLEMENT WATER ANALOG PICKUP");
            //    //        //int ID = (int)Result;
            //    //        //t_WaterRow Selected = FindByID((int)Result);
            //    //        //if (Selected == null) { }
            //    //        //else
            //    //        //{
            //    //        //    ComparingWater["ID"] = Selected.ID;
            //    //        //    ComparingWater["WaterName"] = Selected.WaterName;
            //    //        //    ComparingWater["Description"] = WaterDescription(Selected);
            //    //        //}
            //    //        break;
            //    //}
            //    return result;
            //}
        }

        partial class WaterRow
        {
            public string Description
            {
                get
                {
                    string result = Constants.Null;

                    switch ((WaterType)this.Type)
                    {
                        case WaterType.Stream:

                            if (this.IsOutflowNull())
                            {
                                result = Resources.Interface.StreamNotSpecified;
                            }
                            else
                            {
                                WaterRow outflow = (this.Table as WaterDataTable).FindByID(this.Outflow);

                                switch ((WaterType)outflow.Type)
                                {
                                    case WaterType.Tank:
                                        WaterRow motherStream = outflow.GetOutflow();
                                        if (motherStream == null)
                                        {
                                            if (this.IsMouthCoastNull())
                                            {
                                                result = String.Format(Resources.Interface.InflowOf, outflow.FullName);
                                            }
                                            else
                                            {
                                                switch (this.MouthCoast)
                                                {
                                                    case 0:
                                                        result = String.Format(Resources.Interface.StreamInflow,
                                                            Resources.Interface.Left, outflow.FullName);
                                                        break;
                                                    case 1:
                                                        result = String.Format(Resources.Interface.StreamInflow,
                                                            Resources.Interface.Right, outflow.FullName);
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (this.IsMouthCoastNull())
                                            {
                                                result = String.Format(Resources.Interface.InflowOf,
                                                    motherStream.FullName);
                                            }
                                            else
                                            {
                                                switch (this.MouthCoast)
                                                {
                                                    case 0:
                                                        result = String.Format(Resources.Interface.StreamInflow,
                                                            Resources.Interface.Left, motherStream.FullName);
                                                        break;
                                                    case 1:
                                                        result = String.Format(Resources.Interface.StreamInflow,
                                                            Resources.Interface.Right, motherStream.FullName);
                                                        break;
                                                }
                                            }
                                            result += String.Format(Resources.Interface.ShapeOf,
                                                outflow.FullName);
                                        }
                                        break;

                                    case WaterType.Stream:

                                        if (this.IsMouthCoastNull())
                                        {
                                            result = String.Format(Resources.Interface.InflowOf,
                                                outflow.FullName);
                                        }
                                        else
                                        {
                                            switch (this.MouthCoast)
                                            {
                                                case 0:
                                                    result = String.Format(Resources.Interface.StreamInflow,
                                                        Resources.Interface.Left, outflow.FullName);
                                                    break;
                                                case 1:
                                                    result = String.Format(Resources.Interface.StreamInflow,
                                                        Resources.Interface.Right, outflow.FullName);
                                                    break;
                                            }
                                        }
                                        break;


                                    case WaterType.Lake:
                                        result = String.Format(Resources.Interface.InflowOf, outflow.FullName);
                                        break;
                                }
                            }
                            break;

                        case WaterType.Lake:

                            if (this.IsKindNull())
                            {
                                result = Resources.Interface.LakeNotSpecified;
                            }
                            else
                            {
                                switch (this.Kind)
                                {
                                    case 0:
                                        if (this.IsOutflowNull())
                                        {
                                            result = String.Format(Resources.Interface.LakeFloodplain, string.Empty);
                                        }
                                        else
                                        {
                                            result = String.Format(Resources.Interface.LakeFloodplain,
                                              tableWater.FindByID(this.Outflow).FullName);
                                        }
                                        break;
                                    case 1:
                                        result = Resources.Interface.LakeInland;
                                        break;
                                }
                            }

                            break;

                        case WaterType.Tank:

                            if (!this.IsOutflowNull())
                            {
                                WaterRow outflow = tableWater.FindByID(this.Outflow);
                                if (outflow == null) this.SetOutflowNull();
                                else
                                {
                                    if (this.IsMouthToMouthNull())
                                    {
                                        result = String.Format(Resources.Interface.DamLocationOutflowOnly,
                                            outflow.FullName);
                                    }
                                    else
                                    {
                                        result = String.Format(Resources.Interface.DamLocation, this.MouthToMouth,
                                            outflow.FullName);
                                    }
                                }
                            }
                            else result = Resources.Interface.DamLocationNotSpecified;

                            break;
                    }
                    return result;
                }
            }

            public string FullName
            {
                get
                {
                    if (this.IsWaterNull())
                    {
                        switch ((WaterType)this.Type)
                        {
                            case WaterType.Stream:
                                return String.Format(Resources.Interface.TitleStream,
                                    Resources.Interface.Unnamed);

                            case WaterType.Lake:
                                return String.Format(Resources.Interface.TitleLake,
                                    Resources.Interface.Unnamed);

                            case WaterType.Tank:
                                switch (this.Kind)
                                {
                                    case 2:
                                        return String.Format(Resources.Interface.TitlePond,
                                            Resources.Interface.Unnamed);

                                    default:
                                        return String.Format(Resources.Interface.TitleTank,
                                            Resources.Interface.Unnamed);
                                }

                            default: return string.Empty;
                        }
                    }
                    else
                    {
                        switch ((WaterType)this.Type)
                        {
                            case WaterType.Tank: if (this.IsKindNull())
                                {
                                    return String.Format(Resources.Interface.TitleTank,
                                        this.Water);
                                }
                                else
                                {
                                    switch (this.Kind)
                                    {
                                        case 2:
                                            return String.Format(Resources.Interface.TitlePond,
                                                this.Water);

                                        default:
                                            return String.Format(Resources.Interface.TitleTank,
                                                this.Water);
                                    }
                                }
                            default:
                                {
                                    switch ((WaterType)this.Type)
                                    {
                                        case WaterType.Stream:
                                            return String.Format(Resources.Interface.TitleStream,
                                                this.Water);

                                        case WaterType.Lake:
                                            return String.Format(Resources.Interface.TitleLake,
                                                this.Water);

                                        case WaterType.Tank:
                                            return String.Format(Resources.Interface.TitleTank,
                                                this.Water);

                                        default:
                                            return string.Empty;
                                    }
                                }
                        }
                    }
                }
            }

            public WaterType WaterType
            {
                get
                {
                    return (WaterType)this.Type;
                }
            }



            public WaterRow[] GetInflows()
            {
                return GetInflows(true);
            }

            public WaterRow[] GetInflows(bool searchAllChildren)
            {
                List<WaterRow> result = new List<WaterRow>();

                WaterRow[] directInflows = (WaterRow[])tableWater.Select("Type = 1 AND Outflow = " +
                    this.ID, "MouthToMouth Desc");

                foreach (WaterRow inflow in directInflows)
                {
                    result.Add(inflow);

                    if (searchAllChildren)
                    {
                        result.AddRange(inflow.GetInflows());
                    }
                }

                return result.ToArray();
            }

            public WaterRow[] GetInflows(int order)
            {
                if (order < 1)
                    throw new ArgumentException("Order should be not less than 1");

                WaterRow[] directInflows = (WaterRow[])tableWater.Select("Type = 1 AND Outflow = " +
                    this.ID, "MouthToMouth Desc");

                if (order == 1)
                {
                    return directInflows;
                }
                else
                {
                    List<WaterRow> result = new List<WaterRow>();

                    foreach (WaterRow inflow in directInflows)
                    {
                        result.AddRange(inflow.GetInflows(order - 1));
                    }

                    return result.ToArray();
                }
            }

            public WaterRow[] GetWatershedLakes()
            {
                WaterRow[] inflows = this.GetInflows();

                List<WaterRow> result = new List<WaterRow>();

                foreach (WaterRow inflow in inflows)
                {
                    if (inflow.WaterType != Waters.WaterType.Lake) continue;
                    result.Add(inflow);
                }

                return result.ToArray();

            }

            public WaterRow[] GetFloodplaneLakes()
            {
                WaterRow[] inflows = this.GetInflows(1);

                List<WaterRow> result = new List<WaterRow>();

                foreach (WaterRow inflow in inflows)
                {
                    if (inflow.WaterType != Waters.WaterType.Lake) continue;
                    result.Add(inflow);
                }

                return result.ToArray();

            }

            public WaterRow GetPossibleOutflow(WaterRow outflow)
            {
                if ((WaterType)this.Type == WaterType.Stream)
                {
                    if (!this.IsMouthToMouthNull())
                    {
                        string expression = "Type = 3 AND Outflow = " + outflow.ID +
                            " AND MouthToMouth < " + this.MouthToMouth.ToString(CultureInfo.InvariantCulture) +
                            " AND (MouthToMouth + Length) > " + this.MouthToMouth.ToString(CultureInfo.InvariantCulture);
                        WaterRow[] possibleOutflows = (WaterRow[])tableWater.Select(expression);
                        if (possibleOutflows.Length > 0)
                        {
                            return possibleOutflows[0];
                        }
                    }
                }

                return null;
            }

            public WaterRow GetTerminalInflow(WaterRow destinationOutflow)
            {
                if (this.IsOutflowNull())
                {
                    return null;
                }
                else
                {
                    if (this.Outflow == destinationOutflow.ID)
                    {
                        return this;
                    }
                    else
                    {
                        WaterRow outflow = this.GetOutflow();

                        if (outflow == null)
                        {
                            return null;
                        }
                        else
                        {
                            return outflow.GetTerminalInflow(destinationOutflow);
                        }
                    }
                }
            }

            public WaterRow GetOutflow(int order)
            {
                int i = 0;
                WaterRow result = this;

                while (i < order && !result.IsOutflowNull())
                {
                    result = tableWater.FindByID(result.Outflow);

                    if (result == null)
                    {
                        return null;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (i == order)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }

            public WaterRow GetOutflow()
            {
                return this.GetOutflow(1);
            }



            public override string ToString()
            {
                return this.FullName + " (" + this.Description + ")";
            }

            public string[] GetOutflowChain()
            {
                List<string> result = new List<string>();
                WaterRow outflow = this;
                while (outflow != null) {
                    result.Add(outflow.FullName);
                    outflow = outflow.GetOutflow();
                }
                return result.ToArray();
            }

            public string GetPath(string separator)
            {
                return GetOutflowChain().Merge(separator);
            }

            public string GetPath()
            {
                return GetPath(" > ");
            }


            public int GetImageIndex()
            {
                int result = 0;

                switch ((WaterType)this.Type)
                {
                    case WaterType.Stream:
                        if (this.IsOutflowNull())
                        {
                            result = 1;
                        }
                        else
                        {
                            if (this.IsMouthCoastNull())
                            {
                                result = 2;
                            }
                            else
                            {
                                result = this.MouthCoast == 0 ? 3 : 4;
                            }
                        }
                        break;

                    case WaterType.Tank:
                        result = 5;
                        break;

                    case WaterType.Lake:
                        if (this.IsOutflowNull())
                        {
                            result = 8;
                        }
                        else
                        {
                            if (this.IsMouthCoastNull())
                            {
                                result = 2;
                            }
                            else
                            {
                                result = this.MouthCoast == 1 ? 6 : 7;
                            }
                        }

                        break;
                }

                return result;
            }


            public void AddWaterBlock(Report report)
            {
                switch (this.WaterType)
                {
                    case WaterType.Stream:
                        this.AddStreamBlock(report);
                        break;

                    case WaterType.Lake:
                        this.AddLakeBlock(report);
                        break;

                    case WaterType.Tank:
                        this.AddTankBlock(report);
                        break;
                }
            }

            public void AddStreamBlock(Report report)
            {
                report.AddSubtitle3(string.Format(Resources.Reports.Title,
                    this.FullName));

                ResourceManager resources = new ResourceManager(typeof(CardStream));

                Report.Table table1 = new Report.Table();

                table1.StartRow();
                table1.AddCellPrompt(Waters.Resources.Reports.Channel, this.GetPath(), 2);
                table1.EndRow();

                table1.StartRow();
                string bank = Constants.Null;

                if (!this.IsMouthCoastNull())
                {
                    switch (this.MouthCoast)
                    {
                        case 0:
                            bank = Waters.Resources.Interface.Left;
                            break;
                        case 1:
                            bank = Waters.Resources.Interface.Right;
                            break;
                    }
                }
                table1.AddCellPrompt(resources.GetString("labelBank.Text"),
                    this.IsMouthCoastNull() ? Constants.Null : bank);
                table1.AddCellPrompt(resources.GetString("labelMouthToMouth.Text"),
                    this.IsMouthToMouthNull() ? Constants.Null : this.MouthToMouth.ToString());
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(resources.GetString("labelLength.Text"),
                    this.IsLengthNull() ? Constants.Null : this.Length.ToString());
                table1.AddCellPrompt(resources.GetString("labelSpend.Text"),
                    this.IsConsumptionNull() ? Constants.Null : this.Consumption.ToString());
                table1.EndRow();

                table1.StartRow();
                table1.AddCellPrompt(resources.GetString("labelWatershed.Text"),
                    this.IsWatershedNull() ? Constants.Null : this.Watershed.ToString());
                table1.AddCellPrompt(resources.GetString("labelVolume.Text"),
                    this.IsVolumeNull() ? Constants.Null : this.Volume.ToString());
                table1.EndRow();

                table1.StartRow();
                table1.AddCell(); // PromptValue("Mouth coast", this.Description);
                table1.AddCellPrompt(resources.GetString("labelSlope.Text"),
                    this.IsSlopeNull() ? Constants.Null : this.Slope.ToString());
                table1.EndRow();

                report.AddTable(table1);
            }

            public void AddLakeBlock(Report report)
            {
                //report.AddSubtitle3("Water reference data");

                //ResourceManager resources = new ResourceManager(typeof(Waters.CardStream));

                //Report.Table table1 = new Report.Table();
                //report.StartLog(new string[] { "№",
                //        resources.GetString("labelName.Text"),
                //        resources.GetString("labelOutflow.Text"),	
                //        resources.GetString("labelBank.Text"),
                //        resources.GetString("labelMouthToMouth.Text"),
                //        resources.GetString("labelLength.Text"),
                //        resources.GetString("labelWatershed.Text")
                //    },
                //new int[] { 5, 30, 30, 10 });

                //table1.StartRow();
                //table1.AddCellRight(Math.Abs(this.ID));
                //table1.AddCell(this.FullName);
                //table1.AddCell(Constants.Null);
                //table1.AddCell(Constants.Null);
                //table1.AddCellRight(Constants.Null);

                //if (this.IsLengthNull())
                //{
                //    table1.AddCellRight(Constants.Null);
                //}
                //else
                //{
                //    table1.AddCellRight(this.Length.ToString("# ##0"));
                //}

                //if (this.IsWatershedNull())
                //{
                //    table1.AddCellRight(Constants.Null);
                //}
                //else
                //{
                //    table1.AddCellRight(this.Watershed.ToString("# ##0"));
                //}

                //table1.EndRow();
            }

            public void AddTankBlock(Report report)
            {
                //report.AddSubtitle3("Water reference data");

                //ResourceManager resources = new ResourceManager(typeof(Waters.CardStream));

                //Report.Table table1 = new Report.Table();
                //report.StartLog(new string[] { "№",
                //        resources.GetString("labelName.Text"),
                //        resources.GetString("labelOutflow.Text"),	
                //        resources.GetString("labelBank.Text"),
                //        resources.GetString("labelMouthToMouth.Text"),
                //        resources.GetString("labelLength.Text"),
                //        resources.GetString("labelWatershed.Text")
                //    },
                //new int[] { 5, 30, 30, 10 });

                //table1.StartRow();
                //table1.AddCellRight(Math.Abs(this.ID));
                //table1.AddCell(this.FullName);
                //table1.AddCell(Constants.Null);
                //table1.AddCell(Constants.Null);
                //table1.AddCellRight(Constants.Null);

                //if (this.IsLengthNull())
                //{
                //    table1.AddCellRight(Constants.Null);
                //}
                //else
                //{
                //    table1.AddCellRight(this.Length.ToString("# ##0"));
                //}

                //if (this.IsWatershedNull())
                //{
                //    table1.AddCellRight(Constants.Null);
                //}
                //else
                //{
                //    table1.AddCellRight(this.Watershed.ToString("# ##0"));
                //}

                //table1.EndRow();
            }

            public void AddWatershedBlock(Report report)
            {
                report.AddSubtitle3(string.Format(Resources.Reports.TitleWatershed, this.FullName, this.Description));

                ResourceManager resources = new ResourceManager(typeof(CardStream));

                Report.Table table1 = new Report.Table();

                for (int i = 1; i < int.MaxValue; i++)
                {
                    WaterRow[] inflows = this.GetInflows(i);

                    if (inflows.Length == 0) break;

                    double l = 0;

                    foreach (WaterRow inflow in inflows)
                    {
                        if (inflow.IsLengthNull()) continue;
                        l += inflow.Length;
                    }

                    table1.StartRow();
                    table1.AddCellPrompt(string.Format(Resources.Reports.InflowsN, i), inflows.Length);
                    table1.AddCellPrompt(string.Format(Resources.Reports.InflowsL, i), l);
                    table1.EndRow();
                }

                WaterRow[] floodlakes = this.GetFloodplaneLakes();

                table1.StartRow();
                if (floodlakes.Length > 0)
                {
                    double s = 0;

                    foreach (Waters.WatersKey.WaterRow floodlake in floodlakes)
                    {
                        if (floodlake.IsAreaNull()) continue;
                        s += floodlake.Area;
                    }

                    table1.AddCellPrompt(Resources.Reports.FloodLakesN, floodlakes.Length);
                    table1.AddCellPrompt(Resources.Reports.LakesS, s);
                }
                else
                {
                    table1.AddCellPrompt(Resources.Reports.FloodLakesN, Resources.Reports.LakesZero);
                    table1.AddCellPrompt(Resources.Reports.LakesS, Constants.Null);
                }
                table1.EndRow();

                WaterRow[] shedlakes = this.GetWatershedLakes();

                table1.StartRow();
                if (shedlakes.Length > 0)
                {
                    double s = 0;

                    foreach (WaterRow shedlake in shedlakes)
                    {
                        if (shedlake.IsAreaNull()) continue;
                        s += shedlake.Area;
                    }

                    table1.AddCellPrompt(Resources.Reports.ShedLakesN, shedlakes.Length);
                    table1.AddCellPrompt(Resources.Reports.LakesS, s);
                }
                else
                {
                    table1.AddCellPrompt(Resources.Reports.ShedLakesN, Waters.Resources.Reports.LakesZero);
                    table1.AddCellPrompt(Resources.Reports.LakesS, Constants.Null);
                }
                table1.EndRow();

                report.AddTable(table1);
            }
        }

        public void SaveToFile(string fileName)
        {
            File.WriteAllText(fileName, GetXml());
        }

        public string[] WaterNames
        {
            get
            {
                List<string> result = new List<string>();
                foreach (WatersKey.WaterRow waterRow in Water.Rows)
                {
                    result.Add(waterRow.FullName);
                }
                return result.ToArray();
            }
        }

        public WaterRow Find(string name)
        {
            foreach (WaterRow waterRow in Water.Rows)
            {
                if (waterRow.IsWaterNull()) continue;
                if (waterRow.Water == name)
                    return waterRow;
            }

            return null;
        }

        public WaterRow[] GetWatersNamed(string fullName)
        {
            List<WaterRow> result = new List<WaterRow>();

            foreach (WaterRow waterRow in Water.Rows)
            {
                if (waterRow.FullName == fullName)
                {
                    result.Add(waterRow);
                }
            }

            return result.ToArray();
        }

        public WaterRow[] GetWatersNameContaining(string query)
        {
            List<WaterRow> result = new List<WaterRow>();
            foreach (WaterRow waterRow in Water.Rows)
            {
                if (waterRow.FullName.ToUpperInvariant().Contains(query.ToUpperInvariant()))
                {
                    result.Add(waterRow);
                }
            }
            result.Sort(new SearchResultSorter(query));
            return result.GetRange(0, Math.Min(result.Count, UserSettings.SearchItemsCount)).ToArray();
        }

        public WaterRow[] GetRoots()
        {
            List<WaterRow> result = new List<WaterRow>();
            foreach (WaterRow waterRow in Water.Rows)
            {
                if (waterRow.IsOutflowNull())
                {
                    result.Add(waterRow);
                }
            }
            return result.ToArray();
        }

        public WaterRow[] GetOfType(WaterType type)
        {
            List<WaterRow> result = new List<WaterRow>();
            foreach (WaterRow waterRow in Water.Rows)
            {
                if (waterRow.IsTypeNull()) continue;

                if (waterRow.Type == (int)type)
                {
                    result.Add(waterRow);
                }
            }
            return result.ToArray();
        }

        public WaterRow[] GetStreams()
        {
            return GetOfType(WaterType.Stream);
        }

        public WaterRow[] GetLakes()
        {
            return GetOfType(WaterType.Lake);
        }

        public WaterRow[] GetTanks()
        {
            return GetOfType(WaterType.Tank);
        }

        public WaterRow[] GetInlandLakes()
        {
            List<WaterRow> result = new List<WaterRow>();
            foreach (WaterRow waterRow in GetLakes())
            {
                if (waterRow.IsOutflowNull())
                {
                    result.Add(waterRow);
                }
            }
            return result.ToArray();
        }



        #region Reporting

        public Report Report
        {
            get
            {
                Report report = new Report(FileSystem.GetFriendlyFiletypeName(UserSettings.Interface.Extension));

                ResourceManager StreamResources = new ResourceManager(typeof(CardStream));
                ResourceManager LakeResources = new ResourceManager(typeof(CardLake));
                ResourceManager TankResources = new ResourceManager(typeof(CardTank));

                report.AddSubtitle("Watercourses Drenaige System");
                Report.Table table1 = new Report.Table();
                table1.AddHeader(new string[] { "№",
                    (string)StreamResources.GetObject("labelName.Text"),
                    (string)StreamResources.GetObject("labelOutflow.Text"),	
                    (string)StreamResources.GetObject("labelBank.Text"),
                    (string)StreamResources.GetObject("labelMouthToMouth.Text"),
                    (string)StreamResources.GetObject("labelLength.Text"),
                    (string)StreamResources.GetObject("labelWatershed.Text")
                },
                new double[] { .05, .30, .30, .10 });

                foreach (WaterRow waterRow in GetRoots())
                {
                    if ((WaterType)waterRow.Type == WaterType.Stream)
                    {
                        AddRootStreamRow(table1, waterRow);
                    }
                }

                report.AddTable(table1);

                if (GetLakes().Length > 0)
                {
                    report.BreakPage();

                    report.AddSubtitle("Lakes");
                    Report.Table table2 = new Report.Table();
                    table2.AddHeader(new string[] { "№",
                        (string)LakeResources.GetObject("labelName.Text"),
                        (string)LakeResources.GetObject("labelOutflow.Text"),
                        (string)LakeResources.GetObject("labelWatershed.Text"),
                        (string)LakeResources.GetObject("labelArea.Text") },
                    new double[] { .05, .30, .30 });

                    foreach (WaterRow waterRow in GetLakes())
                    {
                        AddLakeRow(table2, waterRow);
                    }

                    report.AddTable(table2);
                }

                if (GetTanks().Length > 0)
                {
                    report.BreakPage();

                    report.AddSubtitle("Reservoirs");
                    Report.Table table3 = new Report.Table();
                    // TODO: Create resources!!!
                    table3.AddHeader(new string[] { "№",
                        "Название",
                        "Принадлежность к бассейну  реки",
                        "Общая площадь водосбора, км в кв.",
                        "Площадь зеркала, км в кв."
                    },
                    new double[] { .05, .30, .30 });

                    foreach (WaterRow waterRow in GetLakes())
                    {
                        AddLakeRow(table3, waterRow);
                    }

                    report.AddTable(table3);
                }

                report.EndBranded();

                return report;
            }
        }

        private void AddRootStreamRow(Report.Table table1, WaterRow waterRow)
        {
            table1.StartRow();
            table1.AddCellRight(Math.Abs(waterRow.ID));
            table1.AddCell(waterRow.FullName);
            table1.AddCell(Constants.Null);
            table1.AddCell(Constants.Null);
            table1.AddCellRight(Constants.Null);

            if (waterRow.IsLengthNull())
            {
                table1.AddCellRight(Constants.Null);
            }
            else
            {
                table1.AddCellRight(waterRow.Length, "# ##0");
            }

            if (waterRow.IsWatershedNull())
            {
                table1.AddCellRight(Constants.Null);
            }
            else
            {
                table1.AddCellRight(waterRow.Watershed, "# ##0");
            }

            table1.EndRow();

            foreach (WaterRow inflow in waterRow.GetInflows(false))
            {
                if ((WaterType)inflow.Type == WaterType.Stream)
                {
                    AddStreamRow(table1, inflow);
                }
            }
        }

        private void AddStreamRow(Report.Table table1, WaterRow waterRow)
        {
            table1.StartRow();
            table1.AddCellRight(Math.Abs(waterRow.ID));
            table1.AddCell(waterRow.FullName);

            if (waterRow.IsOutflowNull())
            {
                table1.AddCell(Constants.Null);
            }
            else
            {
                table1.AddCell(waterRow.GetOutflow().FullName);
            }

            if (waterRow.IsMouthCoastNull())
            {
                table1.AddCell(Constants.Null);
            }
            else
            {
                ResourceManager resources = new ResourceManager(typeof(CardStream));
                if (waterRow.MouthCoast == 0)
                {
                    table1.AddCell(resources.GetString("comboBoxBank.Items"));
                }
                else
                {
                    table1.AddCell(resources.GetString("comboBoxBank.Items" + waterRow.MouthCoast));
                }
            }

            if (waterRow.IsMouthToMouthNull())
            {
                table1.AddCellRight("-");
            }
            else
            {
                WaterRow outflow = waterRow.GetOutflow();

                switch ((WaterType)outflow.Type)
                {
                    case WaterType.Stream:
                        table1.AddCellRight(waterRow.MouthToMouth, "# ##0.0");
                        break;
                    case WaterType.Tank:
                    case WaterType.Lake:
                        if (outflow.IsMouthToMouthNull())
                        {
                            table1.AddCell();
                        }
                        else
                        {
                            table1.AddCellRight(outflow.MouthToMouth + waterRow.MouthToMouth, "# ##0.0");
                        }
                        break;
                }
            }

            if (waterRow.IsLengthNull())
            {
                table1.AddCellRight("-");
            }
            else
            {
                table1.AddCellRight(waterRow.Length, "# ##0");
            }

            if (waterRow.IsWatershedNull())
            {
                table1.AddCellRight("-");
            }
            else
            {
                table1.AddCellRight(waterRow.Watershed, "# ##0");
            }

            table1.EndRow();

            foreach (WaterRow inflow in waterRow.GetInflows(false))
            {
                if ((WaterType)inflow.Type == WaterType.Stream)
                {
                    AddStreamRow(table1, inflow);
                }
            }
        }

        private void AddLakeRow(Report.Table table1, WaterRow waterRow)
        {
            table1.StartRow();
            table1.AddCellRight(Math.Abs(waterRow.ID));
            table1.AddCell(waterRow.FullName);

            if (waterRow.IsOutflowNull())
            {
                table1.AddCell(Constants.Null);
            }
            else
            {
                table1.AddCell(waterRow.GetOutflow().FullName);
            }

            if (waterRow.IsWatershedNull())
            {
                table1.AddCellRight(Constants.Null);
            }
            else
            {
                table1.AddCellRight(waterRow.Watershed, "# ##0");
            }

            if (waterRow.IsAreaNull())
            {
                table1.AddCellRight(Constants.Null);
            }
            else
            {
                table1.AddCellRight(waterRow.Area, "# ##0");
            }

            table1.EndRow();
        }

        #endregion
    }
}
