using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class SetTimerCommand : CommandBase
    {
        private readonly TimerViewModel _timerViewModel;

        public SetTimerCommand(TimerViewModel timerViewModel)
        {
            _timerViewModel = timerViewModel;
        }

        public override void Execute(object? parameter)
        {
            _timerViewModel.Timer = $"{_timerViewModel.Hours},{_timerViewModel.Minutes},{_timerViewModel.Seconds}";
            _timerViewModel.StartTimer();
        }
    }
}
