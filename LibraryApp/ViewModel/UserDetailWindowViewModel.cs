using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UserDetailWindowViewModel : ViewModelBase
{
    private User _user;
    public ObservableCollection<UserBookItem> Books { get; set; }
    private Book _book;
    private Window _window;
    private ObservableCollection<Book?> _library;
    private string _loanCardStatus;

    public string LoanCardStatus
    {
        get { return _loanCardStatus; }
        set
        {
            _loanCardStatus = value;
            OnPropertyChanged();
        }
    }

    public Book Book
    {
        get { return _book; }
        set
        {
            _book = value;
            OnPropertyChanged();
        }
    }
    public User User
    {
        get { return _user; }
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand EditBooksCommand => new RelayCommand(execute => EditBooks());
    public RelayCommand LoanCardCommand => new RelayCommand(execute => EditLoanCard());
    public RelayCommand CloseCommand => new RelayCommand(execute => CloseWindow(_window));
    public UserDetailWindowViewModel(Window window, User user, ObservableCollection<Book?> library)
    {
        _window = window;
        _user = user;
        _library = library;
        _loanCardStatus = _user.LoanCardStatus;
        Books = User.LoanedBooks;
    }


    private void EditLoanCard()
    {
        var LoanCardWindow = new LoanCardWindow(_window, _user);
        _window.Opacity = 0;
        LoanCardWindow.ShowDialog();
        _window.Opacity = 1;
        LoanCardStatus = _user.LoanCardStatus;
    }

    private void EditBooks()
    {
        var editBooksWindow = new UserBooksWindow(_window, _user, _library);
        editBooksWindow.ShowDialog();
        Books = _user.LoanedBooks;
        OnPropertyChanged(nameof(Books));
    }
}