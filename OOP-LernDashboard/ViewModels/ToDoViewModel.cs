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
        public string NextDate { get; set; }        

        public ICommand DeleteToDoCommand { get; set; }
        public ICommand CheckToDoCommand { get; set; }

        public ToDoViewModel(ToDo toDo, DashboardStore dashboardStore)
        {
            _toDo = toDo;
            DeleteToDoCommand = new DeleteToDoCommand(this, dashboardStore);
            CheckToDoCommand =new CheckToDoCommand(this,dashboardStore);
            NextDate = (_toDo as RecurringToDo)?.NextDate != null
            ? ToDateText((DateTime)(_toDo as RecurringToDo).NextDate)
            : "";            
        }
        private string ToDateText(DateTime date)
        {
            var timeSpan = date - DateTime.Now;           
            if (timeSpan.TotalDays < 1)
            {
                if (timeSpan.TotalHours < 1)
                {
                    return "In weniger als einer Stunde";
                }
                return $"In {Math.Floor(timeSpan.TotalHours)} Stunden";
            }

            if (timeSpan.TotalDays < 2)
            {
                return "Morgen";
            }

            if (timeSpan.TotalDays < 30)
            {
                return $"In {Math.Floor(timeSpan.TotalDays)} Tagen";
            }

            if (timeSpan.TotalDays < 365)
            {
                int months = (int)(timeSpan.TotalDays / 30);
                return $"In {months} Monaten";
            }

            int years = (int)(timeSpan.TotalDays / 365);
            return $"In {years} Jahren";
        }
    }
}

