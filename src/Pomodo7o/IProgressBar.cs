namespace Pomodo7o
{
    public interface IProgressBar
    {
        void SetState(ProgressState state);
        void SetPercent(int value);
    }
}