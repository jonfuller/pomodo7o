using System;
using System.Timers;

namespace Pomodoro.Core
{
    public class TomatoTimer : IDisposable
    {
        public event Action<int> TickPct = t => { };
        public event Action<TimeSpan> TickRemaining = t => { };
        public event Action Complete = () => { };

        private DateTime _startTime;
        private DateTime? _pauseTime;
        private readonly Timer _timer;

        public TomatoTimer(TimeSpan lengthOfTimer)
        {
            var lastPercent = 0;

            _timer = new Timer()
                .Chain(t => t.Interval = 1.Seconds().TotalMilliseconds)
                .Chain(t => t.Elapsed += (o, a) =>
                {
                    if(!IsRunning)
                        return;

                    var timeRemaining = (_startTime + lengthOfTimer) - DateTime.Now;
                    var currentPercent = GetPercentageComplete(
                                DateTime.Now - _startTime,
                                lengthOfTimer);

                    if(currentPercent != lastPercent)
                    {
                        lastPercent = currentPercent;
                        TickPct(currentPercent);
                    }

                    TickRemaining(timeRemaining);

                    if(timeRemaining.IsNegativeOrZero())
                    {
                        Complete();
                        lastPercent = -1;
                        Stop();
                    }
                });
        }

        public bool IsRunning
        {
            get;
            private set;
        }

        public void Start()
        {
            _timer.Start();

            _startTime = _pauseTime.HasValue
                             ? DateTime.Now - (_pauseTime.Value - _startTime)
                             : DateTime.Now;
            IsRunning = true;
        }

        public void Stop()
        {
            Pause();
            _pauseTime = null;
        }

        public void Pause()
        {
            IsRunning = false;
            _pauseTime = DateTime.Now;
        }

        public void Reset()
        {
            _startTime = DateTime.Now;
            IsRunning = true;
        }

        private int GetPercentageComplete(TimeSpan elapsed, TimeSpan total)
        {
            return Convert.ToInt32((elapsed.TotalMilliseconds * 100 / total.TotalMilliseconds));
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}