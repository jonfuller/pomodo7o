namespace Pomodo7o
{
    public class ViewModelProgressBar : IProgressBar
    {
        private readonly ViewModel _viewModel;

        public ViewModelProgressBar(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void SetState(ProgressState state)
        {
            _viewModel.SetProgressState(state);
        }

        public void SetPercent(int value)
        {
            _viewModel.Percent = value;
        }
    }
}