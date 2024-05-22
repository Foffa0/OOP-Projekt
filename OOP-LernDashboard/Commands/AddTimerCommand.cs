using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
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
            if (_viewModel.TimerName.IsNullOrEmpty()) 
            {
                Growl.Error(new GrowlInfo
                {
                    Message = "Timer-Name darf nicht leer sein",
                    ShowDateTime = false,
                    StaysOpen = false
                });
            }
            else if (TimerCount < maxTimerCount)
            {
                _viewModel.Timers.Add(new TimerViewModel(_viewModel.TimerName, _viewModel.EndTime));
                _viewModel.TimerName = "";
                TimerCount++;
            }
        }
    }
}
