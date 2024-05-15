using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutViewModel
    {
        private readonly Shortcut _shortcut;

        public ShortcutType Type => _shortcut.Type;
        public string Path
        {
            get => _shortcut.Path;
            set => _shortcut.Path = value;
        }
        public string? Name
        {
            get => _shortcut.Name;
            set => _shortcut.Name = value;
        }

        public BitmapSource BitmapSource { get; private set; }

        public ICommand OpenShortcutCommand { get; }

        public ShortcutViewModel(Shortcut shortcut)
        {
            _shortcut = shortcut;

            OpenShortcutCommand = new OpenShortcutCommand(this);

            if (_shortcut.Type == ShortcutType.Link)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(_shortcut.IconPath));
                this.BitmapSource = bitmapImage;
            }
            else if (_shortcut.Type == ShortcutType.Application)
            {
                try
                {
                    Icon? icon = Icon.ExtractAssociatedIcon(_shortcut.IconPath);

                    // Convert the Icon to a BitmapSource
                    this.BitmapSource = Imaging.CreateBitmapSourceFromHIcon(
                                        icon.Handle,
                                        Int32Rect.Empty,
                                        BitmapSizeOptions.FromEmptyOptions());
                }
                catch
                {
                    BitmapImage bitmapImage = new();
                    this.BitmapSource = bitmapImage;
                }
            }
            else
            {
                BitmapImage bitmapImage = new();
                this.BitmapSource = bitmapImage;
            }
        }
    }
}
