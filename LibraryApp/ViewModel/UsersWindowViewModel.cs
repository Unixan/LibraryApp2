using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UsersWindowViewModel : ViewModelBase
{
    private Window _window;
    public ObservableCollection<User> Users { get; set; }

    private ObservableCollection<Book?> _library;

    private User _currentUser;

    public User CurrentUser
    {
        get { return _currentUser; }
        set
        {
            _currentUser = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteUser(), canExecute => CurrentUser != null);
    public RelayCommand DetailsCommand => new RelayCommand(execute => ShowUserDetails(), canExecute => CurrentUser != null);
    public RelayCommand AddUserCommand => new RelayCommand(execute => AddUser());
    public RelayCommand CloseWindowCommand => new RelayCommand(execute => CloseWindow(_window));



    public UsersWindowViewModel(Window window, ObservableCollection<User> users, ObservableCollection<Book?> library)
    {
        _window = window;
        Users = users;
        _library = library;
    }

    private void AddUser()
    {
        var addUserWindow = new AddUserWindow(_window, Users);
        _window.Opacity = 0;
        addUserWindow.ShowDialog();
        _window.Opacity = 1;
    }

    private void ShowUserDetails()
    {
        var userDetailWindow = new UserDetailsWindow(_window, CurrentUser, _library);
        _window.Opacity = 0;
        userDetailWindow.ShowDialog();
        _window.Opacity = 1;
        ReloadWindow();
    }

    private void DeleteUser()
    {
        var choice = MessageBox.Show("Er du sikker? Brukeren vil bli slettet for godt",
            $"Slette {CurrentUser.LastName}, {CurrentUser.FirstName}?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (choice != MessageBoxResult.Yes) return;
        RemoveCurrentBooksFromUser();
        Users.Remove(CurrentUser);
    }

    private void RemoveCurrentBooksFromUser()
    {
        if (CurrentUser.LoanedBooks.Count <= 0) return;
        foreach (var booklisting in CurrentUser.LoanedBooks)
        {
            booklisting.Book.LoanedTo = null;
        }
    }
}