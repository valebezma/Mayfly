using Mayfly.Controls;
using Mayfly.Extensions;
using Mayfly.TaskDialogs;
using Mayfly.Wild;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Mayfly.Waters.Controls;
using Mayfly.Fish;

namespace Mayfly.Fish.Explorer
{
    public partial class WizardOb : Form
    {
        internal MainForm Explorer { get; set; }

        string storage { get { return Path.Combine(IO.UserFolder, "surveys"); } }

        string archive { get { return Path.Combine(IO.UserFolder, "surveyarchive"); } }

        string filename { get; set; }

        bool finished = false;

        Samplers samplers;

        StratifiedControl StratifiedControl;

        public Surveys Survey { get; set; }

        Surveys.FleetRow selectedFleetRow;



        public WizardOb()
        {
            InitializeComponent();

            listViewPhantoms.Shine();

            Log.Write(EventType.WizardStarted, "Observations wizard is started.");

            waterSelector.CreateList();
            waterSelector.Index = Wild.UserSettings.WatersIndex;

            samplers = Fish.UserSettings.SamplersIndex;
            columnSampler.DataSource = samplers.GetPassives();
            columnSampler.DisplayMember = "Sampler";
            columnSampler.ValueMember = "ShortName";
            
            columnMesh.ValueType = typeof(int);
            columnLength.ValueType = typeof(double);
            columnHeight.ValueType = typeof(double);

            columnTimepoint.ValueType = typeof(DateTime);

            textBoxInvestigator.Text = Mayfly.UserSettings.Username;

            Survey = new Surveys();

            buttonAddFleet_Click(buttonAddFleet, new EventArgs());
            buttonFleet_Click(flowLayoutPanelRounds.Controls[0], new EventArgs());
            ((RadioButton)flowLayoutPanelRounds.Controls[0]).Checked = true;

            foreach (Equipment.UnitsRow unitRow in Fish.UserSettings.Equipment.Units)
            {
                Samplers.SamplerRow samplerRow = Fish.UserSettings.SamplersIndex.Sampler.FindByID(unitRow.SamplerID);

                if (!samplerRow.IsPassive()) continue;

                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGears);
                if (!unitRow.IsSamplerIDNull()) gridRow.Cells[columnSampler.Index].Value = samplerRow.ShortName;
                if (!unitRow.IsMeshNull()) gridRow.Cells[columnMesh.Index].Value = unitRow.Mesh;
                if (!unitRow.IsLengthNull()) gridRow.Cells[columnLength.Index].Value = unitRow.Length;
                if (!unitRow.IsHeightNull()) gridRow.Cells[columnHeight.Index].Value = unitRow.Height;
                spreadSheetGears.Rows.Add(gridRow);
            }

            ResetLetters();

            SearchForUnfinished();
        }



        public void SearchForUnfinished()
        {
            if (!Directory.Exists(storage)) Directory.CreateDirectory(storage);

            string[] phantoms = Directory.GetFiles(storage);

            if (phantoms.Length == 0) // Just pass first page
            {
                wizardPagePhantoms.Suppress = true;
            }
            else // Getting user to select unfinished file
            {
                listViewPhantoms.Items.Clear();

                foreach (string filename in phantoms)
                {
                    ListViewItem item = new ListViewItem();
                    item.Name = filename;
                    item.Text = File.GetLastWriteTime(filename).ToString();

                    Surveys data = new Surveys();
                    data.ReadXml(filename);
                    item.SubItems.Add(data.Gear.Count.ToString());
                    item.SubItems.Add(data.Action.Count.ToString());
                    listViewPhantoms.Items.Add(item);
                }
            }
        }

        private void LoadData(string _filename)
        {
            this.filename = _filename;
            Survey = new Surveys();
            Survey.ReadXml(filename);
            LoadData();
        }

