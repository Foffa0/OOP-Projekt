namespace OOP_LernDashboard.Models
{
    class NormalToDo : ToDo
    {
        public override void check()
        {
            if (IsChecked)
                throw new InvalidOperationException("Cannot check already checked ToDo");

            this.IsChecked = true;
        }
        public NormalToDo(string description) : base(description)
        {

        }
    }
}
