using HandyControl.Tools.Extension;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

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
            if (_shortcutsViewModel.NewShortcutName.IsNullOrEmpty())
            {
                _shortcutsViewModel.AddError("Name kann nicht leer sein", nameof(ShortcutsViewModel.NewShortcutName));
                return;
            }

            if (_shortcutsViewModel.NewShortcutPath.IsNullOrEmpty())
            {
                _shortcutsViewModel.AddError("Pfad kann nicht leer sein", nameof(ShortcutsViewModel.NewShortcutPath));
                return;
            }

            // check if some shortcut with the same name already exists
            if (_dashboardStore.Shortcuts.Any(s => s.Name == _shortcutsViewModel.NewShortcutName))
            {
                _shortcutsViewModel.AddError("Shortcut mit diesem Namen existiert bereits", nameof(ShortcutsViewModel.NewShortcutName));
                return;
            }

            ShortcutType type;
            string iconPath = "";


            if (Shortcut.IsValidPath(_shortcutsViewModel.NewShortcutPath, ShortcutType.Link))
            {
                type = ShortcutType.Link;

                iconPath = _shortcutsViewModel.NewShortcutPath;

                var uri = new Uri(_shortcutsViewModel.NewShortcutPath);

                iconPath = uri.Host;  // host is "www.google.com" for example

                iconPath = " https://icons.duckduckgo.com/ip3/" + iconPath + ".ico";
            }
            else if (Shortcut.IsValidPath(_shortcutsViewModel.NewShortcutPath, ShortcutType.Application))
            {
                type = ShortcutType.Application;

                iconPath = _shortcutsViewModel.NewShortcutPath;
            }
            else
            {
                _shortcutsViewModel.AddError("Pfad ist nicht valide", nameof(ShortcutsViewModel.NewShortcutPath));
                return;
            }

            Shortcut shortcut = new Shortcut(type, _shortcutsViewModel.NewShortcutPath, _shortcutsViewModel.NewShortcutName, iconPath);
            _shortcutsViewModel.ClearInputFields();
            await _dashboardStore.AddShortcut(shortcut);

        }
    }
}
