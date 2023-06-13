using System;

namespace LibraryApp.Model;

public class Book
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public Guid? LoanedToId { get; set; }
    public string? LoanedOnDate { get; set; }
    public bool IsAvailable => LoanedToId == null;
    public string Availability => IsAvailable ? "Ledig" : "Utlånt";
    public Book()
    {

    }
    public Book(string title, string author, string genre, string description)
    {
        BookId = Guid.NewGuid();
        Title = title;
        Author = author;
        Genre = genre;
        Description = description;
    }
}