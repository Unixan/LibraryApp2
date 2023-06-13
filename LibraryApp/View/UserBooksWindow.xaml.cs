using LibraryApp.ViewModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class UserBooksWindow : Window
    {
        private Window _ownerWindow;
        public UserBooksWindow(Window window)
        {
            Owner = window;
            _ownerWindow = window;
            InitializeComponent();
            var vm = new UserBooksWindowViewModel(this);
            DataContext = vm;
        }
    }
}
