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

        public CalendarViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            Day = new ObservableCollection<DayModel>();

            AddCommand = new CreateCalendarEventCommand(this, dashboardStore);
            LoadCalendarCommand = new LoadCalendarCommand(this, dashboardStore);
        }

        public static CalendarViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            CalendarViewModel viewModel = new CalendarViewModel(dashboard, dashboardStore);
            viewModel.LoadCalendarCommand.Execute(null);
            return viewModel;
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
