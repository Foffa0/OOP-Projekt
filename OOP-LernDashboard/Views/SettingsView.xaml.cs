using HandyControl.Data;
using System.Windows.Controls;

namespace OOP_LernDashboard.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            ColorPicker.SelectedColorChanged += ColorPicker_SelectedColorChanged;
        }

        private void ColorPicker_SelectedColorChanged(object? sender, FunctionEventArgs<System.Windows.Media.Color> e)
        {
            if (DataContext == null)
            {
                return;
            }
            (DataContext as ViewModels.SettingsViewModel).AccentColor = e.Info;
        }
    }
}
