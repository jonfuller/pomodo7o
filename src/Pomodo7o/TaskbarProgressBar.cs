using System;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Taskbar;
using Pomodoro.Core;

namespace Pomodo7o
{
    public class TaskbarProgressBar : IProgressBar
    {
        private readonly ITaskbarManager _manager;
        private readonly IntPtr _handle;

        public TaskbarProgressBar(ITaskbarManager manager, Pomodo7oWindow handle)
        {
            _manager = manager;
            _handle = new WindowInteropHelper(handle).Handle;
        }

        public void SetState(ProgressState state)
        {
            switch(state)
            {
                case ProgressState.Paused:
                    _manager.SetProgressState(TaskbarProgressBarState.Indeterminate, _handle);
                    break;
                case ProgressState.Red:
                    _manager.SetProgressState(TaskbarProgressBarState.Error, _handle);
                    break;
                case ProgressState.Yellow:
                    _manager.SetProgressState(TaskbarProgressBarState.Paused, _handle);
                    break;
                case ProgressState.Green:
                    _manager.SetProgressState(TaskbarProgressBarState.Normal, _handle);
                    break;
            }
        }

        public void SetPercent(int value)
        {
            _manager.SetProgressValue(value, 100, _handle);
        }
    }
}