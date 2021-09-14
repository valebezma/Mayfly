using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Mayfly.Extensions;

namespace Mayfly.Software
{
    public partial class Scheme
    {
        partial class VersionDataTable
        {
            public VersionRow FindByFileVersion(string file, string version)
            {
                foreach (VersionRow versionRow in this)
                {
                    if (versionRow.FileRow.File == file && versionRow.Version == version)
                    {
                        return versionRow;
                    }
                }

                return null;
            }
        }

        partial class FileDataTable
        {
            public FileRow FindByFile(string file)
            {
                foreach (FileRow fileRow in this)
                {
                    if (fileRow.File == file)
                    {
                        return fileRow;
                    }
                }

                return null;
            }
        }

        partial class FileRow
        {
            public string[] GetFilesList()
            {
                List<string> result = new List<string>();

                IO.AppendPath(result, this.File);

                foreach (SatelliteRow satRow in this.GetSatelliteRows())
                {
                    IO.AppendPath(result, satRow.Path);
                }

                return result.ToArray();
            }

            public string[] GetFilesList(CultureInfo cultureInfo)
            {
                List<string> result = new List<string>();

                IO.AppendPath(result, Path.Combine(cultureInfo.Name, Path.GetFileNameWithoutExtension(File) + ".resources.dll"));

                foreach (SatelliteRow satRow in this.GetSatelliteRows())
                {
                    if (!satRow.IsLocalizableNull() && satRow.Localizable)
                    {
                        if (Path.GetExtension(satRow.Path) == ".exe")
                        {
                            IO.AppendPath(result, Path.Combine(cultureInfo.Name,
                                Path.GetFileNameWithoutExtension(satRow.Path) + ".resources.dll"));
                        }
                        else
                        {
                            IO.AppendPath(result, Path.Combine(cultureInfo.Name,
                                satRow.Path));
                        }
                    }
                }

                return result.ToArray();
            }

            public string GetChanges(Version start)
            {
                string result = string.Empty;

                foreach (VersionRow versionRow in this.GetVersionRows())
                {
                    if (new Version(versionRow.Version) <= start) continue;

                    result += Environment.NewLine;

                    result += versionRow.Published == new DateTime(2015, 1, 1) ?
                        Resources.Update.UpdateBegin : versionRow.Published.ToString("D");

                    result += Environment.NewLine;

                    result += versionRow.GetChanges();

                    //result += Environment.NewLine;
                }

                return result;
            }

            public VersionRow GetLatestVersionRow()
            {
                VersionRow[] versionRows = this.GetVersionRows();

                if (versionRows.Length == 0) return null;

                VersionRow result = versionRows[0];

                foreach (VersionRow versionRow in this.GetVersionRows())
                {
                    Version currentVersion = new Version(versionRow.Version);

                    if (currentVersion > new Version(result.Version))
                    {
                        result = versionRow;
                    }
                }

                return result;
            }

            public Version GetLatestVersion()
            {
                VersionRow versionRow = this.GetLatestVersionRow();
                return new Version(versionRow == null ? "1.0.0.0" : versionRow.Version);
            }

            public string GetLatestChanges()
            {
                VersionRow versionRow = this.GetLatestVersionRow();
                return (versionRow == null || versionRow.IsChangesNull()) ? Resources.Update.ChangesNotDescribed : versionRow.Changes;
            }

            public UpdateFeatureArgs GetComponentArgs(string path)
            {
                UpdateFeatureArgs args = new UpdateFeatureArgs();

                args.DownloadUrl = Update.GetUri(Path.GetFileNameWithoutExtension(this.File) + ".zip");
                args.Filename = this.File;
                args.NewVersion = this.GetLatestVersion();

                FileInfo existingFile = new FileInfo(Path.Combine(path, this.File));

                if (existingFile == null)
                {
                    args.OldVersion = new Version(0, 0, 0, 0);
                    args.IsOutlive = this.GetLatestVersion().Major > 0;
                }
                else
                {
                    args.OldVersion = new Version(FileVersionInfo.GetVersionInfo(existingFile.FullName).FileVersion);
                    args.IsOutlive = this.GetLatestVersion().Major > FileVersionInfo.GetVersionInfo(existingFile.FullName).FileMajorPart;
                }

                args.Changes = this.GetChanges(args.OldVersion);

                return args;
            }
        }

        partial class VersionRow
        {
            public string GetChanges()
            {
                if (this.IsChangesNull()) { return Resources.Update.ChangesNotDescribed + Environment.NewLine; }
                else { return this.Changes.GetLocalizedValue() + Environment.NewLine; }
            }
        }

        partial class ProductDataTable
        {
            public ProductRow FindByName(string value)
            {
                foreach (ProductRow productRow in this)
                {
                    if (productRow.Name == value)
                    {
                        return productRow;
                    }
                }

                return null;
            }
        }

        partial class ProductRow
        {
            public bool Contains(string binary)
            {
                foreach (FileRow fileRow in this.GetFileRows())
                {
                    if (fileRow.File == binary)
                        return true;
                }

                return false;
            }

            public FileRow[] GetFileRows()
            {
                List<FileRow> result = new List<FileRow>();

                foreach (FeatureRow featureRow in this.GetFeatureRows())
                {
                    if (!result.Contains(featureRow.FileRow))
                        result.Add(featureRow.FileRow);
                }

                return result.ToArray();
            }

            public UpdateFeatureArgs[] GetComponentEventArgs(string path)
            {
                List<UpdateFeatureArgs> result = new List<UpdateFeatureArgs>();

                foreach (FileRow fileRow in this.GetFileRows())
                {
                    if (!System.IO.File.Exists(Path.Combine(path, fileRow.File))) continue;
                    result.Add(fileRow.GetComponentArgs(path));
                }

                return result.ToArray();
            }
        }
    }
}
