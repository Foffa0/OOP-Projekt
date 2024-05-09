using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
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

        public ICommand AddCommand { get; }

        public CalendarViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            Day = new ObservableCollection<DayModel>();
            for (int i = 0; i < 31; i++)
            {
                DayModel dayModel = new DayModel();
                dayModel.DayDesc = (i + 1).ToString();
                dayModel.Dates.Add(new DateModel("abc"));
                dayModel.Dates.Add(new DateModel("def"));
                Day.Add(dayModel);

            }

            AddCommand = new CreateCalendarEventCommand(this, dashboardStore);

            UpdateGoogleReady(dashboardStore.GoogleCalendar != null);
        }

        public static CalendarViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            CalendarViewModel viewModel = new CalendarViewModel(dashboard, dashboardStore);
            return viewModel;
        }

        public void UpdateGoogleReady(bool newState)
        {
            IsGoogleReady = newState;
        }

        internal class DayModel
        {
            public string DayDesc { get; set; }

            public ObservableCollection<DateModel> Dates
            {
                get;
                set;
            }

            public DayModel()
            {
                Dates = new ObservableCollection<DateModel>();
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
