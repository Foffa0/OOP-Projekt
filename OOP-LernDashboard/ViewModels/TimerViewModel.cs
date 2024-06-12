using OOP_LernDashboard.Commands;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        public ICommand resetTimer { get; }
        public ICommand pauseTimer { get; }

        public Guid Id => timer.Id;
        double _barValue;
        string _timerText;
        TimeSpan _endTime;
        string _IconPath;
        Models.Timer timer;


        public void StartPauseTimer()
        {
            if (timer.isPaused)
            {
                timer.Start();
                timer.isPaused = false;
                IconPath = "/Resources/Images/pauseIcon.png";
            }
            else
            {
                timer.Pause();
                timer.isPaused = true;
                IconPath = "/Resources/Images/playIcon.png";
            }

        }

        public void ResetTimer()
        {
            IconPath = "/Resources/Images/playIcon.png";
            timer.Pause();
            timer.Reset();
            TimerDisplayText = $"{timer.endTime.ToString(@"hh\:mm\:ss")}";
            BarValue = (timer.elapsedTime / timer.totalTime) * 100;
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

        

        public TimerViewModel(Models.Timer timer)
        {
            this.timer = timer;
            resetTimer = new resetTimerCommand(this);
            pauseTimer = new pauseTimerCommand(this);

            timer.TimerDisplayTextChanged += (sender, text) =>
            {
                TimerDisplayText = text;
            };

            timer.BarValueChanged += (sender, value) =>
            {
                BarValue = value;
            };

            IconPath = "/Resources/Images/pauseIcon.png";
            StartPauseTimer();
        }
    }
}
