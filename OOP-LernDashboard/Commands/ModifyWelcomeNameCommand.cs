using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.ComponentModel;

namespace OOP_LernDashboard.Commands
{
    class ModifyWelcomeNameCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly DashboardStore _dashboardStore;

        public ModifyWelcomeNameCommand(SettingsViewModel settingsViewModel, DashboardStore dashboardStore)
        {
            _settingsViewModel = settingsViewModel;
            _dashboardStore = dashboardStore;

            _settingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_settingsViewModel.WelcomeName);
        }

        public override void Execute(object? parameter)
        {
            _dashboardStore.SetWelcomeName(_settingsViewModel.WelcomeName);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
