using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mayfly;
using Mayfly.Extensions;
using System.Windows.Forms;
using Mayfly.Species;
using Mayfly.Controls;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using Mayfly.Extensions;

namespace Mayfly.Wild.Controls
{
    public partial class LogProcessor : Component
    {
        private SpreadSheet sheet;
        private EventHandler changed;
        private EventHandler individualsRequired;

        private DataGridViewColumn columnID => Grid.Columns["ColumnID"];
        private DataGridViewColumn columnDefinition => Grid.Columns["ColumnSpecies"];
        private DataGridViewColumn columnQty => Grid.Columns["ColumnQuantity"];
        private DataGridViewColumn columnMass => Grid.Columns["ColumnMass"];
        private Button button;
        private bool allowKey;



        [Category("Behavior")]
        public SpreadSheet Grid 
        {
            get { return sheet; }
            set
            {
                sheet = value;

                sheet.RowHeaderMouseDoubleClick += ToolStripMenuItemIndividuals_Click;
                sheet.CellEndEdit += sheet_CellEndEdit;
                sheet.CellValueChanged += sheet_CellValueChanged;
                sheet.RowsAdded += sheet_RowsAdded;
                sheet.RowsRemoved += sheet_RowsRemoved;
                sheet.RowValidated += sheet_RowValidated;
                sheet.UserDeletedRow += sheet_UserDeletedRow;
                sheet.OnFormatChanged += sheet_OnFormatChanged;
                sheet.InputFailed += sheet_InputFailed;

                sheet.RowMenu = this.contextLog;

                columnDefinition.ValueType = typeof(TaxonomicIndex.TaxonRow);
                columnQty.ValueType = typeof(int);
                columnMass.ValueType = typeof(double);


                if (Provider != null)
                {
                    Provider.Grid = value;
                    Provider.ColumnName = columnDefinition.Name;
                }
            }
        }

        [Category("Behavior")]
        public bool AutoLogOpen { get; set; }

        [Category("Behavior")]
        public Status Status { get; set; }

        [Category("Behavior")]
        public ToolStripStatusLabel LabelMass { get; set; }

        [Category("Behavior")]
        public ToolStripStatusLabel LabelQty { get; set; }

        [Category("Behaviour")]
        public Button Button 
        {
            get
            {
                return button;
            }

            set
            {
                button = value;

                if (Button != null)
                {
                    Button.Click += Button_Click;
                }
            }
        }

        [Category("Behaviour"), DefaultValue(true)]
        public bool AllowKey 
        {
            get { return allowKey; }
            set
            {
                allowKey = value;
                toolStripMenuItemKey.Visible = toolStripSeparatorKey.Visible = value;
            }
        }

        [Category("Mayfly Events")]
        public event EventHandler Changed 
        {
            add
            {
                changed += value;
            }

            remove
            {
                changed -= value;
            }
        }

        [Category("Mayfly Events")]
        public event EventHandler IndividualsRequired 
        {
            add
            {
                individualsRequired += value;
            }

            remove
            {
                individualsRequired -= value;
            }
        }

        [Browsable(false)]
        public Data Data;

        [Browsable(false)]
        public int Wealth => Definitions.Length;

        [Browsable(false)]
        public string[] Definitions
        {
            get
            {
                List<string> result = new List<string>();

                foreach (DataGridViewRow gridRow in Grid.Rows)
                {
                    if (gridRow.Cells[columnDefinition.Name].Value is string definition)
                    {
                        if (!result.Contains(definition)) result.Add(definition);
                    }
                }

                return result.ToArray();
            }
        }

        [Browsable(false)]
        public double Quantity
        {
            get
            {
                double result = 0;

                foreach (DataGridViewRow gridRow in Grid.Rows)
                {
                    if (gridRow.Cells[columnQty.Name].Value is int q)
                    {
                        result += q;
                    }
                }

                return result;
            }
        }

        [Browsable(false)]
        public double Mass
        {
            get
            {
                double result = 0;

                foreach (DataGridViewRow gridRow in Grid.Rows)
                {
                    if (gridRow.Cells[columnMass.Name].Value is double w)
                    {
                        result += w;
                    }
                }

                return result;
            }
        }



        public LogProcessor()
        {
            InitializeComponent();
        }

