using System.Windows;
using LibraryApp.ViewModel;

namespace LibraryApp.View
{
    public partial class BooksWindow : Window
    {
        public BooksWindow(Window mainWindow)
        {
            Owner = mainWindow;
            InitializeComponent();
            var vm = new BooksWindowViewModel(this);
            DataContext = vm;
        }
    }
}
