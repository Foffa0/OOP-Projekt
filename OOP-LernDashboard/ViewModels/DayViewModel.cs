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

        private Thickness _thickness;
        public Thickness Thickness
        {
            get { return _thickness; }
            set
            {
                if (_thickness != value)
                {
                    _thickness = value;
                    OnPropertyChanged(nameof(Thickness));
                }
            }
        }

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

        public DayViewModel(bool isLeftCol = false, bool isTopRow = false, bool isRightCol = false, bool isBottomRow = false, bool isToday = false)
        {
            Events = new ObservableCollection<EventViewModel>();
            Thickness = new Thickness(isLeftCol ? 2 : 1, isTopRow ? 2 : 1, isRightCol ? 2 : 1, isBottomRow ? 2 : 1);
            BackgroundColor = isToday ? (SolidColorBrush)Application.Current.Resources["PrimaryColor"] : new SolidColorBrush(Colors.Transparent);
        }
    }
}
