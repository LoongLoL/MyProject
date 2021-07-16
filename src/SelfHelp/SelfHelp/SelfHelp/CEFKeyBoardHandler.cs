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
    public class CefKeyBoardHandler : IKeyboardHandler
    {
        public bool IsMaxWindow = true;
        public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode,
            int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            const int vkF5 = 0x74;
            const int vkF11 = 0x7A;
            const int vkF12 = 0x7B;

            switch (windowsKeyCode)
            {
                case vkF5:
                    if (modifiers == CefEventFlags.ControlDown)
                    {
                        browser.Reload(true); //强制忽略缓存
                    }
                    else
                    {
                        browser.Reload();
                    }
                    return true;
                case vkF11:
                    MainForm._mainForm.BeginInvoke(new Action(() =>
                    {
                        if (IsMaxWindow)
                        {
                            MainForm._mainForm.FormBorderStyle = FormBorderStyle.FixedSingle;
                            MainForm._mainForm.WindowState = FormWindowState.Normal;
                            IsMaxWindow = !IsMaxWindow;
                        }
                        else
                        {
                            MainForm._mainForm.FormBorderStyle = FormBorderStyle.None;
                            MainForm._mainForm.WindowState = FormWindowState.Maximized;
                            IsMaxWindow = !IsMaxWindow;
                        }
                    }));
                    return true;
                case vkF12:
                    browser.ShowDevTools();
                    return true;
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
