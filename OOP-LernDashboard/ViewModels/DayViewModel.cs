using System.Collections.ObjectModel;

namespace OOP_LernDashboard.ViewModels
{
    internal class DayViewModel : ViewModelBase
    {
        public int DayDesc { get; set; }

        private bool _isToday;
        public bool IsToday
        {
            get { return _isToday; }
            set
            {
                _isToday = value;
                OnPropertyChanged(nameof(IsToday));
            }
        }

        public ObservableCollection<EventViewModel> Events { get; set; }

        public DayViewModel(bool isToday = false)
        {
            Events = new ObservableCollection<EventViewModel>();
            IsToday = isToday;
        }
    }
}
