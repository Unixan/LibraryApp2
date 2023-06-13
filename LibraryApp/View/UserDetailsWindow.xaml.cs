using System.Windows;
using LibraryApp.ViewModel;

namespace LibraryApp.View
{
    public partial class UserDetailsWindow : Window
    {
        private Window _ownerWindow;

        public UserDetailsWindow(Window ownerWindow)
        {
            _ownerWindow = ownerWindow;
            Owner = ownerWindow;
            InitializeComponent();
            var vm = new UserDetailWindowViewModel(this);
            DataContext = vm;
          
        }
    }
}
