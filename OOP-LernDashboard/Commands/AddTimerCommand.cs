using HandyControl.Controls;
using HandyControl.Data;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class AddTimerCommand : CommandBase
    {
        private readonly TimerCollectionViewModel _viewModel;
        private int TimerCount = 0;
        private int maxTimerCount = 8;


        public AddTimerCommand(TimerCollectionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {

            if (_viewModel.Seconds == 0 && _viewModel.Minutes == 0 && _viewModel.Hours == 0)
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Sekunden müssen >0",
                    ShowDateTime = false,
                    StaysOpen = false
                });
            }
            else if (TimerCount < maxTimerCount)
            {
                _viewModel.Timers.Add(new TimerViewModel(_viewModel.EndTime));
                TimerCount++;
            }
        }
    }
}
