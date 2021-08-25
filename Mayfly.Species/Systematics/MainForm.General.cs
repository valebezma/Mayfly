using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mayfly.Software;
using Mayfly.Extensions;
using Mayfly;
using Mayfly.TaskDialogs;
using System.IO;
using Mayfly.Controls;

namespace Mayfly.Species.Systematics
{
    partial class MainForm
    {
        private string filename;

        public string FileName
        {
            set
            {
                this.ResetText(value ?? IO.GetNewFileCaption(UserSettings.Interface.Extension), EntryAssemblyInfo.Title);
                filename = value;
            }

            get
            {
                return filename;
            }
        }

        public SpeciesKey Data { set; get; }

        public bool IsChanged { set; get; }




        private void LoadData(string filename)
        {
            //Clear();

            FileName = filename;
            Data.Read(FileName);

            LoadList();

            if (Data.IsKeyAvailable)
            {
                LoadKey();
            }
            else
            {
                LoadEngagedList();
            }
            //status.Message(Resources.Messages.Loaded);
            IsChanged = false;
        }

        private DialogResult CheckAndSave()
        {
            if (IsChanged)
            {
                TaskDialogButton b = taskDialogSaveChanges.ShowDialog(this);

                if (b == tdbSave)
                {
                    menuItemSave_Click(menuItemSave, new EventArgs());
                    return DialogResult.OK;
                }
                else if (b == tdbDiscard)
                {
                    return DialogResult.No;
                }
                else if (b == tdbCancelClose)
                {
                    return DialogResult.Cancel;
                }
            }

            return DialogResult.No;
        }

        private void Save(string filename)
        {
            Data.SaveToFile(filename);
            FileName = filename;
            status.Message(Resources.Messages.Saved);
            IsChanged = false;
        }

        private void Clear()
        {
            FileName = null;
            Data = new SpeciesKey();

            ClearTaxa();
            ClearKey();
            ClearPictures();
        }



    }
}
