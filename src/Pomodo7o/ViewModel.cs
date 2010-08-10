using System.Windows.Media;

namespace Pomodo7o
{
    public class ViewModel
    {
        public ViewModel()
        {
            ProgressColor = Brushes.Green;
            TimeRemaining = string.Empty;
        }

        public virtual int Percent { get; set; }
        public virtual Brush ProgressColor { get; set; }
        public virtual bool IsPaused { get; set; }
        public virtual string TimeRemaining { get; set; }

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