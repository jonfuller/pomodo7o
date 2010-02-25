using System.Windows.Media;

namespace Pomodo7o
{
    public class ViewModel : NotifyPropertyChanged
    {
        private int _percent;
        private bool _isPaused;
        private Brush _progressColor = Brushes.Green;
        private string _timeRemaining = string.Empty;

        public int Percent
        {
            get { return _percent; }
            set { SetProperty(() => Percent, () => _percent, value); }
        }

        public Brush ProgressColor
        {
            get { return _progressColor; }
            set { SetProperty(() => ProgressColor, () => _progressColor, value); }
        }

        public bool IsPaused
        {
            get { return _isPaused; }
            set { SetProperty(() => IsPaused, () => _isPaused, value); }
        }

        public string TimeRemaining
        {
            get { return _timeRemaining; }
            set { SetProperty(() => TimeRemaining, () => _timeRemaining, value); }
        }

        public void SetProgressState(ProgressState state)
        {
            IsPaused = state == ProgressState.Paused;

            switch(state)
            {
                case ProgressState.Red:
                    ProgressColor = Brushes.Red;
                    break;
                case ProgressState.Yellow:
                    ProgressColor = Brushes.Yellow;
                    break;
                case ProgressState.Green:
                default:
                    ProgressColor = Brushes.Green;
                    break;
            }
        }
    }
}