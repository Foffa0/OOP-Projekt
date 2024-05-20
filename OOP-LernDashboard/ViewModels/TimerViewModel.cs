
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using OOP_LernDashboard.Commands;



namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        
        public ICommand SetTimerCommand { get; }

        private DispatcherTimer _timer;
        private DateTime _startTime;
        private TimeSpan _totalTime;

        public TimerViewModel()
        {
            SetTimerCommand = new SetTimerCommand(this);
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1),
            };
            _timer.Tick += Timer_Tick;
        }

        public void StartTimer()
        {
            _startTime = DateTime.Now;
            _totalTime = new TimeSpan(Hours, Minutes, Seconds);
            BarValue = 100.00;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = DateTime.Now - _startTime;
            var remaining = _totalTime - elapsed;
            BarValue = (remaining.TotalSeconds / _totalTime.TotalSeconds) * 100;
            if (remaining.TotalSeconds <= 0)
            {
                _timer.Stop();
                Timer = "Ich habe fertig";
            }
        }

        private void UpdateTime()
        {
            Timer = $"{Hours}:{Minutes},{Seconds}";
        }

        private string _time = "Time";
        public string Timer
        {
            get => _time;
            set
            {
                _time = $"{Hours}:{Minutes},{Seconds}";
                OnPropertyChanged(nameof(Timer));
            }
        }

        private double _barValue;
        public double BarValue
        {
            get => this._barValue;
            set
            {
                _barValue = value;
                OnPropertyChanged(nameof(BarValue));
                
            }
        }

        private int _hours;
        public int Hours
        {
            get => this._hours;
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
                UpdateTime();
                
            }
        }

        private int _minutes;
        public int Minutes
        {
            get => this._minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged(nameof(Minutes));
                UpdateTime();
            }
        }

        private int _seconds=1;
        public int Seconds
        {
            get => this._seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged(nameof(Seconds));
                UpdateTime();
            }
        }

        public static TimerViewModel LoadViewModel()
        {
            TimerViewModel viewModel = new TimerViewModel();
            return viewModel;
        }
    }
}
