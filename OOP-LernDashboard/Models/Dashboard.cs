namespace OOP_LernDashboard.Models
{
    class Dashboard
    {
        public LinkedList<ToDo> ToDoList { get; set; }

        public Dashboard()
        {
            this.ToDoList = new LinkedList<ToDo>
            {
                new("Mathe"),
                new("Backup"),
                new("Memes")
            };

            foreach (ToDo todo in ToDoList)
            {
                Console.WriteLine(todo.ToString());
            }
        }
    }
}
