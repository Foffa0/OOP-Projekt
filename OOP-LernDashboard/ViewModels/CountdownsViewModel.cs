using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class CountdownsViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CountdownViewModel> _countdowns;
        public IEnumerable<CountdownViewModel> Countdowns => _countdowns;

        private string _countdownInput;

        public string CountdownInput
        {
            get { return _countdownInput; }
            set
            {
                _countdownInput = value;
                OnPropertyChanged(nameof(CountdownInput));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand AddCountdownCommand { get; set; }
        public ICommand LoadDataCommand { get; set; }

        private DashboardStore _dashboardStore;

        public CountdownsViewModel(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;

            AddCountdownCommand = new CreateCountdownCommand(this, dashboardStore);
            LoadDataCommand = new LoadCountdownsCommand(this, dashboardStore);
            _countdowns = new ObservableCollection<CountdownViewModel>();

            _dashboardStore.CountdownCreated += OnCountdownCreated;
            _dashboardStore.CountdownDeleted += OnCountdownDeleted;
        }

        public override void Dispose()
        {
            _dashboardStore.CountdownCreated -= OnCountdownCreated;
            _dashboardStore.CountdownDeleted -= OnCountdownDeleted;

            base.Dispose();
        }


        /// <summary>
        /// Adds the newly created ToDo to the ObservableCollection
        /// </summary>
        /// <param name="countdown"></param>
        private void OnCountdownCreated(Countdown countdown)
        {
            CountdownViewModel countdownViewModel = new CountdownViewModel(countdown);
            _countdowns.Add(countdownViewModel);
        }

        /// <summary>
        /// Removes the deleted ToDo from the ObservableCollection
        /// </summary>
        /// <param name="toDo"></param>
        private void OnCountdownDeleted(Countdown countdown)
        {
            CountdownViewModel countdownViewModel = new CountdownViewModel(countdown);
            _countdowns.Remove(countdownViewModel);
        }


        public void UpdateCountdowns(IEnumerable<Countdown> countdowns)
        {
            _countdowns.Clear();
            foreach (var countdown in countdowns)
            {
                _countdowns.Add(new CountdownViewModel(countdown));
            }
        }

        public static CountdownsViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            CountdownsViewModel viewModel = new CountdownsViewModel(dashboardStore);
            viewModel.LoadDataCommand.Execute(null);
            return viewModel;
        }
    }
}
