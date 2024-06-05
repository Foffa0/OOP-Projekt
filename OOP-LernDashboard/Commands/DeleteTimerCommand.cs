using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class DeleteTimerCommand : CommandBase
    {
        TimerViewModel _viewModel;


        public DeleteTimerCommand(TimerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.DeleteTimer();
        }
    }
}
