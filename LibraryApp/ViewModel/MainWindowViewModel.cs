using System.Collections.ObjectModel;
using System.Windows;
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
    protected ObservableCollection<User> Users;

    public MainWindowViewModel(Window ownerWindow)
    {
        _ownerWindow = ownerWindow;
        InitLists();
    }

    private void InitLists()
    {
        Books = new ObservableCollection<Book?>
        {
            new("Harry Potter og de vises stein", "J. K. Rowling", "Fantasi",
                "Harry Potter har ikke opplevd mye magi i sitt liv. Han bor i et kott under trappa hos den ekle familien Dumling, og han har ikke feiret bursdagen sin på elleve år. Men alt dette endrer seg når en ugle leverer et mystisk brev med innbydelse til Galtvort høyere skole for hekseri og trolldom ? et utrolig sted som Harry og alle som leser om ham aldri vil glemme. Her får Harry venner, og magien gjennomsyrer alt fra skoletimer til måltider. Men et skjebnesvangert møte venter ham. Vil Harry, gutten med sikksakkarret, leve opp til forventningene alle har til ham?"),
            new("Hobbiten, eller Fram og tilbake igjen", "J. R. R. Tolkien", "Fantasi",
                "Bilbo Lommelun lever et behagelig liv i hobbithullet sitt i Bakken, og det er sjelden han beveger seg lenger enn til spiskammeret. Så en dag dukker trollmannen Gandalv opp sammen med tretten dverger, og vil ha ham med på en reise «fram og tilbake igjen». Dvergenes plan er å røve den veldige skatten som voktes av Smaug, en enorm og svært farlig drage."),
            new("Tørst", "Jo Nesbø", "Drama",
                "Et drapsoffer blir funnet i sitt hjem med bitemerker i halsen. Kroppen er tappet for blod. Kan det være vamyprisme - et svært omdiskutert felt i psykiatrien. Tidligere etterforsker Harry Hole vet bedre enn noen at flere av krimhistoriens verste seriemordere har vært diagnostisert som nettopp vampyrister. Men Harry har et annet motiv for å bistå politiet - morderen som slapp unna.")
        };

        Users = new ObservableCollection<User>
        {
            new("Eli", "Rygg", "Portveien 2"),
            new("Jarl", "Goli", "Portveien 2"),
            new("Espen", "Askeladd", "Rusleveien 23"),
            new("Per", "Askeladd", "Omveien 56"),
            new("Pål", "Askeladd", "Bakveien 69")
        };
    }

    private void OpenBooksWindow()
    {
        var booksWindow = new BooksWindow(_ownerWindow, Books);
        _ownerWindow.Opacity = 0;
        booksWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }
    private void OpenUsersWindow()
    {
        var usersWindow = new UsersWindow(_ownerWindow, Books, Users);
        _ownerWindow.Opacity = 0;
        usersWindow.ShowDialog();
        _ownerWindow.Opacity = 1;
    }
}