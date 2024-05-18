using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class DashboardViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;


        private string _welcomeName = "Studierende Person";
        public string WelcomeName
        {
            get => _welcomeName;
            set
            {
                _welcomeName = value;
                OnPropertyChanged(nameof(WelcomeName));
            }
        }

        public string WelcomeMessage => $"Hallo {WelcomeName}!";

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

        private readonly ObservableCollection<string> _combobox;
        public IEnumerable<string> Combobox => _combobox;

        private readonly ObservableCollection<EventViewModel> _calendarEvents;
        public IEnumerable<EventViewModel> CalendarEvents => _calendarEvents;

        private readonly ObservableCollection<ShortcutViewModel> _shortcuts;
        public IEnumerable<ShortcutViewModel> Shortcuts => _shortcuts;

        private readonly ObservableCollection<CountdownViewModel> _countdowns;
        public IEnumerable<CountdownViewModel> Countdowns => _countdowns;

        public ICommand AddToDoCommand { get; }

        public ICommand LoadDataAsyncCommand { get; }

        public ICommand ShortcutsCommand { get; }

        public DashboardViewModel(Dashboard dashboard, DashboardStore dashboardStore, NavigationService shortcutsNavigationService)
        {
            _dashboardStore = dashboardStore;

            AddToDoCommand = new AddToDoCommand(this, dashboardStore);
            LoadDataAsyncCommand = new LoadDashboardDataCommand(this, dashboardStore);

            ShortcutsCommand = new NavigateCommand(shortcutsNavigationService);

            _toDos = new ObservableCollection<ToDoViewModel>();
            _calendarEvents = new ObservableCollection<EventViewModel>();
            _shortcuts = new ObservableCollection<ShortcutViewModel>();
            _combobox = new ObservableCollection<string>
            {
                "Normales ToDo",
                "Wiederholendes ToDo"
            };
            _countdowns = new ObservableCollection<CountdownViewModel>();



            // Listen for changes in the dashboardStore
            _dashboardStore.ToDoCreated += OnToDoCreated;
            _dashboardStore.ToDoDeleted += OnToDoDeleted;

            IsGoogleReady = dashboardStore.GoogleCalendar != null;
        }

        public override void Dispose()
        {
            _dashboardStore.ToDoCreated -= OnToDoCreated;
            _dashboardStore.ToDoDeleted -= OnToDoDeleted;

            base.Dispose();
        }

        public static DashboardViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore, NavigationService shortcutsNavigationService)
        {
            DashboardViewModel viewModel = new DashboardViewModel(dashboard, dashboardStore, shortcutsNavigationService);
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
            ToDoViewModel toDoViewModel = new ToDoViewModel(toDo);
            _toDos.Add(toDoViewModel);
        }

        /// <summary>
        /// Removes the deleted ToDo from the ObservableCollection
        /// </summary>
        /// <param name="toDo"></param>
        private void OnToDoDeleted(ToDo toDo)
        {
            ToDoViewModel toDoViewModel = new ToDoViewModel(toDo);
            _toDos.Remove(toDoViewModel);
        }


        public void UpdateToDos(IEnumerable<ToDo> todos)
        {
            _toDos.Clear();

            foreach (var toDo in todos)
            {
                _toDos.Add(new ToDoViewModel(toDo));
            }
        }

        public void UpdateWelcomeName(string name)
        {
            WelcomeName = name;
        }

        public void UpdateShortcuts(IEnumerable<Shortcut> shortcuts)
        {
            _shortcuts.Clear();

            foreach (var shortcut in shortcuts)
            {
                _shortcuts.Add(new ShortcutViewModel(_dashboardStore, shortcut));
            }
        }

        public void UpdateCalendarEvents(IEnumerable<CalendarEvent> events)
        {
            _calendarEvents.Clear();

            foreach (var calendarEvent in events)
            {
                _calendarEvents.Add(new EventViewModel(_dashboardStore, calendarEvent));
            }
        }

        public void UpdateCountdowns(IEnumerable<Countdown> countdowns)
        {
            _countdowns.Clear();

            foreach (var countdown in countdowns)
            {
                _countdowns.Add(new CountdownViewModel(countdown));
            }
        }
    }
}
