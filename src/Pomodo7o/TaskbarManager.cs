using System;
using System.Drawing;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public class TaskbarManager : ITaskbarManager
    {
        private readonly Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager _manager;

        public TaskbarManager(Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager manager)
        {
            _manager = manager;
        }

        public void SetOverlayIcon(Window window, Icon icon, string accessibilityText)
        {
            _manager.SetOverlayIcon(window, icon, accessibilityText);
        }

        public void AddThumbButtons(IntPtr windowHandle, params ThumbnailToolbarButton[] buttons)
        {
            _manager.ThumbnailToolbars.AddButtons(windowHandle, buttons);
        }

        public void SetProgressValue(int current, int max, Window window)
        {
            _manager.SetProgressValue(current, max, window);
        }

        public void SetProgressState(TaskbarProgressBarState state, Window window)
        {
            _manager.SetProgressState(state, window);
        }

        public void SetThumbnailClip(IntPtr handle, Rectangle rectangle)
        {
            _manager.TabbedThumbnail.SetThumbnailClip(handle, rectangle);
        }
    }
}