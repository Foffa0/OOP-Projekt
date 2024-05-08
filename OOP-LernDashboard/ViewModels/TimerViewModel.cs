using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    class TimerViewModel : ViewModelBase
    {
        public static TimerViewModel LoadViewModel()
        {
            TimerViewModel viewModel = new TimerViewModel();
            return viewModel;
        }
    }
}
