using LibraryApp.ViewModel;
using System.Windows;

namespace LibraryApp.View
{
    public partial class LoanCardWindow : Window
    {
        public LoanCardWindow(Window ownerWindow)
        {
            Owner = ownerWindow;
            InitializeComponent();
            var LoanCardWindowViewModel = new LoanCardWindowViewModel(this);
            DataContext = LoanCardWindowViewModel;
        }
    }
}
