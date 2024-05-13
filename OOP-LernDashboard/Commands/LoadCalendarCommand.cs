using HandyControl.Controls;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                DateTime currentMonth = _dashboardStore.GoogleCalendar.Start;

                _viewModel.UpdateMonth(currentMonth);

                await _dashboardStore.GoogleCalendar.LoadEvents();
                
                int today = DateTime.Now.Day - 1;
                bool isCurrentMonth = DateTime.Now.Month == currentMonth.Month
                                    && DateTime.Now.Year == currentMonth.Year;

                int dayInWeek = (int)currentMonth.DayOfWeek -1;
                int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);

                _viewModel.UpdateFirstDayOffset(dayInWeek);

                _viewModel.Days.Clear();
                for (int i = 0; i < daysInMonth; i++)
                {
                    DayViewModel dayModel = new DayViewModel(isCurrentMonth && today == i);
                    dayModel.DayDesc = i + 1;

                    dayModel.Events = new ObservableCollection<EventViewModel>(
                        _dashboardStore.GoogleCalendar.Events
                        .Where(e => e.StartTime.Day - 1 == i)
                        .OrderBy(e => e.StartTime)
                        .Select(e => new EventViewModel(e.Title, e.StartTime.ToString("HH:mm")))
                        .ToList());

                    _viewModel.Days.Add(dayModel);
                }
            }
            catch(Exception e)
            {
                MessageBox.Fatal("Failed to load Events", e.Message);
                throw new Exception("Failed to load Events");
            }
            _viewModel.IsLoading = false;
        }
    }
}
