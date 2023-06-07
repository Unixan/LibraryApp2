using LibraryApp.Model;
using LibraryApp.MVVM;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryApp.ViewModel;

public class AddBookWindowViewModel : ViewModelBase
{
    public RelayCommand AddBookCommand => new RelayCommand(execute => AddBook(),canExecute => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Author) && !string.IsNullOrWhiteSpace(SelectedOption) && !string.IsNullOrWhiteSpace(Description) );
    public RelayCommand ClearFieldsCommand => new RelayCommand(execute => EmptyFields(), canExecute => !string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Author) || !string.IsNullOrWhiteSpace(SelectedOption) || !string.IsNullOrWhiteSpace(Description));
    public RelayCommand CloseWindowCommand => new RelayCommand(execute => SureClose());

    private ObservableCollection<string> _options;
    public ObservableCollection<string> Options
    {
        get { return _options; }
        set
        {
            _options = value;
            OnPropertyChanged();
        }
    }
    private string _selectedOption;
    public string? SelectedOption
    {
        get { return _selectedOption; }
        set
        {
            _selectedOption = value;
            OnPropertyChanged();
        }
    }
    private ObservableCollection<Book?> _books;

    public ObservableCollection<Book?> Books
    {
        get { return _books; }
        set
        {
            _books = value;
            OnPropertyChanged();
        }
    }
    private string _title;
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }
    private string _author;
    public string Author
    {
        get { return _author; }
        set
        {
            _author = value;
            OnPropertyChanged();
        }
    }
    private string _description;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }
    private readonly Window _window;

    public AddBookWindowViewModel(Window window, ObservableCollection<Book?> books)
    {
        _window = window;
       _books = books;
        _options = new ObservableCollection<string>
        {
            "Barnebøker",
            "Biografi",
            "Drama",
            "Fantasi",
            "Faglitteratur",
            "Filosofi",
            "Historisk skjønnlitteratur",
            "Humor",
            "Krim",
            "Poesi",
            "Reiselitteratur",
            "Romantikk",
            "Science fiction",
            "Selvhjelp",
            "Skjønnlitteratur",
            "Skrekk",
            "Thriller",
            "Ungdomsbøker",
            "Vitenskap"
        };
    }
    private void AddBook()
    {
        Books.Add(new Book(Title, Author, SelectedOption, Description));
        MessageBox.Show("Ny bok lagt til!");
        EmptyFields();
    }
    private void SureClose()
    {
        if (!string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Author) || !string.IsNullOrWhiteSpace(SelectedOption) || !string.IsNullOrWhiteSpace(Description))
        {
            var choice = MessageBox.Show("Er du sikker? Endringene vil bli forkastet", "Endringene er ikke lagret",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (choice == MessageBoxResult.Yes) { CloseWindow(_window);}
        }
        else {CloseWindow(_window);}
    }

    private void EmptyFields()
    {
        Title = string.Empty;
        Author = string.Empty;
        SelectedOption = null;
        Description = string.Empty;
    }
}