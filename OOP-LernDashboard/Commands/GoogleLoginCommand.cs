using OOP_LernDashboard.Stores;

namespace OOP_LernDashboard.Commands
{
    internal class GoogleLoginCommand : CommandBase
    {
        private readonly DashboardStore _dashboardStore;

        public GoogleLoginCommand(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            _dashboardStore.GoogleLogin.PerformLogin();
        }
    }
}