        private void LoadData()
        {
            if (Survey.Survey.Count > 0)
            {
                Waters.WatersKey.WaterRow waterRow = waterSelector.Index.Water.NewWaterRow();

                if (!Survey.Survey[0].IsWaterNameNull()) waterRow.Water = Survey.Survey[0].WaterName;
                if (!Survey.Survey[0].IsWaterTypeNull()) waterRow.Type = Survey.Survey[0].WaterType;

                waterSelector.WaterObject = waterRow;
            }

            while (flowLayoutPanelRounds.Controls.Count > 1) {
                flowLayoutPanelRounds.Controls.RemoveAt(0);
            }
            
            foreach (Surveys.FleetRow fleetRow in Survey.Fleet) {
                AddFleetButton(fleetRow);
            }

            if (flowLayoutPanelRounds.Controls.Count > 1) {
                buttonFleet_Click(flowLayoutPanelRounds.Controls[0], new EventArgs());
                ((RadioButton)flowLayoutPanelRounds.Controls[0]).Checked = true;
            }

            wizardPageGears.AllowNext = Survey.Gear.Count > 0;

            comboBoxFleet.DataSource = Survey.Fleet.Select(null, "Name asc");
        }


        private void SaveData()
        {
            Surveys.SurveyRow commonRow = Survey.Survey.Count == 0 ?
                Survey.Survey.NewSurveyRow() : Survey.Survey[0];

            if (Survey.Survey.Count == 0)
            {
                Survey.Survey.AddSurveyRow(commonRow);
            }

            if (waterSelector.WaterObject == null)
            {
                commonRow.SetWaterNameNull();
                commonRow.SetWaterTypeNull();
            }
            else
            {
                if (waterSelector.WaterObject.IsWaterNull()) commonRow.SetWaterNameNull(); else commonRow.WaterName = waterSelector.WaterObject.Water;
                if (waterSelector.WaterObject.IsTypeNull()) commonRow.SetWaterTypeNull(); else commonRow.WaterType = waterSelector.WaterObject.Type;
            }

            foreach (Surveys.ActionRow actRow in Survey.Action)
            {
                actRow.SurveyRow = commonRow;
            }
        }

        private void ResetLetters()
        {
            foreach (DataGridViewRow gridRow in spreadSheetGears.Rows)
            {
                if (gridRow.IsNewRow) continue;
                gridRow.HeaderCell.Value = gridRow.Index.ToLetter();
            }

            buttonAddFleet.Enabled = (Survey.EmptyFleetCount == 0);
        }

        private void SaveFleet()
        {
            foreach (DataGridViewRow gridRow in spreadSheetGears.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SaveGear(gridRow);
            }
        }

        private Surveys.GearRow SaveGear(DataGridViewRow gridRow)
        {
            Surveys.GearRow gearRow;

            if (gridRow.Tag == null)
            {
                gearRow = Survey.Gear.NewGearRow();
                gearRow.Name = gridRow.HeaderCell.Value.ToString();
                Survey.Gear.AddGearRow(gearRow);

                gridRow.Tag = gearRow;
            }

            gearRow = (Surveys.GearRow)gridRow.Tag;

            if (gridRow.Cells[columnSampler.Index].Value == null)
            {
                if (gridRow.Index > 0)
                    gridRow.Cells[columnSampler.Index].Value = spreadSheetGears[columnSampler.Index, gridRow.Index - 1].Value;
            }

            if (gridRow.Cells[columnSampler.Index].Value == null) gearRow.SetSamplerIDNull();
            else gearRow.SamplerID = samplers.Sampler.FindByCode((string)gridRow.Cells[columnSampler.Index].Value).ID;

            gearRow.Name = gridRow.HeaderCell.Value.ToString();

            gearRow.FleetRow = selectedFleetRow;

            if (gridRow.Cells[columnMesh.Index].Value == null) gearRow.SetMeshNull();
            else gearRow.Mesh = (int)gridRow.Cells[columnMesh.Index].Value;

            if (gridRow.Cells[columnLength.Index].Value == null) gearRow.SetLengthNull();
            else gearRow.Length = (double)gridRow.Cells[columnLength.Index].Value;

            if (gridRow.Cells[columnHeight.Index].Value == null) gearRow.SetHeightNull();
            else gearRow.Height = (double)gridRow.Cells[columnHeight.Index].Value;

            return gearRow;
        }

        private RadioButton GetButton(Surveys.FleetRow fleetRow)
        {
            foreach (Control ctrl in flowLayoutPanelRounds.Controls)
            {
                if (!(ctrl is RadioButton)) continue;

                if (((RadioButton)ctrl).Tag == fleetRow) return (RadioButton)ctrl;
            }

            return null;
        }



