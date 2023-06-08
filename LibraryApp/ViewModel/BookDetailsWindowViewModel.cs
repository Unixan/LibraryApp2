using System.Windows;
using LibraryApp.Model;
using LibraryApp.MVVM;

namespace LibraryApp.ViewModel;

public class BookDetailsWindowViewModel : ViewModelBase
{
    public RelayCommand CloseCommand => new(execute => ResetAndClose(_ownerWindow));
    private Window _ownerWindow;
    public Book SelectedBook
    {
        get { return App.LibraryService.SelectedBook; }
    }
    public BookDetailsWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
    }
}