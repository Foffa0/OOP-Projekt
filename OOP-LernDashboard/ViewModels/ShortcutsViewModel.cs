using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutsViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;

        private readonly ObservableCollection<ShortcutViewModel> _shortcuts;
        public IEnumerable<ShortcutViewModel> Shortcuts => _shortcuts;

        private string _newShortcutName = "";
        public string NewShortcutName
        {
            get { return _newShortcutName; }
            set
            {
                _newShortcutName = value;
                OnPropertyChanged(nameof(NewShortcutName));
            }
        }

        private string _newShortcutPath = "";
        public string NewShortcutPath
        {
            get { return _newShortcutPath; }
            set
            {
                _newShortcutPath = value;
                OnPropertyChanged(nameof(NewShortcutPath));
            }
        }

        private bool _hasUnsavedChanges = false;
        public bool HasUnsavedChanges
        {
            get { return _hasUnsavedChanges; }
            set
            {
                _hasUnsavedChanges = value;
                OnPropertyChanged(nameof(HasUnsavedChanges));
            }
        }

        public ICommand LoadDataAsyncCommand { get; }
        public ICommand AddShortcutCommand { get; }

        public ShortcutsViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            LoadDataAsyncCommand = new LoadShortcutsCommand(this, dashboardStore);
            this.AddShortcutCommand = new AddShortcutCommand(this, dashboardStore);

            _shortcuts = new ObservableCollection<ShortcutViewModel>();
        }

        public void UpdateShortcuts(IEnumerable<Shortcut> shortcuts)
        {
            _shortcuts.Clear();
            foreach (var shortcut in shortcuts)
            {
                _shortcuts.Add(new ShortcutViewModel(shortcut));
            }
        }

        public static ShortcutsViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            ShortcutsViewModel viewModel = new ShortcutsViewModel(dashboard, dashboardStore);
            viewModel.LoadDataAsyncCommand.Execute(null);
            return viewModel;
        }
    }
}
