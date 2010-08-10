using System;
using System.IO;
using System.Windows;
using Castle.DynamicProxy;
using Pomodo7o.StructureMap;
using StructureMap;

using MSTaskbarManager = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager;

namespace Pomodo7o
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = new Container();
            container.Configure(cfg =>
            {
                cfg.For<ITaskbarManager>()
                    .ConditionallyUse(x =>
                    {
                        x.If(c => MSTaskbarManager.IsPlatformSupported)
                         .ThenIt.Is.ConstructedBy(() =>
                             new TaskbarManager(MSTaskbarManager.Instance));
                        x.TheDefault.IsThis(new FakeTaskbarManager());
                    });

                cfg.For<ViewModel>().Singleton().Use(() => Notifiable.MakeForClassGeneric<ViewModel>());
                cfg.For<Pomodo7oWindow>().Singleton().Use<Pomodo7oWindow>();

                cfg.Scan(scanner =>
                {
                    var extensionDir = "Extensions";
                    if (Directory.Exists(extensionDir))
                    {
                        scanner.AssembliesFromPath(extensionDir);
                        scanner.AddAllTypesOf<IPomodoroPublisher>();
                    }
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

            var app = new Application { MainWindow = container.GetInstance<Pomodo7oWindow>() };
            using (container.GetInstance<AppController>())
            {
                app.Run(app.MainWindow);
            }
        }
    }
}
