using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
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

        public ObservableCollection<DayModel> Days
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

        public ICommand PrevMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public CalendarViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            Days = new ObservableCollection<DayModel>();

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

        internal class DayModel : ViewModelBase
        {
            public string DayDesc { get; set; } = "";
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
                    if (_backgroundColor != value)
                    {
                        _backgroundColor = value;
                        OnPropertyChanged(nameof(BackgroundColor));
                    }
                }
            }

            public ObservableCollection<DateModel> Dates
            {
                get;
                set;
            }

            public DayModel(bool isLeftCol = false, bool isTopRow = false, bool isRightCol = false, bool isBottomRow = false, bool isToday = false)
            {
                Dates = new ObservableCollection<DateModel>();
                IsTopRow = isTopRow;
                Thickness = new Thickness(isLeftCol ? 2 : 1, isTopRow ? 2 : 1, isRightCol ? 2 : 1, isBottomRow ? 2 : 1);
                BackgroundColor = isToday ? (SolidColorBrush)Application.Current.Resources["PrimaryColor"] : new SolidColorBrush(Colors.Transparent);
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
