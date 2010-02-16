using System;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace Pomodo7o
{
    public partial class Pomodo7oWindow
    {
        private readonly TaskbarManager _taskbarManager;

        private ThumbnailToolbarButton _btnReset;
        private ThumbnailToolbarButton _btnPlay;
        private ThumbnailToolbarButton _btnPause;

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
            _taskbarManager.SetOverlayIcon(this,
                running ? null : Properties.Resources.icon_pause,
                running ? String.Empty : Properties.Resources.Mode_Pause);
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
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
    }
}
