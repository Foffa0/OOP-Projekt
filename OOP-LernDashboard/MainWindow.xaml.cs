using System.Windows.Controls;
using System.Windows.Input;

namespace OOP_LernDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox textBox = new TextBox();
            if (FocusManager.GetFocusedElement(this) != null)
            {
                if (FocusManager.GetFocusedElement(this).GetType() == textBox.GetType())
                {
                    Keyboard.ClearFocus();
                }
            }
        }
    }
}