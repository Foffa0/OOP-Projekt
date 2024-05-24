using HandyControl.Tools.Extension;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Calendar = Google.Apis.Calendar.v3.Data.Calendar;

namespace OOP_LernDashboard.ViewModels
{
    internal class CalendarViewModel : ViewModelBase
    {

        #region Properties

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

        private string _newEventDescription = "";
        public string NewEventDescription
        {
            get { return _newEventDescription; }
            set
            {
                _newEventDescription = value;
                OnPropertyChanged(nameof(NewEventDescription));
            }
        }

        private DateTime _newEventDate = DateTime.Today;
        public DateTime NewEventDate
        {
            get { return _newEventDate; }
            set
            {
                _newEventDate = value;
                OnPropertyChanged(nameof(NewEventDate));
            }
        }

        private DateTime _newEventStartTime = DateTime.Now;
        public DateTime NewEventStartTime
        {
            get { return _newEventStartTime; }
            set
            {
                if (value >= NewEventEndTime)
                {
                    return;
                }
                _newEventStartTime = value;
                OnPropertyChanged(nameof(NewEventStartTime));
            }
        }

        private DateTime _newEventEndTime = DateTime.Now.AddHours(1);
        public DateTime NewEventEndTime
        {
            get { return _newEventEndTime; }
            set
            {
                if (value <= NewEventStartTime)
                {
                    return;
                }
                _newEventEndTime = value;
                OnPropertyChanged(nameof(NewEventEndTime));
            }
        }

        private bool _isWholeDay = true;
        public bool IsWholeDay
        {
            get { return _isWholeDay; }
            set
            {
                _isWholeDay = value;
                OnPropertyChanged(nameof(IsWholeDay));
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

        private Calendar _selectedCalendar;
        public Calendar SelectedCalendar
        {
            get { return _selectedCalendar; }
            set
            {
                _selectedCalendar = value;
                OnPropertyChanged(nameof(SelectedCalendar));
            }
        }

        public ObservableCollection<Calendar> Calendars { get; private set; }


        #endregion

        public ICommand AddCommand { get; }
        public ICommand LoadCalendarCommand { get; }
        public ICommand LoginGoogleCommand { get; }

        public ICommand PrevMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public CalendarViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            Days = new ObservableCollection<DayViewModel>();
            Calendars = new ObservableCollection<Calendar>();

            AddCommand = new CreateCalendarEventCommand(this, dashboardStore);
            LoadCalendarCommand = new LoadCalendarCommand(this, dashboardStore);
            LoginGoogleCommand = new GoogleLoginCommand(dashboardStore);

            PrevMonthCommand = new ModifyCalendarMonth(this, dashboardStore, -1);
            NextMonthCommand = new ModifyCalendarMonth(this, dashboardStore, 1);

            UpdateGoogleReady(dashboardStore.GoogleCalendar != null);

            dashboardStore.GoogleLoggedIn += OnGoogleLogin;
        }

        public static CalendarViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            CalendarViewModel viewModel = new(dashboard, dashboardStore);
            viewModel.LoadCalendarCommand.Execute(null);
            return viewModel;
        }

        public void UpdateGoogleReady(bool newState)
        {
            IsGoogleReady = newState;
        }

        public void UpdateMonth(DateTime date)
        {
            CultureInfo de = new CultureInfo("de-DE"); // used to get german month names
            Month = date.ToString("MMMM", de);
            Year = date.ToString("yyyy");
        }

        public void UpdateFirstDayOffset(int offset)
        {
            FirstDayOffset = offset;
        }

        public void UpdateCalendars(Models.LinkedList<Calendar> calendars)
        {
            Calendars.Clear();
            foreach (Calendar calendar in calendars)
            {
                Calendars.Add(calendar);
            }

            if (SelectedCalendar == null)
            {
                SelectedCalendar = Calendars.First();
            }
        }


        public void UpdateSelectedCalendar(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return;
            }

            Calendar? calendar = Calendars.FirstOrDefault(c => c.Id == id);

            if (calendar != null)
            {
                SelectedCalendar = calendar;
            }
        }

        /// <summary>
        /// Loads the calendar events from the Google Calendar
        /// </summary>
        private void OnGoogleLogin()
        {
            LoadCalendarCommand.Execute(null);
        }
    }
}
