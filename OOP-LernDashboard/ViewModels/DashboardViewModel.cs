namespace OOP_LernDashboard.ViewModels
{
    internal class DashboardViewModel : ViewModelBase
    {
        private string? _firstName = "Studierende Person";
        private string? _welcomeMessage;

        public string? WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        public DashboardViewModel()
        {
            this.WelcomeMessage = $"Hallo {_firstName}!";
        }

        public static DashboardViewModel LoadViewModel()
        {
            DashboardViewModel viewModel = new DashboardViewModel();
            return viewModel;
        }
    }
}
