using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    internal class Demo2ViewModel : ViewModelBase
    {

        public Demo2ViewModel()
        {

        }

        public static Demo2ViewModel LoadViewModel()
        {
            Demo2ViewModel viewModel = new Demo2ViewModel();
            return viewModel;
        }

    }
}
