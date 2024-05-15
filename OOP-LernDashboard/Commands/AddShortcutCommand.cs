using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace OOP_LernDashboard.Commands
{
    internal class AddShortcutCommand : CommandBase
    {
        private readonly ShortcutsViewModel _shortcutsViewModel;
        private readonly DashboardStore _dashboardStore;

        public AddShortcutCommand(ShortcutsViewModel shortcutsViewModel, DashboardStore dashboardStore)
        {
            _shortcutsViewModel = shortcutsViewModel;
            _dashboardStore = dashboardStore;
        }


        public override async void Execute(object? parameter)
        {
            if (_shortcutsViewModel.NewShortcutName == "" || _shortcutsViewModel.NewShortcutName == null)
            {
                throw new NullReferenceException("Cannot create Shortcut with Null as name");
            }

            if (_shortcutsViewModel.NewShortcutPath == "" || _shortcutsViewModel.NewShortcutPath == null)
            {
                throw new NullReferenceException("Cannot create Shortcut with Null as path");
            }

            // check if some shortcut with the same name already exists
            if(_dashboardStore.Shortcuts.Any(s => s.Name == _shortcutsViewModel.NewShortcutName))
            {
                MessageBox.Show("Shortcut mit diesem Namen existiert bereits!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ShortcutType type;
            string iconPath = "";

            if (Uri.IsWellFormedUriString(_shortcutsViewModel.NewShortcutPath, UriKind.RelativeOrAbsolute))
            {
                type = ShortcutType.Link;

                iconPath = _shortcutsViewModel.NewShortcutPath;

                Uri uri = new Uri(_shortcutsViewModel.NewShortcutPath);
                iconPath = uri.Host;  // host is "www.google.com" for example

                iconPath = " https://icons.duckduckgo.com/ip3/" + iconPath + ".ico";
            }
            else if (_shortcutsViewModel.NewShortcutPath.IndexOfAny(Path.GetInvalidPathChars()) == -1)
            {
                type = ShortcutType.Application;

                iconPath = _shortcutsViewModel.NewShortcutPath;
            }
            else
            {
                throw new ArgumentException("Path is not a valid link or application path");
            }

            Shortcut shortcut = new Shortcut(type, _shortcutsViewModel.NewShortcutPath, _shortcutsViewModel.NewShortcutName, iconPath);
            _shortcutsViewModel.NewShortcutPath = "";
            _shortcutsViewModel.NewShortcutName = "";
            await _dashboardStore.AddShortcut(shortcut);

        }
    }
}
