using System.Windows.Media;
using Pomodoro.Core;
using Pomodoro.Core.StructureMap;

namespace Pomodo7o
{
    [AutoNotify]
    public class WpfViewModel
    {
        ViewModel _model;

        public WpfViewModel(ViewModel model)
        {
            _model = model;
            ProgressColor = model.ProgressColor.GetBrush();
            TimeRemaining = model.TimeRemaining;
        }

        public virtual int Percent
        {
            get { return _model.Percent; }
            set { _model.Percent = value; }
        }
        public virtual Brush ProgressColor
        {
            get { return _model.ProgressColor.GetBrush(); }
            set { _model.ProgressColor = ((SolidColorBrush)value).GetSDColor(); }
        }
        public virtual bool IsPaused
        {
            get { return _model.IsPaused; }
            set { _model.IsPaused = true; }
        }
        public virtual string TimeRemaining
        {
            get { return _model.TimeRemaining; }
            set { _model.TimeRemaining = value; }
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