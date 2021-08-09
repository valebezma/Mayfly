using System;
using System.Windows.Forms;
using Mayfly.Fish.Explorer;

namespace Mayfly.Fish.Legal
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

            if (args.Length == 0) { Application.Run(new FisheryLicense()); }
            else { Application.Run(new FisheryLicense(args[0])); }

            Log.WriteAppEnded();
        }
    }
}