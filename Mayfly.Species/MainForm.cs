using Mayfly.Extensions;
using Mayfly.Software;
using Mayfly.TaskDialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Mayfly.Species
{
    public partial class MainForm : Form
    {
        public MainForm() {
            InitializeComponent();

            listViewRepresence.Shine();
            taxaTreeView.Shine();
            treeViewStep.Shine();
            listViewEngagement.Shine();

            Data = new TaxonomicIndex();
            FileName = null;

            statusSpecies.ResetFormatted(Constants.Null);
            statusTaxon.ResetFormatted(Constants.Null);
            labelSpecies.Text =
                labelClassification.Text =
                string.Empty;

            foreach (Label label in new Label[] {
                labelStpCount, labelEngagedCount, labelPicCount }) {
                label.UpdateStatus(0);
            }

            taxaTreeView.Bind(Data);
            taxaTreeView.HigherTaxonFormat = UserSettings.HigherTaxonNameFormat;
            taxaTreeView.LowerTaxonFormat = UserSettings.LowerTaxonNameFormat;
            taxaTreeView.LowerTaxonColor = UserSettings.LowerTaxonColor;
            taxaTreeView.DeepestRank = UserSettings.FillTreeWithLowerTaxon ? null : TaxonomicRank.Subtribe;

            tabPageKey.Parent = null;
            tabPagePictures.Parent = null;
        }

        public MainForm(string filename)
            : this() {
            loadData(filename);
        }




        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (IsChanged) {
                TaskDialogButton b = tdSave.ShowDialog(this);

                if (b == tdbCancelClose) {
                    e.Cancel = true;
                } else if (b == tdbSave) {
                    menuItemSave_Click(sender, e);
                }
            }
        }

        private void tab_Changed(object sender, EventArgs e) {
            menuTaxon.Visible = tabControl.SelectedTab == tabPageTaxon;
            menuKey.Visible = tabControl.SelectedTab == tabPageKey;
            menuPictures.Visible = tabControl.SelectedTab == tabPagePictures;
        }



        #region Menus

        #region File menu

        private void menuItemNew_Click(object sender, EventArgs e) {
            if (checkAndSave() != DialogResult.Cancel) {
                clear();
            }
        }

        private void menuItemOpen_Click(object sender, EventArgs e) {
            if (UserSettings.Interface.OpenDialog.ShowDialog() == DialogResult.OK) {
                if (checkAndSave() == DialogResult.Cancel) return;

                clear();
                loadData(UserSettings.Interface.OpenDialog.FileName);
            }
        }

        private void menuItemSave_Click(object sender, EventArgs e) {
            if (IsChanged) {
                if (FileName == null) {
                    menuItemSaveAs_Click(menuItemSaveAs, new EventArgs());
                } else {
                    save(FileName);
                }
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e) {
            if (UserSettings.Interface.ExportDialog.ShowDialog() == DialogResult.OK) {
                switch (Path.GetExtension(UserSettings.Interface.ExportDialog.FileName)) {
                    case ".txn":
                        save(UserSettings.Interface.ExportDialog.FileName);
                        break;
                    case ".html":
                        File.WriteAllText(UserSettings.Interface.ExportDialog.FileName,
                            Data.Report.ToString(), Encoding.UTF8);
                        break;
                }
            }
        }

        private void menuItemPreview_Click(object sender, EventArgs e) {
            Data.Report.Preview();
        }

        private void menuItemPrint_Click(object sender, EventArgs e) {
            Data.Report.Print();
        }

        private void menuItemClose_Click(object sender, EventArgs e) {
            Close();
        }

        #endregion

        #region List menu

        private void menuItemAddTaxon_Click(object sender, EventArgs e) {
            TaxonomicIndex.TaxonRow taxonRow = (sender == menuItemAddTaxon ?
                Data.Taxon.NewTaxonRow((TaxonomicRank)61, Resources.Interface.NewTaxon) :
                Data.Taxon.NewTaxonRow(TaxonomicRank.Species, Resources.Interface.NewSpecies));

            EditTaxon editTaxon = new EditTaxon(taxonRow);

            if (editTaxon.ShowDialog(this) == DialogResult.OK) {
                Data.Taxon.AddTaxonRow(taxonRow);
                taxaTreeView.AddNode(taxonRow);

                if (taxonRow.IsHigher) {
                    listViewRepresence.Groups.Add(getGroup(taxonRow));
                }

                if (taxaTreeView.PickedTaxon != null && taxaTreeView.PickedTaxon.Includes(taxonRow, true))// && checkBoxRepresence.Checked)
                {
                    listViewRepresence.CreateItem(taxonRow);
                }

                ListViewItem item = listViewEngagement.CreateItem(taxonRow);
                item.Group = listViewEngagement.Groups[1];

                IsChanged = true;
            }
        }

        #endregion

        private void menuItemSettings_Click(object sender, EventArgs e) {

            Mayfly.UserSettings.ExpandSettings(
                typeof(Controls.SettingsPageTree),
                typeof(Controls.SettingsPagePrint));

            if (Mayfly.UserSettings.Settings.ShowDialog() == DialogResult.OK) {
                taxaTreeView.HigherTaxonFormat = UserSettings.HigherTaxonNameFormat;
                taxaTreeView.LowerTaxonFormat = UserSettings.LowerTaxonNameFormat;
                taxaTreeView.LowerTaxonColor = UserSettings.LowerTaxonColor;

                taxaTreeView.ApplyAppearanceDescending();
            }
        }

        private void menuItemAbout_Click(object sender, EventArgs e) {
            About about = new About(Properties.Resources.logo);
            about.ShowDialog();
        }

        #endregion


        private void backTreeLoader_DoWork(object sender, DoWorkEventArgs e) {
            foreach (TaxonomicIndex.TaxonRow taxonRow in Data.GetHigherTaxonRows(false)) {
                addGroup(getGroup(taxonRow));
            }

            addGroup(new ListViewGroup(string.Format(Resources.Interface.VariaCount, Data.GetRootSpeciesRows().Length)) {
                Name = "Varia",
                HeaderAlignment = HorizontalAlignment.Center
            });
        }

        private void backTreeLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            processDisplay.StopProcessing();
            status.Message(Resources.Interface.StatusLoaded);

            updateSummary();

            //if (Data.IsKeyAvailable) {
            //    loadKey();
            //} else {
            //    loadEngagedList();
            //}

            IsChanged = false;
        }


        #region Taxonomic tree tab

        private void taxaTreeView_Changed(object sender, EventArgs e) {
            IsChanged = true;
        }

        private void taxaTreeView_TaxonSelected(object sender, TaxonEventArgs e) {
            updateTaxonData(e.TaxonRow);
        }

        private void taxaTreeView_TaxonChanged(object sender, TaxonEventArgs e) {
            applyRename();
            if (taxaTreeView.PickedTaxon == e.TaxonRow) updateTaxonData(e.TaxonRow);
        }


        //private void checkBoxRepresence_CheckedChanged(object sender, EventArgs e)
        //{
        //    listViewRepresence.Visible = checkBoxRepresence.Checked;
        //    checkBoxGroups.Visible = checkBoxRepresence.Checked;

        //    if (checkBoxRepresence.Checked)
        //    {
        //        updateRepresenceSpecies((taxaTreeView.PickedTaxon.IsHigher ? taxaTreeView.PickedTaxon : taxaTreeView.PickedTaxon.ValidRecord).GetSpeciesRows(true));
        //    }
        //}

        private void buttonTaxonEdit_Click(object sender, EventArgs e) {
            taxaTreeView.RunEditing();
        }

        private void checkBoxGroups_CheckedChanged(object sender, EventArgs e) {
            listViewRepresence.ShowGroups = !checkBoxPlain.Checked;

            if (wasPlain && listViewRepresence.ShowGroups) {
                updateRepresenceSpecies(taxaTreeView.PickedTaxon.GetSpeciesRows(true));
            }
        }


        private void listViewRepresence_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (listViewRepresence.HitTest(e.Location).Item != null) {
                    contextSpecies.Show(listViewRepresence, e.Location);
                }
            }
        }

        private void listViewRepresence_ItemDrag(object sender, ItemDragEventArgs e) {
            List<TaxonomicIndex.TaxonRow> speciesRows = new List<TaxonomicIndex.TaxonRow>();

            foreach (ListViewItem item in (sender as ListView).SelectedItems) {
                speciesRows.Add((TaxonomicIndex.TaxonRow)item.Tag);
            }

            listViewRepresence.DoDragDrop(speciesRows.ToArray(), DragDropEffects.Link | DragDropEffects.Copy);
        }

        private void listViewRepresence_DragOver(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(typeof(TaxonomicIndex.TaxonRow[]))) return;

            ListViewItem hoverItem = listViewRepresence.GetHoveringItem(e.X, e.Y);
            if (hoverItem == null) return;
            TaxonomicIndex.TaxonRow hoverSpecies = (TaxonomicIndex.TaxonRow)hoverItem.Tag;
            if (hoverSpecies == null) return;

            TaxonomicIndex.TaxonRow[] carrySpecies = (TaxonomicIndex.TaxonRow[])e.Data.GetData(typeof(TaxonomicIndex.TaxonRow[]));
            if (carrySpecies.Length != 1) return;

            listViewRepresence.SelectedItems.Clear();
            hoverItem.Selected = true;
            e.Effect = taxaTreeView.CheckTaxonCompatibility(carrySpecies[0], hoverSpecies);
        }

        private void listViewRepresence_DragDrop(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(typeof(TaxonomicIndex.TaxonRow[]))) return;

            ListViewItem dropItem = listViewRepresence.GetHoveringItem(e.X, e.Y);
            if (dropItem == null) return;
            TaxonomicIndex.TaxonRow dropSpecies = (TaxonomicIndex.TaxonRow)dropItem.Tag;
            if (dropSpecies == null) return;

            TaxonomicIndex.TaxonRow[] carrySpecies = (TaxonomicIndex.TaxonRow[])e.Data.GetData(typeof(TaxonomicIndex.TaxonRow[]));
            if (carrySpecies.Length != 1) return;

            taxaTreeView.SetParent(carrySpecies, dropSpecies);
            listViewRepresence.Items.Find(carrySpecies[0].ID.ToString(), false)[0].UpdateItem(carrySpecies[0]);
            status.Message((ModifierKeys.HasFlag(Keys.Shift) ? Resources.Interface.StatusTreeSynonymized : Resources.Interface.StatusTreeSubspecies),
                carrySpecies[0], dropSpecies);

            dropItem.Selected = true;
            IsChanged = true;
        }

        private void contextRemoveSynonym_Click(object sender, EventArgs e) {
            //foreach (ListViewItem item in listViewMinor.SelectedItems) {
            //    Data.Taxon.FindByID(item.GetID()).SetTaxIDNull();
            //}

            //listViewRepresence_SelectedIndexChanged(sender, e);
        }

        private void backSpcLoader_DoWork(object sender, DoWorkEventArgs e) {
            if (e.Argument == null) { e.Cancel = true; } else {
                List<ListViewItem> result = new List<ListViewItem>();
                foreach (TaxonomicIndex.TaxonRow speciesRow in (TaxonomicIndex.TaxonRow[])e.Argument) {
                    ListViewItem item = new ListViewItem();
                    item.UpdateItem(speciesRow);
                    item.Group = listViewRepresence.Groups[speciesRow.TaxonRowParent == null ? "Varia" : speciesRow.ValidRecord.HigherParent.Name];
                    result.Add(item);
                }
                e.Result = result.ToArray();
            }
        }

        private void backSpcLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!e.Cancelled) {
                listViewRepresence.Items.Clear();
                ListViewItem[] items = (ListViewItem[])e.Result;
                listViewRepresence.Items.AddRange(items);
            }

            taxaTreeView.Enabled = true;
            taxaTreeView.Focus();
            processDisplay.StopProcessing();
        }


        private void contextSpeciesEdit_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRepresence.SelectedItems) {
                editSpecies(Data.Taxon.FindByID(item.GetID()));
            }
        }

        private void contextSpeciesDelete_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRepresence.SelectedItems) {
                ((TaxonomicIndex.TaxonRow)item.Tag).Delete();
                item.Remove();
            }

            taxaTreeView.ApplyRenameAscending(taxaTreeView.SelectedNode);

            IsChanged = true;
        }

        #endregion


        #region Key tab

        #region Context menu

        private void contextStepDelete_Click(object sender, EventArgs e) {
            if (IsStepNodeSelected) {
                ((TaxonomicIndex.StepRow)treeViewStep.SelectedNode.Tag).Delete();
                treeViewStep.Nodes.Remove(treeViewStep.SelectedNode);
                IsChanged = true;
            }
        }

        private void contextStepNewFeature_Click(object sender, EventArgs e) {
            EditFeature editFeature = new EditFeature(SelectedStep);
            editFeature.Show(this);
        }

        private void contextFeatureEdit_Click(object sender, EventArgs e) {
            EditFeature editFeature = new EditFeature(SelectedFeature);
            editFeature.SetFriendlyDesktopLocation(this, FormLocation.Centered);
            //treeViewStep, treeViewStep.SelectedNode.Bounds.Location);
            //editFeature.FormClosed += new FormClosedEventHandler(taxonEdit_FormClosed);
            editFeature.Show();
        }

        private void contextFeatureDelete_Click(object sender, EventArgs e) {
            if (IsFeatureNodeSelected) {
                ((TaxonomicIndex.FeatureRow)treeViewStep.SelectedNode.Tag).Delete();
                treeViewStep.Nodes.Remove(treeViewStep.SelectedNode);
                IsChanged = true;
            }
        }

        #endregion

        private void treeViewStep_AfterSelect(object sender, TreeViewEventArgs e) {
            rearrangeEnagagedItems(SelectedStep);
        }

        private void treeViewStep_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                treeViewStep.SelectedNode = e.Node;
            }
        }

        private void treeViewStep_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (IsFeatureNodeSelected) {
                contextFeatureEdit_Click(sender, e);
            }

            if (IsStateNodeSelected) {
                if (!SelectedState.IsGotoNull()) {
                    treeViewStep.SelectedNode = treeViewStep.GetNode(SelectedState.Goto.ToString());
                } else if (!SelectedState.IsTaxIDNull()) {
                    if (!SelectedState.TaxonRow.IsHigher) {
                        editSpecies((TaxonomicIndex.TaxonRow)SelectedState.TaxonRow);
                    }
                }
            }
        }

        private void listViewEngagement_ItemActivate(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewEngagement.SelectedItems) {
                editSpecies(Data.Taxon.FindByID(item.GetID()) as TaxonomicIndex.TaxonRow);
            }
        }

        private void buttonTry_Click(object sender, EventArgs e) {
            //Species.Display mainForm = new Species.Display(Data, SelectedStep);
            //mainForm.ShowDialog(this);
        }

        #endregion
    }
}