        public LogProcessor(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            toolStripMenuItemKey.Click += ToolStripMenuItemKey_Click;

            this.ToolStripMenuItemIndividuals.Click += new System.EventHandler(this.ToolStripMenuItemIndividuals_Click);
            this.ToolStripMenuItemIndex.Click += new System.EventHandler(this.ToolStripMenuItemIndex_Click);
            this.ToolStripMenuItemCut.Click += new System.EventHandler(this.ToolStripMenuItemCut_Click);
            this.ToolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
            this.ToolStripMenuItemPaste.Click += new System.EventHandler(this.ToolStripMenuItemPaste_Click);
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
        }



        public void SaveLog()
        {
            foreach (DataGridViewRow gridRow in Grid.Rows)
            {
                if (gridRow.IsNewRow) continue;

                SaveLogRow(gridRow);
            }
        }

        private void Clear(DataGridViewRow gridRow)
        {
            if (gridRow.Cells[columnID.Index].Value != null)
            {
                Data.LogRow logRow = Data.Log.FindByID((int)gridRow.Cells[columnID.Index].Value);

                if (logRow != null)
                {
                    Data.DefinitionRow spcRow = logRow.DefinitionRow;
                    logRow.Delete();

                    if (spcRow.GetLogRows().Length == 0)
                    {
                        spcRow.Delete();
                    }
                }
            }
        }

        private void HandleLogRow(DataGridViewRow gridRow)
        {
            if (gridRow.IsNewRow) return;

            if (Service.IsRowEmpty(gridRow)) 
            {
                Grid.Rows.Remove(gridRow); 
                return; 
            }

            if (gridRow.Cells[columnDefinition.Index].Value == null)
            {
                gridRow.Cells[columnDefinition.Index].Value = Species.Resources.Interface.UnidentifiedTitle;
            }

            foreach (DataGridViewRow currentGridRow in Grid.Rows) {
                if (currentGridRow.IsNewRow) continue;
                if (currentGridRow == gridRow) continue;

                if (Equals(gridRow.Cells[columnDefinition.Index].Value, currentGridRow.Cells[columnDefinition.Index].Value)) {
                    provider_DuplicateDetected(this, new DuplicateFoundEventArgs(gridRow, currentGridRow));
                    break;
                }
            }
        }

        private Data.LogRow LogRow(DataGridViewRow gridRow)
        {
            return LogRow(Data, gridRow);
        }

        private Data.LogRow LogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result;

            if (data == Data)
            {
                if (gridRow.Cells[columnID.Index].Value != null)
                {
                    result = data.Log.FindByID((int)gridRow.Cells[columnID.Index].Value);
                    if (result != null)
                    {
                        goto Saving;
                    }
                }
            }

            result = data.Log.NewLogRow();
            result.CardRow = data.Solitary;

        Saving:

            if (gridRow.Cells[columnDefinition.Index].Value is TaxonomicIndex.TaxonRow tr)
            {
                Data.DefinitionRow existingSpeciesRow = data.Definition.FindByName(tr.Name);
                if (existingSpeciesRow == null)
                {
                    existingSpeciesRow = data.Definition.AddDefinitionRow(tr.Rank, tr.Name);
                }
                result.DefID = existingSpeciesRow.ID;
            }
            else if ((string)gridRow.Cells[columnDefinition.Index].Value is string s)
            {
                if (s == Species.Resources.Interface.UnidentifiedTitle)
                {
                    result.SetDefIDNull();
                }
                else
                {
                    Data.DefinitionRow existingSpeciesRow = data.Definition.FindByName(s);
                    if (existingSpeciesRow == null)
                    {
                        existingSpeciesRow = data.Definition.AddDefinitionRow(TaxonomicRank.Species, s);
                    }
                    result.DefID = existingSpeciesRow.ID;
                }
            }
            //SpeciesKey.TaxonRow speciesRow = UserSettings.SpeciesIndex.FindByName(gridRow.Cells[ColumnSpecies.Index].Value.ToString());

            //if (speciesRow == null)
            //{
            //    // There is no such species in index
            //    if ((string)gridRow.Cells[ColumnSpecies.Index].Value == Species.Resources.Interface.UnidentifiedTitle)
            //    {
            //        result.SetDefIDNull();
            //    }
            //    else if (gridRow.Cells[ColumnSpecies.Index].Value is SpeciesKey.TaxonRow tr)
            //    {
            //        Data.DefinitionRow newSpeciesRow = data.Definition.AddDefinitionRow(tr.Rank, tr.Name);
            //        result.DefID = newSpeciesRow.ID;
            //    }
            //}
            //else
            //{
            //    // There is such species in index you using
            //    Data.DefinitionRow existingSpeciesRow = data.Definition.FindByName(speciesRow.Name);
            //    if (existingSpeciesRow == null)
            //    {
            //        existingSpeciesRow = (Data.DefinitionRow)data.Species.Rows.Add(null, speciesRow.Name);
            //    }
            //    result.SpeciesRow = existingSpeciesRow;
            //}


