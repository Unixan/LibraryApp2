using System.Collections.ObjectModel;
using System.Windows;
using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;

namespace LibraryApp.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    public RelayCommand BooksCommand => new(execute => OpenBooksWindow());
    public RelayCommand UsersCommand => new(execute => OpenUsersWindow());
    public RelayCommand ExitCommand => new(execute => CloseWindow(_ownerWindow));
    private readonly Window _ownerWindow;
    protected ObservableCollection<Book?> Books;
    protected ObservableCollection<User?> Users;

    public MainWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
        Books = LibraryService.Books;
        Users = LibraryService.Users;
    }
    private void OpenBooksWindow()
    {
        var booksWindow = new BooksWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        booksWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }
    private void OpenUsersWindow()
    {
        var usersWindow = new UsersWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        usersWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }
}