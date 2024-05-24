using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class resetTimerCommand : CommandBase
    {
        TimerViewModel _viewModel;


        public resetTimerCommand(TimerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ResetTimer();
        }
    }
}
