using System;

namespace Pomodo7o
{
    public interface IPomodoroPublisher : IDisposable
    {
        void WorkStarted();
        void WorkPercent(int percent);
        void WorkTimeLeft(TimeSpan remaining);
        void WorkComplete();

        void RestStarted();
        void RestPercent(int percent);
        void RestTimeLeft(TimeSpan remaining);
        void RestComplete();
    }
}