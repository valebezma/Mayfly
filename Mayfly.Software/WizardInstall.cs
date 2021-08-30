using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Windows.Forms;
using Mayfly.Extensions;

namespace Mayfly.Software
{
    public partial class WizardInstall : Form
    {
        List<Uri> downloadLinks = new List<Uri>();
        List<string> tempLinks = new List<string>();
        CultureInfo lang { get; set; }
        int progress = 0;

        Scheme.ProductRow SelectedProduct;

        string InstallPath
        {
            get
            {
                return Path.Combine(IO.ProgramFolder, SelectedProduct.Name);
            }
        }


        public WizardInstall()
        {
            InitializeComponent();

            downloadLinks = new List<Uri>();

            comboBoxLang.Items.Add(new CultureInfo("en"));
            comboBoxLang.SelectedIndex = 0;
            backgroundCultures.RunWorkerAsync();

            textBoxUsername.Text = UserSettings.Username == Mayfly.Resources.Interface.UserUnknown ?
                System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName : UserSettings.Username;

            comboBoxProduct.DataSource = Service.GetScheme("Product", "File", "Feature", "FileType", "PropertyHandler", "AddVerb").Product.Select(null, "Name Asc");
            wizardPageLanguage.NextPage = checkBoxOptions.Checked ? wizardPageShortcuts : wizardPageGet;
        }

        

        private void Setup(bool startmenu, bool desktop, bool registerfiletypes, bool registerpropertyhandlers, RegisterFileTypeWhenExistOption replace)
        {
            Service.UpdateStatus(labelStatus, Resources.Interface.Shortcuts);

            foreach (Scheme.FileRow fileRow in SelectedProduct.GetFileRows())
            {
                if (!fileRow.IsFriendlyNameNull() && Path.GetExtension(fileRow.File) == ".exe")
                {
                    Install.RegisterApp(Path.Combine(InstallPath, fileRow.File));

                    if (startmenu)
                    {
                        string startMenu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs", "Mayfly", SelectedProduct.Name);
                        if (!Directory.Exists(startMenu)) Directory.CreateDirectory(startMenu);
                        Install.AddShortcut(startMenu + "\\" + fileRow.FriendlyName.GetLocalizedValue(lang) + ".lnk",
                            Path.Combine(InstallPath, fileRow.File), fileRow.ShortcutTip.GetLocalizedValue(lang));
                    }

                    if (desktop)
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        Install.AddShortcut(desktopPath + "\\" + fileRow.FriendlyName.GetLocalizedValue(lang) + ".lnk",
                            Path.Combine(InstallPath, fileRow.File), fileRow.ShortcutTip.GetLocalizedValue(lang));
                    }
                }
            }

            if (registerfiletypes)
            {
                Service.UpdateStatus(labelStatus, Resources.Interface.RegWork);

                foreach (Scheme.FileRow fileRow in SelectedProduct.GetFileRows())
                {
                    foreach (Scheme.FileTypeRow filetypeRow in fileRow.GetFileTypeRows())
                    {
                        //if (Path.GetExtension(fileRow.File) != ".exe") continue;

                        Service.UpdateStatus(labelStatus, Resources.Interface.RegFileType,
                            filetypeRow.FriendlyName.GetLocalizedValue(lang));

                        // If FileType is already set with other binary - ask for reassociation

                        string application = Install.GetAssociatedExecutablePath(filetypeRow.Extension);

                        if (string.IsNullOrWhiteSpace(application)) goto Associate;

                        if (replace == RegisterFileTypeWhenExistOption.Reassociate) goto Associate;

                        if (replace == RegisterFileTypeWhenExistOption.Keep) continue;

                        if (string.Equals(application, Path.Combine(InstallPath, fileRow.File))) continue;

                        taskDialogReassoc.Content = string.Format(
                            new ResourceManager(this.GetType()).GetString("taskDialogReassoc.Content"), filetypeRow.FriendlyName.GetLocalizedValue(lang), filetypeRow.Extension);
                        taskDialogReassoc.ExpandedInformation = string.Format(
                            new ResourceManager(this.GetType()).GetString("taskDialogReassoc.ExpandedInformation"), application);
                        
                        if (Service.GetButton(taskDialogReassoc) == tdbKeep) continue;

                        Associate:

                        Install.RegisterFileType(filetypeRow.Extension, filetypeRow.ProgID, filetypeRow.FriendlyName.GetLocalizedValue(lang),
                            Path.Combine(InstallPath, fileRow.File) + "," + filetypeRow.IconIndex);

                        Install.RegisterVerb(filetypeRow.ProgID, "open", Resources.Interface.VerbOpen,
                            Path.Combine(InstallPath, fileRow.File) + " \"%1\"", true);

                        foreach (Scheme.AddVerbRow addVerbRow in filetypeRow.GetAddVerbRows())
                        {
                            Install.RegisterVerb(filetypeRow.ProgID, addVerbRow.Verb, addVerbRow.FriendlyName.GetLocalizedValue(lang),
                                string.Format("{0}\\{1} \"%1\" \"-{2}\"", InstallPath, fileRow.File, addVerbRow.Verb), true);
                        }
                    }
                }
            }

