using System;

namespace Pomodo7o
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using(var app = new PomodoroApp())
            {
                app.Run(app.MainWindow);
            }
        }
    }
}