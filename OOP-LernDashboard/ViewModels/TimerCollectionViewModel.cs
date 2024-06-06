using HandyControl.Data;
using HandyControl.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    class TimerCollectionViewModel : ViewModelBase
    {
        public ObservableCollection<TimerViewModel> Timers { get; } = new ObservableCollection<TimerViewModel>();
        public ICommand AddTimerCommand { get; }
        private string _timerName;
        private int _hourInput;
        private int _minuteInput;
        private int _secondInput;


        //save all Timers in 
        public int Hours
        {
            get => this._hourInput;
            set
            {
                _hourInput = value;
                OnPropertyChanged(nameof(Hours));

            }
        }

        public int Minutes
        {
            get => this._minuteInput;
            set
            {
                _minuteInput = value;
                OnPropertyChanged(nameof(Minutes));
            }
        }

        public int Seconds
        {
            get => this._secondInput;
            set
            {
                _secondInput = value;
                OnPropertyChanged(nameof(Seconds));
            }
        }

        public TimeSpan EndTime
        {
            get => new TimeSpan(Hours, Minutes, Seconds);
        }


        public TimerCollectionViewModel()
        {
            AddTimerCommand = new Commands.AddTimerCommand(this);
        }

        public static TimerCollectionViewModel LoadViewModel()
        {
            TimerCollectionViewModel viewModel = new TimerCollectionViewModel();
            return viewModel;
        }
    }
}
