using System;
using System.Windows.Forms;
using Mayfly;
using Mayfly.Software;

namespace Mayfly.Species
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

            if (args.Length > 0)
            {
                if (args.Length == 1) // If filename is given only
                {
                    //Application.Run(new Display(args[0])); // Run key in user mode
                }
                else // If filename is given with action
                {
                    switch (args[1])
                    {
                        case "-edit":
                            Application.Run(new MainForm(args[0]));
                            break;
                    }
                }
            }
            else
            {
                Application.Run(new MainForm());
            }

            Log.WriteAppEnded();
        }
    }
}
