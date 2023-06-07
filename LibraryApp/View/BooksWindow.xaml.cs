using System.Collections.ObjectModel;
using LibraryApp.Model;
using System.Linq;
using System.Windows;
using LibraryApp.ViewModel;

namespace LibraryApp.View
{
    public partial class BooksWindow : Window
    {
        private ObservableCollection<Book?> _library;
        public BooksWindow(Window mainWindow, ObservableCollection<Book?> library)
        {
            _library = library;
            Owner = mainWindow;
            InitializeComponent();
            var vm = new BooksWindowViewModel(this, _library);
            DataContext = vm;
        }
    }
}
