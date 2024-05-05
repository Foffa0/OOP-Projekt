using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public static SettingsViewModel LoadViewModel()
        {
            SettingsViewModel viewModel = new SettingsViewModel();
            return viewModel;
        }
    }
}
