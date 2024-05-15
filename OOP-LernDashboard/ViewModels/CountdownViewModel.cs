using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class CountdownViewModel : ViewModelBase
    {
		private int countdown;

		public int Countdown
		{
			get { return countdown; }
			set { countdown = value; }
		}

		private string countdownInput;

		public string CountdownInput
		{
			get { return countdownInput; }
			set { countdownInput = value; }
		}

		public ICommand AddCountdownCommand;



        public CountdownViewModel(DashboardStore dashboardStore) 
		{ 
			AddCountdownCommand = new CreateCountdownCommand(dashboardStore);
		}

		public static CountdownViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            CountdownViewModel viewModel = new CountdownViewModel(dashboardStore);
            return viewModel;
        }
    }
}
