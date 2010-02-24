using System;
using System.Drawing;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public class FakeTaskbarManager : ITaskbarManager
    {
        public void SetOverlayIcon(Window window, Icon icon, string accessibilityText)
        {
        }

        public void AddThumbButtons(IntPtr windowHandle, params ThumbnailToolbarButton[] buttons)
        {
        }

        public void SetProgressValue(int current, int max, Window window)
        {
        }

        public void SetProgressState(TaskbarProgressBarState state)
        {
        }
    }
}