        private RadioButton AddFleetButton(Surveys.FleetRow fleetRow)
        {
            RadioButton button = new RadioButton();
            button.Appearance = Appearance.Button;
            button.FlatStyle = FlatStyle.System;
            button.Margin = new Padding(0, 0, 3, 3);
            button.Text = fleetRow.Name;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            button.Size = new System.Drawing.Size(100, 23);
            button.Tag = fleetRow;
            button.Click += buttonFleet_Click;

            flowLayoutPanelRounds.Controls.Add(button);
            flowLayoutPanelRounds.Controls.Remove(buttonAddFleet);
            flowLayoutPanelRounds.Controls.Add(buttonAddFleet);

            return button;
        }

        void UpdateGears(Surveys.FleetRow fleetRow)
        {
            spreadSheetGears.Rows.Clear();

            foreach (Surveys.GearRow gearRow in fleetRow.GetGearRows())
            {
                DataGridViewRow gridRow = new DataGridViewRow();
                gridRow.CreateCells(spreadSheetGears);
                if (!gearRow.IsNameNull()) gridRow.HeaderCell.Value = gearRow.Name;
                if (!gearRow.IsSamplerIDNull()) gridRow.Cells[columnSampler.Index].Value = samplers.Sampler.FindByID(gearRow.SamplerID).ShortName;
                if (!gearRow.IsMeshNull()) gridRow.Cells[columnMesh.Index].Value = gearRow.Mesh;
                if (!gearRow.IsLengthNull()) gridRow.Cells[columnLength.Index].Value = gearRow.Length;
                if (!gearRow.IsHeightNull()) gridRow.Cells[columnHeight.Index].Value = gearRow.Height;
                gridRow.Tag = gearRow;
                spreadSheetGears.Rows.Add(gridRow);
            }

            spreadSheetGears.Enabled = true;
            ResetLetters();

            contextMoveTo.DropDownItems.Clear();

            foreach (Surveys.FleetRow _fleetRow in Survey.Fleet)
            {
                if (_fleetRow == fleetRow) continue;

                ToolStripItem ddi = contextMoveTo.DropDownItems.Add(_fleetRow.Name);
                ddi.Tag = _fleetRow;
                ddi.Click += MoveTo_Click;
            }
        }

        void UpdateSchedule(Surveys.FleetRow fleetRow)
        {
            spreadSheetSchedule.ClearInsertedColumns();

            foreach (Surveys.GearRow equipmentRow in fleetRow.GetGearRows())
            {
                InsertColumn(equipmentRow);
            }

            spreadSheetSchedule.Rows.Clear();

            foreach (Surveys.TimelineRow timeRow in Survey.Timeline.Select(null, "Timepoint asc"))
            {
                bool i = false;

                foreach (Surveys.ActionRow actRow in timeRow.GetActionRows())
                {
                    if (actRow.GearRow.FleetRow == fleetRow)
                    {
                        i = true;
                        break;
                    }
                }

                if (!i) continue;

                DataGridViewRow gridRow = new DataGridViewRow();

                gridRow.CreateCells(spreadSheetSchedule);
                gridRow.Cells[columnTimepoint.Index].Value = timeRow.Timepoint;
                gridRow.Tag = timeRow;
                spreadSheetSchedule.Rows.Add(gridRow);

                foreach (Surveys.ActionRow actRow in timeRow.GetActionRows())
                {
                    if (spreadSheetSchedule.GetColumn(actRow.GearRow.Name) == null) continue;

                    gridRow.Cells[actRow.GearRow.Name].Tag = actRow;
                    gridRow.Cells[actRow.GearRow.Name].Value = (EquipmentEvent)actRow.Type;
                }
            }

            UpdateEnvironment();
        }

        private void InsertColumn(Surveys.GearRow gearRow)
        {
            //DataGridViewImageColumn
            SpreadSheetIconTextBoxColumn gridColumn = (SpreadSheetIconTextBoxColumn)spreadSheetSchedule.GetColumn(gearRow.Name);

            if (gridColumn == null)
            {
                gridColumn = spreadSheetSchedule.InsertIconColumn(gearRow.Name,
                    gearRow.ToString(), typeof(EquipmentEvent));
            }

            gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridColumn.Width = 80;
            gridColumn.ReadOnly = true;
            gridColumn.Resizable = DataGridViewTriState.False;
            gridColumn.DefaultCellStyle.Format = "g";
            //gridColumn.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 0, 25, 0);
            gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            gridColumn.Tag = gearRow;
        }

