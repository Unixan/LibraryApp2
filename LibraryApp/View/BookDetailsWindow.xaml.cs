using System.Windows;
using LibraryApp.ViewModel;

namespace LibraryApp.View
{
    public partial class BookDetailsWindow : Window
    {
        public BookDetailsWindow(Window ownerWindow)
        {
            
            Owner = ownerWindow;
            InitializeComponent();
            var bookDetailsWindowViewModel = new BookDetailsWindowViewModel(this);
            DataContext = bookDetailsWindowViewModel;
        }
    }
}
