using LibraryApp.Model;
using LibraryApp.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LibraryApp.View
{
    public partial class AddBookWindow : Window
    {
        public ObservableCollection<Book?> Books;
        private Window _window;
        public AddBookWindowViewModel Vm;

        public AddBookWindow(Window window, ObservableCollection<Book?> books)
        {
            _window = window;
            Owner = _window;
            Books = books;
            InitializeComponent();
            Vm = new AddBookWindowViewModel(this, Books);
            DataContext = Vm;
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            Vm.Description = textBox.Text;
        }
    }
}
