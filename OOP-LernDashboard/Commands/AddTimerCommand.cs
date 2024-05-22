using OOP_LernDashboard.ViewModels;
using System.Collections.ObjectModel;

namespace OOP_LernDashboard.Commands
{
    class AddTimerCommand : CommandBase
    {
        private readonly TimerCollectionViewModel _viewModel;
        private int TimerCount = 0;
        private int maxTimerCount = 4;

        public AddTimerCommand(TimerCollectionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (TimerCount < maxTimerCount)
            {
                _viewModel.Timers.Add(new TimerViewModel());
                TimerCount++;
            }
        }
    }
}
