using OOP_LernDashboard.ViewModels;
using System.Windows.Threading;

namespace OOP_LernDashboard.Models
{
    class Timer
    {
        TimerViewModel _timerViewModel;
        DispatcherTimer timer;
        DateTime startTime;
        DateTime endTime;

        public Timer(TimerViewModel timerViewModel, TimeSpan endTime)
        {
            _timerViewModel = timerViewModel;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(500);

            startTime = DateTime.Now;
            this.endTime = DateTime.Now + endTime;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            TimeSpan total = endTime - startTime;

            _timerViewModel.Timer = $"{elapsed.ToString(@"hh\:mm\:ss")}";
            _timerViewModel.BarValue = (elapsed.TotalMilliseconds / total.TotalMilliseconds) * 100;
            if ((endTime - DateTime.Now).TotalMilliseconds < 0)
            {
                timer.Stop();
                _timerViewModel.Timer = "Ich habe fertig!";
            }
        }

        public void Start()
        {
            timer.Start();
        }
    }
}
