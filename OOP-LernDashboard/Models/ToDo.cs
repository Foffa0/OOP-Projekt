namespace OOP_LernDashboard.Models
{
    class ToDo
    {
        public string? Description { get; set; }
        public Boolean? IsChecked { get; set; }

        public ToDo(string description, bool isChecked = false)
        {
            this.Description = description;
            this.IsChecked = isChecked;
        }
    }
}
