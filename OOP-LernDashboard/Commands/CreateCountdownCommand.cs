using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.ComponentModel;

namespace OOP_LernDashboard.Commands
{
    internal class CreateCountdownCommand : CommandBase
    {
        private readonly CountdownsViewModel _countdownsViewModel;
        private readonly DashboardStore _dashboardStore;

        public CreateCountdownCommand(CountdownsViewModel countdownViewModel, DashboardStore dashboardStore)
        {
            _countdownsViewModel = countdownViewModel;
            _dashboardStore = dashboardStore;

            _countdownsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_countdownsViewModel.CountdownInput) && !string.IsNullOrEmpty(_countdownsViewModel.Description);
        }

        public override async void Execute(object? parameter)
        {
            var dateTime = DateTime.Parse(_countdownsViewModel.CountdownInput);
            DateOnly date = DateOnly.FromDateTime(dateTime);

            Countdown countdown = new Countdown(date, _countdownsViewModel.Description, false);

            await _dashboardStore.AddCountdown(countdown);

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
