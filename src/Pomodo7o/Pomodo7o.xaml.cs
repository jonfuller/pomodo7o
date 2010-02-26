using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    using Res = Properties.Resources;

    public partial class Pomodo7oWindow : IPomodoroPublisher
    {
        private readonly ITaskbarManager _taskbarManager;

        private readonly ThumbnailToolbarButton _btnReset;
        private readonly ThumbnailToolbarButton _btnPlay;
        private readonly ThumbnailToolbarButton _btnPause;
        private readonly ThumbnailToolbarButton _btnGoToWork;
        private readonly ThumbnailToolbarButton _btnGoToRest;

        private readonly ViewModel _viewModel;
        private bool _workIsCurrentTimer;

        public event Action Play = () => { };
        public event Action Pause = () => { };
        public event Action Reset = () => { };
        public event Action GoToWork = () => { };
        public event Action TakeABreak = () => { };

        public Pomodo7oWindow(ITaskbarManager taskbarManager, ViewModel viewModel)
        {
            _taskbarManager = taskbarManager;
            _viewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();

            _btnReset = Button(Res.icon_reset, Res.ToolTip_Reset, () => Reset());
            _btnPlay = Button(Res.icon_play, Res.ToolTip_Play, () => Play());
            _btnPause = Button(Res.icon_pause, Res.ToolTip_Pause, () => Pause());
            _btnGoToWork = Button(Res.icon_tomato, Res.ToolTip_GoToWork, () => GoToWork());
            _btnGoToRest = Button(Res.icon_tomato_rest, Res.ToolTip_GoToRest, () => TakeABreak());
        }

        public void Paused()
        {
            RunningStatus(_workIsCurrentTimer, false);
        }

        public void Resumed()
        {
            RunningStatus(_workIsCurrentTimer, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _taskbarManager.AddThumbButtons(
                new WindowInteropHelper(this).Handle,
                _btnReset,
                _btnPlay,
                _btnPause,
                _btnGoToWork,
                _btnGoToRest);

            var jumpList = JumpList.CreateJumpList();
            jumpList.AddUserTasks(
                new JumpListLink("http://github.com/jonfuller/pomodo7o/issues", "Log Bug"),
                new JumpListLink("http://pomodo7o.uservoice.com/forums/40667-general", "Request a Feature"),
                new JumpListLink("http://github.com/jonfuller/pomodo7o/downloads", "Check for updates"),
                new JumpListLink("http://github.com/jonfuller/pomodo7o", "Fork Me!"),
                new JumpListLink("http://www.twitter.com/jon_fuller", "Tweet Me"));
            jumpList.Refresh();
        }

        public void WorkStarted()
        {
            RunningStatus(true, true);
        }

        public void WorkPercent(int percent)
        {
        }

        public void WorkTimeLeft(TimeSpan remaining)
        {
            _viewModel.TimeRemaining = "{0}:{1}".ToFormat(remaining.Minutes, remaining.Seconds);

            var v = VisualTreeHelper.GetOffset(lblTime);

            _taskbarManager.SetThumbnailClip(
                (new WindowInteropHelper(this)).Handle,
                new Rectangle((int)v.X, (int)v.Y, (int)lblTime.RenderSize.Width, (int)lblTime.RenderSize.Height));
        }

        public void WorkComplete()
        {
        }

        public void RestStarted()
        {
            RunningStatus(false, true);
        }

        public void RestPercent(int percent)
        {
        }

        public void RestTimeLeft(TimeSpan remaining)
        {
            _viewModel.TimeRemaining = "{0}:{1}".ToFormat(remaining.Minutes, remaining.Seconds);

            var v = VisualTreeHelper.GetOffset(lblTime);

            _taskbarManager.SetThumbnailClip(
                (new WindowInteropHelper(this)).Handle,
                new Rectangle((int)v.X, (int)v.Y, (int)lblTime.RenderSize.Width, (int)lblTime.RenderSize.Height));
        }

        public void RestComplete()
        {
        }

        public void Dispose()
        {
        }

        public void Dispatch(Action toInvoke)
        {
            Dispatcher.Invoke(toInvoke);
        }

        private void RunningStatus(bool workIsCurrentTimer, bool running)
        {
            _workIsCurrentTimer = workIsCurrentTimer;

            _btnGoToRest.Visible = workIsCurrentTimer;
            btnGoToRest.Visibility = workIsCurrentTimer.ToVisibility();

            _btnGoToWork.Visible = !workIsCurrentTimer;
            btnGoToWork.Visibility = (!workIsCurrentTimer).ToVisibility();

            _btnReset.Visible = true;
            btnReset.Visibility = true.ToVisibility();

            _btnPlay.Visible = !running;
            btnPlay.Visibility = (!running).ToVisibility();

            _btnPause.Visible = running;
            btnPause.Visibility = running.ToVisibility();

            UpdateOverlayIcon(workIsCurrentTimer, running);
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

        #region UI clicks
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void btnGoToWork_Click(object sender, RoutedEventArgs e)
        {
            GoToWork();
        }

        private void btnGoToRest_Click(object sender, RoutedEventArgs e)
        {
            TakeABreak();
        }
        #endregion
    }
}
