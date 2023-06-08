using LibraryApp.ViewModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class AddUserWindow : Window
    {
        private Window Window;
        public AddUserWindow(Window window)
        {
            Window = window;
            Owner = Window;
            InitializeComponent();
            var vm = new AddUserWindowViewModel(this);
            DataContext = vm;
        }
    }
}
