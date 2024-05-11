using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

                await _dashboardStore.GoogleCalendar.LoadEvents();
                
                int today = DateTime.Now.Day - 1;
                bool isCurrentMonth = DateTime.Now.Month == _dashboardStore.GoogleCalendar.Start.Month
                                    && DateTime.Now.Year == _dashboardStore.GoogleCalendar.Start.Year;

                _viewModel.Days.Clear();
                for (int i = 0; i < 31; i++)
                {
                    DayModel dayModel = new DayModel((i - 4) % 7 == 0 || i == 0, i < 7, (i + 3) % 7 == 6 || i == 30, i >= 31 - 7, isCurrentMonth && today == i);
                    dayModel.DayDesc = (i + 1).ToString();

                    dayModel.Dates = new ObservableCollection<DateModel>(
                        _dashboardStore.GoogleCalendar.Events
                        .OrderBy(e => e.StartTime)
                        .Where(e => e.StartTime.Day - 1 == i)
                        .Select(e => new DateModel(e.Title))
                        .ToList());

                    _viewModel.Days.Add(dayModel);
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new Exception("Failed to load Events");
            }
            _viewModel.IsLoading = false;
        }
    }
}
