using System;
using System.Collections.ObjectModel;
using System.Windows;
using LibraryApp.Model;
using LibraryApp.ViewModel;

namespace LibraryApp.View
{
    public partial class UserDetailsWindow : Window
    {
    private User _user;
    private ObservableCollection<Book?> _library;
    public UserDetailsWindow(Window owner, User user, ObservableCollection<Book?> library)
        {
            _library = library;
            _user = user;
            Owner = owner;
            InitializeComponent();
            var vm = new UserDetailWindowViewModel(this, _user, _library);
            DataContext = vm;
            vm.ReloadRequested += vm_ReloadRequested;
        }
        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }

        private void ReloadWindow()
        {
            UserDetailsWindow newWindow = new UserDetailsWindow(Owner, _user, _library);
            Close();
            newWindow.ShowDialog();
        }
    }
}
