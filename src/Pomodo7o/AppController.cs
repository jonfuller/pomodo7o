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
            _window.GoToWork += GoToWork;
            _window.TakeABreak += TakeABreak;
            _window.Loaded += (s, e) => GoToWork();

            _publishers = publishers;

            _workTimer = new TomatoTimer(25.Minutes());
            _restTimer = new TomatoTimer(5.Minutes());

            _workTimer.TickPct += pct => Notify(x => x.WorkPercent(pct));
            _workTimer.TickRemaining += rmn => Notify(x => x.WorkTimeLeft(rmn));
            _workTimer.Complete += () =>
                                      {
                                          Notify(x => x.WorkComplete());
                                          TakeABreak();
                                      };

            _restTimer.TickPct += pct => Notify(x => x.RestPercent(pct));
            _restTimer.TickRemaining += rmn => Notify(x => x.RestTimeLeft(rmn));
            _restTimer.Complete += () =>
                                      {
                                          Notify(x => x.RestComplete());
                                          GoToWork();
                                      };
        }

        public void Dispose()
        {
            _window = null;
            _publishers = null;
            _workTimer.Dispose();
            _restTimer.Dispose();
        }

        private void GoToWork()
        {
            StartAndNotify(_workTimer, x => x.WorkStarted());
        }

        private void TakeABreak()
        {
            StartAndNotify(_restTimer, x => x.RestStarted());
        }

        private void StartAndNotify(TomatoTimer timerToStart, Action<IPomodoroPublisher> ofAction)
        {
            if(_currentTimer != null)
                _currentTimer.Stop();
            _currentTimer = timerToStart;
            Start();
            Notify(ofAction);
        }

        private void Start()
        {
            _currentTimer.Start();
            Notify(x => x.Resumed());
        }

        private void Reset()
        {
            _currentTimer.Reset();
            Notify(x => x.Resumed());
        }

        private void Pause()
        {
            _currentTimer.Pause();
            Notify(x => x.Paused());
        }

        private void Notify(Action<IPomodoroPublisher> ofAction)
        {
            Dispatch(() => _publishers.ForEach(ofAction));
        }

        private void Dispatch(Action toDispatch)
        {
            _window.Dispatch(toDispatch);
        }
    }
}