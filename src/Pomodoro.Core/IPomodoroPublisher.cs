using System;

namespace Pomodoro.Core
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

        void Paused();
        void Resumed();
    }
}