using OOP_LernDashboard.ViewModels;
using System.Windows.Threading;


namespace OOP_LernDashboard.Models
{
    class Timer
    {
        TimerViewModel _timerViewModel;
        DispatcherTimer timer;

        DateTime startTime;
        public string timerName;
        public TimeSpan endTime;
        public double elapsedTime;
        public double totalTime;
        public int tickSize = 500;
        public bool isPaused = false;

        public Timer(TimerViewModel timerViewModel, TimeSpan endTime)
        {
            _timerViewModel = timerViewModel;

            this.endTime = endTime;
            this.timerName = endTime.ToString();
            _timerViewModel.IconPath = "/Resources/Images/pauseIcon.png";
            _timerViewModel.TimerDisplayText = endTime.ToString(@"hh\:mm\:ss");

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(tickSize);

            totalTime = endTime.TotalMilliseconds;
        }

        void TimerTick(object sender, EventArgs e)
        {
            endTime = endTime.Subtract(new TimeSpan(0, 0, 0, 0, tickSize));
            elapsedTime += tickSize;

            _timerViewModel.TimerDisplayText = $"{endTime.ToString(@"hh\:mm\:ss")}";
            _timerViewModel.BarValue = (elapsedTime / totalTime) * 100;
            if (endTime.TotalMilliseconds < 0)
            {
                timer.Stop();
                _timerViewModel.TimerDisplayText = "Ich habe fertig!";
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Pause()
        {
            if (isPaused)
            {
                _timerViewModel.IconPath = "/Resources/Images/pauseIcon.png";
                timer.Start();
                isPaused = false;

            }
            else
            {
                _timerViewModel.IconPath = "/Resources/Images/playIcon.png";
                timer.Stop();
                isPaused = true;
            }
        }

        public void Reset()
        {
            timer.Stop();
            _timerViewModel.IconPath = "/Resources/Images/playIcon.png";
            elapsedTime = 0;
            endTime = TimeSpan.FromMilliseconds(totalTime);
            _timerViewModel.TimerDisplayText = $"{endTime.ToString(@"hh\:mm\:ss")}";
            _timerViewModel.BarValue = (elapsedTime / totalTime) * 100;
        }
    }
}
