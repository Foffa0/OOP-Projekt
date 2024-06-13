using HandyControl.Controls;
using HandyControl.Data;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class AddTimerCommand : CommandBase
    {
        private readonly TimerCollectionViewModel _viewModel;
        private int TimerCount = 0;
        private int maxTimerCount = 8;
        private readonly DashboardStore _dashboardStore;


        public AddTimerCommand(TimerCollectionViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async void Execute(object? parameter)
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

                Models.Timer timer = new(_viewModel.EndTime);
                await _dashboardStore.AddTimer(timer);
                TimerCount++;
            }
        }
    }
}
