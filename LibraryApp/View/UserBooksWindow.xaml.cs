using LibraryApp.ViewModel;
using System;
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
            vm.ReloadRequested += vm_ReloadRequested;
        }
        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }

        private void ReloadWindow()
        {
            UserBooksWindow newWindow = new UserBooksWindow(this);
            Close();
            newWindow.ShowDialog();
        }
    }
}
