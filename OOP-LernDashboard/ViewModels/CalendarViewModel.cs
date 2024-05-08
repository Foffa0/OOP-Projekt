using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class CalendarViewModel : ViewModelBase
    {
        public ObservableCollection<DayModel> Day
        {
            get;
            set;
        }

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
        }

        public static CalendarViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            CalendarViewModel viewModel = new CalendarViewModel(dashboard, dashboardStore);
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
