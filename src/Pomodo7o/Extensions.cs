using System;
using System.Collections.Generic;

namespace Pomodo7o
{
    public static class Extensions
    {
        public static T Chain<T>(this T target, Action<T> action)
        {
            action(target);
            return target;
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
    }
}