            if (registerpropertyhandlers)
            {
                foreach (Scheme.FileRow fileRow in SelectedProduct.GetFileRows())
                {
                    foreach (Scheme.FileTypeRow filetypeRow in fileRow.GetFileTypeRows())
                    {
                        if (filetypeRow.PropertyHandlerRow == null) continue;

                        Service.UpdateStatus(labelStatus, Resources.Interface.RegPropertyHandler, filetypeRow.PropertyHandlerRow.FriendlyName);

                        Install.RegisterPropertyHandler(
                            filetypeRow.PropertyHandlerRow.CLSID, filetypeRow.PropertyHandlerRow.FriendlyName, Path.Combine(InstallPath, filetypeRow.PropertyHandlerRow.Dll),
                            filetypeRow.Extension, filetypeRow.FullDetails, filetypeRow.PreviewDetails, filetypeRow.PreviewTitle, filetypeRow.TileInfo);
                    }
                }
            }

            Service.UpdateStatus(labelStatus, Resources.Interface.RegWork);

            string destinationFilePath = Path.Combine(new DirectoryInfo(InstallPath).Parent.FullName, "mayflysoftware.exe");

            File.Copy(Application.ExecutablePath, destinationFilePath, true);

            RegistryKey cleanKey = Registry.CurrentUser.CreateSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Uninstall\" + SelectedProduct.Name);
            cleanKey.SetValue("DisplayName", SelectedProduct.Name);
            cleanKey.SetValue("UninstallString", destinationFilePath + " \"-remove\" \"" + SelectedProduct.Name + "\"");
            cleanKey.SetValue("Publisher", "Mayfly");
        }



        private void wizardControl_Cancelling(object sender, EventArgs e)
        {
            Close();
        }

        private void wizardControl_Finished(object sender, EventArgs e)
        {
            Close();
        }


