﻿using System.Windows.Threading;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Models
{
    class Timer
    {
        string timerName;
        TimerViewModel _timerViewModel;
        DispatcherTimer timer;
        DateTime startTime;
        DateTime endTime;

        public Timer(TimerViewModel timerViewModel, TimeSpan endTime)
        {
            _timerViewModel = timerViewModel;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1);

            startTime = DateTime.Now;
            this.endTime = DateTime.Now + endTime;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            TimeSpan total = endTime - startTime;

            _timerViewModel.Timer = $"{elapsed}";
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
