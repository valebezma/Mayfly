using System;
using System.Windows.Forms;

namespace Mayfly.Library
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
                Application.Run(new Catalogue());
            }
            else
            {
                Application.Run(new Catalogue(args[0]));
            }

            Log.WriteAppEnded();
        }
    }
}
