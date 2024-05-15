using HandyControl.Controls;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    internal class EditShortcutCommand : CommandBase
    {
        private readonly ShortcutViewModel _shortcutViewModel;
        private readonly DashboardStore _dashboardStore;
        public EditShortcutCommand(ShortcutViewModel shortcutViewModel)
        {
            _shortcutViewModel = shortcutViewModel;
        }

        public override void Execute(object? parameter)
        {
            PopupWindow popup = new PopupWindow()
            {
                MinWidth = 400,
                Title = "Title",
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = true,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None
            };
            TextBox txtUsername = new TextBox();
            //popup.PopupElement = mainStack;
            popup.ShowDialog();
        }
    }
}
