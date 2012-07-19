namespace Pomodoro.Core
{
    public interface IProgressBar
    {
        void SetState(ProgressState state);
        void SetPercent(int value);
    }
}