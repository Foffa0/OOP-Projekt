using HandyControl.Controls;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using OOP_LernDashboard.Views;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    internal class ShowEventDetailsCommand : CommandBase
    {
        private readonly EventViewModel _eventViewModel;
        private readonly DashboardStore _dashboardStore;

        private PopupWindow _popup;

        public ShowEventDetailsCommand(EventViewModel eventViewModel, DashboardStore dashboardStore)
        {
            _eventViewModel = eventViewModel;
            _dashboardStore = dashboardStore;

            _eventViewModel.OnSaved += () => _popup?.Close();
            _eventViewModel.OnDeleted += () => _popup?.Close();
        }

        public override void Execute(object? parameter)
        {
            // Reset the temporary data in the EventViewModel before showing the EventDetailsView
            // This is necessary to ensure that the EventDetailsView shows the correct data
            _eventViewModel.ResetTempData();

            string titel = _eventViewModel.CanEdit ? "Eintrag bearbeiten" : "Eintrag ansehen";

            _popup = new PopupWindow()
            {
                MinWidth = 400,
                Title = titel,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ShowInTaskbar = false,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None
            };
            // Binds the DataContext of the PopupWindow to the EventViewModel
            EventDetailsView eventDetailsView = new EventDetailsView()
            {
                DataContext = _eventViewModel
            };
            _popup.PopupElement = eventDetailsView;
            _popup.ShowDialog();
        }
    }
}
