using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class DashboardViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;


        private string? _firstName = "Studierende Person";
        private string? _welcomeMessage;

        public string? WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        private string _toDoDesc = "";
        public string ToDoDesc
        {
            get { return _toDoDesc; }
            set
            {
                _toDoDesc = value;
                OnPropertyChanged(nameof(ToDoDesc));
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

        private readonly ObservableCollection<ToDoViewModel> _toDos;
        public IEnumerable<ToDoViewModel> ToDos => _toDos;

        private readonly ObservableCollection<CalendarEventViewModel> _calendarEvents;
        public IEnumerable<CalendarEventViewModel> CalendarEvents => _calendarEvents;

        private readonly ObservableCollection<ShortcutViewModel> _shortcuts;
        public IEnumerable<ShortcutViewModel> Shortcuts => _shortcuts;

        public ICommand AddToDoCommand { get; }
        public ICommand AddShortcutCommand { get; }

        public ICommand LoadDataAsyncCommand { get; }

        public DashboardViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;

            this.WelcomeMessage = $"Hallo {_firstName}!";

            AddToDoCommand = new AddToDoCommand(this, dashboardStore);
            AddShortcutCommand = new AddShortcutCommand(this, dashboardStore);
            LoadDataAsyncCommand = new LoadDashboardDataCommand(this, dashboardStore);

            _toDos = new ObservableCollection<ToDoViewModel>();
            _calendarEvents = new ObservableCollection<CalendarEventViewModel>();

            // Listen for changes in the dashboardStore
            _dashboardStore.ToDoCreated += OnToDoCreated;
            _dashboardStore.ToDoDeleted += OnToDoDeleted;

            _dashboardStore.ShortcutCreated += OnShortcutCreated;

            IsGoogleReady = dashboardStore.GoogleCalendar != null;
        }

        public override void Dispose()
        {
            _dashboardStore.ToDoCreated -= OnToDoCreated;
            _dashboardStore.ToDoDeleted -= OnToDoDeleted;

            _dashboardStore.ShortcutCreated -= OnShortcutCreated;
            base.Dispose();
        }

        public static DashboardViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            DashboardViewModel viewModel = new DashboardViewModel(dashboard, dashboardStore);
            //Load data asynchronously
            viewModel.LoadDataAsyncCommand.Execute(null);
            return viewModel;
        }

        /// <summary>
        /// Adds the newly created ToDo to the ObservableCollection
        /// </summary>
        /// <param name="toDo"></param>
        private void OnToDoCreated(ToDo toDo)
        {
            ToDoViewModel videoViewModel = new ToDoViewModel(toDo);
            _toDos.Add(videoViewModel);
        }

        /// <summary>
        /// Removes the deleted ToDo from the ObservableCollection
        /// </summary>
        /// <param name="toDo"></param>
        private void OnToDoDeleted(ToDo toDo)
        {
            ToDoViewModel videoViewModel = new ToDoViewModel(toDo);
            _toDos.Remove(videoViewModel);
        }

        public void UpdateToDos(IEnumerable<ToDo> todos)
        {
            _toDos.Clear();

            foreach (var toDo in todos) 
            {
                _toDos.Add(new ToDoViewModel(toDo));
            }
        }

        /// <summary>
        /// Adds the newly created Shortcut to the ObservableCollection
        /// </summary>
        /// <param name="shortcut"></param>
        private void OnShortcutCreated(Shortcut shortcut)
        {
            ShortcutViewModel shortcutViewModel = new ShortcutViewModel(shortcut);
            _shortcuts.Add(shortcutViewModel);
        }

        public void UpdateShortcutss(IEnumerable<Shortcut> shortcuts)
        {
            _shortcuts.Clear();

            foreach (var shortcut in shortcuts)
            {
                _shortcuts.Add(new ShortcutViewModel(shortcut));
            }
        }

        public void UpdateCalendarEvents(IEnumerable<CalendarEvent> events)
        {
            _calendarEvents.Clear();

            foreach(var calendarEvent in events)
            {
                _calendarEvents.Add(new CalendarEventViewModel(calendarEvent));
            }
        }
    }
}
