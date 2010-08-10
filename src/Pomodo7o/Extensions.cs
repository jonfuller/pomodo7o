using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using StructureMap;

namespace Pomodo7o
{
    public static class Extensions
    {
        public static Visibility ToVisibility(this bool visible)
        {
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public static string ToFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static T Chain<T>(this T target, Action<T> action)
        {
            action(target);
            return target;
        }

        public static BitmapFrame GetBitmapFrame(this Icon icon)
        {
            return BitmapFrame.Create(icon.GetStream());
        }

        public static Stream GetStream(this Icon icon)
        {
            var stream = new MemoryStream();
            icon.Save(stream);
            return stream;
        }

        public static TimeSpan Seconds(this int seconds)
        {
            return new TimeSpan(0, 0, seconds);
        }

        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, minutes, 0);
        }

        public static bool IsNegativeOrZero(this TimeSpan span)
        {
            return span.TotalMilliseconds <= 0;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> target, Action<T> toPerform)
        {
            foreach(var item in target)
                toPerform(item);
            return target;
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> target, T toAppend)
        {
            foreach(var item in target)
                yield return item;
            yield return toAppend;
        }

        public static object GetInstance(this IContext session, Type instanceType)
        {
            var openMethod = typeof(IContext).GetMethod("GetInstance", new Type[0]);
            var closedMethod = openMethod.MakeGenericMethod(instanceType);

            return closedMethod.Invoke(session, new object[0]);
        }

        public static ConstructorInfo GetGreediestCtor(this Type target)
        {
            return target.GetConstructors().WithMax(ctor => ctor.GetParameters().Length);
        }

        public static bool HasAttribute<TAttr>(this Type type)
        {
            return type.GetCustomAttributes(typeof(TAttr), true).Length > 0;
        }

        public static T WithMax<T>(this IEnumerable<T> target, Func<T, int> selector)
        {
            int max = -1;
            T currentMax = default(T);

            foreach(var item in target)
            {
                var current = selector(item);
                if(current <= max)
                    continue;

                max = current;
                currentMax = item;
            }

            return currentMax;
        }
    }
}