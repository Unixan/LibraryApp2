using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LibraryApp.ViewModel;

public class BooksWindowViewModel : ViewModelBase
{
    public RelayCommand AddBookCommand => new RelayCommand(execute => AddBook());
    public RelayCommand DeleteBookCommand => new RelayCommand(execute => DeleteSelectedBook(), canExecute => SelectedBook != null);
    public RelayCommand BookDetailsCommand => new RelayCommand(execute => OpenBookDetailsWindow(), canExecute => SelectedBook != null);
    public RelayCommand CloseCommand => new RelayCommand(execute => CloseWindow(_ownerWindow));


    private Window _ownerWindow;
    public ObservableCollection<Book?> Books
    {
        get { return App.LibraryService.Books; }
        set
        {
            App.LibraryService.Books = value;
            OnPropertyChanged();
        }
    }
    public Book? SelectedBook
    {
        get { return App.LibraryService.SelectedBook; }
        set
        {
            App.LibraryService.SelectedBook = value;
            OnPropertyChanged();
        }
    }
    public BooksWindowViewModel(Window window)
    {
        _ownerWindow = window;
    }
    private void OpenBookDetailsWindow()
    {
        var bookDetailsWindow = new BookDetailsWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        bookDetailsWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }
    private void DeleteSelectedBook()
    {
        var choice = MessageBox.Show("Er du sikker?", $"Slette {SelectedBook?.Title} permanent?", MessageBoxButton.YesNo);
        if (choice != MessageBoxResult.Yes) return;
        if (!SelectedBook.IsAvailable) ResetBookStatus();
        Books?.Remove(SelectedBook);
        SelectedBook = null;
    }
    private void AddBook()
    {
        var addBookWindow = new AddBookWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        addBookWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }

    private void ResetBookStatus()
    {
        var userWithBook = SelectedBook?.LoanedTo;
        var userBookListIndex = -1;
        foreach (var userBookItem in from userBookItem in userWithBook?.LoanedBooks
                                     where SelectedBook == userBookItem.Book
                                     select userBookItem)
        {
            userBookListIndex = userWithBook!.LoanedBooks.IndexOf(userBookItem);
        }

        if (userBookListIndex >= 0) userWithBook?.LoanedBooks.RemoveAt(userBookListIndex);
    }
}