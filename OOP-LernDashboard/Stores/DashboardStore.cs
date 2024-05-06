using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Services.DataCreators;
using OOP_LernDashboard.Services.DataProviders;

namespace OOP_LernDashboard.Stores
{
    /// <summary>
    /// Stores the application state (and caches it locally).
    /// This avoides to call the datase every single time we need some data.
    /// </summary>
    class DashboardStore
    {
        private Lazy<Task> _initializeLazy;
        private IDataCreator<ToDo> _toDoCreator;
        private IDataProvider<ToDo> _toDoProvider;

        private readonly Models.LinkedList<ToDo> _toDos;
        public IEnumerable<ToDo> ToDos => _toDos;

        public event Action<ToDo> ToDoCreated;
        public event Action<ToDo> ToDoDeleted;



        public DashboardStore(IDataCreator<ToDo> toDoDataCreator, IDataProvider<ToDo> toDoDataProvider)
        {
            _toDoCreator = toDoDataCreator;
            _toDoProvider = toDoDataProvider;
            // the lazy ensures to only load the data once from the database
            _initializeLazy = new Lazy<Task>(Initialize);

            _toDos = new Models.LinkedList<ToDo>();
        }

        /// <summary>
        /// Initializes the data from the database when it is called the first time
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }


        /// <summary>
        /// Adds a ToDo to the database and updates the ToDo-List
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public async Task AddToDo(ToDo toDo)
        {
            await _toDoCreator.CreateModel(toDo);
            _toDos.Add(toDo);
            ToDoCreated?.Invoke(toDo);
        }

        /// <summary>
        /// Removes a ToDo from the database and updates the ToDo-List
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        public async Task DeleteToDo(ToDo toDo)
        {
            await _toDoCreator.DeleteModel(toDo);
            _toDos.Remove(_toDos.Where(i => i.Id == toDo.Id).Single());
            ToDoDeleted?.Invoke(toDo);
        }



        // Loads the data from the database once
        private async Task Initialize()
        {
            IEnumerable<ToDo> toDos = await _toDoProvider.GetAllModels();
            _toDos.Clear();
            foreach (var toDo in toDos)
            {
                _toDos.Add(toDo);
            }
        }

    }
}
