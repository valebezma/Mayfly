using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mayfly.TaskDialogs;
using System.Net;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Wild
{
    public partial class DialogReference : Form
    {
        public OpenFileDialog OpenDialog { get; set; }

        public string DefaultName { get; set; }

        public string FileTypeFriendlyName { get; set; }

        public Uri DownloadUri { get; set; }

        public string FilePath { get; set; }



        public DialogReference(OpenFileDialog openDialog, string defaultName, Uri uri)
        {
            InitializeComponent();

            OpenDialog = openDialog;
            DefaultName = defaultName;
            DownloadUri = uri;
            FileTypeFriendlyName = IO.GetFriendlyFiletypeName(Path.GetExtension(DefaultName));
            //FileTypeIcon = Mayfly.Service.GetIcon(FileSystem.GetIconSource(Path.GetExtension(DefaultName)), new Size(128, 128));

            labelGetInstruction.ResetFormatted(FileTypeFriendlyName, DownloadUri);
            Icon extIcon = Mayfly.Service.GetIcon(IO.GetIconSource(Path.GetExtension(DefaultName)), new Size(128, 128));
            if (extIcon != null) pictureIcon.Image = extIcon.ToBitmap();

            resetDialog();
        }



        private void resetDialog()
        {
            taskDialogMissingReference.CustomMainIcon = Mayfly.Service.GetIcon(IO.GetIconSource(Path.GetExtension(DefaultName)), new Size(32, 32));
            taskDialogMissingReference.MainInstruction = string.Format(taskDialogMissingReference.MainInstruction, FileTypeFriendlyName);
            tdbBrowse.CommandLinkNote = string.Format(tdbBrowse.CommandLinkNote, FileTypeFriendlyName);
            tdbDownload.CommandLinkNote = string.Format(tdbDownload.CommandLinkNote, FileTypeFriendlyName);
            tdbCreate.CommandLinkNote = string.Format(tdbCreate.CommandLinkNote, Path.GetFileNameWithoutExtension(DefaultName));

            //taskDialogMissingReference.WindowTitle = ProductName;

            tdbDownload.Enabled = Server.FileExists(DownloadUri);
        }



        private void dialogReference_Load(object sender, EventArgs e)
        {
            Start:

            TaskDialogButton tdb = taskDialogMissingReference.ShowDialog(this);

            if (tdb == tdbBrowse)
            {
                if (OpenDialog.ShowDialog(this) == DialogResult.OK)
                {
                    FilePath = OpenDialog.FileName;
                    Close();
                }
                else
                {
                    goto Start;
                }
            }
            else if (tdb == tdbCreate)
            {
                FilePath = Path.Combine(UserSettings.FieldDataFolder, DefaultName);
                File.Create(this.FilePath).Close();
                Close();
            }
            else if (tdb == tdbDownload)
            {
                try
                {
                    if (DownloadUri == null) return;

                    WebClient webClient = new WebClient();
                    FilePath = Path.Combine(UserSettings.FieldDataFolder, Path.GetFileName(DownloadUri.LocalPath));
                    webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
                    webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                    webClient.DownloadFileAsync(DownloadUri, FilePath);
                }
                catch
                {
                    goto Start;
                }
            }
            else
            {
                Close();
            }
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Close();
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}
