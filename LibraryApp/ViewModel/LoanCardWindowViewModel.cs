using LibraryApp.CommonLibrary;
using LibraryApp.Model;
using LibraryApp.MVVM;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryApp.ViewModel;

public class LoanCardWindowViewModel : ViewModelBase
{
    public RelayCommand IssueCardCommand => new RelayCommand(execute => IssueLoanCard(), canExecute => User?.LoanCard == "Ingen");
    public RelayCommand RevokeCardCommand => new RelayCommand(execute => RevokeLoanCard(), canExecute => User?.LoanCard != "Ingen");
    public RelayCommand CloseCommand => new RelayCommand(execute => CloseWindow(_ownerWindow));
    private Window _ownerWindow;
    public User? User
    {
        get { return LibraryService.SelectedUser; }
    }
    public Guid ID => User.UserID;
    public string FullName => User.FullName;
    public string LoanCardStatus
   {
       get { return User.LoanCard; }
      
   }

   public LoanCardWindowViewModel(Window window)
    {
        _ownerWindow = window;
        }
    private async void IssueLoanCard()
    {
        User.IssueLoanCard();
        await UpdateLoanCardStatus();
        MessageBox.Show("Lånekort tildelt for 1 år");
        OnPropertyChanged(nameof(LoanCardStatus));
    }

    private async void RevokeLoanCard()
    {
        User.RevokeLoanCard();
        await UpdateLoanCardStatus();
        MessageBox.Show("Lånekort inndratt");
        OnPropertyChanged(nameof(LoanCardStatus));
    }

    private async Task UpdateLoanCardStatus()
    {
        await LibraryService.ChangeLoanCardStatus(LibraryService.SelectedUser);
        LibraryService.Users = await LibraryService.GetUsersList();
    }
}