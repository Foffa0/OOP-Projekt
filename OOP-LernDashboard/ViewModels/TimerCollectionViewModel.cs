using OOP_LernDashboard.Commands;
using System.Collections.ObjectModel;
using System.Timers;
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
        private int _secondInput=1;
        
        

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
