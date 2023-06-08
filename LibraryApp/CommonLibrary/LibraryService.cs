﻿using LibraryApp.Model;
using System.Collections.ObjectModel;

namespace LibraryApp.CommonLibrary;

public class LibraryService
{
    internal ObservableCollection<Book?> Books;
    internal ObservableCollection<User?> Users;
    internal Book? SelectedBook;
    internal User? SelectedUser;

    public LibraryService()
    {
        Books = new ObservableCollection<Book?>();
        Users = new ObservableCollection<User?>();
        PopulateLists();
    }

    private void PopulateLists()
    {
        Books.Add(new("Harry Potter og de vises stein", "J. K. Rowling", "Fantasi", "Harry Potter har ikke opplevd mye magi i sitt liv. Han bor i et kott under trappa hos den ekle familien Dumling, og han har ikke feiret bursdagen sin på elleve år. Men alt dette endrer seg når en ugle leverer et mystisk brev med innbydelse til Galtvort høyere skole for hekseri og trolldom ? et utrolig sted som Harry og alle som leser om ham aldri vil glemme. Her får Harry venner, og magien gjennomsyrer alt fra skoletimer til måltider. Men et skjebnesvangert møte venter ham. Vil Harry, gutten med sikksakkarret, leve opp til forventningene alle har til ham?"));
        Books.Add(new("Hobbiten, eller Fram og tilbake igjen", "J. R. R. Tolkien", "Fantasi", "Bilbo Lommelun lever et behagelig liv i hobbithullet sitt i Bakken, og det er sjelden han beveger seg lenger enn til spiskammeret. Så en dag dukker trollmannen Gandalv opp sammen med tretten dverger, og vil ha ham med på en reise «fram og tilbake igjen». Dvergenes plan er å røve den veldige skatten som voktes av Smaug, en enorm og svært farlig drage."));
        Books.Add(new("Tørst", "Jo Nesbø", "Drama", "Et drapsoffer blir funnet i sitt hjem med bitemerker i halsen. Kroppen er tappet for blod. Kan det være vamyprisme - et svært omdiskutert felt i psykiatrien. Tidligere etterforsker Harry Hole vet bedre enn noen at flere av krimhistoriens verste seriemordere har vært diagnostisert som nettopp vampyrister. Men Harry har et annet motiv for å bistå politiet - morderen som slapp unna."));
        Users.Add(new("Eli", "Rygg", "Portveien 2"));
        Users.Add(new("Jarl", "Goli", "Portveien 2"));
        Users.Add(new("Espen", "Askeladd", "Rusleveien 23"));
        Users.Add(new("Per", "Askeladd", "Omveien 56"));
        Users.Add(new("Pål", "Askeladd", "Bakveien 69"));

    }
}
