using System;

namespace LibraryApp.Model;

public class UserBookItem
{
    public Book Book { get; private set; }
    public DateOnly LoanedOn { get; private set; }
    public string Title { get; private set; }

    public UserBookItem(Book book)
    {
        Book = book;
        LoanedOn = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        Title = book.Title;
    }
}