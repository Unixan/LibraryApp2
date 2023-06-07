using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using LibraryApp.Model;
using LibraryApp.ViewModel;

namespace LibraryApp.View
{
    public partial class UserBooksWindow : Window
    {
        private Window _window;
        private ObservableCollection<UserBookItem> _loanedBooks;
        private User _user;
        private ObservableCollection<Book?> _library;
        
        public UserBooksWindow(Window window, User user, ObservableCollection<Book?> library)
        {
            Owner = window;
            _window = window;
            _loanedBooks = user.LoanedBooks;
            _user = user;
            _library = library;
            InitializeComponent();
            var vm = new UserBooksWindowViewModel(this, _user, _loanedBooks, _library );
            DataContext = vm;
            vm.ReloadRequested += vm_ReloadRequested;
        }
        private void vm_ReloadRequested(object? sender, EventArgs e)
        {
            ReloadWindow();
        }

        private void ReloadWindow()
        {
            UserBooksWindow newWindow = new UserBooksWindow(Owner, _user, _library);
            Close();
            newWindow.ShowDialog();
        }
    }
}
