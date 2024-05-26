using HandyControl.Data;
using OOP_LernDashboard.ViewModels;
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
            if (DataContext is SettingsViewModel settingsViewModel)
            {
                settingsViewModel.AccentColor = e.Info;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
