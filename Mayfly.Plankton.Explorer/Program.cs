using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayfly.Plankton.Explorer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Mayfly.Service.Application_ThreadException;

            Wild.ExplorerSettings.SetFeature("Plankton", ".pcd");
            Log.WriteAppStarted();
            Application.Run(new MainForm());

            Log.WriteAppEnded();
        }
    }
}
