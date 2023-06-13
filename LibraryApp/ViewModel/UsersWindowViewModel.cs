using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;
using LibraryApp.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class UsersWindowViewModel : ViewModelBase
{
    private Window _ownerWindow;
    public ObservableCollection<User?> Users
    {
        get { return LibraryService.Users;}
        set
        {
            LibraryService.Users = value;
            OnPropertyChanged();
        }
    }
    public User? SelectedUser
    {
        get { return LibraryService.SelectedUser; }
        set
        {
            LibraryService.SelectedUser = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteUser(), canExecute => SelectedUser != null);
    public RelayCommand DetailsCommand => new RelayCommand(execute => ShowUserDetails(), canExecute => SelectedUser != null);
    public RelayCommand AddUserCommand => new RelayCommand(execute => AddUser());
    public RelayCommand CloseWindowCommand => new RelayCommand(execute => CloseWindow(_ownerWindow));



    public UsersWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
    }

    private void AddUser()
    {
        var addUserWindow = new AddUserWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        addUserWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }

    private void ShowUserDetails()
    {
        var userDetailWindow = new UserDetailsWindow(_ownerWindow);
        _ownerWindow.Opacity = 0;
        userDetailWindow.ShowDialog();
        ReloadWindow();
        _ownerWindow.Opacity = 1;
       }

    private void DeleteUser()
    {
        var choice = MessageBox.Show("Er du sikker? Brukeren vil bli slettet for godt",
            $"Slette {SelectedUser.LastName}, {SelectedUser.FirstName}?",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);
        if (choice != MessageBoxResult.Yes) return;
        foreach (var book in LibraryService.Books)
        {
            if (SelectedUser.UserID != book.LoanedToId) continue;
            book.LoanedToId = null;
            book.LoanedOnDate = null;
        }
        Users.Remove(SelectedUser);
    }

    
}