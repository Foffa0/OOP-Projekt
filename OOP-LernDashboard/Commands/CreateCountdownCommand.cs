using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class CreateCountdownCommand : CommandBase
    {
        private readonly CountdownViewModel _countdownViewModel;
        private readonly DashboardStore _dashboardStore;

        public CreateCountdownCommand(CountdownViewModel countdownViewModel, DashboardStore dashboardStore)
        {
            _countdownViewModel = countdownViewModel;
            _dashboardStore = dashboardStore;

            _countdownViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _countdownViewModel.CountdownInput != null;
        }

        public override async void Execute(object? parameter)
        {
            DateTime dateTime = DateTime.Parse(_countdownViewModel.CountdownInput);
            Countdown countdown = new Countdown(dateTime);

            await _dashboardStore.AddCountdown(countdown);

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DashboardViewModel.ToDoDesc))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
}
