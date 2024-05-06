using OOP_LernDashboard.Stores;

namespace OOP_LernDashboard.Models
{
    class Dashboard
    {
        private readonly DashboardStore _dashboardStore;


        public Dashboard(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
        }
    }
}
