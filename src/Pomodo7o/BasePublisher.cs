using System;

namespace Pomodo7o
{
    public class BasePublisher : IPomodoroPublisher
    {
        public virtual void Dispose()
        {
        }

        public virtual void WorkStarted()
        {
        }

        public virtual void WorkPercent(int percent)
        {
        }

        public virtual void WorkTimeLeft(TimeSpan remaining)
        {
        }

        public virtual void WorkComplete()
        {
        }

        public virtual void RestStarted()
        {
        }

        public virtual void RestPercent(int percent)
        {
        }

        public virtual void RestTimeLeft(TimeSpan remaining)
        {
        }

        public virtual void RestComplete()
        {
        }

        public virtual void Paused()
        {
        }

        public virtual void Resumed()
        {
        }
    }
}