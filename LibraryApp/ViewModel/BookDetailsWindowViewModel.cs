using System.Linq;
using System.Windows;
using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;

namespace LibraryApp.ViewModel;

public class BookDetailsWindowViewModel : ViewModelBase
{
    public RelayCommand CloseCommand => new(execute => ResetAndClose(_ownerWindow));
    private Window _ownerWindow;
    public string LoanedTo { get; set; }

    public Book? SelectedBook
    {
        get { return LibraryService.SelectedBook; }
    }
    public BookDetailsWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
        if (!SelectedBook.IsAvailable)
        {
            SetLoanedTo();
        }
        else LoanedTo = SelectedBook.Availability;
    }

    private void SetLoanedTo()
    {
        var user = LibraryService.Users.FirstOrDefault(User => User.UserID == SelectedBook.LoanedToId);
        LoanedTo = $"Lånt ut til {user.LastName}, {user.FirstName}";
    }
}