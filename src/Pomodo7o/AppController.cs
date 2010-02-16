using System;
using System.Collections.Generic;

namespace Pomodo7o
{
    public class AppController : IDisposable
    {
        private Pomodo7oWindow _window;
        private IEnumerable<IPomodoroPublisher> _publishers;

        private readonly TomatoTimer _workTimer;
        private readonly TomatoTimer _restTimer;
        private TomatoTimer _currentTimer;

        public AppController(Pomodo7oWindow window, IEnumerable<IPomodoroPublisher> publishers)
        {
            _window = window;
            _window.Play += Start;
            _window.Reset += Reset;
            _window.Pause += Pause;

            _publishers = publishers;

            _workTimer = new TomatoTimer(5.Seconds(), 25.Minutes());
            _restTimer = new TomatoTimer(5.Seconds(), 5.Minutes());

            _workTimer.TickPct += pct => Notify(x => x.WorkPercent(pct));
            _workTimer.TickRemaining += rmn => Notify(x => x.WorkTimeLeft(rmn));
            _workTimer.Complete += () =>
                                      {
                                          _currentTimer = _restTimer;
                                          Notify(x => x.WorkComplete());
                                          Start();
                                          Notify(x => x.RestStarted());
                                      };

            _restTimer.TickPct += pct => Notify(x => x.RestPercent(pct));
            _restTimer.TickRemaining += rmn => Notify(x => x.RestTimeLeft(rmn));
            _restTimer.Complete += () =>
                                      {
                                          _currentTimer = _workTimer;
                                          Notify(x => x.RestComplete());
                                          Start();
                                          Notify(x => x.WorkStarted());
                                      };

            _currentTimer = _workTimer;
            Start();
            Notify(x => x.WorkStarted());
        }

        public void Dispose()
        {
            _window = null;
            _publishers = null;
            _workTimer.Dispose();
            _restTimer.Dispose();
        }

        private void Start()
        {
            _currentTimer.Start();
            Dispatch(() => _window.RunningStatus(_currentTimer.IsRunning));
        }

        private void Reset()
        {
            _currentTimer.Reset();
            Dispatch(() => _window.RunningStatus(_currentTimer.IsRunning));
        }

        private void Pause()
        {
            _currentTimer.Pause();
            Dispatch(() => _window.RunningStatus(_currentTimer.IsRunning));
        }

        private void Notify(Action<IPomodoroPublisher> ofAction)
        {
            Dispatch(() => _publishers.ForEach(ofAction));
        }

        private void Dispatch(Action toDispatch)
        {
            _window.Dispatcher.Invoke(toDispatch);
        }
    }
}