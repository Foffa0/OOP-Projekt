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

        private SolidColorBrush _backgroundColor;
        public SolidColorBrush BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
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
            BackgroundColor = isToday ? (SolidColorBrush)Application.Current.Resources["PrimaryColor"] : new SolidColorBrush(Colors.Transparent);
        }
    }
}
