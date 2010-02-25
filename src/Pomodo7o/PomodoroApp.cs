using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

using MSTaskbarManager = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager;

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

            var manager = MSTaskbarManager.IsPlatformSupported
                             ? (ITaskbarManager)new TaskbarManager(MSTaskbarManager.Instance)
                             : new FakeTaskbarManager();
            var viewModel = new ViewModel();

            var window = new Pomodo7oWindow(manager, viewModel);

            _appController = new AppController(
                window,
                Publishers
                    .Append(new ProgressUpdater(new TaskbarProgressBar(manager, window)))
                    .Append(new ProgressUpdater(new ViewModelProgressBar(viewModel)))
                    .Append(window));

            MainWindow = window;
        }

        public void Dispose()
        {
            Publishers.ForEach(x => x.Dispose());
            _appController.Dispose();
        }
    }
}