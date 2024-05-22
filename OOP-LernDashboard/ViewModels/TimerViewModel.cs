
using System.Threading;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;



namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {

        private string _name;
        private double _barValue;
        private string _timerText = "Time";
        private TimeSpan _endTime;

        public void StartTimer()
        {
            Models.Timer timer = new Models.Timer(this, _endTime);
            timer.Start();
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


        public TimerViewModel(string _name, TimeSpan endTime)
        {
            Name = _name;
            _endTime = endTime;
            StartTimer();
        }


    }
}