        private void backgroundCultures_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = Service.GetAvailableCultures();
        }

        private void backgroundCultures_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO: En + e.Result
            
            foreach (CultureInfo ci in (IEnumerable<CultureInfo>)e.Result)
            {
                //if (!ci.Equals(CultureInfo.CurrentCulture)) continue;

                comboBoxLang.Items.Add(ci);
                comboBoxLang.SelectedItem = ci;
                comboBoxLang_SelectedIndexChanged(sender, e);
                break;
            }

            //wizardPageEula.AllowNext = true;
        }
        

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedProduct = (Scheme.ProductRow)comboBoxProduct.SelectedItem;
        }

        private void wizardPageProduct_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            Uri eula = Server.GetUri(Server.ServerHttps + "/eula",
                SelectedProduct.Name.ToLower().Replace(' ', '_') + ".html", CultureInfo.CurrentUICulture);

            if (!Server.FileExists(eula))
            {
                eula = Server.GetUri(Server.ServerHttps + "/eula",
                  "default.html", CultureInfo.CurrentUICulture);
            }

            webBrowserEula.Navigate(eula);
            webBrowserEula.DocumentCompleted += WebBrowserEula_DocumentCompleted;
        }

        private void WebBrowserEula_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            wizardPageEula.AllowNext = true;
        }

        private void wizardPageEula_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            wizardControl.NextButtonText = Resources.Interface.Agree;
        }

        private void wizardPageEula_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            wizardControl.NextButtonText = Resources.Interface.Next;
        }

        private void wizardPageEula_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            wizardControl.NextButtonText = Resources.Interface.Next;
        }


        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            lang = comboBoxLang.SelectedIndex == 0 ? CultureInfo.InvariantCulture : (CultureInfo)comboBoxLang.SelectedItem;
        }

        private void checkBoxOptions_CheckedChanged(object sender, EventArgs e)
        {
            wizardPageLanguage.NextPage = checkBoxOptions.Checked ? wizardPageShortcuts : wizardPageGet;
        }

        private void wizardPageLanguage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            //textBoxUsername.Text = UserSettings.Username;
        }

        private void wizardPageLanguage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            wizardPageLanguage.AllowNext = !string.IsNullOrWhiteSpace(textBoxUsername.Text);
        }

        private void checkBoxRegFileType_CheckedChanged(object sender, EventArgs e)
        {
            labelFileTypeExists.Visible =
                radioFileTypeReass.Visible =
                radioFiletypeKeep.Visible =
                radioFileTypeAsk.Visible =
                checkBoxRegFileType.Checked;
        }


        private void wizardPageGet_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            foreach (Scheme.FileRow fileRow in SelectedProduct.GetFileRows())
            {
                downloadLinks.Add(Server.GetUri(Service.ServerUpdate,
                    Path.GetFileNameWithoutExtension(fileRow.File) + ".zip"));
            }

            progressBar.Maximum = downloadLinks.Count * 2;

            backgroundDownloader.RunWorkerAsync();
        }

        private void backgroundDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            while (downloadLinks.Count > 0)
            {
                Uri uri = downloadLinks[0];

                string feature = Path.GetFileName(uri.LocalPath);
                Service.UpdateStatus(labelStatus, Resources.Interface.Downloading, feature);

                // Downloading basic feature
                tempLinks.Add(Service.DownloadFile(uri));

                if (lang != CultureInfo.InvariantCulture)
                {
                    // Downloading localization
                    Uri loc = Server.GetLocalizedUri(downloadLinks[0], lang);
                    Service.UpdateStatus(labelStatus, Resources.Interface.DownloadingCulture, feature, lang.DisplayName);

                    try
                    {
                        if (!Directory.Exists(Path.Combine(IO.TempFolder, lang.Name)))
                        {
                            Directory.CreateDirectory(Path.Combine(IO.TempFolder, lang.Name));
                        }
                        
                        Service.DownloadFile(loc, Path.Combine(IO.TempFolder, lang.TwoLetterISOLanguageName, Path.GetFileName(uri.LocalPath)));
                    }
                    catch (WebException) { }
                }

                // Signal
                downloadLinks.RemoveAt(0);
                progress++; backgroundDownloader.ReportProgress(progress);
            }

            Directory.CreateDirectory(InstallPath);

            // Unpack zip
            foreach (string tempFile in tempLinks)
            {
                FileInfo fi = new FileInfo(tempFile);
                Service.UpdateStatus(labelStatus, Resources.Interface.Installing, fi.Name);
                IO.UnpackFiles(tempFile, InstallPath);

                if (lang != CultureInfo.InvariantCulture)
                {
                    fi = new FileInfo(tempFile.Insert(tempFile.LastIndexOf('\\'), '\\' + lang.Name));
                    IO.UnpackFiles(fi.FullName, InstallPath);
                }

                progress++; 
                backgroundDownloader.ReportProgress(progress);
            }

            Setup(Service.IsChecked(checkBoxStart), Service.IsChecked(checkBoxDesktop),
                Service.IsChecked(checkBoxRegFileType), Service.IsChecked(checkBoxRegPropHandler),
                radioFileTypeReass.Checked ? RegisterFileTypeWhenExistOption.Reassociate :
                radioFiletypeKeep.Checked ? RegisterFileTypeWhenExistOption.Keep : RegisterFileTypeWhenExistOption.Ask);
        }

        private void backgroundDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            wizardPageGet.AllowNext = true;
            wizardControl.NextPage();

            UserSetting.InitializeRegistry(UserSettingPaths.KeyGeneral, null,
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.User, textBoxUsername.Text),
                    new UserSetting(UserSettingPaths.ShareDiagnostics, true),
                    new UserSetting(UserSettingPaths.UseUnsafeConnection, true)
                });

            UserSetting.InitializeRegistry(UserSettingPaths.KeyUI, null,
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.FormatCoordinate, "dms"),
                    new UserSetting(UserSettingPaths.Language, lang.ToString())
                });

            UserSetting.InitializeRegistry(UserSettingPaths.KeyFeatures, null,
                new UserSetting[] {
                    new UserSetting(UserSettingPaths.UpdatePolicy, (int)UpdatePolicy.CheckAndNotice)
                });

            IO.ClearTemp();
        }
    }

    enum RegisterFileTypeWhenExistOption
    {
        Reassociate,
        Keep,
        Ask
    }
}
