using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Handler;
using CefSharp.WinForms;

namespace SelfHelp
{
    public partial class MainForm : Form
    {
        public static MainForm _mainForm;
        ChromiumWebBrowser browser;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _mainForm = this;
            //去除顶栏
            this.FormBorderStyle = FormBorderStyle.None;
            //设置全屏展示
            this.WindowState = FormWindowState.Maximized;

            var settings = new CefSettings
            {
                Locale = "zh-CN"
            };

            if (!Cef.IsInitialized)
                Cef.Initialize(settings);

            var webUrl = ConfigurationManager.AppSettings["webUrl"].Trim();
            browser = new ChromiumWebBrowser(webUrl)
            {
                Dock = DockStyle.Fill,
                KeyboardHandler = new CefKeyBoardHandler()
            };
            this.Controls.Add(browser);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 结束时要销毁
            browser.CloseDevTools();
            browser.GetBrowser().CloseBrowser(true);
            browser.Dispose();
            browser = null;
            Cef.Shutdown();
        }

        public bool IsAdministrator()
        {
            bool isAdmin;

            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return isAdmin;
        }
    }
}
