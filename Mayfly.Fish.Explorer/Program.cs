using System;
using System.Windows.Forms;

namespace Mayfly.Fish.Explorer
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

            Wild.SettingsExplorer.SetFeature("Fish", ".fcd");
            Log.WriteAppStarted();

            if (args.Length == 0)
            {
                Application.Run(new MainForm());
            }
            else
            {
                Application.Run(new MainForm(args));
            }

            Log.WriteAppEnded();
        }
    }
}
