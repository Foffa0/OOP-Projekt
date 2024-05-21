
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;



namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        
        public ICommand SetTimerCommand { get; }

        public TimerViewModel()
        {
            SetTimerCommand = new SetTimerCommand(this);
        }

        public void StartTimer()
        {
            Models.Timer timer = new Models.Timer(this, new TimeSpan(Hours, Minutes, Seconds));
            timer.Start();
        }

        private void UpdateTime()
        {
            Timer = $"{Hours}:{Minutes}.{Seconds}";
        }

        private string _time = "Time";
        public string Timer
        {
            get => _time;
            set
            {
                _time = value;
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
