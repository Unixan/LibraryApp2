using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UserDetailWindowViewModel : ViewModelBase
{
    public ObservableCollection<UserBookItem> Books
    {
        get { return User.LoanedBooks; }
    }

    private Window _ownerWindow;
    public string LoanCardStatus
    {
        get { return App.LibraryService.SelectedUser.LoanCardStatus; }
        
    }
    public User? User
    {
        get { return App.LibraryService?.SelectedUser; }
        set
        {
            App.LibraryService.SelectedUser = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand EditBooksCommand => new RelayCommand(execute => EditBooks());
    public RelayCommand LoanCardCommand => new RelayCommand(execute => EditLoanCard());
    public RelayCommand CloseCommand => new RelayCommand(execute => ResetAndClose(_ownerWindow));
    public UserDetailWindowViewModel(Window window)
    {
        _ownerWindow = window;
     }


    private void EditLoanCard()
    {
        var loanCardWindow = new LoanCardWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        loanCardWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
        OnPropertyChanged(nameof(LoanCardStatus));
    }

    private void EditBooks()
    {
        var editBooksWindow = new UserBooksWindow(_ownerWindow);
        editBooksWindow.ShowDialog();
        OnPropertyChanged(nameof(Books));
    }
}