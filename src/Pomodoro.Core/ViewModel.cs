using System.Drawing;
using Pomodoro.Core.StructureMap;

namespace Pomodoro.Core
{
    [AutoNotify]
    public class ViewModel
    {
        public ViewModel()
        {
            ProgressColor = Color.Green;
            TimeRemaining = string.Empty;
        }

        public virtual int Percent { get; set; }
        public virtual Color ProgressColor { get; set; }
        public virtual bool IsPaused { get; set; }
        public virtual string TimeRemaining { get; set; }

        public void SetProgressState(ProgressState state)
        {
            IsPaused = state == ProgressState.Paused;

            switch(state)
            {
                case ProgressState.Red:
                    ProgressColor = Color.Red;
                    break;
                case ProgressState.Yellow:
                    ProgressColor = Color.Yellow;
                    break;
                case ProgressState.Green:
                default:
                    ProgressColor = Color.Green;
                    break;
            }
        }
    }
}