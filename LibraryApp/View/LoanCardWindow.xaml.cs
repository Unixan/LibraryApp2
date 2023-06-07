using System;
using LibraryApp.Model;
using LibraryApp.ViewModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class LoanCardWindow : Window
    {
        private User _user;
        public LoanCardWindow(Window owner, User user)
        {
            Owner = owner;
            _user = user;
            InitializeComponent();
            var vm = new LoanCardWindowViewModel(this, user);
            DataContext = vm;
            vm.ReloadRequested += vm_ReloadRequested;
        }

        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }

        private void ReloadWindow()
        {
            LoanCardWindow newWindow = new LoanCardWindow(Owner, _user);
            Close();
            newWindow.ShowDialog();
        }
    }
}
