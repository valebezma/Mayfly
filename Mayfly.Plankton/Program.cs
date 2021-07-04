using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Plankton
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
                Card card = new Card();

                if (args.Length > 0)
                {
                    card.LoadData(args[0]);
                }

                if (args.Length > 1)
                {
                    switch (args[1])
                    {
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
