using LibraryApp.ViewModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class AddBookWindow : Window
    {
        
        private Window _window;
        
        public AddBookWindow(Window window)
        {
            _window = window;
            Owner = _window;
            InitializeComponent();
            var addBookWindowViewModel = new AddBookWindowViewModel(this);
            DataContext = addBookWindowViewModel;
        }
        
    }
}
