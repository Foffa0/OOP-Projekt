using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutsViewModel : ViewModelBase
    {
        public static ShortcutsViewModel LoadViewModel()
        {
            ShortcutsViewModel viewModel = new ShortcutsViewModel();
            return viewModel;
        }
    }
}
