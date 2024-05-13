using OOP_LernDashboard.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class GoogleLogoutCommand : CommandBase
    {
        private readonly DashboardStore _dashboardStore;

        public GoogleLogoutCommand(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            _dashboardStore.GoogleLogin.Logout();
            _dashboardStore.GoogleCalendar = null;
            DashboardStore.AddUpdateAppSettings("GoogleAuthToken", null);
        }
    }
}
