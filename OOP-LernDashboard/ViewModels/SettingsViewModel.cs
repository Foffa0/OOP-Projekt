using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public ICommand LoginGoogleCommand { get; }

        public SettingsViewModel(DashboardStore dashboardStore) 
        {
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);
        }

        public static SettingsViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            SettingsViewModel viewModel = new SettingsViewModel(dashboardStore);
            return viewModel;
        }
    }
}
