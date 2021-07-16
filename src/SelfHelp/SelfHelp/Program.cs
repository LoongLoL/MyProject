using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace SelfHelp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settings = new CefSettings
            {
                BrowserSubprocessPath = System.IO.Path.GetFullPath(@"./CefSharp.BrowserSubprocess.exe")
            };

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

        }
    }
}
