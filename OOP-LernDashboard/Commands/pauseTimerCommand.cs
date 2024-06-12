using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class PauseTimerCommand : CommandBase
    {
        TimerViewModel _viewModel;

        public PauseTimerCommand(TimerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.StartPauseTimer();
        }
    }
}
