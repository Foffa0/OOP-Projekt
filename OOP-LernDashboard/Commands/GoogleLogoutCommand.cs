using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class GoogleLogoutCommand : CommandBase
    {
        private readonly DashboardStore _dashboardStore;
        private readonly SettingsViewModel _viewModel;

        public GoogleLogoutCommand(DashboardStore dashboardStore, SettingsViewModel viewModel)
        {
            _dashboardStore = dashboardStore;
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _dashboardStore.GoogleLogin.Logout();
            _dashboardStore.GoogleCalendar = null;
            _viewModel.IsGoogleLoggedIn = false;
            DashboardStore.AddUpdateAppSettings("GoogleRefreshToken", null);
        }
    }
}
