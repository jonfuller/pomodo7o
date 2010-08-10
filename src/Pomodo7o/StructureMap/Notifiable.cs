using System;
using System.ComponentModel;
using Castle.DynamicProxy;

namespace Pomodo7o.StructureMap
{
    public class Notifiable
    {
        public static object MakeForInterface(Type type, object obj, ProxyGenerator generator)
        {
            var maker = typeof(Notifiable).GetMethod("MakeForInterfaceGeneric");
            var typed = maker.MakeGenericMethod(type);
            return typed.Invoke(null, new[] { obj, generator });
        }

        public static T MakeForInterfaceGeneric<T>(T obj, ProxyGenerator generator) where T : class
        {
            if(!typeof(T).IsInterface)
                throw new InvalidOperationException(string.Format("{0} is not an interface", typeof(T).Name));

            return (T)generator.CreateInterfaceProxyWithTarget(
                typeof(T),
                new[] { typeof(INotifyPropertyChanged) },
                obj,
                new PropertyChangedDecorator());
        }

        public static object MakeForClass(Type type, object[] ctorArgs, ProxyGenerator generator)
        {
            var maker = typeof(Notifiable).GetMethod("MakeForClassGeneric");
            var typed = maker.MakeGenericMethod(type);
            return typed.Invoke(null, new object[] { ctorArgs, generator });
        }

        public static T MakeForClassGeneric<T>(object[] ctorArgs, ProxyGenerator generator) where T : class
        {
            return (T)generator.CreateClassProxy(
                typeof(T),
                new[] { typeof(INotifyPropertyChanged) },
                ProxyGenerationOptions.Default,
                ctorArgs,
                new PropertyChangedDecorator());
        }
    }
}