namespace OOP_LernDashboard.Models
{
    class ToDo
    {
        public Guid Id { get; }
        public string? Description { get; }
        public Boolean IsChecked { get; protected set; }


        public ToDo(string description, bool isChecked = false)
        {
            Id = Guid.NewGuid();
            this.Description = description;
            this.IsChecked = isChecked;

        }
        public ToDo(Guid id, string description, bool isChecked = false)
        {
            Id = id;
            this.Description = description;
            this.IsChecked = isChecked;
        }


        public virtual void check()
        {
            IsChecked = true;
        }
    }
}
