using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

        private string _month = "";
        public string Month
        {
            get { return _month; }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    OnPropertyChanged(nameof(Month));
                }
            }
        }
        private string _year = "";
        public string Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(nameof(Year));
                }
            }
        }

        public ObservableCollection<DayViewModel> Days
        {
            get;
            set;
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private int _firstDayOffset;
        public int FirstDayOffset
        {
            get { return _firstDayOffset; }
            set
            {
                _firstDayOffset = value;
                OnPropertyChanged(nameof(FirstDayOffset));
            }
        }


        public ICommand AddCommand { get; }
        public ICommand LoadCalendarCommand { get; }
        public ICommand LoginGoogleCommand { get; }

        public ICommand PrevMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public CalendarViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            Days = new ObservableCollection<DayViewModel>();

            AddCommand = new CreateCalendarEventCommand(this, dashboardStore);
            LoadCalendarCommand = new LoadCalendarCommand(this, dashboardStore);
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);

            PrevMonthCommand = new ModifyCalendarMonth(this, dashboardStore, -1);
            NextMonthCommand = new ModifyCalendarMonth(this, dashboardStore, 1);

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

        public void UpdateMonth(DateTime date)
        {
            CultureInfo de = new CultureInfo("de-DE");
            Month = date.ToString("MMMM", de);
            Year = date.ToString("yyyy");
        }

        public void UpdateFirstDayOffset(int offset)
        {
            FirstDayOffset = offset;
        }

        internal class DayViewModel : ViewModelBase
        {
            public int DayDesc { get; set; }
            public bool IsTopRow { get; set; } = false;

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

            private SolidColorBrush _backgroundColor;
            public SolidColorBrush BackgroundColor
            {
                get { return _backgroundColor; }
                set
                {
                    _backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }

            public ObservableCollection<DateViewModel> Dates
            {
                get;
                set;
            }

            public DayViewModel(bool isLeftCol = false, bool isTopRow = false, bool isRightCol = false, bool isBottomRow = false, bool isToday = false)
            {
                Dates = new ObservableCollection<DateViewModel>();
                IsTopRow = isTopRow;
                Thickness = new Thickness(isLeftCol ? 2 : 1, isTopRow ? 2 : 1, isRightCol ? 2 : 1, isBottomRow ? 2 : 1);
                BackgroundColor = isToday ? (SolidColorBrush)Application.Current.Resources["PrimaryColor"] : new SolidColorBrush(Colors.Transparent);
            }
        }

        internal class DateViewModel
        {
            public string DateDesc { get; set; }
            public string EventStart { get; set; }

            public DateViewModel(string date, string eventStart)
            {
                DateDesc = date;
                EventStart = eventStart;
            }
        }
    }
}
