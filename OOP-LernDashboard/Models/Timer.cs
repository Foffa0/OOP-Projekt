using System.Windows.Threading;
using OOP_LernDashboard.ViewModels;


namespace OOP_LernDashboard.Models
{
    class Timer
    {
        TimerViewModel _timerViewModel;
        DispatcherTimer timer;
        
        DateTime startTime;
        TimeSpan endTime;
        double elapsedTime;
        double totalTime;
        private int tickSize = 500;
        bool isPaused = false;

        public Timer(TimerViewModel timerViewModel, TimeSpan endTime)
        {
            _timerViewModel = timerViewModel;
            this.endTime = endTime;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(tickSize);

            totalTime = endTime.TotalMilliseconds;
        }

        void TimerTick(object sender, EventArgs e)
        {
            endTime = endTime.Subtract(new TimeSpan(0, 0, 0, 0, tickSize));
            elapsedTime += tickSize;


            _timerViewModel.Timer = $"{endTime.ToString(@"hh\:mm\:ss")}";
            _timerViewModel.BarValue = (elapsedTime/totalTime) * 100;
            if (endTime.TotalMilliseconds < 0)
            {
                timer.Stop();
                _timerViewModel.Timer = "Ich habe fertig!";
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
            _timerViewModel.Timer = $"{endTime.ToString(@"hh\:mm\:ss")}";
            _timerViewModel.BarValue = (elapsedTime / totalTime) * 100;
        }
    }
}
