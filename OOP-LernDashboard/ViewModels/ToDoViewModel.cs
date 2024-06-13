using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class ToDoViewModel : ViewModelBase
    {
        private readonly ToDo _toDo;
        public ToDo ToDo => _toDo;

        public string? Description => _toDo.Description;
        public Boolean IsChecked
        {
            get => _toDo.IsChecked;
            set
            {
                if (value)
                {
                    ToDo.check();
                }
            }
        }
        private string _dateText;
        public string DateText
        {
            get => _dateText;
            set
            {
                _dateText = value;
                OnPropertyChanged(nameof(DateText));
            }
        }

        public ICommand DeleteToDoCommand { get; set; }
        public ICommand CheckToDoCommand { get; set; }

        public ToDoViewModel(ToDo toDo, DashboardStore dashboardStore)
        {
            _toDo = toDo;
            DeleteToDoCommand = new DeleteToDoCommand(this, dashboardStore);
            CheckToDoCommand = new CheckToDoCommand(this, dashboardStore);
            DateText = (_toDo is RecurringToDo)
            ? ToDateText(_toDo as RecurringToDo)
            : "";
        }
        public void UpdateDateText()
        {
            DateText = (_toDo is RecurringToDo)
            ? ToDateText(_toDo as RecurringToDo)
            : "";
        }
        private string ToDateText(RecurringToDo reToDo)
        {
            if (reToDo.IsChecked)
                return "bereits erledigt";

            TimeSpan timeSpan = reToDo.StartTime + reToDo.TimeInterval - DateTime.Now ?? TimeSpan.Zero;
            if (timeSpan < TimeSpan.FromHours(1))
            {
                return "in " + timeSpan.Minutes + " Minuten";
            }
            if (timeSpan < TimeSpan.FromDays(1))
            {
                return "in " + timeSpan.Hours + " Stunden";
            }
            if (timeSpan < TimeSpan.FromDays(30))
            {
                return "in " + timeSpan.Days + " Tagen";
            }
            if (timeSpan < TimeSpan.FromDays(365))
            {
                return "in " + Math.Floor((double)(timeSpan.Days / 30)) + " Monaten";
            }
            return "";
        }
    }
}

