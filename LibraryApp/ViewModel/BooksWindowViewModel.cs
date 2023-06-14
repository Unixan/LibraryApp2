using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class BooksWindowViewModel : ViewModelBase
{
    public RelayCommand AddBookCommand => new RelayCommand(execute => AddBook());
    public RelayCommand DeleteBookCommand => new RelayCommand(execute => DeleteSelectedBook(), canExecute => SelectedBook != null);
    public RelayCommand BookDetailsCommand => new RelayCommand(execute => OpenBookDetailsWindow(), canExecute => SelectedBook != null);
    public RelayCommand CloseCommand => new RelayCommand(execute => CloseWindow(_ownerWindow));


    private Window _ownerWindow;
    public ObservableCollection<Book?>? Books
    {
        get { return LibraryService.Books; }
        set
        {
            LibraryService.Books = value;
            OnPropertyChanged();
        }
    }
    public Book? SelectedBook
    {
        get { return LibraryService.SelectedBook; }
        set
        {
            LibraryService.SelectedBook = value;
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
}