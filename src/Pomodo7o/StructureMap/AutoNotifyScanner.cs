using System;
using System.Linq;
using StructureMap.Graph;

namespace Pomodo7o.StructureMap
{
    public class AutoNotifyScanner : ITypeScanner
    {
        public void Process(Type type, PluginGraph graph)
        {
            if(type.IsEnum || !type.HasAttribute<AutoNotifyAttribute>())
                return;

            if(type.IsInterface)
                ConfigureInterface(type, graph);
            else if(!type.IsAbstract)
                ConfigureClass(type, graph);
        }

        private void ConfigureInterface(Type type, PluginGraph graph)
        {
            graph.Configure(registry =>
            {
                registry
                    .For(type)
                    .EnrichWith((context, obj) => Notifiable.MakeForInterface(type, obj));
            });
        }

        private void ConfigureClass(Type type, PluginGraph graph)
        {
            graph.Configure(registry =>
            {
                var inst = new LooseConstructorInstance(context =>
                {
                    var ctorArgs = type
                        .GetGreediestCtor()
                        .GetParameters()
                        .Select(p => context.GetInstance(p.ParameterType));

                    return Notifiable.MakeForClass(type, ctorArgs.ToArray());
                });

                registry.For(type).Use(inst);
            });
        }
    }
}