using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Stores;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public MainViewModel(NavigationStore navigationStore) 
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
