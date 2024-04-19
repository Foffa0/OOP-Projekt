namespace OOP_LernDashboard.ViewModels
{
    internal class DemoViewModel : ViewModelBase
    {
        private string? _textBoxInput;
        public string? TextBoxInput 
        { 
            get { return _textBoxInput; }
            set
            {
                _textBoxInput = value;
                OnPropertyChanged(nameof(TextBoxInput));
            }

        }

        public DemoViewModel()
        {

        }

        public static DemoViewModel LoadViewModel()
        {
            DemoViewModel viewModel = new DemoViewModel();
            return viewModel;
        }

    }
}
