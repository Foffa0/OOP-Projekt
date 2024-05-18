using HandyControl.Themes;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Stores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

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

        private string _welcomeName = "Studierende Person";
        public string WelcomeName
        {
            get => _welcomeName;
            set
            {
                _welcomeName = value;
                OnPropertyChanged(nameof(WelcomeName));
            }
        }

        private System.Windows.Media.Color _accentColor;
        public System.Windows.Media.Color AccentColor
        {
            get => _accentColor;
            set
            {
                if(_accentColor == value)
                {
                    return;
                }
                _accentColor = value;
                OnPropertyChanged(nameof(AccentColor));
                OnPropertyChanged(nameof(AccentColorBrush));
            }
        }

        public System.Windows.Media.Brush AccentColorBrush => new SolidColorBrush(AccentColor); 

        public ICommand LoginGoogleCommand { get; }
        public ICommand LogoutGoogleCommand { get; }

        public ICommand UpdateWelcomeNameCommand { get; }

        public ICommand UpdateAccentColorCommand { get; }

        public SettingsViewModel(DashboardStore dashboardStore) 
        {
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);
            LogoutGoogleCommand = new GoogleLogoutCommand(dashboardStore);
            IsGoogleLoggedIn = dashboardStore.GoogleCalendar != null;
            UpdateWelcomeNameCommand = new ModifyWelcomeNameCommand(this, dashboardStore);
            UpdateAccentColorCommand = new UpdateAccentColorCommand(this, dashboardStore);

            _welcomeName = dashboardStore.WelcomeName;
            AccentColor = (ThemeManager.Current.AccentColor as SolidColorBrush).Color;

        }

        public static SettingsViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            SettingsViewModel viewModel = new SettingsViewModel(dashboardStore);
            return viewModel;
        }
    }
}
