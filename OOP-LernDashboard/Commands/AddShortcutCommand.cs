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

            ShortcutType type;
            string iconPath = "";

            if (Uri.IsWellFormedUriString(_shortcutsViewModel.NewShortcutPath, UriKind.RelativeOrAbsolute))
            {
                type = ShortcutType.Link;

                iconPath = _shortcutsViewModel.NewShortcutPath;

                if (iconPath.StartsWith("https://")) iconPath = iconPath.Substring(8);
                if (iconPath.StartsWith("http://")) iconPath = iconPath.Substring(7);
                if (iconPath[^1] == '/') iconPath = iconPath.Substring(0, iconPath.Length - 1);
                iconPath = iconPath.Substring(0, iconPath.IndexOf('/'));

                iconPath = " https://icons.duckduckgo.com/ip3/" + iconPath + ".ico";
            }
            else if (_shortcutsViewModel.NewShortcutPath.IndexOfAny(Path.GetInvalidPathChars()) == -1)
            {
                type = ShortcutType.Application;

                // Get the icon from the executable file
                Icon? icon = Icon.ExtractAssociatedIcon("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");

                // Convert the Icon to a BitmapSource
                BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
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
