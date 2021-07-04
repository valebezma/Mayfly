using System;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using Mayfly.Extensions;

namespace Mayfly.Fish
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Mayfly.Service.Application_ThreadException;

            Log.WriteAppStarted();

            if (args.Length == 0)
            {
                Application.Run(new Card());
            }
            else
            {
                //MessageBox.Show(args.Merge());

                Card card = new Card();

                if (args.Length > 0)
                {
                    card.LoadData(args[0]);
                }

                if (args.Length > 1)
                {
                    switch (args[1])
                    {
                        //case "diet":
                        //    card.OpenTrophics(args[2], args[3]);
                        //    break;
                        default:
                            card.OpenSpecies(args[1]);
                            break;
                    }
                }

                Application.Run(card);
            }

            Log.WriteAppEnded();
        }
    }
}