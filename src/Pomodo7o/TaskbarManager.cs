using System;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Taskbar;
using Pomodoro.Core;

namespace Pomodo7o
{
    public class TaskbarManager : ITaskbarManager
    {
        private readonly Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager _manager;

        public TaskbarManager(Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager manager)
        {
            _manager = manager;
        }

        public void SetOverlayIcon(IntPtr windowHandle, Icon icon, string accessibilityText)
        {
            _manager.SetOverlayIcon(windowHandle, icon, accessibilityText);
        }

        public void AddThumbButtons(IntPtr windowHandle, params ThumbnailToolbarButton[] buttons)
        {
            _manager.ThumbnailToolbars.AddButtons(windowHandle, buttons);
        }

        public void SetProgressValue(int current, int max, IntPtr windowHandle)
        {
            _manager.SetProgressValue(current, max, windowHandle);
        }

        public void SetProgressState(TaskbarProgressBarState state, IntPtr windowHandle)
        {
            _manager.SetProgressState(state, windowHandle);
        }

        public void SetThumbnailClip(IntPtr handle, Rectangle rectangle)
        {
            _manager.TabbedThumbnail.SetThumbnailClip(handle, rectangle);
        }
    }
}