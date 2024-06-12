using HandyControl.Controls;
using HandyControl.Tools.Extension;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Collections.ObjectModel;

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

        public override bool CanExecute(object? parameter)
        {
            return _dashboardStore.GoogleCalendar != null && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                _viewModel.IsLoading = true;
                await _dashboardStore.Load();

                if (_dashboardStore.GoogleCalendar == null)
                    return;

                DateTime currentMonth = _dashboardStore.GoogleCalendar.Start;

                _viewModel.UpdateMonth(currentMonth);
                _viewModel.UpdateGoogleReady(true);

                await _dashboardStore.GoogleCalendar.LoadAllCalendars(onlyEditable: true);

                // check if a single editable calendar exists
                if (_dashboardStore.GoogleCalendar.AllCalendars.IsEmpty)
                {
                    MessageBox.Warning("Keine bearbeitbaren Kalender gefunden.", "Warnung");
                }
                else if (_dashboardStore.SelectedCalendarId.IsNullOrEmpty()) 
                    _dashboardStore.SetSelectedCalendar(_dashboardStore.GoogleCalendar.AllCalendars.First().Id);

                _viewModel.UpdateCalendars(_dashboardStore.GoogleCalendar.AllCalendars);
                _viewModel.UpdateSelectedCalendar(_dashboardStore.SelectedCalendarId);

                if (!_dashboardStore.GoogleCalendar.CalendarIds.IsEmpty)
                {
                    await _dashboardStore.GoogleCalendar.LoadEvents();
                }
                else
                {
                    _dashboardStore.GoogleCalendar.ClearEvents();
                    MessageBox.Info("Kein Kalender ausgewählt.\nGehe in die Einstellungen um dort deine Auswahl zu treffen.");
                }



                int today = DateTime.Now.Day - 1;
                bool isCurrentMonth = DateTime.Now.Month == currentMonth.Month
                                    && DateTime.Now.Year == currentMonth.Year;

                int dayInWeek = (int)currentMonth.DayOfWeek - 1;
                int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);

                _viewModel.UpdateFirstDayOffset(dayInWeek);

                _viewModel.Days.Clear();
                for (int i = 0; i < daysInMonth; i++)
                {
                    DayViewModel dayModel = new DayViewModel(isCurrentMonth && today == i);
                    dayModel.DayDesc = i + 1;

                    dayModel.Events = new ObservableCollection<EventViewModel>(
                        _dashboardStore.GoogleCalendar.Events
                        .Where(e => e.StartTime.Day - 1 == i) // Events for this day
                        .OrderBy(e => e.StartTime) // Sorted by Starttime
                        .Select(e => new EventViewModel(_dashboardStore, e)) // convert into ViewModel
                        .ToList());

                    _viewModel.Days.Add(dayModel);
                }
            }
            catch (Exception e)
            {
                MessageBox.Fatal("Failed to load Events", e.Message);
            }
            _viewModel.IsLoading = false;
        }
    }
}
