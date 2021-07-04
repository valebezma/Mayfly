using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Security.AccessControl;
using Mayfly.Software;
using Mayfly.Extensions;
using System.Globalization;

namespace Mayfly.Software
{
    public partial class WizardUninstall : Form
    {
        string InstallPath
        {
            get
            {
                return Path.Combine(FileSystem.ProgramFolder, Product);
            }
        }

        string StartGroup
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs", "Mayfly", Product);
            }
        }

        string Product { get; set; }



        public WizardUninstall(string product)
        {
            InitializeComponent();

            Product = product;
            //SelectedProduct = Service.GetScheme().Product.FindByName(product);

            labelStart.ResetFormatted(Product);

            checkBoxOptions.Enabled =
                wizardPageStart.AllowNext = Directory.Exists(InstallPath);

            labelClose.ResetFormatted(product, product);

            wizardPageStart.NextPage = checkBoxOptions.Checked ? wizardPageOptions : wizardPageClean;

            buttonRetry_Click(this, new EventArgs());
        }



        public void Uninstall(bool keeplicenses, bool keepfeature)
        {
            Scheme scheme = Service.GetScheme();

            //foreach (Scheme.FileRow fileRow in SelectedProduct.GetFileRows())
            foreach (string filename in Directory.GetFiles(InstallPath))
            {
                Scheme.FileRow fileRow = scheme.File.FindByFile(Path.GetFileName(filename));

                if (fileRow == null) continue;

                if (Path.GetExtension(fileRow.File) == ".exe")
                {
                    Install.UnregisterApp(filename);
                    Install.RemoveShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), filename);
                    Service.AppendStatus(textBoxStatus, Resources.Interface.RemoveShortcuts, fileRow.IsFriendlyNameNull() ? fileRow.File : Mayfly.Service.GetLocalizedValue(fileRow.FriendlyName));
                }


                foreach (Scheme.FileTypeRow filetypeRow in fileRow.GetFileTypeRows())
                {
                    if (Install.GetAssociatedExecutablePath(filetypeRow.Extension) != filename) continue;

                    Install.UnregisterFileType(filetypeRow.Extension);
                    Service.AppendStatus(textBoxStatus, Resources.Interface.UnregFileType, filetypeRow.Extension);

                    if (filetypeRow.PropertyHandlerRow == null) continue;

                    Install.UnregisterPropertyHandler(filetypeRow.Extension);
                }
            }

            try
            {
                Directory.Delete(StartGroup, true);
                Service.AppendStatus(textBoxStatus, Resources.Interface.StartGroupRemoved);
            }
            catch { }

            Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\" + Product, false);

            if (!keeplicenses)
            {
                if (Service.IsLast(Product))
                {
                    Registry.CurrentUser.DeleteSubKey(@"Software\Mayfly\Licenses\", false);
                }
            }

            if (!keepfeature)
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Mayfly\Features\" + Product, false);
            }

            if (Service.IsLast(Product))
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Mayfly\UI\", false);
            }

            Service.AppendStatus(textBoxStatus, Resources.Interface.UnregWork);

            // Clearing temp folder
            try
            {
                FileSystem.ClearTemp();
            }
            catch { }

            // Removing files and directories
            try
            {
                Directory.Delete(InstallPath, true);
                Service.AppendStatus(textBoxStatus, Resources.Interface.RemoveFiles);
            }
            catch { }

            // Clearing logs
            try
            {
                Log.SendLog();
                Log.ClearLog(DateTime.Today.AddDays(1));
            }
            catch { }
        }



        private void wizardControl_Finished(object sender, EventArgs e)
        {
            //Close();
        }

        private void wizardControl_Cancelled(object sender, EventArgs e)
        {
            Close();
        }
        

        private void buttonRetry_Click(object sender, EventArgs e)
        {
            bool running = false;

            //foreach (Scheme.FileRow fileRow in SelectedProduct.GetFileRows())
            foreach (string filename in Directory.GetFiles(InstallPath))
            {
                running |= Service.IsRunning(Path.Combine(InstallPath, filename));
            }

            pictureBox1.Visible =
                buttonRetry.Visible =
                labelClose.Visible = running;

            labelInstruction.Visible =
                wizardPageStart.AllowNext =
                checkBoxOptions.Enabled = !running;
        }

        private void checkBoxOptions_CheckedChanged(object sender, EventArgs e)
        {
            wizardPageStart.NextPage = checkBoxOptions.Checked ? wizardPageOptions : wizardPageClean;
        }

        private void wizardPageClean_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            // TODO: Check if somethng is running from InstallPath



            backCleaner.RunWorkerAsync();
        }

        private void backCleaner_DoWork(object sender, DoWorkEventArgs e)
        {
            Uninstall(Service.IsChecked(checkBoxLicenses), Service.IsChecked(checkBoxFeatures));
        }

        private void backCleaner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;

            wizardPageClean.AllowNext = true;
            //wizardControl.NextPage();
        }
    }
}