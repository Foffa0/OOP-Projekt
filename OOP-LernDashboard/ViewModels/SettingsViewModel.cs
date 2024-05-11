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

        private bool _isGoogleLoggedIn;
        public bool IsGoogleLoggedIn
        {
            get => _isGoogleLoggedIn;
            set
            {
                _isGoogleLoggedIn = value;
                OnPropertyChanged(nameof(IsGoogleLoggedIn));
            }
        }

        public ICommand LoginGoogleCommand { get; }
        public ICommand LogoutGoogleCommand { get; }

        public SettingsViewModel(DashboardStore dashboardStore) 
        {
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);
            LogoutGoogleCommand = new GoogleLogoutCommand(dashboardStore);
            IsGoogleLoggedIn = dashboardStore.GoogleCalendar != null;
        }

        public static SettingsViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            SettingsViewModel viewModel = new SettingsViewModel(dashboardStore);
            return viewModel;
        }
    }
}
