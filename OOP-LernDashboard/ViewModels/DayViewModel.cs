using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OOP_LernDashboard.ViewModels.CalendarViewModel;
using System.Windows.Media;
using System.Windows;

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

        public ObservableCollection<EventViewModel> Events
        {
            get;
            set;
        }


        public DayViewModel(bool isToday = false)
        {
            Events = new ObservableCollection<EventViewModel>();
            IsToday = isToday;
        }
    }
}
