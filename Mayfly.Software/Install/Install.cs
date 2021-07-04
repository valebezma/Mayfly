using Microsoft.Win32;
using System;
using System.IO;

namespace Mayfly.Software
{
    public class Install
    {
        public static void AddShortcut(string fullLinkName, string targetFile, string toolTip)
        {
            ShellLink link = new ShellLink();
            link.Description = toolTip;
            link.ShortCutFile = fullLinkName;
            link.Target = targetFile;
            link.Save();
        }

        public static void RemoveShortcut(string folderpath, string targetFile)
        {
            foreach (FileInfo link in new DirectoryInfo(folderpath).GetFiles("*.lnk")) {
                if (FileSystem.ResolveShortcut(link.FullName) == targetFile) {
                    File.Delete(link.FullName);
                }
            }
        }

        public static void RegisterApp(string appPath)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\" + Path.GetFileName(appPath));
                regKey.SetValue(null, appPath);

                Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true).SetValue(Path.GetFileName(appPath), 10000);
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }

        public static void UnregisterApp(string appPath)
        {
            try
            {
                RegistryKey appKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\" + Path.GetFileName(appPath));

                if (appKey != null && appKey.GetValue(null).ToString() == appPath)
                {
                    Registry.CurrentUser.DeleteSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\" + Path.GetFileName(appPath));
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }

        public static void RegisterFileType(string extension, string progID, string friendlyName, string iconSource)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + extension);
                regKey.SetValue(null, progID);

                regKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + progID);
                regKey.SetValue(null, friendlyName);

                regKey = regKey.CreateSubKey("DefaultIcon");
                regKey.SetValue(null, iconSource);
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }

        public static void RegisterVerb(string progID, string verb, string action, string command, bool defaultverb)
        {
            try
            {
                RegistryKey classKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + progID, true);

                if (defaultverb) classKey.CreateSubKey("Shell").SetValue(null, verb);

                RegistryKey verbKey = classKey.CreateSubKey(@"Shell\" + verb);
                verbKey.SetValue(null, action);
                //verbKey.SetValue("MultiSelectModel", "Document");

                verbKey.CreateSubKey("Command").SetValue(null, command);
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }

        public static string GetAssociatedExecutablePath(string extension)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + extension);
            if (regKey == null) return null; // No such fyletype registered

            string progID = regKey.GetValue(null).ToString();
            regKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + progID + @"\Shell\open\Command");
            if (regKey == null) return null; // No executable is associated

            string command = regKey.GetValue(null).ToString();
            if (string.IsNullOrEmpty(command)) return null; // No executable is associated

            if (command.Contains(".exe")) return command.Substring(0, command.IndexOf(".exe") + 4);
            if (command.Contains(".dll")) return command.Substring(0, command.IndexOf(".dll") + 4);
            return string.Empty;            
        }

        public static void UnregisterFileType(string extension)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + extension);
            if (regKey == null) return;
            string progID = regKey.GetValue(null).ToString();
            Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\" + extension);
            regKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + progID);
            if (regKey == null) return;
            Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\" + progID);
        }

        public static void RegisterPropertyHandler(string clsid, string handlername, string handlerpath,
            string extension, string fulldetails, string previewdetails, string previewtitle, string tileinfo)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\PropertySystem\PropertyHandlers\" + extension);
                regKey.SetValue(null, clsid);

                RegistryKey clsidKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + clsid);
                clsidKey.SetValue(null, handlername);

                clsidKey = clsidKey.CreateSubKey("InProcServer32");
                clsidKey.SetValue(null, handlerpath);

                string progID = (string)Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + extension).GetValue(null);

                RegistryKey classKey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + progID, true);
                classKey.SetValue("FullDetails", "prop:" + fulldetails);
                classKey.SetValue("PreviewDetails", "prop:" + previewdetails);
                classKey.SetValue("PreviewTitle", "prop:" + previewtitle);
                classKey.SetValue("TileInfo", "prop:" + tileinfo);
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }

        public static void UnregisterPropertyHandler(string extension)
        {
            try
            {
                string clsid = (string)Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\PropertySystem\PropertyHandlers\" + extension).GetValue(null);
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\PropertySystem\PropertyHandlers\" + extension);
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\" + clsid);
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
        }
    }
}
