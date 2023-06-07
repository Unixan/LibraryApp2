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
    public RelayCommand CloseCommand => new RelayCommand(execute => CloseWindow(_window));


    private Window _window;
    public ObservableCollection<Book?> Books { get; set; }

    private Book? _selectedBook;
    public Book? SelectedBook
    {
        get { return _selectedBook; }
        set
        {
            _selectedBook = value;
            OnPropertyChanged();
        }
    }
    public BooksWindowViewModel(Window window, ObservableCollection<Book?> books)
    {
        _window = window;
        Books = books;
    }
    private void OpenBookDetailsWindow()
    {
        var bookDetailsWindow = new BookDetailsWindow(_window, SelectedBook);
        _window.Opacity = 0;
        bookDetailsWindow.ShowDialog();
        _window.Opacity = 1;
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
        var addBookWindow = new AddBookWindow(_window, Books);
        _window.Opacity = 0;
        addBookWindow.ShowDialog();
        _window.Opacity = 1;
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