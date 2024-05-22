
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;



namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        


        //public void StartTimer()
        //{
        //    Models.Timer timer = new Models.Timer(this, new TimeSpan(Hours, Minutes, Seconds));
        //    timer.Start();
        //}

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

        public TimerViewModel()
        {

        }

        public static TimerViewModel LoadViewModel()
        {
            TimerViewModel viewModel = new TimerViewModel();
            return viewModel;
        }
    }
}
