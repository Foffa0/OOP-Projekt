using OOP_LernDashboard.ViewModels;
using System.Windows.Threading;


namespace OOP_LernDashboard.Models
{
    class Timer
    {
        public event EventHandler<string> TimerDisplayTextChanged;
        public event EventHandler<double> BarValueChanged;

        DispatcherTimer timer;

        DateTime startTime;
        public string timerName;
        public TimeSpan endTime;
        public double elapsedTime;
        public double totalTime;
        public int tickSize = 500;
        public bool isPaused = true;

        //data fields for EventHandlers
        public string TimerDisplayText;
        public double BarValue { get; private set; }

        public Timer(TimeSpan endTime)
        {
            this.endTime = endTime;
            this.timerName = endTime.ToString();
            
            TimerDisplayText = endTime.ToString(@"hh\:mm\:ss");

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(tickSize);

            totalTime = endTime.TotalMilliseconds;
        }

        void TimerTick(object sender, EventArgs e)
        {
            endTime = endTime.Subtract(new TimeSpan(0, 0, 0, 0, tickSize));
            elapsedTime += tickSize;

            TimerDisplayText = $"{endTime.ToString(@"hh\:mm\:ss")}";
            BarValue = (elapsedTime / totalTime) * 100;
            if (endTime.TotalMilliseconds < 0)
            {
                timer.Stop();
                TimerDisplayText = "Ich habe fertig!";
            }
            TimerDisplayTextChanged?.Invoke(this, TimerDisplayText);
            BarValueChanged?.Invoke(this, BarValue);
        }

        public void Start()
        {
            timer.Start();
        }

        public void Pause()
        {
            timer.Stop();
        }

        public void Reset()
        {
            timer.Stop();
            
            elapsedTime = 0;
            endTime = TimeSpan.FromMilliseconds(totalTime);
            
        }
    }
}
