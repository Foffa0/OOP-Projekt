using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Globalization;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class EventViewModel : ViewModelBase
    {
        private readonly CalendarEvent _event;
        public CalendarEvent Event => _event;

        CultureInfo de = new CultureInfo("de-DE");

        public string EventTitle => _event.Title;
        public string EventDescription => _event.Description;
        public string Date => _event.StartTime.ToString("dd.MM.yyyy", de);

        public string StartTime => _event.StartTime.ToString("HH:mm", de);
        public string EndTime => _event.EndTime?.ToString("HH:mm", de) ?? "";

        public string TimeWindow => IsWholeDayEvent ? "Ganztägig" : $"{StartTime} - {EndTime}";

        public bool IsWholeDayEvent => _event.IsAllDayEvent;

        public bool CanEdit => _event.CanEdit;

        public string DateText => ToDateText(_event.StartTime);

        #region TempData

        // used to store updated data before saving
        private string _eventTitleTemp;
        public string EventTitleTemp
        {
            get => _eventTitleTemp;
            set
            {
                if (_eventTitleTemp == value)
                {
                    return;
                }
                _eventTitleTemp = value;
                OnPropertyChanged(nameof(EventTitleTemp));
            }
        }

        private string _eventDescriptionTemp;
        public string EventDescriptionTemp
        {
            get => _eventDescriptionTemp;
            set
            {
                if (_eventDescriptionTemp == value)
                {
                    return;
                }
                _eventDescriptionTemp = value;
                OnPropertyChanged(nameof(EventDescriptionTemp));
            }
        }

        private DateTime _eventDateTemp;
        public DateTime EventDateTemp
        {
            get => _eventDateTemp;
            set
            {
                _eventDateTemp = value;
                OnPropertyChanged(nameof(EventDateTemp));
            }
        }

        private DateTime _eventStartTimeTemp;
        public DateTime EventStartTimeTemp
        {
            get => _eventStartTimeTemp;
            set
            {
                _eventStartTimeTemp = value;
                OnPropertyChanged(nameof(EventStartTimeTemp));
            }
        }

        private DateTime _eventEndTimeTemp;
        public DateTime EventEndTimeTemp
        {
            get => _eventEndTimeTemp;
            set
            {
                _eventEndTimeTemp = value;
                OnPropertyChanged(nameof(EventEndTimeTemp));
            }
        }

        #endregion

        public Action OnSaved;
        public Action OnDeleted;

        public ICommand ShowDetailsCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public EventViewModel(DashboardStore dashboardStore, CalendarEvent calendarEvent)
        {
            ShowDetailsCommand = new ShowEventDetailsCommand(this, dashboardStore);
            SaveCommand = new ModifyEventDetailsCommand(this, dashboardStore);
            DeleteCommand = new DeleteCalendarEventCommand(this, dashboardStore);

            _event = calendarEvent;

            _eventTitleTemp = EventTitle;
            _eventDescriptionTemp = EventDescription;
            _eventDateTemp = _event.StartTime.Date;
            if (!IsWholeDayEvent)
            {
                _eventStartTimeTemp = _event.StartTime;
                _eventEndTimeTemp = _event.EndTime!.Value;
            }
        }

        /// <summary>
        /// Resets the temp data to the current event data
        /// </summary>
        public void ResetTempData()
        {
            EventTitleTemp = EventTitle;
            EventDescriptionTemp = EventDescription;
            EventDateTemp = _event.StartTime.Date;

            if (!IsWholeDayEvent)
            {
                EventStartTimeTemp = _event.StartTime;
                EventEndTimeTemp = _event.EndTime!.Value;
            }
        }

        /// <summary>
        /// Applies the temp data to the event
        /// </summary>
        public void ApplyTempData()
        {
            _event.Title = EventTitleTemp;
            _event.Description = EventDescriptionTemp;
            _event.StartTime = EventDateTemp.Date.Add(EventStartTimeTemp.TimeOfDay);
            _event.EndTime = IsWholeDayEvent ? null : EventDateTemp.Date.Add(EventEndTimeTemp.TimeOfDay);
        }

        private string ToDateText(DateTime date)
        {
            // date is today 
            if (date.Date == DateTime.Today)
                return "Heute";

            // date is tomorrow
            if (date.Date == DateTime.Today.AddDays(1))
                return "Morgen";

            // date is day after tomorrow
            if (date.Date == DateTime.Today.AddDays(2))
                return "Übermorgen";

            // date is in the next 7 days
            if (date.Date < DateTime.Today.AddDays(7))
                return date.ToString("dddd", de);

            return date.ToString("dd.MM.yyyy", de);
        }
    }
}
