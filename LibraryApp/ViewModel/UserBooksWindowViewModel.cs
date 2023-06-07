using System;
using LibraryApp.Model;
using LibraryApp.MVVM;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UserBooksWindowViewModel : ViewModelBase
{
    public RelayCommand SaveCommand => new RelayCommand(execute => SaveChanges(), canExecute => _changesMade);
    public RelayCommand CancelCommand => new RelayCommand(execute => AreYouSure());
    public RelayCommand LoanBookCommand => new RelayCommand(execute => SelectBookToLoan(), canExecute => SelectedLibraryBook != null);
    public RelayCommand ReturnBookCommand => new RelayCommand(execute => SelectBookToReturn(), canExecute => SelectedLoanedBook != null);
    private Window _window;
    public ObservableCollection<UserBookItem> LoanedBooks { get; set; }
    public ObservableCollection<UserBookItem> TempLoanedBooks { get; set; }
    public ObservableCollection<Book?> LibraryBooks { get; set; }
    public ObservableCollection<Book?> TempAvailableLibraryBooks { get; set; }
    private bool _changesMade = false;
    private string _userName;
    public string UserName
    {
        get { return _userName; }
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }

    private Book _selectedLibraryBook;

    public Book? SelectedLibraryBook
    {
        get
        {
            return _selectedLibraryBook;
        }
        set
        {
            _selectedLibraryBook = value;
            OnPropertyChanged();
        }
    }
    private UserBookItem _selectedLoanedBook;
    private User _user;

    public UserBookItem? SelectedLoanedBook
    {
        get
        {
            return _selectedLoanedBook;
        }
        set
        {
            _selectedLoanedBook = value;
            OnPropertyChanged();
        }
    }

    public UserBooksWindowViewModel(Window window, User user, ObservableCollection<UserBookItem> loanedBooks, ObservableCollection<Book?> libraryBooks)
    {
        _window = window;
        _user = user;
        _userName = user.FullName;
        LoanedBooks = loanedBooks;
        LibraryBooks = libraryBooks;
        TempLoanedBooks = new ObservableCollection<UserBookItem>(LoanedBooks);
        TempAvailableLibraryBooks = GetAvailableBooks();
    }
    private void SelectBookToLoan()
    {
        if (_user.LoanCard != null)
        {
            TempLoanedBooks.Add(new UserBookItem(SelectedLibraryBook));
            TempAvailableLibraryBooks.Remove(SelectedLibraryBook);
            BookListsChanged();
            _changesMade = true;
        }
        else
        {
            MessageBox.Show("Brukeren har ikke lånekort");
        }
    }
    private void SelectBookToReturn()
    {
        TempAvailableLibraryBooks.Add(SelectedLoanedBook.Book);
        TempLoanedBooks.Remove(SelectedLoanedBook);
        BookListsChanged();
        _changesMade = true;
    }
    private void SaveChanges()
    {
        _user.SetLoanedBooks(TempLoanedBooks);
        SetLibraryProperties();
        MessageBox.Show("Endringene er lagret!",String.Empty,MessageBoxButton.OK,MessageBoxImage.Exclamation);
        _changesMade = false;
    }

    private void SetLibraryProperties()
    {
        foreach (var libraryBook in LibraryBooks)
        {
            if (libraryBook.LoanedTo == _user) libraryBook.LoanedTo = null;
        }
        foreach (var booklisting in TempLoanedBooks)
        {
            var book = booklisting.Book;
            if (book.LoanedTo != _user) book.LoanedTo = _user;
            
        }
    }

    private ObservableCollection<Book?> GetAvailableBooks()
    {
        return new ObservableCollection<Book?>(LibraryBooks.Where(book => book.IsAvailable));
    }

    private void BookListsChanged()
    {
        OnPropertyChanged(nameof(TempAvailableLibraryBooks));
        OnPropertyChanged(nameof(TempLoanedBooks));
    }
    private void AreYouSure()
    {
        if (_changesMade)
        {
            var choice = MessageBox.Show("Er du sikker? Alle endringer vil bli forkastet.", "Endringer er ikke lagret",MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (choice == MessageBoxResult.OK)
            {
                CloseWindow(_window);
            }
        }
        else { CloseWindow(_window);}
    }
}