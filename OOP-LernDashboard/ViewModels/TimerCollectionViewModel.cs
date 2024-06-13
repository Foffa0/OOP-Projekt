using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    class TimerCollectionViewModel : ViewModelBase
    {
        public ObservableCollection<TimerViewModel> Timers { get; }
        public ICommand AddTimerCommand { get; }
        public ICommand DeleteTimerCommand { get; }
        public ICommand LoadTimersCommand { get; }

        private DashboardStore _dashboardStore;

        private int _hourInput;
        private int _minuteInput;
        private int _secondInput;


        public int Hours
        {
            get => this._hourInput;
            set
            {
                _hourInput = value;
                OnPropertyChanged(nameof(Hours));

            }
        }

        public int Minutes
        {
            get => this._minuteInput;
            set
            {
                _minuteInput = value;
                OnPropertyChanged(nameof(Minutes));
            }
        }

        public int Seconds
        {
            get => this._secondInput;
            set
            {
                _secondInput = value;
                OnPropertyChanged(nameof(Seconds));
            }
        }

        public TimeSpan EndTime
        {
            get => new TimeSpan(Hours, Minutes, Seconds);
        }


        public TimerCollectionViewModel(DashboardStore dashboardStore)
        {
            Timers = new ObservableCollection<TimerViewModel>();
            _dashboardStore = dashboardStore;

            AddTimerCommand = new AddTimerCommand(this, _dashboardStore);
            LoadTimersCommand = new LoadTimersCommand(this, _dashboardStore);
            DeleteTimerCommand = new DeleteTimerCommand(_dashboardStore);

            _dashboardStore.TimerCreated += OnTimerCreated;
            _dashboardStore.TimerDeleted += OnTimerDeleted;
        }

        public override void Dispose()
        {
            _dashboardStore.TimerCreated -= OnTimerCreated;
            _dashboardStore.TimerDeleted -= OnTimerDeleted;

            base.Dispose();
        }


        private void OnTimerCreated(Models.Timer timer)
        {
            TimerViewModel timerViewModel = new TimerViewModel(timer);
            Timers.Add(timerViewModel);
        }

        private void OnTimerDeleted(Models.Timer timer)
        {
            TimerViewModel timerViewModel = new TimerViewModel(timer);
            //timerViewModel.DeleteTimer();
            Timers.Remove(Timers.Where(i => i.Id == timerViewModel.Id).Single());
        }

        public void UpdateTimers(IEnumerable<Models.Timer> timers)
        {
            Timers.Clear();
            foreach (var timer in timers)
            {
                Timers.Add(new TimerViewModel(timer));
            }
        }

        public static TimerCollectionViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            TimerCollectionViewModel viewModel = new TimerCollectionViewModel(dashboardStore);
            viewModel.LoadTimersCommand.Execute(null);
            return viewModel;
        }
    }
}