            if (gridRow.Cells[columnQty.Index].Value == null)
            {
                result.SetQuantityNull();
            }
            else
            {
                result.Quantity = (int)gridRow.Cells[columnQty.Index].Value;
            }

            if (gridRow.Cells[columnMass.Index].Value == null)
            {
                result.SetMassNull();
            }
            else
            {
                result.Mass = (double)gridRow.Cells[columnMass.Index].Value;
            }

            return result;
        }

        public Data.LogRow SaveLogRow(DataGridViewRow gridRow)
        {
            return SaveLogRow(Data, gridRow);
        }

        private Data.LogRow SaveLogRow(Data data, DataGridViewRow gridRow)
        {
            Data.LogRow result = LogRow(data, gridRow);
            if (data.Log.Rows.IndexOf(result) == -1) data.Log.AddLogRow(result);
            if (data == Data) gridRow.Cells[columnID.Index].Value = result.ID;
            return result;
        }

        public void InsertLogRows(Data data, int rowIndex)
        {
            if (rowIndex == -1)
            {
                rowIndex = Grid.RowCount - 1;
            }

            foreach (Data.LogRow logRow in data.Log.Rows)
            {
                InsertLogRow(logRow, rowIndex);

                if (rowIndex < Grid.RowCount - 1)
                {
                    rowIndex++;
                }
            }
        }

        private void InsertLogRow(Data.LogRow logRow, int rowIndex)
        {
            DataGridViewRow gridRow = columnID.GetRow(logRow.ID);

            if (gridRow == null)
            {
                gridRow = new DataGridViewRow();
                gridRow.CreateCells(Grid);
                gridRow.Cells[columnID.Index].Value = logRow.ID;
                
                Grid.Rows.Insert(rowIndex, gridRow);
            }

            UpdateLogRow(gridRow, logRow);
            HandleLogRow(gridRow);
        }

        public void UpdateLogRow(Data.LogRow logRow)
        {
            UpdateLogRow(Provider.FindLine(logRow.DefinitionRow.Taxon), logRow);
        }

        private void UpdateLogRow(DataGridViewRow gridRow, Data.LogRow logRow)
        {
            gridRow.Cells[columnDefinition.Index].Value = logRow.DefinitionRow.KeyRecord;

            if (logRow.IsQuantityNull()) gridRow.Cells[columnQty.Index].Value = null;
            else gridRow.Cells[columnQty.Index].Value = logRow.Quantity;

            if (logRow.IsMassNull()) gridRow.Cells[columnMass.Index].Value = null;
            else gridRow.Cells[columnMass.Index].Value = logRow.Mass;

            if (logRow.IsCommentsNull()) gridRow.Cells[columnDefinition.Index].ToolTipText = string.Empty;
            else gridRow.Cells[columnDefinition.Index].ToolTipText = logRow.Comments;
        }

        public int InsertSpecies(TaxonomicIndex.TaxonRow species)
        {
            int speciesIndex = -1;

            // Try to find species in the list
            foreach (DataGridViewRow gridRow in Grid.Rows)
            {
                if (gridRow.Cells[columnDefinition.Index].Value == null) continue;

                if (object.Equals(gridRow.Cells[columnDefinition.Index].Value, species))
                {
                    speciesIndex = gridRow.Index;
                    break;
                }
            }

            // If there is no such species - insert the new row
            if (speciesIndex == -1)
            {
                Data.Definition.AddDefinitionRow(species.Rank, species.Name);
                speciesIndex = Grid.Rows.Add(null, species);
            }

            // Select the new row and its first cell
            Grid.ClearSelection();
            Grid.Rows[speciesIndex].Selected = true;
            Grid.CurrentCell = Grid.Rows[speciesIndex].Cells[columnDefinition.Index];

            // Then insert species in selected cell
            Provider.InsertSpeciesHere(species);

            return speciesIndex;
        }

