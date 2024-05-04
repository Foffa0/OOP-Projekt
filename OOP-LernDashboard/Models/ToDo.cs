namespace OOP_LernDashboard.Models
{
    class ToDo
    {
        public string? Description { get; }
        public Boolean IsChecked { get; private set; }

        public ToDo(string description, bool isChecked = false)
        {
            this.Description = description;
            this.IsChecked = isChecked;
        }

        public void check()
        {
            if (IsChecked)
                throw new InvalidOperationException("Cannot check already checked ToDo");

            this.IsChecked = true;
        }
    }
}
