using Mayfly.Extensions;
using Mayfly.Software;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Mayfly.Software.Management
{
    public partial class MainForm : Form
    {
        Scheme SchemeData { get; set; }

        Scheme.ProductRow SelectedProductRow { get; set; }

        Scheme.FileRow SelectedFileRow { get; set; }



        public MainForm()
        {
            InitializeComponent();
            
            listViewProducts.Shine();
            listViewFiles.Shine();

            openFile.InitialDirectory = Application.StartupPath;
            openFile.Filter = FileSystem.FilterFromExt(".exe", ".dll");

            columnExtIndex.ValueType = typeof(int);

            foreach (Label label in new Label[] {
                labelFeatureCount, labelProductCount })
            {
                label.UpdateStatus(0);
            }

            SchemeData = Software.Service.GetScheme();

            UpdateAll();
        }



        private void SetAttention(DataGridViewCell gridCell)
        {
            gridCell.Style.BackColor = Color.Bisque;//.Lighter();
        }

        private void ResetAttention(DataGridViewCell gridCell)
        {
            gridCell.Style = gridCell.OwningColumn.DefaultCellStyle;
        }

        private void UpdateAll()
        {
            LoadProducts();
            LoadFiles();
            LoadFileValues();
        }

        private void LoadProducts()
        {
            listViewProducts.Items.Clear();

            foreach (Scheme.ProductRow productRow in SchemeData.Product)
            {
                ListViewItem li = listViewProducts.CreateItem(productRow.ID.ToString(), productRow.Name);
            }

            labelProductCount.UpdateStatus(SchemeData.Product.Count);
        }

        private void LoadFiles()
        {
            listViewFiles.Items.Clear();

            foreach (Scheme.FileRow fileRow in SchemeData.File)
            {
                AddFileItem(fileRow);
            }

            UpdateFilesStatus();
        }

        private void UpdateFilesStatus()
        {
            if (SelectedProductRow == null)
                labelFeatureCount.UpdateStatus(listViewFiles.Items.Count);
            else
            {
                int f = 0;
                foreach (ListViewItem li in listViewFiles.Items)
                {
                    if (li.Checked) f++;
                }
                labelFeatureCount.UpdateStatus(f);
            }
        }

        private void LoadFileValues()
        {
            if (SelectedFileRow == null)
            {
                spreadSheetSat.Rows.Clear();
                spreadSheetVer.Rows.Clear();
                spreadSheetExt.Rows.Clear();

                labelFileTitle.ResetFormatted("None");

                textBoxFilename.Text =
                    textBoxFileFriendly.Text =
                    textBoxFileTip.Text = string.Empty;

                //labelSat.UpdateStatus(0);
                //labelExt.UpdateStatus(0);
                //labelVersionsCount.UpdateStatus(0);
            }
            else
            {
                labelFileTitle.ResetFormatted(SelectedFileRow.File);

                textBoxFilename.Text = SelectedFileRow.File;
                textBoxFileFriendly.Text = SelectedFileRow.IsFriendlyNameNull() ? string.Empty : SelectedFileRow.FriendlyName;
                textBoxFileTip.Text = SelectedFileRow.IsShortcutTipNull() ? string.Empty : SelectedFileRow.ShortcutTip;

                spreadSheetSat.Rows.Clear();

                //int s = 0;
                foreach (Scheme.SatelliteRow satRow in SelectedFileRow.GetSatelliteRows())
                {
                    //s++;

                    DataGridViewRow typeLine = new DataGridViewRow();
                    typeLine.CreateCells(spreadSheetSat);
                    //typeLine.MinimumHeight = 40; //spreadSheetVersions.RowTemplate.Height;

                    typeLine.Cells[columnSatPath.Index].Value = satRow.Path;

                    if (satRow.IsLocalizableNull()) typeLine.Cells[columnSatLocal.Index].Value = false;
                    else typeLine.Cells[columnSatLocal.Index].Value = satRow.Localizable;

                    spreadSheetSat.Rows.Add(typeLine);
                }

                //labelSat.UpdateStatus(s);



                spreadSheetExt.Rows.Clear();

                //int t = 0;
                foreach (Scheme.FileTypeRow extRow in SelectedFileRow.GetFileTypeRows())
                {
                    //t++;

                    DataGridViewRow extLine = new DataGridViewRow();
                    extLine.CreateCells(spreadSheetExt);
                    //typeLine.MinimumHeight = 40; //spreadSheetVersions.RowTemplate.Height;

                    extLine.Cells[columnExt.Index].Value = extRow.Extension;
                    extLine.Cells[columnExtFriendly.Index].Value = extRow.IsFriendlyNameNull() ? null : extRow.FriendlyName;
                    extLine.Cells[columnExtProgid.Index].Value = extRow.IsProgIDNull() ? null : extRow.ProgID;
                    extLine.Cells[columnExtIndex.Index].Value = extRow.IsIconIndexNull() ? 1 : extRow.IconIndex;
                    extLine.Cells[columnExtFullDetails.Index].Value = extRow.IsFullDetailsNull() ? null : extRow.FullDetails;
                    extLine.Cells[columnExtPreviewDetails.Index].Value = extRow.IsPreviewDetailsNull() ? null : extRow.PreviewDetails;
                    extLine.Cells[columnExtPreviewTitle.Index].Value = extRow.IsPreviewTitleNull() ? null : extRow.PreviewTitle;
                    spreadSheetExt.Rows.Add(extLine);

                    //spreadSheetVersions.AutoResizeRow(versionLine.Index, DataGridViewAutoSizeRowMode.AllCells);
                }

                //labelExt.UpdateStatus(t);


                spreadSheetVer.Rows.Clear();

                //int u = 0;
                foreach (Scheme.VersionRow versionRow in SelectedFileRow.GetVersionRows())
                {
                    //u++;

                    DataGridViewRow versionLine = new DataGridViewRow();
                    versionLine.CreateCells(spreadSheetVer);
                    //versionLine.MinimumHeight = 40; //spreadSheetVersions.RowTemplate.Height;

                    versionLine.Cells[columnVer.Index].Value = versionRow.Version;
                    versionLine.Cells[columnVerPublished.Index].Value = versionRow.IsPublishedNull() ? DateTime.Now : versionRow.Published;
                    if (!versionRow.IsChangesNull()) versionLine.Cells[columnVerNotes.Index].Value = versionRow.Changes;

                    spreadSheetVer.Rows.Add(versionLine);

                    //spreadSheetVersions.AutoResizeRow(versionLine.Index, DataGridViewAutoSizeRowMode.AllCells);
                }

                //labelVersionsCount.UpdateStatus(u);

                if (File.Exists(SelectedFileRow.File))
                {
                    spreadSheetVer.AllowUserToAddRows =
                        (new Version(FileVersionInfo.GetVersionInfo(SelectedFileRow.File).FileVersion) > SelectedFileRow.GetLatestVersion());
                }
            }

            textBoxFilename.Enabled =
                textBoxFileFriendly.Enabled =
                textBoxFileTip.Enabled =
                spreadSheetSat.Enabled =
                spreadSheetVer.Enabled =
                spreadSheetExt.Enabled = (SelectedFileRow != null);

        }

        private ListViewItem AddFileItem(Scheme.FileRow fileRow)
        {
            ListViewItem li = listViewFiles.CreateItem(fileRow.ID.ToString(), fileRow.File);

            switch (Path.GetExtension(fileRow.File))
            {
                case ".exe":
                    li.Group = listViewFiles.Groups[0];
                    break;
                case ".dll":
                    li.Group = listViewFiles.Groups[1];
                    break;
            }

            li.UpdateItem(new object[] { fileRow.GetLatestVersion() });

            if (File.Exists(fileRow.File))
            {
                if (new Version(FileVersionInfo.GetVersionInfo(fileRow.File).FileVersion) > fileRow.GetLatestVersion())
                {
                    li.ForeColor = Color.Green;
                }
            }
            else
            {
                li.ForeColor = Color.Red;
            }

            return li;
        }

        private void UpdateSatellites()
        {
            if (SelectedFileRow == null) return;
            if (!SchemeData.File.Contains(SelectedFileRow)) return;

            while (SelectedFileRow.GetSatelliteRows().Length > 0)
            {
                SchemeData.Satellite.RemoveSatelliteRow(SelectedFileRow.GetSatelliteRows()[0]);
            }

            foreach (DataGridViewRow gridRow in spreadSheetSat.Rows)
            {
                if (gridRow.IsNewRow) continue;

                string sat = (string)spreadSheetSat[columnSatPath.Index, gridRow.Index].Value;
                bool loc = spreadSheetSat[columnSatLocal.Index, gridRow.Index].Value != null && (bool)spreadSheetSat[columnSatLocal.Index, gridRow.Index].Value;
                Scheme.SatelliteRow satRow = SchemeData.Satellite.AddSatelliteRow(SelectedFileRow, sat, loc);
                if (!loc) satRow.SetLocalizableNull();
            }
        }

        private void UpdateExtensions()
        {
            if (SelectedFileRow == null) return;
            if (!SchemeData.File.Contains(SelectedFileRow)) return;

            while (SelectedFileRow.GetFileTypeRows().Length > 0)
            {
                SchemeData.FileType.RemoveFileTypeRow(SelectedFileRow.GetFileTypeRows()[0]);
            }

            foreach (DataGridViewRow gridRow in spreadSheetExt.Rows)
            {
                if (gridRow.IsNewRow) continue;

                Scheme.FileTypeRow extRow = SchemeData.FileType.NewFileTypeRow();

                string ext = (string)spreadSheetExt[columnExt.Index, gridRow.Index].Value;
                extRow.Extension = ext;

                string friendly = (string)spreadSheetExt[columnExtFriendly.Index, gridRow.Index].Value;
                extRow.FriendlyName = friendly;

                string progid = (string)spreadSheetExt[columnExtProgid.Index, gridRow.Index].Value;
                extRow.ProgID = progid;

                extRow.FileRow = SelectedFileRow;
                extRow.IconIndex = (int)spreadSheetExt[columnExtIndex.Index, gridRow.Index].Value;

                SchemeData.FileType.AddFileTypeRow(extRow);
            }
        }




        private void spreadSheet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell editedCell = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];

            if (editedCell.Value == null) return;

            if (string.IsNullOrEmpty(editedCell.Value.ToString()))
            {
                editedCell.Value = null;
            }
        }


        private void menuItemUpdate_Click(object sender, EventArgs e)
        {
            UpdateSatellites();
            UpdateExtensions();

            Hide();
            WizardUpload roller = new WizardUpload(SchemeData);
            roller.SetFriendlyDesktopLocation(this, FormLocation.Centered);

            if (roller.ShowDialog(this) == DialogResult.OK)
            {
                SchemeData.WriteXml(Path.Combine(Application.StartupPath, "scheme", string.Format("{0:yyyy-MM-dd-HH-mm}.xml", DateTime.Now)));
                Close();
            }
            else
            {
                Show();
            }
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void buttonProductAdd_Click(object sender, EventArgs e)
        {
            if (inputProduct.ShowDialog(this) == DialogResult.OK)
            {
                Scheme.ProductRow productRow = SchemeData.Product.AddProductRow(inputProduct.Input);
                listViewProducts.CreateItem(productRow.ID.ToString(), productRow.Name);
            }
        }

        private void contextProduct_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextProductRemove.Enabled = listViewProducts.SelectedItems.Count > 0;
        }

        private void contextProductRemove_Click(object sender, EventArgs e)
        {
            while(listViewProducts.SelectedItems.Count > 0)
            {
                ListViewItem li = listViewProducts.SelectedItems[0];

                SchemeData.Product.RemoveProductRow(SchemeData.Product.FindByID(li.GetID()));
                listViewProducts.Items.Remove(li);
            }
        }

        private void listViewProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewFiles.Shine();

            if (listViewProducts.SelectedItems.Count == 0 || listViewProducts.SelectedItems.Count > 1)
            {
                SelectedProductRow = null;

                foreach (ListViewItem li in listViewFiles.Items)
                {
                    li.Checked = false;
                }
            }
            else
            {
                SelectedProductRow = SchemeData.Product.FindByID(listViewProducts.GetID());
                
                foreach (ListViewItem li in listViewFiles.Items)
                {
                    li.Checked = SelectedProductRow.GetFileRows().Contains(SchemeData.File.FindByID(li.GetID()));
                }
            }

            listViewFiles.CheckBoxes = SelectedProductRow != null;
            UpdateFilesStatus();
        }


        private void buttonFileAdd_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (SchemeData.File.FindByFile(Path.GetFileName(openFile.FileName)) == null)
                {
                    Scheme.FileRow fileRow = SchemeData.File.AddFileRow(Path.GetFileName(openFile.FileName), null, null);

                    ListViewItem li = AddFileItem(fileRow);
                    listViewFiles.Focus();
                    listViewFiles.SelectedItems.Clear();
                    li.Selected = true;
                    li.Checked = (SelectedProductRow != null);
                }
            }
        }

        private void contextFile_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextFileRemove.Enabled = listViewFiles.SelectedItems.Count > 0;
        }

        private void contextFileRemove_Click(object sender, EventArgs e)
        {
            while (listViewFiles.SelectedItems.Count > 0)
            {
                ListViewItem li = listViewFiles.SelectedItems[0];

                SchemeData.File.RemoveFileRow(SchemeData.File.FindByID(li.GetID()));
                listViewFiles.Items.Remove(li);
            }
        }

        private void listViewFiles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listViewFiles.ContainsFocus)
            {
                if (e.Item.Checked)
                {
                    e.Item.Selected = true;
                    SchemeData.Feature.AddFeatureRow(SelectedProductRow, SelectedFileRow);
                }
                else
                { 
                    Scheme.FeatureRow featureRow = SchemeData.Feature.FindByProdIDFileID(SelectedProductRow.ID, SelectedFileRow.ID);
                    SchemeData.Feature.RemoveFeatureRow(featureRow);
                }

                UpdateFilesStatus();
            }
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSatellites();
            UpdateExtensions();

            if (listViewFiles.SelectedItems.Count == 0 || listViewFiles.SelectedItems.Count > 1)
            {
                SelectedFileRow = null;
            }
            else
            {
                SelectedFileRow = SchemeData.File.FindByID(listViewFiles.GetID());
            }

            LoadFileValues();
        }


        private void textBoxFilename_TextChanged(object sender, EventArgs e)
        {
            if (SelectedFileRow == null) return;

            if (string.IsNullOrWhiteSpace(textBoxFilename.Text))
                SelectedFileRow.SetFileNull();
            else SelectedFileRow.File = textBoxFilename.Text;

            listViewFiles.SelectedItems[0].Text = SelectedFileRow.File;

            labelFileTitle.ResetFormatted(SelectedFileRow.File);
        }

        private void textBoxFileFriendly_TextChanged(object sender, EventArgs e)
        {
            if (SelectedFileRow == null) return;

            if (string.IsNullOrWhiteSpace(textBoxFileFriendly.Text))
                SelectedFileRow.SetFriendlyNameNull();
            else SelectedFileRow.FriendlyName = textBoxFileFriendly.Text;

            textBoxFileTip.Enabled = !string.IsNullOrWhiteSpace(textBoxFileFriendly.Text);
        }

        private void textBoxFileTip_TextChanged(object sender, EventArgs e)
        {
            if (SelectedFileRow == null) return;

            if (string.IsNullOrWhiteSpace(textBoxFileTip.Text))
                SelectedFileRow.SetShortcutTipNull();
            else SelectedFileRow.ShortcutTip = textBoxFileTip.Text;
        }


        private void spreadSheetSat_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!spreadSheetSat.ContainsFocus) return;

            string sat = (string)spreadSheetSat[columnSatPath.Index, e.RowIndex].Value;

            if (!File.Exists(Path.Combine(Application.StartupPath, sat)))
                SetAttention(spreadSheetSat[columnSatPath.Index, e.RowIndex]);
            else ResetAttention(spreadSheetSat[columnSatPath.Index, e.RowIndex]);
        }


        private void checkBoxExtDetails_CheckedChanged(object sender, EventArgs e)
        {
            columnExtFullDetails.Visible = columnExtPreviewDetails.Visible = columnExtPreviewTitle.Visible = checkBoxExtDetails.Checked;
        }

        private void spreadSheetExt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!spreadSheetExt.ContainsFocus) return;

            if (spreadSheetExt[columnExt.Index, e.RowIndex].Value == null)
                SetAttention(spreadSheetExt[columnExt.Index, e.RowIndex]);
            else ResetAttention(spreadSheetExt[columnExt.Index, e.RowIndex]);

            if (spreadSheetExt[columnExtFriendly.Index, e.RowIndex].Value == null)
                SetAttention(spreadSheetExt[columnExtFriendly.Index, e.RowIndex]);
            else ResetAttention(spreadSheetExt[columnExtFriendly.Index, e.RowIndex]);

            if (spreadSheetExt[columnExtProgid.Index, e.RowIndex].Value == null)
                SetAttention(spreadSheetExt[columnExtProgid.Index, e.RowIndex]);
            else ResetAttention(spreadSheetExt[columnExtProgid.Index, e.RowIndex]);
        }
        

        private void spreadSheetVer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!spreadSheetVer.ContainsFocus) return;

            string version = (string)spreadSheetVer[columnVer.Index, e.RowIndex].Value;

            Scheme.VersionRow versionRow = SchemeData.Version.FindByFileIDVersion(listViewFiles.GetID(), version);

            if (versionRow == null)
            {
                versionRow = SchemeData.Version.NewVersionRow();
                versionRow.FileRow = SelectedFileRow;
                versionRow.Version = version;
                SchemeData.Version.AddVersionRow(versionRow);
            }

            if (spreadSheetVer[columnVerNotes.Index, e.RowIndex].Value == null) { versionRow.SetChangesNull(); }
            else { versionRow.Changes = (string)spreadSheetVer[columnVerNotes.Index, e.RowIndex].Value; }

        }

        bool newversioninputstarted = false;

        private void spreadSheetVer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == spreadSheetVer.NewRowIndex)
            {
                spreadSheetVer[columnVer.Index, e.RowIndex].Value = FileVersionInfo.GetVersionInfo(SelectedFileRow.File).FileVersion;
                spreadSheetVer[columnVerPublished.Index, e.RowIndex].Value = DateTime.Today.AddDays(1);
                newversioninputstarted = true;
            }
        }

        private void spreadSheetVer_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (newversioninputstarted)
            {
                spreadSheetVer.AllowUserToAddRows = false;
                newversioninputstarted = false;
            }
        }
    }
}
