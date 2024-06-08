using OOP_LernDashboard.Commands;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        public ICommand resetTimer { get; }
        public ICommand pauseTimer { get; }

        private double _barValue;
        private string _timerText;
        private TimeSpan _endTime;
        private string _IconPath;
        Models.Timer timer;


        public void StartTimer()
        {
            timer.Start();
        }

        public void PauseTimer()
        {
            timer.Pause();
        }

        public void ResetTimer()
        {
            timer.Pause();
            timer.Reset();
        }

        public void DeleteTimer()
        {

        }


        public string TimerDisplayText
        {
            get => _timerText;
            set
            {
                _timerText = value;
                OnPropertyChanged(nameof(TimerDisplayText));
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
            get => timer.timerName;
            set
            {
                timer.timerName = value;
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


        public TimerViewModel(TimeSpan endTime)
        {
            timer = new Models.Timer(this, endTime);
            resetTimer = new resetTimerCommand(this);
            pauseTimer = new pauseTimerCommand(this);

            StartTimer();
        }
    }
}
