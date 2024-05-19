using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using HandyControl.Expression.Shapes;
using System.Windows.Controls.Primitives;
using HandyControl.Controls;
using HandyControl.Data;
using System.Windows.Input;
using OOP_LernDashboard.Commands;
using System.Runtime.CompilerServices;
using System.Diagnostics;


namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        public static TimerViewModel LoadViewModel()
        {
            TimerViewModel viewModel = new TimerViewModel();
            return viewModel;
        }

        
        public ICommand SetTimerCommand { get; }


        public void StartTimer()
        {
            BarValue = 100.00;
            
        }

        private void UpdateTime()
        {
            Timer = $"{Hours}:{Minutes},{Seconds}";
        }

        private string _time = "Time";
        public string Timer
        {
            get => _time;
            set
            {
                _time = $"{Hours}:{Minutes},{Seconds}";
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

        private int _hours = 00;
        public int Hours
        {
            get => this._hours;
            set
            {
                _hours = value;
                OnPropertyChanged(nameof(Hours));
                UpdateTime();
                
            }
        }

        private int _minutes = 00;
        public int Minutes
        {
            get => this._minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged(nameof(Minutes));
                UpdateTime();
            }
        }

        private int _seconds;
        public int Seconds
        {
            get => this._seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged(nameof(Seconds));
                UpdateTime();
            }
        }



        public TimerViewModel()
        {
            SetTimerCommand = new SetTimerCommand(this);
        }

        

    }
}