        public void RunDefinition(string definition)
        {

        }
        //private TaxonomicIndex.TaxonRow SpeciesToOpen;

        //public void OpenSpecies(string species)
        //{
        //    SpeciesToOpen = UserSettings.SpeciesIndex.FindByName(species);
        //    Load += new EventHandler(cardOpenSpecies_Load);
        //}

        //private void cardOpenSpecies_Load(object sender, EventArgs e)
        //{
        //    tabControl.SelectedTab = tabPageLog;

        //    mainLogProcessor.Provider.InsertSpecies(SpeciesToOpen);
        //    if (!UserSettings.AutoLogOpen)
        //    {
        //        mainLogProcessor_IndividualsRequired(spreadSheetLog, new EventArgs());
        //    }
        //    IsChanged = false;
        //}

        //private void CardOpenSpecies_Load(object sender, EventArgs e)
        //{
        //    tabControl.SelectedTab = tabPageLog;
        //    mainLogProcessor.InsertSpecies(SpeciesToOpen);
        //    if (UserSettings.AutoLogOpen) mainLogProcessor_IndividualsRequired(spreadSheetLog, new EventArgs());

        //    IsChanged = false;
        //}





















        private void GetSpeciesList()
        {
            while (contextMenuStripSpecies.Items.Count > 4)
            {
                contextMenuStripSpecies.Items.RemoveAt(1);
            }

            UpdateRecent();

            toolStripMenuItemAll.DropDownItems.Clear();
            toolStripMenuItemAll.Visible = Provider.Index.AllSpeciesCount <= Species.UserSettings.AllowableSpeciesListLength;
            if (Provider.Index.AllSpeciesCount <= Species.UserSettings.AllowableSpeciesListLength)
            {
                foreach (TaxonomicIndex.TaxonRow speciesRow in Provider.Index.GetSpeciesRows())
                {
                    ToolStripItem speciesItem = new ToolStripMenuItem
                    {
                        Tag = speciesRow,
                        Text = speciesRow.ToString(columnDefinition.DefaultCellStyle.Format)
                    };
                    speciesItem.Click += new EventHandler(speciesItem_Click);
                    toolStripMenuItemAll.DropDownItems.Add(speciesItem);
                }

                toolStripMenuItemAll.SortItems();
            }

            foreach (TaxonomicIndex.TaxonRow taxonRow in Provider.Index.GetRootTaxonRows())
            {
                if (taxonRow.IsHigher && !taxonRow.HasChildren) continue;
                contextMenuStripSpecies.Items.Add(getTaxonMenuItem(taxonRow));
                if (contextMenuStripSpecies.Items.Count == Species.UserSettings.AllowableSpeciesListLength) break;
            }
        }

        public void UpdateRecent()
        {
            toolStripMenuItemRecent.DropDownItems.Clear();

            ToolStripDropDownItem[] recentItems = MostUsedRecent();
            //toolStripSeparatorKey.Visible =
            toolStripMenuItemRecent.Visible = recentItems.Length > 0;
            toolStripMenuItemRecent.DropDownItems.AddRange(recentItems);
        }

        private ToolStripMenuItem getTaxonMenuItem(TaxonomicIndex.TaxonRow taxonRow)
        {
            ToolStripMenuItem taxonItem = new ToolStripMenuItem
            {
                Tag = taxonRow,
                Text = taxonRow.ToString(columnDefinition.DefaultCellStyle.Format)
            };

            taxonItem.Click += new EventHandler(speciesItem_Click);

            foreach (TaxonomicIndex.TaxonRow derRow in taxonRow.GetTaxonRows())
            {
                if (derRow.IsHigher && !derRow.HasChildren) continue;
                taxonItem.DropDownItems.Add(getTaxonMenuItem(derRow));
            }

            return taxonItem;
        }

        private ToolStripDropDownItem[] MostUsedRecent()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(UserSettings.Path).OpenSubKey(Path.GetFileNameWithoutExtension(Provider.IndexPath));

            if (key == null) return new ToolStripDropDownItem[0];

            List<string> recentSpecies = new List<string>();

            while (recentSpecies.Count < Math.Min(key.ValueCount, Provider.RecentListCount))
            {
                int maxUsed = 0;
                string maxUsedSpecies = string.Empty;

                foreach (string value in key.GetValueNames())
                {
                    if (recentSpecies.Contains(value)) continue;
                    int used = (int)key.GetValue(value);
                    if (used > maxUsed)
                    {
                        maxUsedSpecies = value;
                        maxUsed = used;
                    }
                }

                recentSpecies.Add(maxUsedSpecies);
            }

