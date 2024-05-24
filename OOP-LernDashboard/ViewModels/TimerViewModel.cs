namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        public ICommand resetTimer { get; }
        public ICommand pauseTimer { get; }

        private string _name;
        private double _barValue;
        private string _timerText;
        private TimeSpan _endTime;
        private string _IconPath;
        Models.Timer timer;


        public void StartTimer()
        {
            timer = new Models.Timer(this, _endTime);
            timer.Start();
        }

        public void PauseTimer()
        {
            timer.Pause();
        }

        public void ResetTimer()
        {
            timer.Reset();
        }



        public string Timer
        {
            get => _timerText;
            set
            {
                _timerText = value;
                OnPropertyChanged(nameof(Timer));
            }
        }

        public double BarValue
        {
            get => this._barValue;
            set
            {
                _barValue = value;
                OnPropertyChanged(nameof(BarValue));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string IconPath
        {
            get => _IconPath;
            set
            {
                _IconPath = value;
                OnPropertyChanged(nameof(IconPath));
            }
        }


        public TimerViewModel(string _name, TimeSpan endTime)
        {
            Name = _name;
            _endTime = endTime;
            Timer = endTime.ToString(@"hh\:mm\:ss");
            IconPath = "/Resources/Images/pauseIcon.png";
            resetTimer = new resetTimerCommand(this);
            pauseTimer = new pauseTimerCommand(this);

            StartTimer();
        }
    }
}
