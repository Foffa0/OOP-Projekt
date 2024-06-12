using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class ResetTimerCommand : CommandBase
    {
        TimerViewModel _viewModel;


        public ResetTimerCommand(TimerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ResetTimer();
        }
    }
}
