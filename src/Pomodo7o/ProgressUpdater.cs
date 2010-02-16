using System;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    [Export(typeof(IPomodoroPublisher))]
    public class ProgressUpdater : IPomodoroPublisher
    {
        private readonly TaskbarManager _taskbarManager;
        private readonly Window _window;

        public ProgressUpdater(TaskbarManager taskbarManager, Window window)
        {
            _taskbarManager = taskbarManager;
            _window = window;
        }

        public void WorkStarted()
        {
            _taskbarManager.SetOverlayIcon(
                _window,
                null,
                Properties.Resources.Mode_Work);
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
            _taskbarManager.SetOverlayIcon(
                _window,
                Properties.Resources.icon_rest,
                Properties.Resources.Mode_Rest);
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
            _taskbarManager.SetProgressValue(percent, 100, _window);
        }

        private void UpdateProgressState(TaskbarProgressBarState state)
        {
            _taskbarManager.SetProgressState(state);
        }

        private TaskbarProgressBarState GetProgressStateWork(TimeSpan remaining)
        {
            if(remaining < 1.Minutes())
                return TaskbarProgressBarState.Error;

            if(remaining < 5.Minutes())
                return TaskbarProgressBarState.Paused;

            return TaskbarProgressBarState.Normal;
        }

        private TaskbarProgressBarState GetProgressStateRest(TimeSpan remaining)
        {
            if(remaining < 30.Seconds())
                return TaskbarProgressBarState.Error;

            if(remaining < 1.Minutes())
                return TaskbarProgressBarState.Paused;

            return TaskbarProgressBarState.Normal;
        }
    }
}