using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Pomodoro.Core;
using Color = System.Windows.Media.Color;
using SDColor = System.Drawing.Color;

namespace Pomodo7o
{
    public static class Extensions
    {
        public static Visibility ToVisibility(this bool visible)
        {
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public static BitmapFrame GetBitmapFrame(this Icon icon)
        {
            return BitmapFrame.Create(icon.GetStream());
        }

        public static SolidColorBrush GetBrush(this SDColor color)
        {
            return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
        }

        public static SDColor GetSDColor(this SolidColorBrush brush)
        {
            return SDColor.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
        }
    }
}