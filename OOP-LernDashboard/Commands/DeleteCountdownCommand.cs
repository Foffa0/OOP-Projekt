using HandyControl.Data;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    class DeleteCountdownCommand : CommandBase
    {
        private readonly DashboardStore _dashboardStore;

        public DeleteCountdownCommand(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show(new HandyControl.Data.MessageBoxInfo
            {
                Message = $"Möchtest du diesen Contdown ({((CountdownViewModel)parameter).Description}) wirklich löschen?",
                Button = MessageBoxButton.YesNo,
                ConfirmContent = "Ja",
                NoContent = "Nein",
                IconKey = ResourceToken.AskGeometry,
                IconBrushKey = "PrimaryBrush",
            });
            if (result == MessageBoxResult.Yes && parameter != null)
            {
                _ = _dashboardStore.DeleteCountdown(_dashboardStore.Countdowns.Where(i => i.Id == ((CountdownViewModel)parameter).Id).Single());
            }
        }
    }
}
