using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfHelp
{
    public class CEFKeyBoardHandler : IKeyboardHandler
    {
        public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode,
            int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            const int VK_F1 = 0x70;
            const int VK_F5 = 0x74;
            const int VK_F11 = 0x7A;
            const int VK_F12 = 0x7B;
            var control = chromiumWebBrowser as Control;
            switch (windowsKeyCode)
            {
                case VK_F1:
                    MainForm._mainForm.BeginInvoke(new Action(() =>
                    {
                        MainForm._mainForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        MainForm._mainForm.WindowState = FormWindowState.Normal;
                    }));
                    break;
                case VK_F5:
                    if (modifiers == CefEventFlags.ControlDown)
                    {
                        browser.Reload(true); //强制忽略缓存
                    }
                    else
                    {
                        browser.Reload();
                    }
                    break;
                case VK_F11:
                    MainForm._mainForm.BeginInvoke(new Action(() =>
                    {
                        MainForm._mainForm.FormBorderStyle = FormBorderStyle.None;
                        MainForm._mainForm.WindowState = FormWindowState.Maximized;
                    }));
                    break;
                case VK_F12:
                    browser.ShowDevTools();
                    break;
            }

            return false;
        }

        public bool OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode,
            CefEventFlags modifiers, bool isSystemKey)
        {
            return false;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }

}
