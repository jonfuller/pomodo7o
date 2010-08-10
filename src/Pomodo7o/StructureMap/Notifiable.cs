using System;
using System.ComponentModel;
using Castle.DynamicProxy;

namespace Pomodo7o.StructureMap
{
    public class Notifiable
    {
        private static ProxyGenerator generator = new ProxyGenerator();

        public static object MakeForInterface(Type type, object obj)
        {
            var maker = typeof(Notifiable).GetMethod("MakeForInterfaceGeneric");
            var typed = maker.MakeGenericMethod(type);
            return typed.Invoke(null, new[] { obj });
        }

        public static T MakeForInterfaceGeneric<T>(T obj) where T : class
        {
            if(!typeof(T).IsInterface)
                throw new InvalidOperationException(string.Format("{0} is not an interface", typeof(T).Name));

            return (T)generator.CreateInterfaceProxyWithTarget(
                typeof(T),
                new[] { typeof(INotifyPropertyChanged) },
                obj,
                new PropertyChangedDecorator());
        }

        public static object MakeForClass(Type type, params object[] ctorArgs)
        {
            var maker = typeof(Notifiable).GetMethod("MakeForClassGeneric");
            var typed = maker.MakeGenericMethod(type);
            return typed.Invoke(null, new object[] { ctorArgs });
        }

        public static T MakeForClassGeneric<T>(params object[] ctorArgs) where T : class
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