        //private void UpdateSchedule()
        //{
        //    foreach (DataGridViewColumn gridColumn in spreadSheetSchedule.GetInsertedColumns())
        //    {
        //        gridColumn.Name = ((Surveys.GearRow)gridColumn.Tag).Name;
        //        gridColumn.HeaderText = ((Surveys.GearRow)gridColumn.Tag).ToString();
        //    }
        //}

        private void UpdateEnvironment()
        {
            foreach (DataGridViewRow gridRow in spreadSheetSchedule.Rows)
            {
                Surveys.TimelineRow timelineRow = (Surveys.TimelineRow)gridRow.Tag;

                if (timelineRow != null && !timelineRow.IsWeatherNull())
                {
                    gridRow.Cells[ColumnEnvironment.Index].Value = timelineRow.WeatherConditions;
                }
                else
                {
                    gridRow.Cells[ColumnEnvironment.Index].Value = null;
                }
            }
        }

        private EquipmentEvent LastAction(DataGridViewCell gridCell)
        {
            EquipmentEvent action = EquipmentEvent.NoAction;

            for (int i = gridCell.RowIndex - 1; i >= 0; i--)
            {
                if (spreadSheetSchedule[gridCell.ColumnIndex, i].Value == null) continue;
                EquipmentEvent currentAction = (EquipmentEvent)spreadSheetSchedule[gridCell.ColumnIndex, i].Value;
                if (currentAction != EquipmentEvent.NoAction) { action = currentAction; break; }
            }

            return action;
        }

        private EquipmentEvent NextAction(DataGridViewCell gridCell)
        {
            DataGridViewCell nextActionCell = NextActionCell(gridCell);
            if (nextActionCell == null) return EquipmentEvent.NoAction;
            else return (EquipmentEvent)nextActionCell.Value;
        }

        private DataGridViewCell NextActionCell(DataGridViewCell gridCell)
        {
            for (int i = gridCell.RowIndex + 1; i < spreadSheetSchedule.RowCount; i++)
            {
                if (spreadSheetSchedule[gridCell.ColumnIndex, i].Value == null) continue;
                EquipmentEvent currentAction = (EquipmentEvent)spreadSheetSchedule[gridCell.ColumnIndex, i].Value;
                if (currentAction != EquipmentEvent.NoAction) { return spreadSheetSchedule[gridCell.ColumnIndex, i]; }
            }

            return null;
        }

        private List<EquipmentEvent> AvailableEvents(DataGridViewCell gridCell)
        {
            return Service.AvailableEvents(LastAction(gridCell));
        }

        private void InspectFurther(DataGridViewCell gridCell)
        {
            //TODO: improve!

            EquipmentEvent action = (EquipmentEvent)gridCell.Value;
            DataGridViewCell nextActionCell = NextActionCell(gridCell);
            EquipmentEvent nextAction = NextAction(gridCell);

            switch (action)
            {
                case EquipmentEvent.NoAction:
                    switch (nextAction)
                    {
                        case EquipmentEvent.Inspection:
                            break;
                    }

                    break;
                case EquipmentEvent.Setting:
                    switch (nextAction)
                    {
                        case EquipmentEvent.Setting:
                            nextActionCell.Value = EquipmentEvent.Inspection;
                            break;
                    }

                    break;
                case EquipmentEvent.Inspection:
                    switch (nextAction)
                    {
                        case EquipmentEvent.Setting:
                            nextActionCell.Value = EquipmentEvent.Inspection;
                            break;
                    }
                    break;
                case EquipmentEvent.Removing:
                    switch (nextAction)
                    {
                        case EquipmentEvent.Inspection:
                            nextActionCell.Value = EquipmentEvent.Setting;
                            break;
                        case EquipmentEvent.Removing:
                            nextActionCell.Value = EquipmentEvent.NoAction;
                            break;
                    }
                    break;
            }
        }

