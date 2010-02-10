using System;

namespace Pomodo7o
{
    public static class Extensions
    {
        public static T Chain<T>( this T target, Action<T> action )
        {
            action( target );
            return target;
        }

        public static TimeSpan Seconds( this int seconds )
        {
            return new TimeSpan( 0, 0, seconds );
        }

        public static TimeSpan Minutes( this int minutes )
        {
            return new TimeSpan( 0, minutes, 0 );
        }

        public static bool IsNegativeOrZero( this TimeSpan span )
        {
            return span.TotalMilliseconds <= 0;
        }
    }
}