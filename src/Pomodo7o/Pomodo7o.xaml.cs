using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    using Res = Properties.Resources;

    public partial class Pomodo7oWindow : IPomodoroPublisher
    {
        private readonly TaskbarManager _taskbarManager;

        private readonly ThumbnailToolbarButton _btnReset;
        private readonly ThumbnailToolbarButton _btnPlay;
        private readonly ThumbnailToolbarButton _btnPause;
        private readonly ThumbnailToolbarButton _btnGoToWork;
        private readonly ThumbnailToolbarButton _btnGoToRest;

        private bool _workIsCurrentTimer;

        public event Action Play = () => { };
        public event Action Pause = () => { };
        public event Action Reset = () => { };
        public event Action GoToWork = () => { };
        public event Action TakeABreak = () => { };

        public Pomodo7oWindow(TaskbarManager taskbarManager)
        {
            _taskbarManager = taskbarManager;

            InitializeComponent();

            _btnReset = Button(Res.icon_reset, Res.ToolTip_Reset, () => Reset());
            _btnPlay = Button(Res.icon_play, Res.ToolTip_Play, () => Play());
            _btnPause = Button(Res.icon_pause, Res.ToolTip_Pause, () => Pause());
            _btnGoToWork = Button(Res.icon_tomato, Res.ToolTip_GoToWork, () => GoToWork());
            _btnGoToRest = Button(Res.icon_tomato_rest, Res.ToolTip_GoToRest, () => TakeABreak());
        }

        public void RunningStatus(bool running)
        {
            _btnReset.Visible = true;
            _btnPlay.Visible = !running;
            _btnPause.Visible = running;
            UpdateOverlayIcon(_workIsCurrentTimer, running);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _taskbarManager.ThumbnailToolbars.AddButtons(
                new WindowInteropHelper(this).Handle,
                _btnReset,
                _btnPlay,
                _btnPause,
                _btnGoToWork,
                _btnGoToRest);
        }

        public void WorkStarted()
        {
            _workIsCurrentTimer = true;
            _btnGoToRest.Visible = true;
            _btnGoToWork.Visible = false;
            UpdateOverlayIcon(_workIsCurrentTimer, true);
        }

        public void WorkPercent(int percent)
        {
        }

        public void WorkTimeLeft(TimeSpan remaining)
        {
        }

        public void WorkComplete()
        {
        }

        public void RestStarted()
        {
            _workIsCurrentTimer = false;
            _btnGoToRest.Visible = false;
            _btnGoToWork.Visible = true;
            UpdateOverlayIcon(_workIsCurrentTimer, true);
        }

        public void RestPercent(int percent)
        {
        }

        public void RestTimeLeft(TimeSpan remaining)
        {
        }

        public void RestComplete()
        {
        }

        public void Dispose()
        {
        }

        private void UpdateOverlayIcon(bool workIsCurrentTimer, bool running)
        {
            if(!running)
                _taskbarManager.SetOverlayIcon(this, Res.icon_pause, Res.Mode_Pause);
            else if(!workIsCurrentTimer)
                _taskbarManager.SetOverlayIcon(this, Res.icon_rest, Res.Mode_Rest);
            else
                _taskbarManager.SetOverlayIcon(this, null, Res.Mode_Work);
        }

        private ThumbnailToolbarButton Button(Icon icon, string toolTip, Action onClick)
        {
            return new ThumbnailToolbarButton(icon, toolTip)
                       {
                           DismissOnClick = true
                       }
                .Chain(btn => btn.Click += (o, e) => onClick());
        }
    }
}
