namespace LibraryApp.Model;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public bool IsAvailable => LoanedTo == null;
    public string Availability => IsAvailable ? "Ledig" : $"Lånt ut til: {LoanedTo.FullName}";
    public User? LoanedTo { get; set; }

    public Book(string title, string author, string genre, string description)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Description = description;
        LoanedTo = null;
    }

    public object GetBookShortDetails()
    {
        return new { Title = Title, Author = Author, Genre = Genre };
    }
}