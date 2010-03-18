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

                cfg.For<IPomodoroPublisher>().Singleton().AddInstances(x =>
                {
                    x.ConstructedBy(ctx => ctx.GetInstance<Pomodo7oWindow>());
                    x.Type<ProgressUpdater>()
                        .Ctor<IProgressBar>().Is(ctx => ctx.GetInstance<TaskbarProgressBar>());
                    x.Type<ProgressUpdater>()
                        .Ctor<IProgressBar>().Is(ctx => ctx.GetInstance<ViewModelProgressBar>());
                });
            });

            var app = new Application { MainWindow = ObjectFactory.GetInstance<Pomodo7oWindow>() };
            using (ObjectFactory.GetInstance<AppController>())
            {
                app.Run(app.MainWindow);
            }
        }
    }
}