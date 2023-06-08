using System;
using LibraryApp.Model;
using LibraryApp.ViewModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class LoanCardWindow : Window
    {
        public LoanCardWindow(Window ownerWindow)
        {
            Owner = ownerWindow;
            InitializeComponent();
            var LoanCardWindowViewModel = new LoanCardWindowViewModel(this);
            DataContext = LoanCardWindowViewModel;
            LoanCardWindowViewModel.ReloadRequested += vm_ReloadRequested;
        }

        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }

        private void ReloadWindow()
        {
            LoanCardWindow newWindow = new LoanCardWindow(this);
            Close();
            newWindow.ShowDialog();
        }
    }
}
