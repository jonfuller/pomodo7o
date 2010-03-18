using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

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
                cfg.SelectConstructor(() => new CompositionContainer());
                cfg.SelectConstructor(() => new DirectoryCatalog(""));

                cfg.For<ComposablePartCatalog>()
                    .Use<DirectoryCatalog>().Ctor<string>().Is(@".\");

                cfg.ForConcreteType<CompositionContainer>();

                cfg.For<ITaskbarManager>()
                    .ConditionallyUse(x =>
                      {
                          x.If(c => MSTaskbarManager.IsPlatformSupported)
                           .ThenIt.Is.ConstructedBy(() =>
                               new TaskbarManager(MSTaskbarManager.Instance));
                          x.TheDefault.IsThis(new FakeTaskbarManager());
                      });

                cfg.ForConcreteType<ViewModel>();
                cfg.ForConcreteType<Pomodo7oWindow>();
            });

            using(var app = ObjectFactory.GetInstance<PomodoroApp>())
            {
                app.Run(app.MainWindow);
            }
        }
    }
}