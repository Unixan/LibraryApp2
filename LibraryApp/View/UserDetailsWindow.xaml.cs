using System;
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
            vm.ReloadRequested += vm_ReloadRequested;
        }
        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }

        private void ReloadWindow()
        {
            UserDetailsWindow newWindow = new UserDetailsWindow(_ownerWindow);
            Close();
            newWindow.ShowDialog();
        }
    }
}
