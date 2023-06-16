using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UserBooksWindowViewModel : ViewModelBase
{
    public RelayCommand SaveCommand => new RelayCommand(execute => SaveChanges(), canExecute => _changesMade);
    public RelayCommand CancelCommand => new RelayCommand(execute => AreYouSure());
    public RelayCommand LoanBookCommand => new RelayCommand(execute => LoanBook(), canExecute => SelectedLibraryBook != null);
    public RelayCommand ReturnBookCommand => new RelayCommand(execute => ReturnBook(), canExecute => SelectedLoanedBook != null);
    private Window _ownerWindow;
    public ObservableCollection<Book> TempLoanedBooks { get; private set; }
    public ObservableCollection<Book> TempLibraryBooks { get; private set; }
    private bool _changesMade;
    private Book _selectedLibraryBook;
    public Book SelectedLibraryBook
    {
        get { return _selectedLibraryBook; }
        set
        {
            _selectedLibraryBook  = value; 
            OnPropertyChanged();
        }
    }
    private Book _selectedLoanedBook;

    public Book SelectedLoanedBook
    {
        get { return _selectedLoanedBook; }
        set
        {
            _selectedLoanedBook = value;
            OnPropertyChanged();
        }
    }

    public UserBooksWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
        TempLibraryBooks = new ObservableCollection<Book>(GetAvailableLibraryBooks());
        TempLoanedBooks = new ObservableCollection<Book>(GetUserLoanedBooks());
    }
    private IEnumerable<Book?> GetAvailableLibraryBooks()
    {
        return LibraryService.Books.Where(book => book.IsAvailable);
    }
    private IEnumerable<Book?> GetUserLoanedBooks()
    {
        return LibraryService.Books.Where(book => book.LoanedToId == LibraryService.SelectedUser.UserID);
    }
    private void LoanBook()
    {
        if (LibraryService.SelectedUser.LoanCard == "Ingen")
        {
            MessageBox.Show("Brukeren har ikke lånekort!");
            return;
        }
        SelectedLibraryBook.LoanedOnDate = App.GetTodaysDate();
        TempLoanedBooks.Add(SelectedLibraryBook);
        TempLibraryBooks.Remove(SelectedLibraryBook);
        BookListsChanged();
        _changesMade = true;
    }
    private void ReturnBook()
    {
        SelectedLoanedBook.LoanedOnDate = null;
        TempLibraryBooks.Add(SelectedLoanedBook);
        TempLoanedBooks.Remove(SelectedLoanedBook);
        BookListsChanged();
        _changesMade = true;
    }
    private void BookListsChanged()
    {
        OnPropertyChanged(nameof(TempLoanedBooks));
        OnPropertyChanged(nameof(TempLibraryBooks));
    }
    private void AreYouSure()
    {
        if (_changesMade)
        {
            var choice = MessageBox.Show("Er du sikker? Endringer vil bli forkastet", "Ikke lagret",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (choice == MessageBoxResult.Yes)
            {
                CloseWindow(_ownerWindow);
            }
            else return;
        }
        CloseWindow(_ownerWindow);
    }
    private async void SaveChanges()
    {
        foreach (var book in LibraryService.Books)
        {
            if (book.LoanedToId == LibraryService.SelectedUser.UserID) book.LoanedToId = null;
        }
        foreach (var tempLoanedBook in TempLoanedBooks)
        {
            tempLoanedBook.LoanedToId = LibraryService.SelectedUser.UserID;
        }
        _changesMade = false;
        await UpdateBookList();
        MessageBox.Show("Endringene ble lagret");
    }

    private async Task UpdateBookList()
    {
        foreach (var book in LibraryService.Books)
        {
            await LibraryService.UpdateLoanedStatus(book);
        }
    }
}