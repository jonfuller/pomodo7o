using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace Pomodo7o
{
    public class PomodoroApp : Application, IDisposable
    {
        private readonly AppController _appController;

        [ImportMany(typeof(IPomodoroPublisher))]
        public IEnumerable<IPomodoroPublisher> Publishers { get; set; }

        public PomodoroApp(
            CompositionContainer container,
            ViewModel viewModel,
            ITaskbarManager manager,
            Pomodo7oWindow window)
        {
            container.ComposeParts(this);

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