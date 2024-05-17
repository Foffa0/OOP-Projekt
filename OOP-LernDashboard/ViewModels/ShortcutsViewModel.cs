using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutsViewModel : ViewModelBase, INotifyDataErrorInfo
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
                ClearErrors(nameof(NewShortcutName));
                if (string.IsNullOrEmpty(value))
                {
                    AddError("Name can't be empty", nameof(NewShortcutName));
                }
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
                ClearErrors(nameof(NewShortcutPath));
                if (string.IsNullOrEmpty(value))
                {
                    AddError("Path can't be empty", nameof(NewShortcutPath));
                }
                _newShortcutPath = value;
                OnPropertyChanged(nameof(NewShortcutPath));
            }
        }

        // Validation Errors
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;


        public ICommand LoadDataAsyncCommand { get; }
        public ICommand AddShortcutCommand { get; }

        public ShortcutsViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            LoadDataAsyncCommand = new LoadShortcutsCommand(this, dashboardStore);
            this.AddShortcutCommand = new AddShortcutCommand(this, dashboardStore);

            _shortcuts = new ObservableCollection<ShortcutViewModel>();

            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();

            _dashboardStore.ShortcutDeleted += OnShortcutDeleted;
            _dashboardStore.ShortcutCreated += OnShortcutCreated;
        }

        public void UpdateShortcuts(IEnumerable<Shortcut> shortcuts)
        {
            _shortcuts.Clear();
            foreach (var shortcut in shortcuts)
            {
                _shortcuts.Add(new ShortcutViewModel(_dashboardStore, shortcut));
            }
        }

        /// <summary>
        /// Adds the newly created Shortcut to the ObservableCollection
        /// </summary>
        /// <param name="shortcut"></param>
        private void OnShortcutCreated(Shortcut shortcut)
        {
            ShortcutViewModel shortcutViewModel = new ShortcutViewModel(_dashboardStore, shortcut);
            _shortcuts.Add(shortcutViewModel);
        }

        /// <summary>
        /// Removes the deleted Shortcut from the ObservableCollection
        /// </summary>
        /// <param name="shortcut"></param>
        private void OnShortcutDeleted(Shortcut shortcut)
        {
            var s = _shortcuts.Where(s => s.Name == shortcut.Name).FirstOrDefault();
            if (s != null)
            {
                _shortcuts.Remove(s);
            }
        }

        public static ShortcutsViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            ShortcutsViewModel viewModel = new ShortcutsViewModel(dashboard, dashboardStore);
            viewModel.LoadDataAsyncCommand.Execute(null);
            return viewModel;
        }

        // Input validation

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, new List<string>());
            }

            _propertyNameToErrorsDictionary[propertyName].Add(errorMessage);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }
    }
}
