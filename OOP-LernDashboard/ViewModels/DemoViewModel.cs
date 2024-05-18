using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Services;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class DemoViewModel : ViewModelBase
    {
        private string? _textBoxInput;
        public string? TextBoxInput
        {
            get { return _textBoxInput; }
            set
            {
                _textBoxInput = value;
                OnPropertyChanged(nameof(TextBoxInput));
            }

        }

        public ICommand NavigationTest { get; }

        public DemoViewModel(NavigationService demoNavigationService)
        {
            NavigationTest = new NavigateCommand(demoNavigationService);
        }

        public static DemoViewModel LoadViewModel(NavigationService demoNavigationService)
        {
            DemoViewModel viewModel = new DemoViewModel(demoNavigationService);
            return viewModel;
        }

    }
}
