namespace OOP_LernDashboard.Models
{
    class Dashboard
    {
        public LinkedList<ToDo> ToDoList { get; set; }

        public Dashboard()
        {
            this.ToDoList = new LinkedList<ToDo>
            {
                new("1"),
                new("2"),
                new("3")
            };

            foreach (ToDo todo in ToDoList)
            {
                Console.WriteLine(todo.ToString());
            }
        }
    }
}
