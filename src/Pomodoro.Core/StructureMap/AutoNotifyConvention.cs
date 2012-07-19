using System;
using System.Linq;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Pomodoro.Core.StructureMap
{
    public class AutoNotifyConvention : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if(type.IsEnum || !type.HasAttribute<AutoNotifyAttribute>())
                return;

            if(type.IsInterface)
                ConfigureInterface(type, registry);
            else if(!type.IsAbstract)
                ConfigureClass(type, registry);
        }

        private void ConfigureInterface(Type type, Registry registry)
        {
            registry
                .For(type)
                .EnrichWith((context, obj) => Notifiable.MakeForInterface(type, obj));
        }

        private void ConfigureClass(Type type, Registry registry)
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
        }
    }
}