using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OOP_LernDashboard.ViewModels.CalendarViewModel;

namespace OOP_LernDashboard.Commands
{
    class LoadCalendarCommand : AsyncCommandBase
    {
        private readonly CalendarViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadCalendarCommand(CalendarViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                _viewModel.IsLoading = true;
                await _dashboardStore.Load();

                if(_dashboardStore.GoogleCalendar == null)
                {
                    return;
                }

                //IList<CalendarEvent> events = _dashboardStore.GoogleCalendar.GetEvents();

                await _dashboardStore.GoogleCalendar.LoadEvents();


                for (int i = 0; i < 31; i++)
                {
                    DayModel dayModel = new DayModel((i - 4) % 7 == 0 || i == 0, i < 7, (i + 3) % 7 == 6 || i == 30, i >= 31 - 7);
                    dayModel.DayDesc = (i + 1).ToString();
                    dayModel.Dates.Add(new DateModel("abc"));
                    dayModel.Dates.Add(new DateModel("def"));
                    _viewModel.Day.Add(dayModel);
                }
            }
            catch
            {
                throw new Exception("Failed to load Events");
            }
            _viewModel.IsLoading = false;
        }
    }
}
