using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class CalendarViewModel : ViewModelBase
    {
        private string _newEventTitle = "";
        public string NewEventTitle
        {
            get { return _newEventTitle; }
            set
            {
                _newEventTitle = value;
                OnPropertyChanged(nameof(NewEventTitle));
            }
        }

        private bool _isGoogleReady = false;
        public bool IsGoogleReady
        {
            get { return _isGoogleReady; }
            set
            {
                if (IsGoogleReady != value)
                {
                    _isGoogleReady = value;
                    OnPropertyChanged(nameof(IsGoogleReady));
                }
            }
        }

        public ObservableCollection<DayModel> Day
        {
            get;
            set;
        }

        private bool _isLoading;
        public bool IsLoading 
        { 
            get { return _isLoading; } 
            set { 
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading));
            } 
        }


        public ICommand AddCommand { get; }
        public ICommand LoadCalendarCommand { get; }
        public ICommand LoginGoogleCommand { get; }

        public CalendarViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            Day = new ObservableCollection<DayModel>();

            AddCommand = new CreateCalendarEventCommand(this, dashboardStore);
            LoadCalendarCommand = new LoadCalendarCommand(this, dashboardStore);
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);

            UpdateGoogleReady(dashboardStore.GoogleCalendar != null);
        }

        public static CalendarViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            CalendarViewModel viewModel = new CalendarViewModel(dashboard, dashboardStore);
            viewModel.LoadCalendarCommand.Execute(null);
            return viewModel;
        }

        public void UpdateGoogleReady(bool newState)
        {
            IsGoogleReady = newState;
        }

        internal class DayModel : ViewModelBase
        {
            public string DayDesc { get; set; }
            public bool IsTopRow { get; set; }

            private Thickness _thickness;
            public Thickness Thickness
            {
                get { return _thickness; }
                set
                {
                    if (_thickness != value)
                    {
                        _thickness = value;
                        OnPropertyChanged(nameof(Thickness));
                    }
                }
            }

            public ObservableCollection<DateModel> Dates
            {
                get;
                set;
            }

            public DayModel(bool isLeftCol = false, bool isTopRow = false, bool isRightCol = false, bool isBottomRow = false)
            {
                Dates = new ObservableCollection<DateModel>();
                IsTopRow = isTopRow;
                Thickness = new Thickness(isLeftCol ? 2 : 1, isTopRow ? 2 : 1, isRightCol ? 2 : 1, isBottomRow ? 2 : 1);
            }
        }

        internal class DateModel
        {
            public string DateDesc { get; set; }

            public DateModel(string date)
            {
                DateDesc = date;
            }
        }
    }
}
