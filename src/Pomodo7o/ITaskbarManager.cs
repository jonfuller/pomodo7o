using System;
using System.Drawing;
using System.Windows;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public interface ITaskbarManager
    {
        void SetOverlayIcon(Window window, Icon icon, string accessibilityText);
        void AddThumbButtons(IntPtr windowHandle, params ThumbnailToolbarButton[] buttons);
        void SetProgressValue(int current, int max, Window window);
        void SetProgressState(TaskbarProgressBarState state, Window window);
        void SetThumbnailClip(IntPtr handle, Rectangle rectangle);
    }
}