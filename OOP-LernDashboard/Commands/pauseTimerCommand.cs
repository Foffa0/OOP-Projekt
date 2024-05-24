using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class pauseTimerCommand : CommandBase
    {
        TimerViewModel _viewModel;

        public pauseTimerCommand(TimerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.PauseTimer();
        }
    }
}
