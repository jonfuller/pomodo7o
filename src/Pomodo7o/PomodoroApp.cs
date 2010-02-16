using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public class PomodoroApp : Application, IDisposable
    {
        private readonly AppController _appController;

        [ImportMany(typeof(IPomodoroPublisher))]
        public IEnumerable<IPomodoroPublisher> Publishers { get; set; }

        public PomodoroApp()
        {
            new CompositionContainer(
                new AggregateCatalog(
                    new DirectoryCatalog(@".\"))).ComposeParts(this);

            if(TaskbarManager.IsPlatformSupported)
            {
                var window = new Pomodo7oWindow(TaskbarManager.Instance);
                MainWindow = window;
                _appController = new AppController(
                    (Pomodo7oWindow)MainWindow,
                    Publishers.Append(new ProgressUpdater(TaskbarManager.Instance, window)));
            }
            else
            {
                MainWindow = new Bogus();
            }
        }

        public void Dispose()
        {
            Publishers.ForEach(x => x.Dispose());
            _appController.Dispose();
        }
    }
}