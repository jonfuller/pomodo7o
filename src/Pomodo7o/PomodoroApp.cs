using System;
using System.Collections.Generic;
using System.Windows;

namespace Pomodo7o
{
    public class PomodoroApp : Application, IDisposable
    {
        private readonly AppController _appController;

        public PomodoroApp(
            Pomodo7oWindow window,
            IEnumerable<IPomodoroPublisher> publishers)
        {
            _appController = new AppController(window, publishers);

            MainWindow = window;
        }

        public void Dispose()
        {
            _appController.Dispose();
        }
    }
}