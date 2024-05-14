using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutViewModel
    {
        private readonly Shortcut _shortcut;

        public ShortcutType Type => _shortcut.Type;
        public string Path => _shortcut.Path;
        public string? Name => _shortcut.Name;
        public string? IconPath => _shortcut.IconPath;

        public BitmapSource BitmapSource { get; private set; }

        public ShortcutViewModel(Shortcut shortcut)
        {
            _shortcut = shortcut;

            Icon? icon = Icon.ExtractAssociatedIcon("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");

            // Convert the Icon to a BitmapSource
            this.BitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