        private void ChangeIcon(DataGridView sheet, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell gridCell = sheet[e.ColumnIndex, e.RowIndex];
            List<EquipmentEvent> available = AvailableEvents(gridCell);

            if (gridCell.Value == null)
            {
                gridCell.Value = available[0];

                if (sheet[0, e.RowIndex].Value == null)
                {
                    sheet[0, e.RowIndex].Value =
                         e.RowIndex == 0 || (sheet[0, e.RowIndex - 1].Value == null) ?
                        DateTime.Now : ((DateTime)sheet[0, e.RowIndex - 1].Value).AddHours(12);

                    sheet.NotifyCurrentCellDirty(true);
                    sheet.Update();
                }
            }
            else
            {
                EquipmentEvent value = (EquipmentEvent)gridCell.Value;
                int index = available.IndexOf(value);
                if (index < available.Count - 1)
                {
                    gridCell.Value = available[index + 1];
                }
                else
                {
                    gridCell.Value = available[0];
                }
            }

        }

        private void SetEvent(DataGridViewCell gridCell, EquipmentEvent equipmentEvent)
        {
            gridCell.Value = equipmentEvent;
        }



        private void WizardOb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finished)
            {
                tdbExport.Enabled = Survey.ContainsCatchData;

                TaskDialogButton tdb = tdClose.ShowDialog(this);

                if (tdb == tdbArchive)
                {
                    if (string.IsNullOrEmpty(filename)) filename = Path.Combine(storage, string.Format("{0:yyyyMMdd-HHmmss}.ini", DateTime.Now));
                    SaveData();
                    Survey.WriteXml(filename);
                    if (!Directory.Exists(archive)) Directory.CreateDirectory(archive);
                    File.Move(filename, Path.Combine(archive, Path.GetFileName(filename)));
                }

                if (tdb == tdbShelve)
                {
                    buttonSaveAppData_Click(buttonSaveAppData, e);
                }

                if (tdb == tdbExport)
                {
                    buttonExport_Click(buttonExport, e);
                }

                if (tdb == tdbCancelClose)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void wizardControl1_Cancelling(object sender, EventArgs e)
        {
            Close();
        }



        #region Phantoms

        private void listViewPhantoms_ItemActivate(object sender, EventArgs e)
        {
            wizardControl1.NextPage(wizardPageSchedule);
        }

        private void wizardPagePhantoms_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (listViewPhantoms.SelectedItems.Count > 0)
            {
                LoadData(listViewPhantoms.SelectedItems[0].Name);
            }
        }

        private void buttonClearPhantoms_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewPhantoms.Items)
            {
                File.Move(item.Name, Path.Combine(archive, Path.GetFileName(item.Name)));
                item.Remove();
            }

            wizardControl1.NextPage();
            wizardPagePhantoms.Suppress = true;
        }

        #endregion



        #region Gears

        void buttonFleet_Click(object sender, EventArgs e)
        {
            selectedFleetRow = (Surveys.FleetRow)((RadioButton)sender).Tag;
            UpdateGears(selectedFleetRow);
        }

        private void spreadSheetGears_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Tag == null) return;
            Survey.Gear.RemoveGearRow((Surveys.GearRow)e.Row.Tag);
        }

        private void spreadSheetGears_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ResetLetters();
        }

        private void spreadSheetGears_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.HeaderCell.Value = e.Row.Index.ToLetter();
        }

        private void spreadSheetGears_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (spreadSheetGears.Rows[e.RowIndex].IsNewRow) return;
            if (spreadSheetGears.Rows[e.RowIndex].HeaderCell.Value == null)
                spreadSheetGears.Rows[e.RowIndex].HeaderCell.Value = e.RowIndex.ToLetter();
            SaveGear(spreadSheetGears.Rows[e.RowIndex]);

            wizardPageGears.AllowNext = Survey.Gear.Count > 0;
            buttonAddFleet.Enabled = (Survey.EmptyFleetCount == 0);
        }

        private void contextGear_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMoveTo.Enabled = Survey.Fleet.Count > 1;
        }

        private void MoveTo_Click(object sender, EventArgs e)
        {
            Surveys.FleetRow fleetRow = (Surveys.FleetRow)((ToolStripItem)sender).Tag;

            foreach (DataGridViewRow gridRow in spreadSheetGears.SelectedRows)
            {
                Surveys.GearRow gearRow = SaveGear(gridRow);
                gearRow.SetNameNull();
                gearRow.FleetRow = fleetRow;
            }

            RadioButton rb = GetButton(fleetRow);

            buttonFleet_Click(rb, new EventArgs());
            rb.Checked = true;
        }

        private void buttonAddFleet_Click(object sender, EventArgs e)
        {
            Surveys.FleetRow fleetRow = Survey.Fleet.NewFleetRow();
            fleetRow.Name = string.Format(Resources.Interface.Interface.FleetMask, Survey.Fleet.Count + 1);
            Survey.Fleet.AddFleetRow(fleetRow);

            RadioButton newButton = AddFleetButton(fleetRow);
            newButton.Focus();
            newButton.PerformClick();
        }

        private void wizardPageGears_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            SaveFleet();

            // Save Equipment separately from Survey to equipment.ini

            comboBoxFleet.DataSource = Survey.Fleet.Select(null,"Name asc");
        }

        #endregion



        #region Schedule

        private void comboBoxFleet_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSchedule((Surveys.FleetRow)comboBoxFleet.SelectedItem);
        }

        private void spreadSheetSchedule_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (spreadSheetSchedule.Rows[e.RowIndex].IsNewRow) return;

            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == columnTimepoint.Index) { }
            else if (e.ColumnIndex == ColumnEnvironment.Index) { }
            else {
                contextCatch.Show(System.Windows.Forms.Cursor.Position);
            }
        }

        private void spreadSheetSchedule_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        { }

        private void spreadSheetSchedule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            if (e.RowIndex == -1) return;

            DataGridViewRow gridRow = spreadSheetSchedule.Rows[e.RowIndex];


            if (e.ColumnIndex == columnTimepoint.Index)
            {
                Surveys.TimelineRow timelineRow;

                if (gridRow.Tag == null)
                {
                    timelineRow = Survey.Timeline.NewTimelineRow();
                    Survey.Timeline.AddTimelineRow(timelineRow);
                    gridRow.Tag = timelineRow;
                }
                else
                {
                    timelineRow = (Surveys.TimelineRow)gridRow.Tag;
                }

                timelineRow.Timepoint = (DateTime)spreadSheetSchedule[columnTimepoint.Index, e.RowIndex].Value;
            }
            else if (e.ColumnIndex == ColumnEnvironment.Index) { }
            else {

                TextAndImageCell gridCell = (TextAndImageCell)spreadSheetSchedule[e.ColumnIndex, e.RowIndex];

                if (gridCell.Value == null)
                {
                    gridCell.Image = null;
                    if (gridCell.Tag != null)
                    {
                        Surveys.ActionRow actionRow = (Surveys.ActionRow)gridCell.Tag;
                        Survey.Action.RemoveActionRow(actionRow);
                        gridCell.Tag = null;
                    }
                }
                else
                {
                    Surveys.ActionRow actionRow;

                    if (gridCell.Tag == null)
                    {
                        actionRow = Survey.Action.NewActionRow();
                        actionRow.TimelineRow = (Surveys.TimelineRow)gridRow.Tag;
                        actionRow.GearRow = (Surveys.GearRow)spreadSheetSchedule.Columns[e.ColumnIndex].Tag;
                        Survey.Action.AddActionRow(actionRow);

                        gridCell.Tag = actionRow;
                    }
                    else
                    {
                        actionRow = (Surveys.ActionRow)gridCell.Tag;
                    }

                    actionRow.Type = (int)gridCell.Value;

                    switch ((EquipmentEvent)gridCell.Value)
                    {
                        case EquipmentEvent.NoAction:
                            gridCell.Image = null;
                            break;
                        case EquipmentEvent.Setting:
                            gridCell.Image = Properties.Resources.down;
                            break;
                        case EquipmentEvent.Inspection:
                            gridCell.Image = actionRow.IsCatchXMLNull() ? Properties.Resources.round : Properties.Resources.round_filled;
                            break;
                        case EquipmentEvent.Removing:
                            gridCell.Image = actionRow.IsCatchXMLNull() ? Properties.Resources.up : Properties.Resources.up_filled;
                            break;
                    }

                    InspectFurther(gridCell);
                }
            }

            wizardPageSchedule.AllowNext = Survey.Action.Count > 0;
        }

        private void spreadSheetSchedule_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Index > 1)
            {
                e.Column.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(0, 0, e.Column.Width / 2, 0);
            }
        }

        private void spreadSheetSchedule_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (e.Row.Index > 0)
            {
                //DateTime prev = (DateTime)spreadSheetSchedule[columnTimepoint.Name, e.Row.Index - 1].Value;
                //e.Row.Cells[columnTimepoint.Name] = prev.AddHours(12);
                //spreadSheetSchedule.NotifyCurrentCellDirty(true);
            }
        }



        private void contextWeather_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gridRow in spreadSheetSchedule.SelectedRows)
            {
                Surveys.TimelineRow timelineRow = (Surveys.TimelineRow)gridRow.Tag;
                Environment environment = new Environment(timelineRow);
                environment.SetFriendlyDesktopLocation(gridRow.Cells[ColumnEnvironment.Index]);
                environment.FormClosing += weather_FormClosing;
                environment.ShowDialog();
            }
        }

        private void weather_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateEnvironment();
        }

        private void contextExportCards_Click(object sender, EventArgs e)
        {
            if (fbDialogSaveCards.ShowDialog(this) == DialogResult.OK)
            {
                foreach (DataGridViewRow gridRow in spreadSheetSchedule.SelectedRows)
                {
                    Surveys.TimelineRow timelineRow = (Surveys.TimelineRow)gridRow.Tag;
                    timelineRow.ExportCards(fbDialogSaveCards.SelectedPath);
                }
            }
        }



        private void contextCatch_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<EquipmentEvent> available = AvailableEvents(spreadSheetSchedule.CurrentCell);

            contextSet.Enabled = available.Contains(EquipmentEvent.Setting);
            contextInspect.Enabled = available.Contains(EquipmentEvent.Inspection);
            contextRemove.Enabled = available.Contains(EquipmentEvent.Removing);

            contextLocation.Enabled = spreadSheetSchedule.CurrentCell.Value is EquipmentEvent &&
                ((EquipmentEvent)spreadSheetSchedule.CurrentCell.Value == EquipmentEvent.Setting);

            contextClear.Enabled =
                spreadSheetSchedule.CurrentCell.Value is EquipmentEvent;

            contextEditCatch.Enabled =
            //contextExpressCatch.Enabled =
                spreadSheetSchedule.CurrentCell.Value is EquipmentEvent &&
                ((EquipmentEvent)spreadSheetSchedule.CurrentCell.Value == EquipmentEvent.Inspection ||
                (EquipmentEvent)spreadSheetSchedule.CurrentCell.Value == EquipmentEvent.Removing);

        }

        private void contextSet_Click(object sender, EventArgs e)
        {
            SetEvent(spreadSheetSchedule.CurrentCell, EquipmentEvent.Setting);
        }

        private void contextInspect_Click(object sender, EventArgs e)
        {
            SetEvent(spreadSheetSchedule.CurrentCell, EquipmentEvent.Inspection);
        }

        private void contextRemove_Click(object sender, EventArgs e)
        {
            SetEvent(spreadSheetSchedule.CurrentCell, EquipmentEvent.Removing);
        }

        private void contextClear_Click(object sender, EventArgs e)
        {
            spreadSheetSchedule.CurrentCell.Value = null;
        }

        private void contextEditCatch_Click(object sender, EventArgs e)
        {
            Catch card = new Catch((TextAndImageCell)spreadSheetSchedule.CurrentCell);
            card.TotalChanged += card_TotalChanged;
            card.Show();
        }

        private void card_TotalChanged(object sender, EventArgs e)
        {
            Catch card = (Catch)sender;

            switch ((EquipmentEvent)card.GridCell.Value)
            {
                case EquipmentEvent.Inspection:
                    (card.GridCell).Image = card.Data.Solitary.Quantity > 0 ?
                        Properties.Resources.round_filled : Properties.Resources.round;
                    break;
                case EquipmentEvent.Removing:
                    (card.GridCell).Image = card.Data.Solitary.Quantity > 0 ?
                        Properties.Resources.up_filled : Properties.Resources.up;
                    break;
            }

            if (StratifiedControl != null)
            {
                StratifiedControl.UpdateSample(Survey.GetCombinedData());
            }
        }

        //private void contextExpressCatch_Click(object sender, EventArgs e)
        //{
        //    if (FileSystem.InterfacePictures.OpenDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        ExpressCatch expressCatch = new ExpressCatch((TextAndImageCell)spreadSheetSchedule.CurrentCell);
        //        expressCatch.FormClosed += expressCatch_FormClosed;
        //        expressCatch.Show(this);
        //    }
        //}

        //private void expressCatch_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    ExpressCatch expressCatch = (ExpressCatch)sender;

        //    if (expressCatch.DialogResult == DialogResult.OK)
        //    {
        //        Catch card = new Catch(expressCatch.GridCell);
        //        card.TotalChanged += card_TotalChanged;
        //        card.Show();
        //    }
        //}



        private void buttonClearSchedule_Click(object sender, EventArgs e)
        {
            if (Survey.GetCombinedData().GetStack().Quantity() > 0)
            {
                if (tdClearSchedule.ShowDialog(this) == tdbClear)
                {
                    spreadSheetSchedule.Rows.Clear();
                }
            }
            else
            {
                spreadSheetSchedule.Rows.Clear();
            }
        }

        private void checkBoxControl_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxControl.Checked)
            {
                if (StratifiedControl == null)
                {
                    StratifiedControl = new StratifiedControl(Survey.GetCombinedData());
                    StratifiedControl.ControlBox = false;
                    StratifiedControl.Height = this.Height;
                    StratifiedControl.SetFriendlyDesktopLocation(FindForm(), FormLocation.NextToHost);
                }

                StratifiedControl.Show(FindForm());
            }
            else
            {
                if (StratifiedControl != null)
                {
                    StratifiedControl.Hide();
                }
            }
        }

        private void buttonPrintCribnote_Click(object sender, EventArgs e)
        {
            Survey.GetCombinedData().GetStack().GetStratifiedCribnote().Run();
        }

        #endregion



        #region Finish

        private void wizardPageSave_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            checkBoxKeep.Enabled = filename.IsAcceptable();
        }

        private void buttonSaveAppData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filename)) filename = Path.Combine(storage, string.Format("{0:yyyyMMdd-HHmmss}.ini", DateTime.Now));
            SaveData();
            Survey.WriteXml(filename);

            finished = true;
            Log.Write(EventType.WizardEnded, "Observations wizard is finished with saving observations to system file.");
            Close();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (fbDialogSaveCards.ShowDialog(this) == DialogResult.OK)
            {
                SaveData();

                string[] filenames = Survey.ExportCards(fbDialogSaveCards.SelectedPath);

                if (checkBoxKeep.Checked)
                {
                    if (string.IsNullOrEmpty(filename)) filename = Path.Combine(storage, string.Format("{0:yyyyMMdd-HHmmss}.ini", DateTime.Now));
                    Survey.WriteXml(filename);
                }
                else
                {
                    if (filename != string.Empty)
                    {
                        if (!Directory.Exists(archive)) Directory.CreateDirectory(archive);
                        File.Move(filename, Path.Combine(archive, Path.GetFileName(filename)));
                    }
                }

                if (checkBoxGearReport.Checked)
                {
                    Report report = new Report(Resources.Reports.Header.StatsGear);
                    CardStack stack = Survey.GetCombinedData().GetStack();
                    stack.AddCommon(report);
                    stack.Sort();
                    stack.AddGearStatsReport(report);
                    report.EndBranded();
                    report.Run();
                }

                if (checkBoxSpcReport.Checked)
                {
                    Report report = new Report(Resources.Reports.Sections.SpeciesStats.Title);
                    CardStack stack = Survey.GetCombinedData().GetStack();
                    stack.AddCommon(report);
                    stack.Sort();
                    stack.AddSpeciesStatsReport(report, SpeciesStatsLevel.Totals | SpeciesStatsLevel.Detailed | SpeciesStatsLevel.TreatmentSuggestion | SpeciesStatsLevel.SurveySuggestion, ExpressionVariant.Efforts);
                    report.EndBranded();
                    report.Run();
                }

                TaskDialogButton tdb = tdLoadCards.ShowDialog();

                if (tdb == tdbLoad)
                {
                    Explorer.Show();
                    Explorer.LoadCards(filenames);
                }
                else if (tdb == tdbOpenFolder)
                {
                    Process.Start(fbDialogSaveCards.SelectedPath);
                }

                finished = true;
                Log.Write(EventType.WizardEnded, "Observations wizard is finished with cards exported to {0}.",
                    fbDialogSaveCards.SelectedPath);

                Close();
            }
        }

        #endregion
    }
}
