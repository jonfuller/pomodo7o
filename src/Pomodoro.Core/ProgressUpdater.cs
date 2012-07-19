using System;

namespace Pomodoro.Core
{
    public class ProgressUpdater : IPomodoroPublisher
    {
        private readonly IProgressBar _progressBar;

        public ProgressUpdater(IProgressBar progressBar)
        {
            _progressBar = progressBar;
        }

        public void Paused()
        {
            _progressBar.SetState((ProgressState.Paused));
        }

        public void Resumed()
        {
            _progressBar.SetState(ProgressState.Green);
            _progressBar.SetPercent(0);
        }

        public void WorkStarted()
        {
        }

        public void WorkPercent(int percent)
        {
            UpdateProgress(percent);
        }

        public void WorkTimeLeft(TimeSpan remaining)
        {
            UpdateProgressState(GetProgressStateWork(remaining));
        }

        public void WorkComplete()
        {
            UpdateProgress(0);
        }

        public void RestStarted()
        {
        }

        public void RestPercent(int percent)
        {
            UpdateProgress(percent);
        }

        public void RestTimeLeft(TimeSpan remaining)
        {
            UpdateProgressState(GetProgressStateRest(remaining));
        }

        public void RestComplete()
        {
            UpdateProgress(0);
        }

        public void Dispose()
        {
        }

        private void UpdateProgress(int percent)
        {
            _progressBar.SetPercent(percent);
        }

        private void UpdateProgressState(ProgressState state)
        {
            _progressBar.SetState(state);
        }

        private ProgressState GetProgressStateWork(TimeSpan remaining)
        {
            if(remaining < 1.Minutes())
                return ProgressState.Red;

            if(remaining < 5.Minutes())
                return ProgressState.Yellow;

            return ProgressState.Green;
        }

        private ProgressState GetProgressStateRest(TimeSpan remaining)
        {
            if(remaining < 30.Seconds())
                return ProgressState.Red;

            if(remaining < 1.Minutes())
                return ProgressState.Yellow;

            return ProgressState.Green;
        }
    }
}