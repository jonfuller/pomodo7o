using System;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodoro.Core
{
    public class FakeTaskbarManager : ITaskbarManager
    {
        public void SetOverlayIcon(IntPtr windowHandle, Icon icon, string accessibilityText)
        {
        }

        public void AddThumbButtons(IntPtr windowHandle, params ThumbnailToolbarButton[] buttons)
        {
        }

        public void SetProgressValue(int current, int max, IntPtr windowHandle)
        {
        }

        public void SetProgressState(TaskbarProgressBarState state, IntPtr windowHandle)
        {
        }

        public void SetThumbnailClip(IntPtr handle, Rectangle rectangle)
        {
        }
    }
}