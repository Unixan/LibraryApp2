using System.Collections.ObjectModel;
using LibraryApp.Model;
using System.Windows;
using LibraryApp.ViewModel;
using System;

namespace LibraryApp.View
{
    public partial class UsersWindow : Window
    {
        private ObservableCollection<Book?> _library;
        private ObservableCollection<User> _users;
        public UsersWindow(Window mainWindow, ObservableCollection<Book?> library, ObservableCollection<User> users)
        {
            _users = users;
            InitializeComponent();
            _library = library;
            Owner = mainWindow;
            var vm = new UsersWindowViewModel(this, _users, _library);
            DataContext = vm;
            vm.ReloadRequested += vm_ReloadRequested;

        }
        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }
        private void ReloadWindow()
        {
            var newWindow = new UsersWindow(Owner, _library, _users);
            Close();
            newWindow.ShowDialog();
        }
    }
}
