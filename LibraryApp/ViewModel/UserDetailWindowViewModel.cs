using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UserDetailWindowViewModel : ViewModelBase
{
    public ObservableCollection<Book> Books { get; set; }

    private Window _ownerWindow;
    public User? User
    {
        get { return LibraryService.SelectedUser; }
        set
        {
            LibraryService.SelectedUser = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand EditBooksCommand => new RelayCommand(execute => EditBooks());
    public RelayCommand LoanCardCommand => new RelayCommand(execute => EditLoanCard());
    public RelayCommand CloseCommand => new RelayCommand(execute => ResetAndClose(_ownerWindow));
    public string LoanCard
    {
        get { return LibraryService.SelectedUser.LoanCard; }
        set
        {
            LibraryService.SelectedUser.LoanCard = value;
            OnPropertyChanged();
        }
    }
    public UserDetailWindowViewModel(Window window)
    {
        Books = new ObservableCollection<Book>();
        _ownerWindow = window;
    }

    private void GetUserBooks()
    {
        Books.Clear();
        foreach (var book in LibraryService.Books)
        {
            if (book.LoanedToId == LibraryService.SelectedUser.UserID) Books.Add(book);
        }
    }


    private void EditLoanCard()
    {
        var loanCardWindow = new LoanCardWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        loanCardWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
        OnPropertyChanged(nameof(LoanCard));
    }

    private void EditBooks()
    {
        var editBooksWindow = new UserBooksWindow(_ownerWindow);
        editBooksWindow.ShowDialog();
        GetUserBooks();
        OnPropertyChanged(nameof(Books));
    }
}