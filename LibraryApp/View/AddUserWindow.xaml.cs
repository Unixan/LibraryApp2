using LibraryApp.Model;
using LibraryApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class AddUserWindow : Window
    {
        private Window Window;
        public ObservableCollection<User> Users { get; set; }
        public AddUserWindow(Window window, ObservableCollection<User> users)
        {
            Users = users;
            Window = window;
            Owner = Window;
            var vm = new AddUserWindowViewModel(this, Users);
            DataContext = vm;
            InitializeComponent();
        }
    }
}
