using Mayfly.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Mayfly.Wild;

namespace Mayfly.Prospect
{
    public abstract class Service
    {
        public static void UpdateLocalData()
        {
            //processDisplay1.ShowLook();

            //    BackgroundWorker backLoader = new BackgroundWorker();
            //    //backLoader.WorkerReportsProgress = true;
            //    backLoader.DoWork += backLoader_DoWork;
            //    backLoader.RunWorkerCompleted += backLoader_RunWorkerCompleted;
            //    backLoader.RunWorkerAsync();
            //}

            //private static void backLoader_DoWork(object sender, DoWorkEventArgs e)
            //{
            if (!System.IO.Directory.Exists(UserSettings.CardsPath)) return;

            string[] filenames = FileSystem.MaskedNames(UserSettings.CardsPath, Plankton.UserSettings.Interface.Extension);
            int i = 0;

            //processDisplay1.ShowLook();
            //processDisplay1.StartProcessing(filenames.Length, "Loading plankton cards");

            Data plkData = new Data();

            foreach (string filename in filenames)
            {
                //if (UserSettings.PlanktonData.IsLoaded(filename)) continue;

                Data data = new Data();

                if (data.Read(filename))
                {
                    data.CopyTo(plkData)[0].Path = filename;
                }

                //processDisplay1.SetProgress(i);
                i++;
            }

            plkData.WriteToFile(UserSettingPaths.LocalPlanktonCopyPath);

            filenames = FileSystem.MaskedNames(UserSettings.CardsPath, Benthos.UserSettings.Interface.Extension);
            i = 0;

            //processDisplay1.StartProcessing(filenames.Length, "Loading benthos cards");

            Data benData = new Data();

            foreach (string filename in filenames)
            {
                //if (UserSettings.BenthosData.IsLoaded(filename)) continue;

                Data data = new Data();

                if (data.Read(filename))
                {
                    data.CopyTo(benData)[0].Path = filename;
                }

                //processDisplay1.SetProgress(i);
                i++;
            }

            benData.WriteToFile(UserSettingPaths.LocalBenthosCopyPath);

            filenames = FileSystem.MaskedNames(UserSettings.CardsPath, Fish.UserSettings.Interface.Extension);
            i = 0;

            //processDisplay1.StartProcessing(filenames.Length, "Loading fish cards");

            Data fishData = new Data();

            foreach (string filename in filenames)
            {
                //if (UserSettings.FishData.IsLoaded(filename)) continue;

                Data data = new Data();

                if (data.Read(filename))
                {
                    data.CopyTo(fishData);
                }

                //processDisplay1.SetProgress(i);
                i++;
            }

            fishData.WriteToFile(UserSettingPaths.LocalFishCopyPath);
            //}

            //private static void backLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            //{
            //LoadData();
            //processDisplay1.HideLook();
            //this.WindowState = FormWindowState.Normal;
        }
    }
}
