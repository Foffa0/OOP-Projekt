﻿using Google.Apis.Calendar.v3.Data;
using HandyControl.Themes;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace OOP_LernDashboard.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;

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

        private Color _accentColor;
        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                if (_accentColor == value)
                {
                    return;
                }
                _accentColor = value;
                OnPropertyChanged(nameof(AccentColor));
                OnPropertyChanged(nameof(AccentColorBrush));
            }
        }

        private bool _isAutostartEnabled;
        public bool IsAutostartEnabled
        {
            get => _isAutostartEnabled;
            set
            {
                _isAutostartEnabled = value;
                OnPropertyChanged(nameof(IsAutostartEnabled));
            }
        }

        private bool _isMinimizeEnabled;
        public bool IsMinimizeEnabled
        {
            get => _isMinimizeEnabled;
            set
            {
                _isMinimizeEnabled = value;
                OnPropertyChanged(nameof(IsMinimizeEnabled));
            }
        }

        private readonly ObservableCollection<CalendarSelectViewModel> _calendars;
        public IEnumerable<CalendarSelectViewModel> Calendars => _calendars;

        public Brush AccentColorBrush => new SolidColorBrush(AccentColor);

        public ICommand LoadDataAsyncCommand { get; }

        public ICommand LoginGoogleCommand { get; }
        public ICommand LogoutGoogleCommand { get; }

        public ICommand UpdateWelcomeNameCommand { get; }

        public ICommand UpdateAccentColorCommand { get; }

        public ICommand ModifyAutostartCommand { get; }

        public SettingsViewModel(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;

            LoadDataAsyncCommand = new LoadSettingsDataCommand(this, dashboardStore);
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);
            LogoutGoogleCommand = new GoogleLogoutCommand(dashboardStore, this);

            IsGoogleLoggedIn = dashboardStore.GoogleCalendar != null;

            UpdateWelcomeNameCommand = new ModifyWelcomeNameCommand(this, dashboardStore);
            UpdateAccentColorCommand = new UpdateAccentColorCommand(this, dashboardStore);

            this.ModifyAutostartCommand = new ModifyAutostartCommand(this, dashboardStore);

            _calendars = new ObservableCollection<CalendarSelectViewModel>();

            _dashboardStore.GoogleLoggedIn += () => LoadDataAsyncCommand.Execute(null);

            _welcomeName = dashboardStore.WelcomeName;
            AccentColor = (ThemeManager.Current.AccentColor as SolidColorBrush)?.Color ?? new Color();
        }

        public void UpdateCalendars(Models.LinkedList<Calendar> calendars, Models.LinkedList<string> selectedCalendars)
        {
            _calendars.Clear();
            foreach (Calendar calendar in calendars)
            {
                _calendars.Add(new CalendarSelectViewModel(
                    calendar,
                    _dashboardStore,
                    selectedCalendars.Contains(calendar.Id)
                    ));
            }
        }

        public static SettingsViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            SettingsViewModel viewModel = new SettingsViewModel(dashboardStore);
            viewModel.LoadDataAsyncCommand.Execute(null);
            return viewModel;
        }
    }
}
