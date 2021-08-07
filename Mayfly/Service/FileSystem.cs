﻿using Mayfly.Extensions;
using Microsoft.Win32;
using Shell32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace Mayfly
{
    public static class FileSystem
    {
        public static string FilterFromExt(string extension)
        {
            return string.Format("{0}|*{1}", GetFriendlyFiletypeName(extension), extension);
        }

        public static string GetFriendlyFiletypeName(string extension)
        {
            string result = string.Empty;

            foreach (RegistryKey classKey in new RegistryKey[] {
                Registry.ClassesRoot,
                Registry.CurrentUser.OpenSubKey(@"Software\Classes"),
                Registry.LocalMachine.OpenSubKey(@"Software\Classes")  })
            {

                RegistryKey registryKey = classKey.OpenSubKey(extension);

                if (registryKey == null) continue;
                string progID = (string)registryKey.GetValue(string.Empty);
                if (progID == null) continue;
                registryKey = classKey.OpenSubKey(progID);
                if (registryKey == null) continue;
                string resource = (string)registryKey.GetValue("FriendlyTypeName");
                if (resource == null)
                {
                    result = (string)registryKey.GetValue(null);
                    if (result != null) return result;
                }
                else
                {
                    if (resource.Contains(".dll") || resource.Contains(".exe")) // 'resource' is library or executable
                    {
                        string path = resource.Substring(1, Math.Max(0, resource.IndexOf(',') - 1));
                        uint index = uint.Parse(resource.Substring(resource.IndexOf(',') + 2));
                        result = Service.GetValue(path, index);
                        if (!string.IsNullOrWhiteSpace(result)) return result;
                    }
                    else // 'resource' is string
                    {
                        return resource;
                    }
                }
            }

            return extension.ToLower() + " files";
        }

        public static string GetIconSource(string extension)
        {
            foreach (RegistryKey classKey in new RegistryKey[] {
                Registry.ClassesRoot,
                Registry.CurrentUser.OpenSubKey(@"Software\Classes"),
                Registry.LocalMachine.OpenSubKey(@"Software\Classes")  })
            {
                RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(extension);
                if (registryKey == null) continue;
                string progID = (string)registryKey.GetValue(string.Empty);
                if (progID == null) continue;
                registryKey = Registry.ClassesRoot.OpenSubKey(progID + "\\DefaultIcon");
                if (registryKey == null) continue;
                string resource = (string)registryKey.GetValue(null);
                if (resource == null) continue;
                return resource;
            }

            return string.Empty;
        }

        private static string FilterFromExtNotRegistered(string extension)
        {
            return string.Format("{0} files|*{1}", extension.TrimStart('.').ToUpperInvariant(), extension);
        }

        public static string FilterFromExt(bool includeAll, params string[] extensions)
        {
            List<string> result = new List<string>();
            
            if (includeAll && extensions.Length > 1)
            {
                string all = Resources.Interface.AllSupported + "|";

                foreach (string extension in extensions)
                {
                    all += "*" + extension + ";";
                }

                result.Add(all.Trim(';'));
            }

            foreach (string extension in extensions)
            {
                result.Add(FilterFromExt(extension));
            }

            return result.ToArray().Merge("|");
        }

        public static string FilterFromExt(params string[] extensions)
        {
            return FilterFromExt(true, extensions);
        }


        public static string GetNewFileCaption(string extension)
        {
            return string.Format(Resources.Interface.FileNew, FileSystem.GetFriendlyFiletypeName(extension).ToLower());
        }

        
        public static string FolderName(string fileName)
        {
            if (fileName == null) return null;
            if (!fileName.IsAcceptable()) return null;
            FileInfo fi = new FileInfo(fileName);
            return fi.DirectoryName;
        }

        public static string SuggestName(string folder, string shortFileName)
        {
            if (folder == null) return shortFileName;
            if (folder == string.Empty) return shortFileName;

            FileInfo result = new FileInfo(string.Format(@"{0}\{1}", folder, shortFileName));
            if (result.Directory.Exists)
            {
                int counter = 2;
                while (result.Exists)
                {
                    result = new FileInfo(string.Format(@"{0}\{1}", folder,
                        shortFileName.Insert(shortFileName.Length - result.Extension.Length,
                        counter.ToString(" (0)"))));
                    counter++;
                }
            }
            else
            {
                return shortFileName;
            }

            return result.Name;
        }

        public static string LocalizedPath(string fileName)
        {
            return string.Format("{0}\\{1}\\{2}", Application.StartupPath,
                System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName, fileName);
        }

        public static string[] MaskedNames(string[] entries, string[] extensions)
        {
            List<string> result = new List<string>();

            foreach (string extension in extensions)
            {
                result.AddRange(MaskedNames(entries, extension));
            }

            return result.ToArray();
        }
        
        public static string[] MaskedNames(string[] entries, string extension)
        {
            List<string> result = new List<string>();
            foreach (string entry in entries)
            {
                string[] results = MaskedNames(entry, extension);
                result.AddRange(results);
            }
            return result.ToArray();
        }

        public static string[] MaskedNames(string entry, string extension)
        {
            List<string> result = new List<string>();

            FileInfo file = new FileInfo(entry);

            if (file.Exists)
            {
                if (file.Extension == extension + "s")
                {
                    string[] filenames = File.ReadAllLines(file.FullName);
                    List<string> recoveredpaths = new List<string>();

                    foreach (string filename in filenames)
                    {
                        if (File.Exists(filename))
                        {
                            recoveredpaths.Add(filename);
                        }
                        else
                        {
                            string recoveredpath = Path.Combine(Path.GetDirectoryName(entry), filename);
                            if (File.Exists(recoveredpath))
                            {
                                recoveredpaths.Add(recoveredpath);
                            }
                        }
                    }

                    result.AddRange(MaskedNames(recoveredpaths.ToArray(), extension));
                }
                else  if (file.Extension == extension)
                {
                    result.Add(entry);
                }
                else if (file.Extension == ".lnk")
                {
                    string[] results = MaskedNames(ResolveShortcut(entry), extension);
                    result.AddRange(results);
                }
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(entry);

                if (directory.Exists)
                {
                    string[] entries = Directory.GetFileSystemEntries(directory.FullName);
                    string[] results = MaskedNames(entries, extension);
                    result.AddRange(results);
                }
            }

            return result.ToArray();
        }

        public static string GetCommonPath(IEnumerable<string> entries)
        {
            if (entries.Count() == 0) return string.Empty;

            if (entries.Count() == 1) return entries.ElementAt(0);

            var matchingChars =
                                from len in Enumerable.Range(0, entries.Min(s => s.Length)).Reverse()
                                let possibleMatch = entries.First().Substring(0, len)
                                where entries.All(f => f.StartsWith(possibleMatch))
                                select possibleMatch;

            string completematch = matchingChars.First();

            return completematch.Contains('\\') ? completematch.Substring(0, completematch.LastIndexOf('\\')) : completematch;
        }

        public static string GetFriendlyCommonName(IEnumerable<string> entries)
        {
            string commonPath = FileSystem.GetCommonPath(entries);
            if (string.IsNullOrWhiteSpace(commonPath)) return string.Empty;
            string directory = Path.GetDirectoryName(commonPath);
            return Path.GetFileName(directory);
        }

        public static string GetPath(object path)
        {
            if (path == null)
            {
                return null;
            }

            if (!(path is string))
            {
                return null;
            }

            if (!((string)path).IsAcceptable())
            {
                return null;
            }

            if (File.Exists((string)path))
            {
                return path.ToString();
            }

            if (Directory.Exists((string)path))
            {
                return path.ToString();
            }

            return null;
        }


        public static string ResolveShortcut(string path)
        {
            ShellLink shell = new ShellLink();
            try {
                shell.Open(path);
                return shell.Target;
            } catch {
                return string.Empty;
            }
        }

        public static bool RunFile(string fileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            try
            {
                process.Start();
                return true;
            }
            catch (Win32Exception)
            {
                return false;
            }
        }

        public static void RunFile(string fileName, params object[] args)
        {
            FileInfo fi = new FileInfo(fileName);
            Process process = new Process();

            if (fi.Extension.ToLowerInvariant() == ".exe")
            {
                process.StartInfo.FileName = fileName;

                process.StartInfo.Arguments = string.Empty;

                foreach (object arg in args)
                {
                    process.StartInfo.Arguments += (arg.ToString().Contains(' ') ? "\"" + arg.ToString() + "\"" : arg.ToString()) + " ";
                }
            }
            else
            {
                string appID = Registry.CurrentUser.OpenSubKey(
                    @"Software\Classes\" + fi.Extension).GetValue(string.Empty).ToString();
                string appPath = Registry.CurrentUser.OpenSubKey(
                    @"Software\Classes\" + appID + @"\Shell\Open\Command").GetValue(string.Empty).ToString();
                process.StartInfo.FileName = appPath.Substring(0, appPath.Length - 5);
                process.StartInfo.Arguments = "\"" + fileName + "\" " + args.Merge(" ");
            }

            try
            {
                process.Start();
            }
            catch
            {
                return;
            }
        }


        public static void AppendPath(List<string> result, string path)
        {
            if (Directory.Exists(path))
            {
                foreach (DirectoryInfo dirInfo in new DirectoryInfo(path).GetDirectories())
                {
                    AppendPath(result, Path.Combine(path, dirInfo.Name));
                }

                foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
                {
                    AppendPath(result, Path.Combine(path, fileInfo.Name));
                }
            }
            else
            {
                if (File.Exists(path))
                {
                    result.Add(path);
                }
            }
        }


        public static string GetTempFileName()
        {
            return TempFolder + new FileInfo(Path.GetTempFileName()).Name;
        }

        public static string GetTempFileName(string extension)
        {
            return Path.ChangeExtension(GetTempFileName(), extension);
        }

        public static void ClearTemp()
        {
            DirectoryInfo di = new DirectoryInfo(TempFolder);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public static string TempFolder
        {
            get
            {
                string result = Path.GetTempPath() + "Mayfly\\";

                if (!Directory.Exists(result)) {
                    Directory.CreateDirectory(result);
                }

                return result;
            }
        }

        public static string UserFolder
        {
            get
            {
                string result = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "Mayfly");

                if (!Directory.Exists(result)) {
                    Directory.CreateDirectory(result);
                }

                return result;
            }
        }

        public static string ProgramFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mayfly");
            }
        }




        public static string PackFiles(string[] files)
        {
            string result = FileSystem.GetTempFileName(".zip");
            PackFiles(files, result);
            return result;
        }

        public static void PackFiles(string[] files, string destinationFile)
        {
            ZipArchive zip = ZipFile.Open(destinationFile, ZipArchiveMode.Create);

            foreach (string file in files)
            {
                zip.CreateEntryFromFile(file, file);
            }

            zip.Dispose();
        }

        public static void UnpackFiles(string sourceFile, string destinationPath)
        {
            if (!File.Exists(sourceFile)) return;

            FileStream stream = File.OpenRead(sourceFile);

            try
            {
                ZipArchive zipArchive = new ZipArchive(stream);

                foreach (ZipArchiveEntry entry in zipArchive.Entries)
                {
                    FileInfo fi = new FileInfo(Path.Combine(destinationPath, entry.FullName));
                    if (!fi.Directory.Exists) Directory.CreateDirectory(fi.DirectoryName);

                    if (File.Exists(fi.FullName))
                    {
                        File.Delete(fi.FullName);
                        //MessageBox.Show(string.Format("{0} already exist. File was deleted.", fi.FullName));
                    }

                    entry.ExtractToFile(fi.FullName, true);
                }
            }
            catch {

                throw new FileLoadException("File can't be unzipped.", sourceFile);
            }

            stream.Close();
        }




        public static FileSystemInterface InterfacePictures = new FileSystemInterface(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ".png", ".jpg");

        public static FileSystemInterface InterfaceLocation = new FileSystemInterface(null, new string[] { ".kml", ".gpx", ".wpt", ".jpg", ".jpeg" }, new string[] { ".kml" });

        public static FileSystemInterface InterfaceReport = new FileSystemInterface(".html");

        public static FileSystemInterface InterfaceSheets = new FileSystemInterface(null, ".csv", ".xml", ".txt", ".prn", ".Rda");
    }

    public class FileSystemInterface
    {
        string[] SaveExtensions;

        public string[] OpenExtensions;

        public string Extension { get { return SaveExtensions[0]; } }

        public OpenFileDialog OpenDialog { get; set; }

        public SaveFileDialog SaveDialog { get; set; }

        public SaveFileDialog ExportDialog { get; set; }
        
        //public string InitialDirectory { get; set; }


        public FileSystemInterface(string path, string[] openextensions, string[] saveextensions)
        {
            //InitialDirectory = path;
            OpenExtensions = openextensions;
            SaveExtensions = saveextensions;

            ResetDialogs();
        }

        public FileSystemInterface(string path, string extension)
            : this(path, new string[] { extension }, new string[] { extension })
        {  }

        public FileSystemInterface(string extension)
            : this(null, new string[] { extension }, new string[] { extension })
        {  }

        public FileSystemInterface(string path, string extension, params string[] addtsaveextension)
            : this(path, extension)
        {
            SaveExtensions = new string[addtsaveextension.Length + 1];
            SaveExtensions[0] = extension;
            addtsaveextension.CopyTo(SaveExtensions, 1);
            ResetDialogs();
        }


        private void ResetDialogs()
        {
            OpenDialog = new OpenFileDialog();
            OpenDialog.Filter = FileSystem.FilterFromExt(OpenExtensions);
            OpenDialog.Title = OpenExtensions.Length == 1 ?
                string.Format(Resources.Interface.FileOpen, FileSystem.GetFriendlyFiletypeName(Extension)) :
                Resources.Interface.FileOpenAny;
            OpenDialog.FilterIndex = OpenExtensions.Length == 1 ? 0 : 1;
            OpenDialog.FileOk += new CancelEventHandler(openDialog_FileOk);
            //OpenDialog.InitialDirectory = InitialDirectory;

            SaveDialog = new SaveFileDialog();
            SaveDialog.Filter = FileSystem.FilterFromExt(Extension);
            SaveDialog.Title = string.Format(Resources.Interface.FileSave, FileSystem.GetFriendlyFiletypeName(Extension));
            SaveDialog.FileOk += new CancelEventHandler(saveDialog_FileOk);
            //SaveDialog.InitialDirectory = InitialDirectory;

            if (SaveExtensions.Length > 1)
            {
                ExportDialog = new SaveFileDialog();
                ExportDialog.Filter = FileSystem.FilterFromExt(false, SaveExtensions);
                ExportDialog.Title = Resources.Interface.FileSaveAny;
                ExportDialog.FilterIndex = 0;
                ExportDialog.FileOk += new CancelEventHandler(saveDialog_FileOk);
                //SaveAsDialog.InitialDirectory = InitialDirectory;
            }
        }



        void openDialog_FileOk(object sender, CancelEventArgs e)
        {
            OpenDialog.InitialDirectory = FileSystem.FolderName(((OpenFileDialog)sender).FileName);
        }

        void saveDialog_FileOk(object sender, CancelEventArgs e)
        {
            SaveDialog.InitialDirectory = FileSystem.FolderName(((SaveFileDialog)sender).FileName);
            if (ExportDialog != null) ExportDialog.InitialDirectory = SaveDialog.InitialDirectory;
        }
    }
}