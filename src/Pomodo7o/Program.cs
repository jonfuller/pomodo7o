using System;
using System.Windows;
using StructureMap;

using MSTaskbarManager = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager;

namespace Pomodo7o
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ObjectFactory.Configure(cfg =>
            {
                cfg.For<ITaskbarManager>()
                    .ConditionallyUse(x =>
                      {
                          x.If(c => MSTaskbarManager.IsPlatformSupported)
                           .ThenIt.Is.ConstructedBy(() =>
                               new TaskbarManager(MSTaskbarManager.Instance));
                          x.TheDefault.IsThis(new FakeTaskbarManager());
                      });

                cfg.For<ViewModel>().Singleton().Use<ViewModel>();
                cfg.For<Pomodo7oWindow>().Singleton().Use<Pomodo7oWindow>();

                cfg.Scan(scanner =>
                 {
                     scanner.AssembliesFromPath("Extensions");
                     scanner.AddAllTypesOf<IPomodoroPublisher>();
                 });

                cfg.For<IPomodoroPublisher>()
                    .Use(x => x.GetInstance<Pomodo7oWindow>());
                cfg.For<IPomodoroPublisher>()
                    .Use<ProgressUpdater>()
                    .Ctor<IProgressBar>().Is(x => x.GetInstance<TaskbarProgressBar>());
                cfg.For<IPomodoroPublisher>()
                    .Use<ProgressUpdater>()
                    .Ctor<IProgressBar>().Is(x => x.GetInstance<ViewModelProgressBar>());
            });

            var app = new Application {MainWindow = ObjectFactory.GetInstance<Pomodo7oWindow>()};
            using(ObjectFactory.GetInstance<AppController>())
            {
                app.Run(app.MainWindow);
            }
        }
    }
}