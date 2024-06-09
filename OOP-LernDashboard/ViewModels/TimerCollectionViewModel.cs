using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    class TimerCollectionViewModel : ViewModelBase
    {
        public ObservableCollection<TimerViewModel> _timers;
        public ICommand AddTimerCommand { get; }
        public ICommand RemoveTimerCommand { get; }
        public ICommand LoadTimersCommand { get; }

        private readonly DashboardStore _dashboardStore;

        private string _timerName;
        private int _hourInput;
        private int _minuteInput;
        private int _secondInput;


        //save all Timers in 
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
            _timers = new ObservableCollection<TimerViewModel>();
            _dashboardStore = dashboardStore;

            AddTimerCommand = new AddTimerCommand(this);
            LoadTimersCommand = new LoadTimersCommand(this, _dashboardStore);
        }

        //public void UpdateTimers(IEnumerable<Models.Timer> timers)
        //{
        //    _timers.Clear();
        //    foreach (var timer in timers)
        //    {
        //        _timers.Add(new TimerViewModel(timer));
        //    }
        //}

        public static TimerCollectionViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            TimerCollectionViewModel viewModel = new TimerCollectionViewModel(dashboardStore);
            return viewModel;
        }
    }
}
