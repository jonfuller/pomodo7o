using System;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var win = TaskbarManager.IsPlatformSupported
                             ? (Window)new Win(TaskbarManager.Instance)
                             : new Bogus();
            var app = new Application();
            app.Run(win);
        }
    }
}