using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mayfly.Software
{
    public class UpdateArgs : EventArgs
    {
        public string Product { get; private set; }

        public string ProductFolder { get { return Path.Combine(IO.ProgramFolder, Product); } }

        public bool IsUpdateAvailable
        {
            get
            {
                if (Components == null) return false;

                foreach (UpdateFeatureArgs componentArgs in Components)
                {
                    if (componentArgs.IsUpdateAvailable)
                        return true;
                }

                return false;
            }
        }

        public bool IsOutlive
        {
            get
            {
                if (Components == null) return false;

                foreach (UpdateFeatureArgs componentArgs in Components)
                {
                    if (componentArgs.IsOutlive)
                        return true;
                }

                return false;
            }
        }

        public UpdateFeatureArgs[] Components { get; set; }

        //public string CompleteFilesList { get; internal set; }



        public UpdateArgs()
        {

        }

        public UpdateArgs(Scheme data, string product)
            :this()
        {
            Product = product;
            Scheme.ProductRow productRow = data.Product.FindByName(product);
            if (productRow == null) return;
            Components = productRow.GetComponentEventArgs(ProductFolder);

            //Components[0].
        }



        public string GetReleaseNotes()
        {
            string result = string.Empty;

            foreach (UpdateFeatureArgs componentUpdate in Components)
            {
                if (componentUpdate.IsUpdateAvailable)
                {
                    result += string.Format(Resources.Update.ReleaseNoteStamp,
                        FileVersionInfo.GetVersionInfo(Path.Combine(ProductFolder, componentUpdate.Filename)).FileDescription,
                        componentUpdate.OldVersion, componentUpdate.NewVersion);
                    result += Environment.NewLine;
                    result += componentUpdate.Changes;
                    result += Environment.NewLine;
                    result += "--------------" + Environment.NewLine;;
                    result += Environment.NewLine;
                }
            }

            result += Resources.Update.ReleaseNoteEnd;

            return result.TrimEnd(Environment.NewLine.ToCharArray());
        }

        public Uri[] GetDownloadLinks()
        {
            List<Uri> result = new List<Uri>();

            foreach (UpdateFeatureArgs component in this.Components)
            {
                result.Add(component.DownloadUrl);
            }

            return result.ToArray();
        }

        public bool Contains(string filename)
        {
            foreach (UpdateFeatureArgs args in Components)
            {
                if (args.Filename == filename) return true;
            }

            return false;
        }
    }

    public class UpdateFeatureArgs : EventArgs
    {
        public string Filename { get; set; }

        public Uri DownloadUrl { get; set; }

        public bool IsUpdateAvailable
        {
            get
            {
                if (OldVersion == null)
                    return true;
                else
                {
                    return NewVersion > OldVersion;
                }
            }
        }

        public bool IsOutlive { get; set; }

        public Version OldVersion { get; set; }

        public Version NewVersion { get; set; }

        public string Changes { get; set; }

        //public ReleaseNote[] Notes { get { return notes.ToArray(); } }
        //private List<ReleaseNote> notes = new List<ReleaseNote>();

        public override string ToString()
        {
            return string.Format("{0} {1} (Latest: {2})", Filename, OldVersion, NewVersion);
        }

        //public void AddReleaseNote(CultureInfo ci, string changes)
        //{
        //    notes.Add(new ReleaseNote(ci, changes));
        //}

        //public string GetChanges()
        //{
        //    foreach (ReleaseNote note in Notes)
        //    {
        //        if (note.Language == CultureInfo.CurrentCulture) return note.Changes;
        //    }

        //    return string.Empty;
        //}
    }

    public class ReleaseNote
    {
        public CultureInfo Language { get; set; }

        public string Changes { get; set; }

        public ReleaseNote(CultureInfo ci, string changes)
        {
            Language = ci;
            Changes = changes;
        }
    }
}
