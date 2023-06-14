using System.Windows;
using LibraryApp.ViewModel;
using System;
using LibraryApp.CommonLibrary;

namespace LibraryApp.View
{
    public partial class UsersWindow : Window
    {
        public UsersWindow(Window mainWindow)
        {
            InitializeComponent();
            Owner = mainWindow;
            var vm = new UsersWindowViewModel(this);
            DataContext = vm;
            vm.ReloadRequested += vm_ReloadRequested;

        }
        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }
        private void ReloadWindow()
        {
            var newWindow = new UsersWindow(Owner);
            Close();
            newWindow.ShowDialog();
        }
        
    }
}
