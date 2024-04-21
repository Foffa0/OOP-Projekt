using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Stores
{
    /// <summary>
    /// Stores the application state (and caches it locally)
    /// </summary>
    class DashboardStore
    {
        private readonly List<ToDo> _toDos;
        public IEnumerable<ToDo> ToDos => _toDos;


        public DashboardStore()
        {
            _toDos = new List<ToDo>();
        }
    }
}
