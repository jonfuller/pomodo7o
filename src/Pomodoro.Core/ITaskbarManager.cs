using System;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodoro.Core
{
    public interface ITaskbarManager
    {
        void SetOverlayIcon(IntPtr windowHandle, Icon icon, string accessibilityText);
        void AddThumbButtons(IntPtr windowHandle, params ThumbnailToolbarButton[] buttons);
        void SetProgressValue(int current, int max, IntPtr windowHandle);
        void SetProgressState(TaskbarProgressBarState state, IntPtr windowHandle);
        void SetThumbnailClip(IntPtr handle, Rectangle rectangle);
    }
}