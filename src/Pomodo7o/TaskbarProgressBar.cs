using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public class TaskbarProgressBar : IProgressBar
    {
        private readonly ITaskbarManager _manager;
        private readonly Window _handle;

        public TaskbarProgressBar(ITaskbarManager manager, Window handle)
        {
            _manager = manager;
            _handle = handle;
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