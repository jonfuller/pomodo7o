using System;
using System.Windows;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public partial class Pomodo7oWindow : IPomodoroPublisher
    {
        private readonly TaskbarManager _taskbarManager;

        private ThumbnailToolbarButton _btnReset;
        private ThumbnailToolbarButton _btnPlay;
        private ThumbnailToolbarButton _btnPause;
        private bool _workIsCurrentTimer;

        public event Action Play = () => { };
        public event Action Pause = () => { };
        public event Action Reset = () => { };

        public Pomodo7oWindow(TaskbarManager taskbarManager)
        {
            _taskbarManager = taskbarManager;

            InitializeComponent();

            _btnReset = new ThumbnailToolbarButton(Properties.Resources.icon_reset, Properties.Resources.ToolTip_Reset).Chain(btn => btn.Click += (o, e) => ResetClicked());
            _btnPlay = new ThumbnailToolbarButton(Properties.Resources.icon_play, Properties.Resources.ToolTip_Play).Chain(btn => btn.Click += (o, e) => PlayClicked());
            _btnPause = new ThumbnailToolbarButton(Properties.Resources.icon_pause, Properties.Resources.ToolTip_Pause).Chain(btn => btn.Click += (o, e) => PauseClicked());
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
                new WindowInteropHelper(this).Handle, _btnReset, _btnPlay, _btnPause);
        }

        private void PlayClicked()
        {
            Play();
        }

        private void ResetClicked()
        {
            Reset();
        }

        private void PauseClicked()
        {
            Pause();
        }

        public void WorkStarted()
        {
            _workIsCurrentTimer = true;
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
                _taskbarManager.SetOverlayIcon(this, Properties.Resources.icon_pause, Properties.Resources.Mode_Pause);
            else if(!workIsCurrentTimer)
                _taskbarManager.SetOverlayIcon(this, Properties.Resources.icon_rest, Properties.Resources.Mode_Rest);
            else
                _taskbarManager.SetOverlayIcon(this, null, Properties.Resources.Mode_Work);
        }
    }
}