            List<ToolStripDropDownItem> recentSpeciesItems = new List<ToolStripDropDownItem>();

            foreach (string recent in recentSpecies)
            {
                TaxonomicIndex.TaxonRow speciesRow = Provider.Index.FindByName(recent);

                if (speciesRow == null) continue;

                ToolStripDropDownItem speciesItem = new ToolStripMenuItem
                {
                    Tag = speciesRow,
                    Text = speciesRow.CommonName
                };
                speciesItem.Click += new EventHandler(speciesItem_Click);
                recentSpeciesItems.Add(speciesItem);
            }

            return recentSpeciesItems.ToArray();
        }

        private void resetMenuLabels(ToolStripItemCollection items, string f)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem mi)
                {
                    if (mi.Tag is TaxonomicIndex.TaxonRow tr)
                    {
                        mi.Text = tr.ToString(f);
                    }

                    resetMenuLabels(mi.DropDownItems, f);
                }
            }
        }



        public void UpdateStatus()
        {
            if (Wealth > 0)
            {
                Status.Default = Wealth.ToString(Resources.Interface.Interface.SpeciesCount);
            }
            else
            {
                Status.Default = string.Format("© {0:yyyy} {1}",
                    (Data == null || Data.Solitary.IsWhenNull()) ? DateTime.Today : Data.Solitary.When,
                    (Data == null || Data.Solitary.IsSignNull()) ? Mayfly.UserSettings.Username : Data.Solitary.Investigator);
            }


            LabelMass.ResetFormatted(Mass);
            LabelQty.ResetFormatted(Quantity);
        }


        private void provider_IndexChanged(object sender, EventArgs e)
        {
            if (Button != null) Button.Enabled = Provider.Index != null;
            toolStripMenuItemAll.Visible = Provider.Index.AllSpeciesCount <= Species.UserSettings.AllowableSpeciesListLength;
            GetSpeciesList();
        }

        private void provider_SpeciesSelected(object sender, SpeciesSelectEventArgs e)
        {
            if (changed != null) changed.Invoke(this, new EventArgs());

            if (AutoLogOpen)
            {
                Grid.ClearSelection();
                e.Row.Selected = true;
                ToolStripMenuItemIndividuals_Click(ToolStripMenuItemIndividuals, new EventArgs());
            }
        }

        private void provider_DuplicateDetected(object sender, DuplicateFoundEventArgs e)
        {
            int q = 0;
            double w = 0;

            Data.LogRow editedLogRow = SaveLogRow(e.EditedRow);
            Data.LogRow duplicateLogRow = LogRow(e.DuplicateRow);

            // If quantity is already set in edited row
            if (e.EditedRow.Cells[columnQty.Index].Value != null)
            {
                // Add it to q
                q += (int)e.EditedRow.Cells[columnQty.Name].Value;
            }

            // If mass is already set in edited row
            if (e.EditedRow.Cells[columnMass.Index].Value != null)
            {
                // Add it to w
                w += (double)e.EditedRow.Cells[columnMass.Name].Value;

                // If quantity equals 1 and no detailed individuals
                if (e.EditedRow.Cells[columnQty.Index].Value != null &&
                    (int)e.EditedRow.Cells[columnQty.Index].Value == 1 &&
                    editedLogRow.GetIndividualRows().Length == 0)
                {
                    // Create new individual record with that mass
                    Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = editedLogRow;
                    newIndividualRow.Mass = (double)e.EditedRow.Cells[columnMass.Index].Value;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }
            }

            // If quantity is already set in previously added row
            if (e.DuplicateRow.Cells[columnQty.Index].Value != null)
            {
                // Add it to q
                q += (int)e.DuplicateRow.Cells[columnQty.Name].Value;
            }

            // If mass is already set in previously added row
            if (e.DuplicateRow.Cells[columnMass.Index].Value != null)
            {
                // Add it to w
                w += (double)e.DuplicateRow.Cells[columnMass.Name].Value;

                // If quantity equals 1 and no detailed individuals
                if (e.DuplicateRow.Cells[columnQty.Index].Value != null &&
                    (int)e.DuplicateRow.Cells[columnQty.Index].Value == 1 &&
                    duplicateLogRow.GetIndividualRows().Length == 0)
                {
                    // Create new individual record with that mass and add it to new log row
                    Data.IndividualRow newIndividualRow = Data.Individual.NewIndividualRow();
                    newIndividualRow.LogRow = editedLogRow;
                    if (e.EditedRow.Cells[columnMass.Index].Value != null)
                    {
                        newIndividualRow.Mass = (double)e.EditedRow.Cells[columnMass.Index].Value;
                    }
                    newIndividualRow.Comments = Wild.Resources.Interface.Interface.DuplicateDefaultComment;
                    Data.Individual.AddIndividualRow(newIndividualRow);
                }
            }

            if (q > 0)
            {
                e.EditedRow.Cells[columnQty.Index].Value = q;
            }

            if (w > 0)
            {
                e.EditedRow.Cells[columnMass.Index].Value = w;
            }

            foreach (Data.IndividualRow individualRow in duplicateLogRow.GetIndividualRows())
            {
                individualRow.LogRow = editedLogRow;
            }

            Clear(e.DuplicateRow);
            Grid.Rows.Remove(e.DuplicateRow);

            Status.Default = Wealth.ToString(Resources.Interface.Interface.SpeciesCount);
            Status.Message(string.Format(Resources.Interface.Messages.DuplicateSummed, e.EditedRow.Cells[columnDefinition.Index].Value));
        }



        private void contextLog_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItemPaste.Enabled = Clipboard.ContainsText() && Data.ContainsLog(Clipboard.GetText());
        }

        private void ToolStripMenuItemIndividuals_Click(object sender, EventArgs e)
        {
            Grid.EndEdit();

            if (individualsRequired != null) individualsRequired.Invoke(this, new EventArgs());
        }

        private void ToolStripMenuItemCut_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemCopy_Click(sender, e);
            ToolStripMenuItemDelete_Click(sender, e);
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            Data clipData = new Data();
            Data.CardRow clipCardRow = clipData.Card.NewCardRow();
            clipData.Card.AddCardRow(clipCardRow);

            foreach (DataGridViewRow selectedRow in Grid.SelectedRows)
            {
                if (selectedRow.IsNewRow)
                {
                    continue;
                }

                Data.LogRow clipLogRow = SaveLogRow(clipData, selectedRow);

                if (selectedRow.Cells[columnID.Index].Value != null)
                {
                    Data.LogRow logRow = Data.Log.FindByID((int)selectedRow.Cells[columnID.Index].Value);

                    if (logRow != null)
                    {
                        foreach (Data.IndividualRow individualRow in logRow.GetIndividualRows())
                        {
                            individualRow.CopyTo(clipLogRow);
                        }

                        //foreach (Data.StratifiedRow stratifiedRow in logRow.GetStratifiedRows())
                        //{
                        //    Data.StratifiedRow clipStratifiedRow = clipData.Stratified.NewStratifiedRow();

                        //    clipStratifiedRow.Class = stratifiedRow.Class;
                        //    if (!stratifiedRow.IsCountNull()) clipStratifiedRow.Count = stratifiedRow.Count;
                        //    clipStratifiedRow.LogRow = clipLogRow;
                        //    clipData.Stratified.AddStratifiedRow(clipStratifiedRow);
                        //}
                    }
                }
            }

            Clipboard.SetText(clipData.GetXml());
        }

        private void ToolStripMenuItemPaste_Click(object sender, EventArgs e)
        {
            Data clipData = new Data();
            clipData.ReadXml(new StringReader(Clipboard.GetText()));

            int rowIndex = Grid.SelectedRows[0].Index;

            foreach (Data.LogRow clipLogRow in clipData.Log)
            {
                // Copy from Clipboard Data to local Data

                Data.DefinitionRow definitionRow = Data.Definition.FindByName(clipLogRow.DefinitionRow.Taxon);

                if (definitionRow == null)
                {
                    TaxonomicIndex.TaxonRow clipSpeciesRow = Provider.Index.FindByName(clipLogRow.DefinitionRow.Taxon);
                    definitionRow = (clipSpeciesRow == null ?
                        Data.Definition.AddDefinitionRow(TaxonomicRank.Species, clipLogRow.DefinitionRow.Taxon) :
                        Data.Definition.AddDefinitionRow(clipSpeciesRow.Rank, clipSpeciesRow.Name));
                }

                Data.LogRow logRow = Data.Log.FindByCardIDDefID(Data.Solitary.ID, definitionRow.ID);

                if (logRow == null)
                {
                    logRow = Data.Log.NewLogRow();
                    logRow.DefID = definitionRow.ID;
                    logRow.CardRow = Data.Solitary;
                    logRow.DefID = definitionRow.ID;
                    Data.Log.AddLogRow(logRow);

                    if (!clipLogRow.IsQuantityNull()) logRow.Quantity = clipLogRow.Quantity;
                    if (!clipLogRow.IsMassNull()) logRow.Mass = clipLogRow.Mass;
                }
                else
                {
                    if (!clipLogRow.IsQuantityNull()) logRow.Quantity = logRow.IsQuantityNull() ? clipLogRow.Quantity : logRow.Quantity + clipLogRow.Quantity;
                    if (!clipLogRow.IsMassNull()) logRow.Mass = logRow.IsMassNull() ? clipLogRow.Mass : logRow.Mass + clipLogRow.Mass;
                }

                foreach (Data.IndividualRow clipIndividualRow in clipLogRow.GetIndividualRows())
                {
                    clipIndividualRow.CopyTo(logRow);
                }

                foreach (Data.StratifiedRow clipStratifiedRow in clipData.Stratified)
                {
                    //Data.StratifiedRow stratifiedRow = Data.Stratified.NewStratifiedRow();

                    //stratifiedRow.Class = clipStratifiedRow.Class;
                    //if (!clipStratifiedRow.IsCountNull()) stratifiedRow.Count = clipStratifiedRow.Count;

                    //stratifiedRow.LogRow = logRow;

                    //Data.Stratified.AddStratifiedRow(stratifiedRow);
                }

                InsertLogRow(logRow, rowIndex);

                if (rowIndex < Grid.RowCount - 1)
                {
                    rowIndex++;
                }
            }

            if (changed != null) changed.Invoke(this, new EventArgs());
            UpdateStatus();
            Clipboard.Clear();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            int rowsToDelete = Grid.SelectedRows.Count;
            while (rowsToDelete > 0)
            {
                Clear(Grid.SelectedRows[0]);
                Grid.Rows.Remove(Grid.SelectedRows[0]);
                rowsToDelete--;
            }

            if (changed != null) changed.Invoke(this, new EventArgs());
            UpdateStatus();
        }

        private void ToolStripMenuItemIndex_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in Grid.SelectedRows)
            {
                Provider.FindInKey(selectedRow);
            }
        }



        private void sheet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (changed != null) changed.Invoke(this, new EventArgs());
        }

        private void sheet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid.Focused)
                UpdateStatus();
        }

        private void sheet_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Clear(e.Row);
            UpdateStatus();
            if (changed != null) changed.Invoke(this, new EventArgs());
        }

        private void sheet_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            UpdateStatus();
            if (changed != null) changed.Invoke(this, new EventArgs());
        }

        private void sheet_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateStatus();
            if (changed != null) changed.Invoke(this, new EventArgs());
        }

        private void sheet_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow gridRow = ((DataGridView)sender).Rows[e.RowIndex];
            //gridRow.HeaderCell.Value = gridRow.IsNewRow ? string.Empty : (e.RowIndex + 1).ToString();
        }

        private void sheet_OnFormatChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column == columnDefinition)
                resetMenuLabels(contextMenuStripSpecies.Items, e.Column.DefaultCellStyle.Format);
        }

        private void sheet_InputFailed(object sender, DataGridViewCellEventArgs e)
        {
            Grid.NotifyInstantly(Resources.Interface.Messages.LogInstruction);
        }


        #region Button and menu logics

        private void Button_Click(object sender, EventArgs e)
        {
            contextMenuStripSpecies.Show(Button, Point.Empty, ToolStripDropDownDirection.AboveRight);
        }

        private void speciesItem_Click(object sender, EventArgs e)
        {
            InsertSpecies((TaxonomicIndex.TaxonRow)((ToolStripMenuItem)sender).Tag);
        }

        private void ToolStripMenuItemKey_Click(object sender, EventArgs e)
        {
            if (Form.ModifierKeys.HasFlag(Keys.Control))
            {
                Provider.InsertFromKey(Provider.IndexPath);
            }
            else
            {
                if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    Provider.InsertFromKey(UserSettings.Interface.OpenDialog.FileName);
                }
            }
        }

        #endregion
    }
}
