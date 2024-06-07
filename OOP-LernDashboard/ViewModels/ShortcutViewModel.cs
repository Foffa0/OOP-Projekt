using HandyControl.Controls;
using HandyControl.Data;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;
        private readonly Shortcut _shortcut;

        public ShortcutType Type => _shortcut.Type;
        public string Path
        {
            get => _shortcut.Path;
            set
            {
                if (Shortcut.IsValidPath(value, _shortcut.Type))
                {
                    _shortcut.Path = value;
                    _ = _dashboardStore.ModifyShortcut(_shortcut);
                }
                else
                {
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Ungültigen Pfad",
                        ShowDateTime = false,
                        StaysOpen = false,
                    });
                }
            }
        }
        public string? Name
        {
            get => _shortcut.Name;
            set
            {
                if (value != null && _dashboardStore.IsUniqueShortcutName(value))
                {
                    _shortcut.Name = value;
                    _ = _dashboardStore.ModifyShortcut(_shortcut);
                }
                else
                {
                    Growl.Error(new GrowlInfo
                    {
                        Message = "Name existiert bereits",
                        ShowDateTime = false,
                        StaysOpen = false,
                    });
                }
            }
        }

        private BitmapSource _bitmapSrc;
        public BitmapSource BitmapSrc
        {
            get => _bitmapSrc;
            set
            {
                _bitmapSrc = value;
                OnPropertyChanged(nameof(BitmapSrc));
            }
        }


        public ICommand OpenShortcutCommand { get; }
        public ICommand DeleteShortcutCommand { get; }

        public ShortcutViewModel(DashboardStore dashboardStore, Shortcut shortcut)
        {
            _dashboardStore = dashboardStore;
            _shortcut = shortcut;

            OpenShortcutCommand = new OpenShortcutCommand(this);
            DeleteShortcutCommand = new DeleteShortcutCommand(_shortcut, dashboardStore);

            if (_shortcut.Type == ShortcutType.Link)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(_shortcut.IconPath);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                bitmapImage.DownloadFailed += (s, e) =>
                {
                    // Handle download failure, set BitmapSource to null or fallback image
                    // sets the BitmapSource to a fallback image in Resources/Images
                    this.BitmapSrc = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/FallbackImage.png"));
                };

                this.BitmapSrc = bitmapImage;
            }
            else if (_shortcut.Type == ShortcutType.Application)
            {
                try
                {
                    Icon? icon = Icon.ExtractAssociatedIcon(_shortcut.IconPath);

                    // Convert the Icon to a BitmapSource
                    this.BitmapSrc = Imaging.CreateBitmapSourceFromHIcon(
                                        icon.Handle,
                                        Int32Rect.Empty,
                                        BitmapSizeOptions.FromEmptyOptions());
                }
                catch
                {
                    BitmapImage bitmapImage = new();
                    this.BitmapSrc = bitmapImage;
                }
            }
            else
            {
                BitmapImage bitmapImage = new();
                this.BitmapSrc = bitmapImage;
            }
        }
    }
}
